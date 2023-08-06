using System;
using System.Diagnostics;
using System.Drawing;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TicTacToe
{
    public partial class TicTacToe : Form
    {
        int boardSize;
        private char mySymbol;
        private char enemySymbol;
        private char currentTurn 
        {
            set
            {
                if (value == ' ')
                    currentTurnLabel.Text = "";
                else
                    currentTurnLabel.Text = $"Current Turn: {value}";
            }
        }
        private Button[,] btnArr;
        public TicTacToe()
        {
            InitializeComponent();
            client = new TcpClient();
        }
        string serverAddr = "localhost";
        Int32 port = 65500;
        TcpClient client;
        async Task ReadServerBoardAsync()
        {
            Byte[] dataBoardSize = await ReadDataAsync(client.GetStream(), 4);
            boardSize = BitConverter.ToInt32(dataBoardSize, 0);
            initializeBoard(boardSize);
        }
        private async void connectToServerAsync(object sender, EventArgs e)
        {
            try
            {
                await client.ConnectAsync(serverAddr, port);
                waitingForPlayerText.Text = "Waiting for another player...";
                Controls.Remove(connectButton);
                Controls.Remove(ipBox);
                Controls.Remove(ipText);
                Controls.Remove(portBox);
                Controls.Remove(portText);

                await ReadServerBoardAsync();

                ServerListenLoopAsync();
            }
            catch { Console.WriteLine("Invalid IP or port?"); }
        }
        async void ServerListenLoopAsync()
        {
            while (true)
            {
                Byte[] data = await ReadDataAsync(client.GetStream());
                string ins = Encoding.ASCII.GetString(data);

                if (ins.Contains("VALIDATEDMOVE"))
                    ExecuteValidatedMove(ins);
                else if (ins.Contains("INVALIDMOVE"))
                    ExecuteInvalidMove(ins);
                else if (ins.Contains("DRAW"))
                    ExecuteDraw();
                else if (ins.Contains("WON"))
                    ExecuteWon(ins);
                else if (ins.Contains("RESET"))
                    ExecuteReset();
                else if (ins.Contains("first"))
                    initializeFirstPlayer();
                else if (ins.Contains("second"))
                    initializeSecondPlayer();
            }
        }
        void ExecuteReset()
        {
            winnerLabel.Text = "";
            initializeBoard(boardSize);
            DisableBoard();
        }
        void ExecuteInvalidMove(string ins)
        {
            Console.WriteLine(ins.Split(':')[1][0]);
            Console.WriteLine(mySymbol);
            if (ins.Split(':')[1][0] == mySymbol)
                EnableBoard();
        }
        void ExecuteDraw()
        {
            DisableBoard();
            resetButton.Enabled = true;
            winnerLabel.Text = "Draw!";
        }
        void ExecuteWon(string ins)
        {
            DisableBoard();
            resetButton.Enabled = true;
            char playerWon = ins.Split(':')[1][0];
            winnerLabel.Text = $"{playerWon} Won!";
        }
        void ExecuteValidatedMove(string ins)
        {
            int xValue = int.Parse(ins.Split(',')[1][0].ToString()), yValue = int.Parse(ins.Split(',')[0][ins.Split(',')[0].Length - 1].ToString());
            char player = ins.Split(':')[1][0];

            btnArr[yValue, xValue].Text = player.ToString();
            currentTurn = (player == 'X') ? 'O' : 'X';
            if (mySymbol != player)
            {
                EnableBoard();
            }  
        }
        void initializeFirstPlayer()
        {
            waitingForPlayerText.Text = "";
            mySymbol = 'X';
            enemySymbol = 'O';
            playerSymbol.Text = $"You Are: {mySymbol}";
            currentTurn = mySymbol;
            AddToControls();
            EnableBoard();
        }
        void initializeSecondPlayer()
        {
            waitingForPlayerText.Text = "";
            enemySymbol = 'X';
            mySymbol = 'O';
            playerSymbol.Text = $"You Are: {mySymbol}";
            currentTurn = enemySymbol;
            AddToControls();
            DisableBoard();
        }
        void AddToControls()
        {
            waitingForPlayerText.Text = "";
            foreach (Button b in btnArr)
                Controls.Add(b);
        }
        void EnableBoard()
        {
            foreach (Button b in btnArr)
                if (b.Text == "")
                    b.Enabled = true;
        }
        void DisableBoard()
        {
            foreach (Button b in btnArr)
                b.Enabled = false;
        }
        private void initializeBoard(int boardSize)
        {
            if (btnArr != null)
            {
                waitingForPlayerText.Text = "Waiting for another player...";
                currentTurn = ' ';
                playerSymbol.Text = "";

                foreach (Button b in btnArr)
                    Controls.Remove(b);
            }
            resetButton.Enabled = false;
            btnArr = new Button[boardSize, boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                for (int k = 0; k < boardSize; k++)
                {
                    btnArr[i, k] = new Button();
                    btnArr[i, k].Name = $"{i},{k}";
                    btnArr[i, k].Height = 250 / boardSize;
                    btnArr[i, k].Width = 250 / boardSize;
                    btnArr[i, k].Location = new Point(k * (btnArr[i, k].Width + 10), i * (btnArr[i, k].Height + 10));
                    btnArr[i, k].Click += new EventHandler(GameButton);
                }
            }
        }
        private void GameButton(object sender, EventArgs e)
        {
            string ins =  $"CHECKMOVE {((Button)sender).Name}:{mySymbol}";
            Byte[] data = Encoding.ASCII.GetBytes(ins);
            WriteData(client.GetStream(), data);
            DisableBoard();
        }
        async Task<Byte[]> ReadDataAsync(NetworkStream s)
        {
            Byte[] dataSize = new Byte[4];
            await s.ReadAsync(dataSize, 0, 4);

            int size = BitConverter.ToInt32(dataSize, 0);
            Byte[] data = new Byte[size];
            await s.ReadAsync(data, 0, data.Length);
            return data;
        }
        async Task<Byte[]> ReadDataAsync(NetworkStream s, int length)
        {
            Byte[] data = new Byte[length];
            await s.ReadAsync(data, 0, data.Length);
            return data;
        }
        void WriteData(NetworkStream s, Byte[] data)
        {
            Byte[] dataSize = BitConverter.GetBytes(data.Length);
            s.Write(dataSize, 0, 4);
            s.Write(data, 0, data.Length);
        }
        private void IpTextChanged(object sender, EventArgs e)
        {
            serverAddr = ((TextBox)sender).Text;
        }
        private void PortTextChanged(object sender, EventArgs e)
        {
             int.TryParse(((TextBox)sender).Text, out port);
        }
        private void ResetGameButton(object sendyer, EventArgs e)
        {
            string msg = "RESET";
            Byte[] data = Encoding.ASCII.GetBytes(msg);
            WriteData(client.GetStream(), data);
        }
        private void LeaveGameButton(object sender, EventArgs e)
        {
            client.Close();
            Close();
        }
        private void LeaveGameButtonAfter(object sender, EventArgs e)
        {
            client.Close();
        }
    }
}
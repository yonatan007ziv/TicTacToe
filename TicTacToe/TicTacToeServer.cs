using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Diagnostics;

namespace TicTacToe
{
    public partial class TicTacToeServer : Form
    {
        TcpListener server;
        TcpClient firstPlayer;
        TcpClient secondPlayer;
        int port = 65500;
        private bool listenToP1, listenToP2;
        private async void startServer(object sender, EventArgs e)
        {
            try
            {
                IPAddress localAddr = IPAddress.Any;
                LogToConsole($"Starting a TCP Listener on (ip, port) ({localAddr}, {port})");
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();
                LogToConsole($"Started a TCP Listener on port {port}\n");

                for (int i = 0; i < 2; i++)
                {
                    if (i == 0)
                        await WaitForPlayerAsync("first");
                    else if (i == 1)
                        await WaitForPlayerAsync("second");
                }
                NetworkStream streamP1 = firstPlayer.GetStream();
                NetworkStream streamP2 = secondPlayer.GetStream();

                // make server simulated array
                initializeSimulatedButtonArray(boardLength);

                // write player order
                RandomPlayerOrder();
                InstructionListeningLoopAsync();
            }
            catch (SocketException SE)
            {
                Console.WriteLine("SocketException: {0}", SE);
            }
        }
        async Task WaitForPlayerAsync(string player)
        {
            // write board size
            Byte[] sizeData = BitConverter.GetBytes(boardLength);

            LogToConsole("Waiting for player...");
            if (player == "first")
            {
                firstPlayer = await server.AcceptTcpClientAsync();
                WriteData(firstPlayer.GetStream(), sizeData, 4);
            }
            else if (player == "second")
            {
                secondPlayer = await Task.Run(() => server.AcceptTcpClientAsync());
                WriteData(secondPlayer.GetStream(), sizeData, 4);
            }
            LogToConsole("Connected Player!");
        }
        void RandomPlayerOrder()
        {
            if (!IsConnected(firstPlayer) || !IsConnected(secondPlayer))
                return;
            Random rand = new Random();
            Byte[] dataFirst = Encoding.ASCII.GetBytes("first");
            Byte[] dataSecond = Encoding.ASCII.GetBytes("second");
            if (rand.Next(2) == 0)
            {
                LogToConsole("Settings firstPlayer as X");
                WriteData(firstPlayer.GetStream(), dataFirst);
                listenToP1 = true;
                listenToP2 = false;
                LogToConsole("Settings secondPlayer as O");
                WriteData(secondPlayer.GetStream(), dataSecond);
            }
            else
            {
                LogToConsole("Settings secondPlayer as X");
                WriteData(secondPlayer.GetStream(), dataFirst);
                listenToP1 = false;
                listenToP2 = true;
                LogToConsole("Settings firstPlayer as O");
                WriteData(firstPlayer.GetStream(), dataSecond);
            }
        }
        public bool IsConnected(TcpClient _TcpClient)
        {
            try
            {
                string msg = "HEARTBEAT";
                Byte[] data = Encoding.ASCII.GetBytes(msg);
                WriteData(_TcpClient.GetStream(), data);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async void InstructionListeningLoopAsync()
        {
            while (true)
            {
                bool shouldContinue = false;
                if (!IsConnected(firstPlayer))
                {
                    resetInstruction();
                    await WaitForPlayerAsync("first");
                    RandomPlayerOrder();
                    shouldContinue = true;
                }
                if (!IsConnected(secondPlayer))
                {
                    resetInstruction();
                    await WaitForPlayerAsync("second");
                    RandomPlayerOrder();
                    shouldContinue = true;
                }
                if (shouldContinue)
                    continue;
                string msgInsP1 = "", msgInsP2 = "";
                if (firstPlayer.GetStream().DataAvailable)
                {
                    Byte[] dataInsP1 = await ReadDataAsync(firstPlayer.GetStream());
                    msgInsP1 = Encoding.ASCII.GetString(dataInsP1);
                }
                if (secondPlayer.GetStream().DataAvailable)
                {
                    Byte[] dataInsP2 = await ReadDataAsync(secondPlayer.GetStream());
                    msgInsP2 = Encoding.ASCII.GetString(dataInsP2);
                }

                if (msgInsP1 == "RESET" || msgInsP2 == "RESET")
                {
                    TryInstruction("RESET");
                    continue;
                }

                if (msgInsP1 != "" && listenToP1)
                {
                    TryInstruction(msgInsP1);
                    listenToP1 = false;
                    listenToP2 = true;
                }
                if (msgInsP2 != "" && listenToP2)
                {
                    TryInstruction(msgInsP2);
                    listenToP1 = true;
                    listenToP2 = false;
                }
                await Task.Delay(10);
            }
        }
        private void TryInstruction(string ins)
        {
            if (ins.Contains("CHECKMOVE"))
            {
                moveInstruction(ins);
                TryWin();
            }
            else if (ins.Contains("RESET"))
            {
                resetInstruction();
                RandomPlayerOrder();
            }
        }
        void resetInstruction()
        {
            LogToConsole($"\n RESETTING THE BOARD\n");

            string msg = "RESET";
            Byte[] data = Encoding.ASCII.GetBytes(msg);

            if (firstPlayer.Connected)
            WriteData(firstPlayer.GetStream(), data);
            if (secondPlayer.Connected)
                WriteData(secondPlayer.GetStream(), data);
            initializeSimulatedButtonArray(boardLength);
        }
        void moveInstruction(string ins)
        {
            LogToConsole($"VALIDATING instruction: {ins}");
            int xValue = int.Parse(ins.Split(',')[1][0].ToString()), yValue = int.Parse(ins.Split(',')[0][ins.Split(',')[0].Length - 1].ToString());
            char player = ins.Split(':')[1][0];

            if (!isValid(yValue, xValue))
            {
                Byte[] invalidMoveData = Encoding.ASCII.GetBytes($"INVALIDMOVE:{player}");
                WriteInvalidMove(xValue, yValue, player);
                WriteData(firstPlayer.GetStream(), invalidMoveData);
                WriteData(secondPlayer.GetStream(), invalidMoveData);
                return;
            }

            simulatedButtonArray[yValue, xValue].Text = player.ToString();
            LogToConsole($"VALIDATED instruction: {ins}");
            WriteValidMove(xValue, yValue, player);
        }
        void WriteInvalidMove(int x, int y, char player)
        {
            Byte[] invalidMoveData = Encoding.ASCII.GetBytes($"INVALIDMOVE:{player}");
            WriteData(firstPlayer.GetStream(), invalidMoveData);
            WriteData(secondPlayer.GetStream(), invalidMoveData);
        }
        void WriteValidMove(int x, int y, char player)
        {
            Byte[] moveData = Encoding.ASCII.GetBytes($"VALIDATEDMOVE {y},{x}:{player}");
            WriteData(firstPlayer.GetStream(), moveData);
            WriteData(secondPlayer.GetStream(), moveData);
        }
        public void LogToConsole(string msg)
        {
            ServerLog.Text += msg + Environment.NewLine;
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
        void WriteData(NetworkStream s, Byte[] data)
        {
            Byte[] dataSize = BitConverter.GetBytes(data.Length);
            s.Write(dataSize, 0, 4);
            s.Write(data, 0, data.Length);
        }
        void WriteData(NetworkStream s, Byte[] data, int length)
        {
            s.Write(data, 0, length);
        }
        int boardLength = 3;
        Button[,] simulatedButtonArray;
        public TicTacToeServer()
        {
            InitializeComponent();
        }
        private void initializeSimulatedButtonArray(int boardSize)
        {
            simulatedButtonArray = new Button[boardSize, boardSize];
            for (int i = 0; i < boardSize; i++)
            {
                for (int k = 0; k < boardSize; k++)
                {
                    simulatedButtonArray[i, k] = new Button();
                    simulatedButtonArray[i, k].Name = $"{i},{k}";
                    simulatedButtonArray[i, k].Location = new Point(k * (simulatedButtonArray[i, k].Width + 10), i * (simulatedButtonArray[i, k].Height + 10));
                }
            }
        }
        /// <returns>"X" if x won, "O" if o won, "" if game hasn't ended, "Draw" if no one won</returns>
        private string CheckForWin()
        {
            string last = "";

            //check rows
            for (int i = 0; i < simulatedButtonArray.GetLength(0); i++)
            {
                int counter = 0;
                string current = simulatedButtonArray[i, 0].Text;
                for (int k = 0; k < simulatedButtonArray.GetLength(1); k++)
                {

                    if (simulatedButtonArray[i, k].Text == current && current != "")
                        counter++;
                }
                if (counter == boardLength)
                    return current;
            }

            //check columns
            for (int k = 0; k < simulatedButtonArray.GetLength(1); k++)
            {
                int counter = 0;
                string current = simulatedButtonArray[0, k].Text;
                for (int i = 0; i < simulatedButtonArray.GetLength(0); i++)
                {
                    if (simulatedButtonArray[i, k].Text == current && current != "")
                        counter++;
                }
                if (counter == boardLength)
                    return current;
            }

            //check diagonally
            int Counter = 0;
            string Current = simulatedButtonArray[0, 0].Text;
            for (int i = 0; i < boardLength; i++)
                if (simulatedButtonArray[i, i].Text == Current && Current != "")
                    Counter++;
            if (Counter == boardLength)
                return Current;

            Counter = 0;
            Current = simulatedButtonArray[simulatedButtonArray.GetLength(0) - 1, 0].Text;
            for (int i = 0; i < boardLength; i++)
                if (simulatedButtonArray[simulatedButtonArray.GetLength(0) - 1 - i, i].Text == Current && Current != "")
                    Counter++;
            if (Counter == boardLength)
                return Current;

            //check for draw
            bool isThereEmpty = false;
            foreach (Button b in simulatedButtonArray)
                if (b.Text == "")
                    isThereEmpty = true;
            if (!isThereEmpty)
                return "Draw";

            return last;
        }
        void TryWin()
        {
            string result = CheckForWin();
            if (result != "")
            {
                Byte[] data;
                if (result == "Draw")
                {
                    LogToConsole("Draw");
                    data = Encoding.ASCII.GetBytes($"DRAW");
                }
                else
                {
                    LogToConsole($"{result} won");
                    data = Encoding.ASCII.GetBytes($"WON:{result}");
                }
                WriteData(firstPlayer.GetStream(), data);
                WriteData(secondPlayer.GetStream(), data);
            }
        }
        private void OnSizeChanged(object sender, EventArgs e)
        {
            boardLength = int.Parse(sizeInput.Text);
        }
        private void portTextBoxChanged(object sender, EventArgs e)
        {
            int.TryParse(((TextBox)sender).Text, out port);
        }

        bool isValid(int yValue, int xValue)
        {
            return simulatedButtonArray[yValue, xValue].Text == "";
        }
    }
}
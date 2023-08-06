using System.Net;
using System.Net.Sockets;
using TicTacToe.Client.Controls;
using TicTacToe.Library.Data;
using TicTacToe.Library.MessageCodes;

namespace TicTacToe.Client.Handlers
{
    public class PlayerHandler
    {
        private readonly ClientHandler clientHandler;
        private readonly GameBoard boardView;

        public char Symbol { get; private set; }

        public PlayerHandler(GameBoard boardView, IPAddress ip, int port)
        {
            TcpClient socket = new TcpClient();
            socket.Connect(ip, port);

            this.boardView = boardView;
            clientHandler = new ClientHandler(socket, InterpretMessage);
        }

        public void SendMessage(string msg)
        {
            clientHandler.WriteAsync(msg);
        }

        public void InterpretMessage(string msg)
        {
            CommunicationMessage operation = 0;

            bool hasParameter = false;
            CommunicationParameter parameter = 0;
            
            if (msg.Contains(':'))
            {
                operation = (CommunicationMessage)Enum.Parse(typeof(CommunicationMessage), msg.Split(':')[0]);
                hasParameter = Enum.TryParse(typeof(CommunicationParameter), msg.Split(':')[1],false, out _);
            }
            else
                operation = (CommunicationMessage)Enum.Parse(typeof(CommunicationMessage), msg);

            if (hasParameter)
                parameter = (CommunicationParameter)Enum.Parse(typeof(CommunicationParameter), msg.Split(':')[1]);

            switch (operation)
            {
                case CommunicationMessage.Place:
                    break;
                case CommunicationMessage.PlayAgain:
                    boardView.ResetBoard();
                    break;
                case CommunicationMessage.NotifyWaiting:
                    boardView.WaitingForPlayersText("Waiting for other Player...");
                    boardView.ResetBoard();
                    break;
                case CommunicationMessage.DisableNotifyWaiting:
                    boardView.WaitingForPlayersText("");
                    break;
                case CommunicationMessage.PlayerAssignment:
                    switch (parameter)
                    {
                        case CommunicationParameter.PlayerX:
                            Symbol = 'X';
                            boardView.localSymbol = 'X';
                            break;
                        case CommunicationParameter.PlayerO:
                            Symbol = 'O';
                            boardView.localSymbol = 'O';
                            break;
                    }
                    break;
                case CommunicationMessage.YourTurn:
                    boardView.EnableEmptyButtons();
                    break;
                case CommunicationMessage.OpponentTurn:
                    boardView.DisableButtons();
                    break;
                case CommunicationMessage.InvalidMoveWrongTurn:
                    boardView.Warning("Not your Turn");
                    break;
                case CommunicationMessage.InvalidMove:
                    boardView.Warning("Invalid Move");
                    break;
                case CommunicationMessage.Tie:
                    boardView.GameResult('T');
                    break;
                case CommunicationMessage.XWon:
                    boardView.GameResult('X');
                    break;
                case CommunicationMessage.OWon:
                    boardView.GameResult('O');
                    break;
                case CommunicationMessage.PlacedX:
                    boardView.UpdateVisual('X', Vector2.FromString(msg.Split(':')[1]));
                    break;
                case CommunicationMessage.PlacedO:
                    boardView.UpdateVisual('O', Vector2.FromString(msg.Split(':')[1]));
                    break;
            }
        }

        public void TryPlace(Vector2 pos)
        {
            SendMessage($"{CommunicationMessage.Place}:{pos}");
        }

        public void Disconnect()
        {
            clientHandler.Disconnect();
        }
    }
}
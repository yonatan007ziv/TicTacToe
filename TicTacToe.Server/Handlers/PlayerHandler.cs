using System.Net.Sockets;
using TicTacToe.Library.Data;
using TicTacToe.Library.MessageCodes;
using TicTacToe.Library.Systems;

namespace TicTacToe.Library.Handlers
{
    internal class PlayerHandler
    {
        private readonly ClientHandler clientHandler;
        private readonly Lobby connectedLobby;

        public char Symbol { get; private set; } = '\0';

        public PlayerHandler(TcpClient socket, Lobby connectedLobby)
        {
            Console.WriteLine("New PlayerHandler");
            clientHandler = new ClientHandler(socket, InterpretMessage);
            clientHandler.OnDisconnected += OnDisconnected;
            this.connectedLobby = connectedLobby;
            this.connectedLobby.AddPlayer(this);
        }

        public void OnDisconnected()
        {
            Console.WriteLine("PlayerHandler Disconnected");
            connectedLobby.PlayerDisconnected(this);
        }

        public void SendMessage(string msg)
        {
            Console.WriteLine($"PlayerHandler SendMessage: {msg}");
            clientHandler.WriteAsync(msg);
        }

        public void InterpretMessage(string msg)
        {
            Console.WriteLine($"PlayerHandler InterpretMessage: {msg}");

            CommunicationMessage operation = 0;

            bool hasParameter = false;
            CommunicationParameter parameter = 0;

            if (msg.Contains(':'))
            {
                operation = (CommunicationMessage)Enum.Parse(typeof(CommunicationMessage), msg.Split(':')[0]);
                hasParameter = Enum.TryParse(typeof(CommunicationParameter), msg.Split(':')[1], false, out _);
            }
            else
                operation = (CommunicationMessage)Enum.Parse(typeof(CommunicationMessage), msg);

            if (hasParameter)
                parameter = (CommunicationParameter)Enum.Parse(typeof(CommunicationParameter), msg.Split(':')[1]);

            switch (operation)
            {
                case CommunicationMessage.Place:
                    TryPlace(Vector2.FromString(msg.Split(':')[1]));
                    break;
                case CommunicationMessage.PlayAgain:
                    connectedLobby.PlayAgain();
                    break;
            }
        }

        private void TryPlace(Vector2 pos)
        {
            connectedLobby.PlayerPlace(this, pos);
        }

        public void SetSymbol(char p)
        {
            Symbol = p;
        }
    }
}
using System.Net;
using System.Net.Sockets;
using TicTacToe.Library.Handlers;

namespace TicTacToe.Library.Systems
{
    public class Server
    {
        private const string ip = "127.0.0.1";
        private const int port = 65500;

        private readonly TcpListener listener = new TcpListener(IPAddress.Parse(ip), port);
        private readonly LobbyRouteHandler lobbyRouter = new LobbyRouteHandler();

        public async Task ServerLoop()
        {
            listener.Start();
            await ConnectPlayersAsync();
        }

        private async Task ConnectPlayersAsync()
        {
            while (true)
                _ = new PlayerHandler(await listener.AcceptTcpClientAsync(), lobbyRouter.FindLobby());
        }
    }
}
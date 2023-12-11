using System.Net;
using System.Net.Sockets;
using TicTacToe.Server.Handlers;

namespace TicTacToe.Server.Systems
{
	public class Server
	{
		private const string ip = "127.0.0.1";
		private const int port = 65500;

		private readonly TcpListener listener = new TcpListener(IPAddress.Parse(ip), port);
		private readonly LobbyRouteHandler lobbyRouter = new LobbyRouteHandler();

		public Server()
		{
			listener.Start();
		}

		public async Task ServerLoop()
		{
			while (true)
				_ = new PlayerMessagesHandler(await listener.AcceptTcpClientAsync(), lobbyRouter.FindLobby());
		}
	}
}
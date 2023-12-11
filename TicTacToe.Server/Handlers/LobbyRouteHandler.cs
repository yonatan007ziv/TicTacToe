using TicTacToe.Server.Systems;

namespace TicTacToe.Server.Handlers
{
	internal class LobbyRouteHandler
	{
		private readonly List<Lobby> lobbies = new List<Lobby>();

		public Lobby FindLobby()
		{
			foreach (Lobby lobby in lobbies)
				if (lobby.PlayerCount < 2)
					return lobby;

			Lobby newLobby = new Lobby(this);
			lobbies.Add(newLobby);
			return newLobby;
		}

		public void RemoveLobby(Lobby lobby)
		{
			lobbies.Remove(lobby);
		}
	}
}
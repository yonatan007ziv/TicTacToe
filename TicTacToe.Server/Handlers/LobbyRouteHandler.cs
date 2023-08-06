using TicTacToe.Library.Systems;

namespace TicTacToe.Library.Handlers
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
        
        public void NotifyEmptyLobby(Lobby lobby)
        {
            lobbies.Remove(lobby);
        }
    }
}
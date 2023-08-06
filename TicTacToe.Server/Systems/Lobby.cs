using TicTacToe.Library.Data;
using TicTacToe.Library.Data.Enums;
using TicTacToe.Library.Handlers;
using TicTacToe.Library.MessageCodes;

namespace TicTacToe.Library.Systems
{
    internal class Lobby
    {
        private readonly GameFlow gameFlow = new GameFlow();
        private readonly LobbyRouteHandler lobbyRouter;

        public int PlayerCount { get; private set; }

        private PlayerHandler? x, o;

        public Lobby(LobbyRouteHandler lobbyRouter)
        {
            this.lobbyRouter = lobbyRouter;
        }

        public void PlayerPlace(PlayerHandler player, Vector2 pos)
        {
            if (gameFlow.CurrentTurn != player.Symbol)
            {
                player.SendMessage($"{CommunicationMessage.InvalidMoveWrongTurn}");
                return;
            }

            MoveResult result = gameFlow.PlayerMove(pos);

            if (result == MoveResult.InvalidMove)
                player.SendMessage($"{CommunicationMessage.InvalidMove}");

            NotifyMove(player.Symbol, pos);

            switch (result)
            {
                case MoveResult.XWon:
                    NotifyLobbyWon('X');
                    break;
                case MoveResult.OWon:
                    NotifyLobbyWon('O');
                    break;
                case MoveResult.Tie:
                    NotifyLobbyWon('T');
                    break;
                case MoveResult.NoResult:
                    UpdateTurns();
                    break;
            }
        }

        public void AddPlayer(PlayerHandler player)
        {
            if (x == null)
            {
                if (new Random().Next(2) == 0 || o != null)
                    x = player;
                else
                    o = player;
            }
            else if (o == null)
            {
                if (new Random().Next(2) == 0 || x != null)
                    o = player;
                else
                    x = player;
            }
            else
                throw new Exception("More than 2 Players Tried to Connect to the Lobby!");

            PlayerCount++;
            if (player == x)
            {
                player.SetSymbol('X');
                player.SendMessage($"{CommunicationMessage.PlayerAssignment}:{CommunicationParameter.PlayerX}");
            }
            else if (player == o)
            {
                player.SetSymbol('O');
                player.SendMessage($"{CommunicationMessage.PlayerAssignment}:{CommunicationParameter.PlayerO}");
            }

            if (PlayerCount == 2)
                UpdateTurns();

            NotifyWaitingForPlayer();
        }

        private void UpdateTurns()
        {
            if (gameFlow.CurrentTurn == 'X')
            {
                x?.SendMessage($"{CommunicationMessage.YourTurn}");
                o?.SendMessage($"{CommunicationMessage.OpponentTurn}");
            }
            else if (gameFlow.CurrentTurn == 'O')
            {
                x?.SendMessage($"{CommunicationMessage.OpponentTurn}");
                o?.SendMessage($"{CommunicationMessage.YourTurn}");
            }
        }

        public void PlayAgain()
        {
            x?.SendMessage($"{CommunicationMessage.PlayAgain}");
            o?.SendMessage($"{CommunicationMessage.PlayAgain}");

            gameFlow.ResetBoard();
            gameFlow.RandomizeTurn();

            UpdateTurns();
        }

        public void PlayerDisconnected(PlayerHandler playerHandler)
        {
            PlayerCount--;
            if (x == playerHandler)
                x = null;
            else if (o == playerHandler)
                o = null;

            if (PlayerCount == 0)
                lobbyRouter.NotifyEmptyLobby(this);

            gameFlow.ResetBoard();
            NotifyWaitingForPlayer();
        }

        private void NotifyMove(char symbol, Vector2 pos)
        {
            if (symbol == 'X')
            {
                x?.SendMessage($"{CommunicationMessage.PlacedX}:{pos}");
                o?.SendMessage($"{CommunicationMessage.PlacedX}:{pos}");
            }
            else
            {
                x?.SendMessage($"{CommunicationMessage.PlacedO}:{pos}");
                o?.SendMessage($"{CommunicationMessage.PlacedO}:{pos}");
            }
        }

        private void NotifyLobbyWon(char w)
        {
            switch (w)
            {
                case 'T':
                    x?.SendMessage($"{CommunicationMessage.Tie}");
                    o?.SendMessage($"{CommunicationMessage.Tie}");
                    break;
                case 'X':
                    x?.SendMessage($"{CommunicationMessage.XWon}");
                    o?.SendMessage($"{CommunicationMessage.XWon}");
                    break;
                case 'O':
                    x?.SendMessage($"{CommunicationMessage.OWon}");
                    o?.SendMessage($"{CommunicationMessage.OWon}");
                    break;
            }
        }

        private void NotifyWaitingForPlayer()
        {
            if (x != null && o == null)
                NotifyWaitingForPlayer(x);
            else if (x == null && o != null)
                NotifyWaitingForPlayer(o);
            else if (x != null && o != null)
                DisableNotifyWaitingForPlayer();
                
        }

        private void DisableNotifyWaitingForPlayer()
        {
            x?.SendMessage($"{CommunicationMessage.DisableNotifyWaiting}");
            o?.SendMessage($"{CommunicationMessage.DisableNotifyWaiting}");
        }

        private static void NotifyWaitingForPlayer(PlayerHandler toNotify)
        {
            toNotify.SendMessage($"{CommunicationMessage.NotifyWaiting}");
        }
    }
}
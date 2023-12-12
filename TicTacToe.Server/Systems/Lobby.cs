using TicTacToe.Library.Data;
using TicTacToe.Library.MessageCodes;
using TicTacToe.Server.Data.Enums;
using TicTacToe.Server.Handlers;

namespace TicTacToe.Server.Systems;

internal class Lobby
{
	private readonly GameState gameState = new GameState();
	private readonly LobbyRouteHandler lobbyRouter;

	public int PlayerCount { get; private set; }

	private PlayerMessagesHandler? x, o;

	public Lobby(LobbyRouteHandler lobbyRouter)
	{
		this.lobbyRouter = lobbyRouter;
	}

	public void PlayerPlace(PlayerMessagesHandler player, Vector2 pos)
	{
		if (gameState.CurrentTurn != player.Symbol)
		{
			player.SendMessage(CommunicationMessage.InvalidMoveWrongTurn);
			return;
		}

		MoveResult result = gameState.PlayerMove(pos);

		if (result == MoveResult.InvalidMove)
		{
			player.SendMessage(CommunicationMessage.InvalidMove);
			return;
		}

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

	public void AddPlayer(PlayerMessagesHandler player)
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
			player.SendMessage(CommunicationMessage.PlayerAssignment, CommunicationParameter.PlayerX);
		}
		else if (player == o)
		{
			player.SetSymbol('O');
			player.SendMessage(CommunicationMessage.PlayerAssignment, CommunicationParameter.PlayerO);
		}

		if (PlayerCount == 2)
			UpdateTurns();

		NotifyWaitingForPlayer();
	}

	private void UpdateTurns()
	{
		if (gameState.CurrentTurn == 'X')
		{
			x?.SendMessage(CommunicationMessage.CurrentTurn, CommunicationParameter.PlayerX);
			o?.SendMessage(CommunicationMessage.CurrentTurn, CommunicationParameter.PlayerX);
		}
		else if (gameState.CurrentTurn == 'O')
		{
			x?.SendMessage(CommunicationMessage.CurrentTurn, CommunicationParameter.PlayerO);
			o?.SendMessage(CommunicationMessage.CurrentTurn, CommunicationParameter.PlayerO);
		}
	}

	public void PlayAgain()
	{
		x?.SendMessage(CommunicationMessage.PlayAgain);
		o?.SendMessage(CommunicationMessage.PlayAgain);

		gameState.ResetBoard();
		gameState.RandomizeTurn();

		UpdateTurns();
	}

	public void PlayerDisconnected(PlayerMessagesHandler playerHandler)
	{
		PlayerCount--;
		if (x == playerHandler)
			x = null;
		else if (o == playerHandler)
			o = null;

		if (PlayerCount == 0)
			lobbyRouter.RemoveLobby(this);

		gameState.ResetBoard();
		NotifyWaitingForPlayer();
	}

	private void NotifyMove(char symbol, Vector2 pos)
	{
		if (symbol == 'X')
		{
			x?.SendMessage(CommunicationMessage.Placed, CommunicationParameter.PlayerX, pos.X, pos.Y);
			o?.SendMessage(CommunicationMessage.Placed, CommunicationParameter.PlayerX, pos.X, pos.Y);
		}
		else
		{
			x?.SendMessage(CommunicationMessage.Placed, CommunicationParameter.PlayerO, pos.X, pos.Y);
			o?.SendMessage(CommunicationMessage.Placed, CommunicationParameter.PlayerO, pos.X, pos.Y);
		}
	}

	private void NotifyLobbyWon(char w)
	{
		switch (w)
		{
			case 'T':
				x?.SendMessage(CommunicationMessage.Tie);
				o?.SendMessage(CommunicationMessage.Tie);
				break;
			case 'X':
				x?.SendMessage(CommunicationMessage.Won, CommunicationParameter.PlayerX);
				o?.SendMessage(CommunicationMessage.Won, CommunicationParameter.PlayerX);
				break;
			case 'O':
				x?.SendMessage(CommunicationMessage.Won, CommunicationParameter.PlayerO);
				o?.SendMessage(CommunicationMessage.Won, CommunicationParameter.PlayerO);
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
		x?.SendMessage(CommunicationMessage.DisableNotifyWaiting);
		o?.SendMessage(CommunicationMessage.DisableNotifyWaiting);
	}

	private static void NotifyWaitingForPlayer(PlayerMessagesHandler toNotify)
	{
		toNotify.SendMessage(CommunicationMessage.NotifyWaiting);
	}
}
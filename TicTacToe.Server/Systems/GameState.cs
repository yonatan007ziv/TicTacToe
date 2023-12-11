using TicTacToe.Library.Data;
using TicTacToe.Server.Data;
using TicTacToe.Server.Data.Enums;

namespace TicTacToe.Server.Systems
{
	internal class GameState
	{
		private readonly GameBoard board;

		public char CurrentTurn { get; private set; }

		public GameState()
		{
			board = new GameBoard();

			// Random Starting Turn
			RandomizeTurn();
		}

		public void ResetBoard()
		{
			board.ResetBoard();
		}

		public MoveResult PlayerMove(Vector2 pos)
		{
			if (!board.ValidPos(pos))
				return MoveResult.InvalidMove;

			board.PlayerMove(CurrentTurn, pos);
			CurrentTurn = CurrentTurn == 'O' ? 'X' : 'O';
			return board.CheckBoardState();
		}

		public void RandomizeTurn()
		{
			CurrentTurn = new Random().Next(2) == 0 ? 'X' : 'O';
		}
	}
}
using TicTacToe.Library.Data;
using TicTacToe.Library.Data.Enums;

namespace TicTacToe.Library.Systems
{
    internal class GameFlow
    {
        private readonly GameBoard board;

        public char CurrentTurn { get; private set; }

        public GameFlow()
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
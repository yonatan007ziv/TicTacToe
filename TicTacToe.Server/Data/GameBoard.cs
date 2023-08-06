using System.Collections.ObjectModel;
using TicTacToe.Library.Data.Enums;

namespace TicTacToe.Library.Data
{
    public class GameBoard
    {
        private readonly char[] board = {
            '\0', '\0', '\0',
            '\0', '\0', '\0',
            '\0', '\0', '\0' };

        public void PlayerMove(char player, Vector2 pos)
        {
            int index = pos.X + pos.Y * 3;
            board[index] = player;
        }

        public MoveResult CheckBoardState()
        {
            // Columns
            if (board[0] != '\0' && board[0] == board[3] && board[3] == board[6])
                return board[0] == 'X' ? MoveResult.XWon : MoveResult.OWon;
            else if (board[1] != '\0' && board[1] == board[4] && board[4] == board[7])
                return board[1] == 'X' ? MoveResult.XWon : MoveResult.OWon;
            else if (board[2] != '\0' && board[2] == board[5] && board[5] == board[8])
                return board[2] == 'X' ? MoveResult.XWon : MoveResult.OWon;

            // Rows
            if (board[0] != '\0' && board[0] == board[1] && board[1] == board[2])
                return board[0] == 'X' ? MoveResult.XWon : MoveResult.OWon;
            else if (board[3] != '\0' && board[3] == board[4] && board[4] == board[5])
                return board[3] == 'X' ? MoveResult.XWon : MoveResult.OWon;
            else if (board[6] != '\0' && board[6] == board[7] && board[7] == board[8])
                return board[6] == 'X' ? MoveResult.XWon : MoveResult.OWon;

            // Diagonals
            if (board[0] != '\0' && board[0] == board[4] && board[4] == board[8])
                return board[0] == 'X' ? MoveResult.XWon : MoveResult.OWon;
            else if (board[2] != '\0' && board[2] == board[4] && board[4] == board[6])
                return board[2] == 'X' ? MoveResult.XWon : MoveResult.OWon;

            // Empty Spot Check
            bool emptyExists = false;
            for (int i = 0; i < board.Length; i++)
                if (board[i] == '\0')
                    emptyExists = true;
            if (!emptyExists)
                return MoveResult.Tie;

            // No Result Yet
            return MoveResult.NoResult;
        }

        public bool ValidPos(Vector2 pos)
        {
            int index = pos.X + pos.Y * 3;
            return board[index] == '\0';
        }

        public void ResetBoard()
        {
            for (int i = 0; i < board.Length; i++)
                board[i] = '\0';
        }
    }
}
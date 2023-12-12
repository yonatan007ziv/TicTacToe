using System;
using System.Linq;

namespace TicTacToe.Client.WPF.DI.Implementations;

internal class CpuPlayer
{
	private readonly int difficulty;

	public CpuPlayer(int difficulty)
	{
		this.difficulty = difficulty;
	}

	public int GenerateMove(char[] board, char mySymbol)
	{
		bool maximizeX = mySymbol == 'X';
		return FindMove(board, maximizeX);
	}

	private int FindMove(char[] currentBoard, bool maximizingX)
	{
		int bestMove = -1;
		int bestScore = int.MinValue;

		for (int i = 0; i < 9; i++)
		{
			if (currentBoard[i] == '\0')
			{
				currentBoard[i] = maximizingX ? 'X' : 'O';
				int score = Minimax(currentBoard, difficulty * 3, !maximizingX);
				currentBoard[i] = '\0';

				if (score > bestScore)
				{
					bestMove = i;
					bestScore = score;
				}
			}
		}

		return bestMove;
	}

	private int Minimax(char[] currentBoard, int depth, bool maximizingX)
	{
		int score = Evaluate(currentBoard, maximizingX);

		if (score != 0 || depth == 0 || IsBoardFull(currentBoard))
			return score;

		int best = maximizingX ? int.MinValue : int.MaxValue;

		for (int i = 0; i < 9; i++)
		{
			if (currentBoard[i] == '\0')
			{
				currentBoard[i] = maximizingX ? 'X' : 'O';
				int currentScore = Minimax(currentBoard, depth - 1, !maximizingX);
				currentBoard[i] = '\0';

				best = maximizingX ? Math.Max(best, currentScore) : Math.Min(best, currentScore);
			}
		}

		return best;
	}

	private int Evaluate(char[] currentBoard, bool maximizingX)
	{
		char opponentSymbol = maximizingX ? 'O' : 'X';

		// Check for a win
		for (int i = 0; i < 3; i++)
		{
			if (currentBoard[i * 3] == currentBoard[i * 3 + 1] && currentBoard[i * 3 + 1] == currentBoard[i * 3 + 2])
			{
				if (currentBoard[i * 3] == (maximizingX ? 'X' : 'O'))
					return 1;
				else if (currentBoard[i * 3] == opponentSymbol)
					return -1;
			}
			if (currentBoard[i] == currentBoard[i + 3] && currentBoard[i + 3] == currentBoard[i + 6])
			{
				if (currentBoard[i] == (maximizingX ? 'X' : 'O'))
					return 1;
				else if (currentBoard[i] == opponentSymbol)
					return -1;
			}
		}

		if (currentBoard[0] == currentBoard[4] && currentBoard[4] == currentBoard[8])
		{
			if (currentBoard[0] == (maximizingX ? 'X' : 'O'))
				return 1;
			else if (currentBoard[0] == opponentSymbol)
				return -1;
		}

		if (currentBoard[2] == currentBoard[4] && currentBoard[4] == currentBoard[6])
		{
			if (currentBoard[2] == (maximizingX ? 'X' : 'O'))
				return 1;
			else if (currentBoard[2] == opponentSymbol)
				return -1;
		}

		// Check for a draw
		if (!currentBoard.Any(c => c == '\0'))
			return 0;

		// No win or draw yet
		return 999;
	}

	private bool IsBoardFull(char[] currentBoard)
	{
		return !currentBoard.Any(c => c == '\0');
	}
}
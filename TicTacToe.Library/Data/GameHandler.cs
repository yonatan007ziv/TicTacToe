namespace TicTacToe.Library.Data;

public class GameHandler
{
	private readonly Random rng = new Random();

	public GameState GameState { get; } = new GameState();
	public bool[] ActivatedMapping { get; private set; } = new bool[9];
	public int CurrentTurn { get; private set; }

	public char[] Board { get => GameState.Board; }

	public GameHandler()
	{
		RandomizeTurn();
	}

	private void RandomizeTurn()
	{
		CurrentTurn = rng.Next(2);
	}

	public bool IsTerminal(out char winner)
	{
		// check rows
		for (int i = 0; i < 3; i++)
		{
			if (GameState.IsSpotEmpty(3 * i))
				continue;

			if (Board[3 * i] == Board[3 * i + 1] && Board[3 * i + 1] == Board[3 * i + 2])
			{
				winner = Board[3 * i];
				return true;
			}
		}

		// check columns
		for (int i = 0; i < 3; i++)
		{
			if (GameState.IsSpotEmpty(i))
				continue;

			if (Board[i] == Board[i + 3] && Board[i + 3] == Board[i + 6])
			{
				winner = Board[i];
				return true;
			}
		}

		// check diagonal
		if (!GameState.IsSpotEmpty(0) && Board[0] == Board[4] && Board[4] == Board[8])
		{
			winner = Board[0];
			return true;
		}
		if (!GameState.IsSpotEmpty(2) && Board[2] == Board[4] && Board[4] == Board[6])
		{
			winner = Board[2];
			return true;
		}

		// check not full
		for (int i = 0; i < 9; i++)
			if (GameState.IsSpotEmpty(i))
			{
				winner = 'N';
				return false;
			}

		winner = 'T';
		return true;

	}

	public void SwitchTurns()
	{
		CurrentTurn ^= 1;
	}

	public void PlacePiece(char symbol, int index)
	{
		Board[index] = symbol;
	}
	public void ActivateSlots()
	{
		for (int i = 0; i < 9; i++)
			if (GameState.IsSpotEmpty(i))
				ActivatedMapping[i] = true;
	}

	public void DeactivateSlots()
	{
		for (int i = 0; i < 9; i++)
			ActivatedMapping[i] = false;
	}

	public bool IsXTurn()
		=> CurrentTurn == 0;

	public bool IsOTurn()
		=> !IsXTurn();

	public void ResetGame()
	{
		GameState.ResetState();
		RandomizeTurn();
	}
}
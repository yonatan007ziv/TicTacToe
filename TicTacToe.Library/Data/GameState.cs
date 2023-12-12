namespace TicTacToe.Library.Data;

public class GameState
{
	public char[] Board { get; private set; } = new char[9];

	public bool IsSpotEmpty(int index)
		=> Board[index] == '\0';

	public void ResetState()
		=> Board = new char[9];
}
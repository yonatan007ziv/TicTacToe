namespace TicTacToe.Library.MessageCodes
{
	public enum CommunicationMessage
	{
		Place = 1,
		PlayAgain,
		Leave,
		NotifyWaiting,
		DisableNotifyWaiting,
		PlayerAssignment,
		CurrentTurn,
		InvalidMoveWrongTurn,
		InvalidMove,
		Won,
		Tie,
		Placed,
	}
}
namespace TicTacToe.Library.MessageCodes
{
    public enum CommunicationMessage
    {
        Place,
        PlayAgain,
        Leave,
        NotifyWaiting,
        DisableNotifyWaiting,
        PlayerAssignment,
        YourTurn,
        OpponentTurn,
        InvalidMoveWrongTurn,
        InvalidMove,
        XWon,
        OWon,
        Tie,
        PlacedX,
        PlacedO
    }
}
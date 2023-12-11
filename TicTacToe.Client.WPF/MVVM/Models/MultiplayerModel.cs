using System.Windows.Input;

namespace TicTacToe.Client.WPF.MVVM.Models;

class MultiplayerModel
{
	public char MySymbol { get; set; } = '-';
	public string ResultText { get; set; } = "";
	public string StatusText { get; set; } = "";
	public string WarningText { get; set; } = "";
	public ICommand ButtonCmd { get; set; } = null!;
}
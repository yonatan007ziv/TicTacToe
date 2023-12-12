using System.Windows.Input;
using TicTacToe.Library.Data;

namespace TicTacToe.Client.WPF.MVVM.Models;

class SingplayerGameModel
{
	public GameHandler GameHandler { get; set; } = null!;
	public ICommand ButtonCmd { get; set; } = null!;
	public ICommand ResetCmd { get; set; } = null!;
	public string StatusMessage { get; set; } = "";
}
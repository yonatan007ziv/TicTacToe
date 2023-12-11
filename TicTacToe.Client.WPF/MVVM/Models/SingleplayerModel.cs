using System.Windows.Input;

namespace TicTacToe.Client.WPF.MVVM.Models;

class SingleplayerModel
{
	public ICommand EasyCmd { get; set; } = null!;
	public ICommand MediumCmd { get; set; } = null!;
	public ICommand ImpossibleCmd { get; set; } = null!;
}
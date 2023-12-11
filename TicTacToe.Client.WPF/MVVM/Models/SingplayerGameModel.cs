using System.Collections.ObjectModel;
using System.Windows.Input;
using TicTacToe.Client.WPF.MVVM.ViewModels;

namespace TicTacToe.Client.WPF.MVVM.Models;

class SingplayerGameModel
{
	public ObservableCollection<ButtonViewModel> Buttons { get; set; } = null!;
	public ICommand ButtonCmd { get; set; } = null!;
	public ICommand ResetCmd { get; set; } = null!;
	public string StatusMessage { get; set; } = "";
}
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;

namespace TicTacToe.Client.WPF.MVVM.ViewModels;

class ButtonViewModel : Button
{
	public event PropertyChangedEventHandler? PropertyChanged;
	private void OnPropertyChanged([CallerMemberName] string propertyName = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	public ButtonViewModel(ICommand cmd, int index)
	{
		Enabled = false;
		Text = " ";

		Command = cmd;
		CommandParameter = index;

		Width = 50;
		Height = 50;
	}

	public bool Enabled
	{
		get => IsEnabled;
		set
		{
			IsEnabled = value;
			OnPropertyChanged();
		}
	}
	public string Text
	{
		get => Content.ToString()!;
		set
		{
			Content = value;
			OnPropertyChanged();
		}
	}
}
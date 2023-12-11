using System.Collections.ObjectModel;
using System.Net;
using System.Windows.Input;
using TicTacToe.Client.WPF.DI.Implementations;
using TicTacToe.Client.WPF.MVVM.Core.Base;
using TicTacToe.Client.WPF.MVVM.Models;
using TicTacToe.Library.Data;

namespace TicTacToe.Client.WPF.MVVM.ViewModels;

class MultiplayerViewModel : BaseViewModel
{
	private readonly MultiplayerModel model = new MultiplayerModel();
	private readonly ServerMessagesHandler playerHandler;

	private ObservableCollection<ButtonViewModel> buttonGrid = new ObservableCollection<ButtonViewModel>();
	public ObservableCollection<ButtonViewModel> ButtonGrid
	{
		get => buttonGrid;
		set
		{
			buttonGrid = value;
			OnPropertyChanged();
		}
	}

	public char MySymbol
	{
		get => model.MySymbol;
		set
		{
			model.MySymbol = value;
			OnPropertyChanged();
		}
	}
	public string ResultText
	{
		get => model.ResultText;
		set
		{
			model.ResultText = value;
			OnPropertyChanged();
		}
	}
	public string StatusText
	{
		get => model.StatusText;
		set
		{
			model.StatusText = value;
			OnPropertyChanged();
		}
	}
	public string WarningText
	{
		get => model.WarningText;
		set
		{
			model.WarningText = value;
			OnPropertyChanged();
		}
	}

	public ICommand ButtonCmd
	{
		get => model.ButtonCmd;
		set
		{
			model.ButtonCmd = value;
			OnPropertyChanged();
		}
	}


	public MultiplayerViewModel()
	{
		playerHandler = new ServerMessagesHandler(this, IPAddress.Parse("127.0.0.1"), 65500);
		ButtonCmd = new RelayCommand(ButtonClick, obj => true);
		for (int i = 0; i < 9; i++)
			buttonGrid.Add(new ButtonViewModel(ButtonCmd, i) { Enabled = true });
	}

	private void ButtonClick(object? obj)
	{
		int index = (int)obj!;
		playerHandler?.TryPlace(Vector2.IndexToVector2(index));
	}

	public void DisableButtons()
	{
		foreach (var button in buttonGrid)
			button.Enabled = false;
	}

	public void EnableEmptyButtons()
	{
		foreach (var button in buttonGrid)
			if (button.Text == "")
				button.Enabled = true;
	}

	public void ResetBoard()
	{
		ResultText = "";
		foreach (var button in buttonGrid)
		{
			button.Text = "";
			button.Enabled = false;
		}
	}

	public void UpdateVisual(char v, Vector2 posParam)
	{
		int index = Vector2.Vector2ToIndex(posParam);

		buttonGrid[index].Text = v.ToString();
		buttonGrid[index].Enabled = false;
	}

	public void SetStatus(string v)
	{
		StatusText = $"Waiting for Player {v}...";
	}

	public void SetResult(char v)
	{
		ResultText = v == 'T' ? "It's a Tie!" : $"{v} Won!";
		ResetBoard();
	}

	public void SetWarning(string v)
	{
		WarningText = v;
	}
}
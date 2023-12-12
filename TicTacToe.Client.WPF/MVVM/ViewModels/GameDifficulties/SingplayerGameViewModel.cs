using System;
using System.Threading.Tasks;
using System.Windows.Input;
using TicTacToe.Client.WPF.DI.Implementations;
using TicTacToe.Client.WPF.MVVM.Core.Base;
using TicTacToe.Client.WPF.MVVM.Models;
using TicTacToe.Library.Data;

namespace TicTacToe.Client.WPF.MVVM.ViewModels;

abstract class SingplayerGameViewModel : BaseViewModel
{
    private readonly SingplayerGameModel model = new SingplayerGameModel();
	private readonly CpuPlayer ai;
	private char MySymbol;
	private bool playerClicked;

	public GameHandler GameHandler
	{
        get => model.GameHandler;
        set
        {
			model.GameHandler = value;
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
    public ICommand ResetCmd
    {
		get => model.ResetCmd;
		set
		{
			model.ResetCmd = value;
			OnPropertyChanged();
		}
	}
    public string StatusMessage
	{
		get => model.StatusMessage;
		set
		{
			model.StatusMessage = value;
			OnPropertyChanged();
		}
	}

	public SingplayerGameViewModel(int difficulty)
    {
		ai = new CpuPlayer(difficulty);
		GameHandler = new GameHandler();

		ButtonCmd = new RelayCommand(ButtonClick, obj => true);
		ResetCmd = new RelayCommand(obj => ResetGame(), obj => true);

		GameLoop();
	}

	private async void GameLoop()
	{
		if (new Random().Next(2) == 0)
			MySymbol = 'X';
		else
			MySymbol = 'O';

		char winner;
		while (!GameHandler.IsTerminal(out winner))
		{
			if (GameHandler.IsXTurn() && MySymbol == 'X' || !GameHandler.IsXTurn() && MySymbol == 'O')
				await PlayerTurn();
			else
				await CpuTurn();

			GameHandler.SwitchTurns();
			RefreshVisual();
		}

		if (winner == 'T')
			StatusMessage = $"It's a Tie!";
		else
			StatusMessage = $"{winner} Won!";
		GameHandler.DeactivateSlots();
	}

	private void RefreshVisual()
		=> OnPropertyChanged(nameof(GameHandler));

	private async Task PlayerTurn()
	{
		StatusMessage = $"Your Turn {MySymbol}";
		GameHandler.ActivateSlots();
		RefreshVisual();

		while (!playerClicked) { await Task.Delay(1); }
		playerClicked = false;
		GameHandler.DeactivateSlots();
	}
	private async Task CpuTurn()
	{
		char cpuSymbol = MySymbol == 'X' ? 'O' : 'X';
		StatusMessage = $"CPU Turn {cpuSymbol}";

		await Task.Delay(250); // lol just for the effect

		GameHandler.PlacePiece(cpuSymbol, ai.GenerateMove((char[])GameHandler.GameState.Board.Clone(), cpuSymbol));
	}

	private void ButtonClick(object? obj)
	{
		playerClicked = true;
		int buttonId = int.Parse((string)obj!);
		GameHandler.PlacePiece(MySymbol, buttonId);
	}

	private void ResetGame()
	{
		StatusMessage = "";
		GameHandler.ResetGame();
		GameLoop();
		RefreshVisual();
	}
}
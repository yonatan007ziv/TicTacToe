using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using TicTacToe.Client.WPF.MVVM.Core.Base;
using TicTacToe.Client.WPF.MVVM.Models;

namespace TicTacToe.Client.WPF.MVVM.ViewModels;

abstract class SingplayerGameViewModel : BaseViewModel
{
    private readonly SingplayerGameModel model = new SingplayerGameModel();
	private readonly int difficulty;
	private char MySymbol;
	private bool playerClicked;

	public ObservableCollection<ButtonViewModel> Buttons
    {
        get => model.Buttons;
        set
        {
			model.Buttons = value;
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
		this.difficulty = difficulty;
		Buttons = new ObservableCollection<ButtonViewModel>();
		ButtonCmd = new RelayCommand(ButtonClick, obj => true);
		ResetCmd = new RelayCommand(obj => ResetGame(), obj => true);
		SetupButtons();

		if (new Random().Next(2) == 0)
			MySymbol = 'X';
		else
			MySymbol = 'O';

		GameLoop();
	}

	private async void GameLoop()
	{
		int currentTurn = new Random().Next(2);
		char winner;
		while (!CheckWinner(out winner))
		{
			if (currentTurn == 0)
				await PlayerTurn();
			else
				await CpuTurn();

			currentTurn ^= 1; // flip flop the currentTurn
		}

		if (winner == 'T')
			StatusMessage = $"It's a Tie!";
		else
			StatusMessage = $"{winner} Won!";
		FreezeBoard();
	}

	public int FindMove(char[] currentBoard)
	{
		int bestMove = -1, secondBestMove = -1, thirdBestMove = -1;
		int bestScore = int.MinValue;

		for (int i = 0; i < 9; i++)
		{
			if (currentBoard[i] == ' ')
			{
				currentBoard[i] = 'X';
				int score = Minimax(currentBoard, 0, false);
				currentBoard[i] = ' ';

				if (score > bestScore)
				{
					thirdBestMove = secondBestMove;
					secondBestMove = bestMove;
					bestMove = i;

					bestScore = score;
				}
			}
		}

		// ternary hell to make sure -1 is not returned no matter the difficulty
		if (difficulty == 0)
			return thirdBestMove == -1 ? (secondBestMove == -1 ? bestMove : secondBestMove) : thirdBestMove;
		if (difficulty == 1)
			return secondBestMove == -1 ? bestMove : secondBestMove;
		if (difficulty == 2 && bestMove != -1)
			return bestMove;
		throw new Exception();
	}
	private int Minimax(char[] currentBoard, int depth, bool isMaximizing)
	{
		int score = Evaluate(currentBoard);

		if (score != 0 || depth == 0 || IsBoardFull(currentBoard))
			return score;

		if (isMaximizing)
		{
			int best = int.MinValue;

			for (int i = 0; i < 9; i++)
			{
				if (currentBoard[i] == ' ')
				{
					currentBoard[i] = 'X';
					best = Math.Max(best, Minimax(currentBoard, depth - 1, !isMaximizing));
					currentBoard[i] = ' ';
				}
			}

			return best;
		}
		else
		{
			int best = int.MaxValue;

			for (int i = 0; i < 9; i++)
			{
				if (currentBoard[i] == ' ')
				{
					currentBoard[i] = 'O';
					best = Math.Min(best, Minimax(currentBoard, depth - 1, !isMaximizing));
					currentBoard[i] = ' ';
				}
			}

			return best;
		}
	}
	private int Evaluate(char[] currentBoard)
	{
		for (int i = 0; i < 3; i++)
		{
			if (currentBoard[i * 3] != ' ' && currentBoard[i * 3] == currentBoard[i * 3 + 1] && currentBoard[i * 3 + 1] == currentBoard[i * 3 + 2])
				return currentBoard[i * 3] == 'X' ? 1 : -1;
			if (currentBoard[i] != ' ' && currentBoard[i] == currentBoard[i + 3] && currentBoard[i + 3] == currentBoard[i + 6])
				return currentBoard[i] == 'X' ? 1 : -1;
		}

		if (currentBoard[0] != ' ' && currentBoard[0] == currentBoard[4] && currentBoard[4] == currentBoard[8])
			return currentBoard[0] == 'X' ? 1 : -1;

		if (currentBoard[2] != ' ' && currentBoard[2] == currentBoard[4] && currentBoard[4] == currentBoard[6])
			return currentBoard[2] == 'X' ? 1 : -1;

		if (!currentBoard.Any(c => c == ' '))
			return 0;

		return 999;
	}
	private bool IsBoardFull(char[] currentBoard)
	{
		for (int i = 0; i < 9; i++)
			if (currentBoard[i] == ' ')
				return false;
		return true;
	}

	private bool CheckWinner(out char winner)
	{
		// check rows
		for (int i = 0; i < 3; i++)
		{
			if (Buttons[3 * i].Text == " ")
				continue;

			if(Buttons[3 * i].Text == Buttons[3 * i + 1].Text && Buttons[3 * i + 1].Text == Buttons[3 * i + 2].Text)
			{
				winner = Buttons[3 * i].Text[0];
				return true;
			}
		}

		// check columns
		for (int i = 0; i < 3; i++)
		{
			if (Buttons[i].Text == " ")
				continue;

			if (Buttons[i].Text == Buttons[i + 3].Text && Buttons[i + 3].Text == Buttons[i + 6].Text)
			{
				winner = Buttons[i].Text[0];
				return true;
			}
		}

		// check diagonal
		if (Buttons[0].Text != " " && Buttons[0].Text == Buttons[4].Text && Buttons[4].Text == Buttons[8].Text)
		{
			winner = Buttons[0].Text[0];
			return true;
		}
		if (Buttons[2].Text != " " && Buttons[2].Text == Buttons[4].Text && Buttons[4].Text == Buttons[6].Text)
		{
			winner = Buttons[2].Text[0];
			return true;
		}

		// check not full
		for(int i =0; i < 9; i++)
			for(int j =0; j < 9; j++)
				if (Buttons[i].Text == " ")
				{
					winner = 'N';
					return false;
				}

		winner = 'T';
		return true;
	}

	private void UnfreezeBoard()
	{
		foreach (ButtonViewModel button in Buttons)
			if (button.Text == " ")
				button.Enabled = true;
	}

	private void FreezeBoard()
	{
		foreach (ButtonViewModel button in Buttons)
			button.Enabled = false;
	}

	private async Task PlayerTurn()
	{
		StatusMessage = $"Your Turn {MySymbol}";
		UnfreezeBoard();

		while (!playerClicked) { await Task.Delay(1); }
		playerClicked = false;
	}

	private async Task CpuTurn()
	{
		StatusMessage = $"CPU Turn {(MySymbol == 'X' ? 'O' : 'X')}";
		FreezeBoard();

		await Task.Delay(250); // lol just for the effect

		char[] gameState = Buttons.ToArray().SelectMany(button => new char[] { button.Text[0] }).ToArray();
		int moveIndex = FindMove(gameState);
		Buttons[moveIndex].Content = MySymbol == 'X' ? 'O' : 'X';
	}

	private void SetupButtons()
	{
        Buttons.Clear();
		for (int i = 0; i < 9; i++)
			Buttons.Add(new ButtonViewModel(ButtonCmd, i));
	}

	private void ButtonClick(object? obj)
	{
		playerClicked = true;
        int buttonId = (int)obj!;
        Buttons[buttonId].Content = MySymbol;
	}

	private void ResetGame()
	{
		StatusMessage = "";
		SetupButtons();
		GameLoop();
	}
}
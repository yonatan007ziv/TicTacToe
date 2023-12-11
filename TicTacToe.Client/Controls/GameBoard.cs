using TicTacToe.Client.Handlers;
using TicTacToe.Library.Data;
using TicTacToe.Library.MessageCodes;

namespace TicTacToe.Client.Controls
{
	public partial class GameBoard : UserControl
	{
		private readonly GameView mainWindow;
		private readonly Button[] boardButtons;

		public ServerMessagesHandler? PlayerHandler { get; set; }
		public char CurrentTurn
		{
			set
			{
				turnLabel.Invoke(() => turnLabel.Text = $"Current Turn: {value}");
			}
		}
		public char localSymbol
		{
			set
			{
				playerLabel.Invoke(() => playerLabel.Text = $"You're: {value}");
			}
		}

		public GameBoard(GameView mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
			boardButtons = new Button[] { topLeft, topMiddle, topRight, centerLeft, centerMiddle, centerRight, bottomLeft, bottomMiddle, bottomRight };
		}

		public void ResetBoard()
		{
			Invoke(() =>
			{
				restartButton.Enabled = false;
				turnLabel.Text = "";
				waitingLabel.Text = "";
				warningLabel.Text = "";
				winnerLabel.Text = "";

				foreach (Button button in boardButtons)
				{
					button.Enabled = false;
					button.Text = "";
				}
			});
		}

		public void UpdateVisual(char symbol, Vector2 pos)
		{
			int index = pos.X + pos.Y * 3;
			boardButtons[index].Invoke(() => boardButtons[index].Text = symbol.ToString());
		}

		public void EnableEmptyButtons()
		{
			Invoke(() =>
			{
				foreach (Button button in boardButtons)
					if (button.Text == "")
						button.Enabled = true;
			});
		}

		public void DisableButtons()
		{
			Invoke(() =>
			{
				foreach (Button button in boardButtons)
					button.Enabled = false;
			});
		}

		public void WaitingForPlayersText(string text)
		{
			waitingLabel.Invoke(() => waitingLabel.Text = text);
		}

		public void Warning(string warning)
		{
			warningLabel.Invoke(() => warningLabel.Text = warning);
		}

		public void GameResult(char w)
		{
			Invoke(() =>
			{
				if (w == (PlayerHandler?.Symbol ?? '\0'))
					winnerLabel.Text = "You Won!";
				else if (w == 'T')
					winnerLabel.Text = "It's a Tie!";
				else
					winnerLabel.Text = "You Lost!";
				restartButton.Enabled = true;
			});
			DisableButtons();
		}

		public void OpenConnectMenu()
		{
			Invoke(() =>
			{
				mainWindow.AddViewControl(new ConnectMenu(mainWindow));
				mainWindow.RemoveViewControl(this);
			});
		}

		private void PositionClick(object sender, EventArgs e)
		{
			Button clicked = (Button)sender;
			int index = int.Parse(clicked.Tag.ToString()!);
			PlayerHandler?.TryPlace(Vector2.IndexToVector2(index));
		}

		private void OnLeaveButton(object sender, EventArgs e)
		{
			PlayerHandler?.Disconnect();
			OpenConnectMenu();
		}

		private void OnRestartButton(object sender, EventArgs e)
		{
			PlayerHandler?.SendMessage(CommunicationMessage.PlayAgain);
			restartButton.Invoke(() => restartButton.Enabled = false);
		}
	}
}
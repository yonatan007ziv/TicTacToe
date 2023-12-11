using System.Diagnostics;
using System.Net;
using TicTacToe.Client.Handlers;

namespace TicTacToe.Client.Controls
{
	public partial class ConnectMenu : UserControl
	{
		private readonly GameView mainWindow;

		public ConnectMenu(GameView mainWindow)
		{
			InitializeComponent();
			this.mainWindow = mainWindow;
		}

		private void OnConnectButton(object sender, EventArgs e)
		{
			GameBoard boardControl = new GameBoard(mainWindow);
			mainWindow.AddViewControl(boardControl);

			ServerMessagesHandler playerHandler;
			try
			{
				playerHandler = new ServerMessagesHandler(boardControl, IPAddress.Parse(ipInputField.Text), int.Parse(portInputField.Text));
			}
			catch
			{
				mainWindow.RemoveViewControl(boardControl);
				return;
			}

			boardControl.PlayerHandler = playerHandler;
			Debug.WriteLine(mainWindow.Controls.Count);
			mainWindow.RemoveViewControl(this);
			Debug.WriteLine(mainWindow.Controls.Count);
		}
	}
}
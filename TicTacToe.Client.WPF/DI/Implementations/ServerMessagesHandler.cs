using System.Net;
using System.Net.Sockets;
using TicTacToe.Client.WPF.MVVM.ViewModels;
using TicTacToe.Library.Data;
using TicTacToe.Library.Handlers;
using TicTacToe.Library.MessageCodes;

namespace TicTacToe.Client.WPF.DI.Implementations;

class ServerMessagesHandler
{
	private readonly ClientHandler clientHandler;
	private readonly MultiplayerViewModel boardView;

	public char Symbol { get; private set; }

	public ServerMessagesHandler(MultiplayerViewModel boardView, IPAddress ip, int port)
	{
		TcpClient socket = new TcpClient();
		socket.Connect(ip, port);

		this.boardView = boardView;
		clientHandler = new ClientHandler(socket, InterpretMessage);
	}

	public void SendMessage(CommunicationMessage msg, CommunicationParameter param = 0, int posX = -1, int posY = -1)
	{
		string strMsg = $"{(int)msg}{(param == 0 ? "" : $":{(int)param}")}{(posX == -1 || posY == -1 ? "" : $":{new Vector2(posX, posY)}")}";
		clientHandler.WriteAsync(strMsg);
	}

	public void InterpretMessage(string msg)
	{
		string[] segments = msg.Split(':');
		CommunicationMessage operation = (CommunicationMessage)int.Parse(segments[0]);
		CommunicationParameter parameter = segments.Length > 1 ? (CommunicationParameter)int.Parse(segments[1]) : 0;
		Vector2 posParam = segments.Length > 2 ? Vector2.FromString(segments[2]) : Vector2.Zero;

		switch (operation)
		{
			case CommunicationMessage.PlayAgain:
				boardView.ResetBoard();
				break;
			case CommunicationMessage.NotifyWaiting:
				boardView.SetStatus("Waiting for other Player...");
				boardView.ResetBoard();
				break;
			case CommunicationMessage.DisableNotifyWaiting:
				boardView.SetStatus("");
				break;
			case CommunicationMessage.PlayerAssignment:
				switch (parameter)
				{
					case CommunicationParameter.PlayerX:
						Symbol = 'X';
						boardView.MySymbol = 'X';
						break;
					case CommunicationParameter.PlayerO:
						Symbol = 'O';
						boardView.MySymbol = 'O';
						break;
				}
				break;
			case CommunicationMessage.CurrentTurn:
				if (parameter == CommunicationParameter.PlayerX && Symbol == 'X'
					|| parameter == CommunicationParameter.PlayerO && Symbol == 'O')
					boardView.EnableEmptyButtons();
				else
					boardView.DisableButtons();
				break;
			case CommunicationMessage.InvalidMoveWrongTurn:
				boardView.SetWarning("Not your Turn");
				break;
			case CommunicationMessage.InvalidMove:
				boardView.SetWarning("Invalid Move");
				break;
			case CommunicationMessage.Tie:
				boardView.SetResult('T');
				break;
			case CommunicationMessage.Won:
				boardView.SetResult(parameter == CommunicationParameter.PlayerX ? 'X' : 'O');
				break;
			case CommunicationMessage.Placed:
				if (parameter == CommunicationParameter.PlayerX)
					boardView.UpdateVisual('X', posParam);
				else if (parameter == CommunicationParameter.PlayerO)
					boardView.UpdateVisual('O', posParam);
				break;
		}
	}

	public void TryPlace(Vector2 pos)
	{
		SendMessage(CommunicationMessage.Place, Symbol == 'X' ? CommunicationParameter.PlayerX : CommunicationParameter.PlayerO, pos.X, pos.Y);
	}

	public void Disconnect()
	{
		clientHandler.Disconnect();
	}
}
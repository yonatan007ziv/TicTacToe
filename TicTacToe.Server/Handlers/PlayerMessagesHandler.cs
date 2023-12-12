using System.Net.Sockets;
using TicTacToe.Library.Data;
using TicTacToe.Library.Handlers;
using TicTacToe.Library.MessageCodes;
using TicTacToe.Server.Systems;

namespace TicTacToe.Server.Handlers;

internal class PlayerMessagesHandler
{
	private readonly ClientHandler clientHandler;
	private readonly Lobby connectedLobby;

	public char Symbol { get; private set; } = '\0';

	public PlayerMessagesHandler(TcpClient socket, Lobby connectedLobby)
	{
		Console.WriteLine("New PlayerHandler");
		clientHandler = new ClientHandler(socket, InterpretMessage);
		clientHandler.OnDisconnected += OnDisconnected;
		this.connectedLobby = connectedLobby;
		this.connectedLobby.AddPlayer(this);
	}

	public void OnDisconnected()
	{
		Console.WriteLine("PlayerHandler Disconnected");
		connectedLobby.PlayerDisconnected(this);
	}

	public void SendMessage(CommunicationMessage msg, CommunicationParameter param = 0, int posX = -1, int posY = -1)
	{
		string strMsg = $"{(int)msg}{(param == 0 ? "" : $":{(int)param}")}{(posX == -1 || posY == -1 ? "" : $":{new Vector2(posX, posY)}")}";
		Console.WriteLine($"Server SendMessage: {msg} {param} ({posX},{posY})");
		clientHandler.WriteAsync(strMsg);
	}

	public void InterpretMessage(string msg)
	{
		string[] segments = msg.Split(':');
		CommunicationMessage operation = (CommunicationMessage)int.Parse(segments[0]);
		CommunicationParameter parameter = segments.Length > 1 ? (CommunicationParameter)int.Parse(segments[1]) : 0;
		Vector2 posParam = segments.Length > 2 ? Vector2.FromString(segments[2]) : Vector2.Zero;

		Console.WriteLine($"Server InterpretMessage: {operation} {parameter} {posParam}");
		switch (operation)
		{
			case CommunicationMessage.Place:
				TryPlace(posParam);
				break;
			case CommunicationMessage.PlayAgain:
				connectedLobby.PlayAgain();
				break;
		}
	}

	private void TryPlace(Vector2 pos)
	{
		connectedLobby.PlayerPlace(this, pos);
	}

	public void SetSymbol(char p)
	{
		Symbol = p;
	}
}
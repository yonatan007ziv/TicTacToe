using System.Net.Sockets;
using System.Text;

namespace TicTacToe.Library.Handlers
{
	public class ClientHandler
	{
		private const char Separator = '+';

		private readonly TcpClient _socket;
		private readonly Byte[] buffer;
		private readonly CancellationTokenSource disconnectedCts = new CancellationTokenSource();
		private readonly Action<string> messageInterpreter;

		public event Action? OnDisconnected;

		public ClientHandler(TcpClient socket, Action<string> interpreter)
		{
			_socket = socket;
			messageInterpreter = interpreter;
			buffer = new Byte[_socket.ReceiveBufferSize];

			StartReadLoopAsync();
		}

		public async void WriteAsync(string msg)
		{
			try
			{
				Byte[] msgBuffer = Encoding.UTF8.GetBytes(msg + Separator);
				await _socket.GetStream().WriteAsync(msgBuffer, disconnectedCts.Token);
			}
			catch (OperationCanceledException)
			{
				OnDisconnected?.Invoke();
			}
			catch
			{
				disconnectedCts.Cancel();
				OnDisconnected?.Invoke();
			}
		}

		public void Disconnect()
		{
			_socket.Close();
		}

		private async void StartReadLoopAsync()
		{
			while (!disconnectedCts.IsCancellationRequested)
			{
				int bytesRead;
				try
				{
					bytesRead = await _socket.GetStream().ReadAsync(buffer, disconnectedCts.Token);
				}
				catch (OperationCanceledException)
				{
					OnDisconnected?.Invoke();
					return;
				}
				catch
				{
					disconnectedCts.Cancel();
					OnDisconnected?.Invoke();
					return;
				}

				if (bytesRead == 0)
				{
					// The client has disconnected.
					disconnectedCts.Cancel();
					OnDisconnected?.Invoke();
					return;
				}

				string msg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
				SeparateMessages(msg);
			}
		}

		private void SeparateMessages(string unseparated)
		{
			foreach (string msg in unseparated.Split(Separator))
				if (msg != "")
					messageInterpreter(msg);
		}
	}
}
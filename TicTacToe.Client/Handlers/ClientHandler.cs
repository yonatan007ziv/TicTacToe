using System.Net.Sockets;
using System.Text;

namespace TicTacToe.Client.Handlers
{
    public class ClientHandler
    {
        private const char Separator = '+';

        private readonly TcpClient _socket;
        private readonly Byte[] buffer;
        private readonly CancellationTokenSource disconnectedCts = new CancellationTokenSource();
        private readonly Action<string> messageInterpreter;

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
            catch
            {
                disconnectedCts.Cancel();
            }
        }

        private async void StartReadLoopAsync()
        {
            while (!disconnectedCts.IsCancellationRequested)
            {
                int bytesRead;
                try
                {
                    bytesRead = await _socket.GetStream().ReadAsync(buffer, 0, buffer.Length, disconnectedCts.Token);  // Use disconnectedCts.Token
                }
                catch (OperationCanceledException)
                {
                    return;
                }
                catch
                {
                    disconnectedCts.Cancel();
                    return;
                }

                if (bytesRead == 0)
                {
                    // The client has disconnected.
                    disconnectedCts.Cancel();
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

        public void Disconnect()
        {
            _socket.Close();
        }
    }
}
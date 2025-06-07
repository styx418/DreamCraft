
using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DreamServer.Modules;

namespace DreamServer
{
    public class TcpSessionClient
    {
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;
        private readonly byte[] _buffer = new byte[8192];
        private readonly StringBuilder _messageBuilder = new();
        private const string EndOfMessage = "<EOF>";

        public string SessionId { get; }
        public bool IsConnected => _client.Connected;

        public TcpSessionClient(TcpClient client)
        {
            _client = client;
            _stream = _client.GetStream();
            SessionId = Guid.NewGuid().ToString();

            _ = ReceiveLoopAsync();
        }

        private async Task ReceiveLoopAsync()
        {
            try
            {
                while (IsConnected)
                {
                    int byteCount = await _stream.ReadAsync(_buffer, 0, _buffer.Length);
                    if (byteCount == 0) break;

                    string chunk = Encoding.UTF8.GetString(_buffer, 0, byteCount);
                    _messageBuilder.Append(chunk);

                    string completeMessage = _messageBuilder.ToString();
                    int delimiterIndex;

                    while ((delimiterIndex = completeMessage.IndexOf(EndOfMessage)) >= 0)
                    {
                        string rawMessage = completeMessage[..delimiterIndex];
                        _messageBuilder.Remove(0, delimiterIndex + EndOfMessage.Length);
                        completeMessage = _messageBuilder.ToString();

                        await HandleMessageAsync(rawMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR][{SessionId}] {ex.Message}");
            }
            finally
            {
                Disconnect();
            }
        }

        private async Task HandleMessageAsync(string rawMessage)
        {
            try
            {
                var json = JsonDocument.Parse(rawMessage).RootElement;

                if (json.TryGetProperty("command", out var cmdElement))
                {
                    string command = cmdElement.GetString();
                    await ModuleManager.Instance.ExecuteAsync(this, command, json);
                }
                else
                {
                    Console.WriteLine($"[WARN][{SessionId}] Command field missing.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[INVALID][{SessionId}] {ex.Message}");
            }
        }

        public async Task SendAsync(object obj)
        {
            string json = JsonSerializer.Serialize(obj) + EndOfMessage;
            byte[] data = Encoding.UTF8.GetBytes(json);
            await _stream.WriteAsync(data, 0, data.Length);
        }

        public void Disconnect()
        {
            _stream?.Close();
            _client?.Close();
            Console.WriteLine($"[DISCONNECTED] {SessionId}");
        }
    }
}

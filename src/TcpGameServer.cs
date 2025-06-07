using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DreamCraft
{
    public class TcpGameServer : IServerModule
    {
        private readonly TcpListener listener;
        private bool isRunning;

        public TcpGameServer(int port)
        {
            listener = new TcpListener(IPAddress.Any, port);
        }

        public void Start()
        {
            isRunning = true;
            listener.Start();
            Logger.Info($"TCP Server started on port {((IPEndPoint)listener.LocalEndpoint).Port}");
            Task.Run(ListenAsync);
        }

        private async Task ListenAsync()
        {
            while (isRunning)
            {
                TcpClient client = await listener.AcceptTcpClientAsync();
                _ = HandleClientAsync(client);
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            Logger.Info("Client connected via TCP");
            try
            {
                using var stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Logger.Info($"Received TCP message: {message}");

                string response = "Hello from TCP server";
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
            catch (Exception ex)
            {
                Logger.Error($"TCP error: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }

        public void Stop()
        {
            isRunning = false;
            listener.Stop();
            Logger.Info("TCP Server stopped");
        }
    }
}

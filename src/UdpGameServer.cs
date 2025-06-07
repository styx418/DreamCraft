using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DreamCraft
{
    public class UdpGameServer : IServerModule
    {
        private readonly UdpClient udpClient;
        private bool isRunning;

        public UdpGameServer(int port)
        {
            udpClient = new UdpClient(port);
        }

        public void Start()
        {
            isRunning = true;
            Logger.Info($"UDP Server started on port {((IPEndPoint)udpClient.Client.LocalEndPoint).Port}");
            Task.Run(ReceiveLoopAsync);
        }

        private async Task ReceiveLoopAsync()
        {
            while (isRunning)
            {
                try
                {
                    UdpReceiveResult result = await udpClient.ReceiveAsync();
                    string message = Encoding.UTF8.GetString(result.Buffer);
                    Logger.Info($"Received UDP message: {message}");

                    string response = "Hello from UDP server";
                    byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                    await udpClient.SendAsync(responseBytes, responseBytes.Length, result.RemoteEndPoint);
                }
                catch (Exception ex)
                {
                    Logger.Error($"UDP error: {ex.Message}");
                }
            }
        }

        public void Stop()
        {
            isRunning = false;
            udpClient.Close();
            Logger.Info("UDP Server stopped");
        }
    }
}

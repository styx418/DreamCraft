
using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace DreamServer
{
    public class UnityTcpServer
    {
        private TcpListener _listener;
        private readonly ConcurrentDictionary<string, TcpSessionClient> _clients = new();

        public async Task StartAsync(int port)
        {
            _listener = new TcpListener(IPAddress.Any, port);
            _listener.Start();
            Console.WriteLine($"[SERVER] Listening on port {port}...");

            while (true)
            {
                TcpClient client = await _listener.AcceptTcpClientAsync();
                var session = new TcpSessionClient(client);
                _clients[session.SessionId] = session;
                Console.WriteLine($"[CONNECT] {session.SessionId}");
            }
        }
    }
}

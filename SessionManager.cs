using System.Collections.Concurrent;
using System.Net.Sockets;

namespace DreamServer
{
    public static class SessionManager
    {
        private static ConcurrentDictionary<string, TcpClient> _activeSessions = new();
        private static ConcurrentDictionary<string, int> _accountIds = new();

        public static bool IsLoggedIn(string username)
        {
            return _activeSessions.ContainsKey(username);
        }

        public static bool RegisterSession(string username, TcpClient client, int accountId)
        {
            bool added = _activeSessions.TryAdd(username, client);
            _accountIds[username] = accountId;
            return added;
        }

        public static void UnregisterSession(string username)
        {
            _activeSessions.TryRemove(username, out _);
            _accountIds.TryRemove(username, out _);
        }

        public static void Disconnect(string username)
        {
            if (_activeSessions.TryRemove(username, out TcpClient client))
            {
                client.Close();
                ConsoleUtils.Log($"[SESSION] Déconnexion forcée de {username}", ConsoleColor.DarkRed);
            }

            _accountIds.TryRemove(username, out _);
        }

        public static int GetAccountId(string username)
        {
            return _accountIds.TryGetValue(username, out int id) ? id : -1;
        }
    }
}

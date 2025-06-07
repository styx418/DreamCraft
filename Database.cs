using System;
using MySqlConnector;

namespace DreamServer
{
    public static class Database
    {
        private static MySqlConnection _connection;
        public static MySqlConnection Connection => _connection;

        public static void Init()
        {
            string connectionString = "Server=localhost;Database=dreamcraftmmo;Uid=root;Pwd=;SslMode=none;";
            _connection = new MySqlConnection(connectionString);
            try
            {
                _connection.Open();
                ConsoleUtils.Log("[DB] Connexion à la base de données réussie.", ConsoleColor.Green);
            }
            catch (Exception ex)
            {
                ConsoleUtils.Log("[DB-ERROR] " + ex.Message, ConsoleColor.Red);
                Environment.Exit(1);
            }
        }
    }
}

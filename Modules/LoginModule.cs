// Modules/LoginModule.cs
using System;
using MySqlConnector;
using System.Text.Json;

namespace DreamServer
{
    public class LoginModule : IModule
    {
        public void Init()
        {
            ConsoleUtils.Log("[LoginModule] Prêt à recevoir les connexions Unity.", ConsoleColor.Gray);
        }

        public static string VerifyCredentials(string json)
        {
            try
            {
                var data = JsonSerializer.Deserialize<LoginRequest>(json);
                string query = "SELECT id FROM accounts WHERE username = @username AND password = @password";

                using var cmd = new MySqlCommand(query, Database.Connection);
                cmd.Parameters.AddWithValue("@username", data.username);
                cmd.Parameters.AddWithValue("@password", data.password);

                object result = cmd.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int accountId))
                {
                    if (SessionManager.IsLoggedIn(data.username))
                        return "ALREADY_LOGGED";

                    ConsoleUtils.Log($"[LoginModule] Connexion autorisée pour {data.username}", ConsoleColor.Green);
                    return $"OK:{data.username}:{accountId}";
                }

                ConsoleUtils.Log($"[LoginModule] Connexion refusée pour {data.username}", ConsoleColor.Red);
                return "FAIL";
            }
            catch (Exception ex)
            {
                ConsoleUtils.Log("[LoginModule] Exception: " + ex.Message, ConsoleColor.Red);
                return "ERROR: " + ex.Message;
            }
        }

        public class LoginRequest
        {
            public string username { get; set; }
            public string password { get; set; }
        }
    }
}

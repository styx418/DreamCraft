using System;
using MySqlConnector;

namespace DreamServer
{
    public class PlayerModule : IModule
    {
        public void Init()
        {
            ConsoleUtils.Log("[PlayerModule] Chargement des joueurs depuis la base...", ConsoleColor.Gray);

            try
            {
                // Note: utilisation des backticks autour de `character`
                string query = @"
                    SELECT a.username, c.Player_Name, c.Level, c.Xp
                    FROM accounts a
                    INNER JOIN `characters` c ON a.id = c.acc_id";

                using var command = new MySqlCommand(query, Database.Connection);
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string username = reader.GetString("username");
                    string characterName = reader.GetString("Player_Name");
                    int level = reader.GetInt32("Level");
                    int xp = reader.GetInt32("Xp");

                    ConsoleUtils.Log($" - Compte: {username} | Perso: {characterName} | Niveau: {level} | XP: {xp}", ConsoleColor.DarkGray);
                }

                ConsoleUtils.Log("[PlayerModule] Chargement terminé.", ConsoleColor.Gray);
            }
            catch (Exception ex)
            {
                ConsoleUtils.Log("[PlayerModule] Erreur: " + ex.Message, ConsoleColor.Red);
            }
        }
    }
}

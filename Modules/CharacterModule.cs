using System;
using System.Text;
using System.Text.Json;
using MySqlConnector;
using DreamServer.Modules;
namespace DreamServer
{
    public class CharacterModule : IModule
    {
        public void Init()
        {
            ConsoleUtils.Log("[CharacterModule] Initialisé", ConsoleColor.Gray);
        }

        public static string GetCharactersAsJson(int accountId)
        {
            List<CharacterData> characters = new();

            try
            {
                string query = "SELECT * FROM `characters` WHERE acc_id = @acc_id";
                using MySqlCommand cmd = new MySqlCommand(query, Database.Connection);
                cmd.Parameters.AddWithValue("@acc_id", accountId);

                using MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    CharacterData c = new CharacterData
                    {
                        acc_id = reader.GetInt32("acc_id"),
                        Player_Name = reader.GetString("Player_Name"),
                        Player_Skin = reader.GetString("Player_Skin"),
                        Level = reader.GetInt32("Level"),
                        Xp = reader.GetInt32("Xp"),
                        XpToLevel = reader.GetInt32("XpToLevel"),
                        Pos_X = reader.GetFloat("Pos_X"),
                        Pos_Y = reader.GetFloat("Pos_Y"),
                        Pos_Z = reader.GetFloat("Pos_Z"),
                        str = reader.GetInt32("str"),
                        Endurance = reader.GetInt32("Endurance"),
                        Agility = reader.GetInt32("Agility"),
                        Intelligence = reader.GetInt32("Intelligence"),
                        Sagesse = reader.GetInt32("Sagesse"),
                        AC = reader.GetInt32("AC")
                    };

                    characters.Add(c);
                }
            }
            catch (Exception ex)
            {
                ConsoleUtils.Log("[CharacterModule] Erreur : " + ex.Message, ConsoleColor.Red);
            }
            ConsoleUtils.Log("[CharacterModule] Total personnages trouvés : " + characters.Count, ConsoleColor.Blue);
            // Ajout du délimiteur EOF pour la fin de la réponse
            return JsonSerializer.Serialize(characters) + "<EOF>";
        }


        public static string HasCharacters(int accountId)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM `characters` WHERE acc_id = @acc_id";
                using var cmd = new MySqlCommand(query, Database.Connection);
                cmd.Parameters.AddWithValue("@acc_id", accountId);

                object result = cmd.ExecuteScalar();
                long count = (result != null && long.TryParse(result.ToString(), out var val)) ? val : 0;

                return count > 0 ? "HAS_CHARACTERS" : "NO_CHARACTERS";
            }
            catch (Exception ex)
            {
                ConsoleUtils.Log("[CharacterModule] Erreur: " + ex.Message, ConsoleColor.Red);
                return "ERROR";
            }
        }

        public static string CreateCharacter(int accId, string name, string skin)
        {
            try
            {
                string query = @"INSERT INTO `characters`
                    (acc_id, Player_Name, Player_Skin, Level, Xp, XpToLevel, Pos_X, Pos_Y, Pos_Z,
                     str, Endurance, Agility, Intelligence, Sagesse, AC)
                     VALUES
                    (@acc_id, @name, @skin, 1, 0, 100, 0, 0, 0, 10, 10, 10, 10, 10, 0)";

                using var cmd = new MySqlCommand(query, Database.Connection);
                cmd.Parameters.AddWithValue("@acc_id", accId);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@skin", skin);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    ConsoleUtils.Log($"[CharacterModule] Personnage '{name}' créé pour compte ID {accId}", ConsoleColor.Green);
                    return "CHARACTER_CREATED";
                }
                else
                {
                    ConsoleUtils.Log("[CharacterModule] Aucun personnage créé.", ConsoleColor.Yellow);
                    return "CREATE_FAILED";
                }
            }
            catch (Exception ex)
            {
                ConsoleUtils.Log("[CharacterModule] Erreur lors de la création : " + ex.Message, ConsoleColor.Red);
                return "ERROR";
            }
        }

        public static string FetchCharacterList(int accId)
        {
            try
            {
                string query = "SELECT Player_Name, Player_Skin, Level FROM `character` WHERE acc_id = @acc_id";
                using var cmd = new MySqlCommand(query, Database.Connection);
                cmd.Parameters.AddWithValue("@acc_id", accId);
                using var reader = cmd.ExecuteReader();

                StringBuilder result = new StringBuilder();

                while (reader.Read())
                {
                    string name = reader.GetString("Player_Name");
                    string skin = reader.GetString("Player_Skin");
                    int level = reader.GetInt32("Level");

                    result.AppendLine($"{name}:{skin}:{level}");

                    ConsoleUtils.Log("[CharacterModule] Lecture d'un personnage : " + reader.GetString("Player_Name"), ConsoleColor.Cyan);
                }

                return result.Length > 0 ? result.ToString() : "NO_CHARACTERS";
            }
            catch (Exception ex)
            {
                ConsoleUtils.Log("[CharacterModule] Erreur FetchCharacterList: " + ex.Message, ConsoleColor.Red);
                return "ERROR_FETCHING_CHARACTERS";
            }
        }

        public static string DeleteCharacter(int accId, string characterName)
        {
            try
            {
                string query = "DELETE FROM `characters` WHERE acc_id = @acc_id AND Player_Name = @name";
                using var cmd = new MySqlCommand(query, Database.Connection);
                cmd.Parameters.AddWithValue("@acc_id", accId);
                cmd.Parameters.AddWithValue("@name", characterName);

                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    ConsoleUtils.Log($"[CharacterModule] Personnage '{characterName}' supprimé.", ConsoleColor.Magenta);
                    return "CHARACTER_DELETED";
                }
                else
                {
                    return "CHARACTER_NOT_FOUND";
                }
            }
            catch (Exception ex)
            {
                ConsoleUtils.Log("[CharacterModule] Erreur suppression : " + ex.Message, ConsoleColor.Red);
                return "ERROR_DELETING_CHARACTER";
            }
        }



    }
}

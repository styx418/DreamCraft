using System;
using DreamServer;
using DreamServer.Modules;

namespace DreamServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "DreamCraft MMORPG Server";
            ConsoleUtils.PrintBanner();
            ConsoleUtils.Log("[START] Initialisation du serveur...", ConsoleColor.Cyan);

            Database.Init();
            ModuleManager.LoadModules();

            ConsoleUtils.Log("[READY] Serveur en ligne. En attente de connexions...", ConsoleColor.Green);
            ServerControl.StartKeyListener();
            UnityTcpServer tcpServer = new UnityTcpServer();
            tcpServer.Start(5050);

            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DreamServer
{
    public static class ServerControl
    {
        private static bool f10PressedOnce = false;
        private static bool f9PressedOnce = false;

        public static void StartKeyListener()
        {
            new Thread(() =>
            {
                while (true)
                {
                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.F10)
                    {
                        if (f10PressedOnce)
                        {
                            Console.WriteLine("[CTRL] Fermeture du serveur...");
                            Environment.Exit(0);
                        }
                        else
                        {
                            f10PressedOnce = true;
                            Console.WriteLine("[CTRL] Appuyez encore une fois sur F10 pour quitter.");
                            ResetFlagAfterDelay(() => f10PressedOnce = false);
                        }
                    }
                    else if (key == ConsoleKey.F9)
                    {
                        if (f9PressedOnce)
                        {
                            Console.WriteLine("[CTRL] Redémarrage du serveur...");
                            System.Diagnostics.Process.Start(Environment.ProcessPath);
                            Environment.Exit(0);
                        }
                        else
                        {
                            f9PressedOnce = true;
                            Console.WriteLine("[CTRL] Appuyez encore une fois sur F9 pour redémarrer.");
                            ResetFlagAfterDelay(() => f9PressedOnce = false);
                        }
                    }
                }
            })
            { IsBackground = true }.Start();
        }

        private static void ResetFlagAfterDelay(Action resetAction)
        {
            new Timer(_ => resetAction(), null, 3000, Timeout.Infinite);
        }
    }
}

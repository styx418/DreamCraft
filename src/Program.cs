using System;
using System.Collections.Generic;

namespace DreamCraft
{
    class Program
    {
        static void Main()
        {
            var modules = ModuleManager.LoadModules();

            foreach (var module in modules)
            {
                module.Start();
            }

            Logger.Info("Press ENTER to stop...");
            Console.ReadLine();

            foreach (var module in modules)
            {
                module.Stop();
            }
        }
    }
}

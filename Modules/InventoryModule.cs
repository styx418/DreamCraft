using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamServer
{
    public class InventoryModule : IModule
    {
        public void Init()
        {
            ConsoleUtils.Log("[InventoryModule] Initialisé", ConsoleColor.Gray);
        }
    }
}
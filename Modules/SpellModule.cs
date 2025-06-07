using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamServer
{
    public class SpellModule : IModule
    {
        public void Init()
        {
            ConsoleUtils.Log("[SpellModule] Initialisé", ConsoleColor.Gray);
        }
    }
}

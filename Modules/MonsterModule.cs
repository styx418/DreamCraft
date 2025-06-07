using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DreamServer
{
    public class MonsterModule : IModule
    {
        public void Init()
        {
            ConsoleUtils.Log("[MonsterModule] Initialisé", ConsoleColor.Gray);
        }
    }
}

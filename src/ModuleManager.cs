using System.Collections.Generic;

namespace DreamCraft
{
    public static class ModuleManager
    {
        // Initialize and return all server modules
        public static List<IServerModule> LoadModules()
        {
            const int tcpPort = 5000;
            const int udpPort = 5001;

            return new List<IServerModule>
            {
                new StartupBanner(),
                new TcpGameServer(tcpPort),
                new UdpGameServer(udpPort)
            };
        }
    }
}

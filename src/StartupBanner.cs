using System;

namespace DreamCraft
{
    public class StartupBanner : IServerModule
    {
        public void Start()
        {
            string[] banner = new[]
            {
                " ____                        ____                _   ",
                "|  _ \\ _ __ ___   __ _ _ __ |  _ \\ ___ _ __   __| |  ",
                "| | | | '__/ _ \\ / _` | '_ \\| |_) / _ \\ '_ \\ / _` |  ",
                "| |_| | | | (_) | (_| | | | |  _ <  __/ | | | (_| |  ",
                "|____/|_|  \\___/ \\__,_|_| |_|_| \\_\\___|_| |_|\\__,_|  ",
                "                                                        "
            };

            foreach (var line in banner)
            {
                Logger.Info(line);
            }
        }

        public void Stop()
        {
            // Banner has nothing to stop
        }
    }
}

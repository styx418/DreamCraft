
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace DreamServer.Modules
{
    public interface IModule
    {
        Task ExecuteAsync(TcpSessionClient client, string command, JsonElement payload);
    }

    public class ModuleManager
    {
        private static ModuleManager _instance;
        public static ModuleManager Instance => _instance ??= new ModuleManager();

        private readonly Dictionary<string, IModule> _modules = new();

        public void Register(string command, IModule module)
        {
            _modules[command.ToLower()] = module;
        }

        public async Task ExecuteAsync(TcpSessionClient client, string command, JsonElement payload)
        {
            if (_modules.TryGetValue(command.ToLower(), out var module))
            {
                await module.ExecuteAsync(client, command, payload);
            }
            else
            {
                Console.WriteLine($"[UNKNOWN COMMAND] {command}");
            }
        }
    }
}

using ChaseLabs.CLConfiguration.List;
using ChaseLabs.CLConfiguration.Object;
using com.drewchaseproject.net.asp.mc.OlegMC.Library.Models;
using System;
using static com.drewchaseproject.net.asp.mc.OlegMC.Library.Data.Values;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Data.Streams
{
    public static class Out
    {
        public static void CreateServer(string servername, string serverport, string maxplayers, string levelname, string difficulty, string seed)
        {
            Enum.TryParse(typeof(Difficulty), difficulty, true, out object? diff);

            ConfigManager manager = new ConfigManager(Values.GetServerManifestPath(servername));
            manager.Add(new Config("Name", servername, manager));
            manager.Add(new Config("MaxRam", "2GB", manager));
            manager.Add(new Config("MinRam", "128MB", manager));
            manager.Add(new Config("JavaArgument", Values.DefaultJavaArguments, manager));
            manager.Add(new Config("MinecraftArgument", "nogui", manager));
            manager.Add(new Config("Active", false + "", manager));
            manager.Add(new Config("StartingJar", "", manager));
            ServerModel server = new ServerModel() { Manifest = manager, Name = servername, Port = int.Parse(serverport), MaxPlayers = int.Parse(maxplayers), Difficulty = diff == null ? Difficulty.Peaceful : (Difficulty)diff, Seed = seed, LevelName = levelname };
            ServersListModel.Singleton.Servers.Add(server);
        }
    }
}

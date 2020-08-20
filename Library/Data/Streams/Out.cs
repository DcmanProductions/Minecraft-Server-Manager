using ChaseLabs.CLConfiguration.List;
using ChaseLabs.CLConfiguration.Object;
using com.drewchaseproject.net.asp.mc.OlegMC.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;
using static com.drewchaseproject.net.asp.mc.OlegMC.Library.Data.Values;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Data.Streams
{
    public static class Out
    {
        public static void CreateServer(string servername, string serverport, string maxplayers, string levelname, string difficulty, string seed)
        {
            Enum.TryParse(typeof(Difficulty), difficulty, true, out object? diff);

            var manager = new ConfigManager(Values.GetServerManifestPath(servername));
            manager.Add(new Config("Name", servername, manager));
            manager.Add(new Config("MaxRam", "2GB", manager));
            manager.Add(new Config("MinRam", "128MB", manager));
            manager.Add(new Config("JavaArgument", "-XX:+UseG1GC -XX:+ParallelRefProcEnabled -XX:MaxGCPauseMillis=200 -XX:+UnlockExperimentalVMOptions -XX:+DisableExplicitGC -XX:+AlwaysPreTouch -XX:G1NewSizePercent=30 -XX:G1MaxNewSizePercent=40 -XX:G1HeapRegionSize=8M -XX:G1ReservePercent=20 -XX:G1HeapWastePercent=5 -XX:G1MixedGCCountTarget=4 -XX:InitiatingHeapOccupancyPercent=15 -XX:G1MixedGCLiveThresholdPercent=90 -XX:G1RSetUpdatingPauseTimePercent=5 -XX:SurvivorRatio=32 -XX:+PerfDisableSharedMem -XX:MaxTenuringThreshold=1", manager));
            manager.Add(new Config("MinecraftArgument", "nogui", manager));
            manager.Add(new Config("Active", false + "", manager));
            manager.Add(new Config("StartingJar", "", manager));
            manager.Add(new Config("InstallJar", "", manager));
            var server = new ServerModel() { Manifest = manager, Name = servername, Port = int.Parse(serverport), MaxPlayers = int.Parse(maxplayers), Difficulty = diff == null ? Difficulty.Peaceful : (Difficulty) diff, Seed = seed, LevelName = levelname };
            ServersListModel.Singleton.Servers.Add(server);
        }
    }
}

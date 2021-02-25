using ChaseLabs.CLConfiguration.List;
using com.drewchaseproject.net.asp.mc.OlegMC.Library.Models;
using System.IO;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Data.Streams
{
    public static class In
    {
        public static void ImportPreExistingServers()
        {
            ServersListModel.Singleton.Servers.Clear();
            foreach (string file in Directory.GetFiles(Values.ServerDirectory, "*.manifest", SearchOption.AllDirectories))
            {
                string dir = Directory.GetParent(file).FullName;
                ConfigManager manager = new ConfigManager(file);
                string name = manager.GetConfigByKey("Name").Value;
                string minram = manager.GetConfigByKey("MinRam").Value;
                string maxram = manager.GetConfigByKey("MaxRam").Value;
                string javaargs = manager.GetConfigByKey("JavaArgument").Value;
                string mcargs = manager.GetConfigByKey("MinecraftArgument").Value;
                bool active = manager.GetConfigByKey("Active").ParseBoolean();
                string startjar = manager.GetConfigByKey("StartingJar").Value;
                ServersListModel.Singleton.Servers.Add(new ServerModel() { Manifest = manager, Name = name, Active = active, StartingJar = startjar, MinRam = minram, MaxRam = maxram, JavaArguments = javaargs, MinecraftArguments = mcargs });
            }
        }
    }
}

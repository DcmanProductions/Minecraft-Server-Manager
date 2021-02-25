using com.drewchaseproject.net.asp.mc.OlegMC.Library.Models;
using com.drewchaseproject.net.asp.mc.OlegMC.Library.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Data
{

    /// <summary>
    /// Contains internal values to be used across the application
    /// </summary>
    public class Values
    {
        #region Initializing the Singleton
        private static Values _singleton;
        public static Values Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = new Values();
                }

                return _singleton;
            }
        }
        private Values() { }
        #endregion

        #region Application Constants
        public static string ApplicationName => "Oleg MC";
        public static string CompanyName => "Chase Labs";

        public static string DefaultJavaArguments => "-XX:+UseG1GC -XX:+ParallelRefProcEnabled -XX:MaxGCPauseMillis=200 -XX:+UnlockExperimentalVMOptions -XX:+DisableExplicitGC -XX:+AlwaysPreTouch -XX:G1NewSizePercent=30 -XX:G1MaxNewSizePercent=40 -XX:G1HeapRegionSize=8M -XX:G1ReservePercent=20 -XX:G1HeapWastePercent=5 -XX:G1MixedGCCountTarget=4 -XX:InitiatingHeapOccupancyPercent=15 -XX:G1MixedGCLiveThresholdPercent=90 -XX:G1RSetUpdatingPauseTimePercent=5 -XX:SurvivorRatio=32 -XX:+PerfDisableSharedMem -XX:MaxTenuringThreshold=1";

        #endregion

        #region Directories
        public static string ApplicationRoot
        {
            get
            {
                string path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), CompanyName, ApplicationName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }
        public static string ConfigDirectory
        {
            get
            {
                string path = Path.Combine(ApplicationRoot, "Configuration");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }
        public static string LogDirectory
        {
            get
            {
                string path = Path.Combine(ApplicationRoot, "Log");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }


        public static string MinecraftDirectory
        {
            get
            {
                string path = Path.Combine(ApplicationRoot, "Minecraft");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        public static string ServerDirectory
        {
            get
            {
                string path = Path.Combine(MinecraftDirectory, "Servers");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }
        public static string ProfilesDirectory
        {
            get
            {
                string path = Path.Combine(MinecraftDirectory, "Profiles");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }
        public static string ImportsDirectory
        {
            get
            {
                string path = Path.Combine(MinecraftDirectory, "Imports");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }
        public static string BackupsDirectory
        {
            get
            {
                string path = Path.Combine(MinecraftDirectory, "Profiles");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }


        public static string ArchivesDirectory
        {
            get
            {
                string path = Path.Combine(MinecraftDirectory, "Archives");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        #endregion

        #region Files
        public string SettingsFile => Path.Combine(ConfigDirectory, "settings.cfg");
        public string LogFile => Path.Combine(LogDirectory, "latest.log");
        public string JavaEnvironment => CurrentOS.isWindows ? $"" : CurrentOS.isLinux ? $"" : $"";
        #endregion

        #region Local Variables
        public enum Difficulty
        {
            Peaceful,
            Easy,
            Normal,
            Hard
        }

        private List<Open.Nat.Mapping> openPorts;
        public List<Open.Nat.Mapping> OpenPorts
        {
            get => OpenPorts == null ? new List<Open.Nat.Mapping>() : openPorts;
            private set => openPorts = value;
        }

        public string CurrentView { get; set; }

        private ServerModel _selected = null;
        public ServerModel SelectedServer
        {
            get
            {
                if (_selected != null && ServersListModel.Singleton.GetServerByName(_selected.Name) == null)
                {
                    _selected = null;
                }

                return _selected;
            }
            set
            {
                if (!value.IsEmpty)
                {
                    _selected = value;
                }
            }
        }

        #endregion

        #region Methods
        public static string GetServerPath(ServerModel server)
        {
            return Path.Combine(ServerDirectory, GetDirectoryFriendlyName(server.Name));
        }
        public static string GetServerPath(string servername)
        {
            return Path.Combine(ServerDirectory, GetDirectoryFriendlyName(servername));
        }
        public static string GetServerManifestPath(ServerModel server)
        {
            return Path.Combine(GetServerPath(server), $"{GetDirectoryFriendlyName(server.Name)}.manifest");
        }
        public static string GetServerManifestPath(string servername)
        {
            return Path.Combine(GetServerPath(servername), $"{GetDirectoryFriendlyName(servername)}.manifest");
        }

        public static string GetDirectoryFriendlyName(string value)
        {
            char[] illegal = " `~!@#$%^&*()_+=|\\}{\"':;/?>.<,".ToCharArray();
            foreach (char c in illegal)
            {
                string s = c + "";
                if (value.Contains(c))
                {
                    value = value.Replace(s, "");
                }
            }
            return value;
        }

        #endregion

    }
}

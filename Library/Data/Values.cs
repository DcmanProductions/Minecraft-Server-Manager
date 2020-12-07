using com.drewchaseproject.net.asp.mc.OlegMC.Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Data
{
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
        #endregion

        #region Local Variables
        public enum Difficulty
        {
            Peaceful,
            Easy,
            Normal,
            Hard
        }

        public List<int> openPorts { get; set; }

        public string CurrentView { get; set; }
        public int Port { get => Configuration.Singleton.settings.GetConfigByKey("Port").ParseInt(); set => Configuration.Singleton.settings.GetConfigByKey("Port").Value = value + ""; }

        ServerModel _selected = null;
        public ServerModel SelectedServer
        {
            get
            {
                if (_selected != null && ServersListModel.Singleton.GetServerByName(_selected.Name) == null) _selected = null;
                return _selected;
            }
            set
            {
                if (!value.IsEmpty)
                    _selected = value;
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
            foreach (var c in illegal)
            {
                string s = c + "";
                if (value.Contains(c)) value = value.Replace(s, "");
            }
            return value;
        }

        #endregion

    }
}

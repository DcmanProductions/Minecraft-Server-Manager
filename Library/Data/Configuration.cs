using ChaseLabs.CLConfiguration.List;
using ChaseLabs.CLConfiguration.Object;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Data
{
    /// <summary>
    /// Handles the Application Settings.
    /// <br/>
    /// Contains static objects that can be set or retrieved from the config file.
    /// </summary>
    public class Configuration
    {
        public static int Port { get => Singleton.settings.GetConfigByKey("Port").ParseInt(); set => Singleton.settings.GetConfigByKey("Port").Value = value + ""; }
        public static bool AutoPortForward { get => Singleton.settings.GetConfigByKey("Auto Port Forward").ParseBoolean(); set => Singleton.settings.GetConfigByKey("Auto Port Forward").Value = value + ""; }

        private static Configuration _singleton;
        public static Configuration Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = new Configuration();
                }

                return _singleton;
            }
        }

        public ConfigManager settings;

        private Configuration()
        {
            settings = new ConfigManager(Values.Singleton.SettingsFile);
            settings.Add(new Config("Port", "2112", settings));
            settings.Add(new Config("Auto Port Forward", "True", settings));
        }
    }
}

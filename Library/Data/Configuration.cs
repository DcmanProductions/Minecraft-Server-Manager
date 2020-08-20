using ChaseLabs.CLConfiguration.List;
using ChaseLabs.CLConfiguration.Object;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Data
{
    public class Configuration
    {
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
        }
    }
}

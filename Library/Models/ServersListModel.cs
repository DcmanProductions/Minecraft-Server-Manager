using System.Collections.Generic;
using System.Linq;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Models
{
    public class ServersListModel
    {
        private static ServersListModel _singleton;
        public static ServersListModel Singleton
        {
            get
            {
                if (_singleton == null)
                {
                    _singleton = new ServersListModel();
                }

                return _singleton;
            }
        }

        public List<ServerModel> Servers;
        private ServersListModel()
        {
            Servers = new List<ServerModel>();
        }

        public ServerModel GetServerByName(string name)
        {
            ServerModel server = null;
            Servers.ForEach(s =>
            {
                if (s.Name.Equals(name))
                {
                    server = s;
                    return;
                }
            });

            return server;

        }
        public ServerModel GetServerByIndex(int index)
        {
            return Servers.ElementAt(index);
        }

        public ServerModel[] GetRunningServers()
        {
            List<ServerModel> running = new List<ServerModel>();
            Servers.ForEach(s =>
            {
                if (s.Active) running.Add(s);
            });
            return running.ToArray();
        }

    }
}

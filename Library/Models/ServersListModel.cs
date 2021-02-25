using System;
using System.Collections.Generic;
using System.Linq;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Models
{
    /// <summary>
    /// Contains a list of all servers and functions to sort them.
    /// </summary>
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
        /// <summary>
        /// A List of all servers
        /// </summary>
        public List<ServerModel> Servers;
        private ServersListModel()
        {
            Servers = new List<ServerModel>();
        }

        /// <summary>
        /// Gets the first server with matching name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the server at the specified index
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public ServerModel GetServerByIndex(int index)
        {
            try
            {
            return Servers.ElementAt(index);
            }
            catch (ArgumentOutOfRangeException)
            {
                return null;
            }
        }
        /// <summary>
        /// Returns a list of all currently running servers
        /// </summary>
        /// <returns></returns>
        public ServerModel[] GetRunningServers()
        {
            List<ServerModel> running = new List<ServerModel>();
            Servers.ForEach(s =>
            {
                if (s.Active)
                {
                    running.Add(s);
                }
            });
            return running.ToArray();
        }

    }
}


using com.drewchaseproject.net.asp.mc.OlegMC.Library.Data;
using Open.Nat;
using System;
using System.Threading;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Utilities
{
    /// <summary>
    /// Contains Utilities for Networking
    /// <br />
    /// Ex: Port Mapping
    /// </summary>
    public class NetworkUtilities
    {
        private static readonly ChaseLabs.CLLogger.Interfaces.ILog log = ChaseLabs.CLLogger.LogManger.Init().SetLogDirectory(Values.Singleton.LogFile);
        private static NetworkUtilities _singleton;
        public static NetworkUtilities Singleton => _singleton == null ? new NetworkUtilities() : _singleton;

        private readonly NatDiscoverer discover = new NatDiscoverer();
        private readonly CancellationTokenSource cts = new CancellationTokenSource(10000);
        private readonly NatDevice device;
        private bool canForward = true;

        public NetworkUtilities()
        {
            _singleton = this;

            try
            {
                device = discover.DiscoverDeviceAsync(PortMapper.Upnp, cts).Result;
            }
            catch (NatDeviceNotFoundException)
            {
                canForward = false;
                log.Info("NAT UPNP is not enabled on the router\nWill have to manually port forward");
            }
            catch
            {
                canForward = false;
                log.Info("Unknown Error while trying to discover NAT UPNP Settings on your router\nWill have to manually port forward");
            }
        }

        /// <summary>
        /// Opens Specific Ports to the Public Network
        /// </summary>
        /// <param name="ports"></param>
        public async void PortForward(params int[] ports)
        {
            if (canForward)
            {
                foreach (int port in ports)
                {
                    try
                    {
                        Mapping map = new Mapping(Protocol.Tcp, port, port, "OlegMC");
                        await device.CreatePortMapAsync(map);
                        log.Info($"Opening Port {port}");
                        Values.Singleton.OpenPorts.Add(map);
                    }
                    catch (Exception e)
                    {
                        log.Info($"Unknown Error Has Occurred while trying to port forward port {port}\nWill have to manually port forward\nERROR: {e.Message}\n\n{e.StackTrace}");
                        canForward = false;
                        return;
                    }
                }
            }
        }

        /// <summary>
        /// Closes Specific Ports
        /// <para>Leave Paramater Blank for All Open</para>
        /// </summary>
        /// <param name="ports"></param>
        public async void ClosePorts(params int[] ports)
        {
            if (canForward)
            {
                if (ports == null || ports.Length == 0)
                {
                    foreach (Mapping map in Values.Singleton.OpenPorts)
                    {

                        await device.DeletePortMapAsync(map);
                        log.Info($"Closing Mapping for port {map.PrivatePort}.");
                    }
                    Values.Singleton.OpenPorts.Clear();
                }
                else
                {
                    foreach (int port in ports)
                    {
                        Mapping map = null;
                        Values.Singleton.OpenPorts.ForEach(s =>
                        {
                            if (s.PrivatePort == port && s.PublicPort == port)
                            {
                                map = s;
                            }
                        });
                        if (map != null)
                        {
                            await device.DeletePortMapAsync(map);
                            Values.Singleton.OpenPorts.Remove(map);
                            log.Info($"Closing Mapping for port {port}.");
                        }
                        else
                        {
                            log.Info($"No Mapping for port {port} was found.\nNothing Happend!");
                            return;
                        }
                    }
                }
            }

        }
    }
}

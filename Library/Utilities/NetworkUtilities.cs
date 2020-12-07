
using com.drewchaseproject.net.asp.mc.OlegMC.Library.Data;
using Mono.Nat;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Utilities
{
    public class NetworkUtilities
    {
        /// <summary>
        /// Opens Specific Ports to the Public Network
        /// </summary>
        /// <param name="ports"></param>
        public static void PortForward(params int[] ports)
        {
            foreach (int port in ports)
            {
                NatUtility.DeviceFound += (s, a) =>
                {
                    INatDevice device = a.Device;

                    //Run When Device is Found
                    device.CreatePortMap(new Mapping(Protocol.Udp, port, port));
                    System.Console.WriteLine($"Opening Port for {port}");

                };
                //NatUtility.DeviceLost += (s, a) =>
                //{
                //};
                NatUtility.StartDiscovery();
            }
            if (Values.Singleton.openPorts == null) Values.Singleton.openPorts = new System.Collections.Generic.List<int>();
            Values.Singleton.openPorts.AddRange(ports);
        }

        /// <summary>
        /// Closes Specific Ports
        /// <para>Leave Paramater Blank for All Open</para>
        /// </summary>
        /// <param name="ports"></param>
        public static void ClosePorts(params int[] ports)
        {
            if (ports == null || ports.Length == 0) ports = Values.Singleton.openPorts.ToArray();
            foreach (int port in ports)
            {
                NatUtility.DeviceFound += (s, a) =>
                {
                    INatDevice device = a.Device;

                    //Run When Device is Found
                    device.DeletePortMap(new Mapping(Protocol.Udp, port, port));
                    System.Console.WriteLine($"Closeing Port for {port}");

                };
            }
        }

    }
}

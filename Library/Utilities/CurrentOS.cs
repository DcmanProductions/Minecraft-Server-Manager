using System.Runtime.InteropServices;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Library.Utilities
{
    public static class CurrentOS
    {
        public static bool isWindows => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        public static bool isMacOS => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
        public static bool isLinux => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
    }
}

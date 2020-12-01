using ChaseLabs.CLUpdate;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Windows.Update
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string remote_version = "";
            string root_directory = "";
            string config_directory = "";
            string application_directory = "";
            string local_version = "";
            string url_key = "app_url";
            string exe_key = "app_exe";
            string app_key = "app";
            string temp = Path.Combine(Environment.GetEnvironmentVariable("TEMP"), "ChaseLabs", "OlegMC");
            UpdateManager.Singleton.Init(remote_version, local_version);
            string download_url = UpdateManager.Singleton.GetArchiveURL(url_key);
            string launch_exe = UpdateManager.Singleton.GetExecutableName(exe_key);
            Console.WriteLine("Checking for Updates");
            Task.Run(() =>
            {
                Updater update = Updater.Init(download_url, temp, application_directory, launch_exe);
                string path = Path.Combine(temp, "LauncherUpdater.exe");
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(Directory.GetParent(path).FullName);
                    }

                    client.DownloadFile("http://dl.drewchaseproject.com/LauncherUpdater.exe", path);
                    client.Dispose();
                }
                new Process() { StartInfo = new ProcessStartInfo() { FileName = $"\"{path}\"", Arguments = $"\"{Directory.GetParent(Assembly.GetExecutingAssembly().Location).FullName}\" \"{download_url}\" \"{launch_exe}\"", CreateNoWindow = false } }.Start();
                Environment.Exit(0);
            });
        }
    }
}

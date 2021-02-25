using com.drewchaseproject.net.asp.mc.OlegMC.Library.Data;
using com.drewchaseproject.net.asp.mc.OlegMC.Library.Data.Streams;
using com.drewchaseproject.net.asp.mc.OlegMC.Library.Models;
using com.drewchaseproject.net.asp.mc.OlegMC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using static com.drewchaseproject.net.asp.mc.OlegMC.Library.Data.Values;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ChaseLabs.CLLogger.Interfaces.ILog log = ChaseLabs.CLLogger.LogManger.Init().SetLogDirectory(Values.Singleton.LogFile);
        public static HomeController Singleton { get; private set; }

        public HomeController(ILogger<HomeController> logger)
        {
            Singleton = this;
        }
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Template/dashboard")]
        public IActionResult Dashboard()
        {
            log.Debug($"Loading OlegMC Dashboard");
            In.ImportPreExistingServers();
            return View();
        }

        public IActionResult AcceptEULA()
        {

            log.Debug($"Accepted EULA for {Values.Singleton.SelectedServer.Name}");
            Values.Singleton.SelectedServer.AcceptedEULA = true;
            return RedirectToAction("Status");
        }

        [Route("/Template/create")]
        public IActionResult CreateServer()
        {
            log.Debug($"Loading Create Server View");
            return View();
        }

        [Route("/Template/add")]
        public IActionResult AddServer(string servername, string serverport, string maxplayers, string levelname, string difficulty, string seed)
        {
            log.Debug(
                $"Processing the Creation of a Server with the Following Paramaters",
                $"Name: {servername}",
                $"Port: {serverport}",
                $"Max Players: {maxplayers}",
                $"Level Name: {levelname}",
                $"Difficulty: {difficulty}",
                $"Seed: {seed}"
                );
            servername = string.IsNullOrWhiteSpace(servername) ? "Latest Server" : servername;
            serverport = string.IsNullOrWhiteSpace(serverport) ? "25565" : serverport;
            maxplayers = string.IsNullOrWhiteSpace(maxplayers) ? "20" : maxplayers;
            levelname = string.IsNullOrWhiteSpace(levelname) ? "world" : levelname;
            difficulty = difficulty == "" ? Difficulty.Peaceful.ToString() : difficulty;
            Out.CreateServer(servername, serverport, maxplayers, levelname, difficulty, seed);
            return RedirectToAction("Index");
        }

        [Route("/Template/profiles")]
        public IActionResult Profiles()
        {
            log.Debug($"Loading Server Profiles");
            return View();
        }

        [Route("/Template/status")]
        public IActionResult Status(string name)
        {
            try
            {
                Values.Singleton.SelectedServer = ServersListModel.Singleton.GetServerByName(name) == null ? Values.Singleton.SelectedServer : ServersListModel.Singleton.GetServerByName(name);
            }
            catch
            {
                return RedirectToAction("dashboard");
            }
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null)
            {
                return RedirectToAction("dashboard");
            }
            log.Debug($"Loading Server Status for {Values.Singleton.SelectedServer.Name}");
            return View();
        }

        [Route("/Template/Status/start")]
        public IActionResult StartServer()
        {
            log.Debug($"Starting Server: {Values.Singleton.SelectedServer.Name}");
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
            {
                return RedirectToAction("dashboard");
            }

            Values.Singleton.SelectedServer.Active = true;
            return RedirectToAction("status");
        }

        [Route("/Template/Status/stop")]
        public IActionResult StopServer(ServerModel.StopMethod method)
        {
            log.Debug($"Stopping Server: {Values.Singleton.SelectedServer.Name}");
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
            {
                return RedirectToAction("dashboard");
            }

            Values.Singleton.SelectedServer.Active = false;
            Values.Singleton.SelectedServer.StopServer(method);
            return RedirectToAction("status", "Home");
        }

        [Route("/Template/Status/delete")]
        public IActionResult DeleteServer()
        {
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
            {
                return RedirectToAction("dashboard");
            }
            log.Debug($"Deleting Server: {Values.Singleton.SelectedServer.Name}");
            Values.Singleton.SelectedServer.DeleteServer();
            return RedirectToAction("dashboard");
        }

        [Route("/Template/properties")]
        public IActionResult Properties()
        {
            log.Debug($"Loading Properties for {Values.Singleton.SelectedServer.Name}");
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
            {
                return RedirectToAction("Dashboard");
            }

            Values.Singleton.SelectedServer.ServerProperties.Read();
            return View();
        }
        [Route("/Template/console")]
        public IActionResult Console()
        {
            log.Debug($"Loading Console for {Values.Singleton.SelectedServer.Name}");
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
            {
                return RedirectToAction("dashboard");
            }

            return View();
        }
        [Route("/Template/Console/Text")]
        public IActionResult ConsoleText()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult SaveProperties(string key, string value)
        {
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
            {
                return RedirectToAction("dashboard");
            }

            Values.Singleton.SelectedServer.ServerProperties.SetValue(key, value);
            log.Debug($"Saving Server Properties for {Values.Singleton.SelectedServer.Name}");
            return RedirectToAction("properties");
        }

        [Route("/Status/SaveJavaSettings")]
        public IActionResult SaveJavaSettings(string maxram = "", string maxramext = "G", string minram = "", string minramext = "M", string javaargs = "", string mcargs = "", string startjar = "")
        {
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
            {
                return RedirectToAction("dashboard");
            }

            Values.Singleton.SelectedServer.StartingJar = startjar == "Placeholder" ? "" : startjar;
            Values.Singleton.SelectedServer.MinecraftArguments = mcargs;
            Values.Singleton.SelectedServer.JavaArguments = javaargs;
            Values.Singleton.SelectedServer.MaxRam = maxram == "" ? Values.Singleton.SelectedServer.MaxRam : maxram + maxramext;
            Values.Singleton.SelectedServer.MinRam = minram == "" ? Values.Singleton.SelectedServer.MinRam : minram + minramext;
            log.Debug($"Saving Java Settings for {Values.Singleton.SelectedServer.Name}");
            return RedirectToAction("status");
        }

        public IActionResult SendMessage(string msg)
        {
            log.Debug($"Sending Console Message: {msg}");
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
            {
                return RedirectToAction("dashboard");
            }

            Values.Singleton.SelectedServer.SendCommand(msg);
            return RedirectToAction("console");
        }


        [Route("/Template/Sidebar")]
        public IActionResult _SideBar()
        {
            return View();
        }

        [HttpPost]
        [Route("/UploadJarFile")]
        public async System.Threading.Tasks.Task<IActionResult> UploadJarFiles()
        {
            string path = Values.Singleton.SelectedServer.ServerDirectory;
            Microsoft.AspNetCore.Http.IFormFileCollection files = Request.Form.Files;

            string sFile = "";
            foreach (Microsoft.AspNetCore.Http.IFormFile file in files)
            {
                sFile += $"\n{file.FileName}";
                string fileName = file.FileName;
                string fileNameOnServer = System.IO.Path.Combine(path, fileName);
                using (System.IO.FileStream stream = new System.IO.FileStream(fileNameOnServer, System.IO.FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

            }
            return Json("Upload Successful!");
        }

        [Route("/InstallForge")]
        public IActionResult IntallForge()
        {

            log.Debug($"Starting Server: {Values.Singleton.SelectedServer.Name}");
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
            {
                return RedirectToAction("dashboard");
            }

            Values.Singleton.SelectedServer.InstallForge();
            return RedirectToAction("Console");
        }


    }
}

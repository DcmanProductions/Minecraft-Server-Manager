using com.drewchaseproject.net.asp.mc.OlegMC.Library.Data;
using com.drewchaseproject.net.asp.mc.OlegMC.Library.Data.Streams;
using com.drewchaseproject.net.asp.mc.OlegMC.Library.Models;
using com.drewchaseproject.net.asp.mc.OlegMC.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using static com.drewchaseproject.net.asp.mc.OlegMC.Library.Data.Values;

namespace com.drewchaseproject.net.asp.mc.OlegMC.Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        public static HomeController Singleton { get; private set; }

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            Singleton = this;
        }
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("/Template/Dashboard")]
        public IActionResult Dashboard()
        {
            In.ImportPreExistingServers();
            return View();
        }

        public IActionResult AcceptEULA()
        {
            Values.Singleton.SelectedServer.AcceptedEULA = true;
            return RedirectToAction("Status");
        }

        [Route("/Template/CreateServer")]
        public IActionResult CreateServer()
        {
            return View();
        }

        [Route("/Template/AddServer")]
        public IActionResult AddServer(string servername, string serverport, string maxplayers, string levelname, string difficulty, string seed)
        {
            servername = string.IsNullOrWhiteSpace(servername) ? "Latest Server" : servername;
            serverport = string.IsNullOrWhiteSpace(serverport) ? "25565" : serverport;
            maxplayers = string.IsNullOrWhiteSpace(maxplayers) ? "20" : maxplayers;
            levelname = string.IsNullOrWhiteSpace(levelname) ? "world" : levelname;
            difficulty = difficulty == "" ? Difficulty.Peaceful.ToString() : difficulty;
            System.Console.WriteLine($"{servername}, {serverport}, {maxplayers}, {levelname}, {difficulty}, {seed}");
            Out.CreateServer(servername, serverport, maxplayers, levelname, difficulty, seed);
            return RedirectToAction("Index");
            //return null;
        }

        [Route("/Template/Profiles")]
        public IActionResult Profiles()
        {
            return View();
        }

        [Route("/Template/Status")]
        public IActionResult Status(string name)
        {
            try
            {
                Values.Singleton.SelectedServer = ServersListModel.Singleton.GetServerByName(name) == null ? Values.Singleton.SelectedServer : ServersListModel.Singleton.GetServerByName(name);
            }
            catch
            {
                return RedirectToAction("Dashboard");
            }
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null)
            {
                return RedirectToAction("Dashboard");
            }
            return View();
        }

        [Route("/Template/Status/StartServer")]
        public IActionResult StartServer()
        {
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
                return RedirectToAction("Dashboard");
            Values.Singleton.SelectedServer.Active = true;
            return RedirectToAction("Status");
        }

        [Route("/Template/Status/StartServer")]
        public IActionResult StopServer(ServerModel.StopMethod method)
        {
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
                return RedirectToAction("Dashboard");
            Values.Singleton.SelectedServer.Active = false;
            Values.Singleton.SelectedServer.StopServer(method);
            return RedirectToAction("Status", "Home");
        }

        [Route("/Template/Properties")]
        public IActionResult Properties()
        {
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
                return RedirectToAction("Dashboard");
            Values.Singleton.SelectedServer.ServerProperties.Read();
            return View();
        }
        [Route("/Template/Console")]
        public IActionResult Console()
        {
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
                return RedirectToAction("Dashboard");
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
                return RedirectToAction("Dashboard");
            Values.Singleton.SelectedServer.ServerProperties.SetValue(key, value);
            return RedirectToAction("Properties");
        }

        [Route("/Status/SaveJavaSettings")]
        public IActionResult SaveJavaSettings(string maxram = "", string maxramext = "G", string minram = "", string minramext = "M", string javaargs = "", string mcargs = "", string startjar = "")
        {
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
                return RedirectToAction("Dashboard");
            Values.Singleton.SelectedServer.StartingJar = startjar == "Placeholder" ? "" : startjar + ".jar";
            Values.Singleton.SelectedServer.MinecraftArguments = mcargs;
            Values.Singleton.SelectedServer.JavaArguments = javaargs;
            Values.Singleton.SelectedServer.MaxRam = maxram == "" ? Values.Singleton.SelectedServer.MaxRam : maxram + maxramext;
            Values.Singleton.SelectedServer.MinRam = minram == "" ? Values.Singleton.SelectedServer.MinRam : minram + minramext;
            return RedirectToAction("Status");
        }

        public IActionResult SendMessage(string msg)
        {
            if (Values.Singleton.SelectedServer == null || Values.Singleton.SelectedServer.Manifest == null || Values.Singleton.SelectedServer.IsEmpty)
                return RedirectToAction("Dashboard");
            Values.Singleton.SelectedServer.SendCommand(msg);
            return RedirectToAction("Console");
        }


        [Route("/Template/Sidebar")]
        public IActionResult _SideBar()
        {
            return View();
        }

    }
}

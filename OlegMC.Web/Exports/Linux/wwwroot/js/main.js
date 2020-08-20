function ChangeView(calling) {
    //var frame = document.getElementById('portal');
    //frame.src=calling.dataset.url;
    window.location.href = calling.dataset.url;
    var items = document.getElementsByClassName('sidebar-item');
    for (var i = 0; i < items.length; i++) {
        items[i].classList.remove("sidebar-active");
    }
    calling.classList.add("sidebar-active");
}

function ChangeViewWithIndex(index) {
    var items = document.getElementsByClassName('sidebar-item');
    for (var i = 0; i < items.length; i++) {
        items[i].classList.remove("sidebar-active");
    }
    items[index].classList.add("sidebar-active");
}


function resizeIFrame(frame) {
    // frame.style.width = frame.contentWindow.document.documentElement.scrollWidth+"px";
}

function ToggleRam(e, id) {
    var text = e.innerHTML.trim();
    var input = document.getElementById(id);
    if (text == "GB") {
        e.innerHTML = "MB";
        input.value = "M";
    } else {
        e.innerHTML = "GB";
        input.value = "G";
    }
}

function dropdown(id) {
    var dropdown = document.getElementById(id);
    dropdown.classList.toggle("show");
}

window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}

function loadConsole(e) {
    var console = document.getElementById(e);
    console.value = "HELLO";
    console.value = "[06Aug2020 07:00:04.858] [main/INFO] [cpw.mods.modlauncher.Launcher/MODLAUNCHER]: ModLauncher running: args [--gameDir, ., --launchTarget, fmlserver, --fml.forgeVersion, 31.2.0, --fml.mcpVersion, 20200515.085601, --fml.mcVersion, 1.15.2, --fml.forgeGroup, net.minecraftforge, nogui]<br>[06Aug2020 07:00:04.866] [main/INFO] [cpw.mods.modlauncher.Launcher/MODLAUNCHER]: ModLauncher 5.1.0+69+master.79f13f7 starting: java version 1.8.0_265 by Private Build<br>[06Aug2020 07:00:04.891] [main/DEBUG] [cpw.mods.modlauncher.LaunchServiceHandler/MODLAUNCHER]: Found launch services [minecraft,testharness,fmlclient,fmlserver]<br>[06Aug2020 07:00:04.931] [main/DEBUG] [cpw.mods.modlauncher.NameMappingServiceHandler/MODLAUNCHER]: Found naming services : []<br>[06Aug2020 07:00:04.965] [main/DEBUG] [cpw.mods.modlauncher.LaunchPluginHandler/MODLAUNCHER]: Found launch plugins: [eventbus,object_holder_definalize,runtime_enum_extender,accesstransformer,capability_inject_definalize,runtimedistcleaner]<br>[06Aug2020 07:00:04.998] [main/DEBUG] [cpw.mods.modlauncher.TransformationServicesHandler/MODLAUNCHER]: Discovering transformation services<br>[06Aug2020 07:00:05.012] [main/DEBUG] [cpw.mods.modlauncher.TransformationServicesHandler/MODLAUNCHER]: Found additional transformation services from discovery services: []<br>[06Aug2020 07:00:05.056] [main/DEBUG] [cpw.mods.modlauncher.TransformationServicesHandler/MODLAUNCHER]: Found transformer services : [fml]<br>[06Aug2020 07:00:05.058] [main/DEBUG] [cpw.mods.modlauncher.TransformationServicesHandler/MODLAUNCHER]: Transformation services loading<br>[06Aug2020 07:00:05.060] [main/DEBUG] [cpw.mods.modlauncher.TransformationServiceDecorator/MODLAUNCHER]: Loading service fml<br>[06Aug2020 07:00:05.061] [main/DEBUG] [net.minecraftforge.fml.loading.FMLServiceProvider/]: Injecting tracing printstreams for STDOUT/STDERR.<br>[06Aug2020 07:00:05.067] [main/DEBUG] [net.minecraftforge.fml.loading.LauncherVersion/CORE]: Found FMLLauncher version 31.2<br>[06Aug2020 07:00:05.067] [main/DEBUG] [net.minecraftforge.fml.loading.FMLLoader/CORE]: FML 31.2 loading<br>[06Aug2020 07:00:05.068] [main/DEBUG] [net.minecraftforge.fml.loading.FMLLoader/CORE]: FML found ModLauncher version : 5.1.0+69+master.79f13f7<br>[06Aug2020 07:00:05.068] [main/DEBUG] [net.minecraftforge.fml.loading.FMLLoader/CORE]: Initializing modjar URL handler<br>[06Aug2020 07:00:05.071] [main/DEBUG] [net.minecraftforge.fml.loading.FMLLoader/CORE]: FML found AccessTransformer version : 2.1.1+55+master.08d32b9<br>[06Aug2020 07:00:05.073] [main/DEBUG] [net.minecraftforge.fml.loading.FMLLoader/CORE]: FML found EventBus version : 2.2.0+59+master.4f71e48<br>[06Aug2020 07:00:05.074] [main/DEBUG] [net.minecraftforge.fml.loading.FMLLoader/CORE]: Found Runtime Dist Cleaner<br>[06Aug2020 07:00:05.081] [main/DEBUG] [net.minecraftforge.fml.loading.FMLLoader/CORE]: FML found CoreMod version : 2.0.3+8+master.ca72757<br>[06Aug2020 07:00:05.082] [main/DEBUG] [net.minecraftforge.fml.loading.FMLLoader/CORE]: Found ForgeSPI package implementation version 2.1.2+9+master.a8b4d92<br>[06Aug2020 07:00:05.082] [main/DEBUG] [net.minecraftforge.fml.loading.FMLLoader/CORE]: Found ForgeSPI package specification 3<br>[06Aug2020 07:00:05.512] [main/INFO] [net.minecraftforge.fml.loading.FixSSL/CORE]: Added Lets Encrypt root certificates as additional trust<br>[06Aug2020 07:00:05.520] [main/DEBUG] [cpw.mods.modlauncher.TransformationServiceDecorator/MODLAUNCHER]: Loaded service fml<br>[06Aug2020 07:00:05.551] [main/DEBUG] [cpw.mods.modlauncher.TransformationServicesHandler/MODLAUNCHER]: Configuring option handling for services<br>[06Aug2020 07:00:05.617] [main/DEBUG] [cpw.mods.modlauncher.TransformationServicesHandler/MODLAUNCHER]: Transformation services initializing<br>[06Aug2020 07:00:05.624] [main/DEBUG] [cpw.mods.modlauncher.TransformationServiceDecorator/MODLAUNCHER]: Initializing transformation service fml<br>[06Aug2020 07:00:05.625] [main/DEBUG] [net.minecraftforge.fml.loading.FMLServiceProvider/CORE]: Setting up basic FML game directories<br>[06Aug2020 07:00:05.627] [main/DEBUG] [net.minecraftforge.fml.loading.FileUtils/CORE]: Found existing GAMEDIR directory : /var/games/minecraft/servers/SMP_Server<br>[06Aug2020 07:00:05.629] [main/DEBUG] [net.minecraftforge.fml.loading.FMLPaths/CORE]: Path GAMEDIR is /var/games/minecraft/servers/SMP_Server<br>[06Aug2020 07:00:05.629] [main/DEBUG] [net.minecraftforge.fml.loading.FileUtils/CORE]: Found existing MODSDIR directory : /var/games/minecraft/servers/SMP_Server/mods<br>[06Aug2020 07:00:05.629] [main/DEBUG] [net.minecraftforge.fml.loading.FMLPaths/CORE]: Path MODSDIR is /var/games/minecraft/servers/SMP_Server/mods<br>[06Aug2020 07:00:05.629] [main/DEBUG] [net.minecraftforge.fml.loading.FileUtils/CORE]: Found existing CONFIGDIR directory : /var/games/minecraft/servers/SMP_Server/config<br>[06Aug2020 07:00:05.630] [main/DEBUG] [net.minecraftforge.fml.loading.FMLPaths/CORE]: Path CONFIGDIR is /var/games/minecraft/servers/SMP_Server/config";
}

function SubmitForm(id) {
    document.getElementById(id).submit();
}
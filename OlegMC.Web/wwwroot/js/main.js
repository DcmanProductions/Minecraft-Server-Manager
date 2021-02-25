
function ToggleRam(e, id) {
    var text = e.innerHTML.trim();
    var input = document.getElementById(id);
    if (text == "GB") {
        e.innerHTML = "MB";
        input.value = "MB";
    } else {
        e.innerHTML = "GB";
        input.value = "GB";
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
}

function SubmitForm() {
    $('#background-tasks')[0].innerHTML = "";
    saveJavaSettings($('#max-ram-setting-value')[0].value, $('#maxramext')[0].value, $('#min-ram-setting-value')[0].value, $('#minramext')[0].value, $('#java-args-setting')[0].value, $('#mc-args-setting')[0].value, $('#startjar option:selected').text())
}

function SaveProperty(e) {
    $('#background-tasks')[0].innerHTML = "";
    $('#background-tasks').load(`/Template/properties?key=${e.id}&value=${e.value}`);
    setTimeout(() => {
        $('#background-tasks')[0].innerHTML = "";
    }, 100)
}

function SendConsoleCommand(e, input) {
    $('#background-tasks')[0].innerHTML = "";
    if (e.keyCode === 13) {
        e.preventDefault();
        let message = input.value.replace(/ /gi, "%20");
        $('#background-tasks').load(`/Home/SendMessage?msg=${message}`);
        $('#console-view').load("/Template/Console/Text")
        $('#console-view')[0].scrollTop = $('#console-view')[0].scrollHeight;
        input.value = "";
        setTimeout(() => {
            $('#background-tasks')[0].innerHTML = "";
        }, 1000)
    }
}
function deleteServer() {
    $('#background-tasks')[0].innerHTML = "";
    $('#background-tasks').load('/Template/Status/delete');
    setTimeout(() => {
        $('#background-tasks')[0].innerHTML = "";
        LoadView(0)
        $("#navigation_side_bar").load("/Template/Sidebar");
    }, 1000)
}
function addServer(servername, serverport, maxplayers, levelname, difficulty, seed) {
    $('#background-tasks').load(`/Template/add?servername=${servername}&serverport=${serverport}&maxplayers=${maxplayers}&levelname=${levelname}&difficulty=${difficulty}&seed=${seed}`);
    setTimeout(() => {
        $('#background-tasks')[0].innerHTML = "";
        LoadView(0)
    }, 1500)
}
function saveJavaSettings(maxram = "", maxramext = "G", minram = "", minramext = "M", javaargs = "", mcargs = "", startjar = "") {

    $('#background-tasks')[0].innerHTML = "";
    let args = `maxram=${maxram}&maxramext=${maxramext}&minram=${minram}&minramext=${minramext}`;
    args += (startjar === "---Select Jar---" ? "" : `&startjar=${startjar}`) + (javaargs === "" ? "" : `&javaargs=${javaargs}`);
    console.log(args);
    $('#background-tasks').load(`/Status/SaveJavaSettings?${args}`);//`/Status/SaveJavaSettings?maxram=${maxram}&maxramext=${maxramext}&minram=${minram}&minramext=${minramext}&javaargs=${javaargs}&mcargs=${mcargs}&startjar=${startjar}`
    setTimeout(() => {
        LoadView(3);
        $('#background-tasks')[0].innerHTML = "";
    }, 100)
}

function TryParseInt(str, value) {
    let retValue = value;
    if (str !== null) {
        if (str.length > 0) {
            if (!isNaN(str)) {
                retValue = parseInt(str);
            }
        }
    }
    return retValue;
}

function UploadJarFile() {
    $('#background-tasks')[0].innerHTML = "";
    let fileUpload = $("#upload-jar-file")[0];
    let files = fileUpload.files;
    let data = new FormData();
    for (let i = 0; i < files.length; i++) {
        data.append(files[i].name, files[i]);
    }
    $.ajax({
        type: "POST",
        url: "/UploadJarFile",
        contentType: false,
        processData: false,
        data: data,
        success: message => {
            console.log(message);
            SubmitForm();
        },
        error: message => { console.error(message); }
    });
}
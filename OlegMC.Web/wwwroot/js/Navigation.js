
var nav = $('#nav');

loadNavigationMachanics();
LoadView();
setNavigationToggle();

function ToggleNavigation() {
    const collapsed = nav.hasClass('navigation-collapsed');
    setNavigationToggle(collapsed);
}

function setNavigationToggle(collapsed = false) {
    nav.addClass(collapsed ? "navigation-expanded" : "navigation-collapsed")
    nav.removeClass(collapsed ? "navigation-collapsed" : "navigation-expanded")
}

function LoadView(url = 'dashboard', called = null) {
    if (isNaN(url)) {
        url = url.toLowerCase();

        if (called != null) {
            $('#main-area').load(`/Template/${url}`)
            Array.from(document.getElementsByClassName('menuItem')).forEach(s => { s.classList.remove('active') })
            called.classList.add('active')
        } else {
            Array.from(document.getElementsByClassName('menuItem'))[0].click();
        }
    } else {
        let index = parseInt(url) + 1;
        Array.from(document.getElementsByClassName('menuItem'))[index].click();
    }
    setNavigationToggle();
}


function SetActiveMenuItem(item) {
    if (!isNaN(item)) {
        let index = parseInt(item) + 1;
        Array.from(document.getElementsByClassName('menuItem')).forEach(s => { s.classList.remove('active') })
        Array.from(document.getElementsByClassName('menuItem'))[index].classList.add('active');
    }
}


function loadNavigationMachanics() {
    var timer;
    nav.on({
        mouseenter: function () {
            timer = setTimeout(() => {
                setNavigationToggle(true);
            }, 1500);
        },
        mouseleave: function () {
            setNavigationToggle(false);
            clearTimeout(timer);
        }
    });
}
var contextMenu = {
    menu: $("#context-menu"),
    menuState: 0,
    activeClassName: "context-menu--active",
    contextClassName: "field",
    position: {
        x: 0,
        y: 0
    },
    menuSize: {
        width: 0,
        height: 0
    },
    coords: {
        x: 0,
        y: 0
    }
};


function initializeContextMenu() {
    setupContext();
    clickOutsideContext();
    handleEscapeClick();
    resizeListener();
}


function setupContext() {
    $(".field").each(function () {
        contextMenuListener($(this));
    });
}

function contextMenuListener(item) {
    $(item).bind("contextmenu", function (e) {
        //check if we click inside element
        if (clickInsideElement(e)) {
            //stop default browser context menu
            e.preventDefault();
            //show field menu
            showMenu();
            selectFile(e.currentTarget);
            positionMenu(e);
            return;
        }
        hideMenu();
    });
}

function hideMenu() {
    if (contextMenu.menuState === 1) {
        removePreviousSelectedFiles();
        contextMenu.menuState = 0;
        $(contextMenu.menu).removeClass(contextMenu.activeClassName);
    }
}

function showMenu() {
    if (contextMenu.menuState === 0) {
        contextMenu.menuState = 1;
        $(contextMenu.menu).addClass(contextMenu.activeClassName);
    }
}

function clickInsideElement(e) {
    var el = e.srcElement || e.target;

    if (el.classList.contains(contextMenu.contextClassName)) {
        return el;
    } else {
        while (el === el.parentNode) {
            if (el.classList && el.classList.contains(contextMenu.contextClassName)) {
                return el;
            }
        }
    }

    return false;
}


function clickOutsideContext() {
    $(document).on("click", function (e) {
        var button = e.which || e.button;
        //if user didnt click on context
        // we hide context
        if (button === 1) {
            hideMenu();
        }
    });

}

function handleEscapeClick() {
    window.onkeyup = function (e) {
        if (e.keyCode === 27) {
            hideMenu();
        }
    };
}

function positionMenu(e) {
    contextMenu.coords = getPosition(e);

    contextMenu.menuSize.width = $(contextMenu.menu).offsetWidth + 4;
    contextMenu.menuSize.height = $(contextMenu.menu).offsetHeight + 4;
    var top = 0;
    var left = 0;
    if ((window.innerWidth - contextMenu.coords.x) < contextMenu.menuSize.width) {
        left = window.innerWidth - contextMenu.menuSize.width + "px";
    } else {
        left = contextMenu.coords.x + "px";
    }

    if ((window.innerHeight - contextMenu.coords.y) < contextMenu.menuSize.height) {
        top = window.innerHeight - contextMenu.menuSize.height + "px";
    } else {
        top = contextMenu.coords.y + "px";
    }
    $(contextMenu.menu).css({
        left: left,
        top: top
    });



}


function getPosition(e) {
    var posx = 0;
    var posy = 0;

    if (!e) {
        e = window.event;
    }

    if (e.pageX || e.pageY) {
        posx = e.pageX;
        posy = e.pageY;
    } else if (e.clientX || e.clientY) {
        posx = e.clientX + document.body.scrollLeft +
                           document.documentElement.scrollLeft;
        posy = e.clientY + document.body.scrollTop +
                           document.documentElement.scrollTop;
    }

    return {
        x: posx,
        y: posy
    };
}

function resizeListener() {
    window.onresize = function (e) {
        hideMenu();
    };
}
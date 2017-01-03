//library helper
function isFunction(item) {
    return !(item === undefined || typeof item !== 'function');
}

function ContextMenu(selector, options) {
    this.states = {
        menuState: 0,
        menuSize: {
            width: 0,
            height: 0
        },
        coords: {
            x: 0,
            y: 0
        }
    };

    this.contextMenu = {
        selector: $(".field"),
        menu: $("#context-menu"),
        clickCondition: this._clickInsideElement,
        activeClassName: "context-menu--active",
        contextClassName: "field"
    };

    this._setOptions(selector,options);
    this._setupContext();
    this._clickOutsideContext();
    this._handleEscapeClick();
    this._resizeListener();
}

ContextMenu.prototype._setOptions = function (selector, options) {
    if (!options) {
        return;
    }
    if (selector !== undefined) {
        this.contextMenu.selector = selector;
    }
    if (options.menu !== undefined) {
        this.contextMenu.menu = options.menu;
    }
    if (options.activeClassName !== undefined) {
        this.contextMenu.activeClassName = options.activeClassName;
    }
    if (options.contextClassName !== undefined) {
        this.contextMenu.contextClassName = options.contextClassName;
    }
    if (options.clickCondition !== undefined) {
        this.contextMenu.clickCondition = options.clickCondition;
    }
    if (options.correctClick !== undefined) {
        this.contextMenu.correctClick = options.correctClick;
    }
    if (options.onOpen !== undefined) {
        this.contextMenu.onOpen = options.onOpen;
    }
};


ContextMenu.prototype._setupContext = function () {
    var $this = this;
    $(this.contextMenu.selector).each(function () {
        $this._contextMenuListener($(this));
    });
};

ContextMenu.prototype._contextMenuListener = function (item) {
    var $this = this;
    $(item).bind("contextmenu", function (e) {
        if ($this.contextMenu.clickCondition(e)) {
            e.preventDefault();
            $this._showMenu();
            if (isFunction($this.contextMenu.correctClick)) {
                $this.contextMenu.correctClick(e);
            }
            //selectFile(e.currentTarget);
            $this._positionMenu(e);
            return;
        }
        $this._hideMenu();
    });
};


ContextMenu.prototype._resizeListener = function () {
    var $this = this;
    window.onresize = function (e) {
        $this._hideMenu();
    };
};

ContextMenu.prototype._hideMenu = function () {
    if (this.states.menuState === 1) {
        removePreviousSelectedFiles();
        this.states.menuState = 0;

       
        $(this.contextMenu.menu).removeClass(this.contextMenu.activeClassName);
    }
};

ContextMenu.prototype._showMenu = function () {
    if (this.states.menuState === 0) {
        this.states.menuState = 1;
        if (isFunction(this.contextMenu.onOpen)) {
            this.contextMenu.onOpen();
        }

        $(this.contextMenu.menu).addClass(this.contextMenu.activeClassName);
    }
};

ContextMenu.prototype._clickOutsideContext = function () {
    var $this = this;
    $(document).on("click", function (e) {
        var button = e.which || e.button;
        //if user didnt click on context
        // we hide context
        if (button === 1) {
            $this._hideMenu();
        }
    });

};

ContextMenu.prototype._handleEscapeClick = function () {
    var $this = this;
    window.onkeyup = function (e) {
        if (e.keyCode === 27) {
            $this._hideMenu();
        }
    };
};


ContextMenu.prototype._positionMenu = function (e) {
    this.states.coords = this._getPosition(e);

    this.states.menuSize.width = $(this.contextMenu.menu).offsetWidth + 4;
    this.states.menuSize.height = $(this.contextMenu.menu).offsetHeight + 4;
    var top = 0;
    var left = 0;
    if ((window.innerWidth - this.states.coords.x) < this.states.menuSize.width) {
        left = window.innerWidth - this.states.menuSize.width + "px";
    } else {
        left = this.states.coords.x + "px";
    }

    if ((window.innerHeight - this.states.coords.y) < this.states.menuSize.height) {
        top = window.innerHeight - this.states.menuSize.height + "px";
    } else {
        top = this.states.coords.y + "px";
    }
    $(this.contextMenu.menu).css({
        left: left,
        top: top
    });
};


ContextMenu.prototype._getPosition = function (e) {
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
};

ContextMenu.prototype._clickInsideElement = function (e) {
    var el = e.srcElement || e.target;

    if (el.classList.contains(this.contextClassName)) {
        return el;
    } else {
        while (el === el.parentNode) {
            if (el.classList && el.classList.contains(this.contextMenu.contextClassName)) {
                return el;
            }
        }
    }
    return false;
};

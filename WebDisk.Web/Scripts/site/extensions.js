$.fn.exists = function () {
    return this.length !== 0;
};

$.fn.extend({
    contextMenu: function (menuSelector, options) {
        $(this).each(function () {
            var contextMenu = new ContextMenu(this, menuSelector, options);
        });
    }
});

$.fn.hasAncestor = function (a) {
    return this.filter(function () {
        return !!$(this).closest(a).length;
    });
};


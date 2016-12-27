function backToPreviousDirectory() {
    var items = getHistoryItems(),
        lastItem = getSecoundLastDirectory(items);
    if (items.length <= 1) {
        displayToast("Nie można wrócić do poprzedniego katalogu", toastType.INFO);
        return;
    }
    removeNextElements(lastItem, items);
    lastItem.find("p").click();
}

function removeNextElements(current) {
    $(current).nextAll("div").remove();
}

function getHistoryItems() {
    return $("#history-items").find(".history-item");
}
function getSecoundLastDirectory(items) {

    //items.length return collection length not from 0.
    //to have numeration from 0 we have do decrease length by 1
    //to have previous element we have to decreate length by 1
    return items.eq(items.length - 2);
}

function addHistoryItem(link, name) {
    if (!link || !name) {
        displayToast("Nie można wrócić do poprzedniego katalogu", toastType.INFO);
        return;
    }
    //remove selected attributes from previous links
    removeSelectedLinks();

    //create link and
    //add link to container
    insertLink(addLink(link, name));
}

function addLink(link, name) {
    return '<div class="history-item">' +
            '<i class="material-icons pull-left">navigate_next</i>' +
            '<p class="link-selected" onclick="backToDirectory($(this),&#39; ' + link + ' &#39;)")">' + name + '</p>' +
            '</div>';

}

function removeSelectedLinks() {
    $("#history-items.link-selected").removeClass("link-selected");
}

function insertLink(html) {
    $("#history-items").append(html);
}

function backToDirectory(origin, link) {
    var name = $(origin).text();

    removeNextElements($(origin).parent());
    openDirectory(link, true, name);
}
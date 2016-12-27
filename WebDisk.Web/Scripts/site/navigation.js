function backToPreviousDirectory() {
    var lastItem = getLastDirectory();

    lastItem.click();
}

function getHistoryItems() {
    return $("#history-items").find(".history-item");
}
function getLastDirectory() {
    var items = getHistoryItems();
    if (items.length <= 1) {
        displayToast("Nie można wrócić do poprzedniego katalogu", toastType.INFO);
    }
    return items.last();
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
    return  '<div class="history-item">'+ 
            '<i class="material-icons pull-left">navigate_next</i>'+
            '<p class="link-selected" onclick="openDirectory(&#39; ' + link + ' &#39;)",true,&#39;' + name + '&#39;)">' + name + '</p>'+
            '</div>';
        
}

function removeSelectedLinks() {
    $("#history-items.link-selected").removeClass("link-selected");
}

function insertLink(html) {
    $("#history-items").append(html);
}
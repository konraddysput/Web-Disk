var fieldInformations = {
    defaultFieldColor: "default-color"
};

$(function () {
    removeSelectedMark();
});

function removeSelectedMark() {

    //$(document).on("click", function (e) {
    //    console.log("in");
    //    var button = e.which || e.button;
    //    if (button === 1) {
    //        removePreviousSelectedFiles();
    //    }
    //});
    window.onkeyup = function (e) {
        if (e.keyCode === 27) {
            removePreviousSelectedFiles();
        }
    };
}

function openField(id, type, object) {
    console.log("in");
    selectFile(object);
    switch (type) {
        case 1:
            openDirectory(id);
            break;
        case 0:
            openFile(id);
            break;
        default:
            displayToast("Plik jest nie możliwym do podglądu");
            break;
    }
}

function openDirectory(id) {

}

function openFile(id) {

}

function selectFile(object) {
    removePreviousSelectedFiles();
    $(object).addClass(fieldInformations.defaultFieldColor);
}

function removePreviousSelectedFiles() {
    $("." + fieldInformations.defaultFieldColor).removeClass(fieldInformations.defaultFieldColor);
}
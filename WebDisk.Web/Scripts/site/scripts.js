const toastType = {
    INFO: "info",
    ERROR: "error",
    WARNING: "warning",
    SUCCESS: "success"
};

$(function () {
    initMaterialDesign();
    initAjaxPreloading();
    initBackgroundClick();
    //handle right click on field on starting window
    initFieldClick();
});

function initMaterialDesign() {
    $.material.init();
}

function initAjaxPreloading() {
    $(document).ajaxStart(function () {
        loading();
    });
    $(document).ajaxComplete(function (event, request, settings) {
        endLoading();
    });
}


function displayToast(message, type) {
    switch (type) {
        case toastType.ERROR:
            toastr.error(message);
            break;
        case toastType.WARNING:
            toastr.warning(message);
            break;
        case toastType.SUCCESS:
            toastr.success(message);
            break;
        case toastType.INFO:
        default:
            toastr.info(message);
            break;

    }
}

function loading() {
    $('#preloading-Modal').modal('show');
}

function endLoading() {
    $('#preloading-Modal').modal('hide');
    initFieldClick();
}

function openFieldNameChangeModal() {
    var currentFieldId = getCurrentFile().attr("data-id");
    $("#new-input-name-id").val(currentFieldId);
    $("#field-name-change").modal("show");

}


function initFieldClick() {
    $(".field").contextMenu({
        correctClick: selectClickedField
    });
}

function initBackgroundClick() {
    $("#fields:not(.field)").contextMenu({
        menu: "#app-menu",
        clickCondition: backgroundClickCondition,
        onOpen: function () {
            console.log("opening");
        }
    });
}

function getCurrentDirectoryId() {
    return $("#directoryId").val();
}

function getFileType(file) {
    return $(file).attr("data-type");
}

function openNav() {
    $("#field-display-nav").css("width", "70%");
    $("#main-content").css("margin-left", "70%");
    document.body.style.backgroundColor = "rgba(0,0,0,0.4)";
}

function closeNav() {
    $("#field-display-nav").css("width", "0");
    $("#main-content").css("margin-left", "");
    document.body.style.backgroundColor = "white";
}
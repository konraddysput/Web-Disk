const toastType = {
    INFO: "info",
    ERROR: "error",
    WARNING: "warning",
    SUCCESS: "success"
};

$(function () {
    initMaterialDesign();
});

function initMaterialDesign() {
    $.material.init();
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
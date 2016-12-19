$(function () {
    initMaterialDesign();
});

function initMaterialDesign() {
    $.material.init();

    console.log("init material design");
}


function displayToast(message) {
    toastr["info"](message);
}
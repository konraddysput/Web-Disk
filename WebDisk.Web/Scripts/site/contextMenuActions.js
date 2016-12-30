function getCurrentFile() {
    return $("." + fieldInformations.defaultFieldColor).first();
}

function openFile() {
    getCurrentFile()
         .dblclick();
}

function openFileDetails() {
    var currentFieldId = getCurrentFile().attr("data-id");
    console.log(currentFieldId);
    $.ajax({
        type: "GET",
        url: "Field/Details/" + currentFieldId,
        success: function (data) {
            debugger;
            $("#field-property .modal-body").html(data);
            openDetailsModal();
        }
        //error: function () {
        //    displayToast("Nie udało się wczytać właściwości", toastType.ERROR);
        //}
    });
}

function openDetailsModal() {
    $('#field-property').modal('show');
}
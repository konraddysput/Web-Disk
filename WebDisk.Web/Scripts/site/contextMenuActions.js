var fieldOperationInformation = {
    currentFieldId: undefined
};

function backgroundClickCondition(e) {
    return !$(e.currentTarget).hasAncestor(".context-field").exists();
}

function selectClickedField(e) {
    selectFile(e.currentTarget);
}

function getCurrentFile() {
    return $("." + fieldInformations.defaultFieldColor).first();
}

function openFile() {
    getCurrentFile()
         .dblclick();
}

function openFileDetails() {
    var currentFieldId = getCurrentFile().attr("data-id");
    $.ajax({
        type: "GET",
        url: "Field/Details/" + currentFieldId,
        success: function (data) {
            $("#field-property .modal-body").html(data);
            openDetailsModal();
        },
        error: function () {
            displayToast("Nie udało się wczytać właściwości", toastType.ERROR);
        }
    });
}

function openDetailsModal() {
    $('#field-property').modal('show');
}

function copyFile() {
    fieldOperationInformation.currentFieldId = getCurrentFile().attr("data-id");
    displayToast("Skopiowano plik do schowka", toastType.INFO);
}

function canPaste() {
    return fieldOperationInformation.currentFieldId !== undefined;
}

function pasteFile() {
    if (!canPaste) {
        displayToast("W schowku brakuje plików", toastType.WARNING);
    }
    var currentDirectoryId = 
    $.ajax({

    })
}
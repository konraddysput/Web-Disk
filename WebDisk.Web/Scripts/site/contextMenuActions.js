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

function openDirectoryDetails() {
    var currentDirectoryId = getCurrentDirectoryId();
    loadFielDescription(currentDirectoryId);
}

function openFileDetails() {
    var currentFieldId = getCurrentFile().attr("data-id");
    loadFielDescription(currentFieldId);
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

function tryPasteField() {
    if (!canPaste) {
        displayToast("W schowku brakuje plików", toastType.WARNING);
    }
    pasteField(fieldOperationInformation.currentFieldId);
    fieldOperationInformation.currentFieldId = undefined;
}

function deleteFile() {
    var fileToDelete = getCurrentFile().attr("data-id");
    var currentDirectoryId = getCurrentDirectoryId();
    deleteField(currentDirectoryId,fileToDelete);

}
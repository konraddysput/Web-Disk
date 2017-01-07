var fieldOperationInformation = {
    currentFieldId: undefined,
    currentCutField: undefined
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
function saveCutFile() {
    clearOperationFields();
    fieldOperationInformation.currentCutField = getCurrentFile().attr("data-id");
    displayToast("Skopiowano plik do schowka", toastType.INFO);
}
function saveCopyFile() {
    clearOperationFields();
    fieldOperationInformation.currentFieldId = getCurrentFile().attr("data-id");
    displayToast("Skopiowano plik do schowka", toastType.INFO);
}

function canPaste() {
    return fieldOperationInformation.currentFieldId !== undefined || fieldOperationInformation.currentCutField !== undefined;
}

function tryPasteField() {
    if (!canPaste()) {
        displayToast("W schowku brakuje plików", toastType.WARNING);
    }
    if (fieldOperationInformation.currentFieldId !== undefined) {
        pasteField(fieldOperationInformation.currentFieldId);

    }
    else {
        cutField(fieldOperationInformation.currentCutField);

    }
    clearOperationFields();
}
function deleteFile() {
    var fileToDelete = getCurrentFile().attr("data-id");
    var currentDirectoryId = getCurrentDirectoryId();
    deleteField(currentDirectoryId, fileToDelete);

}

function clearOperationFields() {
    fieldOperationInformation.currentFieldId = undefined;
    fieldOperationInformation.currentCutField = undefined;
}

function downloadFile() {
    var currentField = getCurrentFile();
    //if (getFileType(currentField) !== "File") {
    //    displayToast("Nie można pobrać tego typu pliku", toastType.WARNING);
    //    return;
    //}

    var currentFieldId = $(currentField).attr("data-id");
    downloadField(currentFieldId);
}
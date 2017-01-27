var fieldInformations = {
    defaultFieldColor: "default-color"
};

$(function () {
    removeSelectedMark();
});

function removeSelectedMark() {
    window.onkeyup = function (e) {
        if (e.keyCode === 27) {
            removePreviousSelectedFiles();
        }
    };
}

function openField(id, type, object) {
    selectFile(object);
    debugger;
    switch (type) {
        case 'Directory':
            var nextDirectoryName = $(object).next().text();
            LoadNextDirectory(id, nextDirectoryName);
            break;
        case 'File':
            openFileInNav(id);
            break;
        default:
            displayToast("Plik jest nie możliwym do podglądu", toastType.INFO);
            break;
    }
}
function LoadNextDirectory(id, nextDirectoryName) {
    var url = "Directory/" + id;
    addHistoryItem(url, nextDirectoryName);
    openDirectory(url);
}

function openDirectory(url, inform, directoryName) {
    $.ajax({
        type: "GET",
        url: url,
        success: function (data) {

            $("#fields").html(data);
            initFieldClick();
            if (inform !== undefined && inform) {
                displayToast("Przeszedłeś do folderu:" + '\n' + directoryName);
            }
        },
        error: function () {
            displayToast("Nie można zaladować katalogu", toastType.ERROR);
        }

    });
}

function openFileInNav(id) {
    console.log("Field/Display/" + id);
    $("#frame").attr("src", "Field/Display/" + id);
    openNav();
}

function selectFile(object) {
    removePreviousSelectedFiles();
    $(object).addClass(fieldInformations.defaultFieldColor);
}

function removePreviousSelectedFiles() {
    $("." + fieldInformations.defaultFieldColor).removeClass(fieldInformations.defaultFieldColor);
}

function startUpload() {
    $("#file").click();
}

function initCreateDirectoryModal() {
    $('#modal').modal('show');
}

function uploadFiles() {
    var formData = new FormData(),
        directoryId = getCurrentDirectoryId();

    if (!directoryId) {
        displayToast("Napotkano na problemy. Proszę o odświeżenie strony", toastType.ERROR);
    }

    for (var i = 0; i < document.getElementById("file").files.length; i++) {
        console.log(document.getElementById("file").files[i]);
        formData.append("files[" + i + "]", document.getElementById("file").files[i]);
    }
    $.ajax({
        url: 'Field/Update/' + directoryId,
        type: 'POST',
        data: formData,       
        success: function (data) {
            displayToast("Dodano pliki", toastType.SUCCESS);
            //refresh current window
            $("#fields").html(data);

        },
        error: function (data) {
            displayToast("Napotkano problemy, spróbuj ponownie", toastType.ERROR);
        },
        contentType: false,
        cache: false,
        processData: false
    });
}

function uploadStatus(e) {
    if (e.lengthComputable) {
        var percentage = Math.floor((e.loaded / e.total) * 100);
    }
}


function refreshWindow(directoryId) {
    if (directoryId === undefined)
    {
        directoryId = getCurrentDirectoryId();
    }
    $.ajax({
        type: "GET",
        url: "Directory/" + directoryId,
        success: function (data) {
            $("#fields").html(data);
        },
        error: function () {
            displayToast("Napotkano problemy, spróbuj ponownie", toastType.ERROR);
        }
    });
}


function createDirectory() {
    var directoryName = $("#directory-name").val(),
        rootId = getCurrentDirectoryId();

    $.ajax({
        type: "POST",
        url: "Directory/Create",
        data: JSON.stringify({
            "directoryName": directoryName,
            "rootId": rootId
        }),
        contentType: "application/json",
        success: function (data) {

            //refresh current window
            $("#fields").html(data);
            //close modal
            $("#directory-name").val('');
            $("#close-new-directory-modal").click();
            displayToast("Dodano katalog", toastType.INFO);

        },
        error: function (a, b, c) {
            displayToast("Napotkano problemy, spróbuj ponownie", toastType.ERROR);
        }
    });
}

function pasteField(fieldId) {
    var currentDirectoryId = getCurrentDirectoryId();
    moveField("Field/Copy/", currentDirectoryId, fieldId);
}

function cutField(fieldId) {
    var currentDirectoryId = getCurrentDirectoryId();
    moveField("Field/Cut/", currentDirectoryId, fieldId);
}

function moveField(domainUrl, currentDirectoryId, fieldId) {
    $.ajax({
        type: "POST",
        url: domainUrl + "/" + currentDirectoryId + "/" + fieldId,
        success: function () {
            displayToast("Pomyślnie przeniesiono dane", toastType.INFO);
            refreshWindow(currentDirectoryId);
        },
        error: function () {
            displayToast("Napotkano problemy, spróbuj ponownie", toastType.ERROR);
        }
    });
}

function loadFielDescription(currentFieldId) {
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


function deleteField(currentDirectoryId, currentFieldId) {
    $.ajax({
        type: "GET",
        url: "Field/Delete/" + currentFieldId,
        success: function () {
            refreshWindow(currentDirectoryId);
        },
        error: function () {
            displayToast("Nie udało się usunąć pliku", toastType.ERROR);
        }
    });
}

function downloadField(currentFieldId) {
    window.location = "Field/Download/" + currentFieldId;
}


function updateName() {
    var currentFieldId = $("#new-input-name-id").val();
    var newFileName = $("#new-field-name").val();
    if (!newFileName) {
        displayToast("Należy podać nową nazwę", toastType.ERROR);
        return;
    }

    $.ajax({
        type: "GET",
        url: "Field/Update/" + currentFieldId + "/" + newFileName,
        success: function () {
            displayToast("Zmieniono nazwę", toastType.SUCCESS);
            $("#field-name-change").modal("hide");
            refreshWindow(getCurrentDirectoryId());
        },
        error: function () {
            displayToast("Nie udało się zmienić nazwy pliku", toastType.ERROR);
        }
    });
}


function filterFields() {
    var filter = $("#field-filter").val();
    if (!filter) {
        //if filter is empty show every field
        $(".context-field").show();
    }
    //hide every field that does not contain filter character
    $(".context-field p:not(:contains(" + filter + "))").parent().hide();
}
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

function startUpload() {
    $("#file").click();
}

function uploadFiles() {
    var formData = new FormData(),
        directoryId = $("#directoryId").val();

    if (!directoryId) {
        displayToast("Napotkano na problemy. Proszę o odświeżenie strony");
    }

    for (var i = 0 ; i < document.getElementById("file").files.length; i++) {
        console.log(document.getElementById("file").files[i]);
        formData.append("files[" + i + "]", document.getElementById("file").files[i]);
    }
    $.ajax({
        url: 'Field/Update/' + directoryId,
        type: 'POST',
        data: formData,
        xhr: function () {
            var myXhr = $.ajaxSettings.xhr();
            if (myXhr.upload) { // Check if upload property exists
                // For handling the progress of the upload
                myXhr.upload.addEventListener('progress', uploadStatus, false);

            }
            return myXhr;
        },
        success: function (data) {
            displayToast("Dodano pliki", "success");
            //refresh current window
            $("#fields").html(data);

        },
        error: function (data) {
            console.log(data);
        },
        contentType: false,
        cache: false,
        processData: false
    });
}

function uploadStatus(e) {
    if (e.lengthComputable) {
        var percentage = Math.floor((e.loaded / e.total) * 100);
        //update progressbar percent complete
        //statustxt1.html(percentage + '%');
        console.log(percentage);
        console.log("Value = " + e.loaded + " :: Max =" + e.total);
    }
}


function createDirectory() {
    var directoryName = $("#directory-name").val(),
        rootId = $("#directoryId").val();

    console.log("in directory");
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
            
        },
        error: function (a, b, c) {
            displayToast("Napotkano problemy, spróbuj ponownie", "error");
        }
    })
}
function getCurrentFile() {
    return $("." + fieldInformations.defaultFieldColor).first();
}

function openFile() {
    var current = getCurrentFile();
    console.log("open file");
    current.dblclick();
}


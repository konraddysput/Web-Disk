function openField(id, type) {
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
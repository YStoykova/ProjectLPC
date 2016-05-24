$(document).ready(function () {
    AttachEvents();
});

function AttachEvents() {
    $(".file-selector").click(function () {
        openFile($(this));
    });

    $(".history-selector").click(function () {      
        loadHistory($(this));
    });

    $(".delete-selector").click(function () {
        deleteFile($(this));
    });
}
function openFile(element)
{
    $.ajax({
        method: "POST",
        url: Root + "File/Export",
        data: { fileId: $(element).data("fileid")},
        cache: false,
        async: true,
        success: function (result) {
            window.location = '/File/Download?fileId=' + result;
        }
    });
}

function loadHistory(element) {

    $.ajax({
        method: "GET",
        url: Root + "File/History",
        data: { fileId: $(element).data("fileid")},
        cache: false,
        async: true,
        success: function (result) {
            $('#historyPartialView').html(result);
        }
    });
}

function deleteFile(element) {

    $.ajax({
        method: "POST",
        url: Root + "File/Delete",
        data: { fileId: $(element).data("fileid") },
        cache: false,
        async: true,
        success: function () {
        }
    });
}
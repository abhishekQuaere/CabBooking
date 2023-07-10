 Dropzone.autoDiscover = false;

$(document).ready(function () {
    debugger;
        $("#id_dropzone").dropzone({
            maxFiles: 2000,
            url: "../Handler/ajax_file_upload_handler/",
            success: function (file, response) {
                console.log(response);
            }
        });
   })
$(document).ready(function () {
    Dropzone.autoDiscover = false;
    resetDropzone();
});

/////////////////////////////////////////////////Login Images Carousel
//var maxImageWidth = 800,
//    maxImageHeight = 470;
function resetDropzone() {
    $('#dZUpload').remove();
    console.log('reset');

    var $form = makeElement('div', {
        id: 'dZUpload',
        class: 'dropzone'
    });

    $('#samplediv').append($form);

    var $form1 = makeElement('div', {
        class: 'dz-default dz-message',
        id: 'divMessage'
    });

    $('#dZUpload').append($form1);

    var h2Tag = "<h2>Drop Files or Click to Browse. </h2><i>(Max file size: 10 MegaBytes)</i>";

    $('#divMessage').append(h2Tag);

    $("#dZUpload").dropzone({
        url: "System/CarouselHandler.ashx",
        maxFiles: 10,
        maxfilesize: 12,
        dictDefaultMessage: "Message",
        acceptedFiles: "image/jpg,image/jpeg,image/png,image/gif",
        addRemoveLinks: true,
        success: function (file, response) {
            if (response == 'Only jpg, png , gif, .jpeg are allowed.!' || response == 'File size should not exceed 10 MB.!') {
                swal("Warning", response, "warning");
            }
            else {
                var imgName = response;
                file.previewElement.classList.add("dz-success");
                console.log("Successfully uploaded :" + imgName);
                $.ajax({
                    type: "POST",
                    url: "System/Miscellaneous.aspx/GetLoginImages",
                    data: " {} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#BodyHolder_LoginImages').hide();
                        $('#BodyHolder_LoginImages').html(msg.d);
                        $('#BodyHolder_LoginImages').fadeIn(200);
                    }
                });
            }
        },
        //init: function () {
        //    this.on("thumbnail", function (file) {
        //        if (file.width > maxImageWidth || file.height > maxImageHeight) {
        //            file.rejectDimensions();
        //            swal("Warning", "Please make sure the image width is not larger than 800px and height is not larger than 470px.")
        //        }
        //        else {
        //            if (file.size < maxImageWidth * maxImageHeight * 5/*2MB*/) {
        //                file.acceptDimensions();
        //            }
        //        }
        //    })
        //},
        //accept: function (file, done) {
        //    file.rejectDimensions = function () {
        //        //done("Please make sure the image width and height are not larger than 2500px.");
        //    };
        //    file.acceptDimensions = done;
        //},
        error: function (file, response) {
            file.previewElement.classList.add("dz-error");
        },
        removedfile: function (file) {
            var _ref;
            return (_ref = file.previewElement) != null ? _ref.parentNode.removeChild(file.previewElement) : void 0;
        }
    });
}

function makeElement(element, options) {
    var $formField = document.createElement(element);
    $.each(options, function (key, value) {
        if (key === 'innerHTML') {
            $formField.innerHTML = value;
        }
        else {
            $formField.setAttribute(key, value);
        }
    });
    return $formField;
}

function DeleteImageClick(filename) {

    $.ajax({
        type: 'POST',
        url: "System/Miscellaneous.aspx/DeleteFile",
        data: "{ filename: '" + filename + "' }",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            if (msg.d == "file deleted.") {
                $.ajax({
                    type: "POST",
                    url: "System/Miscellaneous.aspx/GetLoginImages",
                    data: " {} ",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#BodyHolder_LoginImages').hide();
                        $('#BodyHolder_LoginImages').html(msg.d);
                        $('#BodyHolder_LoginImages').fadeIn(200);
                    }
                });
            }
            else {
                swal("Error", "File not found.", "error");
            }
        }
    });
}

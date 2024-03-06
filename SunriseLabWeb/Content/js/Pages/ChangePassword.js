var ErrorMsg = [];
var GetError = function () {
    ErrorMsg = [];
    
    if ($("#txt_Password").val().trim() == "") {
        ErrorMsg.push({
            'Error': "Please Enter Password.",
        });
    }
    else {
        var newlength = $("#txt_Password").val().trim().length;
        if (newlength < 6) {
            ErrorMsg.push({
                'Error': "Please Enter Minimum 6 Character Password.",
            });
        }
    }
    if ($("#txt_CPassword").val().trim() == "") {
        ErrorMsg.push({
            'Error': "Please Enter Confirm Password.",
        });
    }
    else {
        var newlength = $("#txt_CPassword").val().trim().length;
        if (newlength < 6) {
            ErrorMsg.push({
                'Error': "Please Enter Minimum 6 Character Confirm Password.",
            });
        }
    }
    if ($("#txt_Password").val().trim() != "" && $("#txt_CPassword").val().trim() != "") {
        if ($("#txt_Password").val().trim() === $("#txt_CPassword").val().trim()) {

        } else {
            ErrorMsg.push({
                'Error': "Please Enter Confirm Password Same as Password.",
            }); 
        }
    }

    return ErrorMsg;
}
function ChangePassword() {
    ErrorMsg = GetError();

    if (ErrorMsg.length > 0) {
        $("#divError").empty();
        ErrorMsg.forEach(function (item) {
            $("#divError").append('<li>' + item.Error + '</li>');
        });
        $("#ErrorModel").modal("show");
    }
    else {
        loaderShow();
        var obj = {};
        obj.Password = $("#txt_Password").val().trim();

        $.ajax({
            url: "/User/_ChangePassword",
            async: false,
            type: "POST",
            data: { req: obj },
            success: function (data, textStatus, jqXHR) {
                loaderHide();
                if (data.Status == "1") {
                    toastr.remove();
                    toastr.success(data.Message);
                    $("#txt_Password").val("");
                    $("#txt_CPassword").val("");
                }
                else {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.remove();
                    toastr.error(data.Message);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                loaderHide();
            }
        });
        
    }
}
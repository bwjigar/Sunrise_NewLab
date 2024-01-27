var SupplierColumn = [];
var SupplierColumn_ddl = "";
var filetype = "";
$(document).ready(function () {
    $('#file_upload').on('change', function (event) {
        const selectedFile = event.target.files[0];
        const fileExtension = selectedFile.name.split('.').pop().toLowerCase();
        $(".SheetName").hide();
        filetype = fileExtension;
        if (!(fileExtension == "xlsx" || fileExtension == "xls" || fileExtension == "csv")) {
            $('#file_upload').val('');
            toastr.error("Allowed only .XLSX, .XLS, .CSV file format.");
        }
        else if (fileExtension == "xlsx" || fileExtension == "xls") {
            Get_SheetName_From_File();
        }
    });
    Master_Get();
    $("#li_User_SupplierStockUpload").addClass("menuActive");
});
function Get_SheetName_From_File() {
    debugger
    if ($("#DdlSupplierName").val() != "") {
        debugger
        const fileInput = $('#file_upload')[0];
        if (fileInput.files.length > 0) {
            loaderShow();
            debugger
            setTimeout(function () {
                debugger
                const formData = new FormData();
                formData.append('SupplierId', $("#DdlSupplierName").val());
                formData.append('File', fileInput.files[0]);

                debugger
                $.ajax({
                    type: "POST",
                    url: "/User/Get_SheetName_From_File",
                    contentType: false,
                    processData: false,
                    data: formData,
                    async: false,
                    success: function (data) {
                        loaderHide();
                        debugger
                        if (data.Status == "1" && data.Message == "SUCCESS" && data.Data.length > 0) {
                            debugger
                            $(".SheetName").show();
                            $("#DdlSheetName").html("<option value=''>Select</option>");
                            if (data.Data.length > 1) {
                                $("#DdlSheetName").append("<option value='_ALL_SHEET_'>ALL</option>");
                            }
                            _(data.Data).each(function (obj, i) {
                                $("#DdlSheetName").append("<option value=\"" + obj.SheetName + "\">" + obj.SheetName + "</option>");
                            });
                        }
                        else {
                            debugger
                            toastr.error(data.Message);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        loaderHide();
                    }
                });

            }, 50);
        }
        else {
            toastr.error("Please Select File (.XLSX, .XLS, .CSV)");
        }
    }
    else {
        toastr.error("Please Select Supplier Name");
    }
}
function Master_Get() {
    loaderShow();
    $("#DdlSupplierName").html("<option value=''>Select</option>");
    var obj = {};
    obj.OrderBy = "SupplierName asc";
    obj.FileStockUpload = true;
    $.ajax({
        url: "/User/Get_SupplierMaster",
        async: false,
        type: "POST",
        data: { req: obj },
        success: function (data, textStatus, jqXHR) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            if (data != null && data.Data.length > 0) {
                for (var k in data.Data) {
                    $("#DdlSupplierName").append("<option value=" + data.Data[k].Id + ">" + data.Data[k].SupplierName + "</option>");
                }
            }
            loaderHide();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
}
function onchange_SupplierName() {
    $('#file_upload').val('');
    filetype = "";
    $(".SheetName").hide();
}
function Stock_Upload() {
    debugger
    if ($("#DdlSupplierName").val() != "") {
        debugger
        const fileInput = $('#file_upload')[0];
        if (fileInput.files.length > 0) {
            if (((filetype == "xlsx" || filetype == "xls") && $("#DdlSheetName").val() != "") || (filetype == "csv")) {
                //loaderShow_stk_upload();
                loaderShow();
                debugger
                setTimeout(function () {
                    debugger
                    const formData = new FormData();
                    formData.append('SupplierId', $("#DdlSupplierName").val());
                    formData.append('SupplierName', $("#DdlSupplierName option:selected").text());
                    formData.append('SheetName', $("#DdlSheetName").val() + "$");
                    formData.append('File', fileInput.files[0]);
                    formData.append('UserId', $("#hdn_UserId").val());

                    debugger
                    $.ajax({
                        type: "POST",
                        //url: "/User/AddUpdate_SupplierStock_FromFile",
                        url: "/User/Thread_AddUpdate_SupplierStock_FromFile",
                        contentType: false,
                        processData: false,
                        data: formData,
                        async: false,
                        success: function (data) {
                            //loaderHide_stk_upload();
                            loaderHide();
                            debugger
                            var myArray = data.split("_");
                            if (myArray[0] == "1") {
                                toastr.success(myArray[1]);
                            }
                            else {
                                toastr.error(myArray[1]);
                            }
                            //if (data.Status == "1") {
                            //    debugger
                            //    toastr.success(data.Message);
                            //}
                            //else {
                            //    debugger
                            //    toastr.error(data.Message);
                            //}
                        },
                        error: function (jqXHR, textStatus, errorThrown) {
                            //loaderHide_stk_upload();
                            loaderHide();
                        }
                    });

                }, 50);
            }
            else {
                toastr.error("Please Select Sheet Name");
            }
        }
        else {
            toastr.error("Please Select File (.XLSX, .XLS, .CSV)");
        }
    }
    else {
        toastr.error("Please Select Supplier Name");
    }
}

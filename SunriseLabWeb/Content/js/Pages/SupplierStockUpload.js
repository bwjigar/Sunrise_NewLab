var SupplierColumn = [];
var SupplierColumn_ddl = "";

$(document).ready(function () {
    $('#file_upload').on('change', function (event) {
        const selectedFile = event.target.files[0];
        const fileExtension = selectedFile.name.split('.').pop().toLowerCase();
        if (!(fileExtension == "xlsx" || fileExtension == "xls" || fileExtension == "csv")) {
            $('#file_upload').val('');
            toastr.error("Allowed only .XLSX, .XLS, .CSV file format.");
        }
    });
    Master_Get();
});

function Master_Get() {
    loaderShow();
    $("#DdlSupplierName").html("<option value=''>Select</option>");
    var obj = {};
    obj.OrderBy = "SupplierName asc";
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
}
function Stock_Upload() {
    debugger
    if ($("#DdlSupplierName").val() != "") {
        debugger
        const fileInput = $('#file_upload')[0];
        if (fileInput.files.length > 0) {
            loaderShow_stk_upload();
            debugger
            setTimeout(function () {
                debugger
                const formData = new FormData();
                formData.append('SupplierId', $("#DdlSupplierName").val());
                formData.append('SupplierName', $("#DdlSupplierName option:selected").text());
                formData.append('File', fileInput.files[0]);

                debugger
                $.ajax({
                    type: "POST",
                    url: "/User/AddUpdate_SupplierStock_FromFile",
                    contentType: false,
                    processData: false,
                    data: formData,
                    async: false,
                    success: function (data) {
                        loaderHide_stk_upload();
                        if (data.Status == "1") {
                            debugger
                            toastr.success(data.Message);
                        }
                        else {
                            debugger
                            toastr.error(data.Message);
                        }
                    },
                    error: function (jqXHR, textStatus, errorThrown) {
                        loaderHide_stk_upload();
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

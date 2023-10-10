var Column_Mas_Select = [];
var Column_Mas_ddl = "";
var SupplierColumn = [];
var SupplierColumn_ddl = "";

$(document).ready(function () {
    Master_Get();
});

function Master_Get() {
    $("#DdlSupplierName").html("<option value=''>Select</option>");
    var obj = {};
    obj.OrderBy = "SupplierName asc";
    obj.WebAPIFTPStockUpload = true;
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
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });

    $.ajax({
        url: "/User/Get_ColumnMaster",
        async: false,
        type: "POST",
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1" && data.Data != null) {
                Column_Mas_Select = data.Data;

                Column_Mas_ddl = "<option value=''>Select</option>";
                _(Column_Mas_Select).each(function (obj, i) {
                    Column_Mas_ddl += "<option value=\"" + obj.Col_Id + "\">" + obj.SupplierColumn + "</option>";
                });
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}

function Get_SupplierColumnSetting_FromAPI() {
    debugger
    $("#Save_btn").hide();
    $("#Delete_btn").hide();
    $("#TB_ColSetting").hide();
    $('#myTableBody').html("");

    if ($("#DdlSupplierName").val() != "") {
        debugger
        loaderShow();
        setTimeout(function () {
            debugger
            var obj = {};
            obj.SupplierId = $("#DdlSupplierName").val();

            $.ajax({
                url: "/User/Get_SupplierColumnSetting_FromAPI",
                async: false,
                type: "POST",
                data: { req: obj },
                success: function (data, textStatus, jqXHR) {
                    loaderHide();
                    debugger
                    if (data.Status == "1" && data.Message == "SUCCESS" && data.Data.length > 0) {
                        debugger
                        SupplierColumn = data.Data;
                        SupplierColumn_ddl = "<option value=''>Select</option>";
                        _(SupplierColumn).each(function (obj, i) {
                            SupplierColumn_ddl += "<option value=\"" + obj.SupplierColumn + "\">" + obj.SupplierColumn + "</option>";
                        });

                        $.ajax({
                            url: "/User/Get_SupplierColumnSetting",
                            async: false,
                            type: "POST",
                            data: { req: obj },
                            success: function (data, textStatus, jqXHR) {
                                debugger
                                loaderHide();
                                if (data.Status == "1" && data.Message == "SUCCESS" && data.Data.length > 0) {
                                    debugger
                                    $("#Save_btn").show();
                                    $("#TB_ColSetting").show();
                                    $('#myTableBody').html("");
                                    debugger
                                    var exists = false;
                                    _(data.Data).each(function (obj, i) {
                                        if (obj.SupplierColumn != null && exists == false) {
                                            exists = true;
                                        }
                                        SupplierColumn_ddl = "<option value=''>Select</option>";
                                        _(SupplierColumn).each(function (__obj, i) {
                                            SupplierColumn_ddl += "<option value=\"" + __obj.SupplierColumn + "\"" + (obj.SupplierColumn == __obj.SupplierColumn ? 'Selected' : '') + ">" + __obj.SupplierColumn + "</option>";
                                        });

                                        $('#myTableBody').append('<tr><td>' + (parseInt(i) + parseInt(1)) + '</td><td><input type="hidden" class="SunriseColumn" value="' + obj.Col_Id + '" />' + obj.Column_Name +
                                            '</td><td><center><select style="margin-top: -9px;margin-bottom: -9px;" onchange="ddlOnChange(\'' + obj.Col_Id + '\');" id="ddl_' + obj.Col_Id + '" class="col-md-7 form-control select2 SupplierColumn">' + SupplierColumn_ddl +
                                            '</select></center></td></tr>');
                                    });
                                    $("#Save_btn").html("<i class='fa fa-save' aria-hidden='true'></i>&nbsp;" + (exists == true ? "Update" : "Save"));
                                    if (exists == true) {
                                        $("#Delete_btn").show();
                                    }
                                    
                                    contentHeight();
                                }
                                //else if (data.Status == "1" && data.Message == "No records found.") {
                                //    debugger
                                //    $("#Save_btn").html("<i class='fa fa-save' aria-hidden='true'></i>&nbsp;Save");
                                //    $("#Save_btn").show();
                                //    $("#TB_ColSetting").show();
                                //    $('#myTableBody').html("");

                                //    debugger
                                //    _(Column_Mas_Select).each(function (obj, i) {
                                //        $('#myTableBody').append('<tr><td>' + (parseInt(i) + parseInt(1)) + '</td><td><input type="hidden" class="SunriseColumn" value="' + obj.Col_Id + '" />' + obj.Display_Name +
                                //            '</td><td><center><select onchange="ddlOnChange(\'' + obj.Col_Id + '\');" id="ddl_' + obj.Col_Id + '" class="col-md-6 form-control select2 SupplierColumn">' + SupplierColumn_ddl +
                                //            '</select></center></td></tr>');
                                //    });
                                //    contentHeight();
                                //}
                            },
                            error: function (jqXHR, textStatus, errorThrown) {
                                loaderHide();
                            }
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
}
function ddlOnChange(id) {
    //if ($("#ddl_" + id).val() != "") {
    //    var DisOrder = 0;
    //    $("#mytable #myTableBody tr").each(function () {
    //        DisOrder = parseInt(DisOrder) + 1;
    //        if ($(this).find('.CustomColumn').val() != "") {
    //            if (DisOrder != parseInt(id) && $("#ddl_" + id).val() == $(this).find('.CustomColumn').val()) {
    //                toastr.error($("#ddl_" + id).children(":selected").text() + " Custom Column Name alredy selected.");
    //                $("#ddl_" + id).val("");
    //            }
    //        }
    //    });
    //}
}
function SaveData() {
    loaderShow();

    setTimeout(function () {
        debugger
        var List2 = [];
        $("#mytable #myTableBody tr").each(function () {
            List2.push({
                SupplierId: $("#DdlSupplierName").val(),
                SupplierColumn: $(this).find('.SupplierColumn').val(),
                ColumnId: $(this).find('.SunriseColumn').val()
            });
        });
        debugger
        var obj = {};
        obj.col = List2;
        $.ajax({
            url: "/User/AddUpdate_SupplierColumnSetting",
            async: false,
            type: "POST",
            dataType: "json",
            data: JSON.stringify({ req: obj }),
            contentType: "application/json; charset=utf-8",
            success: function (data, textStatus, jqXHR) {
                debugger
                loaderHide();
                if (data.Status == "1") {
                    toastr.success(data.Message);
                    //Get_SupplierColumnSetting();
                    $("#Save_btn").html("<i class='fa fa-save' aria-hidden='true'></i>&nbsp;Update");
                    $("#Delete_btn").show();
                }
                else {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                loaderHide();
            }
        });

    }, 20);
}
var ClearRemoveModel = function () {
    $("#DeleteModal").modal("hide");
}
var Delete = function () {
    $("#DeleteModal").modal("show");
    $("#DeleteModal .modal-body li").html("Are You Sure You Want To Delete Column Setting of " + $("#DdlSupplierName option:selected").text() + " Supplier ?");
}
function DeleteData() {
    if ($("#DdlSupplierName").val() != "") {
        var obj = {};
        obj.SupplierId = $("#DdlSupplierName").val();
        loaderShow();

        $.ajax({
            url: '/User/Delete_SupplierColumnSetting',
            type: "POST",
            data: { req: obj },
            success: function (data) {
                loaderHide();
                if (data.Status == "1") {
                    toastr.success(data.Message);
                    $("#DeleteModal").modal("hide");
                    $("#Save_btn").hide();
                    $("#Delete_btn").hide();
                    $("#TB_ColSetting").hide();
                    $('#myTableBody').html("");
                }
                else {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                loaderHide();
            }
        });
    }
}

function contentHeight() {
    var winH = $(window).height(),
        head = $(".apicol-head").height(),
        contentHei = winH - head - 280;
    $("#mytable").css("height", contentHei);
}
$(window).resize(function () {
    contentHeight();
});
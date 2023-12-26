var Supplierdata = [], Shapedata = [], Pointerdata = [];
var row = $('#tblbody').find('tr').length;
var row_cnt = 0;

$(document).ready(function (e) {
    Master_Get();
    $("#tblbody").on('click', '.RemoveCate', function () {
        $(this).closest('tr').remove();
        if (parseInt($("#tbl #tblbody").find('tr').length) == 0) {
            RefNo_AddNewRow();
        }
        row_cnt = 1;
        row = 1;
        $("#tbl #tblbody tr").each(function () {
            $(this).find("td:eq(0)").html(row_cnt);
            row_cnt += 1;
            row += 1;
        });
        if (row > 0) {
            row = parseInt(row) - 1;
        }
    });
});
function Master_Get() {
    Supplierdata = [];
    var supnm = $("#ddl_SupplierName").val();
    $("#ddl_SupplierName").html("<option value=''>Select</option>");

    var obj = {};
    obj.OrderBy = "SupplierName asc";
    obj.IsActive = "1";
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
                Supplierdata = data.Data;
                for (var k in data.Data) {
                    $("#ddl_SupplierName").append("<option value=" + data.Data[k].Id + ">" + data.Data[k].SupplierName + "</option>");
                }
                $("#ddl_SupplierName").val(supnm);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });

    $.ajax({
        url: "/User/Get_Category_Value",
        async: false,
        type: "POST",
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1" && data.Data != null) {
                for (var k in data.Data) {
                    if (data.Data[k].Col_Id == 1) {
                        Shapedata.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 9) {
                        Pointerdata.push(data.Data[k]);
                    }
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
function RefNo_AddNewRow() {
    row = parseInt(row) + 1;

    var shape = "";
    shape = "<option value=''>Select</option>";
    _(Shapedata).each(function (obj, i) {
        shape += "<option value=\"" + obj.Cat_V_Id + "\">" + obj.Cat_Name + "</option>";
    });
    var pointer = "";
    pointer = "<option value=''>Select</option>";
    _(Pointerdata).each(function (obj, i) {
        pointer += "<option value=\"" + obj.Cat_V_Id + "\">" + obj.Cat_Name + "</option>";
    });

    var tbl_html =
        '<tr>' +
        '<td class="tblbody_sr">' + row + '</td>' +
        '<td>' +
        '<select class="form-control Shape">' +
        shape +
        '</select>' +
        '</td>' +
        '<td>' +
        '<select class="form-control Pointer">' +
        pointer +
        '</select>' +
        '</td>' +
        '<td><input type="text" class="form-control Prefix" maxlength="100" autocomplete="off"></td>' +
        '<td style="width: 50px"><i style="cursor:pointer;" class="error RemoveCate"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;" /></i></td>' +
        '</tr>';

    if (parseInt($("#tbl #tblbody").find('tr').length) == 0) {
        $('#tbl #tblbody').html(tbl_html);
    }
    else {
        $('#tbl #tblbody > tr').eq(0).before(tbl_html);
    }

    row_cnt = 1;
    row = 1;
    $("#tbl #tblbody tr").each(function () {
        $(this).find("td:eq(0)").html(row_cnt);
        row_cnt += 1;
        row += 1;
    });
    if (row > 0) {
        row = parseInt(row) - 1;
    }
}
function GetSearch() {
    $(".tab").hide();
    $(".tabcontent").hide();

    $(".cateDetail").hide();
    $(".DeleteAll").hide();
    $('#tblbody').html("");
    row = $('#tblbody').find('tr').length;
    row_cnt = 0;

    if ($("#ddl_SupplierName").val() != "") {
        openTab('RefNoPrefix')
        loaderShow();
        var obj = {};
        obj.SupplierId = $("#ddl_SupplierName").val();

        $.ajax({
            url: "/User/Get_Supplier_RefNo_Prefix",
            async: false,
            type: "POST",
            data: { req: obj },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                $(".cateDetail").show();

                if (data != null && data.Data.length > 0) {
                    _(data.Data).each(function (_obj, i) {
                        row = parseInt(row) + 1;
                        var shape = "";
                        shape = "<option value=''>Select</option>";
                        _(Shapedata).each(function (obj, i) {
                            shape += "<option value=\"" + obj.Cat_V_Id + "\"" + (parseInt(obj.Cat_V_Id) == parseInt(_obj.Shape) ? 'Selected' : '') + ">" + obj.Cat_Name + "</option>";
                        });
                        var pointer = "";
                        pointer = "<option value=''>Select</option>";
                        _(Pointerdata).each(function (obj, i) {
                            pointer += "<option value=\"" + obj.Cat_V_Id + "\"" + (parseInt(obj.Cat_V_Id) == parseInt(_obj.Pointer) ? 'Selected' : '') + ">" + obj.Cat_Name + "</option>";
                        });

                        $('#tblbody').append(
                            "<tr>" +
                            "<td class='tblbody_sr'>" + row + "</td>" +
                            "<td>" +
                            "<select class='form-control Shape'>" +
                            shape +
                            "</select>" +
                            "</td>" +
                            "<td>" +
                            "<select class='form-control Pointer'>" +
                            pointer +
                            "</select>" +
                            "</td>" +
                            "<td><input value=\"" + (_obj.Prefix != null ? _obj.Prefix : '') + "\" type='text' class='form-control Prefix' maxlength='100' autocomplete='off'></td>" +
                            "<td style='width: 50px'><i style='cursor:pointer;' class='error RemoveCate'><img src='/Content/images/trash-delete-icon.png' style='width: 20px;' /></i></td>" +
                            "</tr>"
                        );
                    });
                    $(".DeleteAll").show();
                }
                else {
                    RefNo_AddNewRow();
                }
                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                loaderHide();
            }
        });
    }
    else {
        $(".cateDetail").hide();
        $(".tab").hide();
        $(".tabcontent").hide();
    }
}
var ErrorMsg = [];
var GetError_REF = function () {
    ErrorMsg = [];
    var Shape = false, Pointer = false,Prefix = false;
    $("#tbl #tblbody tr").each(function () {
        if ($(this).find('.Shape').val() == "" && $(this).find('.Pointer').val() == "") {
            Shape = true;
            Pointer = true;
        }
        if ($(this).find('.Prefix').val() == "") {
            Prefix = true;
        }
    });
    if (Shape == true && Pointer == true) {
        ErrorMsg.push({
            'Error': "Please Select Shape or Pointer.",
        });
    }
    if (Prefix == true) {
        ErrorMsg.push({
            'Error': "Please Enter Prefix.",
        });
    }

    return ErrorMsg;
}
var Save = function () {
    ErrorMsg = GetError_REF();

    var arr = [];
    $("#tbl #tblbody tr").each(function () {
        if ($(this).find('.Shape option:selected').text() != "Select" || $(this).find('.Pointer option:selected').text() != "Select") {
            arr.push({
                Shape: ($(this).find('.Shape option:selected').text() == "Select" ? "All" : $(this).find('.Shape option:selected').text()),
                Pointer: ($(this).find('.Pointer option:selected').text() == "Select" ? "All" : $(this).find('.Pointer option:selected').text()),
            });
        }
    });
    var arr1 = arr.map(JSON.stringify).filter((e, i, a) => i != a.indexOf(e)).map(JSON.parse);
    var arr2 = arr1.map(JSON.stringify).filter((e, i, a) => i === a.indexOf(e)).map(JSON.parse);

    if (ErrorMsg.length > 0 || arr2.length > 0) {
        $("#divError").empty();
        ErrorMsg.forEach(function (item) {
            $("#divError").append('<li>' + item.Error + '</li>');
        });
        arr2.forEach(function (item) {
            $("#divError").append('<li>You can not set multiple ' + item.Shape + ' Shape in ' + item.Pointer + ' Pointer.</li>');
        });
        $("#ErrorModel").modal("show");
    }
    //else if (arr2.length > 0) {
    //    $("#divError").empty();
    //    arr2.forEach(function (item) {
    //        $("#divError").append('<li>You can not set multiple ' + item.OurCategory + ' Base Category in ' + item.SupplierName + ' Supplier.</li>');
    //    });
    //    $("#ErrorModel").modal("show");
    //}
    else {
        var list = [];
        $("#tbl #tblbody tr").each(function () {
            list.push({
                SupplierId: $("#ddl_SupplierName").val(),
                Shape: $(this).find('.Shape').val(),
                Pointer: $(this).find('.Pointer').val(),
                Prefix: $(this).find('.Prefix').val(),
            });
        });

        var obj = {};
        obj.refno = list;

        loaderShow();
        $.ajax({
            url: '/User/AddUpdate_Supplier_RefNo_Prefix',
            type: "POST",
            data: { req: obj },
            success: function (data) {
                loaderHide();
                if (data.Status == "1") {
                    toastr.success(data.Message);
                    $(".DeleteAll").show();
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

var DeleteAll = function () {
    $("#DeleteAll").modal("show");
    $("#DeleteAll .modal-body li").html("Are You Sure You Want To Delete All Prefix of " + $("#ddl_SupplierName option:selected").text() + " ?");
}
var ClearRemoveModel = function () {
    $("#DeleteAll").modal("hide");
}
var Delete = function () {
    if ($("#ddl_SupplierName").val() != "") {
        var obj = {};
        obj.SupplierId = $("#ddl_SupplierName").val();

        loaderShow();

        $.ajax({
            url: '/User/Delete_Supplier_RefNo_Prefix',
            type: "POST",
            data: { req: obj },
            success: function (data) {
                loaderHide();
                if (data.Status == "1") {
                    toastr.success(data.Message);
                    $("#DeleteAll").modal("hide");
                    GetSearch();
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

var UserList = [], BUYERList = [], SUPPLIERList = [], CUSTOMERList = [];
$(document).ready(function () {
    Master_Get();
    $("#div_Columns").hide();
    $("#mytable_Buyer tbody").sortable({
        update: function () {
            SetTableOrder("Buyer");
        }
    });
    $("#mytable_Employee tbody").sortable({
        update: function () {
            SetTableOrder("Employee");
        }
    });
    $("#mytable_Customer tbody").sortable({
        update: function () {
            SetTableOrder("Customer");
        }
    });
    $("#li_User_ColumnSetting").addClass("menuActive");
});
function Master_Get() {
    loaderShow();
    $("#DdlUser").html("<option value=''>Select</option>");
    var obj = {};
    //obj.OrderBy = "SupplierName asc";
    $.ajax({
        url: "/User/GetUsers",
        async: false,
        type: "POST",
        data: { req: obj },
        success: function (data, textStatus, jqXHR) {
            loaderHide();
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            if (data != null && data.Data.length > 0) {
                for (var k in data.Data) {
                    //UserList.push({
                    //    UserId: data.Data[k].UserId,
                    //    UserTypeList: data.Data[k].UserTypeList
                    //});
                    $("#DdlUser").append("<option value=" + data.Data[k].UserId + ">" + data.Data[k].FullName + " [" + data.Data[k].UserName + "]" + "</option>");
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
}
function onchange_User() {
    BUYERList = [], SUPPLIERList = [], CUSTOMERList = [];
    $("#Save_btn").hide();
    $("#div_Columns").hide();
    $("#div_Buyer").hide();
    $("#mytable_Buyer #myTableBody").html("");
    $("#div_Employee").hide();
    $("#mytable_Employee #myTableBody").html("");
    $("#div_Customer").hide();
    $("#mytable_Customer #myTableBody").html("");

    if ($("#DdlUser").val() != "") {
        var obj = {};
        obj.UserId = $("#DdlUser").val();

        loaderShow();
        $.ajax({
            url: '/User/Get_ColumnSetting_UserWise',
            type: "POST",
            data: { req: obj },
            success: function (data) {
                if (data.Status == "1" && data.Data.length > 0) {
                    BUYERList = _.filter(data.Data, function (e) { return (e.Type == "BUYER" && e.Access == true) });
                    SUPPLIERList = _.filter(data.Data, function (e) { return (e.Type == "SUPPLIER" && e.Access == true) });
                    CUSTOMERList = _.filter(data.Data, function (e) { return (e.Type == "CUSTOMER" && e.Access == true) });
                    
                    if (BUYERList.length > 0 || SUPPLIERList.length > 0 || CUSTOMERList.length > 0) {
                        $("#div_Columns").show();
                        $("#Save_btn").show();
                    }
                    var html = "";

                    if (BUYERList.length > 0) {
                        $("#div_Buyer").show();
                        html = "";
                        BUYERList.forEach(function (item) {
                            html += "";
                            html += '<tr>'
                            html += '<td><i style="cursor: move;" class="fa fa-bars" aria-hidden="true"></i><input type="hidden" class="hidden" value="' + item.Id + '" />';
                            html += '<td id="lblFieldName" class="onbinding">' + item.Column_Name + '</td>';
                            html += '<td id="lblOrder" class="ColumnOrder onbinding" style="text-align: center;">' + item.OrderBy + '</td>';
                            html += '<td><center>';
                            if (item.Visible == true) {
                                html += '<img src="/Content/images/chebox-fill.png" class="chebox-fill-1 img-block" id="chebox_fillImg_1_' + item.Id + '" onclick="chebox_fill(1, ' + item.Id + ')" style="cursor:pointer;width: 20px;" />';
                                html += '<img src="/Content/images/chebox-empty.png" class="chebox-empty-1 img-none" id="chebox_emptyImg_1_' + item.Id + '" onclick="chebox_empty(1, ' + item.Id + ')" style="cursor:pointer;width: 20px;" />';
                            }
                            else {
                                html += '<img src="/Content/images/chebox-fill.png" class="chebox-fill-1 img-none" id="chebox_fillImg_1_' + item.Id + '" onclick="chebox_fill(1, ' + item.Id + ')" style="cursor:pointer;width: 20px;" />';
                                html += '<img src="/Content/images/chebox-empty.png" class="chebox-empty-1 img-block" id="chebox_emptyImg_1_' + item.Id + '" onclick="chebox_empty(1, ' + item.Id + ')" style="cursor:pointer;width: 20px;" />';
                            }
                            html += '</center></td>';
                            html += '</tr>';
                        });

                        $("#mytable_Buyer #myTableBody").html(html);
                        header_checkuncheck("1");
                    }
                    else {
                        $("#div_Buyer").hide();
                        $("#mytable_Buyer #myTableBody").html("");
                    }

                    if (SUPPLIERList.length > 0) {
                        $("#div_Employee").show();
                        html = "";
                        SUPPLIERList.forEach(function (item) {
                            html += "";
                            html += '<tr>'
                            html += '<td><i style="cursor: move;" class="fa fa-bars" aria-hidden="true"></i><input type="hidden" class="hidden" value="' + item.Id + '" />';
                            html += '<td id="lblFieldName" class="onbinding">' + item.Column_Name + '</td>';
                            html += '<td id="lblOrder" class="ColumnOrder onbinding" style="text-align: center;">' + item.OrderBy + '</td>';
                            html += '<td><center>';
                            if (item.Visible == true) {
                                html += '<img src="/Content/images/chebox-fill.png" class="chebox-fill-2 img-block" id="chebox_fillImg_2_' + item.Id + '" onclick="chebox_fill(2, ' + item.Id + ')" style="cursor:pointer;width: 20px;" />';
                                html += '<img src="/Content/images/chebox-empty.png" class="chebox-empty-2 img-none" id="chebox_emptyImg_2_' + item.Id + '" onclick="chebox_empty(2, ' + item.Id + ')" style="cursor:pointer;width: 20px;" />';
                            }
                            else {
                                html += '<img src="/Content/images/chebox-fill.png" class="chebox-fill-2 img-none" id="chebox_fillImg_2_' + item.Id + '" onclick="chebox_fill(2, ' + item.Id + ')" style="cursor:pointer;width: 20px;" />';
                                html += '<img src="/Content/images/chebox-empty.png" class="chebox-empty-2 img-block" id="chebox_emptyImg_2_' + item.Id + '" onclick="chebox_empty(2, ' + item.Id + ')" style="cursor:pointer;width: 20px;" />';
                            }
                            html += '</center></td>';
                            html += '</tr>';
                        });

                        $("#mytable_Employee #myTableBody").html(html);
                        header_checkuncheck("2");
                    }
                    else {
                        $("#div_Employee").hide();
                        $("#mytable_Employee #myTableBody").html("");
                    }

                    if (CUSTOMERList.length > 0) {
                        $("#div_Customer").show();
                        html = "";
                        CUSTOMERList.forEach(function (item) {
                            html += "";
                            html += '<tr>'
                            html += '<td><i style="cursor: move;" class="fa fa-bars" aria-hidden="true"></i><input type="hidden" class="hidden" value="' + item.Id + '" />';
                            html += '<td id="lblFieldName" class="onbinding">' + item.Column_Name + '</td>';
                            html += '<td id="lblOrder" class="ColumnOrder onbinding" style="text-align: center;">' + item.OrderBy + '</td>';
                            html += '<td><center>';
                            if (item.Visible == true) {
                                html += '<img src="/Content/images/chebox-fill.png" class="chebox-fill-3 img-block" id="chebox_fillImg_3_' + item.Id + '" onclick="chebox_fill(3, ' + item.Id + ')" style="cursor:pointer;width: 20px;" />';
                                html += '<img src="/Content/images/chebox-empty.png" class="chebox-empty-3 img-none" id="chebox_emptyImg_3_' + item.Id + '" onclick="chebox_empty(3, ' + item.Id + ')" style="cursor:pointer;width: 20px;" />';
                            }
                            else {
                                html += '<img src="/Content/images/chebox-fill.png" class="chebox-fill-3 img-none" id="chebox_fillImg_3_' + item.Id + '" onclick="chebox_fill(3, ' + item.Id + ')" style="cursor:pointer;width: 20px;" />';
                                html += '<img src="/Content/images/chebox-empty.png" class="chebox-empty-3 img-block" id="chebox_emptyImg_3_' + item.Id + '" onclick="chebox_empty(3, ' + item.Id + ')" style="cursor:pointer;width: 20px;" />';
                            }
                            html += '</center></td>';
                            html += '</tr>';
                        });

                        $("#mytable_Customer #myTableBody").html(html);
                        header_checkuncheck("3");
                    }
                    else {
                        $("#div_Customer").hide();
                        $("#mytable_Customer #myTableBody").html("");
                    }
                    contentHeight();
                }
                else {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.remove();
                    toastr.error(data.Message);
                }
                loaderHide();
            },
            error: function (xhr, textStatus, errorThrown) {
                loaderHide();
            }
        });
    }
}
function chebox_fill(type, icolumnId) {
    if (type == "Header") {
        $("#chebox_fillImg_" + type + "_" + icolumnId).removeClass('img-block');
        $("#chebox_fillImg_" + type + "_" + icolumnId).addClass('img-none');

        $("#chebox_emptyImg_" + type + "_" + icolumnId).removeClass('img-none');
        $("#chebox_emptyImg_" + type + "_" + icolumnId).addClass('img-block');

        $(".chebox-fill-" + icolumnId).addClass('img-none');
        $(".chebox-fill-" + icolumnId).removeClass('img-block');

        $(".chebox-empty-" + icolumnId).removeClass('img-none');
        $(".chebox-empty-" + icolumnId).addClass('img-block');

    } else {
        $("#chebox_fillImg_" + type + "_" + icolumnId).addClass('img-none');
        $("#chebox_fillImg_" + type + "_" + icolumnId).removeClass('img-block');

        $("#chebox_emptyImg_" + type + "_" + icolumnId).removeClass('img-none');
        $("#chebox_emptyImg_" + type + "_" + icolumnId).addClass('img-block');

        header_checkuncheck(type);
    }
}
function chebox_empty(type, icolumnId) {
    if (type == "Header") {
        $("#chebox_emptyImg_" + type + "_" + icolumnId).removeClass('img-block');
        $("#chebox_emptyImg_" + type + "_" + icolumnId).addClass('img-none');

        $("#chebox_fillImg_" + type + "_" + icolumnId).removeClass('img-none');
        $("#chebox_fillImg_" + type + "_" + icolumnId).addClass('img-block');

        $(".chebox-fill-" + icolumnId).removeClass('img-none');
        $(".chebox-fill-" + icolumnId).addClass('img-block');

        $(".chebox-empty-" + icolumnId).addClass('img-none');
        $(".chebox-empty-" + icolumnId).removeClass('img-block');

    } else {
        $("#chebox_fillImg_" + type + "_" + icolumnId).removeClass('img-none');
        $("#chebox_fillImg_" + type + "_" + icolumnId).addClass('img-block');

        $("#chebox_emptyImg_" + type + "_" + icolumnId).addClass('img-none');
        $("#chebox_emptyImg_" + type + "_" + icolumnId).removeClass('img-block');

        header_checkuncheck(type);
    }
}
function header_checkuncheck(type) {
    var tot = 0, tblname = '', existlist = [];
    if (type == "1") {
        tblname = "mytable_Buyer";
        existlist = BUYERList;
    }
    if (type == "2") {
        tblname = "mytable_Employee";
        existlist = SUPPLIERList;
    }
    if (type == "3") {
        tblname = "mytable_Customer";
        existlist = CUSTOMERList;
    }

    $("#" + tblname + " #myTableBody tr").each(function () {
        var Id = $(this).find('input[type="hidden"]').val();
        if ($('#chebox_fillImg_' + type + '_' + Id).hasClass('img-block')) {
            tot += parseInt(1);
        }
    });

    if (existlist.length == tot) {
        $("#chebox_fillImg_Header_" + type).addClass('img-block');
        $("#chebox_fillImg_Header_" + type).removeClass('img-none');
        $("#chebox_emptyImg_Header_" + type).addClass('img-none');
        $("#chebox_emptyImg_Header_" + type).removeClass('img-block');
    }
    else {
        $("#chebox_fillImg_Header_" + type).addClass('img-none');
        $("#chebox_fillImg_Header_" + type).removeClass('img-block');
        $("#chebox_emptyImg_Header_" + type).addClass('img-block');
        $("#chebox_emptyImg_Header_" + type).removeClass('img-none');
    }
}
function SetTableOrder(type) {
    var OrderNo = 1;
    $("#mytable_" + type + " tbody tr").each(function () {
        ($(this).find(".ColumnOrder").text(OrderNo));
        OrderNo = OrderNo + 1;
    });
};
function contentHeight() {
    var winH = $(window).height(),
        serachHei = $(".gridview").height(),
        serachHei1 = 0,
        contentHei = winH - serachHei - serachHei1 - 210;
    $("#div_Buyer").css("height", contentHei);
    $("#div_Employee").css("height", contentHei);
    $("#div_Customer").css("height", contentHei);
}
$(window).resize(function () {
    contentHeight();
});
function SaveData() {
    var BUYERSave = [], SUPPLIERSave = [], CUSTOMERSave = [];

    if (BUYERList.length > 0) {
        $("#mytable_Buyer #myTableBody tr").each(function () {
            var Id = $(this).find('input[type="hidden"]').val();
            var OrderBy = $(this).find("td:eq(2)").html();
            if ($('#chebox_fillImg_1_' + Id).hasClass('img-block')) {
                BUYERSave.push({
                    UserId: $("#DdlUser").val(),
                    Id: Id,
                    OrderBy: OrderBy
                });
            }
        });
        if (BUYERSave.length == 0) {
            toastr.remove();
            toastr.warning("Please Select Minimum 1 Column in Buyer !");
            return;
        }
    }

    if (SUPPLIERList.length > 0) {
        $("#mytable_Employee #myTableBody tr").each(function () {
            var Id = $(this).find('input[type="hidden"]').val();
            var OrderBy = $(this).find("td:eq(2)").html();
            if ($('#chebox_fillImg_2_' + Id).hasClass('img-block')) {
                SUPPLIERSave.push({
                    UserId: $("#DdlUser").val(),
                    Id: Id,
                    OrderBy: OrderBy
                });
            }
        });
        if (SUPPLIERSave.length == 0) {
            toastr.remove();
            toastr.warning("Please Select Minimum 1 Column in Employee !");
            return;
        }
    }

    if (CUSTOMERList.length > 0) {
        $("#mytable_Customer #myTableBody tr").each(function () {
            var Id = $(this).find('input[type="hidden"]').val();
            var OrderBy = $(this).find("td:eq(2)").html();
            if ($('#chebox_fillImg_3_' + Id).hasClass('img-block')) {
                CUSTOMERSave.push({
                    UserId: $("#DdlUser").val(),
                    Id: Id,
                    OrderBy: OrderBy
                });
            }
        });
        if (CUSTOMERSave.length == 0) {
            toastr.remove();
            toastr.warning("Please Select Minimum 1 Column in Customer !");
            return;
        }
    }

    var obj = {};
    obj.BUYER = BUYERSave;
    obj.SUPPLIER = SUPPLIERSave;
    obj.CUSTOMER = CUSTOMERSave;

    loaderShow();
    $.ajax({
        url: '/User/AddUpdate_ColumnSetting_UserWise',
        type: "POST",
        data: { req: obj },
        success: function (data) {
            loaderHide();
            if (data.Status == "1") {
                toastr.remove();
                toastr.success(data.Message);
            }
            else {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                toastr.remove();
                toastr.error(data.Message);
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            loaderHide();
        }
    });
}
var Supplierdata = [], Categorydata = [];
var row = $('#tblbody').find('tr').length;
var row_cnt = 0;
var hdn_Col_Id = "";

$(document).ready(function (e) {
    Master_Get();
    //$("#tblbody").on('click', '.RemoveCate', function () {
    //    $(this).closest('tr').remove();
    //    if (parseInt($("#tbl #tblbody").find('tr').length) == 0) {
    //        AddNewRow();
    //    }
    //    row_cnt = 1;
    //    row = 1;
    //    $("#tbl #tblbody tr").each(function () {
    //        $(this).find("td:eq(0)").html(row_cnt);
    //        row_cnt += 1;
    //        row += 1;
    //    });
    //    if (row > 0) {
    //        row = parseInt(row) - 1;
    //    }
    //});
    $('#Modal_CategoryMas').on('shown.bs.modal', function (e) {
        CateMas_Back();
        CateMas_GetSearch();
    });
    $('#Modal_CategoryMas').on('hidden.bs.modal', function (e) {
        //Master_Get();
        //GetSearch();
        BindCategoryTab();
    });
    $("#li_User_SupplierValue").addClass("menuActive");
});
function Master_Get() {
    Supplierdata = [];
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
                $("#ddl_SupplierName").html("<option value=''>Select</option>");
                for (var k in data.Data) {
                    $("#ddl_SupplierName").append("<option value=" + data.Data[k].Id + ">" + data.Data[k].SupplierName + "</option>");
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
function BindCategoryTab() {
    if ($("#ddl_SupplierName").val() != "") {
        $("#tab").show();

        var obj = {};
        obj.OrderBy = "Col_Id asc";
        obj.Not_Col_Id = "9,67,48,49,46,51";
        
        $.ajax({
            url: "/User/Get_CategoryMas",
            async: false,
            type: "POST",
            data: { req: obj },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data != null && data.Data.length > 0) {
                    Categorydata = data.Data;
                    var html = "";
                    for (var k in data.Data) {
                        html += '<button class="tablinks tab_' + data.Data[k].Col_Id + '" onclick="openTab(\'tab_' + data.Data[k].Col_Id + '\')">' + data.Data[k].Column_Name + '</button>';
                    }
                    $("#tab").html('<div class="tab" style="display:none;">' + html + '</div>');

                    for (var k in data.Data) {
                        $("#tab").append('<div id="tab_' + data.Data[k].Col_Id + '" class="tabcontent tab_' + data.Data[k].Col_Id + '">' + data.Data[k].Id + '</div>');
                    }

                    openTab('tab_' + data.Data[0].Col_Id);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
            }
        });
    }
    else {
        $("#tab").hide();
    }
}
function openTab(MenuName) {
    $(".tab").show();
    $(".tablinks").removeClass("active");
    $(".tabcontent").removeClass("active");
    $(".tabcontent").hide();
    $("." + MenuName).addClass("active");
    $("#" + MenuName).show();

    for (var k in Categorydata) {
        $("#tab_" + Categorydata[k].Id).html("");
    }
    $(".tabcontent").html("");
    //$("#" + MenuName).html('');
    hdn_Col_Id = MenuName.slice(4);
    $.ajax
        ({
            url: "/User/SupplierValue_AddNew",
            contentType: "application/html; charset=utf-8",
            type: "GET",
            cache: !0,
            datatype: "html",
            success: function (t) {
                $("#" + MenuName).html(t);
                //$(".cateDetail").show();
                Master_Get();
                GetSearch();
            },
            error: function () {

            }
        })
}






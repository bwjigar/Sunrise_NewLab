var FortuneCodeValid = true;
var FortuneCodeValid_Msg = "";
var OrderBy = "";

var gridOptions = {};
var iUserid = 0;
//var today = new Date();

let today = new Date();
// Add 2.5 hours to the current date
//today.setHours(today.getHours() + 2, today.getMinutes() + 30);
today.setHours(today.getHours() + 0, today.getMinutes() + 0);

var lastWeekDate = new Date(today.setDate(today.getDate() - 7));

today = new Date();
// Add 2.5 hours to the current date
//today.setHours(today.getHours() + 2, today.getMinutes() + 30);
today.setHours(today.getHours() + 0, today.getMinutes() + 0);

var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var date = new Date(lastWeekDate),
    mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    day = ("0" + date.getDate()).slice(-2);
var F_date = [day, m_names[mnth - 1], date.getFullYear()].join("-");
function SetCurrentDate() {
    var curr_date = today.getDate();
    var curr_month = today.getMonth();
    var curr_year = today.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
    return FinalDate;
}
function FromTo_Date() {
    $('#txtFromDate').val(F_date);
    $('#txtToDate').val(SetCurrentDate());
    $('#txtFromDate').daterangepicker({
        singleDatePicker: true,
        startDate: F_date,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        maxDate: today,
        minYear: parseInt(moment().format('YYYY'), 10) - 10
    }).on('change', function (e) {
        greaterThanDate(e, "txtFromDate", "txtToDate");
    });
    $('#txtToDate').daterangepicker({
        singleDatePicker: true,
        startDate: today,
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        maxDate: today,
        minYear: parseInt(moment().format('YYYY'), 10) - 10
    }, function (start, end, label) {
        var years = moment().diff(start, 'years');
    }).on('change', function (e) {
        greaterThanDate(e, "txtFromDate", "txtToDate");
    });

}
var pgSize = 200;
function onPageSizeChanged() {
    var value = $("#ddlPagesize").val();
    pgSize = Number(value);
    GetSearch();
}
var showEntryHtml = '<div class="show_entry"><label>'
    + 'Show <select onchange = "onPageSizeChanged()" id = "ddlPagesize">'
    + '<option value="200">200</option>'
    + '<option value="500">500</option>'
    + '<option value="1000">1000</option>'
    + '</select> entries'
    + '</label>'
    + '</div>';

var columnDefs = [];
columnDefs.push({ headerName: "Activity Date", field: "SearchDate", width: 130, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "User Name", field: "UserName", width: 140, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Company Name", field: "CompName", width: 230, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Customer Name", field: "CustomerName", width: 160, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "IP Address", field: "IPAddress", width: 130, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Mac ID", field: "MacID", width: 130, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Device Type", field: "DeviceType", width: 85, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Form Name", field: "FormName", width: 100, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Activity", field: "Activity", width: 100, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Supplier Name", field: "SupplierName", width: 250, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Ref No", field: "RefNo", width: 250, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Shape", field: "Shape", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Carat", field: "Carat", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Discount", field: "Discount", width: 100, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Final Price/Ct", field: "Final_Price_Ct", width: 100, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Amount", field: "Amount", width: 100, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Color Type", field: "ColorType", width: 100, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Color", field: "Color", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Intensity", field: "Intensity", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Overtone", field: "Overtone", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Fancy Color", field: "FancyColor", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Clarity", field: "Clarity", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Cut", field: "Cut", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Symmetry", field: "Symmetry", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Polish", field: "Polish", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Fluorescence", field: "Fluorescence", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "BGM", field: "BGM", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Media", field: "Media", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Lab", field: "Lab", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "KTS Blank", field: "KTS_Blank", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "KTS Check", field: "KTS_Check", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "KTS Un Check", field: "KTS_UnCheck", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "RC Blank", field: "RC_Blank", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "RC Check", field: "RC_Check", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "RC Un Check", field: "RC_UnCheck", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Culet", field: "Culet", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Location", field: "Location", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Length", field: "Length", width: 80, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Length Blank", field: "Length_Blank", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Width", field: "Width", width: 80, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Width Blank", field: "Width_Blank", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Depth", field: "Depth", width: 80, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Depth Blank", field: "Depth_Blank", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Depth %", field: "Depth_Per", width: 80, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Depth % Blank", field: "Depth_Per_Blank", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Table %", field: "Table_Per", width: 80, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Table % Blank", field: "Table_Per_Blank", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Girdle", field: "Girdle", width: 80, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Girdle Blank", field: "Girdle_Blank", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Crown Angle", field: "CrownAngle", width: 80, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Crown Angle Blank", field: "CrownAngle_Blank", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Crown Height", field: "CrownHeight", width: 80, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Crown Height Blank", field: "CrownHeight_Blank", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Pavilion Angle", field: "PavilionAngle", width: 80, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Pavilion Angle Blank", field: "PavilionAngle_Blank", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Pav Height", field: "PavHeight", width: 80, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Pav Height Blank", field: "PavHeight_Blank", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Star Lengh", field: "StarLengh", width: 80, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Star Lengh Blank", field: "StarLengh_Blank", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Lower Half", field: "LowerHalf", width: 80, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Lower Half Blank", field: "LowerHalf_Blank", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Table Black", field: "TableBlack", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Crown Black", field: "CrownBlack", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Table White", field: "TableWhite", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Crown White", field: "CrownWhite", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Table Open", field: "TableOpen", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Crown Open", field: "CrownOpen", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Pav Open", field: "PavOpen", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Girdle Open", field: "GirdleOpen", width: 190, sortable: false, tooltip: function (params) { return (params.value); } });

function GetSearch() {
    loaderShow();
    if (gridOptions.api != undefined) {
        gridOptions.api.destroy();
    }

    gridOptions = {
        defaultColDef: {
            enableSorting: true,
            sortable: true,
            resizable: true,
            filter: 'agTextColumnFilter',
            filterParams: {
                applyButton: true,
                resetButton: true,
            }
        },
        pagination: true,
        icons: {
            groupExpanded:
                '<i class="fa fa-minus-circle"/>',
            groupContracted:
                '<i class="fa fa-plus-circle"/>'
        },
        rowSelection: 'multiple',
        suppressRowClickSelection: true,
        columnDefs: columnDefs,
        rowModelType: 'serverSide',
        cacheBlockSize: pgSize, // you can have your custom page size
        paginationPageSize: pgSize, //pagesize
        getContextMenuItems: getContextMenuItems,
        getRowHeight: function (params) { return 35; },
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };
    var gridDiv = document.querySelector('#Cart-Gride');
    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource1);

    showEntryVar = setInterval(function () {
        if ($('#Cart-Gride .ag-paging-panel').length > 0) {
            $('#Cart-Gride .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');

            $(showEntryHtml).appendTo('#Cart-Gride .ag-paging-panel');
            $('#ddlPagesize').val(pgSize);
            clearInterval(showEntryVar);
        }
    }, 1000);
}
var SortColumn = "";
var SortDirection = "";
const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var obj = {};

        if (params.request.sortModel.length > 0) {
            obj.OrderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort;
            OrderBy = obj.OrderBy;
        }
        obj.PgNo = PageNo;
        obj.PgSize = pgSize;
        obj.CustName = $("#txt_S_Search").val();
        obj.FromDate = $("#txtFromDate").val();
        obj.ToDate = $("#txtToDate").val();
        obj.UserType = $("#ddl_UserType").val().join(",")

        $.ajax({
            url: "/User/Get_UserActivity",
            async: false,
            type: "POST",
            data: { req: obj },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    params.successCallback(data.Data, data.Data[0].iTotalRec);
                }
                else {
                    toastr.remove();
                    toastr.error("No Data Found", { timeOut: 2500 });
                    gridOptions.api.showNoRowsOverlay();
                    params.successCallback([], 0);
                }
                setInterval(function () {
                    $(".ag-header-cell-text").addClass("grid_prewrap");
                }, 30);
                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                loaderHide();
            }
        });
    }
};

function onGridReady(params) {
    if (navigator.userAgent.indexOf('Windows') > -1) {
        this.api.sizeColumnsToFit();
    }
}
var Reset = function () {
    FromTo_Date();
    UserType_Reset();
    $('#txt_S_Search').val('');
    GetSearch();
}
function UserType_Reset() {
    $('#ddl_UserType').multiselect({
        includeSelectAllOption: true, numberDisplayed: 1
    });
    $('#ddl_UserType option:selected').each(function () {
        $(this).prop('selected', false);
    })

    var selectedOptions = [3];
    for (var i in selectedOptions) {
        var optionVal = selectedOptions[i];
        $("#ddl_UserType").find("option[value=" + optionVal + "]").prop("selected", "selected");
    }
    $("#ddl_UserType").multiselect('refresh');
}
function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".order-title").height(),
        serachHei = $(".order-history-data").height(),
        contentHei = winH - serachHei - navbarHei - 112;
    contentHei = (contentHei < 200 ? 369 : contentHei);
    $("#Cart-Gride").css("height", contentHei);
}

$(document).ready(function (e) {
    $("#li_User_UserActivity").addClass("menuActive");
    UserTypeGet();
    setTimeout(function () {
        FromTo_Date();
        GetSearch();
        contentHeight();
    }, 50);
});

$(window).resize(function () {
    contentHeight();
});
function Excel_UserActivity() {
    if (gridOptions.api != undefined) {
        loaderShow();
        setTimeout(function () {
            var obj = {};
            obj.OrderBy = OrderBy;
            obj.CustName = $("#txt_S_Search").val();
            obj.FromDate = $("#txtFromDate").val();
            obj.ToDate = $("#txtToDate").val();
            obj.UserType = $("#ddl_UserType").val().join(",")

            $.ajax({
                url: "/User/Excel_UserActivity",
                async: false,
                type: "POST",
                data: { req: obj },
                success: function (data, textStatus, jqXHR) {
                    loaderHide();
                    if (data.search('.xlsx') == -1) {
                        if (data.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        toastr.remove();
                        toastr.error(data);
                    } else {
                        location.href = data;
                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    loaderHide();
                }
            });
        }, 50);
    }
}
function UserTypeGet() {
    $.ajax({
        url: '/User/get_UserType',
        type: "POST",
        async: false,
        success: function (data, textStatus, jqXHR) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            if (data != null && data.Data.length > 0) {
                for (var k in data.Data) {
                    $("#ddl_UserType").append("<option value=" + data.Data[k].Id + ">" + data.Data[k].UserType + "</option>");
                }
                UserType_Reset();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
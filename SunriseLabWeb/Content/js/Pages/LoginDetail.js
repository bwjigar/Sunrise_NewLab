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
columnDefs.push({ headerName: "Login Date", field: "LoginDate", width: 100, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Login Time", field: "LoginTime", width: 70, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "User Name", field: "UserName", width: 140, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Company Name", field: "CompName", width: 230, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Customer Name", field: "CustomerName", width: 160, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "IP Address", field: "IPAddress", width: 130, sortable: false, tooltip: function (params) { return (params.value); } });
columnDefs.push({ headerName: "Device Type", field: "DeviceType", width: 85, sortable: false, tooltip: function (params) { return (params.value); } });

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
            url: "/User/Get_LoginDetail",
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
        contentHei = winH - serachHei - navbarHei - 110;
    contentHei = (contentHei < 200 ? 369 : contentHei);
    $("#Cart-Gride").css("height", contentHei);
}

$(document).ready(function (e) {
    $("#li_User_LoginDetail").addClass("menuActive");
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
function Excel_LoginDetail() {
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
                url: "/User/Excel_LoginDetail",
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
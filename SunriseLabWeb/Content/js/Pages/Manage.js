var CompNameValid = true;
var CompNameValid_Msg = "";
var FortuneCodeValid = true;
var FortuneCodeValid_Msg = "";
var UserCodeValid = true;
var UserCodeValid_Msg = "";
var Rowdata = [];
var view = "Add";
var ipAddresses_Wrong = [];

var gridOptions = {};
var iUserid = 0;
var today = new Date();
var lastWeekDate = new Date(today.setDate(today.getDate() - 7));
var m_names = new Array("Jan", "Feb", "Mar",
    "Apr", "May", "Jun", "Jul", "Aug", "Sep",
    "Oct", "Nov", "Dec");
var date = new Date(lastWeekDate),
    mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    day = ("0" + date.getDate()).slice(-2);
var F_date = [day, m_names[mnth - 1], date.getFullYear()].join("-");
function SetCurrentDate() {
    var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
    var d = new Date();
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
    return FinalDate;
}
function ddlFilterType() {
    if ($("#ddlFilterType").val() == "CD" || $("#ddlFilterType").val() == "LAD" || $("#ddlFilterType").val() == "LLD") {
        $("#divDatetime").show();
        $("#divWithoutDatetime").hide();
        $("#txtCommonName").val("");
        FromTo_Date();
    }
    else {
        $("#divDatetime").hide();
        $("#divWithoutDatetime").show();
    }
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
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }).on('change', function (e) {
        greaterThanDate(e);
    });
    $('#txtToDate').daterangepicker({
        singleDatePicker: true,
        startDate: SetCurrentDate(),
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        minYear: 1901,
        maxYear: parseInt(moment().format('YYYY'), 10)
    }).on('change', function (e) {
        greaterThanDate(e);
    });
}
function greaterThanDate(evt) {
    if ($.trim($('#txtToDate').val()) != "") {
        var fDate = $.trim($('#txtFromDate').val());
        var tDate = $.trim($('#txtToDate').val());
        if (fDate != "" && tDate != "") {
            if (new Date(tDate) >= new Date(fDate)) {
                return true;
            }
            else {
                evt.currentTarget.value = "";
                toastr.remove();
                toastr.warning("To date must be greater than From date");
                FromTo_Date();
                return false;
            }
        }
        else {
            return true;
        }
    }
}

//single node get from Multi diamension Array List
function filterByProperty(data, prop, value) {
    var filtered = [];
    for (var i = 0; i < data.length; i++) {
        var obj = data[i];
        if (obj[prop] == value) {
            filtered.push(data[i]);
        }
    }
    return filtered;
}


var columnDefs = [
    { headerName: "UserId", field: "UserId", hide: true },
    { headerName: "Sr", field: "iSr", tooltip: function (params) { return (params.value); }, sortable: false, width: 40 },
    { headerName: "Action", field: "bIsAction", tooltip: function (params) { return (params.value); }, width: 60, cellRenderer: 'deltaIndicator', sortable: false },
    { headerName: "Create Date", field: "CreatedDate", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: "Last Login Date", field: "LastLoginDate", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: "UserTypeId", field: "UserTypeId", hide: true },
    { headerName: "User Type", field: "UserType", sortable: false, tooltip: function (params) { return (params.value); }, width: 190 },
    { headerName: "Active", field: "IsActive", cellRenderer: 'faIndicator', tooltip: function (params) { if (params.value == true) { return 'Yes'; } else { return 'No'; } }, cellClass: ['muser-fa-font'], width: 55 },
    { headerName: "User Name", field: "UserName", tooltip: function (params) { return (params.value); }, width: 160 },
    { headerName: "Password", field: "Password", hide: true },
    { headerName: "FirstName", field: "FirstName", hide: true },
    { headerName: "LastName", field: "LastName", hide: true },
    { headerName: "Customer Name", field: "FullName", tooltip: function (params) { return (params.value); }, width: 175 },
    { headerName: "Company Name", field: "CompName", tooltip: function (params) { return (params.value); }, width: 210 },
    { headerName: "Fortune Party Code", field: "FortunePartyCode", tooltip: function (params) { return (params.data.FortunePartyCode > 0 ? params.data.FortunePartyCode : ""); }, cellRenderer: function (params) { return (params.data.FortunePartyCode > 0 ? params.data.FortunePartyCode : ""); }, width: 75 },
    { headerName: "User Code", field: "UserCode", tooltip: function (params) { return (params.value); }, cellRenderer: function (params) { return (params.data.UserCode > 0 ? params.data.UserCode : ""); }, width: 75 },
    { headerName: "Assist", field: "AssistByName", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "Sub Assist", field: "SubAssistByName", tooltip: function (params) { return (params.value); }, width: 150 },
    { headerName: "Mobile", field: "MobileNo", tooltip: function (params) { return (params.value); }, width: 140 },
    { headerName: "Email Id", field: "EmailId", tooltip: function (params) { return (params.value); }, width: 155 },
    { headerName: "Email Id 2", field: "EmailId_2", tooltip: function (params) { return (params.value); }, width: 155 },
];var deltaIndicator = function (params) {
    var element = "";
    element = '<a title="Edit" onclick="EditView(\'' + params.data.UserId + '\')" ><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 17px;cursor:pointer;"></i></a>';
    element += '&nbsp;&nbsp;<a title="Delete" onclick="DeleteView(\'' + params.data.UserId + '\')"><i class="fa fa-trash-o" aria-hidden="true" style="cursor:pointer;"></i></a>';
    return element;
}
var faIndicator = function (params) {
    var element = document.createElement("a");
    element.title = '';
    element.innerHTML = '<i class="fa fa-check" aria-hidden="true"></i>';
    if (params.value) {
        return element;
    }
}
var GoToUserDetail = function (sUserType, iUserid, sUsername) {
    window.location = '/User/Edit?UserType=' + sUserType + '&UserID=' + iUserid + '&UserName=' + sUsername;
}

function EditView(UserId) {
    var data = filterByProperty(Rowdata, "UserId", UserId);
    if (data.length == 1) {
        view = "Edit";
        $(".pwd").hide();
        if ($("#hdn_UserType").val().includes("1")) {
            $(".pwd").show();
            $("#txt_Password").val(data[0].Password);
        }
        AssistBy_Get();

        $("#hdn_Mng_UserId").val(data[0].UserId);
        $("#txt_UserName").val(data[0].UserName);
        $("#chk_Active").prop('checked', data[0].IsActive);

        if (data[0].UserTypeId != "") {
            var selectedOptions = data[0].UserTypeId.split(",");
            for (var i in selectedOptions) {
                var optionVal = selectedOptions[i];
                $("#ddl_UserType").find("option[value=" + optionVal + "]").prop("selected", "selected");
            }
            $("#ddl_UserType").multiselect('refresh');
        }

        $("#txt_FirstName").val(data[0].FirstName);
        $("#txt_LastName").val(data[0].LastName);
        $("#txt_CompanyName").val(data[0].CompName);
        $("#txt_FortunePartyCode").val((data[0].FortunePartyCode > 0 ? data[0].FortunePartyCode : ""));
        $("#txt_UserCode").val((data[0].UserCode > 0 ? data[0].UserCode : ""));
        $("#txt_EmailId").val(data[0].EmailId);
        $("#txt_EmailId_2").val(data[0].EmailId_2);
        $("#txt_MobileNo").val(data[0].MobileNo);
        $("#ddl_AssistBy").val((data[0].AssistBy > 0 ? data[0].AssistBy : ""));
        $("#ddl_SubAssistBy").val((data[0].SubAssistBy > 0 ? data[0].SubAssistBy : ""));

        var stock = [];
        if (data[0].View == true)
            stock.push('1');
        if (data[0].Download == true)
            stock.push('2');

        if (stock.length > 0) {
            for (var i in stock) {
                var optionVal = stock[i];
                $("#ddl_Stock").find("option[value=" + optionVal + "]").prop("selected", "selected");
            }
            $("#ddl_Stock").multiselect('refresh');
        }

        //$("#View").prop("checked", data[0].View);
        //$("#Download").prop("checked", data[0].Download);

        $("#txt_CompanyAddress").val(data[0].CompAddress);
        $("#txt_RestrictedIP").val(data[0].RestrictedIP);

        $(".gridview").hide();
        $(".AddEdit").show();
        $("#btn_AddNew").hide();
        $("#btn_Back").show();
        $("#h2_titl").html("Edit User");
        $("#hdn_Mng_UserId").val(data[0].UserId);
        ChangeUserType();
        Get_SubUser(UserId);
    }
}
function DeleteView(iUserid) {
    $("#hdnDelUserId").val(iUserid);
    $("#Remove").modal("show");
}

var ClearRemoveModel = function () {
    $("#hdnDelUserId").val("0");
    $("#Remove").modal("hide");
}

var DeleteUser = function () {
    loaderShow();
    $.ajax({
        type: "POST",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        url: '/User/Delete_UserMas',
        data: '{ "UserId": ' + $("#hdnDelUserId").val() + '}',
        success: function (data) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            loaderHide();

            if (data.Status == "-1") {
                toastr.remove();
                toastr.warning(data.Message, { timeOut: 3000 });
            }
            else {
                ClearRemoveModel();
                GetSearch();
                toastr.remove();
                toastr.success(data.Message, { timeOut: 3000 });
            }

        }
    });
}

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
        components: {
            deltaIndicator: deltaIndicator,
            faIndicator: faIndicator,
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
        //rowData: data,
        rowModelType: 'serverSide',
        //onGridReady: onGridReady,
        cacheBlockSize: 50, // you can have your custom page size
        paginationPageSize: 50, //pagesize
        getContextMenuItems: getContextMenuItems,
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };
    var gridDiv = document.querySelector('#Cart-Gride');
    new agGrid.Grid(gridDiv, gridOptions);

    $(".ag-header-cell-text").addClass("grid_prewrap");

    gridOptions.api.setServerSideDatasource(datasource1);
}var SortColumn = "";
var SortDirection = "";const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var obj = {};

        if (params.request.sortModel.length > 0) {
            obj.OrderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort;
        }
        obj.PgNo = PageNo;
        obj.PgSize = "50";
        if ($("#ddlFilterType").val() == "CD" || $("#ddlFilterType").val() == "LLD") {
            obj.FilterType = $("#ddlFilterType").val();
            obj.FromDate = $("#txtFromDate").val();
            obj.ToDate = $("#txtToDate").val();
        }
        if ($("#ddlFilterType").val() == "FPC") {
            obj.FortunePartyCode = $("#txtCommonName").val();
        }
        if ($("#ddlFilterType").val() == "CUN") {
            obj.FullName = $("#txtCommonName").val();
        }
        if ($("#ddlFilterType").val() == "UN") {
            obj.UserName = $("#txtCommonName").val();
        }
        if ($("#ddlFilterType").val() == "CM") {
            obj.CompName = $("#txtCommonName").val();
        }
        obj.IsActive = $('#ddlIsActive').val();
        obj.UserType = $('#ddlUserType').val();
        obj.UserId_Grid = $('#hdn_UserId').val();

        Rowdata = [];
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
                if (data.Data.length > 0) {
                    Rowdata = data.Data;
                    params.successCallback(data.Data, data.Data[0].iTotalRec);
                }
                else {
                    Rowdata = [];
                    toastr.remove();
                    toastr.error(data.Message, { timeOut: 2500 });
                    params.successCallback([], 0);
                }
                setInterval(function () {
                    $(".ag-header-cell-text").addClass("grid_prewrap");
                }, 30);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                Rowdata = [];
                loaderHide();
            }
        });
    }
};function onGridReady(params) {
    if (navigator.userAgent.indexOf('Windows') > -1) {
        this.api.sizeColumnsToFit();
    }
}var Reset = function () {
    $('#ddlFilterType').val('UN');
    $('#txtCommonName').val('');
    $('#ddlUserType').val('');
    $('#ddlIsActive').val('');
    ddlFilterType();
    GetSearch();
}function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".order-title").height(),
        serachHei = $(".order-history-data").height(),
        contentHei = winH - serachHei - navbarHei - 130;
    $("#Cart-Gride").css("height", contentHei);
}$(document).ready(function (e) {
    UserTypeGet();
    StockDdlGet();
    GetSearch();
    contentHeight();
    //$('#txt_FortunePartyCode').onFocusout(function () {
    //    Check_FortunePartyCode_Exist();
    //});
    //$('#txt_UserCode').onFocusout(function () {
    //    Check_UserCode_Exist();
    //});
    $("#li_User_Manage").addClass("menuActive");

    $("#tbl_SubUser").on('click', '.RemoveUser', function () {
        $(this).closest('tr').remove();
        if (parseInt($("#tbl_SubUser #tblbody_SubUser").find('tr').length) == 0) {
            //SubUser_AddNewRow("0");
            $("#tbl_SubUser").hide();
        }
        row_cnt = 1;
        row = 1;
        $("#tbl_SubUser #tblbody_SubUser tr").each(function () {
            $(this).find("td:eq(1)").html(row_cnt);
            row_cnt += 1;
            row += 1;
        });
        if (row > 0) {
            row = parseInt(row) - 1;
        }
    });

    $('#txt_FortunePartyCode, #txt_UserCode').on('keypress', function (event) {
        var charCode = event.which || event.keyCode;
        if (charCode >= 48 && charCode <= 57) // 0-9
        {
            return true;
        } else {
            event.preventDefault();
            return false;
        }
    });
    $('#txt_FortunePartyCode, #txt_UserCode').on('input', function () {
        var sanitizedValue = $(this).val().replace(/\D/g, ''); // Remove non-integer characters
        $(this).val(sanitizedValue); // Update input value
    });
    $('#txt_MobileNo').on('keypress', function (event) {
        var charCode = event.which || event.keyCode;
        if ((charCode >= 48 && charCode <= 57) || // 0-9
            charCode === 43) //+
        {
            return true;
        } else {
            event.preventDefault();
            return false;
        }
    });
    $('#txt_MobileNo').on('input', function () {
        var sanitizedValue = $(this).val().replace(/[^\d+]/g, ''); // Remove characters that are not digits or +
        $(this).val(sanitizedValue); // Update input value
    });
    $('#txt_Password').on('keypress', function (event) {
        var charCode = event.which || event.keyCode;
        if (charCode == 32) // Blank space
        {
            event.preventDefault();
            return false;
        } else {
            return true;
        }
    });
    $('#txt_RestrictedIP').focusout(function () {
        var input = $("#txt_RestrictedIP").val().trim();
        // Remove duplicate commas
        var step1 = input.replace(/,{2,}/g, ',');
        // Remove commas not surrounded by integers
        var step2 = step1.replace(/(?<=,)\D+|(?<=\D),/g, '');
        // Remove commas at the start or end of the string
        var step3 = step2.replace(/^,|,$/g, '').trim();
        $("#txt_RestrictedIP").val(step3);

        if (step3 != "") {
            var ipAddresses = step3.split(',');
            ipAddresses_Wrong = [];
            // Regular expression to validate IP address
            var ipRegex = /^(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/;

            for (var i = 0; i < ipAddresses.length; i++) {
                var ipAddress = ipAddresses[i].trim(); // Remove leading/trailing spaces
                if (ipAddress != "") {
                    if (!ipRegex.test(ipAddress)) {
                        ipAddresses_Wrong.push(ipAddress);
                    }
                }
            }

        }
    });
    $('#txt_RestrictedIP').on('keypress', function (event) {
        var charCode = event.which || event.keyCode;
        if ((charCode >= 48 && charCode <= 57) || // 0-9
            charCode === 46 || //.
            charCode === 44) //,
        {
            return true;
        } else {
            event.preventDefault();
            return false;
        }
    });
    $('#txt_RestrictedIP').on('input', function () {
        var sanitizedValue = $(this).val().replace(/[^\d.,]/g, ''); // Remove characters that are not digits or +
        $(this).val(sanitizedValue); // Update input value
    });
});

$(window).resize(function () {
    contentHeight();
});

function AddNew() {
    view = "Add";
    $(".pwd").show();
    Clear()
    $(".gridview").hide();
    $(".AddEdit").show();
    $("#btn_AddNew").hide();
    $("#btn_Back").show();
    $("#h2_titl").html("Add User");
    AssistBy_Get();
    $("#hdn_Mng_UserId").val("");
}
function Back() {
    $(".gridview").show();
    $(".AddEdit").hide();
    $("#btn_AddNew").show();
    $("#btn_Back").hide();
    $("#h2_titl").html("Manage User");
    $("#hdn_Mng_UserId").val("");

    Clear();

    GetSearch();
}
function Clear() {
    $("#txt_UserName").val("");
    $("#txt_Password").val("");
    $("#txt_CompanyAddress").val("");
    $("#txt_RestrictedIP").val("");
    $("#chk_Active").prop('checked', true);

    $('#ddl_UserType option:selected').each(function () {
        $(this).prop('selected', false);
    })
    $('#ddl_UserType').multiselect('refresh');

    $('#ddl_Stock option:selected').each(function () {
        $(this).prop('selected', false);
    })
    $('#ddl_Stock').multiselect('refresh');

    $("#lbl_assist_by").html('1<small style="font-weight: 600;">st</small> Assist By: ');
    $("#lbl_fortun_code").html('Fortune Party Code: ');
    $("#lbl_user_code").html('User Code: ');

    $("#txt_FirstName").val("");
    $("#txt_LastName").val("");
    $("#txt_CompanyName").val("");
    $("#txt_FortunePartyCode").val("");
    $("#txt_UserCode").val("");
    $("#txt_EmailId").val("");
    $("#txt_EmailId_2").val("");
    $("#txt_MobileNo").val("");
    $("#ddl_AssistBy").val("");
    $("#ddl_SubAssistBy").val("");
    CompNameValid = true;
    CompNameValid_Msg = "";
    FortuneCodeValid = true;
    FortuneCodeValid_Msg = "";
    UserCodeValid = true;
    UserCodeValid_Msg = "";
    ipAddresses_Wrong = [];
    //$("#View").prop("checked", true);
    //$("#Download").prop("checked", true);
    $("#ChkPwd").prop("checked", false);
    $('#txt_Password').addClass("pwd-field");

    $('#tbl_SubUser #tblbody_SubUser').html("");
    $("#tbl_SubUser").hide();
    $('#txt_Password').on('paste', function (e) {
        e.preventDefault();
        //alert("Pasting is disabled in this textbox.");
    });
}
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57)) {
        //toastr.warning("Please Enter Only Number only.");
        return false;
    }
    return true;
}
function AssistBy_Get() {
    $("#ddl_AssistBy").html("<option value=''>Select</option>")
    $("#ddl_SubAssistBy").html("<option value=''>Select</option>")
    var obj = {};
    obj.UserType = "2";

    $.ajax({
        url: "/User/GetUsers",
        async: false,
        type: "POST",
        data: { req: obj },
        success: function (data, textStatus, jqXHR) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            if (data != null && data.Data.length > 0) {
                for (var k in data.Data) {
                    //if ($("#hdn_UserType").val().includes("2")) {
                    //    if ($("#hdn_UserId").val() == data.Data[k].UserId) {
                    //        $("#ddl_AssistBy").append("<option value=" + data.Data[k].UserId + ">" + data.Data[k].FullName + "</option>");
                    //        $("#ddl_SubAssistBy").append("<option value=" + data.Data[k].UserId + ">" + data.Data[k].FullName + "</option>");
                    //    }
                    //} else {
                    $("#ddl_AssistBy").append("<option value=" + data.Data[k].UserId + ">" + data.Data[k].FullName + "</option>");
                    $("#ddl_SubAssistBy").append("<option value=" + data.Data[k].UserId + ">" + data.Data[k].FullName + "</option>");
                    //}
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
function Check_CompName_Exist() {
    if ($("#txt_CompanyName").val().trim() != "") {
        $.ajax({
            url: '/User/CompName_Exist',
            type: "POST",
            async: false,
            data: { iUserId: $("#hdn_Mng_UserId").val(), CompName: $("#txt_CompanyName").val().trim() },
            success: function (data) {
                if (data != null) {
                    if (data.Status == "-1") {
                        CompNameValid_Msg = data.Message;
                        CompNameValid = false;
                    }
                    else {
                        CompNameValid = true;
                        CompNameValid_Msg = "";
                    }
                }
            }
        });
    }
    else {
        CompNameValid = true;
        CompNameValid_Msg = "";
    }
}
function Check_FortunePartyCode_Exist() {
    if ($("#txt_FortunePartyCode").val().trim() != "") {
        $.ajax({
            url: '/User/FortunePartyCode_Exist',
            type: "POST",
            async: false,
            data: { iUserId: $("#hdn_Mng_UserId").val(), FortunePartyCode: $("#txt_FortunePartyCode").val().trim() },
            success: function (data) {
                if (data != null) {
                    if (data.Status == "-1") {
                        FortuneCodeValid_Msg = data.Message;
                        FortuneCodeValid = false;
                    }
                    else {
                        FortuneCodeValid = true;
                        FortuneCodeValid_Msg = "";
                    }
                }
            }
        });
    }
    else {
        FortuneCodeValid = true;
        FortuneCodeValid_Msg = "";
    }
}
function Check_UserCode_Exist() {
    if ($("#txt_UserCode").val().trim() != "") {
        $.ajax({
            url: '/User/UserCode_Exists',
            type: "POST",
            async: false,
            data: { iUserId: $("#hdn_Mng_UserId").val(), UserCode: $("#txt_UserCode").val().trim() },
            success: function (data) {
                if (data != null) {
                    if (data.Status == "-1") {
                        UserCodeValid_Msg = data.Message;
                        UserCodeValid = false;
                    }
                    else {
                        UserCodeValid = true;
                        UserCodeValid_Msg = "";
                    }
                }
            }
        });
    }
    else {
        UserCodeValid = true;
        UserCodeValid_Msg = "";
    }
}
var checkemail1 = function (valemail) {
    //var forgetfilter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    var forgetfilter = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    if (forgetfilter.test(valemail)) {
        return true;
    }
    else {
        return false;
    }
}
var ErrorMsg = [];
var GetError = function () {
    ErrorMsg = [];

    if ($("#txt_CompanyName").val().trim() == "") {
        ErrorMsg.push({
            'Error': "Please Enter Company Name.",
        });
    }
    else {
        if (CompNameValid == false) {
            ErrorMsg.push({
                'Error': CompNameValid_Msg,
            });
        }
    }

    var usertype = $("#ddl_UserType").val().join(",");

    if (usertype == "") {
        ErrorMsg.push({
            'Error': "Please Select User Type.",
        });
    }

    if (usertype.includes("3")) {
        if ($("#txt_FortunePartyCode").val().trim() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Fortune Party Code.",
            });
        }
        else {
            if (FortuneCodeValid == false) {
                ErrorMsg.push({
                    'Error': FortuneCodeValid_Msg,
                });
            }
        }
    }

    if ($("#txt_FirstName").val().trim() == "") {
        ErrorMsg.push({
            'Error': "Please Enter First Name of Primary User.",
        });
    }

    if ($("#txt_LastName").val().trim() == "") {
        ErrorMsg.push({
            'Error': "Please Enter Last Name of Primary User.",
        });
    }

    if (usertype.includes("3")) {
        if ($("#ddl_AssistBy").val() == "") {
            ErrorMsg.push({
                'Error': "Please Select 1<small style='font- weight: 600;'>st</small> Assist By.",
            });
        }
    }

    if ($("#txt_EmailId").val().trim() == "") {
        ErrorMsg.push({
            'Error': "Please Enter Email Id 1 of Primary User.",
        });
    }
    else {
        if (!checkemail1($("#txt_EmailId").val().trim())) {
            ErrorMsg.push({
                'Error': "Please Enter Valid Email Id 1 Format of Primary User.",
            });
        }
    }

    if ($("#txt_EmailId_2").val().trim() != "") {
        if (!checkemail1($("#txt_EmailId").val().trim())) {
            ErrorMsg.push({
                'Error': "Please Enter Valid Email Id 2 Format of Primary User.",
            });
        }
    }

    if ($("#txt_MobileNo").val().trim() == "") {
        ErrorMsg.push({
            'Error': "Please Enter Mobile No of Primary User.",
        });
    }

    if (usertype.includes("1") || usertype.includes("2")) {
        if ($("#txt_UserCode").val().trim() == "") {
            ErrorMsg.push({
                'Error': "Please Enter User Code.",
            });
        }
        else {
            if (UserCodeValid == false) {
                ErrorMsg.push({
                    'Error': UserCodeValid_Msg,
                });
            }
        }
    }

    if ($("#txt_UserName").val().trim() == "") {
        ErrorMsg.push({
            'Error': "Please Enter User Name of Primary User.",
        });
    }
    else {
        var newlength = $("#txt_UserName").val().trim().length;
        if (newlength < 5) {
            ErrorMsg.push({
                'Error': "Please Enter Minimum 5 Character User Name of Primary User.",
            });
        }
    }

    if (view == "Add") {
        if ($("#txt_Password").val().trim() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Password of Primary User.",
            });
        }
        else {
            var newlength = $("#txt_Password").val().trim().length;
            if (newlength < 6) {
                ErrorMsg.push({
                    'Error': "Please Enter Minimum 6 Character Password of Primary User.",
                });
            }
        }
    }
    else if (view == "Edit") {
        if ($("#hdn_UserType").val().includes("1")) {
            if ($("#txt_Password").val().trim() == "") {
                ErrorMsg.push({
                    'Error': "Please Enter Password of Primary User.",
                });
            }
            else {
                var newlength = $("#txt_Password").val().trim().length;
                if (newlength < 6) {
                    ErrorMsg.push({
                        'Error': "Please Enter Minimum 6 Character Password of Primary User.",
                    });
                }
            }
        }
    }
    
    if (ipAddresses_Wrong.length > 0) {
        ErrorMsg.push({
            'Error': "Invalid Restricted IP : " + ipAddresses_Wrong.join(', '),
        });
    }

    var id = 0;

    $("#tbl_SubUser #tblbody_SubUser tr").each(function () {
        id += 1;

        if ($(this).find('.FirstName').val().trim() == "") {
            ErrorMsg.push({
                'Error': "Please Enter First Name of Sub User " + id + ".",
            });
        }
        if ($(this).find('.LastName').val().trim() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Last Name of Sub User " + id + ".",
            });
        }
        if ($(this).find('.MobileNo').val().trim() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Mobile No. of Sub User " + id + ".",
            });
        }
        if ($(this).find('.EmailId').val().trim() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Email Id of Sub User " + id + ".",
            });
        }
        else {
            if (!checkemail1($(this).find('.EmailId').val().trim())) {
                ErrorMsg.push({
                    'Error': "Please Enter Valid Email Id Format of Sub User " + id + ".",
                });
            }
        }
        if ($(this).find('.UserName').val().trim() == "") {
            ErrorMsg.push({
                'Error': "Please Enter User Name of Sub User " + id + ".",
            });
        }
        else {
            var newlength = $(this).find('.UserName').val().trim().length;
            if (newlength < 5) {
                ErrorMsg.push({
                    'Error': "Please Enter Minimum 5 Character User Name of Sub User " + id + ".",
                });
            }
        }

        if ($(this).find('.Password').val().trim() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Password of Sub User " + id + ".",
            });
        }
        else {
            var newlength = $(this).find('.Password').val().trim().length;
            if (newlength < 6) {
                ErrorMsg.push({
                    'Error': "Please Enter Minimum 6 Character Password of Sub User " + id + ".",
                });
            }
        }
        if ($(this).find('.CPassword').val().trim() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Confirm Password of Sub User " + id + ".",
            });
        }
        else {
            var newlength = $(this).find('.CPassword').val().trim().length;
            if (newlength < 6) {
                ErrorMsg.push({
                    'Error': "Please Enter Minimum 6 Character Confirm Password of Sub User " + id + ".",
                });
            }
        }
        if ($(this).find('.Password').val().trim() != "" && $(this).find('.CPassword').val().trim() != "") {
            if ($(this).find('.Password').val().trim() === $(this).find('.CPassword').val().trim()) {

            } else {
                ErrorMsg.push({
                    'Error': "Please Enter Confirm Password Same as Password of Sub User " + id + ".",
                });
            }
        }

    });

    return ErrorMsg;
}
var SaveCompanyUser = function () {
    Check_CompName_Exist();
    Check_FortunePartyCode_Exist();
    Check_UserCode_Exist();
    setTimeout(function () {
        ErrorMsg = GetError();

        if (ErrorMsg.length > 0) {
            $("#divError").empty();
            ErrorMsg.forEach(function (item) {
                $("#divError").append('<li>' + item.Error + '</li>');
            });
            $("#ErrorModel").modal("show");
        }
        else {
            var List = [];
            $("#tbl_SubUser #tblbody_SubUser tr").each(function () {
                List.push({
                    UserId: $(this).find('.hdn_UserId').val().trim(),
                    FirstName: $(this).find('.FirstName').val().trim(),
                    LastName: $(this).find('.LastName').val().trim(),
                    MobileNo: $(this).find('.MobileNo').val().trim(),
                    EmailId: $(this).find('.EmailId').val().trim(),
                    UserName: $(this).find('.UserName').val().trim(),
                    Password: $(this).find('.Password').val().trim(),
                    SearchStock: $(this).find('.SearchStock').prop("checked"),
                    OrderHistoryAll: $(this).find('.OrderHistoryAll').prop("checked"),
                    OrderHistoryByHisUser: $(this).find('.OrderHistoryByHisUser').prop("checked"),
                    PlaceOrder: $(this).find('.PlaceOrder').prop("checked"),
                    MyCart: $(this).find('.MyCart').prop("checked"),
                    StockDownload: $(this).find('.StockDownload').prop("checked"),
                    OrderHistoryDownload: $(this).find('.OrderHistoryDownload').prop("checked"),
                    OrderHistoryShowPricing: $(this).find('.OrderHistoryShowPricing').prop("checked"),
                });
            });

            var obj = {};
            obj.UserId = $("#hdn_Mng_UserId").val();
            obj.UserName = $("#txt_UserName").val();
            obj.Password = $("#txt_Password").val();
            obj.Active = $("#chk_Active").is(":checked");
            obj.UserType = $("#ddl_UserType").val().join(",");
            obj.FirstName = $("#txt_FirstName").val();
            obj.LastName = $("#txt_LastName").val();
            obj.CompanyName = $("#txt_CompanyName").val();
            obj.FortunePartyCode = $("#txt_FortunePartyCode").val();
            obj.UserCode = $("#txt_UserCode").val();
            obj.EmailId = $("#txt_EmailId").val();
            obj.EmailId_2 = $("#txt_EmailId_2").val();
            obj.MobileNo = $("#txt_MobileNo").val();
            obj.AssistBy = $("#ddl_AssistBy").val();
            obj.SubAssistBy = $("#ddl_SubAssistBy").val();
            //obj.View = ($('#View:checked').val() == undefined ? false : true);
            //obj.Download = ($('#Download:checked').val() == undefined ? false : true);
            obj.View = ($("#ddl_Stock").val().join(",").includes("1") ? true : false);
            obj.Download = ($("#ddl_Stock").val().join(",").includes("2") ? true : false);
            obj.CompanyAddress = $("#txt_CompanyAddress").val();
            obj.RestrictedIP = $("#txt_RestrictedIP").val().trim();

            obj.SubUser = List;

            loaderShow();
            $.ajax({
                url: '/User/SaveUserData',
                type: "POST",
                async: false,
                data: { req: obj },
                success: function (data) {
                    loaderHide();
                    if (data.Status == "1") {
                        toastr.remove();
                        toastr.success(data.Message);
                        Back();
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
    }, 50);
}
function UserTypeGet() {
    $.ajax({
        url: '/User/get_UserType',
        type: "POST",
        success: function (data, textStatus, jqXHR) {
            if (data.Message.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            if (data != null && data.Data.length > 0) {
                $("#ddlUserType").append("<option value=''>Select an Option</option>");
                for (var k in data.Data) {
                    $("#ddlUserType").append("<option value=" + data.Data[k].Id + ">" + data.Data[k].UserType + "</option>");
                }

                for (var k in data.Data) {
                    $("#ddl_UserType").append("<option value=" + data.Data[k].Id + ">" + data.Data[k].UserType + "</option>");
                }
                $(function () {
                    $('#ddl_UserType').multiselect({
                        includeSelectAllOption: true, numberDisplayed: 1
                    });
                });
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
function StockDdlGet() {
    $("#ddl_Stock").html("<option value='1'>View</option>");
    $("#ddl_Stock").append("<option value='2'>Download</option>");
    $(function () {
        $('#ddl_Stock').multiselect({
            includeSelectAllOption: true, numberDisplayed: 1
        });
    });
}
function generate_uuidv4() {
    return 'xxxxxxxx_xxxx_4xxx_yxxx_xxxxxxxxxxxx'.replace(/[xy]/g,
        function (c) {
            var uuid = Math.random() * 16 | 0, v = c == 'x' ? uuid : (uuid & 0x3 | 0x8);
            return uuid.toString(16);
        });
}
var row = $('#tblbody_SubUser').find('tr').length;
var row_cnt = 0;
var All = 'All', SearchStock = 'SearchStock', OrderHistoryAll = 'OrderHistoryAll', OrderHistoryByHisUser = 'OrderHistoryByHisUser',
    PlaceOrder = 'PlaceOrder', MyCart = 'MyCart', StockDownload = 'StockDownload', OrderHistoryDownload = 'OrderHistoryDownload',
    OrderHistoryShowPricing = 'OrderHistoryShowPricing';
function SubUser_AddNewRow(UserId) {
    $("#tbl_SubUser").show();
    row = parseInt(row) + 1;
    var new_id = generate_uuidv4();
    var blank = "";
    var tbl_html =
        '<tr id="tr_' + new_id + '">' +
        '<input type="hidden" class="hdn_UserId" value="' + UserId + '" />' +
        '<td style="width: 50px"><i style="cursor:pointer;" class="error RemoveUser"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;" /></i></td>' +
        '<td class="tblbody_sr">' + row + '</td>' +
        '<td><input type="text" class="form-control common-control FirstName" maxlength="50" autocomplete="off" style="width:180px;"></td>' +
        '<td><input type="text" class="form-control common-control LastName" maxlength="50" autocomplete="off" style="width:180px;"></td>' +
        '<td><input type="text" class="form-control common-control MobileNo" maxlength="15" autocomplete="off" style="width:180px;"></td>' +
        '<td><input type="text" class="form-control common-control EmailId" maxlength="255" autocomplete="off" style="width:180px;"></td>' +
        '<td><input type="text" class="form-control common-control UserName" maxlength="50" autocomplete="off" style="width:180px;"></td>' +
        '<td>' +
        '<input type="text" id="txtPwd_' + new_id + '" class="form-control common-control Password pwd-field" maxlength="50" autocomplete="off" style="width:180px;" >' +
        '<div class="row pwdtick" style="margin-top: -25px;float: left;margin-left: -14px;" >' +
        '<input tabindex="-1" id="Chktick_' + new_id + '" name="Chktick_' + new_id + '" onchange="tick(\'' + blank + '\', \'' + new_id + '\')" class="onpristine onuntouched onvalid onempty chkBx" id="ChkPwd" name="ChkPwd" type="checkbox" value="Pwd" style="cursor: pointer;">' +
        '<div for="Chktick_' + new_id + '" class="offer-label">' +
        '</div>' +
        '</div>' +
        '</td>' +
        '<td><input type="text" id="txtCPwd_' + new_id + '" class="form-control common-control CPassword pwd-field" maxlength="50" autocomplete="off" style="width:180px;"></td>' +
        '<td><center><input class="onpristine onuntouched onvalid onempty chkBx All" onchange="Chkblur(\'' + new_id + '\',\'' + All + '\');" id="ChkAll_' + new_id + '" name="ChkAll_' + new_id + '" type="checkbox" value="IsAll" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
        '<td><center><input class="onpristine onuntouched onvalid onempty chkBx SearchStock" onchange="Chkblur(\'' + new_id + '\',\'' + SearchStock + '\');" id="ChkSearchStock_' + new_id + '" name="ChkSearchStock_' + new_id + '" type="checkbox" value="IsSearchStock" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
        '<td><center><input class="onpristine onuntouched onvalid onempty chkBx StockDownload" onchange="Chkblur(\'' + new_id + '\',\'' + StockDownload + '\');" id="ChkStockDownload_' + new_id + '" name="ChkStockDownload_' + new_id + '" type="checkbox" value="IsStockDownload" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
        '<td><center><input class="onpristine onuntouched onvalid onempty chkBx PlaceOrder" onchange="Chkblur(\'' + new_id + '\',\'' + PlaceOrder + '\');" id="ChkPlaceOrder_' + new_id + '" name="ChkPlaceOrder_' + new_id + '" type="checkbox" value="IsPlaceOrder" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
        '<td><center><input class="onpristine onuntouched onvalid onempty chkBx MyCart" onchange="Chkblur(\'' + new_id + '\',\'' + MyCart + '\');" id="ChkMyCart_' + new_id + '" name="ChkMyCart_' + new_id + '" type="checkbox" value="IsMyCart" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
        '<td><center><input class="onpristine onuntouched onvalid onempty chkBx OrderHistoryAll" onchange="Chkblur(\'' + new_id + '\',\'' + OrderHistoryAll + '\');" id="ChkOrderHistoryAll_' + new_id + '" name="ChkOrderHistoryAll_' + new_id + '" type="checkbox" value="IsOrderHistoryAll" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
        '<td><center><input class="onpristine onuntouched onvalid onempty chkBx OrderHistoryByHisUser" onchange="Chkblur(\'' + new_id + '\',\'' + OrderHistoryByHisUser + '\');" id="ChkOrderHistoryByHisUser_' + new_id + '" name="ChkOrderHistoryByHisUser_' + new_id + '" type="checkbox" value="IsOrderHistoryByHisUser" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
        '<td><center><input class="onpristine onuntouched onvalid onempty chkBx OrderHistoryDownload" onchange="Chkblur(\'' + new_id + '\',\'' + OrderHistoryDownload + '\');" id="ChkOrderHistoryDownload_' + new_id + '" name="ChkOrderHistoryDownload_' + new_id + '" type="checkbox" value="IsOrderHistoryDownload" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
        '<td><center><input class="onpristine onuntouched onvalid onempty chkBx OrderHistoryShowPricing" onchange="Chkblur(\'' + new_id + '\',\'' + OrderHistoryShowPricing + '\');" id="ChkOrderHistoryShowPricing_' + new_id + '" name="ChkOrderHistoryShowPricing_' + new_id + '" type="checkbox" value="IsOrderHistoryShowPricing" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
        '</tr>';

    //if (parseInt($("#tbl_SubUser #tblbody_SubUser").find('tr').length) == 0) {
    $('#tbl_SubUser #tblbody_SubUser').append(tbl_html);
    //}
    //else {
    //    $('#tbl_SubUser #tblbody_SubUser > tr').eq(0).before(tbl_html);
    //}

    if (!($("#hdn_IsPrimaryUser").val() == "True" && $("#hdn_UserType").val().includes("1"))) {
        $(".pwdtick").html("");
    }
    row_cnt = 1;
    row = 1;
    $("#tbl_SubUser #tblbody_SubUser tr").each(function () {
        $(this).find("td:eq(1)").html(row_cnt);
        row_cnt += 1;
        row += 1;
    });
    if (row > 0) {
        row = parseInt(row) - 1;
    }
    $("#ChkAll_" + new_id).prop("checked", true);
    Chkblur(new_id, "All");

    Comman_Input_Valid();
}
function Chkblur(id, type) {
    if (type == "All") {
        var val = false;
        if ($("#ChkAll_" + id).prop("checked") == true) {
            val = true;
        }
        $("#ChkSearchStock_" + id).prop("checked", val);
        $("#ChkOrderHistoryAll_" + id).prop("checked", val);
        $("#ChkOrderHistoryByHisUser_" + id).prop("checked", val);
        $("#ChkPlaceOrder_" + id).prop("checked", val);
        $("#ChkMyCart_" + id).prop("checked", val);
        $("#ChkStockDownload_" + id).prop("checked", val);
        $("#ChkOrderHistoryDownload_" + id).prop("checked", val);
        $("#ChkOrderHistoryShowPricing_" + id).prop("checked", val);
    }
    else {
        if ($("#ChkSearchStock_" + id).prop("checked") == true && $("#ChkOrderHistoryAll_" + id).prop("checked") == true &&
            $("#ChkOrderHistoryByHisUser_" + id).prop("checked") == true && $("#ChkPlaceOrder_" + id).prop("checked") == true &&
            $("#ChkMyCart_" + id).prop("checked") == true && $("#ChkStockDownload_" + id).prop("checked") == true &&
            $("#ChkOrderHistoryDownload_" + id).prop("checked") == true && $("#ChkOrderHistoryShowPricing_" + id).prop("checked") == true) {
            $("#ChkAll_" + id).prop("checked", true);
        }
        else {
            $("#ChkAll_" + id).prop("checked", false);
        }
    }
}
function Get_SubUser(UserId) {
    loaderShow();
    var obj = {};
    obj.UserId = UserId

    $.ajax({
        url: '/User/Get_SubUserMas',
        type: "POST",
        data: { req: obj },
        success: function (data) {
            loaderHide();
            if (data.Status == "1" && data.Message == "SUCCESS" && data.Data.length > 0) {
                $("#tbl_SubUser").show();
                $('#tbl_SubUser #tblbody_SubUser').append("");
                row = $('#tblbody_SubUser').find('tr').length;
                row_cnt = 0;
                var blank = "";
                _(data.Data).each(function (obj, i) {
                    var new_id = generate_uuidv4();
                    var tbl_html =
                        '<tr id="tr_' + new_id + '">' +
                        '<input type="hidden" class="hdn_UserId" value="' + obj.UserId + '" />' +
                        '<td style="width: 50px"><i style="cursor:pointer;" class="error RemoveUser"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;" /></i></td>' +
                        '<td class="tblbody_sr"></td>' +
                        '<td><input value="' + obj.FirstName + '" type="text" class="form-control common-control FirstName" maxlength="50" autocomplete="off" style="width:180px;"></td>' +
                        '<td><input value="' + obj.LastName + '" type="text" class="form-control common-control LastName" maxlength="50" autocomplete="off" style="width:180px;"></td>' +
                        '<td><input value="' + obj.MobileNo + '" type="text" class="form-control common-control MobileNo" maxlength="15" autocomplete="off" style="width:180px;"></td>' +
                        '<td><input value="' + obj.EmailId + '" type="text" class="form-control common-control EmailId" maxlength="255" autocomplete="off" style="width:180px;"></td>' +
                        '<td><input value="' + obj.UserName + '" type="text" class="form-control common-control UserName" maxlength="50" autocomplete="off" style="width:180px;"></td>' +
                        '<td>' +
                        '<input value="' + obj.Password + '" type="text" id="txtPwd_' + new_id + '" class="form-control common-control Password pwd-field" maxlength="50" autocomplete="off" style="width:180px;">' +
                        '<div class="row pwdtick" style="margin-top: -25px;float: left;margin-left: -14px;" >' +
                        '<input tabindex="-1" id="Chktick_' + new_id + '" name="Chktick_' + new_id + '" onchange="tick(\'' + blank + '\', \'' + new_id + '\')" class="onpristine onuntouched onvalid onempty chkBx" id="ChkPwd" name="ChkPwd" type="checkbox" value="Pwd" style="cursor: pointer;">' +
                        '<div for="Chktick_' + new_id + '" class="offer-label">' +
                        '</div>' +
                        '</div>' +
                        '</td>' +
                        '<td><input value="' + obj.Password + '" type="text" id="txtCPwd_' + new_id + '" class="form-control common-control CPassword pwd-field" maxlength="50" autocomplete="off" style="width:180px;"></td>' +
                        '<td><center><input ' + (obj.SearchStock == true && obj.OrderHistoryAll == true && obj.OrderHistoryByHisUser == true && obj.PlaceOrder == true && obj.MyCart == true && obj.StockDownload == true && obj.OrderHistoryDownload == true && obj.OrderHistoryShowPricing == true ? 'checked' : '') + ' class="onpristine onuntouched onvalid onempty chkBx All" onchange="Chkblur(\'' + new_id + '\',\'' + All + '\');" id="ChkAll_' + new_id + '" name="ChkAll_' + new_id + '" type="checkbox" value="IsAll" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
                        '<td><center><input ' + (obj.SearchStock == true ? 'checked' : '') + ' class="onpristine onuntouched onvalid onempty chkBx SearchStock" onchange="Chkblur(\'' + new_id + '\',\'' + SearchStock + '\');" id="ChkSearchStock_' + new_id + '" name="ChkSearchStock_' + new_id + '" type="checkbox" value="IsSearchStock" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
                        '<td><center><input ' + (obj.StockDownload == true ? 'checked' : '') + ' class="onpristine onuntouched onvalid onempty chkBx StockDownload" onchange="Chkblur(\'' + new_id + '\',\'' + StockDownload + '\');" id="ChkStockDownload_' + new_id + '" name="ChkStockDownload_' + new_id + '" type="checkbox" value="IsStockDownload" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
                        '<td><center><input ' + (obj.PlaceOrder == true ? 'checked' : '') + ' class="onpristine onuntouched onvalid onempty chkBx PlaceOrder" onchange="Chkblur(\'' + new_id + '\',\'' + PlaceOrder + '\');" id="ChkPlaceOrder_' + new_id + '" name="ChkPlaceOrder_' + new_id + '" type="checkbox" value="IsPlaceOrder" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
                        '<td><center><input ' + (obj.MyCart == true ? 'checked' : '') + ' class="onpristine onuntouched onvalid onempty chkBx MyCart" onchange="Chkblur(\'' + new_id + '\',\'' + MyCart + '\');" id="ChkMyCart_' + new_id + '" name="ChkMyCart_' + new_id + '" type="checkbox" value="IsMyCart" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
                        '<td><center><input ' + (obj.OrderHistoryAll == true ? 'checked' : '') + ' class="onpristine onuntouched onvalid onempty chkBx OrderHistoryAll" onchange="Chkblur(\'' + new_id + '\',\'' + OrderHistoryAll + '\');" id="ChkOrderHistoryAll_' + new_id + '" name="ChkOrderHistoryAll_' + new_id + '" type="checkbox" value="IsOrderHistoryAll" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
                        '<td><center><input ' + (obj.OrderHistoryByHisUser == true ? 'checked' : '') + ' class="onpristine onuntouched onvalid onempty chkBx OrderHistoryByHisUser" onchange="Chkblur(\'' + new_id + '\',\'' + OrderHistoryByHisUser + '\');" id="ChkOrderHistoryByHisUser_' + new_id + '" name="ChkOrderHistoryByHisUser_' + new_id + '" type="checkbox" value="IsOrderHistoryByHisUser" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
                        '<td><center><input ' + (obj.OrderHistoryDownload == true ? 'checked' : '') + ' class="onpristine onuntouched onvalid onempty chkBx OrderHistoryDownload" onchange="Chkblur(\'' + new_id + '\',\'' + OrderHistoryDownload + '\');" id="ChkOrderHistoryDownload_' + new_id + '" name="ChkOrderHistoryDownload_' + new_id + '" type="checkbox" value="IsOrderHistoryDownload" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
                        '<td><center><input ' + (obj.OrderHistoryShowPricing == true ? 'checked' : '') + ' class="onpristine onuntouched onvalid onempty chkBx OrderHistoryShowPricing" onchange="Chkblur(\'' + new_id + '\',\'' + OrderHistoryShowPricing + '\');" id="ChkOrderHistoryShowPricing_' + new_id + '" name="ChkOrderHistoryShowPricing_' + new_id + '" type="checkbox" value="IsOrderHistoryShowPricing" style="cursor: pointer;width: 50px; height: 15px;"></center></td>' +
                        '</tr>';

                    //if (parseInt($("#tbl_SubUser #tblbody_SubUser").find('tr').length) == 0) {
                    $('#tbl_SubUser #tblbody_SubUser').append(tbl_html);
                    //}
                    //else {
                    //    $('#tbl_SubUser #tblbody_SubUser > tr').eq(0).before(tbl_html);
                    //}
                    if (!($("#hdn_IsPrimaryUser").val() == "True" && $("#hdn_UserType").val().includes("1"))) {
                        $(".pwdtick").html("");
                    }
                });

                $("#tbl_SubUser #tblbody_SubUser tr").each(function () {
                    row_cnt += 1;
                    row += 1;
                    $(this).find("td:eq(1)").html(row_cnt);
                });
                if (row > 0) {
                    row = parseInt(row) - 1;
                }
                Comman_Input_Valid();
            }
        },
        error: function (xhr, textStatus, errorThrown) {
            loaderHide();
        }
    });
}
function Comman_Input_Valid() {
    $('.Password, .CPassword').on('paste', function (e) {
        e.preventDefault();
        //alert("Pasting is disabled in this textbox.");
    });
    $('.Password, .CPassword').on('keypress', function (event) {
        var charCode = event.which || event.keyCode;
        if (charCode == 32) // Blank space
        {
            event.preventDefault();
            return false;
        } else {
            return true;
        }
    });
    $('.MobileNo').on('keypress', function (event) {
        var charCode = event.which || event.keyCode;
        if ((charCode >= 48 && charCode <= 57) || // 0-9
            charCode === 43) //+
        {
            return true;
        } else {
            event.preventDefault();
            return false;
        }
    });
    $('.MobileNo').on('input', function () {
        var sanitizedValue = $(this).val().replace(/[^\d+]/g, ''); // Remove characters that are not digits or +
        $(this).val(sanitizedValue); // Update input value
    });
}
function tick(el, new_id) {
    if (new_id == "") {
        if (el.checked == true) {
            $('#txt_Password').removeClass("pwd-field");
        }
        else {
            $('#txt_Password').addClass("pwd-field");
        }
    }
    else {
        if ($("#Chktick_" + new_id).prop("checked") == true) {
            $('#txtPwd_' + new_id).removeClass("pwd-field");
            $('#txtCPwd_' + new_id).removeClass("pwd-field");
        }
        else {
            $('#txtPwd_' + new_id).addClass("pwd-field");
            $('#txtCPwd_' + new_id).addClass("pwd-field");
        }
    }
}
function ChangeUserType() {
    var usertype = $("#ddl_UserType").val().join(",");

    $("#lbl_assist_by").html('1<small style="font-weight: 600;">st</small> Assist By: ');
    $("#lbl_fortun_code").html('Fortune Party Code: ');
    $("#lbl_user_code").html('User Code: ');

    if (usertype.includes("3")) {
        $("#lbl_assist_by").html('1<small style="font-weight: 600;">st</small> Assist By: <span class="reqvalidation"> * </span>');
        $("#lbl_fortun_code").html('Fortune Party Code: <span class="reqvalidation"> * </span>');
    }
    if (usertype.includes("1") || usertype.includes("2")) {
        $("#lbl_user_code").html('User Code: <span class="reqvalidation"> * </span>');
    }
}
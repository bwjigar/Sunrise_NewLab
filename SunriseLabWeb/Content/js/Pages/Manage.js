var FortuneCodeValid = true;
var FortuneCodeValid_Msg = "";
var UserCodeValid = true;
var UserCodeValid_Msg = "";
var Rowdata = [];

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
    { headerName: "User Name", field: "UserName", tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Password", field: "Password", hide: true },
    { headerName: "FirstName", field: "FirstName", hide: true },
    { headerName: "LastName", field: "LastName", hide: true },
    { headerName: "Customer Name", field: "FullName", tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Company Name", field: "CompName", tooltip: function (params) { return (params.value); }, width: 200 },
    { headerName: "Fortune Party Code", field: "FortunePartyCode", tooltip: function (params) { return (params.data.FortunePartyCode > 0 ? params.data.FortunePartyCode : ""); }, cellRenderer: function (params) { return (params.data.FortunePartyCode > 0 ? params.data.FortunePartyCode : ""); }, width: 75 },
    { headerName: "User Code", field: "UserCode", tooltip: function (params) { return (params.value); }, cellRenderer: function (params) { return (params.data.UserCode > 0 ? params.data.UserCode : ""); }, width: 75 },
    { headerName: "Assist", field: "AssistByName", tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Sub Assist", field: "SubAssistByName", tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Mobile", field: "MobileNo", tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Email Id", field: "EmailId", tooltip: function (params) { return (params.value); }, width: 140 },
    { headerName: "Email Id 2", field: "EmailId_2", tooltip: function (params) { return (params.value); }, width: 140 },
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
        AssistBy_Get();

        $("#hdn_Mng_UserId").val(data[0].UserId);
        $("#txt_UserName").val(data[0].UserName);
        $("#txt_Password").val(data[0].Password);
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
        debugger
        $("#View").prop("checked", data[0].View);
        $("#Download").prop("checked", data[0].Download);

        $(".gridview").hide();
        $(".AddEdit").show();
        $("#btn_AddNew").hide();
        $("#btn_Back").show();
        $("#h2_titl").html("Edit User");
        $("#hdn_Mng_UserId").val(data[0].UserId);
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
                toastr.warning(data.Message, { timeOut: 3000 });
            }
            else {
                ClearRemoveModel();
                GetSearch();
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
}//var DownloadUser = function () {
//    loaderShow();

//    setTimeout(function () {

//        var CountryName = "";
//        var UserName = "";
//        var UserFullName = "";
//        var CompanyName = "";
//        var _FortunePartyCode = "";

//        if ($("#ddlFilterType").val() == "CT") {
//            CountryName = $("#txtCommonName").val();
//        }
//        if ($("#ddlFilterType").val() == "CUN") {
//            UserFullName = $("#txtCommonName").val();
//        }
//        if ($("#ddlFilterType").val() == "UN") {
//            UserName = $("#txtCommonName").val();
//        }
//        if ($("#ddlFilterType").val() == "CM") {
//            CompanyName = $("#txtCommonName").val();
//        }
//        if ($("#ddlFilterType").val() == "FPC") {
//            _FortunePartyCode = $("#txtCommonName").val();
//        }

//        var _FilterType, _FromDate, _ToDate;
//        if ($("#ddlFilterType").val() == "CD" || $("#ddlFilterType").val() == "LAD" || $("#ddlFilterType").val() == "LLD") {
//            _FilterType = $("#ddlFilterType").val();
//            _FromDate = $("#txtFromDate").val();
//            _ToDate = $("#txtToDate").val();
//        }

//        var UserType = $('#ddlUserType').val();
//        var UserStatus = $('#ddlIsActive').val();

//        var FormName = 'Manage User';
//        var ActivityType = 'Excel Export';

//        $.ajax({
//            url: '/User/DownloadUser',
//            async: false,
//            type: "POST",
//            data: {
//                CompanyName: CompanyName,
//                CountryName: CountryName,
//                UserName: UserName,
//                UserFullName: UserFullName,
//                UserType: UserType,
//                UserStatus: UserStatus,
//                IsEmployee: $("#hdn_IsEmployee").val(),
//                SortColumn: SortColumn,
//                SortDirection: SortDirection,
//                PrimaryUser: true,
//                FilterType: _FilterType,
//                FromDate: _FromDate,
//                ToDate: _ToDate,
//                FortunePartyCode: _FortunePartyCode,
//                FormName: FormName,
//                ActivityType: ActivityType
//            },
//            success: function (data) {
//                if (data.indexOf('Something Went wrong') > -1) {
//                    MoveToErrorPage(0);
//                }
//                else if (data.indexOf('No record found') > -1) {
//                    toastr.error(data);
//                }
//                else {
//                    location.href = data;
//                }
//                loaderHide();
//            }
//        });//    }, 15);//}function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".order-title").height(),
        serachHei = $(".order-history-data").height(),
        contentHei = winH - serachHei - navbarHei - 130;
    $("#Cart-Gride").css("height", contentHei);
}$(document).ready(function (e) {
    UserTypeGet();
    GetSearch();
    contentHeight();
    //$('#txt_FortunePartyCode').onFocusout(function () {
    //    Check_FortunePartyCode_Exist();
    //});
    //$('#txt_UserCode').onFocusout(function () {
    //    Check_UserCode_Exist();
    //});
    $("#li_User_Manage").addClass("menuActive");
});

$(window).resize(function () {
    contentHeight();
});

function AddNew() {
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
    $("#chk_Active").prop('checked', true);

    $('#ddl_UserType option:selected').each(function () {
        $(this).prop('selected', false);
    })
    $('#ddl_UserType').multiselect('refresh');

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
    FortuneCodeValid = true;
    FortuneCodeValid_Msg = "";
    UserCodeValid = true;
    UserCodeValid_Msg = "";
    $("#View").prop("checked", true);
    $("#Download").prop("checked", true);
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
                    $("#ddl_AssistBy").append("<option value=" + data.Data[k].UserId + ">" + data.Data[k].FullName + "</option>");
                    $("#ddl_SubAssistBy").append("<option value=" + data.Data[k].UserId + ">" + data.Data[k].FullName + "</option>");
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
function Check_FortunePartyCode_Exist() {
    if ($("#txt_FortunePartyCode").val().replace(' ', '') != "") {
        $.ajax({
            url: '/User/FortunePartyCode_Exist',
            type: "POST",
            data: { iUserId: $("#hdn_Mng_UserId").val(), FortunePartyCode: $("#txt_FortunePartyCode").val() },
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
    if ($("#txt_UserCode").val().replace(' ', '') != "") {
        $.ajax({
            url: '/User/UserCode_Exists',
            type: "POST",
            data: { iUserId: $("#hdn_Mng_UserId").val(), UserCode: $("#txt_UserCode").val() },
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
    if ($("#txt_UserName").val() == "") {
        ErrorMsg.push({
            'Error': "Please Enter User Name.",
        });
    }
    else {
        var newlength = $("#txt_UserName").val().length;

        if (newlength < 5) {
            ErrorMsg.push({
                'Error': "Please Enter Minimum 5 Character UserName.",
            });
        }
    }

    if ($("#txt_Password").val() == "") {
        ErrorMsg.push({
            'Error': "Please Enter Password.",
        });
    }
    else {
        var newlength = $("#txt_Password").val().length;
        if (newlength < 6) {
            ErrorMsg.push({
                'Error': "Please Enter Minimum 6 Character Password.",
            });
        }
    }

    if ($("#txt_FirstName").val() == "") {
        ErrorMsg.push({
            'Error': "Please Enter First Name .",
        });
    }
    if ($("#txt_LastName").val() == "") {
        ErrorMsg.push({
            'Error': "Please Enter Last Name.",
        });
    }
    if ($("#txt_CompanyName").val() == "") {
        ErrorMsg.push({
            'Error': "Please Enter Company Name.",
        });
    }

    if (FortuneCodeValid == false) {
        ErrorMsg.push({
            'Error': FortuneCodeValid_Msg,
        });
    }
    if (UserCodeValid == false) {
        ErrorMsg.push({
            'Error': UserCodeValid_Msg,
        });
    }

    if ($("#txt_EmailId").val() == "") {
        ErrorMsg.push({
            'Error': "Please Enter  Bussiness Email Id .",
        });
    }
    else {
        if (!checkemail1($("#txt_EmailId").val())) {
            ErrorMsg.push({
                'Error': "Please Enter Valid  Email Id Format.",
            });
        }
    }
    if ($("#txt_MobileNo").val() == "") {
        ErrorMsg.push({
            'Error': "Please Enter Mobile No.",
        });
    }

    return ErrorMsg;
}
var SaveCompanyUser = function () {

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
            

            var obj = {};
            obj.UserId = $("#hdn_Mng_UserId").val();
            obj.UserName = $("#txt_UserName").val();
            obj.Password = $("#txt_Password").val();
            obj.Active = $("#chk_Active").is(":checked");
            obj.UserType = $("#ddl_UserType").val().join(",")
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
            obj.View = ($('#View:checked').val() == undefined ? false : true);
            obj.Download = ($('#Download:checked').val() == undefined ? false : true);

            loaderShow();

            $.ajax({
                url: '/User/SaveUserData',
                type: "POST",
                data: { req: obj },
                success: function (data) {
                    loaderHide();
                    if (data.Status == "1") {
                        toastr.success(data.Message);
                        Back();
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
var FortuneCodeValid = true;
var FortuneCodeValid_Msg = "";
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
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57)) {
        //toastr.warning("Please Enter Only Number only.");
        return false;
    }
    return true;
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
    { headerName: "Id", field: "Id", hide: true },
    { headerName: "APIType", field: "APIType", hide: true },
    { headerName: "SupplierHitUrl", field: "SupplierHitUrl", hide: true },
    { headerName: "SupplierResponseFormat", field: "SupplierResponseFormat", hide: true },
    { headerName: "FileLocation", field: "FileLocation", hide: true },
    { headerName: "LocationExportType", field: "LocationExportType", hide: true },
    { headerName: "RepeateveryType", field: "RepeateveryType", hide: true },
    { headerName: "Repeatevery", field: "Repeatevery", hide: true },
    { headerName: "SupplierAPIMethod", field: "SupplierAPIMethod", hide: true },
    { headerName: "UserName", field: "UserName", hide: true },
    { headerName: "Password", field: "Password", hide: true },
    { headerName: "FileName", field: "FileName", hide: true },
    { headerName: "DiscInverse", field: "DiscInverse", hide: true },
    { headerName: "DataGetFrom", field: "DataGetFrom", hide: true },
    { headerName: "Image", field: "Image", hide: true },
    { headerName: "Video", field: "Video", hide: true },
    { headerName: "Certi", field: "Certi", hide: true },
    { headerName: "DocumentViewType", field: "DocumentViewType", hide: true },

    { headerName: "Sr", field: "iSr", tooltip: function (params) { return (params.value); }, sortable: false, width: 40 },
    { headerName: "Action", field: "Action", tooltip: function (params) { return (params.value); }, width: 50, cellRenderer: 'Action', sortable: false },
    { headerName: "Upload Stock", field: "StockUpload", width: 60, cellRenderer: 'StockUpload', sortable: false },
    { headerName: "Last Not Mapped Stock Download", field: "NotMappedStock", width: 120, cellRenderer: 'NotMappedStock', sortable: false },
    { headerName: "Supplier Name", field: "SupplierName", tooltip: function (params) { return (params.value); }, width: 280 },
    { headerName: "Short Name", field: "ShortName", tooltip: function (params) { return (params.value); }, width: 100 },
    { headerName: "API Type", field: "APIType", width: 90, cellRenderer: APIType, sortable: false },
    { headerName: "Auto Upload Stock", field: "AutoUploadStock", width: 120, sortable: false },
    { headerName: "Supplier URL", field: "SupplierURL", width: 630, cellRenderer: SupplierURL, hide: true },
    { headerName: "Active", field: "Active", width: 58, cellRenderer: Status, },
    { headerName: "New RefNo Gen", field: "NewRefNoGenerate", width: 100, cellRenderer: _NewRefNoGenerate },
    { headerName: "Display Image", field: "Image", width: 65, cellRenderer: Status, },
    { headerName: "Display Video", field: "Video", width: 65, cellRenderer: Status, },
    { headerName: "Display Certi", field: "Certi", width: 65, cellRenderer: Status, },
    { headerName: "Last Updated", field: "UpdateDate", width: 130 },
];

function SupplierURL(params) {
    if (params.data.APIType == "WEB_API") {
        return params.data.SupplierURL;
    }
    else {
        return params.data.FileLocation;
    }
}
function _NewRefNoGenerate(params) {
    return params.data.NewRefNoGenerate + (params.data.NewRefNoGenerate == "Common" ? ' <span style="font-weight: 800;">( ' + params.data.NewRefNoCommonPrefix + ' )</span>' : "");
}
function APIType(params) {
    return params.value.replace("_", " ") + (params.data.DataGetFrom == "FILE" ? " (FILE)" : "");
}
function Status(params) {
    if (params.value == true) {
        return "<span class='Yes'> Yes </span>";
    }
    else {
        return "<span class='No'> No </span>";
    }
}
var Action = function (params) {
    var element = "";
    element = '<a title="Edit" onclick="EditView(\'' + params.data.Id + '\')" ><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 18px;cursor:pointer;"></i></a>';
    //element += '&nbsp;&nbsp;<a title="Delete" onclick="DeleteView(\'' + params.data.Id + '\')"><i class="fa fa-trash-o" aria-hidden="true" style="cursor:pointer;"></i></a>';
    return element;
}
var StockUpload = function (params) {
    var element = "";
    if (params.data.DataGetFrom == "WEB_API_FTP" && params.data.Active == true) {
        element = '<a title="Upload Stock" onclick="StockUploadView(\'' + params.data.Id + '\')" ><i class="fa fa-upload" aria-hidden="true" style="font-size: 18px;cursor:pointer;"></i></a>';
    }
    return element;
}
var NotMappedStock = function (params) {
    var element = "";
    element = '<a title="Last Not Mapped Stock Download" onclick="NotMappedStockExcelDownload(\'' + params.data.Id + '\')" ><i class="fa fa-download" aria-hidden="true" style="font-size: 18px;cursor:pointer;"></i></a>';
    return element;
}
function StockUploadView(Id) {
    var obj = {};
    obj.Id = $("#hdn_UserId").val();
    obj.SUPPLIER = Id;

    loaderShow();
    //loaderShow_stk_upload();
    setTimeout(function () {
        $.ajax({
            //url: '/User/AddUpdate_SupplierStock_FromSupplier',
            url: '/User/Thread_AddUpdate_SupplierStock_FromSupplier',
            type: "POST",
            data: { req: obj },
            success: function (data) {
                debugger
                loaderHide();
                toastr.success(data);
                //loaderHide_stk_upload();
                //if (data.Status == "1") {
                //    toastr.success(data.Message);
                //}
                //else {
                //    toastr.error(data.Message);
                //}
            },
            error: function (xhr, textStatus, errorThrown) {
                loaderHide_stk_upload();
            }
        });
    }, 100);
}
function NotMappedStockExcelDownload(Id) {
    loaderShow();
    setTimeout(function () {
        var obj = {};
        obj.SupplierId = Id;
        
        $.ajax({
            url: "/User/Get_Not_Mapped_SupplierStock",
            async: false,
            type: "POST",
            data: { req: obj },
            success: function (data, textStatus, jqXHR) {
                
                loaderHide();
                if (data.search('.xlsx') == -1) {
                    if (data.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
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
function EditView(Id) {
    var data = filterByProperty(Rowdata, "Id", Id);
    if (data.length == 1) {
        
        $("#hdn_Id").val(data[0].Id);
        $("#txtSupplierName").val(data[0].SupplierName);
        $("#DdlRepeatevery").val(data[0].RepeateveryType);
        Repeatevery();
        if ($("#DdlRepeatevery").val() == "Minute") {
            $("#txtMinute").val(data[0].Repeatevery);
        }
        else if ($("#DdlRepeatevery").val() == "Hour") {
            $("#txtHour").val(data[0].Repeatevery);
        }
        document.getElementById("APIStatus").checked = data[0].Active;
        document.getElementById("DiscInverse").checked = data[0].DiscInverse;
        //document.getElementById("NewRefNoGenerate").checked = data[0].NewRefNoGenerate;
        $("#ddlNewRefNoGenerate").val(data[0].NewRefNoGenerate);
        NewRefNoGenerate();
        $("#txtNewRefNoCommonPrefix").val(data[0].NewRefNoCommonPrefix);
        document.getElementById("Image").checked = data[0].Image;
        document.getElementById("Video").checked = data[0].Video;
        document.getElementById("Certi").checked = data[0].Certi;
        $("#DocViewType_Image1").val(data[0].DocViewType_Image1);
        $("#DocViewType_Image2").val(data[0].DocViewType_Image2);
        $("#DocViewType_Image3").val(data[0].DocViewType_Image3);
        $("#DocViewType_Image4").val(data[0].DocViewType_Image4);
        $("#DocViewType_Video").val(data[0].DocViewType_Video);
        $("#DocViewType_Certi").val(data[0].DocViewType_Certi);

        if (data[0].APIType == "WEB_API") {
            document.getElementById("WEB_API").checked = true;
            API_Type = "WEB_API";
            WEBAPI_View();
            $("#txtURL").val(data[0].SupplierURL);
            $("#txtFileName").val(data[0].FileName);
            $("#txtFileLocation").val(data[0].FileLocation);
            $("#txtUserName").val(data[0].UserName);
            $("#txtPassword").val(data[0].Password);
            $("#LocationExportType").val(data[0].LocationExportType);
            $("#ddlAPIResponse").val(data[0].SupplierResponseFormat);
            $("#ddlAPIMethod").val(data[0].SupplierAPIMethod);
        }
        else {
            document.getElementById("FTP").checked = true;
            API_Type = "FTP";
            FTP_View();
            $("#txtFTPFileLocation").val(data[0].FileLocation);
        }

        if (data[0].DataGetFrom == "WEB_API_FTP") {
            document.getElementById("WEB_API_FTP").checked = true;
            DATA_GET_FROM = "WEB_API_FTP";
        }
        else if (data[0].DataGetFrom == "FILE") {
            document.getElementById("FILE").checked = true;
            DATA_GET_FROM = "FILE";
        }

        $("#ImageURL_1").val(data[0].ImageURL_1);
        $("#ImageFormat_1").val(data[0].ImageFormat_1);
        $("#ImageURL_2").val(data[0].ImageURL_2);
        $("#ImageFormat_2").val(data[0].ImageFormat_2);
        $("#ImageURL_3").val(data[0].ImageURL_3);
        $("#ImageFormat_3").val(data[0].ImageFormat_3);
        $("#ImageURL_4").val(data[0].ImageURL_4);
        $("#ImageFormat_4").val(data[0].ImageFormat_4);
        $("#VideoURL").val(data[0].VideoURL);
        $("#VideoFormat").val(data[0].VideoFormat);
        $("#CertiURL").val(data[0].CertiURL);
        $("#CertiFormat").val(data[0].CertiFormat);
        $("#txtShortName").val(data[0].ShortName);
        
        $(".gridview").hide();
        $(".AddEdit").show();
        $("#btn_AddNew").hide();
        $("#btn_Back").show();
        $("#h2_titl").html("Edit Supplier Detail");
    }
}
var DeleteView = function (Id) {
    $("#hdn_Id").val(Id);
    $("#Remove").modal("show");
}

var ClearRemoveModel = function () {
    $("#hdn_Id").val("");
    $("#Remove").modal("hide");
}

var Delete = function () {
    var obj = {};
    obj.Id = $("#hdn_Id").val();

    loaderShow();

    $.ajax({
        url: '/User/Delete_CategoryDet',
        type: "POST",
        data: { req: obj },
        success: function (data) {
            loaderHide();
            if (data.Status == "1") {
                toastr.success(data.Message);
                $("#Remove").modal("hide");
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
            Action: Action,
            StockUpload: StockUpload,
            NotMappedStock: NotMappedStock
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
}
var SortColumn = "";
var SortDirection = "";
const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var obj = {};

        if (params.request.sortModel.length > 0) {
            obj.OrderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort;
        }
        obj.PgNo = PageNo;
        obj.PgSize = "50";
        obj.SupplierName = $("#txt_S_SupplierName").val();

        Rowdata = [];
        $.ajax({
            url: "/User/Get_SupplierMaster",
            async: false,
            type: "POST",
            data: { req: obj },
            success: function (data, textStatus, jqXHR) {
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
                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                Rowdata = [];
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
    $('#txt_S_SupplierName').val('');
    GetSearch();
}


function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".order-title").height(),
        serachHei = $(".order-history-data").height(),
        contentHei = winH - serachHei - navbarHei - 125;
    $("#Cart-Gride").css("height", contentHei);
}
function WEBAPI_View() {
    $(".SP_NM").show();
    $(".SRT_NM").show();
    $(".URL").show();
    $(".FL_NM").show();
    $(".FL_LOC").show();
    $(".FL_LOC_1").hide();
    $(".USR_NM").show();
    $(".PWD").show();
    $(".EX_TYP").show();
    $(".RPT").show();
    $(".API_RES").show();
    $(".API_MTD").show();
    $(".API_STS").show();
    $(".DIS_IVS").show();
    $(".N_RF_GEN").show();
    $(".DATA_GET_FROM").show();
    $(".DIS_IN_GRID_EXL").show();
    $(".DOC_VIEW_TYP").show();
}
function FTP_View() {
    $(".SP_NM").show();
    $(".SRT_NM").show();
    $(".URL").hide();
    $(".FL_NM").hide();
    $(".FL_LOC").hide();
    $(".FL_LOC_1").show();
    $(".USR_NM").hide();
    $(".PWD").hide();
    $(".EX_TYP").hide();
    $(".RPT").show();
    $(".API_RES").hide();
    $(".API_MTD").hide();
    $(".API_STS").show();
    $(".DIS_IVS").show();
    $(".N_RF_GEN").show();
    $(".DATA_GET_FROM").show();
    $(".DIS_IN_GRID_EXL").show();
    $(".DOC_VIEW_TYP").show();
}

var API_Type = "WEB_API", DATA_GET_FROM = "WEB_API_FTP";
$(document).ready(function (e) {
    GetSearch();
    contentHeight();
    $("input[name$='API']").click(function () {
        API_Type = $(this).val();
        if ($(this).val() == "WEB_API") {
            WEBAPI_View();
        }
        else if ($(this).val() == "FTP") {
            FTP_View();
        }
    });
    $("input[name$='DATA_GET_FROM']").click(function () {
        DATA_GET_FROM = $(this).val();
    });
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
    $("#h2_titl").html("Add Supplier Detail");
    $("#hdn_Id").val("");
    document.getElementById("WEB_API").checked = true;
    API_Type = "WEB_API";
    document.getElementById("WEB_API_FTP").checked = true;
    DATA_GET_FROM = "WEB_API_FTP";
    WEBAPI_View();
}
function Back() {
    $(".gridview").show();
    $(".AddEdit").hide();
    $("#btn_AddNew").show();
    $("#btn_Back").hide();
    $("#h2_titl").html("Supplier Master");
    $("#hdn_Id").val("");

    Clear();
    GetSearch();
}
function Clear() {
    API_Type = "WEB_API", DATA_GET_FROM = "WEB_API_FTP";

    $("#txtURL").val("");
    $("#txtSupplierName").val("");
    $("#ddlAPIResponse").val("");
    $("#txtFileName").val("");
    $("#txtFileLocation").val("");
    $("#txtFTPFileLocation").val("");
    $("#LocationExportType").val("");
    $("#DdlRepeatevery").val("Minute");
    Repeatevery();
    $("#ddlAPIMethod").val("");
    document.getElementById("APIStatus").checked = true;
    document.getElementById("DiscInverse").checked = false;
    //document.getElementById("NewRefNoGenerate").checked = false;
    $("#ddlNewRefNoGenerate").val("");
    $("#txtNewRefNoCommonPrefix").hide();
    $("#txtNewRefNoCommonPrefix").val("");
    document.getElementById("Image").checked = true;
    document.getElementById("Video").checked = true;
    document.getElementById("Certi").checked = true;
    $("#txtDocViewType").val("");

    $("#txtUserName").val("");
    $("#txtPassword").val("");

    $("#DocViewType_Image1").val("");
    $("#DocViewType_Image2").val("");
    $("#DocViewType_Image3").val("");
    $("#DocViewType_Image4").val("");
    $("#DocViewType_Video").val("");
    $("#DocViewType_Certi").val("");

    $("#ImageURL_1").val("");
    $("#ImageFormat_1").val("");
    $("#ImageURL_2").val("");
    $("#ImageFormat_2").val("");
    $("#ImageURL_3").val("");
    $("#ImageFormat_3").val("");
    $("#ImageURL_4").val("");
    $("#ImageFormat_4").val("");
    $("#VideoURL").val("");
    $("#VideoFormat").val("");
    $("#CertiURL").val("");
    $("#CertiFormat").val("");
    $("#txtShortName").val("");
}
function Repeatevery() {
    if ($("#DdlRepeatevery").val() == "Minute") {
        $("#txtMinute").val("");
        $("#txtMinute").show();
        $("#txtHour").hide();
    }
    else if ($("#DdlRepeatevery").val() == "Hour") {
        $("#txtHour").val("");
        $("#txtMinute").hide();
        $("#txtHour").show();
    }
}
function NewRefNoGenerate() {
    if ($("#ddlNewRefNoGenerate").val() == "Common") {
        $("#txtNewRefNoCommonPrefix").show();
    }
    else {
        $("#txtNewRefNoCommonPrefix").hide();
    }
}
var ErrorMsg = [];
var GetError = function () {
    ErrorMsg = [];
    if ($("#txtSupplierName").val() == "") {
        ErrorMsg.push({
            'Error': "Please Enter Supplier Name.",
        });
    }

    if ($("#txtShortName").val() == "") {
        ErrorMsg.push({
            'Error': "Please Enter Short Name.",
        });
    }
    

    if (API_Type == "FTP") {
        if ($("#txtFTPFileLocation").val() == "") {
            ErrorMsg.push({
                'Error': "Please Enter File Location.",
            });
        }
    }

    if (API_Type == "WEB_API") {
        if ($("#txtURL").val() == "") {
            ErrorMsg.push({
                'Error': "Please Enter URL.",
            });
        }

        if ($("#txtFileName").val() == "") {
            ErrorMsg.push({
                'Error': "Please Enter File Name.",
            });
        }

        if ($("#txtFileLocation").val() == "") {
            ErrorMsg.push({
                'Error': "Please Enter File Location.",
            });
        }
        if ($("#LocationExportType").val() == "") {
            ErrorMsg.push({
                'Error': "Please Select Export Type.",
            });
        }
    }
    if ($("#DdlRepeatevery").val() == "") {
        ErrorMsg.push({
            'Error': "Please Select Repeat Every.",
        });
    }
    if ($("#DdlRepeatevery").val() == "Minute" && $("#txtMinute").val() == "") {
        ErrorMsg.push({
            'Error': "Please Enter Minute.",
        });
    }
    if ($("#DdlRepeatevery").val() == "Hour" && $("#txtHour").val() == "") {
        ErrorMsg.push({
            'Error': "Please Select Hour.",
        });
    }
    if (API_Type == "WEB_API") {
        if ($("#ddlAPIResponse").val() == "") {
            ErrorMsg.push({
                'Error': "Please Select API Response.",
            });
        }
    }

    if ($("#ddlNewRefNoGenerate").val() == "") {
        ErrorMsg.push({
            'Error': "Please Select New Ref No Generate.",
        });
    }

    if ($("#ddlNewRefNoGenerate").val() == "Common") {
        if ($("#txtNewRefNoCommonPrefix").val() == "") {
            ErrorMsg.push({
                'Error': "Please Enter New Ref No Common Prefix.",
            });
        }
    }

    if ($("#txtDocViewType").val() == "") {
        ErrorMsg.push({
            'Error': "Please Enter Document View Type.",
        });
    }

    if ($("#ImageURL_1").val() != "") {
        if ($("#DocViewType_Image1").val() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Image 1 in Document View Type.",
            });
        }
        if ($("#ImageFormat_1").val() == "") {
            ErrorMsg.push({
                'Error': "Please Select Image Format 1.",
            });
        }
    }
    if ($("#ImageURL_2").val() != "") {
        if ($("#DocViewType_Image2").val() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Image 2 in Document View Type.",
            });
        }
        if ($("#ImageFormat_2").val() == "") {
            ErrorMsg.push({
                'Error': "Please Select Image Format 2.",
            });
        }
    }
    if ($("#ImageURL_3").val() != "") {
        if ($("#DocViewType_Image3").val() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Image 3 in Document View Type.",
            });
        }
        if ($("#ImageFormat_3").val() == "") {
            ErrorMsg.push({
                'Error': "Please Select Image Format 3.",
            });
        }
    }
    if ($("#ImageURL_4").val() != "") {
        if ($("#DocViewType_Image4").val() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Image 4 in Document View Type.",
            });
        }
        if ($("#ImageFormat_4").val() == "") {
            ErrorMsg.push({
                'Error': "Please Select Image Format 4.",
            });
        }
    }
    if ($("#VideoURL").val() != "") {
        if ($("#DocViewType_Video").val() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Video in Document View Type.",
            });
        }
        if ($("#VideoFormat").val() == "") {
            ErrorMsg.push({
                'Error': "Please Select Video Format.",
            });
        }
    }
    if ($("#CertiURL").val() != "") {
        if ($("#DocViewType_Certi").val() == "") {
            ErrorMsg.push({
                'Error': "Please Enter Certificate in Document View Type.",
            });
        }
        if ($("#CertiFormat").val() == "") {
            ErrorMsg.push({
                'Error': "Please Select Certificate Format.",
            });
        }
    }
    return ErrorMsg;
}
var Save = function () {
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
        obj.Id = $("#hdn_Id").val();
        obj.APIType = API_Type;
        obj.SupplierName = $("#txtSupplierName").val();
        obj.RepeateveryType = $("#DdlRepeatevery").val();
        obj.Repeatevery = $('#DdlRepeatevery').val() == "Minute" ? $("#txtMinute").val() : $("#txtHour").val();
        obj.Active = document.getElementById("APIStatus").checked;
        obj.DiscInverse = document.getElementById("DiscInverse").checked;
        //obj.NewRefNoGenerate = document.getElementById("NewRefNoGenerate").checked;
        obj.NewRefNoGenerate = $("#ddlNewRefNoGenerate").val();
        obj.NewRefNoCommonPrefix = ($("#ddlNewRefNoGenerate").val() == "Common" ? $("#txtNewRefNoCommonPrefix").val() : "");
        obj.Image = document.getElementById("Image").checked;
        obj.Video = document.getElementById("Video").checked;
        obj.Certi = document.getElementById("Certi").checked;
        obj.DocViewType_Image1 = $("#DocViewType_Image1").val();
        obj.DocViewType_Image2 = $("#DocViewType_Image2").val();
        obj.DocViewType_Image3 = $("#DocViewType_Image3").val();
        obj.DocViewType_Image4 = $("#DocViewType_Image4").val();
        obj.DocViewType_Video = $("#DocViewType_Video").val();
        obj.DocViewType_Certi = $("#DocViewType_Certi").val();

        obj.DataGetFrom = DATA_GET_FROM;

        if (API_Type == "WEB_API") {
            obj.SupplierURL = $("#txtURL").val();
            obj.SupplierResponseFormat = $("#ddlAPIResponse").val();
            obj.SupplierAPIMethod = $("#ddlAPIMethod").val();
            obj.FileName = $("#txtFileName").val();
            obj.FileLocation = $("#txtFileLocation").val();
            obj.LocationExportType = $("#LocationExportType").val();
            obj.UserName = $("#txtUserName").val();
            obj.Password = $("#txtPassword").val();
        }
        else if (API_Type == "FTP") {
            obj.FileLocation = $("#txtFTPFileLocation").val();
        }

        obj.ImageURL_1 = $("#ImageURL_1").val();
        obj.ImageFormat_1 = $("#ImageFormat_1").val();
        obj.ImageURL_2 = $("#ImageURL_2").val();
        obj.ImageFormat_2 = $("#ImageFormat_2").val();
        obj.ImageURL_3 = $("#ImageURL_3").val();
        obj.ImageFormat_3 = $("#ImageFormat_3").val();
        obj.ImageURL_4 = $("#ImageURL_4").val();
        obj.ImageFormat_4 = $("#ImageFormat_4").val();
        obj.VideoURL = $("#VideoURL").val();
        obj.VideoFormat = $("#VideoFormat").val();
        obj.CertiURL = $("#CertiURL").val();
        obj.CertiFormat = $("#CertiFormat").val();
        obj.ShortName = $("#txtShortName").val();
        

        loaderShow();

        $.ajax({
            url: '/User/AddUpdate_SupplierMaster',
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
}
function validateAlphanumeric(event) {
    var input = event.key;
    var regex = /^[a-zA-Z0-9]+$/;
    if (!regex.test(input)) {
        event.preventDefault();
    }
}
function convertToUppercase(inputId) {
    var inputElement = document.getElementById(inputId);
    inputElement.value = inputElement.value.toUpperCase();
}
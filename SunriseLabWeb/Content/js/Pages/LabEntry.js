function SetCurrentDate() {
    var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
    var d = new Date();
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
    return FinalDate;
}
var pgSize = 50;
function onPageSizeChanged() {
    var value = $("#ddlPagesize").val();
    pgSize = Number(value);
    GetSearch();
}
var showEntryHtml = '<div class="show_entry"><label>'
    + 'Show <select onchange = "onPageSizeChanged()" id = "ddlPagesize">'
    + '<option value="50">50</option>'
    + '<option value="200">200</option>'
    + '<option value="500">500</option>'
    + '<option value="1000">1000</option>'
    + '</select> entries'
    + '</label>'
    + '</div>';

$(document).ready(function () {
    $("#txtLabDate").val(SetCurrentDate());
    Master_Get();
    contentHeight();
});
function Master_Get() {
    loaderShow();

    $("#ddl_User").html("<option value=''>Select</option>");
    var obj = {};
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
                    if (data.Data[k].UserTypeList == "Customer") {
                        $("#ddl_User").append("<option value=" + data.Data[k].UserId + ">" + data.Data[k].FullName + " [" + data.Data[k].UserName + "]" + "</option>");
                    }
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
}

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
function Imag_Video_Certi(params, Img, Vdo, Cert) {
    if (params.data == undefined) {
        return '';
    }

    var image_url = (params.data.Image_URL == null ? "" : params.data.Image_URL);
    var movie_url = (params.data.Video_URL == null ? "" : params.data.Video_URL);
    var certi_url = (params.data.Certificate_URL == null ? "" : params.data.Certificate_URL);

    if (Img == true) {
        if (image_url != "") {
            image_url = '<li><a href="' + image_url + '" target="_blank" title="View Diamond Image">' +
                '<img src="../Content/images/frame.svg" class="frame-icon"></a></li>';
        }
        else {
            image_url = '<li><a href="javascript:void(0);" title="View Diamond Image">' +
                '<img src="../Content/images/image-not-available.svg" class="frame-icon"></a></li>';
        }
    }
    else {
        image_url = "";
    }

    if (Vdo == true) {
        if (movie_url != "") {
            movie_url = '<li><a href="' + movie_url + '" target="_blank" title="View Diamond Video">' +
                '<img src="../Content/images/video-recording.svg" class="frame-icon"></a></li>';
        }
        else {
            movie_url = '<li><a href="javascript:void(0);" title="View Diamond Video">' +
                '<img src="../Content/images/video-recording-not-available.svg" class="frame-icon"></a></li>';
        }
    }
    else {
        movie_url = "";
    }

    //if (Cert == true) {
    //    if (certi_url != "") {
    //        certi_url = '<li><a href="' + certi_url + '" target="_blank" title="View Diamond Certificate">' +
    //            '<img src="../Content/images/medal.svg" class="medal-icon"></a></li>';
    //    }
    //    else {
    //        certi_url = '<li><a href="javascript:void(0);" title="View Diamond Certificate">' +
    //            '<img src="../Content/images/medal-not-available.svg" class="medal-icon"></a></li>';
    //    }
    //}
    //else {
    //    certi_url = "";
    //}

    var data = ('<ul class="flat-icon-ul">' + image_url + movie_url + '</ul>');
    return data;
}
function cellStyle(field, params) {
    if (params.data != undefined) {
        if (params.data.Cut == '3EX' && (field == 'Cut' || field == 'Polish' || field == 'Symm')) {
            return { 'font-size': '11px', 'font-weight': 'bold' };
        }
        else if (field == "Disc" || field == "Value" || field == "SUPPLIER_COST_DISC" || field == "SUPPLIER_COST_VALUE" || field == "MAX_DISC" || field == "MAX_VALUE" ||
            field == "CUSTOMER_COST_DISC" || field == "CUSTOMER_COST_VALUE" || field == "Bid_Disc" || field == "Bid_Amt" || field == "Avg_Stock_Disc" ||
            field == "Avg_Pur_Disc" || field == "Avg_Sales_Disc") {
            //return { 'color': 'red', 'font-weight': 'bold', 'font-size': '11px', 'text-align': 'center' };
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
        else if (field == "Cts" || field == "Rap_Rate" || field == "Rap_Amount" || field == "Base_Price_Cts" || field == "RATIO" || field == "Length" ||
            field == "Width" || field == "Depth" || field == "Depth_Per" || field == "Table_Per" || field == "Crown_Angle" || field == "Pav_Angle" ||
            field == "Crown_Height" || field == "Pav_Height" || field == "Girdle_Per" || field == "RANK" || field == "Avg_Stock_Pcs" || field == "Avg_Pur_Pcs" ||
            field == "Sales_Pcs") {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
        else if (field == "Rank") {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
        else {
            return { 'font-size': '11px', 'text-align': 'center' }
        }
    }
}
function selectAllRendererDetail(params) {
    var cb = document.createElement('input');
    cb.setAttribute('type', 'checkbox');
    cb.setAttribute('id', 'checkboxAll');
    var eHeader = document.createElement('label');
    var eTitle = document.createTextNode(params.colDef.headerName);
    eHeader.appendChild(cb);
    eHeader.appendChild(eTitle);

    cb.addEventListener('change', function (e) {
        if ($(this)[0].checked) {
            if (Filtered_Data.length > 0) {
                gridOptions.api.forEachNodeAfterFilter(function (node) {
                    node.setSelected(true);
                })
            }
            else {
                gridOptions.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
        }
        else {
            params.api.deselectAll();
            var data = [];
            gridOptions_Selected_Calculation(data);
        }

    });

    return eHeader;
}
function onBodyScroll(params) {
    $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');

    $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
        if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
            gridOptions.api.forEachNode(function (node) {
                node.setSelected(false);
            });
        } else {
            gridOptions.api.forEachNode(function (node) {
                node.setSelected(true);
            });
        }
        onSelectionChanged();
    });
}

var columnDefs = [];
columnDefs.push({
    headerName: "", field: "",
    headerCheckboxSelection: true,
    checkboxSelection: true, width: 28,
    suppressSorting: true,
    suppressMenu: true,
    headerCheckboxSelectionFilteredOnly: true,
    headerCellRenderer: selectAllRendererDetail,
    suppressMovable: false
});
columnDefs.push({ headerName: "VIEW", field: "Imag_Video_Certi", width: 65, cellRenderer: function (params) { return Imag_Video_Certi(params, true, true, true); }, suppressSorting: true, suppressMenu: true, sortable: false });
columnDefs.push({ headerName: "Ref No", field: "Ref_No", width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("RefNo", params); } });
columnDefs.push({ headerName: "Shape", field: "Shape", width: 100, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Shape", params); } });
columnDefs.push({ headerName: "Pointer", field: "Pointer", width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Pointer", params); } });
columnDefs.push({ headerName: "Color", field: "Color", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Color", params); } });
columnDefs.push({ headerName: "Clarity", field: "Clarity", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Clarity", params); } });
columnDefs.push({ headerName: "Cts", field: "Cts", width: 70, tooltip: function (params) { return parseFloat(params.value).toFixed(2) }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2) }, cellStyle: function (params) { return cellStyle("Cts", params); } });
columnDefs.push({ headerName: "Cut", field: "Cut", tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Cut", params); } });
columnDefs.push({ headerName: "Polish", field: "Polish", tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Polish", params); } });
columnDefs.push({ headerName: "Symm", field: "Symm", tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Symm", params); } });
columnDefs.push({ headerName: "Fls", field: "Fls", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Fls", params); } });
columnDefs.push({ headerName: "Cert No", field: "Certificate_No", width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Certificate_No", params); } });
columnDefs.push({ headerName: "Supplier Name", field: "SupplierName", width: 230, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("SupplierName", params); } });
columnDefs.push({ headerName: "Supplier Stone Id", field: "Supplier_Stone_Id", width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Supplier_Stone_Id", params); } });
columnDefs.push({ headerName: "Rap Rate($)", field: "Rap_Rate", width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Rap_Rate", params); } });
columnDefs.push({ headerName: "Rap Amount($)", field: "Rap_Amount", width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Rap_Amount", params); } });
columnDefs.push({ headerName: "Supplier Cost Disc(%)", field: "SUPPLIER_COST_DISC", width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("SUPPLIER_COST_DISC", params); } });
columnDefs.push({ headerName: "Supplier Cost Value($)", field: "SUPPLIER_COST_VALUE", width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("SUPPLIER_COST_VALUE", params); } });
columnDefs.push({ headerName: "Offer Disc(%)", field: "CUSTOMER_COST_DISC", width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Disc", params); } });
columnDefs.push({ headerName: "Offer Value($)", field: "CUSTOMER_COST_VALUE", width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Value", params); } });
columnDefs.push({ headerName: "Length", field: "Length", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Length", params); } });
columnDefs.push({ headerName: "Width", field: "Width", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Width", params); } });
columnDefs.push({ headerName: "Depth", field: "Depth", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Depth", params); } });
columnDefs.push({ headerName: "Depth (%)", field: "Depth_Per", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Depth_Per", params); } });
columnDefs.push({ headerName: "Table (%)", field: "Table_Per", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Table_Per", params); } });
columnDefs.push({ headerName: "Key To Symbol", field: "Key_To_Symboll", width: 300, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Key_To_Symboll", params); } });
columnDefs.push({ headerName: "Crown Angle", field: "Crown_Angle", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Crown_Angle", params); } });
columnDefs.push({ headerName: "Crown Height", field: "Crown_Height", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Crown_Height", params); } });
columnDefs.push({ headerName: "Pav Angle", field: "Pav_Angle", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Pav_Angle", params); } });
columnDefs.push({ headerName: "Pav Height", field: "Pav_Height", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Pav_Height", params); } });
columnDefs.push({ headerName: "Table Natts", field: "Table_Natts", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Natts", params); } });
columnDefs.push({ headerName: "Crown Natts", field: "Crown_Natts", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Natts", params); } });
columnDefs.push({ headerName: "Table Inclusion", field: "Table_Inclusion", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Inclusion", params); } });
columnDefs.push({ headerName: "Crown Inclusion", field: "Crown_Inclusion", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Inclusion", params); } });
columnDefs.push({ headerName: "Table Open", field: "Table_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Open", params); } });
columnDefs.push({ headerName: "Girdle Open", field: "Girdle_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Girdle_Open", params); } });
columnDefs.push({ headerName: "Crown Open", field: "Crown_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Open", params); } });
columnDefs.push({ headerName: "Pavilion Open", field: "Pav_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Pav_Open", params); } });




function ddl_User_change() {
    if ($("#ddl_User").val() == "") {
        $(".gridview").hide();
    }
}
var gridOptions = {};
function GetSearch() {
    if ($("#ddl_User").val() != "") {
        $(".gridview").show();
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
            //rowData: data,
            onBodyScroll: onBodyScroll,
            rowModelType: 'serverSide',
            //onGridReady: onGridReady,
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

        $('#Cart-Gride .ag-header-cell[col-id="0"] .ag-header-select-all').click(function () {
            if ($(this).find('.ag-icon').hasClass('ag-icon-checkbox-unchecked')) {
                gridOptions.api.forEachNode(function (node) {
                    node.setSelected(false);
                });
            } else {
                gridOptions.api.forEachNode(function (node) {
                    node.setSelected(true);
                });
            }
            //onSelectionChanged();
        });


    }
    else {
        $(".gridview").hide();
        toastr.error("Please select User");
    }
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
        obj.UserId = $("#ddl_User").val();
        obj.PgNo = PageNo;
        obj.PgSize = pgSize;
        obj.RefNo = $("#txtStoneId").val();

        Rowdata = [];
        $.ajax({
            url: "/User/Get_SearchStock",
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
                    toastr.error("No Data Found", { timeOut: 2500 });
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
function CommaSeperatedStone_list(e) {
    var data = document.getElementById("txtStoneId").value;
    var lines = data.split(' ');
    document.getElementById("txtStoneId").value = lines.join(',');
}
function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".order-title").height(),
        serachHei = $(".order-history-data").height(),
        contentHei = winH - serachHei - navbarHei - 125;
    $("#Cart-Gride").css("height", contentHei);
}
$(window).resize(function () {
    contentHeight();
});
function Reset() {
    $('#ddl_User').val("");
    $('#txtStoneId').val("");
    ddl_User_change();
}


function LabEntry() {
    debugger
    var selectedRows = gridOptions.api.getSelectedRows();
    var list = '';
    var i = 0, tot = selectedRows.length;
    for (; i < tot; i++) {
        list += selectedRows[i].SupplierId + "_" + selectedRows[i].Ref_No + "_" + selectedRows[i].Supplier_Stone_Id + ',';
    }
    list = (list != '' ? list.substr(0, (list.length - 1)) : '');

    debugger
    if (list != "") {
        loaderShow();

        var obj = {};
        obj.UserId = $("#ddl_User").val();
        obj.SupplierId_RefNo_SupplierRefNo = list;

        $.ajax({
            url: '/User/LabEntry_Save',
            type: "POST",
            data: { req: obj },
            success: function (data) {
                debugger
                loaderHide();
                if (data.Status == "1") {
                    $('#PlaceOrderModal').modal('hide');
                    toastr.success(data.Message);
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
    else {
        toastr.error("Please Select atlease One Stone");
    }
}
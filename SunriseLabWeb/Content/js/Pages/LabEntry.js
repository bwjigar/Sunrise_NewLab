var TempData_Array = [];
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
    $('#txtLabDate').daterangepicker({
        singleDatePicker: true,
        startDate: moment(),
        showDropdowns: true,
        locale: {
            separator: "-",
            format: 'DD-MMM-YYYY'
        },
        maxDate: new Date(),
        minYear: parseInt(moment().format('YYYY'), 10) - 10
    });


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
                    if (data.Data[k].UserTypeId.includes("2") || data.Data[k].UserTypeId.includes("3")) {
                        $("#ddl_User").append("<option value=" + data.Data[k].UserId + ">" + data.Data[k].CompName + " [" + data.Data[k].UserName + "]" + "</option>");
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
        else if (field == "Profit" || field == "Profit Amount" || field == "Disc" || field == "Value" || field == "SUPPLIER_COST_DISC" || field == "SUPPLIER_COST_VALUE" || field == "MAX_DISC" || field == "MAX_VALUE" ||
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
function gridOptions_Selected_Calculation(data) {
}
function onBodyScroll(params) {
    $('#Cart-Gride .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');

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
        onSelectionChanged();
    });
}
function onSelectionChanged(event) {
}
function onGridReady(params) {
    if (navigator.userAgent.indexOf('Windows') > -1) {
        this.api.sizeColumnsToFit();
    }
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
columnDefs.push({ headerName: "Ref No", field: "Ref_No", width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("RefNo", params); } });
columnDefs.push({ headerName: "Lab", field: "Lab", width: 50, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Lab", params); }, cellRenderer: function (params) { return Lab(params); } });
columnDefs.push({ headerName: "VIEW", field: "Imag_Video_Certi", width: 65, cellRenderer: function (params) { return Imag_Video_Certi(params, true, true, true); }, suppressSorting: true, suppressMenu: true, sortable: false });
columnDefs.push({ headerName: "Supplier Stone Id", field: "Supplier_Stone_Id", width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Supplier_Stone_Id", params); } });
columnDefs.push({ headerName: "Certificate No", field: "Certificate_No", width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Certificate_No", params); } });
columnDefs.push({ headerName: "Supplier Name", field: "SupplierName", width: 230, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("SupplierName", params); } });
columnDefs.push({ headerName: "Company Name", field: "CompName", width: 190, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("CompName", params); } });
columnDefs.push({
    headerName: "QC Require",
    field: "QC_Require",
    width: 135,
    sortable: false,
    cellRenderer: 'input_QC_Require_Indicator'
});
columnDefs.push({
    headerName: "Lab Status",
    field: "Lab_Status",
    width: 130,
    sortable: false,
    cellRenderer: 'input_Lab_Status_Indicator'
});
columnDefs.push({ headerName: "Shape", field: "Shape", width: 100, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Shape", params); } });
columnDefs.push({ headerName: "Pointer", field: "Pointer", width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Pointer", params); } });
columnDefs.push({ headerName: "BGM", field: "BGM", width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("BGM", params); } });
columnDefs.push({ headerName: "Color", field: "Color", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Color", params); } });
columnDefs.push({ headerName: "Clarity", field: "Clarity", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Clarity", params); } });
columnDefs.push({ headerName: "Cts", field: "Cts", width: 70, tooltip: function (params) { return parseFloat(params.value).toFixed(2) }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2) }, cellStyle: function (params) { return cellStyle("Cts", params); } });
columnDefs.push({ headerName: "Rap Rate($)", field: "Rap_Rate", width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Rap_Rate", params); } });
columnDefs.push({ headerName: "Rap Amount($)", field: "Rap_Amount", width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Rap_Amount", params); } });
columnDefs.push({
    headerName: "Supplier Cost Disc(%)",
    field: "SUPPLIER_COST_DISC",
    width: 105,
    sortable: false,
    cellRenderer: 'input_SUPPLIER_COST_DISC_Indicator'
});
columnDefs.push({
    headerName: "Supplier Cost Value($)",
    field: "SUPPLIER_COST_VALUE",
    width: 115,
    sortable: false,
    cellRenderer: 'input_SUPPLIER_COST_VALUE_Indicator'
});
columnDefs.push({
    headerName: "Final Sale Disc(%)",
    field: "CUSTOMER_COST_DISC",
    width: 110,
    sortable: false,
    cellRenderer: 'input_CUSTOMER_COST_DISC_Indicator'
});
columnDefs.push({
    headerName: "Final Sale Amt US($)",
    field: "CUSTOMER_COST_VALUE",
    width: 110,
    sortable: false,
    cellRenderer: 'input_CUSTOMER_COST_VALUE_Indicator'
});
columnDefs.push({
    headerName: "Profit(%)",
    field: "PROFIT",
    width: 110,
    sortable: false,
    cellRenderer: 'input_PROFIT_Indicator'
});
columnDefs.push({
    headerName: "Profit Amount($)",
    field: "PROFIT_AMOUNT",
    width: 110,
    sortable: false,
    cellRenderer: 'input_PROFIT_AMOUNT_Indicator'
});
columnDefs.push({ headerName: "Supplier Base Offer Disc(%)", field: "Disc", width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Disc", params); } });
columnDefs.push({ headerName: "Supplier Base Value($)", field: "Value", width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Value", params); } });
columnDefs.push({ headerName: "Cut", field: "Cut", tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Cut", params); } });
columnDefs.push({ headerName: "Polish", field: "Polish", tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Polish", params); } });
columnDefs.push({ headerName: "Symm", field: "Symm", tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Symm", params); } });
columnDefs.push({ headerName: "Fls", field: "Fls", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Fls", params); } });
columnDefs.push({ headerName: "Length", field: "Length", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Length", params); } });
columnDefs.push({ headerName: "Width", field: "Width", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Width", params); } });
columnDefs.push({ headerName: "Depth", field: "Depth", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Depth", params); } });
columnDefs.push({ headerName: "Depth (%)", field: "Depth_Per", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Depth_Per", params); } });
columnDefs.push({ headerName: "Table (%)", field: "Table_Per", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellStyle: function (params) { return cellStyle("Table_Per", params); } });
columnDefs.push({ headerName: "Key To Symbol", field: "Key_To_Symboll", width: 300, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Key_To_Symboll", params); } });
columnDefs.push({ headerName: "Girdle(%)", field: "Girdle_Per", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Girdle_Per", params); } });
columnDefs.push({ headerName: "Crown Angle", field: "Crown_Angle", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Crown_Angle", params); } });
columnDefs.push({ headerName: "Crown Height", field: "Crown_Height", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Crown_Height", params); } });
columnDefs.push({ headerName: "Pav Angle", field: "Pav_Angle", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Pav_Angle", params); } });
columnDefs.push({ headerName: "Pav Height", field: "Pav_Height", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Pav_Height", params); } });
columnDefs.push({ headerName: "Table Natts", field: "Table_Natts", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Natts", params); } });
columnDefs.push({ headerName: "Crown Natts", field: "Crown_Natts", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Natts", params); } });
columnDefs.push({ headerName: "Table Inclusion", field: "Table_Inclusion", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Inclusion", params); } });
columnDefs.push({ headerName: "Crown Inclusion", field: "Crown_Inclusion", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Inclusion", params); } });
columnDefs.push({ headerName: "Table Open", field: "Table_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Open", params); } });
columnDefs.push({ headerName: "Crown Open", field: "Crown_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Open", params); } });
columnDefs.push({ headerName: "Pavilion Open", field: "Pav_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Pav_Open", params); } });
columnDefs.push({ headerName: "Girdle Open", field: "Girdle_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Girdle_Open", params); } });
columnDefs.push({ headerName: "Culet", field: "Culet", width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Culet", params); } });



function ddl_User_change() {
    $(".gridview").hide();
    $("#li_LabEntry").hide();
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
            masterDetail: true,
            detailCellRenderer: 'myDetailCellRenderer',
            detailRowHeight: 70,
            groupDefaultExpanded: 0,
            components: {
                input_QC_Require_Indicator: input_QC_Require_Indicator,
                input_Lab_Status_Indicator: input_Lab_Status_Indicator,
                input_SUPPLIER_COST_DISC_Indicator: input_SUPPLIER_COST_DISC_Indicator,
                input_SUPPLIER_COST_VALUE_Indicator: input_SUPPLIER_COST_VALUE_Indicator,
                input_CUSTOMER_COST_DISC_Indicator: input_CUSTOMER_COST_DISC_Indicator,
                input_CUSTOMER_COST_VALUE_Indicator: input_CUSTOMER_COST_VALUE_Indicator,
                input_PROFIT_Indicator: input_PROFIT_Indicator,
                input_PROFIT_AMOUNT_Indicator: input_PROFIT_AMOUNT_Indicator,
                myDetailCellRenderer: DetailCellRenderer
            },
            defaultColDef: {
                enableSorting: true,
                sortable: true,
                resizable: true
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
            onRowSelected: onSelectionChanged,
            onBodyScroll: onBodyScroll,
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
            onSelectionChanged();
        });
    }
    else {
        $(".gridview").hide();
        toastr.warning("Please Select User");
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
            url: "/User/Get_LabEntry",
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
                    $("#li_LabEntry").show();

                    TempData_Array = [];
                    for (var i = 0; i < data.Data.length; i++) {
                        var SUPPLIER_COST_DISC = (data.Data[i].SUPPLIER_COST_DISC != "" ? parseFloat(data.Data[i].SUPPLIER_COST_DISC).toFixed(2) : "0");
                        var SUPPLIER_COST_VALUE = (data.Data[i].SUPPLIER_COST_VALUE != "" ? parseFloat(data.Data[i].SUPPLIER_COST_VALUE).toFixed(2) : "0")
                        var CUSTOMER_COST_DISC = (data.Data[i].CUSTOMER_COST_DISC != "" ? parseFloat(data.Data[i].CUSTOMER_COST_DISC).toFixed(2) : "0")
                        var CUSTOMER_COST_VALUE = (data.Data[i].CUSTOMER_COST_VALUE != "" ? parseFloat(data.Data[i].CUSTOMER_COST_VALUE).toFixed(2) : "0")
                        var PROFIT = ((parseFloat(CUSTOMER_COST_VALUE) - parseFloat(SUPPLIER_COST_VALUE)) * 100) / parseFloat(SUPPLIER_COST_VALUE);
                        var PROFIT_AMOUNT = parseFloat(CUSTOMER_COST_VALUE) - parseFloat(SUPPLIER_COST_VALUE);
                        PROFIT = (PROFIT != "" ? parseFloat(PROFIT).toFixed(2) : "0");
                        PROFIT_AMOUNT = (PROFIT_AMOUNT != "" ? parseFloat(PROFIT_AMOUNT).toFixed(2) : "0");

                        TempData_Array.push([data.Data[i].Ref_No, data.Data[i].SupplierId, "", (data.Data[i].LabEntry_Status != null ? data.Data[i].LabEntry_Status : ""), SUPPLIER_COST_DISC, SUPPLIER_COST_VALUE, CUSTOMER_COST_DISC, CUSTOMER_COST_VALUE, PROFIT, PROFIT_AMOUNT]);
                    }
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
    $("#file_upload").val("");
    $("#li_LabEntry").hide();
    ddl_User_change();
}


var LabEntry_List = [];
function LabEntry() {
    LabEntry_List = [];
    LabEntry_StatusBlank = "";
    if ($("#ddl_User").val() != "") {
        var selectedRows = gridOptions.api.getSelectedRows();
        if (selectedRows.length > 0) {
            var list = '', labCount = 0;
            for (var i = 0; i < selectedRows.length; i++) {
                for (var j = 0; j < TempData_Array.length; j++) {
                    if (selectedRows[i].Ref_No == TempData_Array[j][0] && selectedRows[i].SupplierId == TempData_Array[j][1]) {
                        LabEntry_List.push({
                            LabDate: $("#txtLabDate").val(),
                            UserId: $("#ddl_User").val(),
                            Ref_No: TempData_Array[j][0],
                            SupplierId: TempData_Array[j][1],
                            QC_Require: (TempData_Array[j][2] == "" ? "Regular" : TempData_Array[j][2]),
                            LabEntry_Status: TempData_Array[j][3],
                            SUPPLIER_COST_DISC: TempData_Array[j][4],
                            SUPPLIER_COST_VALUE: TempData_Array[j][5],
                            CUSTOMER_COST_DISC: TempData_Array[j][6],
                            CUSTOMER_COST_VALUE: TempData_Array[j][7],
                            PROFIT: TempData_Array[j][8],
                            PROFIT_AMOUNT: TempData_Array[j][9],
                        });

                        debugger
                        if (TempData_Array[j][3] == "") {
                            debugger
                            labCount += parseInt(labCount) + 1;
                            LabEntry_StatusBlank += TempData_Array[j][0] + ", ";
                        }
                    }
                }
            }
            debugger
            
            if (labCount > 0) {
                debugger
                //return toastr.warning("Please Select Lab Status");

                LabEntry_StatusBlank = LabEntry_StatusBlank.slice(0, -2);

                var final_msg = ''
                final_msg = '<label class="offerComment" style="word-break:break-word;">Missing Lab Status For Following Ref No !!</label>';
                final_msg += '<br><label class="offerComment" style="word-break:break-word;">' + LabEntry_StatusBlank + '</label>';
                $("#Excel_Stone_Invalid_Modal .form-group").html(final_msg);
                $('#Excel_Stone_Invalid_Modal').modal('show');
            }
            else {

                var msg = "<label class='offerComment' style='word -break: break-word;'>Are you sure you want to Add Below Ref No in Lab Entry ?</label>"
                msg += "<table border='1' style='font-size:12px; width:100%; margin-top:5px; display:block; max-height:360px; overflow-y:auto;'>";
                msg += "<tbody>";
                msg += "<tr>";
                msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 4%;\"><center><b>No.</b></center></td>";
                msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 15%;\"><center><b>Ref No</b></center></td>";
                msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 10%;\"><center><b>QC Require</b></center></td>";
                msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 10%;\"><center><b>Lab Status</b></center></td>";
                msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 8%;\"><center><b>Supplier Cost Disc(%)</b></center></td>";
                msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 10%;\"><center><b>Supplier Cost Value($)</b></center></td>";
                msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 8%;\"><center><b>Final Sale Disc(%)</b></center></td>";
                msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 10%;\"><center><b>Final Sale Amt US($)</b></center></td>";
                msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 8%;\"><center><b>Profit(%)</b></center></td>";
                msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 10%;\"><center><b>Profit Amount($)</b></center></td>";
                msg += "</tr>";

                for (var q = 0; q < LabEntry_List.length; q++) {
                    msg += "<tr>";
                    msg += "<td><center><b>" + (parseInt(q) + 1) + "</b></center></td>";
                    msg += "<td><center>" + LabEntry_List[q].Ref_No + "</center></td>";
                    msg += "<td><center>" + LabEntry_List[q].QC_Require + "</center></td>";
                    msg += "<td><center>" + LabEntry_List[q].LabEntry_Status + "</center></td>";
                    msg += "<td><center>" + formatNumber(LabEntry_List[q].SUPPLIER_COST_DISC) + "</center></td>";
                    msg += "<td style='color: #003d66;font-weight:600'><center>" + formatNumber(LabEntry_List[q].SUPPLIER_COST_VALUE) + "</center></td>";
                    msg += "<td><center>" + formatNumber(LabEntry_List[q].CUSTOMER_COST_DISC) + "</center></td>";
                    msg += "<td style='color: #003d66;font-weight:600'><center>" + formatNumber(LabEntry_List[q].CUSTOMER_COST_VALUE) + "</center></td>";
                    msg += "<td><center>" + formatNumber(LabEntry_List[q].PROFIT) + "</center></td>";
                    msg += "<td style='color: #003d66;font-weight:600'><center>" + formatNumber(LabEntry_List[q].PROFIT_AMOUNT) + "</center></td>";
                    msg += "</tr>";
                }
                msg += "</tbody>";
                msg += "</table>";

                $("#LabEntry_Modal #divList").html(msg);
                $('#LabEntry_Modal').modal('show');
            }
        }
        else {
            toastr.warning("Please Select atleast One Stone");
        }
    }
    else {
        $(".gridview").hide();
        toastr.warning("Please Select User");
    }
}
function Save_LabEntry() {
    if (LabEntry_List.length > 0) {
        loaderShow();

        var obj = {};
        obj.LabEntry_List = LabEntry_List;

        $.ajax({
            url: '/User/Save_LabEntry',
            type: "POST",
            data: { req: obj },
            success: function (data) {
                loaderHide();
                if (data.Status == "1") {
                    $('#LabEntry_Modal').modal('hide');
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
}
function Excel_LabEntry() {
    if (gridOptions.api != undefined) {
        loaderShow();
        setTimeout(function () {
            var selectedRows = gridOptions.api.getSelectedRows();
            var list = '';
            var i = 0, tot = selectedRows.length;
            for (; i < tot; i++) {
                list += selectedRows[i].SupplierId + "_" + selectedRows[i].Ref_No + "_" + selectedRows[i].Supplier_Stone_Id + ',';
            }
            list = (list != '' ? list.substr(0, (list.length - 1)) : '');

            var obj = {};
            obj.UserId = $("#ddl_User").val();
            obj.SupplierId_RefNo_SupplierRefNo = list;
            obj.RefNo = (list == "" ? $("#txtStoneId").val() : "");

            $.ajax({
                url: "/User/Excel_LabEntry",
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
}





function UploadExcelFile() {
    if ($("#ddl_User").val() != "") {
        var file = document.getElementById('file_upload').files[0];
        if (file == undefined) {
            return toastr.warning("Please Select Excel File For Upload");
        }

        loaderShow();

        const formData = new FormData();
        formData.append('UserId', $("#ddl_User").val());
        formData.append("file", file);


        $.ajax({
            url: "/User/UploadExcelforLabEntry",
            processData: false,
            contentType: false,
            type: "POST",
            data: formData,
            success: function (data, textStatus, jqXHR) {
                var Culet = data[data.length - 1].Culet;

                if (Culet == "0") {
                    return toastr.warning(data[data.length - 1].Lab_Comments);
                }
                else {
                    var Invalid_Stone_Body = data[data.length - 1].Lab_Comments;
                    data.splice(data.length - 1);

                    loaderHide();

                    var gridDiv = document.querySelector('#Cart-Gride');
                    if (gridOptions.api != undefined) {
                        gridOptions.api.destroy();
                    }
                    gridOptions = {
                        masterDetail: true,
                        detailCellRenderer: 'myDetailCellRenderer',
                        detailRowHeight: 70,
                        groupDefaultExpanded: 0,
                        components: {
                            input_QC_Require_Indicator: input_QC_Require_Indicator,
                            input_Lab_Status_Indicator: input_Lab_Status_Indicator,
                            input_SUPPLIER_COST_DISC_Indicator: input_SUPPLIER_COST_DISC_Indicator,
                            input_SUPPLIER_COST_VALUE_Indicator: input_SUPPLIER_COST_VALUE_Indicator,
                            input_CUSTOMER_COST_DISC_Indicator: input_CUSTOMER_COST_DISC_Indicator,
                            input_CUSTOMER_COST_VALUE_Indicator: input_CUSTOMER_COST_VALUE_Indicator,
                            input_PROFIT_Indicator: input_PROFIT_Indicator,
                            input_PROFIT_AMOUNT_Indicator: input_PROFIT_AMOUNT_Indicator,
                            myDetailCellRenderer: DetailCellRenderer
                        },
                        defaultColDef: {
                            enableSorting: true,
                            sortable: true,
                            resizable: true
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
                        onSelectionChanged: onSelectionChanged,
                        columnDefs: columnDefs,
                        rowData: data,
                        cacheBlockSize: pgSize,
                        paginationPageSize: pgSize,
                        getContextMenuItems: getContextMenuItems,
                        paginationNumberFormatter: function (params) {
                            return '[' + params.value.toLocaleString() + ']';
                        }
                    };
                    $("#file_upload").val("");

                    var gridDiv = document.querySelector('#Cart-Gride');
                    new agGrid.Grid(gridDiv, gridOptions);

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
                        onSelectionChanged();
                    });

                    setInterval(function () {
                        $(".ag-header-cell-text").addClass("grid_prewrap");
                    }, 30);


                    if (Invalid_Stone_Body != '' && Invalid_Stone_Body != 'undefined') {
                        Excel_Invalid_RefNo(Invalid_Stone_Body);
                    }

                    $("#li_LabEntry").show();
                    TempData_Array = [];
                    for (var i = 0; i < data.length; i++) {
                        var SUPPLIER_COST_DISC = (data[i].SUPPLIER_COST_DISC != "" ? parseFloat(data[i].SUPPLIER_COST_DISC).toFixed(2) : "0");
                        var SUPPLIER_COST_VALUE = (data[i].SUPPLIER_COST_VALUE != "" ? parseFloat(data[i].SUPPLIER_COST_VALUE).toFixed(2) : "0")
                        var CUSTOMER_COST_DISC = (data[i].CUSTOMER_COST_DISC != "" ? parseFloat(data[i].CUSTOMER_COST_DISC).toFixed(2) : "0")
                        var CUSTOMER_COST_VALUE = (data[i].CUSTOMER_COST_VALUE != "" ? parseFloat(data[i].CUSTOMER_COST_VALUE).toFixed(2) : "0")
                        var PROFIT = ((parseFloat(CUSTOMER_COST_VALUE) - parseFloat(SUPPLIER_COST_VALUE)) * 100) / parseFloat(SUPPLIER_COST_VALUE);
                        var PROFIT_AMOUNT = parseFloat(CUSTOMER_COST_VALUE) - parseFloat(SUPPLIER_COST_VALUE);
                        PROFIT = (PROFIT != "" ? parseFloat(PROFIT).toFixed(2) : "0");
                        PROFIT_AMOUNT = (PROFIT_AMOUNT != "" ? parseFloat(PROFIT_AMOUNT).toFixed(2) : "0");

                        var QCRequire = (data[i].QCRequire != null ? data[i].QCRequire : "");
                        var LabEntry_Status = (data[i].LabEntry_Status != null ? data[i].LabEntry_Status : "");

                        TempData_Array.push([data[i].Ref_No, data[i].SupplierId, QCRequire, LabEntry_Status, SUPPLIER_COST_DISC, SUPPLIER_COST_VALUE, CUSTOMER_COST_DISC, CUSTOMER_COST_VALUE, PROFIT, PROFIT_AMOUNT]);
                    }
                    if (TempData_Array.length > 0) {
                        $(".gridview").show();
                    }
                    else {
                        $(".gridview").hide();
                    }
                }

                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                loaderHide();
            }
        });
    }
    else {
        $(".gridview").hide();
        toastr.warning("Please Select User");
    }
}
function Excel_Invalid_RefNo(Body) {
    var str = Body;
    str = str.replace(/&lt;/g, '<');
    str = str.replace(/&gt;/g, '>');

    var final_msg = ''
    final_msg = '<label class="offerComment" style="word-break:break-word;">Below Ref No are Not Found !!</label>';
    final_msg += str;
    $("#Excel_Stone_Invalid_Modal .form-group").html(final_msg);
    $('#Excel_Stone_Invalid_Modal').modal('show');
}
function CloseExcel_Stone_InvalidPopup() {
    $('#Excel_Stone_Invalid_Modal').modal('hide');
}
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function isNumberKeyWithNegative(evt) {

    var charCode = (evt.which) ? evt.which : evt.keyCode;

    if (charCode == 45)
        return true;

    if (charCode != 46 && charCode > 31
        && (charCode < 48 || charCode > 57))
        return false;

    return true;
}

function input_QC_Require_Indicator(params) {
    var QC_Require = '';
    if (TempData_Array.length > 0) {
        for (var i = 0; i < TempData_Array.length; i++) {
            if (TempData_Array[i][0] == params.data.Ref_No && TempData_Array[i][1] == params.data.SupplierId) {
                QC_Require = TempData_Array[i][2];
            }
        }
    }
    var element = document.createElement("span");
    element.title = 'QC Require';
    element.innerHTML = '<input type="text" style="text-align: center;width: 120px;" class="input-inc QC_Require" value = "' + QC_Require
        + '" Ref_No = "' + params.data.Ref_No
        + '" SupplierId = "' + params.data.SupplierId
        + '" onblur="QC_Require(this);">';
    return element;
}
function QC_Require(e) {
    var Ref_No = $(e).attr("Ref_No");
    var SupplierId = $(e).attr("SupplierId");
    var QC_Require = $(e).val();

    TEMP_SAVE("QC_REQUIRE", Ref_No, SupplierId, QC_Require, "", "", "", "", "", "", "", "");
}
function input_Lab_Status_Indicator(params) {
    var LabStatus = '';
    if (TempData_Array.length > 0) {
        for (var i = 0; i < TempData_Array.length; i++) {
            if (TempData_Array[i][0] == params.data.Ref_No && TempData_Array[i][1] == params.data.SupplierId) {
                LabStatus = TempData_Array[i][3];
            }
        }
    }
    var element = document.createElement("span");
    element.title = 'Lab Status';
    element.innerHTML = '<select class="input-inc LabStatus" style="width: 100px;"'
        + '" Ref_No = "' + params.data.Ref_No
        + '" SupplierId = "' + params.data.SupplierId
        + '" onblur="LabStatus(this);">'
        + '<option ' + ((LabStatus == "") ? 'selected' : '') +' value="">Select</option>'
        + '<option ' + ((LabStatus == "Confirm") ? 'selected' : '') +' value="Confirm">Confirm</option>'
        + '<option ' + ((LabStatus == "Hold") ? 'selected' : '') +' value="Hold">Hold</option>'
        + '<option ' + ((LabStatus == "Bidded") ? 'selected' : '') +' value="Bidded">Bidded</option>'
        + '<option ' + ((LabStatus == "Waiting") ? 'selected' : '') +' value="Waiting">Waiting</option>'
        + '<option ' + ((LabStatus == "Qc Pending") ? 'selected' : '') +' value="Qc Pending">Qc Pending</option>'
        + '<option ' + ((LabStatus == "Qc Reject") ? 'selected' : '') +' value="Qc Reject">Qc Reject</option>'
        + '<option ' + ((LabStatus == "Bid Reject") ? 'selected' : '') +' value="Bid Reject">Bid Reject</option>'
        + '<option ' + ((LabStatus == "Sold") ? 'selected' : '') +' value="Sold">Sold</option>'
        + '<option ' + ((LabStatus == "Transit") ? 'selected' : '') +' value="Transit">Transit</option>'
        + '<option ' + ((LabStatus == "Busy") ? 'selected' : '') +' value="Busy">Busy</option>'
        + '<option ' + ((LabStatus == "Cancel") ? 'selected' : '') +' value="Cancel">Cancel</option>'
        + '<option ' + ((LabStatus == "Other") ? 'selected' : '') +' value="Other">Other</option>'
        + '</select>'
    return element;
}
function LabStatus(e) {
    var Ref_No = $(e).attr("Ref_No");
    var SupplierId = $(e).attr("SupplierId");
    var LabStatus = $(e).val();

    TEMP_SAVE("LAB_STATUS", Ref_No, SupplierId, "", LabStatus, "", "", "", "", "", "");
}


function input_SUPPLIER_COST_DISC_Indicator(params) {
    var SUPPLIER_COST_DISC = '';
    if (TempData_Array.length > 0) {
        for (var i = 0; i < TempData_Array.length; i++) {
            if (TempData_Array[i][0] == params.data.Ref_No && TempData_Array[i][1] == params.data.SupplierId) {
                SUPPLIER_COST_DISC = formatNumber(TempData_Array[i][4]);
            }
        }
    }

    var element = document.createElement("span");
    element.title = 'Supplier Cost Disc(%)';
    element.innerHTML = '<label style="text-align: center;color: #003d66;font-size: 11px;text-align:center;font-weight:600;" class="input-inc SUPPLIER_COST_DISC">' + SUPPLIER_COST_DISC + '</label>';
    return element;
}

function input_SUPPLIER_COST_VALUE_Indicator(params) {
    var SUPPLIER_COST_VALUE = '';
    if (TempData_Array.length > 0) {
        for (var i = 0; i < TempData_Array.length; i++) {
            if (TempData_Array[i][0] == params.data.Ref_No && TempData_Array[i][1] == params.data.SupplierId) {
                SUPPLIER_COST_VALUE = parseFloat(TempData_Array[i][5]).toFixed(2);
            }
        }
    }

    var element = document.createElement("span");
    element.title = 'Supplier Cost Value($)';
    element.innerHTML = '<input type="text" style="text-align: center;" class="input-inc SUPPLIER_COST_VALUE" value = "' + SUPPLIER_COST_VALUE
        + '" onkeypress="return isNumberKeyWithNegative(event)" Ref_No = "' + params.data.Ref_No
        + '" SupplierId = "' + params.data.SupplierId
        + '" Rap_Amount = "' + params.data.Rap_Amount
        + '" Cts = "' + params.data.Cts
        + '" SUPPLIER_COST_DISC = "' + params.data.SUPPLIER_COST_DISC
        + '" SUPPLIER_COST_VALUE = "' + params.data.SUPPLIER_COST_VALUE
        + '" onblur="SUPPLIER_COST_VALUE(this);">';
    return element;
}
function SUPPLIER_COST_VALUE(e) {
    var Ref_No = $(e).attr("Ref_No");
    var SupplierId = $(e).attr("SupplierId");
    var Rap_Amount = $(e).attr("Rap_Amount");
    var Cts = $(e).attr("Cts");
    var OLD_SUPPLIER_COST_DISC = $(e).attr("SUPPLIER_COST_DISC");
    var OLD_SUPPLIER_COST_VALUE = $(e).attr("SUPPLIER_COST_VALUE");

    var CUSTOMER_COST_VALUE = $(e).parent().parent().parent().find('.CUSTOMER_COST_VALUE').val();

    var PROFIT = '', PROFIT_AMOUNT = '';

    if ($(e).val() != 0 && $(e).val() != "") {
        var SUPPLIER_COST_VALUE = parseFloat($(e).val());
        var SUPPLIER_COST_DISC = ((- 1 * (1 - (parseFloat(SUPPLIER_COST_VALUE) / parseFloat(Rap_Amount))) * 100));

        PROFIT = ((parseFloat(CUSTOMER_COST_VALUE) - parseFloat(SUPPLIER_COST_VALUE)) * 100) / parseFloat(SUPPLIER_COST_VALUE);
        PROFIT_AMOUNT = parseFloat(CUSTOMER_COST_VALUE) - parseFloat(SUPPLIER_COST_VALUE);

        $(e).parent().parent().parent().find('.SUPPLIER_COST_DISC').html(formatNumber(SUPPLIER_COST_DISC));
        $(e).parent().parent().parent().find('.SUPPLIER_COST_VALUE').val(parseFloat(SUPPLIER_COST_VALUE).toFixed(2));

        $(e).parent().parent().parent().find('.PROFIT').html(formatNumber(PROFIT));
        $(e).parent().parent().parent().find('.PROFIT_AMOUNT').html(formatNumber(PROFIT_AMOUNT));

        TEMP_SAVE("SUPPLIER_COST_VALUE", Ref_No, SupplierId, "", "", SUPPLIER_COST_DISC, SUPPLIER_COST_VALUE, "", "", PROFIT, PROFIT_AMOUNT);
    }
    else {
        $(e).parent().parent().parent().find('.SUPPLIER_COST_DISC').html(formatNumber(OLD_SUPPLIER_COST_DISC));
        $(e).parent().parent().parent().find('.SUPPLIER_COST_VALUE').val(parseFloat(OLD_SUPPLIER_COST_VALUE).toFixed(2));

        PROFIT = ((parseFloat(CUSTOMER_COST_VALUE) - parseFloat(OLD_SUPPLIER_COST_VALUE)) * 100) / parseFloat(OLD_SUPPLIER_COST_VALUE);
        PROFIT_AMOUNT = parseFloat(CUSTOMER_COST_VALUE) - parseFloat(OLD_SUPPLIER_COST_VALUE);

        $(e).parent().parent().parent().find('.PROFIT').html(formatNumber(PROFIT));
        $(e).parent().parent().parent().find('.PROFIT_AMOUNT').html(formatNumber(PROFIT_AMOUNT));

        TEMP_SAVE("SUPPLIER_COST_VALUE", Ref_No, SupplierId, "", "", OLD_SUPPLIER_COST_DISC, OLD_SUPPLIER_COST_VALUE, "", "", PROFIT, PROFIT_AMOUNT);
    }
}


function input_CUSTOMER_COST_DISC_Indicator(params) {
    var CUSTOMER_COST_DISC = '';
    if (TempData_Array.length > 0) {
        for (var i = 0; i < TempData_Array.length; i++) {
            if (TempData_Array[i][0] == params.data.Ref_No && TempData_Array[i][1] == params.data.SupplierId) {
                CUSTOMER_COST_DISC = formatNumber(TempData_Array[i][6]);
            }
        }
    }

    var element = document.createElement("span");
    element.title = 'Final Sale Disc(%)';
    element.innerHTML = '<label style="text-align: center;color: #003d66;font-size: 11px;text-align:center;font-weight:600;" class="input-inc CUSTOMER_COST_DISC">' + CUSTOMER_COST_DISC + '</label>';
    return element;
}

function input_CUSTOMER_COST_VALUE_Indicator(params) {
    var CUSTOMER_COST_VALUE = '';
    if (TempData_Array.length > 0) {
        for (var i = 0; i < TempData_Array.length; i++) {
            if (TempData_Array[i][0] == params.data.Ref_No && TempData_Array[i][1] == params.data.SupplierId) {
                CUSTOMER_COST_VALUE = parseFloat(TempData_Array[i][7]).toFixed(2);
            }
        }
    }

    var element = document.createElement("span");
    element.title = 'Final Sale Amt US($)'; 
    element.innerHTML = '<input type="text" style="text-align: center;" class="input-inc CUSTOMER_COST_VALUE" value = "' + CUSTOMER_COST_VALUE
        + '" onkeypress="return isNumberKeyWithNegative(event)" Ref_No = "' + params.data.Ref_No
        + '" SupplierId = "' + params.data.SupplierId
        + '" Rap_Amount = "' + params.data.Rap_Amount
        + '" Cts = "' + params.data.Cts
        + '" CUSTOMER_COST_DISC = "' + params.data.CUSTOMER_COST_DISC
        + '" CUSTOMER_COST_VALUE = "' + params.data.CUSTOMER_COST_VALUE
        + '" onblur="CUSTOMER_COST_VALUE(this);">';
    return element;
}
function CUSTOMER_COST_VALUE(e) {
    var Ref_No = $(e).attr("Ref_No");
    var SupplierId = $(e).attr("SupplierId");
    var Rap_Amount = $(e).attr("Rap_Amount");
    var Cts = $(e).attr("Cts");
    var OLD_CUSTOMER_COST_DISC = $(e).attr("CUSTOMER_COST_DISC");
    var OLD_CUSTOMER_COST_VALUE = $(e).attr("CUSTOMER_COST_VALUE");

    var SUPPLIER_COST_VALUE = $(e).parent().parent().parent().find('.SUPPLIER_COST_VALUE').val();

    var PROFIT = '', PROFIT_AMOUNT = '';
    
    if ($(e).val() != 0 && $(e).val() != "") {
        var CUSTOMER_COST_VALUE = parseFloat($(e).val());
        var CUSTOMER_COST_DISC = ((- 1 * (1 - (parseFloat(CUSTOMER_COST_VALUE) / parseFloat(Rap_Amount))) * 100));

        PROFIT = ((parseFloat(CUSTOMER_COST_VALUE) - parseFloat(SUPPLIER_COST_VALUE)) * 100) / parseFloat(SUPPLIER_COST_VALUE);
        PROFIT_AMOUNT = parseFloat(CUSTOMER_COST_VALUE) - parseFloat(SUPPLIER_COST_VALUE);

        $(e).parent().parent().parent().find('.CUSTOMER_COST_DISC').html(formatNumber(CUSTOMER_COST_DISC));
        $(e).parent().parent().parent().find('.CUSTOMER_COST_VALUE').val(parseFloat(CUSTOMER_COST_VALUE).toFixed(2));

        $(e).parent().parent().parent().find('.PROFIT').html(formatNumber(PROFIT));
        $(e).parent().parent().parent().find('.PROFIT_AMOUNT').html(formatNumber(PROFIT_AMOUNT));

        TEMP_SAVE("CUSTOMER_COST_VALUE", Ref_No, SupplierId, "", "", "", "", CUSTOMER_COST_DISC, CUSTOMER_COST_VALUE, PROFIT, PROFIT_AMOUNT);
    }
    else {
        $(e).parent().parent().parent().find('.CUSTOMER_COST_DISC').html(formatNumber(OLD_CUSTOMER_COST_DISC));
        $(e).parent().parent().parent().find('.CUSTOMER_COST_VALUE').val(parseFloat(OLD_CUSTOMER_COST_VALUE).toFixed(2));

        PROFIT = ((parseFloat(OLD_CUSTOMER_COST_VALUE) - parseFloat(SUPPLIER_COST_VALUE)) * 100) / parseFloat(SUPPLIER_COST_VALUE);
        PROFIT_AMOUNT = parseFloat(OLD_CUSTOMER_COST_VALUE) - parseFloat(SUPPLIER_COST_VALUE);

        $(e).parent().parent().parent().find('.PROFIT').html(formatNumber(PROFIT));
        $(e).parent().parent().parent().find('.PROFIT_AMOUNT').html(formatNumber(PROFIT_AMOUNT));

        TEMP_SAVE("CUSTOMER_COST_VALUE", Ref_No, SupplierId, "", "", "", "", OLD_CUSTOMER_COST_DISC, OLD_CUSTOMER_COST_VALUE, PROFIT, PROFIT_AMOUNT);
    }
}

function input_PROFIT_Indicator(params) {
    var CUSTOMER_COST_VALUE = '', SUPPLIER_COST_VALUE = '', PROFIT = '';

    if (TempData_Array.length > 0) {
        for (var i = 0; i < TempData_Array.length; i++) {
            if (TempData_Array[i][0] == params.data.Ref_No && TempData_Array[i][1] == params.data.SupplierId) {
                PROFIT = formatNumber(TempData_Array[i][8]);
            }
        }
    }

    var element = document.createElement("span");
    element.title = 'Profit(%)';
    element.innerHTML = '<label style="text-align: center;color: #003d66;font-size: 11px;text-align:center;font-weight:600;" class="input-inc PROFIT">' + PROFIT + '</label>';
    return element;
}
function input_PROFIT_AMOUNT_Indicator(params) {
    var CUSTOMER_COST_VALUE = '', SUPPLIER_COST_VALUE = '', PROFIT_AMOUNT = '';

    if (TempData_Array.length > 0) {
        for (var i = 0; i < TempData_Array.length; i++) {
            if (TempData_Array[i][0] == params.data.Ref_No && TempData_Array[i][1] == params.data.SupplierId) {
                PROFIT_AMOUNT = formatNumber(TempData_Array[i][9]);
            }
        }
    }

    var element = document.createElement("span");
    element.title = 'Profit Amount($)';
    element.innerHTML = '<label style="text-align: center;color: #003d66;font-size: 11px;text-align:center;font-weight:600;" class="input-inc PROFIT_AMOUNT">' + PROFIT_AMOUNT + '</label>';
    return element;
}


function TEMP_SAVE(WHEN, Ref_No, SupplierId, QC_Require, Lab_Status, SUPPLIER_COST_DISC, SUPPLIER_COST_VALUE, CUSTOMER_COST_DISC, CUSTOMER_COST_VALUE, PROFIT, PROFIT_AMOUNT) {
    SUPPLIER_COST_DISC = (SUPPLIER_COST_DISC != "" ? parseFloat(SUPPLIER_COST_DISC).toFixed(2) : "0");
    SUPPLIER_COST_VALUE = (SUPPLIER_COST_VALUE != "" ? parseFloat(SUPPLIER_COST_VALUE).toFixed(2) : "0");
    CUSTOMER_COST_DISC = (CUSTOMER_COST_DISC != "" ? parseFloat(CUSTOMER_COST_DISC).toFixed(2) : "0");
    CUSTOMER_COST_VALUE = (CUSTOMER_COST_VALUE != "" ? parseFloat(CUSTOMER_COST_VALUE).toFixed(2) : "0");
    PROFIT = (PROFIT != "" ? parseFloat(PROFIT).toFixed(2) : "0");
    PROFIT_AMOUNT = (PROFIT_AMOUNT != "" ? parseFloat(PROFIT_AMOUNT).toFixed(2) : "0");

    var exists = false;
    if (TempData_Array.length > 0) {
        for (var i = 0; i < TempData_Array.length; i++) {
            if (TempData_Array[i][0] == Ref_No && TempData_Array[i][1] == SupplierId) {
                if (WHEN == "QC_REQUIRE") {
                    TempData_Array[i][2] = QC_Require;
                }
                else if (WHEN == "LAB_STATUS") {
                    TempData_Array[i][3] = Lab_Status;
                }
                else if (WHEN == "SUPPLIER_COST_VALUE") {
                    TempData_Array[i][4] = SUPPLIER_COST_DISC;
                    TempData_Array[i][5] = SUPPLIER_COST_VALUE;
                    TempData_Array[i][8] = PROFIT;
                    TempData_Array[i][9] = PROFIT_AMOUNT;
                }
                else if (WHEN == "CUSTOMER_COST_VALUE") {
                    TempData_Array[i][6] = CUSTOMER_COST_DISC;
                    TempData_Array[i][7] = CUSTOMER_COST_VALUE;
                    TempData_Array[i][8] = PROFIT;
                    TempData_Array[i][9] = PROFIT_AMOUNT;
                }
                exists = true;
            }
        }
    }
    if (exists == false) {
        TempData_Array.push([Ref_No, SupplierId, QC_Require, Lab_Status, SUPPLIER_COST_DISC, SUPPLIER_COST_VALUE, CUSTOMER_COST_DISC, CUSTOMER_COST_VALUE, PROFIT, PROFIT_AMOUNT]);
    }
}
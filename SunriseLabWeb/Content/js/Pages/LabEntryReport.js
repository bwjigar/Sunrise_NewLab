var FortuneCodeValid = true;
var FortuneCodeValid_Msg = "";
var Rowdata = [], summary = [];
var OrderBy = "";

var gridOptions = {};
var iUserid = 0;
//var today = new Date();

let today = new Date();
// Add 2.5 hours to the current date
today.setHours(today.getHours() + 2, today.getMinutes() + 30);

var lastWeekDate = new Date(today.setDate(today.getDate() - 7));

today = new Date();
// Add 2.5 hours to the current date
today.setHours(today.getHours() + 2, today.getMinutes() + 30);

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
        else if (field == "SUPPLIER_COST_DISC" || field == "SUPPLIER_COST_VALUE") {
            return { 'color': '#143f58', 'font-weight': '600', 'background-color': '#ff99cc', 'text-align': 'center', 'font-size': '11px' };
        }
        else if (field == "CUSTOMER_COST_DISC" || field == "CUSTOMER_COST_VALUE") {
            return { 'color': '#143f58', 'font-weight': '600', 'background-color': '#ccffff', 'text-align': 'center', 'font-size': '11px' };
        }
        else if (field == "Disc" || field == "Value" || field == "SUPPLIER_COST_DISC" || field == "SUPPLIER_COST_VALUE" || field == "MAX_DISC" || field == "MAX_VALUE" ||
            field == "CUSTOMER_COST_DISC" || field == "CUSTOMER_COST_VALUE" || field == "Bid_Disc" || field == "Bid_Amt" || field == "Avg_Stock_Disc" ||
            field == "Avg_Pur_Disc" || field == "Avg_Sales_Disc" || field == "PROFIT" || field == "PROFIT_AMOUNT") {
            //return { 'color': 'red', 'font-weight': 'bold', 'font-size': '11px', 'text-align': 'center' };
            return { 'color': '#143f58', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
        else if (field == "Cts" || field == "Rap_Rate" || field == "Rap_Amount" || field == "Base_Price_Cts" || field == "RATIO" || field == "Length" ||
            field == "Width" || field == "Depth" || field == "Depth_Per" || field == "Table_Per" || field == "Crown_Angle" || field == "Pav_Angle" ||
            field == "Crown_Height" || field == "Pav_Height" || field == "Girdle_Per" || field == "RANK" || field == "Avg_Stock_Pcs" || field == "Avg_Pur_Pcs" ||
            field == "Sales_Pcs") {
            return { 'color': '#143f58', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
        else if (field == "Rank") {
            return { 'color': '#143f58', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
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
columnDefs.push({ headerName: "VIEW", field: "Imag_Video_Certi", width: 65, cellRenderer: function (params) { return Imag_Video_Certi(params, true, true, true); }, suppressSorting: true, suppressMenu: true, sortable: false });
columnDefs.push({ headerName: "Lab Entry Date", field: "LabDate", width: 100, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("LabDate", params); } });
columnDefs.push({ headerName: "Lab No", field: "LabId", width: 90, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("LabId", params); } });
columnDefs.push({ headerName: "Customer Name", field: "CustName", width: 140, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("CustName", params); } });
columnDefs.push({ headerName: "Company Name", field: "CompName", width: 220, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("CompName", params); } });
columnDefs.push({ headerName: "Ref No", field: "Ref_No", width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("RefNo", params); } });
columnDefs.push({ headerName: "QC Require", field: "QCRequire", width: 150, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("QCRequire", params); } });
columnDefs.push({ headerName: "Lab Entry Status", field: "LabEntry_Status", width: 100, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("LabEntry_Status", params); } });
columnDefs.push({ headerName: "Lab", field: "Lab", width: 50, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Lab", params); }, cellRenderer: function (params) { return Lab(params); } });
columnDefs.push({ headerName: "Supplier Stone Id", field: "Supplier_Stone_Id", width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Supplier_Stone_Id", params); } });
columnDefs.push({ headerName: "Certificate No", field: "Certificate_No", width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Certificate_No", params); } });
columnDefs.push({ headerName: "Supplier Name", field: "SupplierName", width: 230, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("SupplierName", params); } });
columnDefs.push({ headerName: "Shape", field: "Shape", width: 100, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Shape", params); } });
columnDefs.push({ headerName: "Pointer", field: "Pointer", width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Pointer", params); } });
columnDefs.push({ headerName: "BGM", field: "BGM", width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("BGM", params); } });
columnDefs.push({ headerName: "Color", field: "Color", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Color", params); } });
columnDefs.push({ headerName: "Clarity", field: "Clarity", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Clarity", params); } });
columnDefs.push({ headerName: "Cts", field: "Cts", width: 70, tooltip: function (params) { return parseFloat(params.value).toFixed(2) }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2) }, cellStyle: function (params) { return cellStyle("Cts", params); } });
columnDefs.push({ headerName: "Rap Rate($)", field: "Rap_Rate", width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Rap_Rate", params); } });
columnDefs.push({ headerName: "Rap Amount($)", field: "Rap_Amount", width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Rap_Amount", params); } });
columnDefs.push({ headerName: "Supplier Cost Disc(%)", field: "SUPPLIER_COST_DISC", width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("SUPPLIER_COST_DISC", params); } });
columnDefs.push({ headerName: "Supplier Cost Value($)", field: "SUPPLIER_COST_VALUE", width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("SUPPLIER_COST_VALUE", params); } });
columnDefs.push({ headerName: "Final Sale Disc(%)", field: "CUSTOMER_COST_DISC", width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("CUSTOMER_COST_DISC", params); } });
columnDefs.push({ headerName: "Final Sale Amt US($)", field: "CUSTOMER_COST_VALUE",  width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("CUSTOMER_COST_VALUE", params); } });
columnDefs.push({ headerName: "Profit(%)", field: "PROFIT", width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("PROFIT", params); } });
columnDefs.push({ headerName: "Profit Amount($)", field: "PROFIT_AMOUNT", width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("PROFIT_AMOUNT", params); } });
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
columnDefs.push({ headerName: "Table Black", field: "Table_Natts", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Natts", params); } });
columnDefs.push({ headerName: "Crown Black", field: "Crown_Natts", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Natts", params); } });
columnDefs.push({ headerName: "Table White", field: "Table_Inclusion", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Inclusion", params); } });
columnDefs.push({ headerName: "Crown White", field: "Crown_Inclusion", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Inclusion", params); } });
columnDefs.push({ headerName: "Table Open", field: "Table_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Open", params); } });
columnDefs.push({ headerName: "Crown Open", field: "Crown_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Open", params); } });
columnDefs.push({ headerName: "Pavilion Open", field: "Pav_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Pav_Open", params); } });
columnDefs.push({ headerName: "Girdle Open", field: "Girdle_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Girdle_Open", params); } });
columnDefs.push({ headerName: "Culet", field: "Culet", width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Culet", params); } });



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
        obj.StoneId = $("#txt_S_StoneId").val()
        obj.CustName = $("#txt_S_CustName").val();
        obj.Filter = filter;
        obj.FromDate = $("#txtFromDate").val();
        obj.ToDate = $("#txtToDate").val();

        Rowdata = [];
        summary = [];
        $.ajax({
            url: "/User/Get_LabEntryReport",
            async: false,
            type: "POST",
            data: { req: obj },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    Rowdata = data.Data;
                    summary = data.Data[0].DataSummary;
                    params.successCallback(data.Data[0].DataList, summary.TOT_PCS);
                }
                else {
                    Rowdata = [];
                    summary = [];
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
    FromTo_Date();
    $('#txt_S_StoneId').val('');
    $('#txt_S_CustName').val('');
    
    filter = "";
    ConfirmHold = false;
    Confirm = false;
    Hold = false;
    Bidded = false;
    Waiting = false;
    QcPending = false;
    QcReject = false;
    BidReject = false;
    Sold = false;
    Transit = false;
    Busy = false;
    Cancel = false;
    Other = false;

    $("#ConfirmHold").removeClass("btn-spn-opt-active");
    $("#Confirm").removeClass("btn-spn-opt-active");
    $("#Hold").removeClass("btn-spn-opt-active");
    $("#Bidded").removeClass("btn-spn-opt-active");
    $("#Waiting").removeClass("btn-spn-opt-active");
    $("#QcPending").removeClass("btn-spn-opt-active");
    $("#QcReject").removeClass("btn-spn-opt-active");
    $("#BidReject").removeClass("btn-spn-opt-active");
    $("#Sold").removeClass("btn-spn-opt-active");
    $("#Transit").removeClass("btn-spn-opt-active");
    $("#Busy").removeClass("btn-spn-opt-active");
    $("#Cancel").removeClass("btn-spn-opt-active");
    $("#Other").removeClass("btn-spn-opt-active");
    GetSearch();
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
    $("#li_User_LabEntryReport").addClass("menuActive");
    FromTo_Date();
    GetSearch();
    contentHeight();
});

$(window).resize(function () {
    contentHeight();
});
function Excel_LabEntryReport() {
    if (gridOptions.api != undefined) {
        loaderShow();
        setTimeout(function () {debugger
            var selectedRows = gridOptions.api.getSelectedRows();
            var LabDetId = '';
            var i = 0, tot = selectedRows.length;
            for (; i < tot; i++) {
                LabDetId += selectedRows[i].LabDetId + ',';
            }
            LabDetId = (LabDetId != '' ? LabDetId.substr(0, (LabDetId.length - 1)) : '');

            var obj = {};

            obj.StoneId = (LabDetId == "" ? $("#txt_S_StoneId").val() : "");
            obj.CustName = $("#txt_S_CustName").val();
            obj.Filter = filter;
            obj.LabDetId = LabDetId;
            obj.OrderBy = OrderBy;
            obj.FromDate = $("#txtFromDate").val();
            obj.ToDate = $("#txtToDate").val();

            $.ajax({
                url: "/User/Excel_LabEntryReport",
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

var ConfirmHold = false;
var Confirm = false;
var Hold = false;
var Bidded = false;
var Waiting = false;
var QcPending = false;
var QcReject = false;
var BidReject = false;
var Sold = false;
var Transit = false;
var Busy = false;
var Cancel = false;
var Other = false;
var filter = "";
function ActiveOrNot(id) {
    var flag = false;
    if ($("#" + id).hasClass("btn-spn-opt-active")) {
        $("#" + id).removeClass("btn-spn-opt-active");
        flag = false;
    }
    else {
        $("#" + id).addClass("btn-spn-opt-active");
        flag = true;
    }

    if (id == "ConfirmHold") {
        ConfirmHold = flag;
    }
    else if (id == "Confirm") {
        Confirm = flag;
    }
    else if (id == "Hold") {
        Hold = flag;
    }
    else if (id == "Bidded") {
        Bidded = flag;
    }
    else if (id == "Waiting") {
        Waiting = flag;
    }
    else if (id == "QcPending") {
        QcPending = flag;
    }
    else if (id == "QcReject") {
        QcReject = flag;
    }
    else if (id == "BidReject") {
        BidReject = flag;
    }
    else if (id == "Sold") {
        Sold = flag;
    }
    else if (id == "Transit") {
        Transit = flag;
    }
    else if (id == "Busy") {
        Busy = flag;
    }
    else if (id == "Cancel") {
        Cancel = flag;
    }
    else if (id == "Other") {
        Other = flag;
    }

    filter = "";
    if (ConfirmHold == true)
        filter += "Confirm Hold,";
    if (Confirm == true)
        filter += "Confirm,";
    if (Hold == true)
        filter += "Hold,";
    if (Bidded == true)
        filter += "Bidded,";
    if (Waiting == true)
        filter += "Waiting,";
    if (QcPending == true)
        filter += "Qc Pending,";
    if (QcReject == true)
        filter += "Qc Reject,";
    if (BidReject == true)
        filter += "Bid Reject,";
    if (Sold == true)
        filter += "Sold,";
    if (Transit == true)
        filter += "Transit,";
    if (Busy == true)
        filter += "Busy,";
    if (Cancel == true)
        filter += "Cancel,";
    if (Other == true)
        filter += "Other,";

    GetSearch();
}
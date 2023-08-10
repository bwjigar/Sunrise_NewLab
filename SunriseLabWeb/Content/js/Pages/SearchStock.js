var FortuneCodeValid = true;
var FortuneCodeValid_Msg = "";
var Rowdata = [];
var pgSize = 200;
var showEntryHtml = '<div class="show_entry"><label>'
    + 'Show <select onchange = "onPageSizeChanged()" id = "ddlPagesize">'
    + '<option value="200">200</option>'
    + '<option value="500">500</option>'
    + '<option value="1000">1000</option>'
    + '</select> entries'
    + '</label>'
    + '</div>';

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
    //{ headerName: "SupplierId", field: "SupplierId", hide: true },
    //{ headerName: "SupplierName", field: "SupplierName", hide: true },
    //{ headerName: "RefNo", field: "RefNo", hide: true },
    //{ headerName: "Shape", field: "Shape", hide: true },
    //{ headerName: "Color", field: "Color", hide: true },
    //{ headerName: "Clarity", field: "Clarity", hide: true },
    //{ headerName: "Cut", field: "Cut", hide: true },
    //{ headerName: "Polish", field: "Polish", hide: true },
    //{ headerName: "Symm", field: "Symm", hide: true },
    //{ headerName: "Fls", field: "Fls", hide: true },
    //{ headerName: "Cts", field: "Cts", hide: true },
    //{ headerName: "Pointer", field: "Pointer", hide: true },
    //{ headerName: "Sub_Pointer", field: "Sub_Pointer", hide: true },
    //{ headerName: "Base_Price_Cts", field: "Base_Price_Cts", hide: true },
    //{ headerName: "Rap_Rate", field: "Rap_Rate", hide: true },
    //{ headerName: "Base_Amount", field: "Base_Amount", hide: true },
    //{ headerName: "Measurement", field: "Measurement", hide: true },
    //{ headerName: "Length", field: "Length", hide: true },
    //{ headerName: "Width", field: "Width", hide: true },
    //{ headerName: "Depth", field: "Depth", hide: true },
    //{ headerName: "Table_Per", field: "Table_Per", hide: true },
    //{ headerName: "Depth_Per", field: "Depth_Per", hide: true },
    //{ headerName: "Table_Inclusion", field: "Table_Inclusion", hide: true },
    //{ headerName: "Crown_Inclusion", field: "Crown_Inclusion", hide: true },
    //{ headerName: "Table_Natts", field: "Table_Natts", hide: true },
    //{ headerName: "Crown_Natts", field: "Crown_Natts", hide: true },
    //{ headerName: "Side_Inclusion", field: "Side_Inclusion", hide: true },
    //{ headerName: "Side_Natts", field: "Side_Natts", hide: true },
    //{ headerName: "Crown_Open", field: "Crown_Open", hide: true },
    //{ headerName: "Pav_Open", field: "Pav_Open", hide: true },
    //{ headerName: "Table_Open", field: "Table_Open", hide: true },
    //{ headerName: "Girdle_Open", field: "Girdle_Open", hide: true },
    //{ headerName: "Crown_Angle", field: "Crown_Angle", hide: true },
    //{ headerName: "Pav_Angle", field: "Pav_Angle", hide: true },
    //{ headerName: "Crown_Height", field: "Crown_Height", hide: true },
    //{ headerName: "Pav_Height", field: "Pav_Height", hide: true },
    //{ headerName: "Rap_Amount", field: "Rap_Amount", hide: true },
    //{ headerName: "Lab", field: "Lab", hide: true },
    //{ headerName: "Certificate_URL", field: "Certificate_URL", hide: true },
    //{ headerName: "Image_URL", field: "Image_URL", hide: true },
    //{ headerName: "Image_URL_2", field: "Image_URL_2", hide: true },
    //{ headerName: "Image_URL_3", field: "Image_URL_3", hide: true },
    //{ headerName: "Image_URL_4", field: "Image_URL_4", hide: true },
    //{ headerName: "Video_URL", field: "Video_URL", hide: true },
    //{ headerName: "Video_URL_2", field: "Video_URL_2", hide: true },
    //{ headerName: "Status", field: "Status", hide: true },
    //{ headerName: "Supplier_Stone_Id", field: "Supplier_Stone_Id", hide: true },
    //{ headerName: "Location", field: "Location", hide: true },
    //{ headerName: "Shade", field: "Shade", hide: true },
    //{ headerName: "Luster", field: "Luster", hide: true },
    //{ headerName: "Type_2A", field: "Type_2A", hide: true },
    //{ headerName: "Milky", field: "Milky", hide: true },
    //{ headerName: "BGM", field: "BGM", hide: true },
    //{ headerName: "Key_To_Symboll", field: "Key_To_Symboll", hide: true },
    //{ headerName: "RATIO", field: "RATIO", hide: true },
    //{ headerName: "Supplier_Comments", field: "Supplier_Comments", hide: true },
    //{ headerName: "Lab_Comments", field: "Lab_Comments", hide: true },
    //{ headerName: "Culet", field: "Culet", hide: true },
    //{ headerName: "Girdle_Per", field: "Girdle_Per", hide: true },
    //{ headerName: "Girdle_Type", field: "Girdle_Type", hide: true },
    //{ headerName: "Girdle_MM", field: "Girdle_MM", hide: true },
    //{ headerName: "Inscription", field: "Inscription", hide: true },
    //{ headerName: "Culet_Condition", field: "Culet_Condition", hide: true },
    //{ headerName: "Star_Length", field: "Star_Length", hide: true },
    //{ headerName: "Lower_Halves", field: "Lower_Halves", hide: true },
    //{ headerName: "Stage", field: "Stage", hide: true },
    //{ headerName: "Certi_Date", field: "Certi_Date", hide: true },
    //{ headerName: "Disc", field: "Disc", hide: true },
    //{ headerName: "Fix_Price", field: "Fix_Price", hide: true },
    //{ headerName: "Certificate_No", field: "Certificate_No", hide: true },
    //{ headerName: "Goods_Type", field: "Goods_Type", hide: true },
    //{ headerName: "SUPPLIER_COST_DISC", field: "SUPPLIER_COST_DISC", hide: true },
    //{ headerName: "SUPPLIER_COST_VALUE", field: "SUPPLIER_COST_VALUE", hide: true },
    //{ headerName: "MAX_DISC", field: "MAX_DISC", hide: true },
    //{ headerName: "MAX_VALUE", field: "MAX_VALUE", hide: true },
    //{ headerName: "Shape_Sr", field: "Shape_Sr", hide: true },
    //{ headerName: "Color_Sr", field: "Color_Sr", hide: true },
    //{ headerName: "Clarity_Sr", field: "Clarity_Sr", hide: true },
    //{ headerName: "Cut_Sr", field: "Cut_Sr", hide: true },
    //{ headerName: "Polish_Sr", field: "Polish_Sr", hide: true },
    //{ headerName: "Symm_Sr", field: "Symm_Sr", hide: true },
    //{ headerName: "Fls_Sr", field: "Fls_Sr", hide: true },

    {
        headerName: "", field: "",
        headerCheckboxSelection: true,
        checkboxSelection: true, width: 28,
        suppressSorting: true,
        suppressMenu: true,
        headerCheckboxSelectionFilteredOnly: true,
        headerCellRenderer: selectAllRendererDetail,
        suppressMovable: false
    },
    { headerName: "VIEW", field: "Imag_Video_Certi", width: 90, cellRenderer: Imag_Video_Certi, suppressSorting: true, suppressMenu: true, sortable: false },
    { headerName: "Lab", field: "Lab", width: 50, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Lab", params); } },
    { headerName: "Ref No", field: "Ref_No", width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("RefNo", params); } },
    { headerName: "Supplier Stone Id", field: "Supplier_Stone_Id", width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Supplier_Stone_Id", params); } },
    { headerName: "Certificate No", field: "Certificate_No", width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Certificate_No", params); } },
    { headerName: "Shape", field: "Shape", width: 100, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Shape", params); } },
    { headerName: "Supplier Name", field: "SupplierName", width: 200, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("SupplierName", params); } },
    { headerName: "Pointer", field: "Pointer", width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Pointer", params); } },
    { headerName: "Sub Pointer", field: "Sub_Pointer", width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Sub_Pointer", params); } },
    { headerName: "Color", field: "Color", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Color", params); } },
    { headerName: "Clarity", field: "Clarity", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Clarity", params); } },
    { headerName: "Cts", field: "Cts", width: 70, tooltip: function (params) { return parseFloat(params.value).toFixed(2) }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2) }, cellStyle: function (params) { return cellStyle("Cts", params); } },
    { headerName: "Rap Rate ($)", field: "Rap_Rate", width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Rap_Rate", params); } },
    { headerName: "Rap Amount ($)", field: "Rap_Amount", width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Rap_Amount", params); } },
    { headerName: "Base Price Cts ($)", field: "Base_Price_Cts", width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Base_Price_Cts", params); } },
    { headerName: "Base Amount ($)", field: "Base_Amount", width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); } },
    
    { headerName: "Base Disc (%)", field: "Disc", width: 85, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellStyle: function (params) { return cellStyle("Disc", params); } },
    { headerName: "Supplier Final Disc (%)", field: "SUPPLIER_COST_DISC", width: 85, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellStyle: function (params) { return cellStyle("SUPPLIER_COST_DISC", params); } },
    { headerName: "Supplier Final Amt ($)", field: "SUPPLIER_COST_VALUE", width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellStyle: function (params) { return cellStyle("SUPPLIER_COST_VALUE", params); } },
    { headerName: "Supplier Max Disc (%)", field: "MAX_DISC", width: 85, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellStyle: function (params) { return cellStyle("MAX_DISC", params); } },
    { headerName: "Supplier Max Amt ($)", field: "MAX_VALUE", width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellStyle: function (params) { return cellStyle("MAX_VALUE", params); } },
    { headerName: "Offer Disc (%)", field: "CUSTOMER_COST_DISC", width: 85, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellStyle: function (params) { return cellStyle("MAX_VALUE", params); } },
    { headerName: "Offer Amt ($)", field: "CUSTOMER_COST_VALUE", width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellStyle: function (params) { return cellStyle("MAX_VALUE", params); } },

    { headerName: "Cut", field: "Cut", tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Cut", params); } },
    { headerName: "Polish", field: "Polish", tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Polish", params); } },
    { headerName: "Symm", field: "Symm", tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Symm", params); } },
    { headerName: "Fls", field: "Fls", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Fls", params); } },
    { headerName: "Key To Symboll", field: "Key_To_Symboll", width: 300, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Key_To_Symboll", params); } },

    { headerName: "RATIO", field: "RATIO", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("RATIO", params); } },
    { headerName: "Length", field: "Length", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Length", params); } },
    { headerName: "Width", field: "Width", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Width", params); } },
    { headerName: "Depth", field: "Depth", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Depth", params); } },
    { headerName: "Measurement", field: "Measurement", width: 130, tooltip: function (params) { return (params.value); } },
    { headerName: "Depth Per", field: "Depth_Per", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Depth_Per", params); } },
    { headerName: "Table Per", field: "Table_Per", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Table_Per", params); } },
    { headerName: "Crown Angle", field: "Crown_Angle", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Crown_Angle", params); } },
    { headerName: "Pav Angle", field: "Pav_Angle", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Pav_Angle", params); } },
    { headerName: "Crown Height", field: "Crown_Height", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Crown_Height", params); } },
    { headerName: "Pav Height", field: "Pav_Height", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Pav_Height", params); } },
    { headerName: "Girdle Per", field: "Girdle_Per", width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Girdle_Per", params); } },

    { headerName: "Luster", field: "Luster", width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Luster", params); } },
    { headerName: "Type 2A", field: "Type_2A", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Type_2A", params); } },
    { headerName: "Table Inclusion", field: "Table_Inclusion", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Inclusion", params); } },
    { headerName: "Crown Inclusion", field: "Crown_Inclusion", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Inclusion", params); } },
    { headerName: "Table Natts", field: "Table_Natts", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Natts", params); } },
    { headerName: "Crown Natts", field: "Crown_Natts", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Natts", params); } },
    { headerName: "Culet", field: "Culet", width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Culet", params); } },
    { headerName: "Lab Comments", field: "Lab_Comments", width: 300, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Lab_Comments", params); } },
    { headerName: "Supplier Comments", field: "Supplier_Comments", width: 300, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Supplier_Comments", params); } },
    { headerName: "Crown Open", field: "Crown_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Open", params); } },
    { headerName: "Pav Open", field: "Pav_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Pav_Open", params); } },
    { headerName: "Table Open", field: "Table_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Open", params); } },
    { headerName: "Girdle Open", field: "Girdle_Open", width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Girdle_Open", params); } },
    { headerName: "Shade", field: "Shade", width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Shade", params); } },
    { headerName: "Milky", field: "Milky", width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Milky", params); } },
    { headerName: "Status", field: "Status", width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Status", params); } },
    { headerName: "Location", field: "Location", width: 100, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Location", params); } },
];


function cellStyle(field, params) {
    if (params.data != undefined) {
        if (params.data.Cut == '3EX' && (field == 'Cut' || field == 'Polish' || field == 'Symm')) {
            return { 'font-size': '11px', 'font-weight': 'bold' };
        }
        else if (field == "Disc" || field == "SUPPLIER_COST_DISC" || field == "SUPPLIER_COST_VALUE" || field == "MAX_DISC" || field == "MAX_VALUE" || field == "CUSTOMER_COST_DISC" || field == "CUSTOMER_COST_VALUE") {
            return { 'color': 'red', 'font-weight': 'bold', 'font-size': '11px', 'text-align': 'center' };
        }
        else if (field == "Cts" || field == "Rap_Rate" || field == "Rap_Amount" || field == "Base_Price_Cts" || field == "RATIO" || field == "Length" || field == "Width" || field == "Depth" ||
            field == "Depth_Per" || field == "Table_Per" || field == "Crown_Angle" || field == "Pav_Angle" || field == "Crown_Height" || field == "Pav_Height" || field == "Girdle_Per") {
            return { 'color': '#003d66', 'font-size': '11px', 'text-align': 'center', 'font-weight': '600' };
        }
        else {
            return { 'font-size': '11px', 'text-align': 'center' }
        }
    }
}
function Status(params) {
    if (params.value == true) {
        return "<span class='Yes'> Yes </span>";
    }
    else {
        return "<span class='No'> No </span>";
    }
}
var deltaIndicator = function (params) {
    var element = "";
    element = '<a title="Edit" onclick="EditView(\'' + params.data.Id + '\')" ><i class="fa fa-pencil-square-o" aria-hidden="true" style="font-size: 17px;cursor:pointer;"></i></a>';
    //element += '&nbsp;&nbsp;<a title="Delete" onclick="DeleteView(\'' + params.data.Id + '\')"><i class="fa fa-trash-o" aria-hidden="true" style="cursor:pointer;"></i></a>';
    return element;
}
function onPageSizeChanged() {
    var value = $("#ddlPagesize").val();
    pgSize = Number(value);
    GetHoldDataGrid();
}
function Search() {
    $("#divFilter").hide();
    $("#divGridView").show();
    //if (type == "Customer") {
    //    //$(".sup").hide();
    //    isCustomer = true;
    //    CustomerColumn();
    //}
    //else if (type == "Supplier") {
    //    //$(".sup").show();
    //    isCustomer = false;
    //    SupplierColumn();
    //}
    GetHoldDataGrid();
}
function GetHoldDataGrid() {
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
        onRowSelected: onSelectionChanged, onBodyScroll: onBodyScroll,
        rowModelType: 'serverSide',
        cacheBlockSize: pgSize, // you can have your custom page size
        paginationPageSize: pgSize, //pagesize
        getContextMenuItems: getContextMenuItems,
        paginationNumberFormatter: function (params) {
            return '[' + params.value.toLocaleString() + ']';
        }
    };
    var gridDiv = document.querySelector('#myGrid');
    new agGrid.Grid(gridDiv, gridOptions);
    gridOptions.api.setServerSideDatasource(datasource1);

    showEntryVar = setInterval(function () {
        if ($('#myGrid .ag-paging-panel').length > 0) {
            $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');

            $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
            $('#ddlPagesize').val(pgSize);
            clearInterval(showEntryVar);
        }
    }, 1000);

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
var SortColumn = "";
var SortDirection = "";
const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var obj = {};
        
        var SizeLst = "";
        var CaratType = "";
        
        var lst = _.filter(CaratList, function (e) { return e.isActive == true });
        if (($('#txtfromcarat').val() != "" && parseFloat($('#txtfromcarat').val()) > 0) && ($('#txttocarat').val() != "" && parseFloat($('#txttocarat').val()) > 0)) {
            NewSizeGroup();
        }
        if (SizeGroupList.length != 0) {
            SizeLst = _.pluck(SizeGroupList, 'Size').join(",");
            CaratType = 'Specific';
        }
        if (lst.length != 0) {
            SizeLst = _.pluck(_.filter(CaratList, function (e) { return e.isActive == true }), 'Value').join(",");
            CaratType = 'General';
        }

        var refno = $("#txtRefNo").val().replace(/ /g, ',');
        var supplier = $('#ddlSupplierId').val().join(",");
        var shapeLst = _.pluck(_.filter(ShapeList, function (e) { return e.isActive == true }), 'Value').join(",");
        var colorLst = _.pluck(_.filter(ColorList, function (e) { return e.isActive == true }), 'Value').join(",");
        var clarityLst = _.pluck(_.filter(ClarityList, function (e) { return e.isActive == true }), 'Value').join(",");
        var labLst = _.pluck(_.filter(LabList, function (e) { return e.isActive == true }), 'Value').join(",");
        var cutLst = _.pluck(_.filter(CutList, function (e) { return e.isActive == true }), 'Value').join(",");
        var polishLst = _.pluck(_.filter(PolishList, function (e) { return e.isActive == true }), 'Value').join(",");
        var symLst = _.pluck(_.filter(SymList, function (e) { return e.isActive == true }), 'Value').join(",");
        var fluoLst = _.pluck(_.filter(FlouList, function (e) { return e.isActive == true }), 'Value').join(",");
        var bgmLst = _.pluck(_.filter(BGMList, function (e) { return e.isActive == true }), 'Value').join(",");

        var tblincLst = _.pluck(_.filter(TblInclList, function (e) { return e.isActive == true }), 'Value').join(",");
        var tblnattsLst = _.pluck(_.filter(TblNattsList, function (e) { return e.isActive == true }), 'Value').join(",");
        var crwincLst = _.pluck(_.filter(CrwnInclList, function (e) { return e.isActive == true }), 'Value').join(",");
        var crwnattsLst = _.pluck(_.filter(CrwnNattsList, function (e) { return e.isActive == true }), 'Value').join(",");

        var tableopen = _.pluck(_.filter(TableOpenList, function (e) { return e.isActive == true }), 'Value').join(",");
        var crownopen = _.pluck(_.filter(CrownOpenList, function (e) { return e.isActive == true }), 'Value').join(",");
        var pavopen = _.pluck(_.filter(PavOpenList, function (e) { return e.isActive == true }), 'Value').join(",");
        var girdleopen = _.pluck(_.filter(GirdleOpenList, function (e) { return e.isActive == true }), 'Value').join(",");

        var KeyToSymLst_Check = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
        var KeyToSymLst_uncheck = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");

        
        obj.PgNo = PageNo;
        obj.PgSize = pgSize;
        if (params.request.sortModel.length > 0) {
            obj.OrderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort;
        }

        obj.RefNo = refno
        obj.SupplierId = supplier;
        obj.Shape = shapeLst;
        obj.Pointer = SizeLst;
        obj.ColorType = Color_Type;

        if (Color_Type == "Fancy") {
            obj.Color = "";
            obj.INTENSITY = _.pluck(Check_Color_1, 'Symbol').join(",");
            obj.OVERTONE = _.pluck(Check_Color_2, 'Symbol').join(",");
            obj.FANCY_COLOR = _.pluck(Check_Color_3, 'Symbol').join(",");
        }
        else if (Color_Type == "Regular") {
            obj.Color = colorLst;
            obj.INTENSITY = "";
            obj.OVERTONE = "";
            obj.FANCY_COLOR = "";
        }

        obj.Clarity = clarityLst;
        obj.Cut = cutLst;
        obj.Polish = polishLst;
        obj.Symm = symLst;
        obj.Fls = fluoLst;
        obj.BGM = bgmLst;
        obj.Lab = labLst;

        obj.CrownBlack = crwnattsLst;
        obj.TableBlack = tblnattsLst;
        obj.CrownWhite = crwincLst;
        obj.TableWhite = tblincLst;

        obj.TableOpen = tableopen;
        obj.CrownOpen = crownopen;
        obj.PavOpen = pavopen;
        obj.GirdleOpen = girdleopen;

        obj.KTSBlank = (KTSBlank == true ? true : "");
        obj.Keytosymbol = KeyToSymLst_Check + '-' + KeyToSymLst_uncheck;
        obj.CheckKTS = KeyToSymLst_Check;
        obj.UNCheckKTS = KeyToSymLst_uncheck;

        obj.FromDisc = $('#FromDiscount').val();
        obj.ToDisc = $('#ToDiscount').val();

        obj.FromTotAmt = $('#FromTotalAmt').val();
        obj.ToTotAmt = $('#ToTotalAmt').val();

        obj.LengthBlank = (LengthBlank == true ? true : "");
        obj.FromLength = $('#FromLength').val();
        obj.ToLength = $('#ToLength').val();

        obj.WidthBlank = (WidthBlank == true ? true : "");
        obj.FromWidth = $('#FromWidth').val();
        obj.ToWidth = $('#ToWidth').val();

        obj.DepthBlank = (DepthBlank == true ? true : "");
        obj.FromDepth = $('#FromDepth').val();
        obj.ToDepth = $('#ToDepth').val();

        obj.DepthPerBlank = (DepthPerBlank == true ? true : "");
        obj.FromDepthPer = $('#FromDepthPer').val();
        obj.ToDepthPer = $('#ToDepthPer').val();

        obj.TablePerBlank = (TablePerBlank == true ? true : "");
        obj.FromTablePer = $('#FromTablePer').val();
        obj.ToTablePer = $('#ToTablePer').val();

        obj.Img = $('#SearchImage').hasClass('active') ? "YES" : "";
        obj.Vdo = $('#SearchVideo').hasClass('active') ? "YES" : "";
        obj.Certi = $('#SearchCerti').hasClass('active') ? "YES" : "";

        obj.CrAngBlank = (CrAngBlank == true ? true : "");
        obj.FromCrAng = $('#FromCrAng').val();
        obj.ToCrAng = $('#ToCrAng').val();

        obj.CrHtBlank = (CrHtBlank == true ? true : "");
        obj.FromCrHt = $('#FromCrHt').val();
        obj.ToCrHt = $('#ToCrHt').val();

        obj.PavAngBlank = (PavAngBlank == true ? true : "");
        obj.FromPavAng = $('#FromPavAng').val();
        obj.ToPavAng = $('#ToPavAng').val();

        obj.PavHtBlank = (PavHtBlank == true ? true : "");
        obj.FromPavHt = $('#FromPavHt').val();
        obj.ToPavHt = $('#ToPavHt').val();

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
                    if (data.Data.length == 0) {
                        $("#divFilter").show();
                        $("#divGridView").hide();
                        toastr.error("No data found.");
                    }

                    Rowdata = [];
                    params.successCallback([], 0);
                }
                setInterval(function () {
                    $(".ag-header-cell-text").addClass("grid_prewrap");
                    contentHeight();
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
function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".order-title").height(),
        contentHei = winH - 0 - navbarHei - 115;
    $("#myGrid").css("height", contentHei);
}
new WOW().init();

$(window).resize(function () {
    contentHeight();
});
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
    //var Totalpcs = 0;
    //var TotalCts = 0.0;
    //var TotalNetAmt = 0.0;
    //var TotalRapAmt = 0.0;
    //var net_amount = 0.0;
    //var rap_amount = 0.0;
    //var TotalPricePerCts = 0.0;
    //var dDisc = 0, dRepPrice = 0, DCTS = 0, dNetPrice = 0, Web_Benefit = 0, Final_Disc = 0, Net_Value = 0;

    //var SearchResultData = gridOptions.api.getSelectedRows();//data;//data.length == 0 ? Filtered_Data : gridOptions.api.getSelectedRows();
    //if (SearchResultData.length != 0) {
    //    for (var i = 0; i < SearchResultData.length; i++) {
    //        Totalpcs = Totalpcs + 1;
    //        TotalCts += parseFloat(SearchResultData[i].cts);

    //        net_amount = parseFloat(SearchResultData[i].net_amount);
    //        rap_amount = parseFloat(SearchResultData[i].rap_amount);
    //        net_amount = isNaN(net_amount) ? 0 : net_amount.toFixed(2);
    //        rap_amount = isNaN(rap_amount) ? 0 : rap_amount.toFixed(2);

    //        TotalNetAmt += parseFloat(net_amount);
    //        TotalRapAmt += parseFloat(rap_amount);
    //        dDisc += parseFloat(SearchResultData[i].sales_disc_per);
    //    }

    //    if (Scheme_Disc_Type == "Discount") {
    //        Net_Value = 0;
    //        Final_Disc = 0;
    //        Web_Benefit = 0;
    //    }
    //    else if (Scheme_Disc_Type == "Value") {
    //        Net_Value = parseFloat(TotalNetAmt) + (parseFloat(TotalNetAmt) * parseFloat(Scheme_Disc) / 100);
    //        Final_Disc = ((1 - parseFloat(Net_Value) / parseFloat(TotalRapAmt)) * 100) * -1;
    //        Web_Benefit = parseFloat(TotalNetAmt) - parseFloat(Net_Value);
    //    }
    //    else {
    //        Net_Value = parseFloat(TotalNetAmt);
    //        Final_Disc = parseFloat(AvgDis);
    //        Web_Benefit = 0;
    //    }
    //    $('#tab1_WebDisc_t').show();
    //    $('#tab1_FinalValue_t').show();
    //    $('#tab1_FinalDisc_t').show();
    //}
    //else {
    //    AllDataList = Filtered_Data.length > 0 ? Filtered_Data : rowData;
    //    if (AllDataList.length != 0) {
    //        for (var i = 0; i < AllDataList.length; i++) {
    //            Totalpcs = Totalpcs + 1;
    //            TotalCts += parseFloat(AllDataList[i].cts);

    //            net_amount = parseFloat(AllDataList[i].net_amount);
    //            rap_amount = parseFloat(AllDataList[i].rap_amount);
    //            net_amount = isNaN(net_amount) ? 0 : net_amount.toFixed(2);
    //            rap_amount = isNaN(rap_amount) ? 0 : rap_amount.toFixed(2);
    //            dDisc += parseFloat(AllDataList[i].sales_disc_per);
    //        }
    //    }
    //    else {
    //        SearchStoneDataList = Filtered_Data.length > 0 ? Filtered_Data : SearchStoneDataList;
    //        for (var i = 0; i < SearchStoneDataList.length; i++) {
    //            Totalpcs = Totalpcs + 1;
    //            TotalCts += parseFloat(SearchStoneDataList[i].cts);

    //            net_amount = parseFloat(SearchStoneDataList[i].net_amount);
    //            rap_amount = parseFloat(SearchStoneDataList[i].rap_amount);
    //            net_amount = isNaN(net_amount) ? 0 : net_amount.toFixed(2);
    //            rap_amount = isNaN(rap_amount) ? 0 : rap_amount.toFixed(2);
    //            dDisc += parseFloat(SearchStoneDataList[i].sales_disc_per);
    //        }
    //    }
    //    $('#tab1_WebDisc_t').hide();
    //    $('#tab1_FinalValue_t').hide();
    //    $('#tab1_FinalDisc_t').hide();
    //}
    //TotalPricePerCts = (TotalNetAmt / TotalCts).toFixed(2);
    //AvgDis = ((1 - (TotalNetAmt / TotalRapAmt)) * (-100)).toFixed(2);

    //TotalPricePerCts = isNaN(TotalPricePerCts) ? 0 : TotalPricePerCts;
    //AvgDis = isNaN(AvgDis) ? 0 : AvgDis;

    //setTimeout(function () {
    //    //$('#tab1cts').html($("#hdn_Cts").val() + ' : ' + formatNumber(TotalCts) + '');
    //    //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : ' + formatNumber(AvgDis) + '');
    //    //$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(TotalPricePerCts) + '');
    //    //$('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(TotalNetAmt) + '');
    //    //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + Totalpcs + '');
    //    $('#tab1TCount').show();
    //    $('#tab1pcs').html(Totalpcs);
    //    $('#tab1cts').html(formatNumber(TotalCts));
    //    $('#tab1disc').html(formatNumber(AvgDis));
    //    $('#tab1ppcts').html(formatNumber(TotalPricePerCts));
    //    $('#tab1totAmt').html(formatNumber(TotalNetAmt));

    //    $('#tab1Web_Disc').html(formatNumber(Web_Benefit));
    //    $('#tab1Net_Value').html(formatNumber(Net_Value));
    //    $('#tab1Final_Disc').html(formatNumber(Final_Disc));
    //});

}
function Imag_Video_Certi(params) {
    if (params.data == undefined) {
        return '';
    }

    var image_url = (params.data.Image_URL == null ? "" : params.data.Image_URL);
    var movie_url = (params.data.Video_URL == null ? "" : params.data.Video_URL);
    var certi_url = (params.data.Certificate_URL == null ? "" : params.data.Certificate_URL);

    if (image_url != "") {
        image_url = '<li><a href="' + image_url + '" target="_blank" title="View Diamond Image">' +
            '<img src="../Content/images/frame.svg" class="frame-icon"></a></li>';
    }
    else {
        image_url = '<li><a href="javascript:void(0);" title="View Diamond Image">' +
            '<img src="../Content/images/image-not-available.svg" class="frame-icon"></a></li>';
    }

    if (movie_url != "") {
        movie_url = '<li><a href="' + movie_url + '" target="_blank" title="View Diamond Video">' +
            '<img src="../Content/images/video-recording.svg" class="frame-icon"></a></li>';
    }
    else {
        movie_url = '<li><a href="javascript:void(0);" title="View Diamond Video">' +
            '<img src="../Content/images/video-recording-not-available.svg" class="frame-icon"></a></li>';
    }

    if (certi_url != "") {
        certi_url = '<li><a href="' + certi_url + '" target="_blank" title="View Diamond Certificate">' +
            '<img src="../Content/images/medal.svg" class="medal-icon"></a></li>';
    }
    else {
        certi_url = '<li><a href="javascript:void(0);" title="View Diamond Certificate">' +
            '<img src="../Content/images/medal-not-available.svg" class="medal-icon"></a></li>';
    }

    var data = ('<ul class="flat-icon-ul">' + image_url + movie_url + certi_url + '</ul>');

    return data;
}
function onSelectionChanged(event) {
    //var Totalpcs = 0;
    //var TotalCts = 0.0;
    //var TotalNetAmt = 0.0;
    //var TotalRapAmt = 0.0;
    //var net_amount = 0.0;
    //var rap_amount = 0.0;
    //var CUR_RAP_RATE = 0;
    //var TotalPricePerCts = 0.0;
    //var dDisc = 0, dRepPrice = 0, DCTS = 0, dNetPrice = 0, Web_Benefit = 0, Final_Disc = 0, Net_Value = 0;

    //var SearchResultData = gridOptions.api.getSelectedRows();
    //if (SearchResultData.length != 0) {
    //    for (var i = 0; i < SearchResultData.length; i++) {
    //        Totalpcs = Totalpcs + 1;
    //        TotalCts += parseFloat(SearchResultData[i].cts);

    //        net_amount = parseFloat(SearchResultData[i].net_amount);
    //        rap_amount = parseFloat(SearchResultData[i].rap_amount);
    //        CUR_RAP_RATE += parseFloat(SearchResultData[i].cur_rap_rate);
    //        net_amount = isNaN(net_amount) ? 0 : net_amount.toFixed(2);
    //        rap_amount = isNaN(rap_amount) ? 0 : rap_amount.toFixed(2);

    //        TotalNetAmt += parseFloat(net_amount);
    //        TotalRapAmt += parseFloat(rap_amount);
    //        dDisc += parseFloat(SearchResultData[i].sales_disc_per);
    //    }
    //    if (Scheme_Disc_Type == "Discount") {
    //        Net_Value = 0;
    //        Final_Disc = 0;
    //        Web_Benefit = 0;
    //    }
    //    else if (Scheme_Disc_Type == "Value") {
    //        Net_Value = parseFloat(TotalNetAmt) + (parseFloat(TotalNetAmt) * parseFloat(Scheme_Disc) / 100);
    //        Final_Disc = ((1 - parseFloat(Net_Value) / parseFloat(TotalRapAmt)) * 100) * -1;
    //        Web_Benefit = parseFloat(TotalNetAmt) - parseFloat(Net_Value);
    //    }
    //    else {
    //        Net_Value = parseFloat(TotalNetAmt);
    //        Final_Disc = parseFloat(AvgDis);
    //        Web_Benefit = 0;
    //    }
    //    $('#tab1_WebDisc_t').show();
    //    $('#tab1_FinalValue_t').show();
    //    $('#tab1_FinalDisc_t').show();
    //}
    //else {
    //    SearchStoneDataList = Filtered_Data.length > 0 ? Filtered_Data : rowData;
    //    for (var i = 0; i < SearchStoneDataList.length; i++) {
    //        Totalpcs = Totalpcs + 1;
    //        TotalCts += parseFloat(SearchStoneDataList[i].cts);

    //        net_amount = parseFloat(SearchStoneDataList[i].net_amount);
    //        rap_amount = parseFloat(SearchStoneDataList[i].rap_amount);
    //        CUR_RAP_RATE += parseFloat(SearchStoneDataList[i].cur_rap_rate);
    //        net_amount = isNaN(net_amount) ? 0 : net_amount.toFixed(2);
    //        rap_amount = isNaN(rap_amount) ? 0 : rap_amount.toFixed(2);

    //        TotalNetAmt += parseFloat(net_amount);
    //        TotalRapAmt += parseFloat(rap_amount);
    //        dDisc += parseFloat(SearchStoneDataList[i].sales_disc_per);
    //    }
    //    $('#tab1_WebDisc_t').hide();
    //    $('#tab1_FinalValue_t').hide();
    //    $('#tab1_FinalDisc_t').hide();
    //}

    //TotalPricePerCts = (TotalNetAmt / TotalCts).toFixed(2);
    //AvgDis = ((1 - (TotalNetAmt / TotalRapAmt)) * (-100)).toFixed(2);

    //TotalPricePerCts = isNaN(TotalPricePerCts) ? 0 : TotalPricePerCts;
    //AvgDis = isNaN(AvgDis) ? 0 : AvgDis;
    //if (CUR_RAP_RATE == 0) {
    //    Final_Disc = 0;
    //    AvgDis = 0;
    //}
    //setTimeout(function () {
    //    //$('#tab1cts').html($("#hdn_Cts").val() + ' : ' + formatNumber(TotalCts) + '');
    //    //$('#tab1disc').html($("#hdn_Avg_Disc_Per").val() + ' : ' + formatNumber(AvgDis) + '');
    //    //$('#tab1ppcts').html($("#hdn_Price_Per_Cts").val() + ' : $ ' + formatNumber(TotalPricePerCts) + '');
    //    //$('#tab1totAmt').html($("#hdn_Total_Amount").val() + ' : $ ' + formatNumber(TotalNetAmt) + '');
    //    //$('#tab1pcs').html($("#hdn_Pcs").val() + ' : ' + Totalpcs + '');
    //    $('#tab1TCount').show();
    //    $('#tab1pcs').html(Totalpcs);
    //    $('#tab1cts').html(formatNumber(TotalCts));
    //    $('#tab1disc').html(formatNumber(AvgDis));
    //    $('#tab1ppcts').html(formatNumber(TotalPricePerCts));
    //    $('#tab1totAmt').html(formatNumber(TotalNetAmt));

    //    $('#tab1Web_Disc').html(formatNumber(Web_Benefit));
    //    $('#tab1Net_Value').html(formatNumber(Net_Value));
    //    $('#tab1Final_Disc').html(formatNumber(Final_Disc));
    //});
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
function NullReplaceDecimalToFixed(value) {
    return (value != null && value != "" ? parseFloat(value).toFixed(2) : "");
}





var isCustomer = false;
var ParameterList;
var ShapeList = [];
var CaratList = [];
var ColorList = [];
var ClarityList = [];
var CutList = [];
var PolishList = [];
var SymList = [];
var FlouList = [];
var BGMList = [];
var LabList = [];
var SizeGroupList = [];
var TblInclList = [];
var TblNattsList = [];
var CrwnInclList = [];
var CrwnNattsList = [];

var KTSBlank = false;
var LengthBlank = false;
var WidthBlank = false;
var DepthBlank = false;
var DepthPerBlank = false;
var TablePerBlank = false;
var CrAngBlank = false;
var CrHtBlank = false;
var PavAngBlank = false;
var PavHtBlank = false;

var TableOpenList = [];
var CrownOpenList = [];
var PavOpenList = [];
var GirdleOpenList = [];

var KeyToSymbolList = [];
var CheckKeyToSymbolList = [];
var UnCheckKeyToSymbolList = [];

var Check_Color_1 = [];
var Check_Color_2 = [];
var Check_Color_3 = [];
var KTS = 0, C1 = 0, C2 = 0, C3 = 0;
var INTENSITY = [], OVERTONE = [], FANCY_COLOR = [];
var Color_Type = 'Regular';
var IsFiltered = true;
var ActivityType = "";
function FancyDDLHide() {
    $("#sym-sec1 .carat-dropdown-main").hide();
    $("#sym-sec2 .carat-dropdown-main").hide();
    $("#sym-sec3 .carat-dropdown-main").hide();
}
function INTENSITYShow() {
    setTimeout(function () {
        if (C1 == 0) {
            $("#sym-sec0 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").hide();
            $("#sym-sec1 .carat-dropdown-main").show();
            C1 = 1;
            KTS = 0, C2 = 0, C3 = 0;
        }
        else {
            $("#sym-sec1 .carat-dropdown-main").hide();
            C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function OVERTONEShow() {
    setTimeout(function () {
        if (C2 == 0) {
            $("#sym-sec0 .carat-dropdown-main").hide();
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").show();
            C2 = 1;
            C1 = 0, KTS = 0, C3 = 0;
        }
        else {
            $("#sym-sec2 .carat-dropdown-main").hide();
            C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function FANCY_COLORShow() {
    setTimeout(function () {
        if (C3 == 0) {
            $("#sym-sec0 .carat-dropdown-main").hide();
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").show();
            C3 = 1;
            C1 = 0, KTS = 0, C2 = 0;
        }
        else {
            $("#sym-sec3 .carat-dropdown-main").hide();
            C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function Key_to_symbolShow() {
    setTimeout(function () {
        if (KTS == 0) {
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").hide();
            $("#sym-sec0 .carat-dropdown-main").show();
            KTS = 1;
            C1 = 0, C2 = 0, C3 = 0;
        }
        else {
            $("#sym-sec0 .carat-dropdown-main").hide();
            C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function FcolorBind() {
    //INTENSITY = ['W-X', 'Y-Z', 'Faint', 'Very Light', 'Light', 'Fancy Light', 'Fancy', 'Fancy Intense', 'Fancy Green', 'Fancy Dark',
    //    'Fancy Deep', 'Fancy Vivid', 'Fancy Faint', 'Dark'];
    //OVERTONE = ['None', 'Brownish', 'Grayish', 'Greenish', 'Yellowish', 'Pinkish', 'Orangey', 'Bluish', 'Reddish', 'Purplish'];
    //FANCY_COLOR = ['Yellow', 'Pink', 'Red', 'Green', 'Orange', 'Violet', 'Brown', 'Gray', 'Blue', 'Purple'];

    INTENSITY = ['W-X', 'Y-Z', 'FAINT', 'VERY LIGHT', 'LIGHT', 'FANCY LIGHT', 'FANCY', 'FANCY INTENSE', 'FANCY GREEN', 'FANCY DARK',
        'FANCY DEEP', 'FANCY VIVID', 'FANCY FAINT', 'DARK'];
    OVERTONE = ['NONE', 'BROWNISH', 'GRAYISH', 'GREENISH', 'YELLOWISH', 'PINKISH', 'ORANGEY', 'BLUISH', 'REDDISH', 'PURPLISH'];
    FANCY_COLOR = ['YELLOW', 'PINK', 'RED', 'GREEN', 'ORANGE', 'VIOLET', 'BROWN', 'GRAY', 'BLUE', 'PURPLE'];

    INTENSITY.sort();
    OVERTONE.sort();
    FANCY_COLOR.sort();
    INTENSITY.unshift("ALL SELECTED");
    OVERTONE.unshift("ALL SELECTED");
    FANCY_COLOR.unshift("ALL SELECTED");

    for (var i = 0; i <= INTENSITY.length - 1; i++) {
        $('#ddl_INTENSITY').append('<div class="col-12 pl-0 pr-0 ng-scope">'
            + '<ul class="row m-0">'
            + '<li class="carat-dropdown-chkbox">'
            + '<div class="main-cust-check">'
            + '<label class="cust-rdi-bx mn-check">'
            + '<input type="checkbox" class="checkradio f_clr_clk" id="CHK_I_' + i + '" name="CHK_I_' + i + '" onclick="GetCheck_INTENSITY_List(\'' + INTENSITY[i] + '\',' + i + ');">'
            + '<span class="cust-rdi-check">'
            + '<i class="fa fa-check"></i>'
            + '</span>'
            + '</label>'
            + '</div>'
            + '</li>'
            + '<li class="col" style="text-align: left;margin-left: -15px;">'
            + '<span>' + INTENSITY[i] + '</span>'
            + '</li>'
            + '</ul>'
            + '</div>');
    }
    $('#ddl_INTENSITY').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');

    for (var j = 0; j <= OVERTONE.length - 1; j++) {
        $('#ddl_OVERTONE').append('<div class="col-12 pl-0 pr-0 ng-scope">'
            + '<ul class="row m-0">'
            + '<li class="carat-dropdown-chkbox">'
            + '<div class="main-cust-check">'
            + '<label class="cust-rdi-bx mn-check">'
            + '<input type="checkbox" class="checkradio f_clr_clk" id="CHK_O_' + j + '" name="CHK_O_' + j + '" onclick="GetCheck_OVERTONE_List(\'' + OVERTONE[j] + '\',' + j + ');">'
            + '<span class="cust-rdi-check">'
            + '<i class="fa fa-check"></i>'
            + '</span>'
            + '</label>'
            + '</div>'
            + '</li>'
            + '<li class="col" style="text-align: left;margin-left: -15px;">'
            + '<span>' + OVERTONE[j] + '</span>'
            + '</li>'
            + '</ul>'
            + '</div>');
    }
    $('#ddl_OVERTONE').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');

    for (var k = 0; k <= FANCY_COLOR.length - 1; k++) {
        $('#ddl_FANCY_COLOR').append('<div class="col-12 pl-0 pr-0 ng-scope">'
            + '<ul class="row m-0">'
            + '<li class="carat-dropdown-chkbox">'
            + '<div class="main-cust-check">'
            + '<label class="cust-rdi-bx mn-check">'
            + '<input type="checkbox" class="checkradio f_clr_clk" id="CHK_F_' + k + '" name="CHK_F_' + k + '" onclick="GetCheck_FANCY_COLOR_List(\'' + FANCY_COLOR[k] + '\',' + k + ');" style="cursor:pointer;">'
            + '<span class="cust-rdi-check">'
            + '<i class="fa fa-check"></i>'
            + '</span>'
            + '</label>'
            + '</div>'
            + '</li>'
            + '<li class="col" style="text-align: left;margin-left: -15px;">'
            + '<span>' + FANCY_COLOR[k] + '</span>'
            + '</li>'
            + '</ul>'
            + '</div>');
    }
    $('#ddl_FANCY_COLOR').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');
}
function GetCheck_INTENSITY_List(item, id) {
    var res = _.filter(Check_Color_1, function (e) { return (e.Symbol == item) });
    if (id == "0") {
        Check_Color_1 = [];
        if ($("#CHK_I_0").prop("checked") == true) {
            for (var i = 1; i <= INTENSITY.length - 1; i++) {
                Check_Color_1.push({
                    "NewID": Check_Color_1.length + 1,
                    "Symbol": INTENSITY[i],
                });
                $("#CHK_I_" + i).prop('checked', true);
            }
        }
        else {
            for (var i = 0; i <= INTENSITY.length - 1; i++) {
                $("#CHK_I_" + i).prop('checked', false);
            }
        }
        $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
    }
    else {
        $("#CHK_I_0").prop('checked', false);
        if (res.length == 0) {
            Check_Color_1.push({
                "NewID": Check_Color_1.length + 1,
                "Symbol": item,
            });
        }
        else {
            for (var i = 0; i <= Check_Color_1.length - 1; i++) {
                if (Check_Color_1[i].Symbol == item) {
                    $("#CHK_I_" + id).prop('checked', false);
                    Check_Color_1.splice(i, 1);
                }
            }
        }
        if (INTENSITY.length - 1 == Check_Color_1.length) {
            $("#CHK_I_0").prop('checked', true);
        }
        $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
    }

    setTimeout(function () {
        $("#sym-sec1 .carat-dropdown-main").show();
    }, 2);
}
function GetCheck_OVERTONE_List(item, id) {
    var res = _.filter(Check_Color_2, function (e) { return (e.Symbol == item) });
    if (id == "0") {
        Check_Color_2 = [];
        if ($("#CHK_O_0").prop("checked") == true) {
            for (var i = 1; i <= OVERTONE.length - 1; i++) {
                Check_Color_2.push({
                    "NewID": Check_Color_2.length + 1,
                    "Symbol": OVERTONE[i],
                });
                $("#CHK_O_" + i).prop('checked', true);
            }
        }
        else {
            for (var i = 0; i <= OVERTONE.length - 1; i++) {
                $("#CHK_O_" + i).prop('checked', false);
            }
        }
        $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
    }
    else {
        $("#CHK_O_0").prop('checked', false);
        if (res.length == 0) {
            Check_Color_2.push({
                "NewID": Check_Color_2.length + 1,
                "Symbol": item,
            });
        }
        else {
            for (var i = 0; i <= Check_Color_2.length - 1; i++) {
                if (Check_Color_2[i].Symbol == item) {
                    $("#CHK_O_" + id).prop('checked', false);
                    Check_Color_2.splice(i, 1);
                }
            }
        }
        if (OVERTONE.length - 1 == Check_Color_2.length) {
            $("#CHK_O_0").prop('checked', true);
        }
        $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
    }
    setTimeout(function () {
        $("#sym-sec2 .carat-dropdown-main").show();
    }, 2);
}
function GetCheck_FANCY_COLOR_List(item, id) {
    var res = _.filter(Check_Color_3, function (e) { return (e.Symbol == item) });
    if (id == "0") {
        Check_Color_3 = [];
        if ($("#CHK_F_0").prop("checked") == true) {
            for (var i = 1; i <= FANCY_COLOR.length - 1; i++) {
                Check_Color_3.push({
                    "NewID": Check_Color_3.length + 1,
                    "Symbol": FANCY_COLOR[i],
                });
                $("#CHK_F_" + i).prop('checked', true);
            }
        }
        else {
            for (var i = 0; i <= FANCY_COLOR.length - 1; i++) {
                $("#CHK_F_" + i).prop('checked', false);
            }
        }
        $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
    }
    else {
        $("#CHK_F_0").prop('checked', false);
        if (res.length == 0) {
            Check_Color_3.push({
                "NewID": Check_Color_3.length + 1,
                "Symbol": item,
            });
        }
        else {
            for (var i = 0; i <= Check_Color_3.length - 1; i++) {
                if (Check_Color_3[i].Symbol == item) {
                    $("#CHK_F_" + id).prop('checked', false);
                    Check_Color_3.splice(i, 1);
                }
            }
        }
        if (FANCY_COLOR.length - 1 == Check_Color_3.length) {
            $("#CHK_F_0").prop('checked', true);
        }
        $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
    }
    setTimeout(function () {
        $("#sym-sec3 .carat-dropdown-main").show();
    }, 2);
}
function resetINTENSITY() {
    Check_Color_1 = [];
    $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
    $('#ddl_INTENSITY input[type="checkbox"]').prop('checked', false);
    C1 = 1;
    INTENSITYShow();
}
function resetOVERTONE() {
    Check_Color_2 = [];
    $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
    $('#ddl_OVERTONE input[type="checkbox"]').prop('checked', false);
    C2 = 1;
    OVERTONEShow();
}
function resetFANCY_COLOR() {
    Check_Color_3 = [];
    $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
    $('#ddl_FANCY_COLOR input[type="checkbox"]').prop('checked', false);
    C3 = 1;
    FANCY_COLORShow();
}
function Color_Hide_Show(type) {
    if (type == '1' || type == '3') {
        $("#div_Color").show();
        $("#div_Fancy_Color").hide();
        $("#Color_Hide_Show_1").addClass("active");
        $("#Color_Hide_Show_2").removeClass("active");
        Color_Type = "Regular";
    }
    else if (type == '2' || type == '4') {
        $("#div_Color").hide();
        $("#div_Fancy_Color").show();
        $("#Color_Hide_Show_3").removeClass("active");
        $("#Color_Hide_Show_3").removeClass("active_btn_active");
        $("#Color_Hide_Show_4").addClass("active");
        $("#Color_Hide_Show_4").addClass("active_btn_active");
        Color_Type = "Fancy";
        $(".carat-dropdown-main").hide();
        C1 = 0, C2 = 0, C3 = 0;
    }
}


$(document).ready(function () {
    // For Shape selection
    var icon_selected = new Array();
    $('ul.search').on('click', ".common-ico", function () {

        var aa = $(this)
        if (!aa.is('.active')) {
            aa.addClass('active');

            var my_id = this.id;
            icon_selected.push(my_id);

        } else {
            aa.removeClass('active');
            var my_id = this.id;
            var index = icon_selected.indexOf(my_id);
            if (index > -1) {
                icon_selected.splice(index, 1);
            }
        }
    });

    var li_selected = new Array();
    $('ul.common-li').on('click', "li", function () {

        var ab = $(this)
        if (!ab.hasClass('active')) {
            ab.addClass('active');

            var l_id = this.id;
            li_selected.push(l_id);

        } else {
            ab.removeClass('active');
            var l_id = this.id;
            var index = li_selected.indexOf(l_id);
            if (index > -1) {
                li_selected.splice(index, 1);
            }
        }
    });

    //$(".numeric").numeric({ decimal: ".", negative: true, decimalPlaces: 2 });

    GetSearchParameter();

    GetTransId();

    FcolorBind();
    $('#tabhome').click(function () {
        IsFiltered = true;
    });
    $('#ddl_INTENSITY').click(function () {
        setTimeout(function () {
            if (IsFiltered == true) {
                $("#sym-sec1 .carat-dropdown-main").show();
            }
        }, 0.1);
    });
    $('#ddl_OVERTONE').click(function () {
        setTimeout(function () {
            if (IsFiltered == true) {
                $("#sym-sec2 .carat-dropdown-main").show();
            }
        }, 0.1);
    });
    $('#ddl_FANCY_COLOR').click(function () {
        setTimeout(function () {
            if (IsFiltered == true) {
                $("#sym-sec3 .carat-dropdown-main").show();
            }
        }, 0.1);
    });
    BindKeyToSymbolList();
    $('#divGridView li a.download-popup').on('click', function (event) {
        $('.download-toggle').toggleClass('active');
        event.stopPropagation();
    });

    $('#ExcelModal').on('show.bs.modal', function (event) {
        var count = gridOptions.api.getSelectedRows().length;
        if (count > 0) {
            $('#customRadio4').prop('checked', true);
        } else {
            $('#customRadio3').prop('checked', true);
        }
    });
});
SetCutMaster = function (item) {
    _.each(CutList, function (itm) {
        $('#searchcut li[onclick="SetActive(\'CUT\',\'' + itm.Value + '\')"]').removeClass('active');
        itm.ACTIVE = false;
    });
    _.each(PolishList, function (itm) {
        $('#searchpolish li[onclick="SetActive(\'POLISH\',\'' + itm.Value + '\')"]').removeClass('active');
        itm.ACTIVE = false;
    });
    _.each(SymList, function (itm) {
        $('#searchsymm li[onclick="SetActive(\'SYMM\',\'' + itm.Value + '\')"]').removeClass('active');
        itm.ACTIVE = false;
    });
    if (item == '3EX' && !$('#li3ex').hasClass('active')) {
        $('#li3vg').removeClass('active');

        _.each(_.filter(CutList, function (e) { return e.Value == "EX" || e.Value == "3EX" }), function (itm) {
            $('#searchcut li[onclick="SetActive(\'CUT\',\'' + itm.Value + '\')"]').addClass('active');
            itm.ACTIVE = true;
        });
        _.each(_.filter(PolishList, function (e) { return e.Value == "EX" }), function (itm) {
            $('#searchpolish li[onclick="SetActive(\'POLISH\',\'EX\')"]').addClass('active');
            itm.ACTIVE = true;
        });
        _.each(_.filter(SymList, function (e) { return e.Value == "EX" }), function (itm) {
            $('#searchsymm li[onclick="SetActive(\'SYMM\',\'EX\')"]').addClass('active');
            itm.ACTIVE = true;
        });
    }
    else if (item == '3VG' && !$('#li3vg').hasClass('active')) {

        $('#li3ex').removeClass('active');
        _.each(_.filter(CutList, function (e) { return e.Value == "EX" || e.Value == "VG" || e.Value == "3EX" }), function (itm) {
            $('#searchcut li[onclick="SetActive(\'CUT\',\'' + itm.Value + '\')"]').addClass('active');
            itm.ACTIVE = true;
        });
        _.each(_.filter(PolishList, function (e) { return e.Value == "EX" || e.Value == "VG" }), function (itm) {
            $('#searchpolish li[onclick="SetActive(\'POLISH\',\'' + itm.Value + '\')"]').addClass('active');
            itm.ACTIVE = true;
        });
        _.each(_.filter(SymList, function (e) { return e.Value == "EX" || e.Value == "VG" }), function (itm) {
            $('#searchsymm li[onclick="SetActive(\'SYMM\',\'' + itm.Value + '\')"]').addClass('active');
            itm.ACTIVE = true;
        });
    }
}
function NewSizeGroup() {
    fcarat = $('#txtfromcarat').val();
    tcarat = $('#txttocarat').val();

    if (fcarat == "" && tcarat == "" || fcarat == 0 && tcarat == 0) {
        toastr.warning("Please Enter Carat.");
        return false;
    }
    if (fcarat == "") {
        fcarat = "0";
    }
    var SizeGroupList_ = [];
    SizeGroupList_.push({
        "NewID": SizeGroupList.length + 1,
        "FromCarat": fcarat,
        "ToCarat": tcarat,
        "Size": fcarat + '-' + tcarat,

    });
    var lst = _.filter(SizeGroupList, function (e) { return (e.Size == SizeGroupList_[0].Size) });
    if (lst.length == 0) {
        SizeGroupList.push({
            "NewID": SizeGroupList_[0].NewID,
            "FromCarat": SizeGroupList_[0].FromCarat,
            "ToCarat": SizeGroupList_[0].ToCarat,
            "Size": SizeGroupList_[0].Size,
        });
        //<li class="carat-li-top">1.00-1.00<i class="fa fa-plus-circle" aria-hidden="true"></i></li>
        $('#searchcaratspecific').append('<li id="' + SizeGroupList_[0].NewID + '" class="carat-li-top">' + SizeGroupList_[0].Size + '<i class="fa fa-times-circle" aria-hidden="true" onclick="NewSizeGroupRemove(' + SizeGroupList_[0].NewID + ');"></i></li>');

        $('#txtfromcarat').val("");
        $('#txttocarat').val("");
    }
    else {
        $('#txtfromcarat').val("");
        $('#txttocarat').val("");
        toastr.warning("Carat is already exist.");
    }
    //SetSearchParameter();
}
function NewSizeGroupRemove(id) {
    $('#' + id).remove();
    var SList = _.reject(SizeGroupList, function (e) { return e.NewID == id });
    SizeGroupList = SList;
    //SetSearchParameter();
}

function setFromCarat() {
    if ($('#txtfromcarat').val() != "") {
        $('#txtfromcarat').val(parseFloat($('#txtfromcarat').val()).toFixed(2));
        $('#txttocarat').val(parseFloat($('#txtfromcarat').val()).toFixed(2));
    } else {
        $('#txtfromcarat').val("0");
    }
    if ($('#txttocarat').val() == "") {
        $('#txttocarat').val("0");
    }
}
function setToCarat() {
    if ($('#txttocarat').val() != "") {
        $('#txttocarat').val(parseFloat($('#txttocarat').val()).toFixed(2));
    } else {
        $('#txttocarat').val("0");
    }
    if ($('#txtfromcarat').val() == "") {
        $('#txtfromcarat').val("0");
    }
}

function isNumberKey_ISD(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        if (charCode == 45) {
            return true;
        }
        return false;
    }
    return true;
}

function GetSearchParameter() {
    loaderShow();

    $.ajax({
        url: "/User/Get_PriceListCategory",
        async: false,
        type: "POST",
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1" && data.Data != null) {
                ParameterList = data.Data;
                //if (ParameterList.length > 0) {
                //    _.each(ParameterList, function (itm) {
                //        itm.ACTIVE = false;
                //    });
                //}
                //if (ParameterList.length > 0) {
                //    _.each(ParameterList, function (itm) {
                //        itm.ACTIVE = false;
                //    });
                //    ParameterList.push({
                //        Id: 12, Value: "OTHERS", ListType: "SHAPE",
                //        UrlValue: "https://sunrisediamonds.com.hk/Images/Shape/ROUND.svg",
                //        UrlValueHov: "https://sunrisediamonds.com.hk/Images/Shape/ROUND_Trans.png"
                //    })
                //    ParameterList.push({
                //        Id: 13, Value: "ALL", ListType: "SHAPE",
                //        UrlValue: "",
                //        UrlValueHov: ""
                //    })

                //    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "BGM", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                //    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "TABLE_INCL", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                //    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "TABLE_NATTS", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                //    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "CROWN_INCL", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                //    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "CROWN_NATTS", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                //    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "TABLEOPEN", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                //    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "CROWNOPEN", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                //    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "PAVILIONOPEN", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                //    ParameterList.push({ Id: 4, Value: "BLANK", ListType: "GIRDLEOPEN", UrlValue: "", UrlValueHov: "", ACTIVE: false })
                //}

                $('#searchcaratgen').html("");
                CaratList = _.filter(ParameterList, function (e) { return e.Type == 'Pointer' });
                _(CaratList).each(function (carat, i) {
                    $('#searchcaratgen').append('<li onclick="SetActive(\'CARAT\',\'' + carat.Value + '\')">' + carat.Value + '</li>');
                });

                //$('#searchshape').html("");
                //ShapeList = _.filter(ParameterList, function (e) { return e.Type == 'Shape' });
                //_(ShapeList).each(function (shape, i) {
                //    $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'Shape\',\'' + shape.Value + '\')" class="common-ico"><div class="icon-image one"><img src="' + shape.UrlValue + '" class="first-ico"><img src="' + shape.UrlValueHov + '" class="second-ico"></div><span>' + shape.Value + '</span></a></li>');
                //});

                ParameterList.push({
                    Id: 0, Value: "ALL", SORT_NO: 1, Type: "Shape", isActive: false, Col_Id: 1
                })

                $('#searchshape').html("");
                $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'SHAPE\',\'' + 'ALL' + '\')" class="common-ico"><div class="icon-image one"><span class="first-ico">ALL</span></div></a></li>');
                //$('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'Shape\',\'' + 'ALL' + '\')" class="common-ico"><div class="icon-image one"><span class="first-ico">ALL</span></div><span>ALL</span></a></li>');

                ShapeList = _.filter(ParameterList, function (e) { return e.Type == 'Shape' });
                _(ShapeList).each(function (shape, i) {
                    if (shape.Value != 'ALL') {
                        $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'SHAPE\',\'' + shape.Value + '\')" class="common-ico"><div class="icon-image one"><img src="https://sunrisediamonds.com.hk/Images/Shape/ROUND.svg" class="first-ico"><img src="https://sunrisediamonds.com.hk/Images/Shape/ROUND_Trans.png" class="second-ico"></div><span>' + shape.Value + '</span></a></li>');
                    }
                });


                $('#searchcolor').html("");
                ColorList = _.filter(ParameterList, function (e) { return e.Type == 'Color' });
                _(ColorList).each(function (color, i) {
                    $('#searchcolor').append('<li onclick="SetActive(\'COLOR\',\'' + color.Value + '\')">' + color.Value + '</li>');
                });

                $('#searchclarity').html("");
                ClarityList = _.filter(ParameterList, function (e) { return e.Type == 'Clarity' });
                _(ClarityList).each(function (clarity, i) {
                    $('#searchclarity').append('<li onclick="SetActive(\'CLARITY\',\'' + clarity.Value + '\')">' + clarity.Value + '</li>');
                });

                $('#searchcut').html("");
                CutList = _.filter(ParameterList, function (e) { return e.Type == 'Cut' });
                _(CutList).each(function (cut, i) {
                    $('#searchcut').append('<li onclick="SetActive(\'CUT\',\'' + cut.Value + '\')">' + (cut.Value == "FR" ? "F" : cut.Value) + '</li>');
                });

                $('#searchpolish').html("");
                PolishList = _.filter(ParameterList, function (e) { return e.Type == 'Polish' });
                _(PolishList).each(function (polish, i) {
                    $('#searchpolish').append('<li onclick="SetActive(\'POLISH\',\'' + polish.Value + '\')">' + polish.Value + '</li>');
                });

                $('#searchsymm').html("");
                SymList = _.filter(ParameterList, function (e) { return e.Type == 'Symm' });
                _(SymList).each(function (sym, i) {
                    $('#searchsymm').append('<li onclick="SetActive(\'SYMM\',\'' + sym.Value + '\')">' + sym.Value + '</li>');
                });

                $('#searchfls').html("");
                FlouList = _.filter(ParameterList, function (e) { return e.Type == 'Fls' });
                _(FlouList).each(function (fls, i) {
                    $('#searchfls').append('<li onclick="SetActive(\'FLS\',\'' + fls.Value + '\')">' + fls.Value + '</li>');
                });

                $('#searchbgm').html("");
                BGMList = _.filter(ParameterList, function (e) { return e.Type == 'BGM' });
                _(BGMList).each(function (bgm, i) {
                    $('#searchbgm').append('<li onclick="SetActive(\'BGM\',\'' + bgm.Value + '\')">' + bgm.Value + '</li>');
                });

                $('#searchlab').html("");
                LabList = _.filter(ParameterList, function (e) { return e.Type == 'Lab' });
                _(LabList).each(function (lab, i) {
                    $('#searchlab').append('<li onclick="SetActive(\'LAB\',\'' + lab.Value + '\')">' + lab.Value + '</li>');
                });

                $('#searchtableincl').html("");
                TblInclList = _.filter(ParameterList, function (e) { return e.Type == 'Table Inclusion' });
                _(TblInclList).each(function (tblincl, i) {
                    $('#searchtableincl').append('<li onclick="SetActive(\'TABLE_INCL\',\'' + tblincl.Value + '\')">' + tblincl.Value + '</li>');
                });

                $('#searchtablenatts').html("");
                TblNattsList = _.filter(ParameterList, function (e) { return e.Type == 'Table Natts' });
                _(TblNattsList).each(function (tblnatts, i) {
                    $('#searchtablenatts').append('<li onclick="SetActive(\'TABLE_NATTS\',\'' + tblnatts.Value + '\')">' + tblnatts.Value + '</li>');
                });

                $('#searchcrownincl').html("");
                CrwnInclList = _.filter(ParameterList, function (e) { return e.Type == 'Crown Inclusion' });
                _(CrwnInclList).each(function (crwnincl, i) {
                    $('#searchcrownincl').append('<li onclick="SetActive(\'CROWN_INCL\',\'' + crwnincl.Value + '\')">' + crwnincl.Value + '</li>');
                });

                $('#searchcrownnatts').html("");
                CrwnNattsList = _.filter(ParameterList, function (e) { return e.Type == 'Crown Natts' });
                _(CrwnNattsList).each(function (crwnnatt, i) {
                    $('#searchcrownnatts').append('<li onclick="SetActive(\'CROWN_NATTS\',\'' + crwnnatt.Value + '\')">' + crwnnatt.Value + '</li>');
                });

                $('#searchtableopen').html("");
                TableOpenList = _.filter(ParameterList, function (e) { return e.Type == 'Table Open' });
                _(TableOpenList).each(function (tableopen, i) {
                    $('#searchtableopen').append('<li onclick="SetActive(\'TABLEOPEN\',\'' + tableopen.Value + '\')">' + tableopen.Value + '</li>');
                });

                $('#searchcrownopen').html("");
                CrownOpenList = _.filter(ParameterList, function (e) { return e.Type == 'Crown Open' });
                _(CrownOpenList).each(function (crownopen, i) {
                    $('#searchcrownopen').append('<li onclick="SetActive(\'CROWNOPEN\',\'' + crownopen.Value + '\')">' + crownopen.Value + '</li>');
                });

                $('#searchpavopen').html("");
                PavOpenList = _.filter(ParameterList, function (e) { return e.Type == 'Pav Open' });
                _(PavOpenList).each(function (pavopen, i) {
                    $('#searchpavopen').append('<li onclick="SetActive(\'PAVILIONOPEN\',\'' + pavopen.Value + '\')">' + pavopen.Value + '</li>');
                });

                $('#searchgirdleopen').html("");
                GirdleOpenList = _.filter(ParameterList, function (e) { return e.Type == 'Girdle Open' });
                _(GirdleOpenList).each(function (girdleopen, i) {
                    $('#searchgirdleopen').append('<li onclick="SetActive(\'GIRDLEOPEN\',\'' + girdleopen.Value + '\')">' + girdleopen.Value + '</li>');
                });

                KeyToSymbolList = _.filter(ParameterList, function (e) { return e.Type == 'Key To Symboll' });


                loaderHide();
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {

        }
    });
}

var ddlTransObj = null;
function GetTransId() {
    if (ddlTransObj != null) {
        $("#ddlSupplierId").multiselect('destroy');
    }
    $("#ddlSupplierId").html("");

    var obj = {};
    obj.OrderBy = "SupplierName asc";
    $.ajax({
        url: "/User/Get_SupplierMaster",
        async: false,
        type: "POST",
        data: { req: obj },
        success: function (data, textStatus, jqXHR) {
            if (data != null && data.Data.length > 0) {
                for (var k in data.Data) {
                    $("#ddlSupplierId").append("<option value='" + data.Data[k].Id + "'>" + data.Data[k].SupplierName + "</option>");
                }
            }
            ddlTransObj = $('#ddlSupplierId').multiselect({
                includeSelectAllOption: true
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
function SetActive(flag, value) {
    if (flag == "SHAPE") {
        if (value == "ALL") {
            if (_.find(ShapeList, function (num) { return num.isActive == true && num.Value == value; })) {
                _(ShapeList).each(function (shape, i) {
                    shape.isActive = false;
                });
                $("#searchshape").find('.common-ico').each(function (i, shape) {
                    if ($($(shape).find('span')[0]).text() != 'ALL') {
                        $(shape).removeClass('active');
                    }
                });
            } else {
                _(ShapeList).each(function (shape, i) {
                    shape.isActive = true;
                });
                //$("#searchshape").find('.common-ico').addClass('active');
                $("#searchshape").find('.common-ico').each(function (i, shape) {
                    if ($($(shape).find('span')[0]).text() != 'ALL') {
                        $(shape).addClass('active');
                    }
                });
            }
        }
        else {
            if (_.find(ShapeList, function (num) { return num.isActive == true && num.Value == value; })) {
                _.findWhere(ShapeList, { Value: value }).isActive = false;

                if (_.find(ShapeList, function (num) { return num.isActive == true && num.Value == "ALL"; })) {
                    _.findWhere(ShapeList, { Value: "ALL" }).isActive = false;

                    $("#searchshape").find('.common-ico').each(function (i, shape) {
                        if ($($(shape).find('span')[0]).text() == 'ALL') {
                            $(shape).removeClass('active');
                        }
                    });
                }
            } else {
                _.findWhere(ShapeList, { Value: value }).isActive = true;
                var isAllActive = true;
                _(ShapeList).each(function (shape, i) {
                    if (shape.Value != "ALL" && shape.isActive != true) {
                        isAllActive = false;
                    }
                });

                if (isAllActive) {
                    if (_.find(ShapeList, function (num) { return num.isActive == false && num.Value == "ALL"; })) {
                        _.findWhere(ShapeList, { Value: "ALL" }).isActive = true;

                        $("#searchshape").find('.common-ico').each(function (i, shape) {
                            if ($($(shape).find('span')[0]).text() == 'ALL') {
                                $(shape).addClass('active');
                            }
                        });
                    }
                }
            }
        }
    }
    //if (flag == "Shape") {
    //    if (_.find(ShapeList, function (num) { return num.ACTIVE == true && num.Value == value; })) {
    //        _.findWhere(ShapeList, { Value: value }).ACTIVE = false;
    //    } else {
    //        _.findWhere(ShapeList, { Value: value }).ACTIVE = true;
    //    }
    //}
    else if (flag == "COLOR") {
        if (_.find(ColorList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(ColorList, { Value: value }).isActive = false;
        } else {
            _.findWhere(ColorList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "CLARITY") {
        if (_.find(ClarityList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(ClarityList, { Value: value }).isActive = false;
        } else {
            _.findWhere(ClarityList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "CUT") {
        if (_.find(CutList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(CutList, { Value: value }).isActive = false;
        } else {
            _.findWhere(CutList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "LAB") {
        if (_.find(LabList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(LabList, { Value: value }).isActive = false;
        } else {
            _.findWhere(LabList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "POLISH") {
        if (_.find(PolishList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(PolishList, { Value: value }).isActive = false;
        } else {
            _.findWhere(PolishList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "SYMM") {
        if (_.find(SymList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(SymList, { Value: value }).isActive = false;
        } else {
            _.findWhere(SymList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "FLS") {
        if (_.find(FlouList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(FlouList, { Value: value }).isActive = false;
        } else {
            _.findWhere(FlouList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "BGM") {
        if (_.find(BGMList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(BGMList, { Value: value }).isActive = false;
        } else {
            _.findWhere(BGMList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "TABLE_INCL") {
        if (_.find(TblInclList, function (num) { return num.isActive == true; })) {
            _.findWhere(TblInclList, { Value: value }).isActive = false;
        } else {
            _.findWhere(TblInclList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "TABLE_NATTS") {
        if (_.find(TblNattsList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(TblNattsList, { Value: value }).isActive = false;
        } else {
            _.findWhere(TblNattsList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "CROWN_INCL") {
        if (_.find(CrwnInclList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(CrwnInclList, { Value: value }).isActive = false;
        } else {
            _.findWhere(CrwnInclList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "CROWN_NATTS") {
        if (_.find(CrwnNattsList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(CrwnNattsList, { Value: value }).isActive = false;
        } else {
            _.findWhere(CrwnNattsList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "CARAT") {
        if (_.find(CaratList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(CaratList, { Value: value }).isActive = false;
        } else {
            _.findWhere(CaratList, { Value: value }).isActive = true;
        }

        $(CaratList).each(function (i, res) {
            if (_.find(CaratList, function (num) { return num.Value == res.Value && num.isActive == true; })) {
                $('#searchcaratgen li[onclick="SetActive(\'CARAT\',\'' + res.Value + '\')"]').addClass('active');
            }
            else {
                $('#searchcaratgen li[onclick="SetActive(\'CARAT\',\'' + res.Value + '\')"]').removeClass('active');
            }
        });
        $('a[href="#carat2"]').click()
    }
    else if (flag == "TABLEOPEN") {
        if (_.find(TableOpenList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(TableOpenList, { Value: value }).isActive = false;
        } else {
            _.findWhere(TableOpenList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "CROWNOPEN") {
        if (_.find(CrownOpenList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(CrownOpenList, { Value: value }).isActive = false;
        } else {
            _.findWhere(CrownOpenList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "PAVILIONOPEN") {
        if (_.find(PavOpenList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(PavOpenList, { Value: value }).isActive = false;
        } else {
            _.findWhere(PavOpenList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "GIRDLEOPEN") {
        if (_.find(GirdleOpenList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(GirdleOpenList, { Value: value }).isActive = false;
        } else {
            _.findWhere(GirdleOpenList, { Value: value }).isActive = true;
        }
    }
    else if (flag == "KTSBlank") {
        if (KTSBlank) {
            KTSBlank = false;
            $("#KTSBlank").removeClass("active");
        } else {
            KTSBlank = true;
            $("#KTSBlank").addClass("active");
        }
    }
    else if (flag == "LengthBlank") {
        if (LengthBlank) {
            LengthBlank = false;
            $("#LengthBlank").removeClass("active");
        } else {
            LengthBlank = true;
            $("#LengthBlank").addClass("active");
        }
    }
    else if (flag == "WidthBlank") {
        if (WidthBlank) {
            WidthBlank = false;
            $("#WidthBlank").removeClass("active");
        } else {
            WidthBlank = true;
            $("#WidthBlank").addClass("active");
        }
    }
    else if (flag == "DepthBlank") {
        if (DepthBlank) {
            DepthBlank = false;
            $("#DepthBlank").removeClass("active");
        } else {
            DepthBlank = true;
            $("#DepthBlank").addClass("active");
        }
    }
    else if (flag == "DepthPerBlank") {
        if (DepthPerBlank) {
            DepthPerBlank = false;
            $("#DepthPerBlank").removeClass("active");
        } else {
            DepthPerBlank = true;
            $("#DepthPerBlank").addClass("active");
        }
    }
    else if (flag == "TablePerBlank") {
        if (TablePerBlank) {
            TablePerBlank = false;
            $("#TablePerBlank").removeClass("active");
        } else {
            TablePerBlank = true;
            $("#TablePerBlank").addClass("active");
        }
    }
    else if (flag == "CrAngBlank") {
        if (CrAngBlank) {
            CrAngBlank = false;
            $("#CrAngBlank").removeClass("active");
        } else {
            CrAngBlank = true;
            $("#CrAngBlank").addClass("active");
        }
    }
    else if (flag == "CrHtBlank") {
        if (CrHtBlank) {
            CrHtBlank = false;
            $("#CrHtBlank").removeClass("active");
        } else {
            CrHtBlank = true;
            $("#CrHtBlank").addClass("active");
        }
    }
    else if (flag == "PavAngBlank") {
        if (PavAngBlank) {
            PavAngBlank = false;
            $("#PavAngBlank").removeClass("active");
        } else {
            PavAngBlank = true;
            $("#PavAngBlank").addClass("active");
        }
    }
    else if (flag == "PavHtBlank") {
        if (PavHtBlank) {
            PavHtBlank = false;
            $("#PavHtBlank").removeClass("active");
        } else {
            PavHtBlank = true;
            $("#PavHtBlank").addClass("active");
        }
    }
}
function Reset() {
    GetTransId();
    $("#txtRefNo").val("");
    _.map(ShapeList, function (data) { return data.ACTIVE = false; });
    _.map(ColorList, function (data) { return data.ACTIVE = false; });
    _.map(ClarityList, function (data) { return data.ACTIVE = false; });
    _.map(CutList, function (data) { return data.ACTIVE = false; });
    _.map(PolishList, function (data) { return data.ACTIVE = false; });
    _.map(SymList, function (data) { return data.ACTIVE = false; });
    _.map(FlouList, function (data) { return data.ACTIVE = false; });
    _.map(BGMList, function (data) { return data.ACTIVE = false; });

    _.map(LabList, function (data) { return data.ACTIVE = false; });
    _.map(TblInclList, function (data) { return data.ACTIVE = false; });
    _.map(TblNattsList, function (data) { return data.ACTIVE = false; });
    _.map(CrwnInclList, function (data) { return data.ACTIVE = false; });
    _.map(CrwnNattsList, function (data) { return data.ACTIVE = false; });
    _.map(CaratList, function (data) { return data.ACTIVE = false; });

    $('#SearchImage').removeClass('active');
    $('#SearchVideo').removeClass('active');
    $('#SearchCerti').removeClass('active');

    $('#li3ex').removeClass('active');
    $('#li3vg').removeClass('active');

    SizeGroupList = [];
    $('#searchcaratspecific').html("");
    $('a[href="#carat1"]').click();
    $('#searchcaratgen').html("");
    _(CaratList).each(function (carat, i) {
        $('#searchcaratgen').append('<li onclick="SetActive(\'CARAT\',\'' + carat.Value + '\')">' + carat.Value + '</li>');
    });

    $('#searchshape').html("");
    $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'SHAPE\',\'' + 'ALL' + '\')" class="common-ico"><div class="icon-image one"><span class="first-ico">ALL</span></div><span>ALL</span></a></li>');

    _(ShapeList).each(function (shape, i) {
        if (shape.Value != 'ALL') {
            $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'SHAPE\',\'' + shape.Value + '\')" class="common-ico"><div class="icon-image one"><img src="https://sunrisediamonds.com.hk/Images/Shape/ROUND.svg" class="first-ico"><img src="https://sunrisediamonds.com.hk/Images/Shape/ROUND_Trans.png" class="second-ico"></div><span>' + shape.Value + '</span></a></li>');
        }
    });

    //$('#searchshape').html("");
    //_(ShapeList).each(function (shape, i) {
    //    $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'Shape\',\'' + shape.Value + '\')" class="common-ico"><div class="icon-image one"><img src="' + shape.UrlValue + '" class="first-ico"><img src="' + shape.UrlValueHov + '" class="second-ico"></div><span>' + shape.Value + '</span></a></li>');
    //});

    $('#searchcolor').html("");
    _(ColorList).each(function (color, i) {
        $('#searchcolor').append('<li onclick="SetActive(\'COLOR\',\'' + color.Value + '\')">' + color.Value + '</li>');
    });

    $('#searchclarity').html("");
    _(ClarityList).each(function (clarity, i) {
        $('#searchclarity').append('<li onclick="SetActive(\'CLARITY\',\'' + clarity.Value + '\')">' + clarity.Value + '</li>');
    });

    $('#searchcut').html("");
    _(CutList).each(function (cut, i) {
        $('#searchcut').append('<li onclick="SetActive(\'CUT\',\'' + cut.Value + '\')">' + (cut.Value == "FR" ? "F" : cut.Value) + '</li>');
    });

    $('#searchpolish').html("");
    _(PolishList).each(function (polish, i) {
        $('#searchpolish').append('<li onclick="SetActive(\'POLISH\',\'' + polish.Value + '\')">' + polish.Value + '</li>');
    });

    $('#searchsymm').html("");
    _(SymList).each(function (sym, i) {
        $('#searchsymm').append('<li onclick="SetActive(\'SYMM\',\'' + sym.Value + '\')">' + sym.Value + '</li>');
    });

    $('#searchfls').html("");
    _(FlouList).each(function (fls, i) {
        $('#searchfls').append('<li onclick="SetActive(\'FLS\',\'' + fls.Value + '\')">' + fls.Value + '</li>');
    });

    $('#searchbgm').html("");
    _(BGMList).each(function (bgm, i) {
        $('#searchbgm').append('<li onclick="SetActive(\'BGM\',\'' + bgm.Value + '\')">' + bgm.Value + '</li>');
    });

    $('#searchlab').html("");
    _(LabList).each(function (lab, i) {
        $('#searchlab').append('<li onclick="SetActive(\'LAB\',\'' + lab.Value + '\')">' + lab.Value + '</li>');
    });

    $('#searchtableincl').html("");
    _(TblInclList).each(function (tblincl, i) {
        $('#searchtableincl').append('<li onclick="SetActive(\'TABLE_INCL\',\'' + tblincl.Value + '\')">' + tblincl.Value + '</li>');
    });

    $('#searchtablenatts').html("");
    _(TblNattsList).each(function (tblnatts, i) {
        $('#searchtablenatts').append('<li onclick="SetActive(\'TABLE_NATTS\',\'' + tblnatts.Value + '\')">' + tblnatts.Value + '</li>');
    });

    $('#searchcrownincl').html("");
    _(CrwnInclList).each(function (crwnincl, i) {
        $('#searchcrownincl').append('<li onclick="SetActive(\'CROWN_INCL\',\'' + crwnincl.Value + '\')">' + crwnincl.Value + '</li>');
    });

    $('#searchcrownnatts').html("");
    _(CrwnNattsList).each(function (crwnnatt, i) {
        $('#searchcrownnatts').append('<li onclick="SetActive(\'CROWN_NATTS\',\'' + crwnnatt.Value + '\')">' + crwnnatt.Value + '</li>');
    });

    $('#searchtableopen').html("");
    _(TableOpenList).each(function (tableopen, i) {
        $('#searchtableopen').append('<li onclick="SetActive(\'TABLEOPEN\',\'' + tableopen.Value + '\')">' + tableopen.Value + '</li>');
    });

    $('#searchcrownopen').html("");
    _(CrownOpenList).each(function (crownopen, i) {
        $('#searchcrownopen').append('<li onclick="SetActive(\'CROWNOPEN\',\'' + crownopen.Value + '\')">' + crownopen.Value + '</li>');
    });

    $('#searchpavopen').html("");
    _(PavOpenList).each(function (pavopen, i) {
        $('#searchpavopen').append('<li onclick="SetActive(\'PAVILIONOPEN\',\'' + pavopen.Value + '\')">' + pavopen.Value + '</li>');
    });

    $('#searchgirdleopen').html("");
    _(GirdleOpenList).each(function (girdleopen, i) {
        $('#searchgirdleopen').append('<li onclick="SetActive(\'GIRDLEOPEN\',\'' + girdleopen.Value + '\')">' + girdleopen.Value + '</li>');
    });
    
    $('#txtfromcarat').val("");
    $('#txttocarat').val("");

    $('#FromDiscount').val("");
    $('#ToDiscount').val("");
    $('#txtPrCtsFrom').val("");
    $('#txtPrCtsTo').val("");
    $('#FromTotalAmt').val("");
    $('#ToTotalAmt').val("");
    $('#FromLength').val("");
    $('#ToLength').val("");
    $('#FromWidth').val("");
    $('#ToWidth').val("");
    $('#FromDepth').val("");
    $('#ToDepth').val("");
    $('#FromDepthPer').val("");
    $('#ToDepthPer').val("");
    $('#FromTablePer').val("");
    $('#ToTablePer').val("");
    $('#FromCrAng').val("");
    $('#ToCrAng').val("");
    $('#FromCrHt').val("");
    $('#ToCrHt').val("");
    $('#FromPavAng').val("");
    $('#ToPavAng').val("");
    $('#FromPavHt').val("");
    $('#ToPavHt').val("");

    resetKeytoSymbol();

    KTSBlank = false;
    $("#KTSBlank").removeClass("active");
    LengthBlank = false;
    $("#LengthBlank").removeClass("active");
    WidthBlank = false;
    $("#WidthBlank").removeClass("active");
    DepthBlank = false;
    $("#DepthBlank").removeClass("active");
    DepthPerBlank = false;
    $("#DepthPerBlank").removeClass("active");
    TablePerBlank = false;
    $("#TablePerBlank").removeClass("active");
    CrAngBlank = false;
    $("#CrAngBlank").removeClass("active");
    CrHtBlank = false;
    $("#CrHtBlank").removeClass("active");
    PavAngBlank = false;
    $("#PavAngBlank").removeClass("active");
    PavHtBlank = false;
    $("#PavHtBlank").removeClass("active");

    Color_Hide_Show('3');
    resetINTENSITY();
    resetOVERTONE();
    resetFANCY_COLOR();
    $(".carat-dropdown-main").hide();
    FancyDDLHide();
}

function setToFixed(obj) {
    if ($(obj).val() != "") {
        $(obj).val(parseFloat($(obj).val()).toFixed(2));
    }
}
function numvalid(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function ddl_close() {
    $("#sym-sec1 .carat-dropdown-main").hide();
    $("#sym-sec2 .carat-dropdown-main").hide();
    $("#sym-sec3 .carat-dropdown-main").hide();
    $("#sym-sec0 .carat-dropdown-main").hide();
    C1 = 0, KTS = 0, C2 = 0, C3 = 0;
    return;
}
function resetKeytoSymbol() {
    CheckKeyToSymbolList = [];
    UnCheckKeyToSymbolList = [];
    $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
    $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
    $('#searchkeytosymbol input[type="radio"]').prop('checked', false);
    KTS = 1;
    Key_to_symbolShow();
}
function Key_to_symbolShow() {
    setTimeout(function () {
        if (KTS == 0) {
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").hide();
            $("#sym-sec0 .carat-dropdown-main").show();
            KTS = 1;
            C1 = 0, C2 = 0, C3 = 0;
        }
        else {
            $("#sym-sec0 .carat-dropdown-main").hide();
            C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function BindKeyToSymbolList() {
    //$.ajax({
    //    url: "/SearchStock/GetKeyToSymbolList",
    //    async: false,
    //    type: "POST",
    //    data: null,
    //    success: function (data, textStatus, jqXHR) {
    //        var KeytoSymbolList = data.Data;
    //        $('#searchkeytosymbol').html("");
    //        if (KeytoSymbolList != null) {
    //            if (KeytoSymbolList.length > 0) {
    //                $.each(KeytoSymbolList, function (i, itm) {
    //                    $('#searchkeytosymbol').append('<div class="col-12 pl-0 pr-0 ng-scope">'
    //                        + '<ul class="row m-0">'
    //                        + '<li class="carat-dropdown-chkbox">'
    //                        + '<div class="main-cust-check">'
    //                        + '<label class="cust-rdi-bx mn-check">'
    //                        + '<input type="radio" class="checkradio" id="CHK_KTS_Radio_' + (i + 1) + '" name="radio' + (i + 1) + '" onclick="GetCheck_KTS_List(\'' + itm.sSymbol + '\');">'
    //                        + '<span class="cust-rdi-check">'
    //                        + '<i class="fa fa-check"></i>'
    //                        + '</span>'
    //                        + '</label>'
    //                        + '<label class="cust-rdi-bx mn-time">'
    //                        + '<input type="radio" id="UNCHK_KTS_Radio_' + (i + 1) + '" class="checkradio" name="radio' + (i + 1) + '" onclick="GetUnCheck_KTS_List(\'' + itm.sSymbol + '\');">'
    //                        + '<span class="cust-rdi-check">'
    //                        + '<i class="fa fa-times"></i>'
    //                        + '</span>'
    //                        + '</label>'
    //                        + '</div>'
    //                        + '</li>'
    //                        + '<li class="col" style="text-align: left;margin-left: -15px;">'
    //                        + '<span>' + itm.sSymbol + '</span>'
    //                        + '</li>'
    //                        + '</ul>'
    //                        + '</div>')
    //                });
    //                $('#searchkeytosymbol').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');
    //            }
    //        }
    //    },
    //    error: function (jqXHR, textStatus, errorThrown) {
    //        $('.loading-overlay-image-container').hide();
    //        $('.loading-overlay').hide();
    //    }
    //});

    if (KeyToSymbolList.length > 0) {
        $.each(KeyToSymbolList, function (i, itm) {
            $('#searchkeytosymbol').append('<div class="col-12 pl-0 pr-0 ng-scope">'
                + '<ul class="row m-0">'
                + '<li class="carat-dropdown-chkbox">'
                + '<div class="main-cust-check">'
                + '<label class="cust-rdi-bx mn-check">'
                + '<input type="radio" class="checkradio" id="CHK_KTS_Radio_' + (i + 1) + '" name="radio' + (i + 1) + '" onclick="GetCheck_KTS_List(\'' + itm.Value + '\');">'
                + '<span class="cust-rdi-check">'
                + '<i class="fa fa-check"></i>'
                + '</span>'
                + '</label>'
                + '<label class="cust-rdi-bx mn-time">'
                + '<input type="radio" id="UNCHK_KTS_Radio_' + (i + 1) + '" class="checkradio" name="radio' + (i + 1) + '" onclick="GetUnCheck_KTS_List(\'' + itm.Value + '\');">'
                + '<span class="cust-rdi-check">'
                + '<i class="fa fa-times"></i>'
                + '</span>'
                + '</label>'
                + '</div>'
                + '</li>'
                + '<li class="col" style="text-align: left;margin-left: -15px;">'
                + '<span>' + itm.Value + '</span>'
                + '</li>'
                + '</ul>'
                + '</div>')
        });
        $('#searchkeytosymbol').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');
    }
}
function GetCheck_KTS_List(item) {
    var SList = _.reject(UnCheckKeyToSymbolList, function (e) { return e.Symbol == item });
    UnCheckKeyToSymbolList = SList;

    var res = _.filter(CheckKeyToSymbolList, function (e) { return (e.Symbol == item) });
    if (res.length == 0) {
        CheckKeyToSymbolList.push({
            "NewID": CheckKeyToSymbolList.length + 1,
            "Symbol": item,
        });
        $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
        $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
    }
    setTimeout(function () {
        $("#sym-sec0 .carat-dropdown-main").show();
        KTS = 0;
        return;
    }, 2);
}
function GetUnCheck_KTS_List(item) {
    var SList = _.reject(CheckKeyToSymbolList, function (e) { return e.Symbol == item });
    CheckKeyToSymbolList = SList

    var res = _.filter(UnCheckKeyToSymbolList, function (e) { return (e.Symbol == item) });
    if (res.length == 0) {
        UnCheckKeyToSymbolList.push({
            "NewID": UnCheckKeyToSymbolList.length + 1,
            "Symbol": item,
        });
        $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
        $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Deselected');
    }
    setTimeout(function () {
        $("#sym-sec0 .carat-dropdown-main").show();
        KTS = 0;
        return;
    }, 2);
}
function CommaSeperated_list(e) {
    var data = document.getElementById("txtRefNo").value;
    var lines = data.split(' ');
    document.getElementById("txtRefNo").value = lines.join(',');
    Stone_List = document.getElementById("txtRefNo").value;
    //var key = e.which;
    //if (key == 13)  // the enter key code
    //{
    //    alert("enter")
    //} else {
    //    return false;
    //}
}
var LeaveTextBox = function (ele, fromid, toid) {
    $("#" + fromid).val($("#" + fromid).val() == "" ? "0.00" : $("#" + fromid).val() == undefined ? "0.00" : parseFloat($("#" + fromid).val()).toFixed(2));
    $("#" + toid).val($("#" + toid).val() == "" ? "0.00" : $("#" + toid).val() == undefined ? "0.00" : parseFloat($("#" + toid).val()).toFixed(2));

    var fromvalue = parseFloat($("#" + fromid).val()).toFixed(2) == "" ? 0 : parseFloat($("#" + fromid).val()).toFixed(2);
    var tovalue = parseFloat($("#" + toid).val()).toFixed(2) == "" ? 0 : parseFloat($("#" + toid).val()).toFixed(2);
    if (ele == "FROM") {
        if (parseFloat(parseFloat(fromvalue).toFixed(2)) > parseFloat(parseFloat(tovalue).toFixed(2))) {
            $("#" + toid).val(fromvalue);
            if (fromvalue == 0) {
                $("#" + fromid).val("");
                $("#" + toid).val("");
            }
        }
    }
    else if (ele == "TO") {
        if (parseFloat(parseFloat(tovalue).toFixed(2)) < parseFloat(parseFloat(fromvalue).toFixed(2))) {
            $("#" + fromid).val($("#" + toid).val());
            if (tovalue == 0) {
                $("#" + fromid).val("");
                $("#" + toid).val("");
            }
        }
    }
    if (parseFloat(parseFloat($("#" + fromid).val())) == "0" && parseFloat(parseFloat($("#" + toid).val())) == "0") {
        $("#" + fromid).val("");
        $("#" + toid).val("");
    }
}
function SetModifyParameter() {
    $("#divFilter").show();
    $("#divGridView").hide();
}
function DownloadExcel() {
    $('.loading-overlay-image-container').show();
    $('.loading-overlay').show();
    var obj = {};
    $('#ExcelModal').modal('hide');
    if ($('#customRadio3').prop('checked')) {
        obj.RefNo = $("#txtRefNo").val().replace(/ /g, ',');
    }
    else {
        var count = gridOptions.api.getSelectedRows().length;
        if (count > 0) {
            var REF_NO = _.pluck(gridOptions.api.getSelectedRows(), 'REF_NO').join(",");
            obj.RefNo = REF_NO;
        } else {
            toastr.warning("No stone selected for download as a excel !");
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            return;
        }
    }

    var PointerSizeLst = "";
    var lst = _.filter(CaratList, function (e) { return e.ACTIVE == true });
    if (($('#txtfromcarat').val() != "" && parseFloat($('#txtfromcarat').val()) > 0) && ($('#txttocarat').val() != "" && parseFloat($('#txttocarat').val()) > 0)) {
        NewSizeGroup();
    }
    if (SizeGroupList.length != 0) {
        PointerSizeLst = _.pluck(SizeGroupList, 'Size').join(",");
    }
    if (lst.length != 0) {
        PointerSizeLst = _.pluck(_.filter(CaratList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    }

    var shapeLst = _.pluck(_.filter(ShapeList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var colorLst = _.pluck(_.filter(ColorList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var clarityLst = _.pluck(_.filter(ClarityList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var labLst = _.pluck(_.filter(LabList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var cutLst = _.pluck(_.filter(CutList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var polishLst = _.pluck(_.filter(PolishList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var symLst = _.pluck(_.filter(SymList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var fluoLst = _.pluck(_.filter(FlouList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var bgmLst = _.pluck(_.filter(BGMList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

    var tblincLst = _.pluck(_.filter(TblInclList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var tblnattsLst = _.pluck(_.filter(TblNattsList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crwincLst = _.pluck(_.filter(CrwnInclList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crwnattsLst = _.pluck(_.filter(CrwnNattsList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

    var tableopen = _.pluck(_.filter(TableOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var crownopen = _.pluck(_.filter(CrownOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var pavopen = _.pluck(_.filter(PavOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");
    var girdleopen = _.pluck(_.filter(GirdleOpenList, function (e) { return e.ACTIVE == true }), 'Value').join(",");

    var KeyToSymLst_Check = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
    var KeyToSymLst_uncheck = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");

    obj.PgNo = 1;
    obj.PgSize = 10;
    obj.OrderBy = "";
    obj.FromDate = $("#txtFromDate").val();
    obj.ToDate = $("#txtToDate").val();
    obj.iVendor = $('#ddlSupplierId').val().join(",");
    obj.sShape = shapeLst;
    obj.sPointer = PointerSizeLst;
    obj.sColorType = Color_Type;

    if (Color_Type == "Fancy") {
        obj.sColor = "";
        obj.sINTENSITY = _.pluck(Check_Color_1, 'Symbol').join(",");
        obj.sOVERTONE = _.pluck(Check_Color_2, 'Symbol').join(",");
        obj.sFANCY_COLOR = _.pluck(Check_Color_3, 'Symbol').join(",");
    }
    else if (Color_Type == "Regular") {
        obj.sColor = colorLst;
        obj.sINTENSITY = "";
        obj.sOVERTONE = "";
        obj.sFANCY_COLOR = "";
    }

    obj.sClarity = clarityLst;
    obj.sCut = cutLst;
    obj.sPolish = polishLst;
    obj.sSymm = symLst;
    obj.sFls = fluoLst;
    obj.sLab = labLst;

    obj.dFromDisc = $('#FromDiscount').val();
    obj.dToDisc = $('#ToDiscount').val();
    obj.dFromTotAmt = $('#FromTotalAmt').val();
    obj.dToTotAmt = $('#ToTotalAmt').val();

    obj.dFromLength = $('#FromLength').val();
    obj.dToLength = $('#ToLength').val();
    obj.dFromWidth = $('#FromWidth').val();
    obj.dToWidth = $('#ToWidth').val();
    obj.dFromDepth = $('#FromDepth').val();
    obj.dToDepth = $('#ToDepth').val();
    obj.dFromDepthPer = $('#FromDepthPer').val();
    obj.dToDepthPer = $('#ToDepthPer').val();
    obj.dFromTablePer = $('#FromTablePer').val();
    obj.dToTablePer = $('#ToTablePer').val();
    obj.dFromCrAng = $('#FromCrAng').val();
    obj.dToCrAng = $('#ToCrAng').val();
    obj.dFromCrHt = $('#FromCrHt').val();
    obj.dToCrHt = $('#ToCrHt').val();
    obj.dFromPavAng = $('#FromPavAng').val();
    obj.dToPavAng = $('#ToPavAng').val();
    obj.dFromPavHt = $('#FromPavHt').val();
    obj.dToPavHt = $('#ToPavHt').val();
    obj.dKeytosymbol = KeyToSymLst_Check + '-' + KeyToSymLst_uncheck;
    obj.dCheckKTS = KeyToSymLst_Check;
    obj.dUNCheckKTS = KeyToSymLst_uncheck;
    obj.sBGM = bgmLst;
    obj.sCrownBlack = crwnattsLst;
    obj.sTableBlack = tblnattsLst;
    obj.sCrownWhite = crwincLst;
    obj.sTableWhite = tblincLst;
    obj.sTableOpen = tableopen;
    obj.sCrownOpen = crownopen;
    obj.sPavOpen = pavopen;
    obj.sGirdleOpen = girdleopen;
    obj.Img = $('#SearchImage').hasClass('active') ? "Yes" : "";
    obj.Vdo = $('#SearchVideo').hasClass('active') ? "Yes" : "";
    obj.KTSBlank = (KTSBlank == true ? true : "");
    obj.LengthBlank = (LengthBlank == true ? true : "");
    obj.WidthBlank = (WidthBlank == true ? true : "");
    obj.DepthBlank = (DepthBlank == true ? true : "");
    obj.DepthPerBlank = (DepthPerBlank == true ? true : "");
    obj.TablePerBlank = (TablePerBlank == true ? true : "");
    obj.CrAngBlank = (CrAngBlank == true ? true : "");
    obj.CrHtBlank = (CrHtBlank == true ? true : "");
    obj.PavAngBlank = (PavAngBlank == true ? true : "");
    obj.PavHtBlank = (PavHtBlank == true ? true : "");
    obj.ExcelType = (isCustomer == true ? 2 : 3);

    $.ajax({
        url: "/LabStock/LabSearchGridExcel",
        type: "POST",
        data: obj,
        success: function (data, textStatus, jqXHR) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
            if (data.indexOf('Something Went wrong') > -1) {
                MoveToErrorPage(0);
            }
            else if (data.indexOf('No data found.') > -1) {
                toastr.error(data);
            }
            else if (data.indexOf('ExcelFile') > -1) {
                window.location = data;
            }
            else {
                toastr.error(data);
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $('.loading-overlay-image-container').hide();
            $('.loading-overlay').hide();
        }
    });
}
function formatNumber(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function formatIntNumber(number) {
    return (parseInt(number)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
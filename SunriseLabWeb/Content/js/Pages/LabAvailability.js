var pgSize = 200;
var ExcelUploadRefNo = "";
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

$(document).ready(function () {
    $("#txtStoneId").focus();
    contentHeight();
    $("#txtStoneId").keyup(function (event) {
        if (event.keyCode === 13) {
            GetSearch();
        }
    });
    $("#li_User_LabAvailibility").addClass("menuActive");
});

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
        else if (field == "Profit" || field == "Profit Amount" || field == "Disc" || field == "Value" || field == "SUPPLIER_COST_DISC" || field == "SUPPLIER_COST_VALUE" || field == "MAX_DISC" || field == "MAX_VALUE" ||
            field == "CUSTOMER_COST_DISC" || field == "CUSTOMER_COST_VALUE" || field == "Bid_Disc" || field == "Bid_Amt" || field == "Avg_Stock_Disc" ||
            field == "Avg_Pur_Disc" || field == "Avg_Sales_Disc") {
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
    var TOT_CTS = 0;
    var AVG_SALES_DISC_PER = 0;
    var AVG_PRICE_PER_CTS = 0;
    var TOT_NET_AMOUNT = 0;
    var TOT_PCS = 0;
    var TOT_RAP_AMOUNT = 0;
    var CUR_RAP_RATE = 0;
    var dDisc = 0, dRepPrice = 0, DCTS = 0, dNetPrice = 0, Web_Benefit = 0, Final_Disc = 0, Net_Value = 0;

    if (gridOptions.api.getSelectedRows().length > 0) {
        dDisc = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'CUSTOMER_COST_DISC'), function (memo, num) { return memo + num; }, 0);
        TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'CUSTOMER_COST_VALUE'), function (memo, num) { return memo + num; }, 0);
        TOT_CTS = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'Cts'), function (memo, num) { return memo + num; }, 0);
        TOT_RAP_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'Rap_Amount'), function (memo, num) { return memo + num; }, 0);
        CUR_RAP_RATE = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'Rap_Rate'), function (memo, num) { return memo + num; }, 0);

        AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
        AVG_PRICE_PER_CTS = TOT_NET_AMOUNT / TOT_CTS;
        TOT_PCS = gridOptions.api.getSelectedRows().length;

        if (CUR_RAP_RATE == 0) {
            Final_Disc = 0;
            AVG_SALES_DISC_PER = 0;
        }
    } else {
        if (summary.length == 0) {
            dDisc = _.reduce(_.pluck(Rowdata, 'CUSTOMER_COST_DISC'), function (memo, num) { return memo + num; }, 0);
            TOT_NET_AMOUNT = _.reduce(_.pluck(Rowdata, 'CUSTOMER_COST_VALUE'), function (memo, num) { return memo + num; }, 0);
            TOT_CTS = _.reduce(_.pluck(Rowdata, 'Cts'), function (memo, num) { return memo + num; }, 0);
            TOT_RAP_AMOUNT = _.reduce(_.pluck(Rowdata, 'Rap_Amount'), function (memo, num) { return memo + num; }, 0);
            CUR_RAP_RATE = _.reduce(_.pluck(Rowdata, 'Rap_Rate'), function (memo, num) { return memo + num; }, 0);

            AVG_SALES_DISC_PER = (-1 * (((TOT_RAP_AMOUNT - TOT_NET_AMOUNT) / TOT_RAP_AMOUNT) * 100)).toFixed(2);
            AVG_PRICE_PER_CTS = TOT_NET_AMOUNT / TOT_CTS;
            TOT_PCS = Rowdata.length;

            if (CUR_RAP_RATE == 0) {
                Final_Disc = 0;
                AVG_SALES_DISC_PER = 0;
            }
        } else {
            TOT_CTS = summary.TOT_CTS;
            AVG_SALES_DISC_PER = summary.AVG_SALES_DISC_PER;
            AVG_PRICE_PER_CTS = summary.AVG_PRICE_PER_CTS;
            TOT_NET_AMOUNT = summary.TOT_NET_AMOUNT;
            TOT_PCS = summary.TOT_PCS;
        }
        
    }
    setTimeout(function () {
        $('.tab1Pcs').html(formatIntNumber(TOT_PCS));
        $('.tab1CTS').html(formatNumber(TOT_CTS));
        $('.tab1OfferDisc').html(formatNumber(AVG_SALES_DISC_PER));
        $('.tab1OfferValue').html(formatNumber(TOT_NET_AMOUNT));
        $('.tab1PriceCts').html(formatNumber(AVG_PRICE_PER_CTS));
    });
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
columnDefs.push({ headerName: "Final Sale Amt US($)", field: "CUSTOMER_COST_VALUE", width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("CUSTOMER_COST_VALUE", params); } });
columnDefs.push({ headerName: "Supplier Base Offer(%)", field: "Disc", width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Disc", params); } });
columnDefs.push({ headerName: "Supplier Base Offer Value($)", field: "Value", width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Value", params); } });
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
columnDefs.push({ headerName: "Comment", field: "Lab_Comments", width: 300, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Lab_Comments", params); } });
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


var gridOptions = {};
function GetSearch() {
    $(".tab1TCount").hide();
    $(".gridview").hide();
    $(".excel").hide();
    summary = [];
    Rowdata = [];

    ExcelUploadRefNo = "";
    if ($("#txtStoneId").val() != "") {
        loaderShow();
        if (gridOptions.api != undefined) {
            gridOptions.api.destroy();
        }

        gridOptions = {
            masterDetail: true,
            detailCellRenderer: 'myDetailCellRenderer',
            detailRowHeight: 70,
            groupDefaultExpanded: 0,
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
}

var SortColumn = "";
var SortDirection = "";
var Rowdata = [];
var summary = [];
const datasource1 = {
    getRows(params) {
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var obj = {};

        if (params.request.sortModel.length > 0) {
            obj.OrderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort;
        }
        obj.PgNo = PageNo;
        obj.PgSize = pgSize;
        obj.RefNo = $("#txtStoneId").val();

        $.ajax({
            url: "/User/Get_LabAvailibility",
            async: false,
            type: "POST",
            data: { req: obj },
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data.length > 0) {
                    $(".tab1TCount").show();
                    $(".gridview").show();
                    $(".excel").show();

                    summary = data.Data[0].DataSummary;
                    Rowdata = data.Data[0].DataList;
                    //Rowdata = data.Data;
                    //params.successCallback(data.Data, data.Data[0].iTotalRec);
                    params.successCallback(data.Data[0].DataList, summary.TOT_PCS);

                    gridOptions.api.forEachNode(function (node) {
                        node.setSelected(true);
                    });
                }
                else {
                    toastr.remove();
                    toastr.error("No Data Found", { timeOut: 2500 });
                    gridOptions.api.showNoRowsOverlay();
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
function CommaSeperatedStone_list(e) {
    var data = document.getElementById("txtStoneId").value;
    var lines = data.split(' ');
    document.getElementById("txtStoneId").value = lines.join(',');
}
function contentHeight() {
    var winH = $(window).height(),
        navbarHei = $(".order-title").height(),
        serachHei = $(".order-history-data").height(),
        contentHei = winH - serachHei - navbarHei - 115;
    contentHei = (contentHei < 200 ? 369 : contentHei);
    $("#Cart-Gride").css("height", contentHei);
}
$(window).resize(function () {
    contentHeight();
});
function Reset() {
    $("#txtStoneId").focus();
    $('#txtStoneId').val("");
    $("#file_upload").val("");
    $(".excel").hide();
    $(".gridview").hide();
    $(".tab1TCount").hide();
}

function ExcelExport(Type) {
    if (gridOptions.api != undefined) {
        loaderShow();
        setTimeout(function () {
            var selectedRows = gridOptions.api.getSelectedRows();
            var list = '';
            var i = 0, tot = selectedRows.length;

            if (tot == 0) {
                gridOptions.api.forEachNode(function (node) {
                    selectedRows.push(node.data);
                });
                tot = selectedRows.length;
            }

            for (; i < tot; i++) {
                list += selectedRows[i].SupplierId + "_" + selectedRows[i].Ref_No + "_" + selectedRows[i].Supplier_Stone_Id + ',';
            }
            list = (list != '' ? list.substr(0, (list.length - 1)) : '');
            
            var obj = {};
            obj.SupplierId_RefNo_SupplierRefNo = list;
            obj.RefNo = (list == "" ? $("#txtStoneId").val() : "");
            obj.Type = Type;
            
            if (obj.SupplierId_RefNo_SupplierRefNo != "" || obj.RefNo != "") {
                $.ajax({
                    url: "/User/Excel_LabAvailibility",
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
            }
            else {
                Reset();
                loaderHide();
            }
        }, 50);
    }
}



function formatIntNumber(number) {
    return (parseInt(number)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
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


function UploadExcelFile() {
    $(".tab1TCount").hide();
    $(".gridview").hide();
    $(".excel").hide();
    summary = [];
    Rowdata = [];

    var file = document.getElementById('file_upload').files[0];
    if (file == undefined) {
        toastr.remove();
        return toastr.warning("Please Select Excel File For Upload");
    }

    loaderShow();

    const formData = new FormData();
    formData.append("file", file);

    $.ajax({
        url: "/User/UploadExcelforLabAvailibility",
        processData: false,
        contentType: false,
        type: "POST",
        data: formData,
        success: function (data, textStatus, jqXHR) {
            debugger
            loaderHide();

            var Culet = data[data.length - 1].Culet;

            if (Culet == "0") {
                toastr.remove();
                return toastr.warning(data[data.length - 1].Lab_Comments);
            }
            else {
                var Invalid_Stone_Body = data[data.length - 1].Lab_Comments;
                ExcelUploadRefNo = data[data.length - 1].Supplier_Comments;

                data.splice(data.length - 1);
                Rowdata = data;

                var gridDiv = document.querySelector('#Cart-Gride');
                if (gridOptions.api != undefined) {
                    gridOptions.api.destroy();
                }
                gridOptions = {
                    masterDetail: true,
                    detailCellRenderer: 'myDetailCellRenderer',
                    detailRowHeight: 70,
                    groupDefaultExpanded: 0,
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
                
                if (data.length > 0) {
                    $(".tab1TCount").show();
                    $(".gridview").show();
                    $(".excel").show();

                    gridOptions.api.forEachNode(function (node) {
                        node.setSelected(true);
                    });
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
}
function Excel_Invalid_RefNo(Body) {
    var str = Body;
    str = str.replace(/&lt;/g, '<');
    str = str.replace(/&gt;/g, '>');

    var final_msg = ''
    final_msg = '<label class="offerComment" style="word-break:break-word;">Below Ref No / Certi No / Supplier Ref No are Not Found !!</label>';
    final_msg += str;
    $("#Excel_Stone_Invalid_Modal .form-group").html(final_msg);
    $('#Excel_Stone_Invalid_Modal').modal('show');
}
function CloseExcel_Stone_InvalidPopup() {
    $('#Excel_Stone_Invalid_Modal').modal('hide');
}
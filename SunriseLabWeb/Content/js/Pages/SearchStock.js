var obj = {};
var FortuneCodeValid = true;
var FortuneCodeValid_Msg = "";
var Type = "";
var OrderBy = "";
var Rowdata = [];
var summary = [];
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

var columnDefs = [];
function Master_Get() {
    $("#ddlUserId").html("<option value=''>Select</option>");
    var obj = {};
    obj.Assist_UserId = $("#hdn_UserId").val();

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
                    if (data.Data[k].StockDiscMgt_Count > 0) {
                        $("#ddlUserId").append("<option value=" + data.Data[k].UserId + ">" + data.Data[k].CompName + " [" + data.Data[k].UserName + "]" + "</option>");
                    }
                }
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
function cellStyle(field, params) {
    if (params.data != undefined) {
        if (params.data.Cut == '3EX' && (field == 'Cut' || field == 'Polish' || field == 'Symm')) {
            return { 'font-size': '11px', 'font-weight': 'bold' };
        }
        else if (field == "CUSTOMER_COST_DISC" || field == "CUSTOMER_COST_VALUE") {
            return { 'font-weight': '600', 'background-color': '#ccffff', 'text-align': 'center', 'font-size': '11px' };
        }
        else if (field == "SUPPLIER_COST_DISC" || field == "SUPPLIER_COST_VALUE") {
            return { 'font-weight': '600', 'background-color': '#ff99cc', 'text-align': 'center', 'font-size': '11px' };
        }
        else if (field == "Disc" || field == "Value" || field == "MAX_DISC" || field == "MAX_VALUE" ||
            field == "Bid_Disc" || field == "Bid_Amt" || field == "Avg_Stock_Disc" ||
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

function SupplierList() {
    if ($("#txtDisc_1_1").val() != undefined) {
        if ($("#PricingMethod_1").val() == "" && $("#txtDisc_1_1").val() != "") {
            return toastr.warning("Please Select Pricing Method");
        }
        if ($("#PricingMethod_1").val() != "" && $("#txtDisc_1_1").val() == "") {
            return toastr.warning("Please Enter Pricing Method " + $("#PricingMethod_1").val());
        }
    }

    Type = "Supplier List";
    columnDefs = [];

    var obj = {};
    obj.Type = "SUPPLIER";

    loaderShow();
    $.ajax({
        url: '/User/Get_SearchStock_ColumnSetting',
        type: "POST",
        data: { req: obj },
        success: function (data) {
            if (data.Status == "1" && data.Data.length == 0) {
                $("#divFilter").show();
                $("#divGridView").hide();
                toastr.error("No Columns Found.");
            }
            else if (data.Status == "1" && data.Data.length > 0) {
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
                data.Data.forEach(function (item) {
                    if (item.Column_Name == "Ref No") {
                        columnDefs.push({ headerName: "Ref No", field: "Ref_No", filter: getValuesAsync1("Ref_No"), width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("RefNo", params); } });
                    }
                    else if (item.Column_Name == "Lab") {
                        columnDefs.push({
                            headerName: "Lab", field: "Lab",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Lab"),
                            filterParams: {
                                values: getValuesAsync("Lab"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 50, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Lab", params); }, cellRenderer: function (params) { return Lab(params); }
                        });
                    }
                    else if (item.Column_Name == "Image-Video") {
                        columnDefs.push({ headerName: "VIEW", field: "DNA_Image_Video_Certi", filter: getValuesAsync1("DNA_Image_Video_Certi"), width: 65, cellRenderer: function (params) { return DNA_Image_Video_Certi(params, false, true, true, false); }, suppressSorting: true, suppressMenu: true, sortable: false });
                    }
                    else if (item.Column_Name == "Supplier Ref No") {
                        columnDefs.push({ headerName: "Supplier Ref No", field: "Supplier_Stone_Id", filter: getValuesAsync1("Supplier_Stone_Id"), width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Supplier_Stone_Id", params); } });
                    }
                    else if (item.Column_Name == "Cert No") {
                        columnDefs.push({ headerName: "Cert No", field: "Certificate_No", filter: getValuesAsync1("Certificate_No"), width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Certificate_No", params); } });
                    }
                    else if (item.Column_Name == "Supplier Name") {
                        columnDefs.push({ headerName: "Supplier Name", field: "SupplierShortName", filter: getValuesAsync1("SupplierShortName"), width: 200, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("SupplierShortName", params); } });
                    }
                    else if (item.Column_Name == "Shape") {
                        columnDefs.push({
                            headerName: "Shape", field: "Shape",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Shape"),
                            filterParams: {
                                values: getValuesAsync("Shape"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 100, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Shape", params); }
                        });
                    }
                    else if (item.Column_Name == "Pointer") {
                        columnDefs.push({
                            headerName: "Pointer", field: "Pointer",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Pointer"),
                            filterParams: {
                                values: getValuesAsync("Pointer"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Pointer", params); }
                        });
                    }
                    else if (item.Column_Name == "BGM") {
                        columnDefs.push({
                            headerName: "BGM", field: "BGM",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("BGM"),
                            filterParams: {
                                values: getValuesAsync("BGM"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("BGM", params); }
                        });
                    }
                    else if (item.Column_Name == "Color") {
                        columnDefs.push({
                            headerName: "Color", field: "Color",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Color"),
                            filterParams: {
                                values: getValuesAsync("Color"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Color", params); }
                        });
                    }
                    else if (item.Column_Name == "Clarity") {
                        columnDefs.push({
                            headerName: "Clarity", field: "Clarity",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Clarity"),
                            filterParams: {
                                values: getValuesAsync("Clarity"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Clarity", params); }
                        });
                    }
                    else if (item.Column_Name == "Cts") {
                        columnDefs.push({
                            headerName: "Cts", field: "Cts",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Cts"),
                            filterParams: {
                                values: getValuesAsync("Cts"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 70, tooltip: function (params) { return parseFloat(params.value).toFixed(2) }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2) }, cellStyle: function (params) { return cellStyle("Cts", params); }
                        });
                    }
                    else if (item.Column_Name == "Rap Rate($)") {
                        columnDefs.push({ headerName: "Rap Rate($)", field: "Rap_Rate", filter: getValuesAsync1("Rap_Rate"), width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Rap_Rate", params); } });
                    }
                    else if (item.Column_Name == "Rap Amount($)") {
                        columnDefs.push({ headerName: "Rap Amount($)", field: "Rap_Amount", filter: getValuesAsync1("Rap_Amount"), width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Rap_Amount", params); } });
                    }
                    else if (item.Column_Name == "Supplier Cost Disc(%)") {
                        columnDefs.push({ headerName: "Supplier Cost Disc(%)", field: "SUPPLIER_COST_DISC", filter: getValuesAsync1("SUPPLIER_COST_DISC"), width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("SUPPLIER_COST_DISC", params); } });
                    }
                    else if (item.Column_Name == "Supplier Cost Value($)") {
                        columnDefs.push({ headerName: "Supplier Cost Value($)", field: "SUPPLIER_COST_VALUE", filter: getValuesAsync1("SUPPLIER_COST_VALUE"), width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("SUPPLIER_COST_VALUE", params); } });
                    }
                    else if (item.Column_Name == "Final Disc(%)") {
                        columnDefs.push({ headerName: "Final Disc(%)", field: "CUSTOMER_COST_DISC", filter: getValuesAsync1("CUSTOMER_COST_DISC"), width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("CUSTOMER_COST_DISC", params); } });
                    }
                    else if (item.Column_Name == "Final Amt US($)") {
                        columnDefs.push({ headerName: "Final Amt US($)", field: "CUSTOMER_COST_VALUE", filter: getValuesAsync1("CUSTOMER_COST_VALUE"), width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("CUSTOMER_COST_VALUE", params); } });
                    }
                    else if (item.Column_Name == "Supplier Base Offer(%)") {
                        columnDefs.push({ headerName: "Supplier Base Offer(%)", field: "Disc", filter: getValuesAsync1("Disc"), width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Disc", params); } });
                    }
                    else if (item.Column_Name == "Supplier Base Offer Value($)") {
                        columnDefs.push({ headerName: "Supplier Base Offer Value($)", field: "Value", filter: getValuesAsync1("Value"), width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Value", params); } });
                    }
                    else if (item.Column_Name == "Cut") {
                        columnDefs.push({
                            headerName: "Cut", field: "Cut",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Cut"),
                            filterParams: {
                                values: getValuesAsync("Cut"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Cut", params); }
                        });
                    }
                    else if (item.Column_Name == "Polish") {
                        columnDefs.push({
                            headerName: "Polish", field: "Polish",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Polish"),
                            filterParams: {
                                values: getValuesAsync("Polish"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Polish", params); }
                        });
                    }
                    else if (item.Column_Name == "Symm") {
                        columnDefs.push({
                            headerName: "Symm", field: "Symm",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Symm"),
                            filterParams: {
                                values: getValuesAsync("Symm"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Symm", params); }
                        });
                    }
                    else if (item.Column_Name == "Fls") {
                        columnDefs.push({
                            headerName: "Fls", field: "Fls",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Fls"),
                            filterParams: {
                                values: getValuesAsync("Fls"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Fls", params); }
                        });
                    }
                    else if (item.Column_Name == "Length") {
                        columnDefs.push({ headerName: "Length", field: "Length", filter: getValuesAsync1("Length"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Length", params); } });
                    }
                    else if (item.Column_Name == "Width") {
                        columnDefs.push({ headerName: "Width", field: "Width", filter: getValuesAsync1("Width"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Width", params); } });
                    }
                    else if (item.Column_Name == "Depth") {
                        columnDefs.push({ headerName: "Depth", field: "Depth", filter: getValuesAsync1("Depth"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Depth", params); } });
                    }
                    else if (item.Column_Name == "Depth(%)") {
                        columnDefs.push({ headerName: "Depth(%)", field: "Depth_Per", filter: getValuesAsync1("Depth_Per"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Depth_Per", params); } });
                    }
                    else if (item.Column_Name == "Table(%)") {
                        columnDefs.push({ headerName: "Table(%)", field: "Table_Per", filter: getValuesAsync1("Table_Per"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellStyle: function (params) { return cellStyle("Table_Per", params); } });
                    }
                    else if (item.Column_Name == "Key To Symbol") {
                        columnDefs.push({ headerName: "Key To Symbol", field: "Key_To_Symboll", filter: getValuesAsync1("Key_To_Symboll"), width: 300, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Key_To_Symboll", params); } });
                    }
                    else if (item.Column_Name == "Comment") {
                        columnDefs.push({ headerName: "Comment", field: "Lab_Comments", filter: getValuesAsync1("Lab_Comments"), width: 300, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Lab_Comments", params); } });
                    }
                    else if (item.Column_Name == "Girdle(%)") {
                        columnDefs.push({ headerName: "Girdle(%)", field: "Girdle_Per", filter: getValuesAsync1("Girdle_Per"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Girdle_Per", params); } });
                    }
                    else if (item.Column_Name == "Crown Angle") {
                        columnDefs.push({ headerName: "Crown Angle", field: "Crown_Angle", filter: getValuesAsync1("Crown_Angle"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Crown_Angle", params); } });
                    }
                    else if (item.Column_Name == "Crown Height") {
                        columnDefs.push({ headerName: "Crown Height", field: "Crown_Height", filter: getValuesAsync1("Crown_Height"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Crown_Height", params); } });
                    }
                    else if (item.Column_Name == "Pavilion Angle") {
                        columnDefs.push({ headerName: "Pavilion Angle", field: "Pav_Angle", filter: getValuesAsync1("Pav_Angle"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Pav_Angle", params); } });
                    }
                    else if (item.Column_Name == "Pavilion Height") {
                        columnDefs.push({ headerName: "Pavilion Height", field: "Pav_Height", filter: getValuesAsync1("Pav_Height"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Pav_Height", params); } });
                    }
                    else if (item.Column_Name == "Table Black") {
                        columnDefs.push({ headerName: "Table Black", field: "Table_Natts", filter: getValuesAsync1("Table_Natts"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Natts", params); } });
                    }
                    else if (item.Column_Name == "Crown Black") {
                        columnDefs.push({ headerName: "Crown Black", field: "Crown_Natts", filter: getValuesAsync1("Crown_Natts"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Natts", params); } });
                    }
                    else if (item.Column_Name == "Table White") {
                        columnDefs.push({ headerName: "Table White", field: "Table_Inclusion", filter: getValuesAsync1("Table_Inclusion"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Inclusion", params); } });
                    }
                    else if (item.Column_Name == "Crown White") {
                        columnDefs.push({ headerName: "Crown White", field: "Crown_Inclusion", filter: getValuesAsync1("Crown_Inclusion"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Inclusion", params); } });
                    }
                    else if (item.Column_Name == "Culet") {
                        columnDefs.push({ headerName: "Culet", field: "Culet", filter: getValuesAsync1("Culet"), width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Culet", params); } });
                    }
                    else if (item.Column_Name == "Table Open") {
                        columnDefs.push({ headerName: "Table Open", field: "Table_Open", filter: getValuesAsync1("Table_Open"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Open", params); } });
                    }
                    else if (item.Column_Name == "Crown Open") {
                        columnDefs.push({ headerName: "Crown Open", field: "Crown_Open", filter: getValuesAsync1("Crown_Open"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Open", params); } });
                    }
                    else if (item.Column_Name == "Pav Open") {
                        columnDefs.push({ headerName: "Pav Open", field: "Pav_Open", filter: getValuesAsync1("Pav_Open"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Pav_Open", params); } });
                    }
                    else if (item.Column_Name == "Girdle Open") {
                        columnDefs.push({ headerName: "Girdle Open", field: "Girdle_Open", filter: getValuesAsync1("Girdle_Open"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Girdle_Open", params); } });
                    }
                });
                if (columnDefs.length > 0) {
                    Search();
                }
            }
            else {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                toastr.error(data.Message);
            }
            //loaderHide();
        },
        error: function (xhr, textStatus, errorThrown) {
            loaderHide();
        }
    });
}
function BuyerList() {
    if ($("#txtDisc_1_1").val() != undefined) {
        if ($("#PricingMethod_1").val() == "" && $("#txtDisc_1_1").val() != "") {
            return toastr.warning("Please Select Pricing Method");
        }
        if ($("#PricingMethod_1").val() != "" && $("#txtDisc_1_1").val() == "") {
            return toastr.warning("Please Enter Pricing Method " + $("#PricingMethod_1").val());
        }
    }

    Type = "Buyer List";
    columnDefs = [];

    var obj = {};
    obj.Type = "BUYER";

    loaderShow();
    $.ajax({
        url: '/User/Get_SearchStock_ColumnSetting',
        type: "POST",
        data: { req: obj },
        success: function (data) {
            if (data.Status == "1" && data.Data.length == 0) {
                $("#divFilter").show();
                $("#divGridView").hide();
                toastr.error("No Columns Found.");
            }
            else if (data.Status == "1" && data.Data.length > 0) {
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
                data.Data.forEach(function (item) {
                    if (item.Column_Name == "DNA-Image-Video") {
                        columnDefs.push({ headerName: "VIEW", field: "DNA_Image_Video_Certi", filter: getValuesAsync1("DNA_Image_Video_Certi"), width: 85, cellRenderer: function (params) { return DNA_Image_Video_Certi(params, true, true, true, true); }, suppressSorting: true, suppressMenu: true, sortable: false });
                    }
                    else if (item.Column_Name == "Lab") {
                        columnDefs.push({
                            headerName: "Lab", field: "Lab",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Lab"),
                            filterParams: {
                                values: getValuesAsync("Lab"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 50, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Lab", params); }, cellRenderer: function (params) { return Lab(params); }
                        });
                    }
                    else if (item.Column_Name == "Supplier Name") {
                        columnDefs.push({ headerName: "Supplier Name", field: "SupplierName", filter: getValuesAsync1("SupplierName"), width: 200, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("SupplierShortName", params); } });
                    }
                    else if (item.Column_Name == "Rank") {
                        columnDefs.push({ headerName: "Rank", field: "Rank", filter: getValuesAsync1("Rank"), width: 50, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellStyle: function (params) { return cellStyle("Rank", params); } });
                    }
                    else if (item.Column_Name == "Supplier Status") {
                        columnDefs.push({ headerName: "Supplier Status", field: "SupplierShortName", filter: getValuesAsync1("SupplierShortName"), width: 180, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Supplier_Status", params); } });
                    }
                    else if (item.Column_Name == "Buyer Name") {
                        columnDefs.push({ headerName: "Buyer Name", field: "Buyer_Name", filter: getValuesAsync1("Buyer_Name"), width: 200, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Buyer_Name", params); } });
                    }
                    else if (item.Column_Name == "Status") {
                        columnDefs.push({ headerName: "Status", field: "Status", filter: getValuesAsync1("Status"), width: 50, tooltip: function (params) { return (params.value); }, cellRenderer: function (params) { return ""; }, cellStyle: function (params) { return cellStyle("Status", params); } });
                    }
                    else if (item.Column_Name == "Supplier Ref No") {
                        columnDefs.push({ headerName: "Supplier Ref No", field: "Supplier_Stone_Id", filter: getValuesAsync1("Supplier_Stone_Id"), width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Supplier_Stone_Id", params); } });
                    }
                    else if (item.Column_Name == "Cert No") {
                        columnDefs.push({ headerName: "Cert No", field: "Certificate_No", filter: getValuesAsync1("Certificate_No"), width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Certificate_No", params); } });
                    }
                    else if (item.Column_Name == "Shape") {
                        columnDefs.push({
                            headerName: "Shape", field: "Shape",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Shape"),
                            filterParams: {
                                values: getValuesAsync("Shape"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 100, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Shape", params); }
                        });
                    }
                    else if (item.Column_Name == "Pointer") {
                        columnDefs.push({
                            headerName: "Pointer", field: "Pointer",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Pointer"),
                            filterParams: {
                                values: getValuesAsync("Pointer"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Pointer", params); }
                        });
                    }
                    else if (item.Column_Name == "Sub Pointer") {
                        columnDefs.push({
                            headerName: "Sub Pointer", field: "Sub_Pointer",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Sub_Pointer"),
                            filterParams: {
                                values: getValuesAsync("Sub_Pointer"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Sub_Pointer", params); }
                        });
                    }
                    else if (item.Column_Name == "Color") {
                        columnDefs.push({
                            headerName: "Color", field: "Color",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Color"),
                            filterParams: {
                                values: getValuesAsync("Color"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Color", params); }
                        });
                    }
                    else if (item.Column_Name == "Clarity") {
                        columnDefs.push({
                            headerName: "Clarity", field: "Clarity",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Clarity"),
                            filterParams: {
                                values: getValuesAsync("Clarity"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Clarity", params); }
                        });
                    }
                    else if (item.Column_Name == "Cts") {
                        columnDefs.push({
                            headerName: "Cts", field: "Cts",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Cts"),
                            filterParams: {
                                values: getValuesAsync("Cts"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 70, tooltip: function (params) { return parseFloat(params.value).toFixed(2) }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2) }, cellStyle: function (params) { return cellStyle("Cts", params); }
                        });
                    }
                    else if (item.Column_Name == "Rap Rate($)") {
                        columnDefs.push({ headerName: "Rap Rate($)", field: "Rap_Rate", filter: getValuesAsync1("Rap_Rate"), width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Rap_Rate", params); } });
                    }
                    else if (item.Column_Name == "Rap Amount($)") {
                        columnDefs.push({ headerName: "Rap Amount($)", field: "Rap_Amount", filter: getValuesAsync1("Rap_Amount"), width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Rap_Amount", params); } });
                    }
                    else if (item.Column_Name == "Supplier Base Offer(%)") {
                        columnDefs.push({ headerName: "Supplier Base Offer(%)", field: "Disc", filter: getValuesAsync1("Disc"), width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Disc", params); } });
                    }
                    else if (item.Column_Name == "Supplier Base Offer Value($)") {
                        columnDefs.push({ headerName: "Supplier Base Offer Value($)", field: "Value", filter: getValuesAsync1("Value"), width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Value", params); } });
                    }
                    else if (item.Column_Name == "Supplier Final Disc(%)") {
                        columnDefs.push({ headerName: "Supplier Final Disc(%)", field: "SUPPLIER_COST_DISC", filter: getValuesAsync1("SUPPLIER_COST_DISC"), width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("SUPPLIER_COST_DISC", params); } });
                    }
                    else if (item.Column_Name == "Supplier Final Value($)") {
                        columnDefs.push({ headerName: "Supplier Final Value($)", field: "SUPPLIER_COST_VALUE", filter: getValuesAsync1("SUPPLIER_COST_VALUE"), width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("SUPPLIER_COST_VALUE", params); } });
                    }
                    else if (item.Column_Name == "Supplier Final Disc. With Max Slab(%)") {
                        columnDefs.push({ headerName: "Supplier Final Disc. With Max Slab(%)", field: "MAX_DISC", filter: getValuesAsync1("MAX_DISC"), width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("MAX_DISC", params); } });
                    }
                    else if (item.Column_Name == "Supplier Final Value With Max Slab($)") {
                        columnDefs.push({ headerName: "Supplier Final Value With Max Slab($)", field: "MAX_VALUE", filter: getValuesAsync1("MAX_VALUE"), width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("MAX_VALUE", params); } });
                    }
                    else if (item.Column_Name == "Bid Disc(%)") {
                        columnDefs.push({ headerName: "Bid Disc(%)", field: "Bid_Disc", filter: getValuesAsync1("Bid_Disc"), width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Bid_Disc", params); } });
                    }
                    else if (item.Column_Name == "Bid Amt") {
                        columnDefs.push({ headerName: "Bid Amt", field: "Bid_Amt", filter: getValuesAsync1("Bid_Amt"), width: 115, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Bid_Amt", params); } });
                    }
                    else if (item.Column_Name == "Bid/Ct") {
                        columnDefs.push({ headerName: "Bid/Ct", field: "Bid_Ct", filter: getValuesAsync1("Bid_Ct"), width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Bid_Ct", params); } });
                    }
                    else if (item.Column_Name == "Avg. Stock Disc(%)") {
                        columnDefs.push({ headerName: "Avg. Stock Disc(%)", field: "Avg_Stock_Disc", filter: getValuesAsync1("Avg_Stock_Disc"), width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Avg_Stock_Disc", params); } });
                    }
                    else if (item.Column_Name == "Avg. Stock Pcs") {
                        columnDefs.push({ headerName: "Avg. Stock Pcs", field: "Avg_Stock_Pcs", filter: getValuesAsync1("Avg_Stock_Pcs"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellStyle: function (params) { return cellStyle("Avg_Stock_Pcs", params); } });
                    }
                    else if (item.Column_Name == "Avg. Pur. Disc(%)") {
                        columnDefs.push({ headerName: "Avg. Pur. Disc(%)", field: "Avg_Pur_Disc", filter: getValuesAsync1("Avg_Pur_Disc"), width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellStyle: function (params) { return cellStyle("Avg_Pur_Disc", params); } });
                    }
                    else if (item.Column_Name == "Avg. Pur. Pcs") {
                        columnDefs.push({ headerName: "Avg. Pur. Pcs", field: "Avg_Pur_Pcs", filter: getValuesAsync1("Avg_Pur_Pcs"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellStyle: function (params) { return cellStyle("Avg_Pur_Pcs", params); } });
                    }
                    else if (item.Column_Name == "Avg. Sales Disc(%)") {
                        columnDefs.push({ headerName: "Avg. Sales Disc(%)", field: "Avg_Sales_Disc", filter: getValuesAsync1("Avg_Sales_Disc"), width: 105, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 4); }, cellStyle: function (params) { return cellStyle("Avg_Sales_Disc", params); } });
                    }
                    else if (item.Column_Name == "Sales Pcs") {
                        columnDefs.push({ headerName: "Sales Pcs", field: "Sales_Pcs", filter: getValuesAsync1("Sales_Pcs"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellStyle: function (params) { return cellStyle("Sales_Pcs", params); } });
                    }
                    else if (item.Column_Name == "KTS Grade") {
                        columnDefs.push({ headerName: "KTS Grade", field: "KTS_Grade", filter: getValuesAsync1("KTS_Grade"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("KTS_Grade", params); } });
                    }
                    else if (item.Column_Name == "Comm. Grade") {
                        columnDefs.push({ headerName: "Comm. Grade", field: "Comm_Grade", filter: getValuesAsync1("Comm_Grade"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Comm_Grade", params); } });
                    }
                    else if (item.Column_Name == "Zone") {
                        columnDefs.push({ headerName: "Zone", field: "Zone", filter: getValuesAsync1("Zone"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Zone", params); } });
                    }
                    else if (item.Column_Name == "Para. Grade") {
                        columnDefs.push({ headerName: "Para. Grade", field: "Para_Grade", filter: getValuesAsync1("Para_Grade"), tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Para_Grade", params); } });
                    }
                    else if (item.Column_Name == "Cut") {
                        columnDefs.push({
                            headerName: "Cut", field: "Cut",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Cut"),
                            filterParams: {
                                values: getValuesAsync("Cut"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Cut", params); }
                        });
                    }
                    else if (item.Column_Name == "Polish") {
                        columnDefs.push({
                            headerName: "Polish", field: "Polish",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Polish"),
                            filterParams: {
                                values: getValuesAsync("Polish"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Polish", params); }
                        });
                    }
                    else if (item.Column_Name == "Symm") {
                        columnDefs.push({
                            headerName: "Symm", field: "Symm",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Symm"),
                            filterParams: {
                                values: getValuesAsync("Symm"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Symm", params); }
                        });
                    }
                    else if (item.Column_Name == "Fls") {
                        columnDefs.push({
                            headerName: "Fls", field: "Fls",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Fls"),
                            filterParams: {
                                values: getValuesAsync("Fls"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Fls", params); }
                        });
                    }
                    else if (item.Column_Name == "Key To Symbol") {
                        columnDefs.push({ headerName: "Key To Symbol", field: "Key_To_Symboll", filter: getValuesAsync1("Key_To_Symboll"), width: 300, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Key_To_Symboll", params); } });
                    }
                    else if (item.Column_Name == "Ratio") {
                        columnDefs.push({ headerName: "Ratio", field: "RATIO", filter: getValuesAsync1("RATIO"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("RATIO", params); } });
                    }
                    else if (item.Column_Name == "Length") {
                        columnDefs.push({ headerName: "Length", field: "Length", filter: getValuesAsync1("Length"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Length", params); } });
                    }
                    else if (item.Column_Name == "Width") {
                        columnDefs.push({ headerName: "Width", field: "Width", filter: getValuesAsync1("Width"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Width", params); } });
                    }
                    else if (item.Column_Name == "Depth") {
                        columnDefs.push({ headerName: "Depth", field: "Depth", filter: getValuesAsync1("Depth"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Depth", params); } });
                    }
                    else if (item.Column_Name == "Depth(%)") {
                        columnDefs.push({ headerName: "Depth(%)", field: "Depth_Per", filter: getValuesAsync1("Depth_Per"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Depth_Per", params); } });
                    }
                    else if (item.Column_Name == "Table(%)") {
                        columnDefs.push({ headerName: "Table(%)", field: "Table_Per", filter: getValuesAsync1("Table_Per"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellStyle: function (params) { return cellStyle("Table_Per", params); } });
                    }
                    else if (item.Column_Name == "Crown Angle") {
                        columnDefs.push({ headerName: "Crown Angle", field: "Crown_Angle", filter: getValuesAsync1("Crown_Angle"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Crown_Angle", params); } });
                    }
                    else if (item.Column_Name == "Crown Height") {
                        columnDefs.push({ headerName: "Crown Height", field: "Crown_Height", filter: getValuesAsync1("Crown_Height"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Crown_Height", params); } });
                    }
                    else if (item.Column_Name == "Pavilion Angle") {
                        columnDefs.push({ headerName: "Pavilion Angle", field: "Pav_Angle", filter: getValuesAsync1("Pav_Angle"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Pav_Angle", params); } });
                    }
                    else if (item.Column_Name == "Pavilion Height") {
                        columnDefs.push({ headerName: "Pavilion Height", field: "Pav_Height", filter: getValuesAsync1("Pav_Height"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Pav_Height", params); } });
                    }
                    else if (item.Column_Name == "Girdle(%)") {
                        columnDefs.push({ headerName: "Girdle(%)", field: "Girdle_Per", filter: getValuesAsync1("Girdle_Per"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Girdle_Per", params); } });
                    }
                    else if (item.Column_Name == "Luster") {
                        columnDefs.push({ headerName: "Luster", field: "Luster", filter: getValuesAsync1("Luster"), width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Luster", params); } });
                    }
                    else if (item.Column_Name == "Cert Type") {
                        columnDefs.push({ headerName: "Cert Type", field: "Type_2A", filter: getValuesAsync1("Type_2A"), width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Type_2A", params); } });
                    }
                    else if (item.Column_Name == "Table White") {
                        columnDefs.push({ headerName: "Table White", field: "Table_Inclusion", filter: getValuesAsync1("Table_Inclusion"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Inclusion", params); } });
                    }
                    else if (item.Column_Name == "Crown White") {
                        columnDefs.push({ headerName: "Crown White", field: "Crown_Inclusion", filter: getValuesAsync1("Crown_Inclusion"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Inclusion", params); } });
                    }
                    else if (item.Column_Name == "Table Black") {
                        columnDefs.push({ headerName: "Table Black", field: "Table_Natts", filter: getValuesAsync1("Table_Natts"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Natts", params); } });
                    }
                    else if (item.Column_Name == "Crown Black") {
                        columnDefs.push({ headerName: "Crown Black", field: "Crown_Natts", filter: getValuesAsync1("Crown_Natts"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Natts", params); } });
                    }
                    else if (item.Column_Name == "Culet") {
                        columnDefs.push({ headerName: "Culet", field: "Culet", filter: getValuesAsync1("Culet"), width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Culet", params); } });
                    }
                    else if (item.Column_Name == "Comment") {
                        columnDefs.push({ headerName: "Comment", field: "Lab_Comments", filter: getValuesAsync1("Lab_Comments"), width: 300, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Lab_Comments", params); } });
                    }
                    else if (item.Column_Name == "Supplier Comment") {
                        columnDefs.push({ headerName: "Supplier Comment", field: "Supplier_Comments", filter: getValuesAsync1("Supplier_Comments"), width: 300, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Supplier_Comments", params); } });
                    }
                    else if (item.Column_Name == "Table Open") {
                        columnDefs.push({ headerName: "Table Open", field: "Table_Open", filter: getValuesAsync1("Table_Open"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Open", params); } });
                    }
                    else if (item.Column_Name == "Crown Open") {
                        columnDefs.push({ headerName: "Crown Open", field: "Crown_Open", filter: getValuesAsync1("Crown_Open"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Open", params); } });
                    }
                    else if (item.Column_Name == "Pav Open") {
                        columnDefs.push({ headerName: "Pav Open", field: "Pav_Open", filter: getValuesAsync1("Pav_Open"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Pav_Open", params); } });
                    }
                    else if (item.Column_Name == "Girdle Open") {
                        columnDefs.push({ headerName: "Girdle Open", field: "Girdle_Open", filter: getValuesAsync1("Girdle_Open"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Girdle_Open", params); } });
                    }
                    else if (item.Column_Name == "Shade") {
                        columnDefs.push({ headerName: "Shade", field: "Shade", filter: getValuesAsync1("Shade"), width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Shade", params); } });
                    }
                    else if (item.Column_Name == "Milky") {
                        columnDefs.push({ headerName: "Milky", field: "Milky", filter: getValuesAsync1("Milky"), width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Milky", params); } });
                    }
                });
                if (columnDefs.length > 0) {
                    Search();
                }
            }
            else {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                toastr.error(data.Message);
            }
            //loaderHide();
        },
        error: function (xhr, textStatus, errorThrown) {
            loaderHide();
        }
    });
}
function CustomerList() {
    if ($("#txtDisc_1_1").val() != undefined) {
        if ($("#PricingMethod_1").val() == "" && $("#txtDisc_1_1").val() != "") {
            return toastr.warning("Please Select Pricing Method");
        }
        if ($("#PricingMethod_1").val() != "" && $("#txtDisc_1_1").val() == "") {
            return toastr.warning("Please Enter Pricing Method " + $("#PricingMethod_1").val());
        }
    }

    Type = "Customer List";
    columnDefs = [];

    var obj = {};
    obj.Type = "CUSTOMER";

    loaderShow();
    $.ajax({
        url: '/User/Get_SearchStock_ColumnSetting',
        type: "POST",
        data: { req: obj },
        success: function (data) {
            if (data.Status == "1" && data.Data.length == 0) {
                $("#divFilter").show();
                $("#divGridView").hide();
                toastr.error("No Columns Found.");
            }
            else if (data.Status == "1" && data.Data.length > 0) {
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
                data.Data.forEach(function (item) {
                    if (item.Column_Name == "Image-Video-Certi") {
                        columnDefs.push({ headerName: "VIEW", field: "DNA_Image_Video_Certi", filter: getValuesAsync1("DNA_Image_Video_Certi"), width: 65, cellRenderer: function (params) { return DNA_Image_Video_Certi(params, false, true, true, true); }, suppressSorting: true, suppressMenu: true, sortable: false });
                    }
                    else if (item.Column_Name == "Ref No") {
                        columnDefs.push({ headerName: "Ref No", field: "Ref_No", filter: getValuesAsync1("Ref_No"), width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("RefNo", params); } });
                    }
                    else if (item.Column_Name == "Lab") {
                        columnDefs.push({
                            headerName: "Lab", field: "Lab",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Lab"),
                            filterParams: {
                                values: getValuesAsync("Lab"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 50, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Lab", params); }, cellRenderer: function (params) { return Lab(params); }
                        });
                    }
                    else if (item.Column_Name == "Cert No") {
                        columnDefs.push({ headerName: "Cert No", field: "Certificate_No", filter: getValuesAsync1("Certificate_No"), width: 110, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Certificate_No", params); } });
                    }
                    else if (item.Column_Name == "Shape") {
                        columnDefs.push({
                            headerName: "Shape", field: "Shape",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Shape"),
                            filterParams: {
                                values: getValuesAsync("Shape"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 100, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Shape", params); }
                        });
                    }
                    else if (item.Column_Name == "Pointer") {
                        columnDefs.push({
                            headerName: "Pointer", field: "Pointer",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Pointer"),
                            filterParams: {
                                values: getValuesAsync("Pointer"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Pointer", params); }
                        });
                    }
                    else if (item.Column_Name == "BGM") {
                        columnDefs.push({
                            headerName: "BGM", field: "BGM",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("BGM"),
                            filterParams: {
                                values: getValuesAsync("BGM"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("BGM", params); }
                        });
                    }
                    else if (item.Column_Name == "Color") {
                        columnDefs.push({
                            headerName: "Color", field: "Color",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Color"),
                            filterParams: {
                                values: getValuesAsync("Color"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Color", params); }
                        });
                    }
                    else if (item.Column_Name == "Clarity") {
                        columnDefs.push({
                            headerName: "Clarity", field: "Clarity",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Clarity"),
                            filterParams: {
                                values: getValuesAsync("Clarity"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Clarity", params); }
                        });
                    }
                    else if (item.Column_Name == "Cts") {
                        columnDefs.push({
                            headerName: "Cts", field: "Cts",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Cts"),
                            filterParams: {
                                values: getValuesAsync("Cts"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 70, tooltip: function (params) { return parseFloat(params.value).toFixed(2) }, cellRenderer: function (params) { return parseFloat(params.value).toFixed(2) }, cellStyle: function (params) { return cellStyle("Cts", params); }
                        });
                    }
                    else if (item.Column_Name == "Rap Rate($)") {
                        columnDefs.push({ headerName: "Rap Rate($)", field: "Rap_Rate", filter: getValuesAsync1("Rap_Rate"), width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Rap_Rate", params); } });
                    }
                    else if (item.Column_Name == "Rap Amount($)") {
                        columnDefs.push({ headerName: "Rap Amount($)", field: "Rap_Amount", filter: getValuesAsync1("Rap_Amount"), width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Rap_Amount", params); } });
                    }
                    else if (item.Column_Name == "Final Disc(%)") {
                        columnDefs.push({ headerName: "Final Disc(%)", field: "CUSTOMER_COST_DISC", filter: getValuesAsync1("CUSTOMER_COST_DISC"), width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("CUSTOMER_COST_DISC", params); } });
                    }
                    else if (item.Column_Name == "Final Amt US($)") {
                        columnDefs.push({ headerName: "Final Amt US($)", field: "CUSTOMER_COST_VALUE", filter: getValuesAsync1("CUSTOMER_COST_VALUE"), width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("CUSTOMER_COST_VALUE", params); } });
                    }
                    else if (item.Column_Name == "Price / Cts") {
                        columnDefs.push({ headerName: "Price / Cts", field: "Base_Price_Cts", filter: getValuesAsync1("Base_Price_Cts"), width: 110, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Base_Price_Cts", params); } });
                    }
                    else if (item.Column_Name == "Cut") {
                        columnDefs.push({
                            headerName: "Cut", field: "Cut",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Cut"),
                            filterParams: {
                                values: getValuesAsync("Cut"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Cut", params); }
                        });
                    }
                    else if (item.Column_Name == "Polish") {
                        columnDefs.push({
                            headerName: "Polish", field: "Polish",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Polish"),
                            filterParams: {
                                values: getValuesAsync("Polish"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Polish", params); }
                        });
                    }
                    else if (item.Column_Name == "Symm") {
                        columnDefs.push({
                            headerName: "Symm", field: "Symm",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Symm"),
                            filterParams: {
                                values: getValuesAsync("Symm"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, tooltip: function (params) { return (params.value); }, width: 70, cellStyle: function (params) { return cellStyle("Symm", params); }
                        });
                    }
                    else if (item.Column_Name == "Fls") {
                        columnDefs.push({
                            headerName: "Fls", field: "Fls",
                            menuTabs: ['filterMenuTab', 'generalMenuTab', 'columnsMenuTab'],
                            filter: getValuesAsync1("Fls"),
                            filterParams: {
                                values: getValuesAsync("Fls"),
                                resetButton: true,
                                applyButton: true,
                                filterOptions: ['inRange'],
                                comparator: function (a, b) {
                                    return 0;
                                }
                            }, width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Fls", params); }
                        });
                    }
                    else if (item.Column_Name == "RATIO") {
                        columnDefs.push({ headerName: "RATIO", field: "RATIO", filter: getValuesAsync1("RATIO"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("RATIO", params); } });
                    }
                    else if (item.Column_Name == "Key To Symbol") {
                        columnDefs.push({ headerName: "Key To Symbol", field: "Key_To_Symboll", filter: getValuesAsync1("Key_To_Symboll"), width: 300, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Key_To_Symboll", params); } });
                    }
                    else if (item.Column_Name == "Length") {
                        columnDefs.push({ headerName: "Length", field: "Length", filter: getValuesAsync1("Length"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Length", params); } });
                    }
                    else if (item.Column_Name == "Width") {
                        columnDefs.push({ headerName: "Width", field: "Width", filter: getValuesAsync1("Width"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Width", params); } });
                    }
                    else if (item.Column_Name == "Depth") {
                        columnDefs.push({ headerName: "Depth", field: "Depth", filter: getValuesAsync1("Depth"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Depth", params); } });
                    }
                    else if (item.Column_Name == "Depth(%)") {
                        columnDefs.push({ headerName: "Depth (%)", field: "Depth_Per", filter: getValuesAsync1("Depth_Per"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Depth_Per", params); } });
                    }
                    else if (item.Column_Name == "Table(%)") {
                        columnDefs.push({ headerName: "Table (%)", field: "Table_Per", filter: getValuesAsync1("Table_Per"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 0); }, cellStyle: function (params) { return cellStyle("Table_Per", params); } });
                    }
                    else if (item.Column_Name == "Comment") {
                        columnDefs.push({ headerName: "Comment", field: "Lab_Comments", filter: getValuesAsync1("Lab_Comments"), width: 300, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Lab_Comments", params); } });
                    }
                    else if (item.Column_Name == "Girdle(%)") {
                        columnDefs.push({ headerName: "Girdle(%)", field: "Girdle_Per", filter: getValuesAsync1("Girdle_Per"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Girdle_Per", params); } });
                    }
                    else if (item.Column_Name == "Crown Angle") {
                        columnDefs.push({ headerName: "Crown Angle", field: "Crown_Angle", filter: getValuesAsync1("Crown_Angle"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Crown_Angle", params); } });
                    }
                    else if (item.Column_Name == "Crown Height") {
                        columnDefs.push({ headerName: "Crown Height", field: "Crown_Height", filter: getValuesAsync1("Crown_Height"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Crown_Height", params); } });
                    }
                    else if (item.Column_Name == "Pav Angle") {
                        columnDefs.push({ headerName: "Pav Angle", field: "Pav_Angle", filter: getValuesAsync1("Pav_Angle"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Pav_Angle", params); } });
                    }
                    else if (item.Column_Name == "Pav Height") {
                        columnDefs.push({ headerName: "Pav Height", field: "Pav_Height", filter: getValuesAsync1("Pav_Height"), width: 70, tooltip: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellRenderer: function (params) { return NullReplaceCommaPointDecimalToFixed(params.value, 2); }, cellStyle: function (params) { return cellStyle("Pav_Height", params); } });
                    }
                    else if (item.Column_Name == "Table Black") {
                        columnDefs.push({ headerName: "Table Black", field: "Table_Natts", filter: getValuesAsync1("Table_Natts"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Natts", params); } });
                    }
                    else if (item.Column_Name == "Crown Black") {
                        columnDefs.push({ headerName: "Crown Black", field: "Crown_Natts", filter: getValuesAsync1("Crown_Natts"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Natts", params); } });
                    }
                    else if (item.Column_Name == "Table White") {
                        columnDefs.push({ headerName: "Table White", field: "Table_Inclusion", filter: getValuesAsync1("Table_Inclusion"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Inclusion", params); } });
                    }
                    else if (item.Column_Name == "Crown White") {
                        columnDefs.push({ headerName: "Crown White", field: "Crown_Inclusion", filter: getValuesAsync1("Crown_Inclusion"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Inclusion", params); } });
                    }
                    else if (item.Column_Name == "Culet") {
                        columnDefs.push({ headerName: "Culet", field: "Culet", filter: getValuesAsync1("Culet"), width: 80, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Culet", params); } });
                    }
                    else if (item.Column_Name == "Table Open") {
                        columnDefs.push({ headerName: "Table Open", field: "Table_Open", filter: getValuesAsync1("Table_Open"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Table_Open", params); } });
                    }
                    else if (item.Column_Name == "Crown Open") {
                        columnDefs.push({ headerName: "Crown Open", field: "Crown_Open", filter: getValuesAsync1("Crown_Open"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Crown_Open", params); } });
                    }
                    else if (item.Column_Name == "Pavilion Open") {
                        columnDefs.push({ headerName: "Pavilion Open", field: "Pav_Open", filter: getValuesAsync1("Pav_Open"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Pav_Open", params); } });
                    }
                    else if (item.Column_Name == "Girdle Open") {
                        columnDefs.push({ headerName: "Girdle Open", field: "Girdle_Open", filter: getValuesAsync1("Girdle_Open"), width: 70, tooltip: function (params) { return (params.value); }, cellStyle: function (params) { return cellStyle("Girdle_Open", params); } });
                    }
                });
                if (columnDefs.length > 0) {
                    Search();
                }
            }
            else {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                toastr.error(data.Message);
            }
            //loaderHide();
        },
        error: function (xhr, textStatus, errorThrown) {
            loaderHide();
        }
    });
}

function getValuesAsync1(field) {
    if (field == "Cts") {
        return "agNumberColumnFilter";
    }
    else if (field == "Lab" || field == "Shape" || field == "Pointer" || field == "Sub_Pointer" || field == "BGM" || field == "Color" || field == "Clarity" || field == "Cut" || field == "Polish" || field == "Symm" || field == "Fls") {
        return "agSetColumnFilter";
    }
    else {
        return false;
    }
}
function getValuesAsync(field) {
    if (field == "Lab") {
        return _.pluck(LabList, 'Value');
    }
    else if (field == "Shape") {
        var ShapeList_new = ShapeList.filter((obj) => obj.Id !== 0);
        return _.pluck(ShapeList_new, 'Value');
    }
    else if (field == "Pointer") {
        return _.pluck(CaratList, 'Value');
    }
    else if (field == "Sub_Pointer") {
        return _.pluck(SubPointerList, 'Value');
    }
    else if (field == "BGM") {
        return _.pluck(BGMList, 'Value');
    }
    else if (field == "Color") {
        return _.pluck(ColorList, 'Value');
    }
    else if (field == "Clarity") {
        return _.pluck(ClarityList, 'Value');
    }
    else if (field == "Cts") {
        return _.pluck(CaratList, 'Value');
    }
    else if (field == "Cut") {
        return _.pluck(CutList, 'Value');
    }
    else if (field == "Polish") {
        return _.pluck(PolishList, 'Value');
    }
    else if (field == "Symm") {
        return _.pluck(SymList, 'Value');
    }
    else if (field == "Fls") {
        return _.pluck(FlouList, 'Value');
    }
    else {
        return null;
    }
}
function Search() {
    $("#divFilter").hide();
    $("#divGridView").show();

    GetHoldDataGrid();
}
function GetHoldDataGrid() {
    //loaderShow();
    //$('#tab1TCount').hide();

    if (Type == "Buyer List") {
        $(".totdisc").html("Supplier Final Disc(%)");
        $(".totval").html("Supplier Final Value($)");
    }
    else if (Type == "Supplier List" || Type == "Customer List") {
        $(".totdisc").html("Final Disc(%)");
        $(".totval").html("Final Amt US($)");
    }

    $(".tab1Pcs").html("0");
    $(".tab1CTS").html("0.0");
    $(".tab1OfferDisc").html("0.0");
    $(".tab1OfferValue").html("0.0");
    $(".tab1PriceCts").html("0.0");


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
        overlayLoadingTemplate: '<span class="ag-overlay-loading-center">NO DATA TO SHOW..</span>',
        suppressRowClickSelection: true,
        columnDefs: columnDefs,
        rowModelType: 'serverSide',
        //rowData: data,
        onRowSelected: onSelectionChanged,
        onBodyScroll: onBodyScroll,

        cacheBlockSize: pgSize, // you can have your custom page size
        paginationPageSize: pgSize, //pagesize
        getContextMenuItems: getContextMenuItems,
        //getMainMenuItems: getMainMenuItems,
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
var Filter_Pointer = '';
var Filter_Lab = '';
var Filter_Shape = '';
var Filter_Sub_Pointer = '';
var Filter_BGM = '';
var Filter_ColorType = '';
var Filter_Color = '';
var Filter_Clarity = '';
var Filter_Cut = '';
var Filter_Polish = '';
var Filter_Symm = '';
var Filter_Fls = '';
const datasource1 = {
    getRows(params) {
        Filter_Pointer = '';
        Filter_Lab = '';
        Filter_Shape = '';
        Filter_Sub_Pointer = '';
        Filter_BGM = '';
        Filter_ColorType = '';
        Filter_Color = '';
        Filter_Clarity = '';
        Filter_Cut = '';
        Filter_Polish = '';
        Filter_Symm = '';
        Filter_Fls = '';

        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        var obj = {};
        OrderBy = "";
        if (params.request.sortModel.length > 0) {
            OrderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort;
        }

        obj = ObjectCreate(PageNo, pgSize, OrderBy, '');
        obj.View = true;
        obj.Download = false;

        if (params.request.filterModel.Cts) {
            var str = "";
            if (params.request.filterModel.Cts.operator == "AND" || params.request.filterModel.Cts.operator == "OR") {
                if (params.request.filterModel.Cts.condition1) {
                    str = params.request.filterModel.Cts.condition1.filter + "-";
                    if (params.request.filterModel.Cts.condition1.filterTo != null) {
                        str = str + params.request.filterModel.Cts.condition1.filterTo
                    } else {
                        str = str + params.request.filterModel.Cts.condition1.filter
                    }
                }
                if (params.request.filterModel.Cts.condition2) {
                    if (str != "")
                        str = str + ",";
                    str = params.request.filterModel.Cts.condition2.filter + "-";
                    if (params.request.filterModel.Cts.condition2.filterTo != null) {
                        str = str + params.request.filterModel.Cts.condition2.filterTo
                    } else {
                        str = str + params.request.filterModel.Cts.condition2.filter
                    }
                }
            }
            else {
                str = params.request.filterModel.Cts.filter + "-";
                if (params.request.filterModel.Cts.filterTo != null) {
                    str = str + params.request.filterModel.Cts.filterTo
                } else {
                    str = str + params.request.filterModel.Cts.filter
                }
            }
            obj.Pointer = str;
            Filter_Pointer = obj.Pointer;
        }

        if (params.request.filterModel.Lab) {
            obj.Lab = params.request.filterModel.Lab.values.join(",");
            Filter_Lab = obj.Lab;
        }
        if (params.request.filterModel.Shape) {
            obj.Shape = params.request.filterModel.Shape.values.join(",");
            Filter_Shape = obj.Shape;
        }
        if (params.request.filterModel.Pointer) {
            obj.Pointer = params.request.filterModel.Pointer.values.join(",");
            Filter_Pointer = obj.Pointer;
        }
        if (params.request.filterModel.Sub_Pointer) {
            obj.Sub_Pointer = params.request.filterModel.Sub_Pointer.values.join(",");
            Filter_Sub_Pointer = obj.Sub_Pointer;
        }
        if (params.request.filterModel.BGM) {
            obj.BGM = params.request.filterModel.BGM.values.join(",");
            Filter_BGM = obj.BGM;
        }
        if (params.request.filterModel.Color) {
            obj.ColorType = "Regular";
            obj.Color = params.request.filterModel.Color.values.join(",");
            Filter_ColorType = obj.ColorType;
            Filter_Color = obj.Color;
        }
        if (params.request.filterModel.Clarity) {
            obj.Clarity = params.request.filterModel.Clarity.values.join(",");
            Filter_Clarity = obj.Clarity;
        }
        if (params.request.filterModel.Cut) {
            obj.Cut = params.request.filterModel.Cut.values.join(",");
            Filter_Cut = obj.Cut;
        }
        if (params.request.filterModel.Polish) {
            obj.Polish = params.request.filterModel.Polish.values.join(",");
            Filter_Polish = obj.Polish;
        }
        if (params.request.filterModel.Symm) {
            obj.Symm = params.request.filterModel.Symm.values.join(",");
            Filter_Symm = obj.Symm;
        }
        if (params.request.filterModel.Fls) {
            obj.Fls = params.request.filterModel.Fls.values.join(",");
            Filter_Fls = obj.Fls;
        }


        Rowdata = [], summary = [];
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
                    summary = data.Data[0].DataSummary;
                    $.map(data.Data[0].DataList, function (obj) {
                        Rowdata.push(obj);
                    });
                    params.successCallback(data.Data[0].DataList, summary.TOT_PCS);

                    //$('#tab1TCount').show();
                    $('.tab1Pcs').html(formatIntNumber(summary.TOT_PCS));
                    $('.tab1CTS').html(formatNumber(summary.TOT_CTS));
                    $('.tab1OfferDisc').html(formatNumber(summary.AVG_SALES_DISC_PER));
                    $('.tab1OfferValue').html(formatNumber(summary.TOT_NET_AMOUNT));
                    $('.tab1PriceCts').html(formatNumber(summary.AVG_PRICE_PER_CTS));
                }
                else {
                    if (data.Data.length == 0) {
                        gridOptions.api.showNoRowsOverlay();
                        Rowdata = [], summary = [];

                        $("#divFilter").show();
                        $("#divGridView").hide();
                        //$('#tab1TCount').hide();
                        $('.tab1Pcs').html(formatIntNumber(0));
                        $('.tab1CTS').html(formatNumber(0));
                        $('.tab1OfferDisc').html(formatNumber(0));
                        $('.tab1OfferValue').html(formatNumber(0));
                        $('.tab1PriceCts').html(formatNumber(0));
                        toastr.error("No Stock found as per filter criteria !");
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
function ExcelFilter(type, from) {
    if ($("#txtDisc_1_1").val() != undefined) {
        if ($("#PricingMethod_1").val() == "" && $("#txtDisc_1_1").val() != "") {
            return toastr.warning("Please Select Pricing Method");
        }
        if ($("#PricingMethod_1").val() != "" && $("#txtDisc_1_1").val() == "") {
            return toastr.warning("Please Enter Pricing Method " + $("#PricingMethod_1").val());
        }
    }

    Type = "";
    if (type == "1") {
        Type = "Buyer List";
    }
    else if (type == "2") {
        Type = "Supplier List";
    }
    else if (type == "3") {
        Type = "Customer List";
    }
    if (Type != "") {
        ExcelDownload('Filter', from);
    }
}
function ExcelDownload(where, from) {
    loaderShow();
    setTimeout(function () {
        var obj = {};
        obj = ObjectCreate("", "", OrderBy, where);
        obj.View = false;
        obj.Download = true;

        if (from == 'In') {
            obj.Pointer = (Filter_Pointer != "" ? Filter_Pointer : obj.Pointer);
            obj.Lab = (Filter_Lab != "" ? Filter_Lab : obj.Lab);
            obj.Shape = (Filter_Shape != "" ? Filter_Shape : obj.Shape);
            obj.Sub_Pointer = (Filter_Sub_Pointer != "" ? Filter_Sub_Pointer : obj.Sub_Pointer);
            obj.BGM = (Filter_BGM != "" ? Filter_BGM : obj.BGM);
            obj.ColorType = (Filter_ColorType != "" ? Filter_ColorType : obj.ColorType);
            obj.Color = (Filter_Color != "" ? Filter_Color : obj.Color);
            obj.Clarity = (Filter_Clarity != "" ? Filter_Clarity : obj.Clarity);
            obj.Cut = (Filter_Cut != "" ? Filter_Cut : obj.Cut);
            obj.Polish = (Filter_Polish != "" ? Filter_Polish : obj.Polish);
            obj.Symm = (Filter_Symm != "" ? Filter_Symm : obj.Symm);
            obj.Fls = (Filter_Fls != "" ? Filter_Fls : obj.Fls);
        }
        debugger

        var Stock_Disc_Count = 0;

        var _obj = {};
        _obj.UserId = obj.UserId

        $.ajax({
            url: '/User/Get_Customer_Stock_Disc_Count',
            type: "POST",
            async: false,
            data: { req: _obj },
            success: function (data) {
                debugger
                if (data.Status == "1" && data.Data.length > 0) {
                    Stock_Disc_Count = parseFloat(data.Data[0].TotCount);
                }
            },
            error: function (xhr, textStatus, errorThrown) {
            }
        });

        debugger
        if (obj.PgNo == "" && obj.PgSize == "" && obj.OrderBy == "" &&
            obj.Location == "" &&
            obj.RefNo == "" &&
            obj.SupplierId == "" &&
            obj.Shape == "" &&
            obj.Pointer == "" &&
            obj.ColorType == "Regular" && obj.Color == "" && obj.INTENSITY == "" && obj.OVERTONE == "" && obj.FANCY_COLOR == "" &&
            obj.Clarity == "" &&
            obj.Cut == "" &&
            obj.Polish == "" &&
            obj.Symm == "" &&
            obj.Fls == "" &&
            obj.BGM == "" &&
            obj.Lab == "" &&
            obj.CrownBlack == "" && obj.TableBlack == "" && obj.TableWhite == "" && obj.CrownWhite == "" &&
            obj.TableOpen == "" && obj.CrownOpen == "" && obj.PavOpen == "" && obj.GirdleOpen == "" &&
            obj.KTSBlank == "" && obj.Keytosymbol == "-" && obj.CheckKTS == "" && obj.UNCheckKTS == "" &&
            obj.RCommentBlank == "" && obj.RComment == "-" && obj.CheckRComment == "" && obj.UNCheckRComment == "" &&
            obj.FromDisc == "" && obj.ToDisc == "" &&
            obj.FromTotAmt == "" && obj.ToTotAmt == "" &&
            obj.LengthBlank == "" && obj.FromLength == "" && obj.ToLength == "" &&
            obj.WidthBlank == "" && obj.FromWidth == "" && obj.ToWidth == "" &&
            obj.DepthBlank == "" && obj.FromDepth == "" && obj.ToDepth == "" &&
            obj.DepthPerBlank == "" && obj.FromDepthPer == "" && obj.ToDepthPer == "" &&
            obj.TablePerBlank == "" && obj.FromTablePer == "" && obj.ToTablePer == "" &&
            obj.GirdlePerBlank == "" && obj.FromGirdlePer == "" && obj.ToGirdlePer == "" &&
            obj.Img == "" && obj.Vdo == "" && obj.Certi == "" &&
            obj.CrAngBlank == "" && obj.FromCrAng == "" && obj.ToCrAng == "" &&
            obj.CrHtBlank == "" && obj.FromCrHt == "" && obj.ToCrHt == "" &&
            obj.PavAngBlank == "" && obj.FromPavAng == "" && obj.ToPavAng == "" &&
            obj.PavHtBlank == "" && obj.FromPavHt == "" && obj.ToPavHt == "" &&
            obj.StarLengthBlank == "" && obj.FromStarLength == "" && obj.ToStarLength == "" &&
            obj.LowerHalfBlank == "" && obj.FromLowerHalf == "" && obj.ToLowerHalf == "" &&
            obj.View == false && obj.Download == true &&
            (obj.PricingDisc == undefined || obj.PricingDisc == "") &&
            Stock_Disc_Count == 0) {
            debugger
            $.ajax({
                url: "/User/Get_Auto_Excel_Download",
                async: false,
                type: "POST",
                data: { req: obj },
                success: function (data, textStatus, jqXHR) {
                    debugger
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
        }
        else {
            debugger
            $.ajax({
                url: "/User/Excel_SearchStock",
                async: false,
                type: "POST",
                data: { req: obj },
                success: function (data, textStatus, jqXHR) {
                    debugger
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
        }
    }, 50);
}

function ObjectCreate(PageNo, pgSize, OrderBy, where) {
    obj = {};

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
    var Selected_Ref_No = (where == 'Filter' ? '' : _.pluck(_.filter(gridOptions.api.getSelectedRows()), 'Ref_No').join(","));
    var supplier = "";
    if ($('#ddlSupplierId').val() != undefined) {
        supplier = $('#ddlSupplierId').val().join(",");
    }
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
    var location = _.pluck(_.filter(LocationList, function (e) { return e.isActive == true }), 'Value').join(",");


    var KeyToSymLst_Check = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
    var KeyToSymLst_uncheck = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");


    var RCommentLst_Check = _.pluck(CheckRCommentList, 'Symbol').join(",");
    var RCommentLst_uncheck = _.pluck(UnCheckRCommentList, 'Symbol').join(",");


    if ($('#ddlUserId').val() != undefined) {
        if ($('#ddlUserId').val() == "") {
            obj.UserId = $('#hdn_UserId').val();
        }
        else {
            obj.UserId = $('#ddlUserId').val();
        }
    }
    else {
        obj.UserId = $("#hdn_UserId").val();
    }
    obj.PgNo = PageNo;
    obj.PgSize = pgSize;
    obj.OrderBy = OrderBy;

    obj.RefNo = (Selected_Ref_No == "" ? refno : Selected_Ref_No);
    obj.Location = location;
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

    obj.RCommentBlank = (RCommentBlank == true ? true : "");
    obj.RComment = RCommentLst_Check + '-' + RCommentLst_uncheck;
    obj.CheckRComment = RCommentLst_Check;
    obj.UNCheckRComment = RCommentLst_uncheck;

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

    obj.GirdlePerBlank = (GirdlePerBlank == true ? true : "");
    obj.FromGirdlePer = $('#FromGirdlePer').val();
    obj.ToGirdlePer = $('#ToGirdlePer').val();

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

    obj.StarLengthBlank = (StarLengthBlank == true ? true : "");
    obj.FromStarLength = $('#FromStarLength').val();
    obj.ToStarLength = $('#ToStarLength').val();

    obj.LowerHalfBlank = (LowerHalfBlank == true ? true : "");
    obj.FromLowerHalf = $('#FromLowerHalf').val();
    obj.ToLowerHalf = $('#ToLowerHalf').val();

    obj.Type = Type;

    if ($("#txtDisc_1_1").val() != undefined && $("#txtDisc_1_1").val() != "") {
        obj.PricingMethod = $("#PricingMethod_1").val();
        obj.PricingSign = $("#PricingSign_1").val();
        obj.PricingDisc = $("#txtDisc_1_1").val();
    }

    return obj;
}

function onGridReady(params) {
    if (navigator.userAgent.indexOf('Windows') > -1) {
        this.api.sizeColumnsToFit();
    }
    const api = params.api;
    api.selectTab('ag-tab-header', 'ag-icon-filter');
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
function DNA_Image_Video_Certi(params, DNA, Img, Vdo, Cert) {
    if (params.data == undefined) {
        return '';
    }

    var DNA_url = (params.data.DNA == null ? "" : params.data.DNA);
    var image_url = (params.data.Image_URL == null ? "" : params.data.Image_URL);
    var movie_url = (params.data.Video_URL == null ? "" : params.data.Video_URL);
    var certi_url = (params.data.Certificate_URL == null ? "" : params.data.Certificate_URL);

    if (DNA == true) {
        if (DNA_url != "") {
            DNA_url = '<li><a href="' + DNA_url + '" target="_blank" title="View Diamond DNA">' +
                '<img src="../Content/images/medal.svg" class="frame-icon"></a></li>';
        }
        else {
            DNA_url = '<li><a href="javascript:void(0);" title="View Diamond DNA">' +
                '<img src="../Content/images/medal-not-available.svg" class="frame-icon"></a></li>';
        }
    }
    else {
        DNA_url = "";
    }

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

    var data = ('<ul class="flat-icon-ul">' + DNA_url + image_url + movie_url + '</ul>');
    return data;
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
        if (Type == "Buyer List") {
            dDisc = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'SUPPLIER_COST_DISC'), function (memo, num) { return memo + num; }, 0);
            TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'SUPPLIER_COST_VALUE'), function (memo, num) { return memo + num; }, 0);
        }
        else if (Type == "Supplier List" || Type == "Customer List") {
            dDisc = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'CUSTOMER_COST_DISC'), function (memo, num) { return memo + num; }, 0);
            TOT_NET_AMOUNT = _.reduce(_.pluck(gridOptions.api.getSelectedRows(), 'CUSTOMER_COST_VALUE'), function (memo, num) { return memo + num; }, 0);
        }
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
        TOT_CTS = summary.TOT_CTS;
        AVG_SALES_DISC_PER = summary.AVG_SALES_DISC_PER;
        AVG_PRICE_PER_CTS = summary.AVG_PRICE_PER_CTS;
        TOT_NET_AMOUNT = summary.TOT_NET_AMOUNT;
        TOT_PCS = summary.TOT_PCS;
    }
    setTimeout(function () {
        $('.tab1Pcs').html(formatIntNumber(TOT_PCS));
        $('.tab1CTS').html(formatNumber(TOT_CTS));
        $('.tab1OfferDisc').html(formatNumber(AVG_SALES_DISC_PER));
        $('.tab1OfferValue').html(formatNumber(TOT_NET_AMOUNT));
        $('.tab1PriceCts').html(formatNumber(AVG_PRICE_PER_CTS));
    });
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
var LocationList = [];
var ShapeList = [];
var CaratList = [];
var SubPointerList = [];
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
var RCommentBlank = false;
var LengthBlank = false;
var WidthBlank = false;
var DepthBlank = false;
var DepthPerBlank = false;
var TablePerBlank = false;
var GirdlePerBlank = false;
var CrAngBlank = false;
var CrHtBlank = false;
var PavAngBlank = false;
var PavHtBlank = false;
var StarLengthBlank = false;
var LowerHalfBlank = false;

var TableOpenList = [];
var CrownOpenList = [];
var PavOpenList = [];
var GirdleOpenList = [];

var KeyToSymbolList = [];
var CheckKeyToSymbolList = [];
var UnCheckKeyToSymbolList = [];

var RCommentList = [];
var CheckRCommentList = [];
var UnCheckRCommentList = [];


var Check_Color_1 = [];
var Check_Color_2 = [];
var Check_Color_3 = [];
var RC = 0, KTS = 0, C1 = 0, C2 = 0, C3 = 0;
var INTENSITY = [], OVERTONE = [], FANCY_COLOR = [];
var Color_Type = 'Regular';
var IsFiltered = true;
var ActivityType = "";
function _checkValue(textbox) {
    const value = textbox.value.trim();
    const numericValue = parseFloat(value);
    if (numericValue >= 0 && numericValue <= 100) {
        textbox.value = NullReplaceDecimal4ToFixed(numericValue);
    } else {
        textbox.value = '';
    }
}
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
            $("#sym-sec4 .carat-dropdown-main").hide();
            $("#sym-sec1 .carat-dropdown-main").show();
            C1 = 1;
            RC = 0, KTS = 0, C2 = 0, C3 = 0;
        }
        else {
            $("#sym-sec1 .carat-dropdown-main").hide();
            RC = 0, C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function OVERTONEShow() {
    setTimeout(function () {
        if (C2 == 0) {
            $("#sym-sec0 .carat-dropdown-main").hide();
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").hide();
            $("#sym-sec4 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").show();
            C2 = 1;
            RC = 0, C1 = 0, KTS = 0, C3 = 0;
        }
        else {
            $("#sym-sec2 .carat-dropdown-main").hide();
            RC = 0, C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function FANCY_COLORShow() {
    setTimeout(function () {
        if (C3 == 0) {
            $("#sym-sec0 .carat-dropdown-main").hide();
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").hide();
            $("#sym-sec4 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").show();
            C3 = 1;
            RC = 0, C1 = 0, KTS = 0, C2 = 0;
        }
        else {
            $("#sym-sec3 .carat-dropdown-main").hide();
            C = 0, C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function Key_to_symbolShow() {
    setTimeout(function () {
        if (KTS == 0) {
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").hide();
            $("#sym-sec4 .carat-dropdown-main").hide();
            $("#sym-sec0 .carat-dropdown-main").show();
            KTS = 1;
            RC = 0, C1 = 0, C2 = 0, C3 = 0;
        }
        else {
            $("#sym-sec0 .carat-dropdown-main").hide();
            RC = 0, C1 = 0, KTS = 0, C2 = 0, C3 = 0;
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
    $("#hdn_PageName").val("SearchStock");
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
    Master_Get();
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
    BindRCommentList();
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

    $('.numeric_value').on('paste', function (event) {
        if (event.originalEvent.clipboardData.getData('Text').match(/[^\d]/)) {
            event.preventDefault();
        }
    });

    $('#EmailModal').on('show.bs.modal', function (event) {
        debugger
        ClearSendMail();
    });
    $('#SaveSearchModal').on('show.bs.modal', function (event) {
        $("#frmSaveSearch").validate().resetForm();
    });
    SetSavedSearchParameter();
    $("#li_User_SearchStock").addClass("menuActive");
    GetCompanyList();
});
function validmail(e) {
    debugger
    var emailID = $(e).val();
    if (emailID != "") {
        emailID = emailID.split(',');
        var finalemailId = "";
        for (var i = 0; i < emailID.length; i++) {
            if (!checkemail(emailID[i])) {
                //toastr.error("Invalid Email Format");
                //$("#txtemail").val('');
                //return;
            }
            else {
                finalemailId += emailID[i] + ",";
            }
        }
        $("#txtemail").val(finalemailId);
        return;
    }
}
function checkemail(valemail) {
    var forgetfilter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)(\s*(;|,)\s*|\s*$)/;  ///^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    if (forgetfilter.test(valemail)) {
        return true;
    }
    else {
        return false;
    }
}
function ClearSendMail() {
    debugger
    //$('#txtemail').val("");
    $('#txtNotes').val("");
    var count = 0;
    count = gridOptions.api.getSelectedRows().length;
    if (count > 0) {
        $('#customRadiomail2').prop('checked', true);
    } else {
        $('#customRadiomail').prop('checked', true);
    }
}
SetCutMaster = function (item) {
    _.each(CutList, function (itm) {
        $('#searchcut li[onclick="SetActive(\'CUT\',\'' + itm.Value + '\')"]').removeClass('active');
        itm.isActive = false;
    });
    _.each(PolishList, function (itm) {
        $('#searchpolish li[onclick="SetActive(\'POLISH\',\'' + itm.Value + '\')"]').removeClass('active');
        itm.isActive = false;
    });
    _.each(SymList, function (itm) {
        $('#searchsymm li[onclick="SetActive(\'SYMM\',\'' + itm.Value + '\')"]').removeClass('active');
        itm.isActive = false;
    });
    if (item == '3EX' && !$('#li3ex').hasClass('active')) {
        $('#li3vg').removeClass('active');

        _.each(_.filter(CutList, function (e) { return e.Value == "EX" || e.Value == "3EX" }), function (itm) {
            $('#searchcut li[onclick="SetActive(\'CUT\',\'' + itm.Value + '\')"]').addClass('active');
            itm.isActive = true;
        });
        _.each(_.filter(PolishList, function (e) { return e.Value == "EX" }), function (itm) {
            $('#searchpolish li[onclick="SetActive(\'POLISH\',\'EX\')"]').addClass('active');
            itm.isActive = true;
        });
        _.each(_.filter(SymList, function (e) { return e.Value == "EX" }), function (itm) {
            $('#searchsymm li[onclick="SetActive(\'SYMM\',\'EX\')"]').addClass('active');
            itm.isActive = true;
        });
    }
    else if (item == '3VG' && !$('#li3vg').hasClass('active')) {

        $('#li3ex').removeClass('active');
        _.each(_.filter(CutList, function (e) { return e.Value == "EX" || e.Value == "VG" || e.Value == "3EX" }), function (itm) {
            $('#searchcut li[onclick="SetActive(\'CUT\',\'' + itm.Value + '\')"]').addClass('active');
            itm.isActive = true;
        });
        _.each(_.filter(PolishList, function (e) { return e.Value == "EX" || e.Value == "VG" }), function (itm) {
            $('#searchpolish li[onclick="SetActive(\'POLISH\',\'' + itm.Value + '\')"]').addClass('active');
            itm.isActive = true;
        });
        _.each(_.filter(SymList, function (e) { return e.Value == "EX" || e.Value == "VG" }), function (itm) {
            $('#searchsymm li[onclick="SetActive(\'SYMM\',\'' + itm.Value + '\')"]').addClass('active');
            itm.isActive = true;
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
                SubPointerList = _.filter(ParameterList, function (e) { return e.Type == 'Sub Pointer' });
                _(CaratList).each(function (carat, i) {
                    $('#searchcaratgen').append('<li onclick="SetActive(\'CARAT\',\'' + carat.Value + '\')">' + carat.Value + '</li>');
                });



                ParameterList.push({
                    Id: 0, Value: "ALL", SORT_NO: 1, Type: "Shape", isActive: false, Col_Id: 1, Icon_Url: ""
                })
                ParameterList.push({
                    Id: 50, Value: "OTHERS", SORT_NO: 1, Type: "Shape", isActive: false, Col_Id: 1, Icon_Url: "https://sunrisediamonds.com.hk/Images/Shape/ROUND.svg__https://sunrisediamonds.com.hk/Images/Shape/ROUND_Trans.png"
                })

                $('#searchshape').html("");
                $('#searchshape').append('<li style="margin-left: -21px;" class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'SHAPE\',\'' + 'ALL' + '\')" class="common-ico"><div class="icon-image one"><span class="first-ico">ALL</span></div></a></li>');
                ShapeList = _.filter(ParameterList, function (e) { return e.Type == 'Shape' && e.Icon_Url != null });
                _(ShapeList).each(function (shape, i) {
                    if (shape.Value != 'ALL') {
                        var iconurl = shape.Icon_Url.split("__");
                        $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'SHAPE\',\'' + shape.Value + '\')" class="common-ico"><div class="icon-image one"><img src="' + iconurl[0] + '" class="first-ico"><img src="' + iconurl[1] + '" class="second-ico"></div><span>' + shape.Value + '</span></a></li>');
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
                TblInclList = _.filter(ParameterList, function (e) { return e.Type == 'Table White' });
                _(TblInclList).each(function (tblincl, i) {
                    $('#searchtableincl').append('<li onclick="SetActive(\'TABLE_INCL\',\'' + tblincl.Value + '\')">' + tblincl.Value + '</li>');
                });

                $('#searchtablenatts').html("");
                TblNattsList = _.filter(ParameterList, function (e) { return e.Type == 'Table Black' });
                _(TblNattsList).each(function (tblnatts, i) {
                    $('#searchtablenatts').append('<li onclick="SetActive(\'TABLE_NATTS\',\'' + tblnatts.Value + '\')">' + tblnatts.Value + '</li>');
                });

                $('#searchcrownincl').html("");
                CrwnInclList = _.filter(ParameterList, function (e) { return e.Type == 'Crown White' });
                _(CrwnInclList).each(function (crwnincl, i) {
                    $('#searchcrownincl').append('<li onclick="SetActive(\'CROWN_INCL\',\'' + crwnincl.Value + '\')">' + crwnincl.Value + '</li>');
                });

                $('#searchcrownnatts').html("");
                CrwnNattsList = _.filter(ParameterList, function (e) { return e.Type == 'Crown Black' });
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

                $('#searchlocation').html("");
                LocationList = _.filter(ParameterList, function (e) { return e.Type == 'Location' });
                _(LocationList).each(function (location, i) {
                    $('#searchlocation').append('<li onclick="SetActive(\'LOCATION\',\'' + location.Value + '\')">' + location.Value + '</li>');
                });



                KeyToSymbolList = _.filter(ParameterList, function (e) { return e.Type == 'Key to Symbol' });
                RCommentList = _.filter(ParameterList, function (e) { return e.Type == 'Comment' });

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
    //obj.OrderBy = "SupplierName asc";
    $.ajax({
        url: "/User/Get_Supplier_ForSearchStock",
        //url: "/User/Get_SupplierMaster",
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
    else if (flag == "LOCATION") {
        if (_.find(LocationList, function (num) { return num.isActive == true && num.Value == value; })) {
            _.findWhere(LocationList, { Value: value }).isActive = false;
        } else {
            _.findWhere(LocationList, { Value: value }).isActive = true;
        }
    }
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
    else if (flag == "RCommentBlank") {
        if (RCommentBlank) {
            RCommentBlank = false;
            $("#RCommentBlank").removeClass("active");
        } else {
            RCommentBlank = true;
            $("#RCommentBlank").addClass("active");
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
    else if (flag == "GirdlePerBlank") {
        if (GirdlePerBlank) {
            GirdlePerBlank = false;
            $("#GirdlePerBlank").removeClass("active");
        } else {
            GirdlePerBlank = true;
            $("#GirdlePerBlank").addClass("active");
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
    else if (flag == "StarLengthBlank") {
        if (StarLengthBlank) {
            StarLengthBlank = false;
            $("#StarLengthBlank").removeClass("active");
        } else {
            StarLengthBlank = true;
            $("#StarLengthBlank").addClass("active");
        }
    }
    else if (flag == "LowerHalfBlank") {
        if (LowerHalfBlank) {
            LowerHalfBlank = false;
            $("#LowerHalfBlank").removeClass("active");
        } else {
            LowerHalfBlank = true;
            $("#LowerHalfBlank").addClass("active");
        }
    }
}
function Reset() {
    GetTransId();
    $("#txtRefNo").val("");
    $("#ddlUserId").val("");
    $("#PricingMethod_1").val("");
    $("#PricingSign_1").val("Plus");
    $("#txtDisc_1_1").val("");
    _.map(ShapeList, function (data) { return data.isActive = false; });
    _.map(ColorList, function (data) { return data.isActive = false; });
    _.map(ClarityList, function (data) { return data.isActive = false; });
    _.map(CutList, function (data) { return data.isActive = false; });
    _.map(PolishList, function (data) { return data.isActive = false; });
    _.map(SymList, function (data) { return data.isActive = false; });
    _.map(FlouList, function (data) { return data.isActive = false; });
    _.map(BGMList, function (data) { return data.isActive = false; });

    _.map(LabList, function (data) { return data.isActive = false; });
    _.map(TblInclList, function (data) { return data.isActive = false; });
    _.map(TblNattsList, function (data) { return data.isActive = false; });
    _.map(CrwnInclList, function (data) { return data.isActive = false; });
    _.map(CrwnNattsList, function (data) { return data.isActive = false; });
    _.map(CaratList, function (data) { return data.isActive = false; });
    _.map(LocationList, function (data) { return data.isActive = false; });

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
    $('#searchshape').append('<li style="margin-left: -21px;" class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'SHAPE\',\'' + 'ALL' + '\')" class="common-ico"><div class="icon-image one"><span class="first-ico">ALL</span></div></a></li>');
    ShapeList = _.filter(ParameterList, function (e) { return e.Type == 'Shape' && e.Icon_Url != null });
    _(ShapeList).each(function (shape, i) {
        if (shape.Value != 'ALL') {
            var iconurl = shape.Icon_Url.split("__");
            $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'SHAPE\',\'' + shape.Value + '\')" class="common-ico"><div class="icon-image one"><img src="' + iconurl[0] + '" class="first-ico"><img src="' + iconurl[1] + '" class="second-ico"></div><span>' + shape.Value + '</span></a></li>');
        }
    });


    //$('#searchshape').html("");
    //_(ShapeList).each(function (shape, i) {
    //    $('#searchshape').append('<li class="wow zoomIn animated" data-wow-delay="0.8s"><a href="javascript:void(0);" onclick="SetActive(\'Shape\',\'' + shape.Value + '\')" class="common-ico"><div class="icon-image one"><img src="' + shape.UrlValue + '" class="first-ico"><img src="' + shape.UrlValueHov + '" class="second-ico"></div><span>' + shape.Value + '</span></a></li>');
    //});
    $('#searchlocation').html("");
    _(LocationList).each(function (location, i) {
        $('#searchlocation').append('<li onclick="SetActive(\'LOCATION\',\'' + location.Value + '\')">' + location.Value + '</li>');
    });
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
    $('#FromGirdlePer').val("");
    $('#ToGirdlePer').val("");
    $('#FromStarLength').val("");
    $('#ToStarLength').val("");
    $('#FromLowerHalf').val("");
    $('#ToLowerHalf').val("");

    resetKeytoSymbol();
    resetRComment();


    KTSBlank = false;
    $("#KTSBlank").removeClass("active");
    RCommentBlank = false;
    $("#RCommentBlank").removeClass("active");
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
    $("#sym-sec4 .carat-dropdown-main").hide();
    $("#sym-sec0 .carat-dropdown-main").hide();
    C1 = 0, KTS = 0, C2 = 0, C3 = 0, RC = 0;
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
function resetRComment() {
    CheckRCommentList = [];
    UnCheckRCommentList = [];
    $('#RComment_spanselected').html('' + CheckRCommentList.length + ' - Selected');
    $('#RComment_spanunselected').html('' + UnCheckRCommentList.length + ' - Deselected');
    $('#searchRComment input[type="radio"]').prop('checked', false);
    RC = 1;
    RCommentShow();
}
function Key_to_symbolShow() {
    setTimeout(function () {
        if (KTS == 0) {
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").hide();
            $("#sym-sec4 .carat-dropdown-main").hide();
            $("#sym-sec0 .carat-dropdown-main").show();
            KTS = 1;
            RC = 0, C1 = 0, C2 = 0, C3 = 0;
        }
        else {
            $("#sym-sec0 .carat-dropdown-main").hide();
            RC = 0, C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function RCommentShow() {
    setTimeout(function () {

        if (RC == 0) {
            $("#sym-sec1 .carat-dropdown-main").hide();
            $("#sym-sec2 .carat-dropdown-main").hide();
            $("#sym-sec3 .carat-dropdown-main").hide();
            $("#sym-sec0 .carat-dropdown-main").hide();
            $("#sym-sec4 .carat-dropdown-main").show();
            RC = 1;
            KTS = 0, C1 = 0, C2 = 0, C3 = 0;
        }
        else {
            $("#sym-sec4 .carat-dropdown-main").hide();
            RC = 0, C1 = 0, KTS = 0, C2 = 0, C3 = 0;
        }
    }, 2);
}
function BindKeyToSymbolList() {
    if (KeyToSymbolList.length > 0) {
        $.each(KeyToSymbolList, function (i, itm) {
            $('#searchkeytosymbol').append('<div class="col-12 pl-0 pr-0 ng-scope">'
                + '<ul class="row m-0">'
                + '<li class="carat-dropdown-chkbox">'
                + '<div class="main-cust-check">'
                + '<label class="cust-rdi-bx mn-check">'
                + '<input type="radio" class="checkradio" id="CHK_KTS_Radio_' + itm.Value.replaceAll(" ", "_") + '" name="CHK_KTS_Radio_' + itm.Value.replaceAll(" ", "_") + '" onclick="GetCheck_KTS_List(\'' + itm.Value + '\');">'
                + '<span class="cust-rdi-check">'
                + '<i class="fa fa-check"></i>'
                + '</span>'
                + '</label>'
                + '<label class="cust-rdi-bx mn-time">'
                + '<input type="radio" id="UNCHK_KTS_Radio_' + itm.Value.replaceAll(" ", "_") + '" class="checkradio" name="UNCHK_KTS_Radio_' + itm.Value.replaceAll(" ", "_") + '" onclick="GetUnCheck_KTS_List(\'' + itm.Value + '\');">'
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
function BindRCommentList() {
    if (RCommentList.length > 0) {
        $.each(RCommentList, function (i, itm) {
            $('#searchRComment').append('<div class="col-12 pl-0 pr-0 ng-scope">'
                + '<ul class="row m-0">'
                + '<li class="carat-dropdown-chkbox">'
                + '<div class="main-cust-check">'
                + '<label class="cust-rdi-bx mn-check">'
                + '<input type="radio" class="checkradio" id="CHK_RC_Radio_' + itm.Value.replaceAll(" ", "_") + '" name="CHK_RC_Radio_' + itm.Value.replaceAll(" ", "_") + '" onclick="GetCheck_RC_List(\'' + itm.Value + '\');">'
                + '<span class="cust-rdi-check">'
                + '<i class="fa fa-check"></i>'
                + '</span>'
                + '</label>'
                + '<label class="cust-rdi-bx mn-time">'
                + '<input type="radio" id="UNCHK_RC_Radio_' + itm.Value.replaceAll(" ", "_") + '" class="checkradio" name="UNCHK_RC_Radio_' + itm.Value.replaceAll(" ", "_") + '" onclick="GetUnCheck_RC_List(\'' + itm.Value + '\');">'
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
        $('#searchRComment').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');
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
        KTS = 1;
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
        KTS = 1;
        return;
    }, 2);
}
function GetCheck_RC_List(item) {
    var SList = _.reject(UnCheckRCommentList, function (e) { return e.Symbol == item });
    UnCheckRCommentList = SList;

    var res = _.filter(CheckRCommentList, function (e) { return (e.Symbol == item) });
    if (res.length == 0) {
        CheckRCommentList.push({
            "NewID": CheckRCommentList.length + 1,
            "Symbol": item,
        });
        $('#RComment_spanselected').html('' + CheckRCommentList.length + ' - Selected');
        $('#RComment_spanunselected').html('' + UnCheckRCommentList.length + ' - Deselected');
    }
    setTimeout(function () {
        $("#sym-sec4 .carat-dropdown-main").show();
        RC = 1;
        return;
    }, 2);
}
function GetUnCheck_RC_List(item) {
    var SList = _.reject(CheckRCommentList, function (e) { return e.Symbol == item });
    CheckRCommentList = SList

    var res = _.filter(UnCheckRCommentList, function (e) { return (e.Symbol == item) });
    if (res.length == 0) {
        UnCheckRCommentList.push({
            "NewID": UnCheckRCommentList.length + 1,
            "Symbol": item,
        });
        $('#RComment_spanselected').html('' + CheckRCommentList.length + ' - Selected');
        $('#RComment_spanunselected').html('' + UnCheckRCommentList.length + ' - Deselected');
    }
    setTimeout(function () {
        $("#sym-sec4 .carat-dropdown-main").show();
        RC = 1;
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
function checkValue(textbox, point) {
    const value = textbox.value.trim();
    const numericValue = parseFloat(value).toFixed(point);
    if (isNaN(numericValue)) {
        textbox.value = '';
    }
    else {
        textbox.value = numericValue;
    }
}
var LeaveTextBox = function (ele, fromid, toid, point) {
    var from = $("#" + fromid).val() == "" ? "0.00" : $("#" + fromid).val() == undefined ? "0.00" : parseFloat($("#" + fromid).val()).toFixed(point);
    var to = $("#" + toid).val() == "" ? "0.00" : $("#" + toid).val() == undefined ? "0.00" : parseFloat($("#" + toid).val()).toFixed(point);

    $("#" + fromid).val(isFloat(from) == true ? from : 0);
    $("#" + toid).val(isFloat(to) == true ? to : 0);

    var fromvalue = parseFloat($("#" + fromid).val()).toFixed(point) == "" ? 0 : parseFloat($("#" + fromid).val()).toFixed(point);
    var tovalue = parseFloat($("#" + toid).val()).toFixed(point) == "" ? 0 : parseFloat($("#" + toid).val()).toFixed(point);

    if (ele == "FROM") {
        if (parseFloat(parseFloat(fromvalue).toFixed(point)) > parseFloat(parseFloat(tovalue).toFixed(point))) {
            $("#" + toid).val(fromvalue);
            if (fromvalue == 0) {
                $("#" + fromid).val("");
                $("#" + toid).val("");
            }
        }
    }
    else if (ele == "TO") {
        if (parseFloat(parseFloat(tovalue).toFixed(point)) < parseFloat(parseFloat(fromvalue).toFixed(point))) {
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

    var f = parseFloat(parseFloat(($("#" + fromid).val() == "" ? "0" : $("#" + fromid).val())).toFixed(point));
    var t = parseFloat(parseFloat(($("#" + toid).val() == "" ? "0" : $("#" + toid).val())).toFixed(point));
    var cal_fromval = "";
    var cal_toval = "";

    if (fromid == "txtfromcarat" && toid == "txttocarat" && $("#" + fromid).val() != "" && $("#" + toid).val() != "") {
        var cal_fromval = 0.01;
        var cal_toval = 99.99;
    }
    else if (fromid == "FromDiscount" && toid == "ToDiscount" && $("#" + fromid).val() != "" && $("#" + toid).val() != "") {
        var cal_fromval = -99.99;
        var cal_toval = 99.99;
    }
    else if (fromid == "FromTotalAmt" && toid == "ToTotalAmt" && $("#" + fromid).val() != "" && $("#" + toid).val() != "") {
        var cal_fromval = 1;
        var cal_toval = 999999;
    }
    else if (((fromid == "FromLength" && toid == "ToLength") || (fromid == "FromWidth" && toid == "ToWidth") || (fromid == "FromDepth" && toid == "ToDepth")
        || (fromid == "FromDepthPer" && toid == "ToDepthPer") || (fromid == "FromGirdlePer" && toid == "ToGirdlePer")
        || (fromid == "FromCrAng" && toid == "ToCrAng") || (fromid == "FromCrHt" && toid == "ToCrHt") || (fromid == "FromPavAng" && toid == "ToPavAng")
        || (fromid == "FromPavHt" && toid == "ToPavHt") || (fromid == "FromStarLength" && toid == "ToStarLength") || (fromid == "FromLowerHalf" && toid == "ToLowerHalf"))
        && $("#" + fromid).val() != "" && $("#" + toid).val() != "") {
        var cal_fromval = 0.01;
        var cal_toval = 99.99;
    }
    else if (fromid == "FromTablePer" && toid == "ToTablePer" && $("#" + fromid).val() != "" && $("#" + toid).val() != "") {
        var cal_fromval = 1;
        var cal_toval = 99;
    }

    if (cal_fromval != "" && cal_toval != "") {
        if (f >= cal_fromval && f <= cal_toval) {
            if (t >= cal_fromval && t <= cal_toval) {
            } else {
                $("#" + toid).val(f);
                //toastr.error("You can enter only " + cal_fromval + " to " + cal_toval);
            }
        } else {
            $("#" + fromid).val("");
            $("#" + toid).val("");
            if (fromid == "FromTablePer" && toid == "ToTablePer") {
                toastr.error("You can enter only " + cal_fromval + " to " + cal_toval);
            }
            else {
                toastr.error("You can enter only " + formatNumber(cal_fromval) + " to " + formatNumber(cal_toval));
            }
        }
    }
}
function isNumberKey(evt, sign = "") {
    if (sign == "-") {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        // Allow numbers (0-9)
        if ((charCode < 48 || charCode > 57) && charCode !== 46 && charCode !== 45) {
            return false;
        }
        return true;
    }
    else {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        // Allow numbers (0-9)
        if ((charCode < 48 || charCode > 57) && charCode !== 46) {
            return false;
        }
        return true;
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
    var lst = _.filter(CaratList, function (e) { return e.isActive == true });
    if (($('#txtfromcarat').val() != "" && parseFloat($('#txtfromcarat').val()) > 0) && ($('#txttocarat').val() != "" && parseFloat($('#txttocarat').val()) > 0)) {
        NewSizeGroup();
    }
    if (SizeGroupList.length != 0) {
        PointerSizeLst = _.pluck(SizeGroupList, 'Size').join(",");
    }
    if (lst.length != 0) {
        PointerSizeLst = _.pluck(_.filter(CaratList, function (e) { return e.isActive == true }), 'Value').join(",");
    }

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

    obj.PgNo = 1;
    obj.PgSize = 10;
    obj.OrderBy = "";
    obj.FromDate = $("#txtFromDate").val();
    obj.ToDate = $("#txtToDate").val();
    if ($('#ddlSupplierId').val() != undefined) {
        obj.iVendor = $('#ddlSupplierId').val().join(",");
    }
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


function AddToCart() {
    if (gridOptions.api != undefined) {
        var selectedRows = gridOptions.api.getSelectedRows();
        var list = '';
        var i = 0, tot = selectedRows.length;
        for (; i < tot; i++) {
            list += selectedRows[i].SupplierId + "_" + selectedRows[i].Ref_No + "_" + selectedRows[i].Supplier_Stone_Id + ',';
        }
        list = (list != '' ? list.substr(0, (list.length - 1)) : '');

        if (list != "") {
            loaderShow();
            var obj = {};
            obj.SupplierId_RefNo_SupplierRefNo = list;

            $.ajax({
                url: '/User/Add_MyCart',
                type: "POST",
                data: { req: obj },
                success: function (data) {
                    loaderHide();
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    if (data.Status == "1") {
                        toastr.success(data.Message);
                    }
                    else {
                        toastr.error(data.Message);
                    }
                },
                error: function (xhr, textStatus, errorThrown) {
                    loaderHide();
                }
            });
        }
        else {
            toastr.warning("Please Select atlease One Stone");
            return;
        }
    }
}


function SendMail() {
    debugger
    var isValid = $('#frmSendMail').valid();
    if ($('#txtemail').val() == "") {
        return false;
    }
    debugger
    if ($('#customRadiomail').prop('checked')) {
        debugger
        var old_PgNo = obj.PgNo;
        var old_PgSize = obj.PgSize;

        obj.PgNo = "";
        obj.PgSize = "";
        obj.ToAddress = $('#txtemail').val();
        obj.Comments = $('#txtNotes').val();

        loaderShow();

        $.ajax({
            url: "/User/Email_SearchStock",
            type: "POST",
            data: { req: obj },
            success: function (data, textStatus, jqXHR) {
                debugger
                if (data.Status == "0") {
                    if (data.Message.indexOf('Something Went wrong') > -1) {
                        MoveToErrorPage(0);
                    }
                    toastr.error(data.Message);
                } else {
                    toastr.success(data.Message);
                    $('#EmailModal').modal('hide');
                }
                loaderHide();
                obj.PgNo = old_PgNo;
                obj.PgSize = old_PgSize;
            },
            error: function (jqXHR, textStatus, errorThrown) {
                loaderHide();
            }
        });
    } else {
        debugger
        loaderShow();

        var count = gridOptions.api.getSelectedRows().length;
        var stoneno = _.pluck(gridOptions.api.getSelectedRows(), 'Ref_No').join(",");

        if (count > 0) {
            debugger
            var old_RefNo = obj.RefNo;
            var old_PgNo = obj.PgNo;
            var old_PgSize = obj.PgSize;

            obj.PgNo = "";
            obj.PgSize = "";
            obj.ToAddress = $('#txtemail').val();
            obj.Comments = $('#txtNotes').val();
            obj.RefNo = stoneno;

            $.ajax({
                url: "/User/Email_SearchStock",
                type: "POST",
                data: { req: obj },
                success: function (data, textStatus, jqXHR) {
                    debugger
                    if (data.Status == "0") {
                        if (data.Message.indexOf('Something Went wrong') > -1) {
                            MoveToErrorPage(0);
                        }
                        toastr.error(data.Message);
                    } else {
                        toastr.success(data.Message);
                        $('#EmailModal').modal('hide');
                    }
                    loaderHide();
                    obj.RefNo = old_RefNo;
                    obj.PgNo = old_PgNo;
                    obj.PgSize = old_PgSize;
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    loaderHide();
                }
            });
        } else {
            toastr.warning("No stones selected to send email");
            loaderHide();
        }
    }
}
function SaveSearch() {
    var isValid = $('#frmSaveSearch').valid();
    if ($('#txtSearchName').val() == "") {
        return false;
    }
    loaderShow();
    debugger
    var obj = ObjectCreate('1', '200', '', 'Filter');
    obj.SearchName = $('#txtSearchName').val();
    obj.UserId = $('#hdn_UserId').val();
    obj.SearchID = $('#hdnSavedSearchId').val();

    $.ajax({
        url: "/User/AddUpdate_Save_Search",
        type: "POST",
        async: false,
        data: { req: obj },
        success: function (data, textStatus, jqXHR) {
            loaderHide();
            debugger
            if (data.Status == "0") {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                toastr.error(data.Message);
            } else {
                toastr.success(data.Message);
                $('#SaveSearchModal').modal('hide');
            }
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
}

function SetSavedSearchParameter() {
    if ($('#hdnType').val() == 'SaveSearch') {
        loaderShow();
        $.ajax({
            url: "/User/SavedSearchDataSessionGet",
            async: false,
            type: "POST",
            data: null,
            success: function (data, textStatus, jqXHR) {
                $('#hdnSavedSearchId').val(data.SearchID);
                $('#txtSearchName').val(data.SearchName);

                if ($('#ddlSupplierId').val() != undefined) {
                    if (data.SupplierId != null) {
                        if (data.SupplierId != "") {
                            var selectedOptions = data.SupplierId.split(",");
                            for (var i in selectedOptions) {
                                var optionVal = selectedOptions[i];
                                $("#ddlSupplierId").find("option[value=" + optionVal + "]").prop("selected", "selected");
                            }
                            $("#ddlSupplierId").multiselect('refresh');
                        }
                    }
                }
                var shape = data.Shape;
                if (shape != null) {
                    shape = shape.split(',');
                    $(shape).each(function (i, res) {
                        if (_.find(ShapeList, function (num) { return num.Value == res; })) {
                            _.findWhere(ShapeList, { Value: res }).isActive = true;
                            $('#searchshape li a[onclick="SetActive(\'SHAPE\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                if (data.Pointer != null) {
                    var pointer = data.Pointer.split(',');
                    _.each(pointer, function (item) {
                        _.each(_.filter(CaratList, function (e) { return e.value == item }), function (itm) {
                            itm.isActive = true;
                        });
                    });
                    var lst = _.filter(CaratList, function (e) { return e.isActive == true })
                    if (lst.length == 0) {
                        $(pointer).each(function (i, res) {
                            var res1 = res.split('-')
                            var NewID = SizeGroupList.length + 1;
                            SizeGroupList.push({
                                "NewID": NewID,
                                "FromCarat": res1[0],
                                "ToCarat": res1[1],
                                "Size": res,
                            });
                            //<li class="carat-li-top">1.00-1.00<i class="fa fa-plus-circle" aria-hidden="true"></i></li>
                            $('#searchcaratspecific').append('<li id="' + NewID + '" class="carat-li-top">' + res + '<i class="fa fa-times-circle" aria-hidden="true" onclick="NewSizeGroupRemove(' + NewID + ');"></i></li>');
                        });
                        $('a[href="#carat1"]').click();
                    }
                    else {

                        $(pointer).each(function (i, res) {
                            if (_.find(CaratList, function (num) { return num.Value == res; })) {
                                $('#searchcaratgen li[onclick="SetActive(\'CARAT\',\'' + res + '\')"]').addClass('active');
                            }
                        });
                        $('a[href="#carat2"]').click()
                    }
                }
                if (data.ColorType == "Regular") {
                    Color_Hide_Show('1');
                    var color = data.Color;
                    if (color != null) {
                        color = color.split(',');
                        $(color).each(function (i, res) {

                            if (_.find(ColorList, function (num) { return num.Value == res; })) {
                                _.findWhere(ColorList, { Value: res }).isActive = true;
                                $('#searchcolor li[onclick="SetActive(\'COLOR\',\'' + res + '\')"]').addClass('active');
                            }
                        });
                    }
                }
                else if (data.ColorType == "Fancy") {
                    Color_Hide_Show('2');
                    $(".carat-dropdown-main").hide();
                    C1 = 0, C2 = 0, C3 = 0;
                    if (data.INTENSITY != null) {
                        var i = data.INTENSITY;
                        var _INTENSITY = i.split(',');
                        if (_INTENSITY.length == INTENSITY.length - 1) {
                            $("#CHK_I_0").prop('checked', true);
                            GetCheck_INTENSITY_List('ALL SELECTED', 0);
                        }
                        else {
                            for (var _j = 0; _j <= _INTENSITY.length - 1; _j++) {
                                for (var j = 0; j <= INTENSITY.length - 1; j++) {
                                    if (_INTENSITY[_j] == INTENSITY[j]) {
                                        $("#CHK_I_" + j).prop('checked', true);
                                        GetCheck_INTENSITY_List(INTENSITY[j], j);
                                    }
                                }
                            }
                        }
                    }
                    if (data.OVERTONE != null) {
                        var o = data.OVERTONE;
                        var _OVERTONE = o.split(',');
                        if (_OVERTONE.length == OVERTONE.length - 1) {
                            $("#CHK_O_0").prop('checked', true);
                            GetCheck_OVERTONE_List('ALL SELECTED', 0);
                        }
                        else {
                            for (var _j = 0; _j <= _OVERTONE.length - 1; _j++) {
                                for (var j = 0; j <= OVERTONE.length - 1; j++) {
                                    if (_OVERTONE[_j] == OVERTONE[j]) {
                                        $("#CHK_O_" + j).prop('checked', true);
                                        GetCheck_OVERTONE_List(OVERTONE[j], j);
                                    }
                                }
                            }
                        }
                    }
                    if (data.FANCY_COLOR != null) {
                        var fc = data.FANCY_COLOR;
                        var _FANCY_COLOR = fc.split(',');
                        if (_FANCY_COLOR.length == FANCY_COLOR.length - 1) {
                            $("#CHK_F_0").prop('checked', true);
                            GetCheck_FANCY_COLOR_List('ALL SELECTED', 0);
                        }
                        else {
                            for (var _j = 0; _j <= _FANCY_COLOR.length - 1; _j++) {
                                for (var j = 0; j <= FANCY_COLOR.length - 1; j++) {
                                    if (_FANCY_COLOR[_j] == FANCY_COLOR[j]) {
                                        $("#CHK_F_" + j).prop('checked', true);
                                        GetCheck_FANCY_COLOR_List(FANCY_COLOR[j], j);
                                    }
                                }
                            }
                        }
                    }
                }
                var clarity = data.Clarity;
                if (clarity != null) {
                    clarity = clarity.split(',');
                    $(clarity).each(function (i, res) {

                        if (_.find(ClarityList, function (num) { return num.Value == res; })) {
                            _.findWhere(ClarityList, { Value: res }).isActive = true;
                            $('#searchclarity li[onclick="SetActive(\'CLARITY\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var cut = data.Cut;
                var polish = data.Polish;
                var symm = data.Symm;

                if (cut == "EX" && polish == "EX" && symm == "EX") {
                    $('#li3ex').addClass('active');
                }
                else {
                    $('#li3ex').removeClass('active');
                }
                if (cut == "EX,VG" && polish == "EX,VG" && symm == "EX,VG") {
                    $('#li3vg').addClass('active');
                }
                else {
                    $('#li3vg').removeClass('active');
                }
                if (cut != null) {
                    cut = cut.split(',');
                    $(cut).each(function (i, res) {

                        if (_.find(CutList, function (num) { return num.Value == res; })) {
                            _.findWhere(CutList, { Value: res }).isActive = true;
                            $('#searchcut li[onclick="SetActive(\'CUT\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                if (polish != null) {
                    polish = polish.split(',');
                    $(polish).each(function (i, res) {

                        if (_.find(PolishList, function (num) { return num.Value == res; })) {
                            _.findWhere(PolishList, { Value: res }).isActive = true;
                            $('#searchpolish li[onclick="SetActive(\'POLISH\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                if (symm != null) {
                    symm = symm.split(',');
                    $(symm).each(function (i, res) {

                        if (_.find(SymList, function (num) { return num.Value == res; })) {
                            _.findWhere(SymList, { Value: res }).isActive = true;
                            $('#searchsymm li[onclick="SetActive(\'SYMM\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var fls = data.Fls;
                if (fls != null) {
                    fls = fls.split(',');
                    $(fls).each(function (i, res) {

                        if (_.find(FlouList, function (num) { return num.Value == res; })) {
                            _.findWhere(FlouList, { Value: res }).isActive = true;
                            $('#searchfls li[onclick="SetActive(\'FLS\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var bgm = data.BGM;
                if (bgm != null) {
                    bgm = bgm.split(',');
                    $(bgm).each(function (i, res) {

                        if (_.find(BGMList, function (num) { return num.Value == res; })) {
                            _.findWhere(BGMList, { Value: res }).isActive = true;
                            $('#searchbgm li[onclick="SetActive(\'BGM\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var lab = data.Lab;
                if (lab != null) {
                    lab = lab.split(',');
                    $(lab).each(function (i, res) {

                        if (_.find(LabList, function (num) { return num.Value == res; })) {
                            _.findWhere(LabList, { Value: res }).isActive = true;
                            $('#searchlab li[onclick="SetActive(\'LAB\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var crownnatts = data.CrownBlack;
                if (crownnatts != null) {
                    crownnatts = crownnatts.split(',');
                    $(crownnatts).each(function (i, res) {

                        if (_.find(CrwnNattsList, function (num) { return num.Value == res; })) {
                            _.findWhere(CrwnNattsList, { Value: res }).isActive = true;
                            $('#searchcrownnatts li[onclick="SetActive(\'CROWN_NATTS\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var natts = data.TableBlack;
                if (natts != null) {
                    natts = natts.split(',');
                    $(natts).each(function (i, res) {

                        if (_.find(TblNattsList, function (num) { return num.Value == res; })) {
                            _.findWhere(TblNattsList, { Value: res }).isActive = true;
                            $('#searchtablenatts li[onclick="SetActive(\'TABLE_NATTS\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var inclusion = data.TableWhite;
                if (inclusion != null) {
                    inclusion = inclusion.split(',');
                    $(inclusion).each(function (i, res) {

                        if (_.find(TblInclList, function (num) { return num.Value == res; })) {
                            _.findWhere(TblInclList, { Value: res }).isActive = true;
                            $('#searchtableincl li[onclick="SetActive(\'TABLE_INCL\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var crowninclusion = data.CrownWhite;
                if (crowninclusion != null) {
                    crowninclusion = crowninclusion.split(',');
                    $(crowninclusion).each(function (i, res) {

                        if (_.find(CrwnInclList, function (num) { return num.Value == res; })) {
                            _.findWhere(CrwnInclList, { Value: res }).isActive = true;
                            $('#searchcrownincl li[onclick="SetActive(\'CROWN_INCL\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }
                var _tableopenlist = data.TableOpen;
                if (_tableopenlist != null) {
                    _tableopenlist = _tableopenlist.split(',');
                    $(_tableopenlist).each(function (i, res) {
                        if (_.find(TableOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(TableOpenList, { Value: res }).isActive = true;
                            $('#searchtableopen li[onclick="SetActive(\'TABLEOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _crownopenlist = data.CrownOpen;
                if (_crownopenlist != null) {
                    _crownopenlist = _crownopenlist.split(',');
                    $(_crownopenlist).each(function (i, res) {
                        if (_.find(CrownOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(CrownOpenList, { Value: res }).isActive = true;
                            $('#searchcrownopen li[onclick="SetActive(\'CROWNOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _pavopenlist = data.PavOpen;
                if (_pavopenlist != null) {
                    _pavopenlist = _pavopenlist.split(',');
                    $(_pavopenlist).each(function (i, res) {
                        if (_.find(PavOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(PavOpenList, { Value: res }).isActive = true;
                            $('#searchpavopen li[onclick="SetActive(\'PAVILIONOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                var _girdleopenlist = data.GirdleOpen;
                if (_girdleopenlist != null) {
                    _girdleopenlist = _girdleopenlist.split(',');
                    $(_girdleopenlist).each(function (i, res) {
                        if (_.find(GirdleOpenList, function (num) { return num.Value == res; })) {
                            _.findWhere(GirdleOpenList, { Value: res }).isActive = true;
                            $('#searchgirdleopen li[onclick="SetActive(\'GIRDLEOPEN\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }


                if (data.CheckKTS != null) {
                    var KeyToSymbol = data.CheckKTS;
                    KeyToSymbol = KeyToSymbol.split(',');
                    $(KeyToSymbol).each(function (i, res) {
                        CheckKeyToSymbolList.push({
                            "NewID": KeyToSymbolList.length + 1,
                            "Symbol": res,
                        });
                        //$('#searchkeytosymbol input[onclick="GetCheck_KTS_List(\'' + res + '\');"]').prop('checked', true);
                        res = res.replaceAll(" ", "_");
                        $('#searchkeytosymbol #CHK_KTS_Radio_' + res).prop('checked', true);
                    });
                    $('#spanselected').html('' + CheckKeyToSymbolList.length + ' - Selected');
                }
                if (data.UNCheckKTS != null) {
                    var KeyToSymbol = data.UNCheckKTS;
                    KeyToSymbol = KeyToSymbol.split(',');
                    $(KeyToSymbol).each(function (i, res) {
                        UnCheckKeyToSymbolList.push({
                            "NewID": KeyToSymbolList.length + 1,
                            "Symbol": res,
                        });
                        //$('#searchkeytosymbol input[onclick="GetUnCheck_KTS_List(\'' + res + '\');"]').prop('checked', true);
                        res = res.replaceAll(" ", "_");
                        $('#searchkeytosymbol #UNCHK_KTS_Radio_' + res).prop('checked', true);
                    });
                    $('#spanunselected').html('' + UnCheckKeyToSymbolList.length + ' - Selected');
                }
                if (data.KTSBlank == "true") {
                    SetActive("KTSBlank", "");
                }

                if (data.CheckRComment != null) {
                    var RComment = data.CheckRComment;
                    RComment = RComment.split(',');
                    $(RComment).each(function (i, res) {
                        CheckRCommentList.push({
                            "NewID": RCommentList.length + 1,
                            "Symbol": res,
                        });
                        //$('#searchRComment input[onclick="GetCheck_RC_List(\'' + res + '\');"]').prop('checked', true);
                        res = res.replaceAll(" ", "_");
                        $('#searchRComment #CHK_RC_Radio_' + res).prop('checked', true);
                    });
                    $('#RComment_spanselected').html('' + CheckRCommentList.length + ' - Selected');
                }
                if (data.UNCheckRComment != null) {
                    var RComment = data.UNCheckRComment;
                    RComment = RComment.split(',');
                    $(RComment).each(function (i, res) {
                        UnCheckRCommentList.push({
                            "NewID": RCommentList.length + 1,
                            "Symbol": res,
                        });
                        //$('#searchRComment input[onclick="GetUnCheck_RC_List(\'' + res + '\');"]').prop('checked', true);
                        res = res.replaceAll(" ", "_");
                        $('#searchRComment #UNCHK_RC_Radio_' + res).prop('checked', true);
                    });
                    $('#RComment_spanunselected').html('' + UnCheckRCommentList.length + ' - Selected');
                }
                if (data.RCommentBlank == "true") {
                    SetActive("RCommentBlank", "");
                }


                $('#FromDiscount').val(data.FromDisc);
                $('#ToDiscount').val(data.ToDisc);
                $('#FromTotalAmt').val(data.FromTotAmt);
                $('#ToTotalAmt').val(data.ToTotAmt);

                if (data.LengthBlank == "true") {
                    SetActive("LengthBlank", "");
                }
                $('#FromLength').val(data.FromLength);
                $('#ToLength').val(data.ToLength);

                if (data.WidthBlank == "true") {
                    SetActive("WidthBlank", "");
                }
                $('#FromWidth').val(data.FromWidth);
                $('#ToWidth').val(data.ToWidth);

                if (data.DepthBlank == "true") {
                    SetActive("DepthBlank", "");
                }
                $('#FromDepth').val(data.FromDepth);
                $('#ToDepth').val(data.ToDepth);

                if (data.DepthPerBlank == "true") {
                    SetActive("DepthPerBlank", "");
                }
                $('#FromDepthPer').val(data.FromDepthPer);
                $('#ToDepthPer').val(data.ToDepthPer);

                if (data.TablePerBlank == "true") {
                    SetActive("TablePerBlank", "");
                }
                $('#FromTablePer').val(data.FromTablePer);
                $('#ToTablePer').val(data.ToTablePer);

                if (data.GirdlePerBlank == "true") {
                    SetActive("GirdlePerBlank", "");
                }
                $('#FromGirdlePer').val(data.FromGirdlePer);
                $('#ToGirdlePer').val(data.ToGirdlePer);

                if (data.StarLengthBlank == "true") {
                    SetActive("StarLengthBlank", "");
                }
                $('#FromStarLength').val(data.FromStarLength);
                $('#ToStarLength').val(data.ToStarLength);

                if (data.LowerHalfBlank == "true") {
                    SetActive("LowerHalfBlank", "");
                }
                $('#FromLowerHalf').val(data.FromLowerHalf);
                $('#ToLowerHalf').val(data.ToLowerHalf);

                if (data.Img == "YES") {
                    $('#SearchImage').addClass('active');
                }
                if (data.Vdo == "YES") {
                    $('#SearchVideo').addClass('active');
                }
                if (data.Certi == "YES") {
                    $('#SearchCerti').addClass('active');
                }

                if (data.CrAngBlank == "true") {
                    SetActive("CrAngBlank", "");
                }
                $('#FromCrAng').val(data.FromCrAng);
                $('#ToCrAng').val(data.ToCrAng);

                if (data.CrHtBlank == "true") {
                    SetActive("CrHtBlank", "");
                }
                $('#FromCrHt').val(data.FromCrHt);
                $('#ToCrHt').val(data.ToCrHt);

                if (data.PavAngBlank == "true") {
                    SetActive("PavAngBlank", "");
                }
                $('#FromPavAng').val(data.FromPavAng);
                $('#ToPavAng').val(data.ToPavAng);

                if (data.PavHtBlank == "true") {
                    SetActive("PavHtBlank", "");
                }
                $('#FromPavHt').val(data.FromPavHt);
                $('#ToPavHt').val(data.ToPavHt);

                var location = data.Location;
                if (location != null) {
                    location = location.split(',');
                    $(location).each(function (i, res) {

                        if (_.find(LocationList, function (num) { return num.Value == res; })) {
                            _.findWhere(LocationList, { Value: res }).isActive = true;
                            $('#searchlocation li[onclick="SetActive(\'LOCATION\',\'' + res + '\')"]').addClass('active');
                        }
                    });
                }

                Type = data.Type;

                setTimeout(function () {
                    $("#sym-sec0 .carat-dropdown-main").hide();
                    $("#sym-sec1 .carat-dropdown-main").hide();
                    $("#sym-sec2 .carat-dropdown-main").hide();
                    $("#sym-sec3 .carat-dropdown-main").hide();
                    $("#sym-sec4 .carat-dropdown-main").hide();
                }, 2);


                CustomerList();
                //loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                loaderHide();
            }
        });
    }
}
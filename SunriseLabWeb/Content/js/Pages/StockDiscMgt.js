var pgSize = 50;
var showEntryVar = null;
var total_record = null;
var Rowdata = [];
var orderBy = '';
var lbl = '<div style="float: left;font-size: 14px;position: absolute;left: 5%;transform: translateX(-50%);text-transform: capitalize;">'
    + '<button type="button" id="btnAddFilters" onclick="AddFilters();" class="offer-btn"><i class="fa fa-plus" aria-hidden="true"></i>Add Filters</button></div>';
var showEntryHtml = '<div class="show_entry">'
    + '<label>Show <select id="ddlPagesize" onchange="onPageSizeChanged()">'
    + '<option value="50">50</option>'
    + '<option value="100">100</option>'
    + '<option value="500">500</option>'
    + '</select> entries</label></div>';
var gridOptions = {};
var ErrorMsg = [];
var columnDefs = [
    {
        headerName: "", field: "",
        headerCheckboxSelection: true,
        checkboxSelection: true, width: 40,
        suppressSorting: true,
        suppressMenu: true,
        headerCheckboxSelectionFilteredOnly: true,
        headerCellRenderer: selectAllRendererDetail,
        suppressMovable: false
    },
    { headerName: "UserId", field: "UserId", hide: true },
    { headerName: "Sr", field: "iSr", tooltip: function (params) { return (params.value); }, sortable: false, width: 40 },
    { headerName: "Create Date", field: "CreatedDate", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: "Last Login Date", field: "LastLoginDate", tooltip: function (params) { return (params.value); }, width: 90 },
    { headerName: "UserTypeId", field: "UserTypeId", hide: true },
    { headerName: "User Type", field: "UserType", sortable: false, tooltip: function (params) { return (params.value); }, width: 190 },
    { headerName: "Active", field: "IsActive", cellRenderer: 'faIndicator', tooltip: function (params) { if (params.value == true) { return 'Yes'; } else { return 'No'; } }, cellClass: ['muser-fa-font'], width: 55 },
    { headerName: "User Name", field: "UserName", tooltip: function (params) { return (params.value); }, width: 95 },
    { headerName: "Password", field: "Password", hide: true },
    { headerName: "FirstName", field: "FirstName", hide: true },
    { headerName: "LastName", field: "LastName", hide: true },
    { headerName: "Customer Name", field: "FullName", tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Company Name", field: "CompName", tooltip: function (params) { return (params.value); }, width: 180 },
    { headerName: "Fortune Party Code", field: "FortunePartyCode", tooltip: function (params) { return (params.value); }, width: 75 },
    { headerName: "Assist", field: "AssistByName", tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Sub Assist", field: "SubAssistByName", tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Mobile", field: "MobileNo", tooltip: function (params) { return (params.value); }, width: 120 },
    { headerName: "Email Id", field: "EmailId", tooltip: function (params) { return (params.value); }, width: 140 },
    { headerName: "Email Id 2", field: "EmailId_2", tooltip: function (params) { return (params.value); }, width: 140 },
];
var faIndicator = function (params) {
    var element = document.createElement("a");
    element.title = '';
    element.innerHTML = '<i class="fa fa-check" aria-hidden="true"></i>';
    if (params.value) {
        return element;
    }
}
function GetUserList() {
    if ($.trim($("#txtCompanyName").val()) != "") {
        $("#divGrid").show();

        if (gridOptions.api != undefined) {
            gridOptions.api.destroy();
        }
        gridOptions = {
            masterDetail: true,
            detailCellRenderer: 'myDetailCellRenderer',
            detailRowHeight: 70,
            groupDefaultExpanded: 2,
            defaultColDef: {
                enableValue: false,
                enableRowGroup: false,
                enableSorting: false,
                sortable: false,
                resizable: true,
                enablePivot: false,
                filter: true
            },
            components: {
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
            overlayLoadingTemplate: '<span class="ag-overlay-loading-center">NO DATA TO SHOW..</span>',
            suppressRowClickSelection: true,
            columnDefs: columnDefs,
            rowModelType: 'serverSide',
            //onGridReady: onGridReady,
            cacheBlockSize: pgSize, // you can have your custom page size
            paginationPageSize: pgSize, //pagesize
            getContextMenuItems: getContextMenuItems,
            onBodyScroll: onBodyScroll,
            paginationNumberFormatter: function (params) {
                return '[' + params.value.toLocaleString() + ']';
            }
        };

        var gridDiv = document.querySelector('#myGrid');
        new agGrid.Grid(gridDiv, gridOptions);
        gridOptions.api.setServerSideDatasource(datasource1);

        $('#myGrid .ag-header-cell[col-id="0"] .ag-header-select-all').removeClass('ag-hidden');

        setTimeout(function () {
            if ($('#myGrid .ag-paging-panel').length > 0) {
                $("").appendTo('#myGrid .ag-paging-panel');
                $(lbl).appendTo('#myGrid .ag-paging-panel');
                $("").appendTo('#myGrid .ag-paging-panel');
                $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
                $('#ddlPagesize').val(pgSize);
                clearInterval(showEntryVar);
            }
        }, 1000);

        //showEntryVar = setInterval(function () {
        //    if ($('#myGrid .ag-paging-panel').length > 0) {
        //        $("").appendTo('#myGrid .ag-paging-panel');
        //        $(lbl).appendTo('#myGrid .ag-paging-panel');
        //        $("").appendTo('#myGrid .ag-paging-panel');
        //        $(showEntryHtml).appendTo('#myGrid .ag-paging-panel');
        //        $('#ddlPagesize').val(pgSize);
        //        clearInterval(showEntryVar);
        //    }
        //}, 1000);
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
        });
    }
    else {
        $("#divGrid").hide();
    }
}
const datasource1 = {
    getRows(params) {
        var obj = {};
        if (params.request.sortModel.length > 0) {
            obj.OrderBy = params.request.sortModel[0].colId + ' ' + params.request.sortModel[0].sort;
        }
        var PageNo = gridOptions.api.paginationGetCurrentPage() + 1;
        obj.PgNo = PageNo;
        obj.PgSize = pgSize;
        obj.CompanyUserCustomer = $.trim($("#txtCompanyName").val());
        Rowdata = [];

        $.ajax({
            url: "/User/GetUsers",
            async: false,
            type: "POST",
            data: obj,
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
                    //toastr.error(data.Message, { timeOut: 2500 });
                    params.successCallback([], 0);
                    gridOptions.api.showNoRowsOverlay();
                }
                contentHeight();
                setInterval(function () {
                    $(".ag-header-cell-text").addClass("grid_prewrap");
                }, 30);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                params.successCallback([], 0);
                Rowdata = [];
            }
        });

    }
};
function onPageSizeChanged() {
    var value = $('#ddlPagesize').val();
    pgSize = Number(value);
    GetUserList();
}
function onGridReady(params) {
    if (navigator.userAgent.indexOf('Windows') > -1) {
        this.api.sizeColumnsToFit();
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
        //onSelectionChanged();
    });
}
function contentHeight() {
    var winH = $(window).height(),
        tabsmarkerHei = $(".order-title").height(),
        navbarHei = $(".navbar").height(),
        contentHei = winH - navbarHei - tabsmarkerHei - 110;
    contentHei = (contentHei < 100 ? 500 : contentHei);
    $("#myGrid").css("height", contentHei);
}
$(window).resize(function () {
    contentHeight();
});
function Back() {
    $("#h2Title").show();
    $("#h2AddTitle").hide();
    $("#divGrid").show();
    $("#divAddFilters").hide();
    $("#btnBack").hide();
    $(".order-title").removeClass("col-xl-12");
    $("#divSearchFilter").show();
    //$(".import").show();
}
function AddFilters() {
    if (_.filter(gridOptions.api.getSelectedRows()).length == 1) {
        $("#h2Title").hide();
        $("#h2AddTitle").show();
        $("#divSearchFilter").hide();
        $("#divGrid").hide();
        $("#divAddFilters").show();
        $("#spnUsernmList").html('<span style="font-weight:600;">User Name :&nbsp;</span>' + _.pluck(_.filter(gridOptions.api.getSelectedRows()), 'UserName').join(", "));
        //$(".import").hide();
        $(".order-title").addClass("col-xl-12");
        $("#btnBack").show();
        Get_Customer_Stock_Disc();
    }
    else if (_.filter(gridOptions.api.getSelectedRows()).length == 0) {
        toastr.warning("Please select user for Add Stock & Disc Filters");
    }
    else {
        toastr.warning("Please select only 1 user for Add Stock & Disc Filters");
    }
}







var EditCriteria_UniqueId = "";
var SupplierList = [];
var PointerList = [];
var GoodsTypeList = [];
var LocationList = [];
var ShapeList = [];
var CaratList = [];
var ColorList = [];
var ClarityList = [];
var CutList = [];
var PolishList = [];
var SymList = [];
var FlsList = [];
var LabList = [];
var KTSList = [];
var BGMList = [];
var CrownBlackList = [];
var TableBlackList = [];
var CrownWhiteList = [];
var TableWhiteList = [];
var CheckedSupplierValue = "";
var CheckedPointerValue = "";
var CheckedGoodsTypeValue = "";
var CheckedLocationValue = "";
var CheckedShapeValue = "";
var CheckedColorValue = "";
var CheckedClarityValue = "";
var CheckedCutValue = "";
var CheckedPolValue = "";
var CheckedSymValue = "";
var CheckedLabValue = "";
var CheckedCaratValue = "";
var CheckedFLsValue = "";
var CheckedBgmValue = "";
var CheckedCrnBlackValue = "";
var CheckedTblBlackValue = "";
var CheckedCrnWhiteValue = "";
var CheckedTblWhiteValue = "";
var ColumnList = [];
var _pointerlst = [];

var SSN_CARAT = [];
var CheckedCaratValue = '';
var CaratFrom = '';
var CaratTo = '';
var Color = '';
var IsCaratFT = true
var CARAT = false;
var FromSize1 = "";
var ToSize1 = "";
var FromSize2 = "";
var ToSize2 = "";
var FromSize3 = "";
var ToSize3 = "";
var FromSize4 = "";
var ToSize4 = "";
var FromSize5 = "";
var ToSize5 = "";
var FromSize6 = "";
var ToSize6 = "";
var FromSize7 = "";
var ToSize7 = "";
var FromSize8 = "";
var ToSize8 = "";
var FromSize11 = "";
var ToSize11 = "";
var FromSize10 = "";
var ToSize10 = "";
var FromSize11 = "";
var ToSize11 = "";
var FromSize12 = "";
var ToSize12 = "";
var FromSize13 = "";
var ToSize13 = "";
var FromSize14 = "";
var ToSize14 = "";
var FromSize15 = "";
var ToSize15 = "";
var FromSize16 = "";
var ToSize16 = "";
var FromSize17 = "";
var ToSize17 = "";
var FromSize18 = "";
var ToSize18 = "";

var CARAT_Size2 = false;
var CARAT_Size3 = false;
var CARAT_Size4 = false;
var CARAT_Size5 = false;
var CARAT_Size6 = false;
var CARAT_Size7 = false;
var CARAT_Size8 = false;
var CARAT_Size9 = false;
var CARAT_Size10 = false;
var CARAT_Size11 = false;
var CARAT_Size12 = false;
var CARAT_Size13 = false;
var CARAT_Size14 = false;
var CARAT_Size15 = false;
var CARAT_Size16 = false;
var CARAT_Size17 = false;
var CARAT_Size18 = false;
var Carat = "";
var KeyToSymbolList = [];
var CheckKeyToSymbolList = [];
var UnCheckKeyToSymbolList = [];
var Regular = true, Fancy = false;
var Regular_All = false, Fancy_All = false;
var INTENSITY = [], OVERTONE = [], FANCY_COLOR = [];
var KTS = 0, C1 = 0, C2 = 0, C3 = 0;
var Check_Color_1 = [], Check_Color_2 = [], Check_Color_3 = [];
var FC = "";
var Exists_Record = 0;

var today = new Date();
var next1WeekDate = new Date(today.setDate(today.getDate() + 7));
var m_names = new Array("Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec");
var date = new Date(next1WeekDate),
    mnth = ("0" + (date.getMonth() + 1)).slice(-2),
    day = ("0" + date.getDate()).slice(-2);
var L_date = [day, m_names[mnth - 1], date.getFullYear()].join("-");

function SetCurrentDate() {
    var d = new Date();
    var curr_date = d.getDate();
    var curr_month = d.getMonth();
    var curr_year = d.getFullYear();
    var FinalDate = (curr_date + "-" + m_names[curr_month] + "-" + curr_year);
    return FinalDate;
}
function FromTo_Date(type) {
    if (type == "1") {
        $('#txtFromDate').val(SetCurrentDate());
        $('#txtToDate').val(L_date);
        $('#txtFromDate').daterangepicker({
            singleDatePicker: true,
            startDate: moment(),
            showDropdowns: true,
            locale: {
                separator: "-",
                format: 'DD-MMM-YYYY'
            },
            minDate: new Date(),
            minYear: parseInt(moment().format('YYYY'), 10),
            maxYear: parseInt(moment().format('YYYY'), 10) + 10
        }).on('change', function (e) {
            greaterThanDate(e, "txtFromDate", "txtToDate", type);
        });

        $('#txtToDate').daterangepicker({
            singleDatePicker: true,
            startDate: L_date,
            showDropdowns: true,
            locale: {
                separator: "-",
                format: 'DD-MMM-YYYY'
            },
            minDate: new Date($('#txtFromDate').val()),
            minYear: parseInt(moment().format('YYYY'), 10),
            maxYear: parseInt(moment().format('YYYY'), 10) + 10
        }, function (start, end, label) {
            var years = moment().diff(start, 'years');
        }).on('change', function (e) {
            greaterThanDate(e, "txtFromDate", "txtToDate", type);
        });
    }
    //else if (type == "2") {
    //    $('#txtFromDate1').val(SetCurrentDate());
    //    $('#txtToDate1').val(L_date);
    //    $('#txtFromDate1').daterangepicker({
    //        singleDatePicker: true,
    //        startDate: moment(),
    //        showDropdowns: true,
    //        locale: {
    //            separator: "-",
    //            format: 'DD-MMM-YYYY'
    //        },
    //        minDate: new Date(),
    //        minYear: parseInt(moment().format('YYYY'), 10),
    //        maxYear: parseInt(moment().format('YYYY'), 10) + 10
    //    }).on('change', function (e) {
    //        greaterThanDate(e, "txtFromDate1", "txtToDate1", type);
    //    });
    //    $('#txtToDate1').daterangepicker({
    //        singleDatePicker: true,
    //        startDate: L_date,
    //        showDropdowns: true,
    //        locale: {
    //            separator: "-",
    //            format: 'DD-MMM-YYYY'
    //        },
    //        minDate: new Date($('#txtFromDate1').val()),
    //        minYear: parseInt(moment().format('YYYY'), 10),
    //        maxYear: parseInt(moment().format('YYYY'), 10) + 10
    //    }, function (start, end, label) {
    //        var years = moment().diff(start, 'years');
    //    }).on('change', function (e) {
    //        greaterThanDate(e, "txtFromDate1", "txtToDate1", type);
    //    });
    //}
}
function againCalendarCall(type) {
    if (type == "1") {
        $('#txtFromDate').daterangepicker({
            singleDatePicker: true,
            startDate: $('#txtFromDate').val(),
            showDropdowns: true,
            locale: {
                separator: "-",
                format: 'DD-MMM-YYYY'
            },
            minDate: (new Date() <= new Date($('#txtFromDate').val()) ? new Date() : new Date($('#txtFromDate').val())),
            minYear: parseInt(moment().format('YYYY'), 10),
            maxYear: parseInt(moment().format('YYYY'), 10) + 10
            , onSelect: function (e) {
                greaterThanDate(e, "txtFromDate", "txtToDate", type);
            }
        });
        //    .on('change', function (e) {
        //    greaterThanDate(e, "txtFromDate", "txtToDate", type);
        //});
        $('#txtToDate').daterangepicker({
            singleDatePicker: true,
            startDate: $('#txtToDate').val(),
            showDropdowns: true,
            locale: {
                separator: "-",
                format: 'DD-MMM-YYYY'
            },
            minDate: (new Date() <= new Date($('#txtFromDate').val()) ? new Date() : new Date($('#txtFromDate').val())),
            minYear: parseInt(moment().format('YYYY'), 10),
            maxYear: parseInt(moment().format('YYYY'), 10) + 10
            , onSelect: function (e) {
                greaterThanDate(e, "txtFromDate", "txtToDate", type);
            }
        }, function (start, end, label) {
            var years = moment().diff(start, 'years');
        });
        //    .on('change', function (e) {
        //    greaterThanDate(e, "txtFromDate", "txtToDate", type);
        //});
    }
    //else if (type == "2") {
    //    $('#txtFromDate1').daterangepicker({
    //        singleDatePicker: true,
    //        startDate: $('#txtFromDate1').val(),
    //        showDropdowns: true,
    //        locale: {
    //            separator: "-",
    //            format: 'DD-MMM-YYYY'
    //        },
    //        minDate: (new Date() <= new Date($('#txtFromDate1').val()) ? new Date() : new Date($('#txtFromDate1').val())),
    //        minYear: parseInt(moment().format('YYYY'), 10),
    //        maxYear: parseInt(moment().format('YYYY'), 10) + 10
    //        , onSelect: function (e) {
    //            greaterThanDate(e, "txtFromDate1", "txtToDate1", type);
    //        }
    //    });
    //    //    .on('change', function (e) {
    //    //    greaterThanDate(e, "txtFromDate1", "txtToDate1", type);
    //    //});
    //    $('#txtToDate1').daterangepicker({
    //        singleDatePicker: true,
    //        startDate: $('#txtToDate1').val(),
    //        showDropdowns: true,
    //        locale: {
    //            separator: "-",
    //            format: 'DD-MMM-YYYY'
    //        },
    //        minDate: (new Date() <= new Date($('#txtFromDate1').val()) ? new Date() : new Date($('#txtFromDate1').val())),
    //        minYear: parseInt(moment().format('YYYY'), 10),
    //        maxYear: parseInt(moment().format('YYYY'), 10) + 10
    //        , onSelect: function (e) {
    //            greaterThanDate(e, "txtFromDate1", "txtToDate1", type);
    //        }
    //    }, function (start, end, label) {
    //        var years = moment().diff(start, 'years');
    //    });
    //    //    .on('change', function (e) {
    //    //    greaterThanDate(e, "txtFromDate1", "txtToDate1", type);
    //    //});
    //}
}
function greaterThanDate(evt, from, to, type) {
    var fDate = $.trim($('#' + from).val());
    var tDate = $.trim($('#' + to).val());
    if (fDate != "" && tDate != "") {
        if (new Date(tDate) >= new Date(fDate)) {
            againCalendarCall(type);
            return true;
        }
        else {
            evt.currentTarget.value = "";
            toastr.warning("To date must be greater than From date !");
            FromTo_Date(type);
            return false;
        }
    }
    else {
        return true;
    }
}

$(document).ready(function () {
    //$(".apifilter .import").show();
    $("#divSearchFilter").show();

    //openTab('CustomerDisc');
    Reset_API_Filter();
    Get_API_StockFilter();
    BindKeyToSymbolList();

    $("#tblFilters").on('click', '.RemoveCriteria', function () {
        $(this).closest('tr').remove();
        if (parseInt($("#tblFilters #tblBodyFilters").find('tr').length) == 0) {
            $("#lblCustNoFound").show();
            $("#tblFilters").hide();
        }
        else {
            $("#lblCustNoFound").hide();
            $("#tblFilters").show();
        }
        var idd = 1;
        $("#tblFilters #tblBodyFilters tr").each(function () {
            $(this).find("th:eq(0)").html(idd);
            idd += 1;
        });
    });

    Bind_RColor();
    FcolorBind();
    $('#ColorModal').on('show.bs.modal', function (event) {
        color_ddl_close();
    });
    $('#chk_Regular_All').change(function () {
        R_F_All_Only_Checkbox_Clr_Rst("-1");
        Regular_All = $(this).is(':checked');
        SetSearchParameter();
    });
    $('#chk_Fancy_All').change(function () {
        R_F_All_Only_Checkbox_Clr_Rst("-1");
        Fancy_All = $(this).is(':checked');
        SetSearchParameter();
    });

    $('.dis_val').on('paste', function (event) {
        if (event.originalEvent.clipboardData.getData('Text').match(/[^\d]/)) {
            event.preventDefault();
        }
    });

    const numberInputs = document.querySelectorAll('.dis_val');
    numberInputs.forEach(input => {
        input.addEventListener('input', function (event) {
            const inputValue = event.target.value;
            const validNumberRegex = /^-?\d*\.?\d*$/; // Regular expression for decimal and integer numbers

            if (!validNumberRegex.test(inputValue)) {
                // Remove invalid characters from the input value
                event.target.value = inputValue.replace(/[^\d.-]/g, '');
            }
        });
    });
    $("#mytable tbody").sortable({
        update: function () {
            SetTableOrder();
        }
    });

    //document.getElementById('copy').addEventListener('change', function () {
    //    if (this.checked) {
    //        //if ($("#PricingMethod_1").val() != "") {
    //        $("#PricingMethod_3").val($("#PricingMethod_1").val());
    //        PricingMethodDDL('3');
    //        if ($("#PricingMethod_3").val() == "Disc") {
    //            $("#txtDisc_3_1").val($("#txtDisc_1_1").val());
    //        }
    //        else {
    //            $("#txtValue_3_1").val($("#txtValue_1_1").val());
    //            $("#txtValue_3_2").val($("#txtValue_1_2").val());
    //            $("#txtValue_3_3").val($("#txtValue_1_3").val());
    //            $("#txtValue_3_4").val($("#txtValue_1_4").val());
    //        }
    //        //}
    //        $("#PricingSign_3").val($("#PricingSign_1").val());

    //        document.getElementById("Chk_Speci_Additional_2").checked = document.getElementById("Chk_Speci_Additional_1").checked;

    //        if ($("#txtFromDate").val() != "") {
    //            $("#txtFromDate1").val($("#txtFromDate").val());
    //        }
    //        if ($("#txtToDate").val() != "") {
    //            $("#txtToDate1").val($("#txtToDate").val());
    //        }
    //        againCalendarCall("2");
    //        //if ($("#PricingMethod_2").val() != "") {
    //        $("#PricingMethod_4").val($("#PricingMethod_2").val());
    //        PricingMethodDDL('4');
    //        if ($("#PricingMethod_4").val() == "Disc") {
    //            $("#txtDisc_4_1").val($("#txtDisc_2_1").val());
    //        }
    //        else {
    //            $("#txtValue_4_1").val($("#txtValue_2_1").val());
    //            $("#txtValue_4_2").val($("#txtValue_2_2").val());
    //            $("#txtValue_4_3").val($("#txtValue_2_3").val());
    //            $("#txtValue_4_4").val($("#txtValue_2_4").val());
    //        }
    //        //}
    //        $("#PricingSign_4").val($("#PricingSign_2").val());
    //        setTimeout(function () {
    //            document.getElementById("copy").checked = false;
    //        }, 1000);
    //    }
    //});
});
function checkValue(textbox) {
    const value = textbox.value.trim();
    const numericValue = parseFloat(value);
    if (numericValue >= 0 && numericValue <= 100) {
        textbox.value = NullReplaceDecimal4ToFixed(numericValue);
    } else {
        textbox.value = '';
    }
}
function Get_API_StockFilter() {
    $("#loading").css("display", "block");
    $.ajax({
        url: "/User/Get_PriceListCategory",
        async: false,
        type: "POST",
        success: function (data, textStatus, jqXHR) {
            if (data.Status == "1" && data.Data != null) {
                for (var k in data.Data) {
                    if (data.Data[k].Col_Id == 0) {
                        SupplierList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 1) {
                        ShapeList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 9) {
                        PointerList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 2) {
                        ColorList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 3) {
                        ClarityList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 4) {
                        CutList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 5) {
                        PolishList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 6) {
                        SymList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 7) {
                        FlsList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 34) {
                        LabList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 48) {
                        BGMList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 22) {
                        CrownBlackList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 20) {
                        CrownWhiteList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 21) {
                        TableBlackList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 19) {
                        TableWhiteList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 49) {
                        KeyToSymbolList.push(data.Data[k]);
                    }
                    if (data.Data[k].Col_Id == 67) {
                        GoodsTypeList.push(data.Data[k]);
                    }
                }


                if (SupplierList.length > 1) {
                    SupplierList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Supplier', isActive: false, Col_Id: 0 });
                }
                if (ShapeList.length > 1) {
                    ShapeList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Shape', isActive: false, Col_Id: 1 });
                }
                if (ColorList.length > 1) {
                    ColorList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Color', isActive: false, Col_Id: 2 });
                }
                if (ClarityList.length > 1) {
                    ClarityList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Clarity', isActive: false, Col_Id: 3 });
                }
                if (CutList.length > 1) {
                    CutList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Cut', isActive: false, Col_Id: 4 });
                }
                if (PolishList.length > 1) {
                    PolishList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Polish', isActive: false, Col_Id: 5 });
                }
                if (SymList.length > 1) {
                    SymList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Symm', isActive: false, Col_Id: 6 });
                }
                if (FlsList.length > 1) {
                    FlsList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Fls', isActive: false, Col_Id: 7 });
                }
                if (LabList.length > 1) {
                    LabList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Lab', isActive: false, Col_Id: 34 });
                }
                if (BGMList.length > 1) {
                    BGMList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'BGM', isActive: false, Col_Id: 48 });
                }
                if (CrownBlackList.length > 1) {
                    CrownBlackList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Crown Natts', isActive: false, Col_Id: 22 });
                }
                if (CrownWhiteList.length > 1) {
                    CrownWhiteList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Crown Inclusion', isActive: false, Col_Id: 20 });
                }
                if (TableBlackList.length > 1) {
                    TableBlackList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Table Natts', isActive: false, Col_Id: 21 });
                }
                if (TableWhiteList.length > 1) {
                    TableWhiteList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Table Inclusion', isActive: false, Col_Id: 19 });
                }
                if (GoodsTypeList.length > 1) {
                    GoodsTypeList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Goods Type', isActive: false, Col_Id: 67 });
                }

                INTENSITY = ['W-X', 'Y-Z', 'FAINT', 'VERY LIGHT', 'LIGHT', 'FANCY LIGHT', 'FANCY', 'FANCY INTENSE', 'FANCY GREEN', 'FANCY DARK', 'FANCY DEEP', 'FANCY VIVID', 'FANCY FAINT', 'DARK'];
                OVERTONE = ['NONE', 'BROWNISH', 'GRAYISH', 'GREENISH', 'YELLOWISH', 'PINKISH', 'ORANGEY', 'BLUISH', 'REDDISH', 'PURPLISH'];
                FANCY_COLOR = ['YELLOW', 'PINK', 'RED', 'GREEN', 'ORANGE', 'VIOLET', 'BROWN', 'GRAY', 'BLUE', 'PURPLE'];

                INTENSITY.sort();
                OVERTONE.sort();
                FANCY_COLOR.sort();
                INTENSITY.unshift("ALL SELECTED");
                OVERTONE.unshift("ALL SELECTED");
                FANCY_COLOR.unshift("ALL SELECTED");
            }


            //if (data.Status == "1" && data.Message == "SUCCESS") {
            //    $.each(data.Data, function (i, item) {
            //        //if (item.Type == "LOC") { LocationList.push(item); }
            //        //if (item.Type == "Goods Type") { GoodsTypeList.push(item); }
            //        //if (item.Type == "Supplier") { SupplierList.push(item); }
            //        if (item.Type == "POINTER") { PointerList.push(item); }
            //        if (item.Type == "SHAPE") { ShapeList.push(item); }
            //        if (item.Type == "COLOR") { ColorList.push(item); }
            //        if (item.Type == "CLARITY") { ClarityList.push(item); }
            //        if (item.Type == "CUT") { CutList.push(item); }
            //        if (item.Type == "POLISH") { PolishList.push(item); }
            //        if (item.Type == "SYMM") { SymList.push(item); }
            //        if (item.Type == "FLS") { FlsList.push(item); }
            //        if (item.Type == "LAB") { LabList.push(item); }
            //        if (item.Type == "BGM") { BGMList.push(item); }

            //        if (item.Type == "CROWN_NATTS") { CrownBlackList.push(item); }
            //        if (item.Type == "CROWN_INCL") { CrownWhiteList.push(item); }
            //        if (item.Type == "TABLE_NATTS") { TableBlackList.push(item); }
            //        if (item.Type == "TABLE_INCL") { TableWhiteList.push(item); }
            //    });

            //    //LocationList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'LOC', isActive: false });
            //    //SupplierList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Supplier', isActive: false });
            //    //GoodsTypeList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'Goods Type', isActive: false });
            //    ShapeList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'SHAPE', isActive: false });
            //    ColorList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'COLOR', isActive: false });
            //    ClarityList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'CLARITY', isActive: false });
            //    CutList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'CUT', isActive: false });
            //    PolishList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'POLISH', isActive: false });
            //    SymList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'SYMM', isActive: false });
            //    FlsList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'FLS', isActive: false });
            //    LabList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'LAB', isActive: false });
            //    BGMList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'BGM', isActive: false });
            //    CrownBlackList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'CROWN_NATTS', isActive: false });
            //    CrownWhiteList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'CROWN_INCL', isActive: false });
            //    TableBlackList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'TABLE_NATTS', isActive: false });
            //    TableWhiteList.unshift({ Id: 0, Value: 'ALL', SORT_NO: 0, Type: 'TABLE_INCL', isActive: false });

            //    INTENSITY = ['W-X', 'Y-Z', 'FAINT', 'VERY LIGHT', 'LIGHT', 'FANCY LIGHT', 'FANCY', 'FANCY INTENSE', 'FANCY GREEN', 'FANCY DARK', 'FANCY DEEP', 'FANCY VIVID', 'FANCY FAINT', 'DARK'];
            //    OVERTONE = ['NONE', 'BROWNISH', 'GRAYISH', 'GREENISH', 'YELLOWISH', 'PINKISH', 'ORANGEY', 'BLUISH', 'REDDISH', 'PURPLISH'];
            //    FANCY_COLOR = ['YELLOW', 'PINK', 'RED', 'GREEN', 'ORANGE', 'VIOLET', 'BROWN', 'GRAY', 'BLUE', 'PURPLE'];

            //    INTENSITY.sort();
            //    OVERTONE.sort();
            //    FANCY_COLOR.sort();
            //    INTENSITY.unshift("ALL SELECTED");
            //    OVERTONE.unshift("ALL SELECTED");
            //    FANCY_COLOR.unshift("ALL SELECTED");
            //}
            $("#loading").css("display", "none");
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("#loading").css("display", "none");
        }
    });
}
var ModalShow = function (ParameterLabel, ObjLst) {
    if (ParameterLabel == "CrownBlack")
        $('#exampleModalLabel').text("Crown Black");
    else if (ParameterLabel == "TableBlack")
        $('#exampleModalLabel').text("Table Black");
    else if (ParameterLabel == "CrownWhite")
        $('#exampleModalLabel').text("Crown White");
    else if (ParameterLabel == "TableWhite")
        $('#exampleModalLabel').text("Table White");
    else if (ParameterLabel == "GoodsType")
        $('#exampleModalLabel').text("Goods Type");
    else
        $('#exampleModalLabel').text(ParameterLabel);

    $('#divModal').removeClass("ng-hide").addClass("ng-show");

    var content = '<ul id="popupul" class="color-whit-box">';
    var c = 0, IsAllActiveC = 0;
    var list = [];
    list = ObjLst;
    list.forEach(function (item) {
        content += '<li id="li_' + ParameterLabel + '_' + item.Id + '" onclick="ItemClicked(\'' + ParameterLabel + '\',\'' + item.Value + '\',\'' + item.Id + '\', this);" class="';
        if (item.isActive) {
            content += 'active';

            if (ParameterLabel == "Supplier" || ParameterLabel == "Location" || ParameterLabel == "Shape" || ParameterLabel == "Color"
                || ParameterLabel == "Clarity" || ParameterLabel == "Cut" || ParameterLabel == "Polish" || ParameterLabel == "Sym"
                || ParameterLabel == "Lab" || ParameterLabel == "Fls" || ParameterLabel == "BGM" || ParameterLabel == "CrownBlack"
                || ParameterLabel == "TableBlack" || ParameterLabel == "CrownWhite" || ParameterLabel == "TableWhite"
                || ParameterLabel == "GoodsType") {
                IsAllActiveC = parseInt(IsAllActiveC) + 1;
            }
        }
        content += '">' + item.Value + '</li>';
        c = parseInt(c) + 1;
    });
    content += '</ul>';
    $('#divModal').empty();
    $('#divModal').append(content);

    $("#mpdal-footer").append('<button type="button" class="btn btn-primary" ng-click="ResetSelectedAttr(' + ParameterLabel + ');">Reset</button><button type="button" class="btn btn-secondary" data-dismiss="modal">Done</button>');

    if (ParameterLabel == "Supplier" || ParameterLabel == "Location" || ParameterLabel == "Shape" || ParameterLabel == "Color"
        || ParameterLabel == "Clarity" || ParameterLabel == "Cut" || ParameterLabel == "Polish" || ParameterLabel == "Sym"
        || ParameterLabel == "Lab" || ParameterLabel == "Fls" || ParameterLabel == "BGM" || ParameterLabel == "CrownBlack"
        || ParameterLabel == "TableBlack" || ParameterLabel == "CrownWhite" || ParameterLabel == "TableWhite"
        || ParameterLabel == "GoodsType") {
        if (IsAllActiveC == ObjLst.length - 1) {
            $("#li_" + ParameterLabel + "_0").addClass('active');
        }
    }
    $('#myModal').modal('toggle');
}
var ResetSelectedAttr = function (attr, obj) {
    _.each(obj, function (itm) {
        itm.isActive = false;
    });
    $(attr).empty();
}
var ResetCheckCarat = function () {
    _pointerlst = [];
    $(".divCheckedCaratValue").empty();
    $(".divCheckedPointerValue").empty();
}
function PricingMethodDDL(type) {
    if ($("#PricingMethod_" + type).val() == "") {
        document.getElementById("txtValue_" + type + "_1").disabled = true;
        document.getElementById("txtValue_" + type + "_2").disabled = true;
        document.getElementById("txtValue_" + type + "_3").disabled = true;
        document.getElementById("txtValue_" + type + "_4").disabled = true;
        document.getElementById("txtDisc_" + type + "_1").disabled = true;

        $("#PricingSign_" + type).val("=");
        $(".txtDisc_" + type).show();
        $(".txtValue_" + type).hide();
        pricing_reset(type);
        $("#lblPMErr_" + type).hide();
    }
    else {
        if ($("#PricingMethod_" + type).val() == "Disc") {
            document.getElementById("txtDisc_" + type + "_1").disabled = false;
            document.getElementById("txtValue_" + type + "_1").disabled = true;
            document.getElementById("txtValue_" + type + "_2").disabled = true;
            document.getElementById("txtValue_" + type + "_3").disabled = true;
            document.getElementById("txtValue_" + type + "_4").disabled = true;

            $(".txtDisc_" + type).show();
            $(".txtValue_" + type).hide();
            pricing_reset(type);

            $("#lblPMErr_" + type).show();
            //$("#lblPMErr_" + type).html("Allowed only value between -100 and 100.");
            $("#lblPMErr_" + type).html("Allowed only value between 0 and 100.");
        }
        if ($("#PricingMethod_" + type).val() == "Value") {
            document.getElementById("txtValue_" + type + "_1").disabled = false;
            document.getElementById("txtValue_" + type + "_2").disabled = false;
            document.getElementById("txtValue_" + type + "_3").disabled = false;
            document.getElementById("txtValue_" + type + "_4").disabled = false;
            document.getElementById("txtDisc_" + type + "_1").disabled = true;

            $(".txtDisc_" + type).hide();
            $(".txtValue_" + type).show();
            pricing_reset(type);

            $("#lblPMErr_" + type).show();
            $("#lblPMErr_" + type).html("Allowed only value between 0 and 100.");
        }
    }
    //$("#lblPMErr_" + type).html("");
    //$("#lblPMErr_" + type).hide();
}
function pricing_reset(type) {
    $("#txtDisc_" + type + "_1").val("");
    $("#txtValue_" + type + "_1").val("");
    $("#txtValue_" + type + "_2").val("");
    $("#txtValue_" + type + "_3").val("");
    $("#txtValue_" + type + "_4").val("");
}
var ItemClicked = function (ParameterLabel, item, c, curritem) {
    var list = [];
    if (ParameterLabel == 'Supplier') {
        list = SupplierList;
    }
    if (ParameterLabel == 'Carat') {
        list = PointerList;
    }
    if (ParameterLabel == 'GoodsType') {
        list = GoodsTypeList;
    }
    if (ParameterLabel == 'Location') {
        list = LocationList;
    }
    if (ParameterLabel == 'Shape') {
        list = ShapeList;
    }
    if (ParameterLabel == 'Color') {
        list = ColorList;
    }
    if (ParameterLabel == 'Clarity') {
        list = ClarityList;
    }
    if (ParameterLabel == 'Cut') {
        list = CutList;
    }
    if (ParameterLabel == 'Polish') {
        list = PolishList;
    }
    if (ParameterLabel == 'Sym') {
        list = SymList;
    }
    if (ParameterLabel == 'Lab') {
        list = LabList;
    }
    if (ParameterLabel == 'Fls') {
        list = FlsList;
    }
    if (ParameterLabel == 'BGM') {
        list = BGMList;
    }
    if (ParameterLabel == 'CrownBlack') {
        list = CrownBlackList;
    }
    if (ParameterLabel == 'TableBlack') {
        list = TableBlackList;
    }
    if (ParameterLabel == 'CrownWhite') {
        list = CrownWhiteList;
    }
    if (ParameterLabel == 'TableWhite') {
        list = TableWhiteList;
    }
    if (item == "ALL") {
        if (ParameterLabel == "Supplier" || ParameterLabel == "Location" || ParameterLabel == "Shape" || ParameterLabel == "Color"
            || ParameterLabel == "Clarity" || ParameterLabel == "Cut" || ParameterLabel == "Polish" || ParameterLabel == "Sym"
            || ParameterLabel == "Lab" || ParameterLabel == "Fls" || ParameterLabel == "BGM" || ParameterLabel == "CrownBlack"
            || ParameterLabel == "TableBlack" || ParameterLabel == "CrownWhite" || ParameterLabel == "TableWhite"
            || ParameterLabel == "GoodsType") {
            if (ParameterLabel == "Color" && item == "ALL" && $("#li_" + ParameterLabel + "_0").hasClass("active") == true) {
                R_F_All_Only_Checkbox_Clr_Rst("1");
            }
            else {
                for (var j = 0; j <= list.length - 1; j++) {
                    if (list[j].Value != "ALL") {
                        var itm = _.find(list, function (i) {
                            return i.Value == list[j].Value
                        });
                        if ($("#li_" + ParameterLabel + "_0").hasClass("active")) {
                            itm.isActive = true;
                            $("#li_" + ParameterLabel + "_" + (ParameterLabel == "Supplier" ? j : list[j].Id)).addClass('active');
                        }
                        else {
                            itm.isActive = false;
                            $("#li_" + ParameterLabel + "_" + (ParameterLabel == "Supplier" ? j : list[j].Id)).removeClass('active');
                        }
                        //itm.isActive = !itm.isActive;
                    }
                    else {
                        $("#li_" + ParameterLabel + "_0").toggleClass('active');
                    }
                }
            }
        }
        else if (ParameterLabel == "Carat") {
            _pointerlst = PointerList;
            $(".divCheckedPointerValue").empty();
            for (var j = 0; j <= list.length - 1; j++) {
                list[j].isActive = true;
                $('.divCheckedPointerValue').append('<li id="C_' + list[j].Id + '" class="carat-li-top allcrt">' + list[j].Value + '<i class="fa fa-times-circle" aria-hidden="true" onclick="NewSizeGroupRemove(' + list[j].Id + ');"></i></li>');
            }
        }
    }
    else {
        if (ParameterLabel == "Color") {
            R_F_All_Only_Checkbox_Clr_Rst("-0");
        }
        var itm = _.find(list, function (i) { return i.Value == item });
        if ($("#li_" + ParameterLabel + "_" + c).hasClass("active")) {
            itm.isActive = false;
            $("#li_" + ParameterLabel + "_" + c).removeClass('active');
        }
        else {
            itm.isActive = true;
            $("#li_" + ParameterLabel + "_" + c).addClass('active');
        }

        if (ParameterLabel == "Supplier" || ParameterLabel == "Location" || ParameterLabel == "Shape" || ParameterLabel == "Color"
            || ParameterLabel == "Clarity" || ParameterLabel == "Cut" || ParameterLabel == "Polish" || ParameterLabel == "Sym"
            || ParameterLabel == "Lab" || ParameterLabel == "Fls" || ParameterLabel == "BGM" || ParameterLabel == "CrownBlack"
            || ParameterLabel == "TableBlack" || ParameterLabel == "CrownWhite" || ParameterLabel == "TableWhite"
            || ParameterLabel == "GoodsType") {
            var IsAllActiveC = 0;
            for (var j = 0; j <= list.length - 1; j++) {
                if (list[j].Value != "ALL") {
                    if (list[j].isActive == true) {
                        IsAllActiveC = parseInt(IsAllActiveC) + 1;
                    }
                }
            }
            if (IsAllActiveC == list.length - 1) {
                $("#li_" + ParameterLabel + "_0").addClass('active');
            }
            else {
                $("#li_" + ParameterLabel + "_0").removeClass('active');
            }
        }
        //$(curritem).toggleClass('active');
    }
    SetSearchParameter();
}
var SetSearchParameter = function () {

    var supplierLst = _.pluck(_.filter(SupplierList, function (e) { return e.isActive == true }), 'Value').join(",");
    var pointerLst = _.pluck(_.filter(PointerList, function (e) { return e.isActive == true }), 'Value').join(",");
    //var locationLst = _.pluck(_.filter(LocationList, function (e) { return e.isActive == true }), 'Value').join(",");
    var shapeLst = _.pluck(_.filter(ShapeList, function (e) { return e.isActive == true }), 'Value').join(",");
    var colorLst = _.pluck(_.filter(ColorList, function (e) { return e.isActive == true }), 'Value').join(",");
    var clarityLst = _.pluck(_.filter(ClarityList, function (e) { return e.isActive == true }), 'Value').join(",");
    var cutlst = _.pluck(_.filter(CutList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Pollst = _.pluck(_.filter(PolishList, function (e) { return e.isActive == true }), 'Value').join(",");
    var Symlst = _.pluck(_.filter(SymList, function (e) { return e.isActive == true }), 'Value').join(",");
    var labLst = _.pluck(_.filter(LabList, function (e) { return e.isActive == true }), 'Value').join(",");
    var flslst = _.pluck(_.filter(FlsList, function (e) { return e.isActive == true }), 'Value').join(",");
    var bgmlst = _.pluck(_.filter(BGMList, function (e) { return e.isActive == true }), 'Value').join(",");
    var crnblacklst = _.pluck(_.filter(CrownBlackList, function (e) { return e.isActive == true }), 'Value').join(",");
    var tblblacklst = _.pluck(_.filter(TableBlackList, function (e) { return e.isActive == true }), 'Value').join(",");
    var crnwhitelst = _.pluck(_.filter(CrownWhiteList, function (e) { return e.isActive == true }), 'Value').join(",");
    var tblwhitelst = _.pluck(_.filter(TableWhiteList, function (e) { return e.isActive == true }), 'Value').join(",");
    var goodstypeLst = _.pluck(_.filter(GoodsTypeList, function (e) { return e.isActive == true }), 'Value').join(",");

    CheckedSupplierValue = supplierLst;
    CheckedPointerValue = pointerLst;
    //CheckedLocationValue = locationLst;
    CheckedShapeValue = shapeLst;
    CheckedColorValue = colorLst;
    CheckedClarityValue = clarityLst;
    CheckedCutValue = cutlst;
    CheckedPolValue = Pollst;
    CheckedSymValue = Symlst;
    CheckedLabValue = labLst;
    CheckedFLsValue = flslst;
    CheckedBgmValue = bgmlst;
    CheckedCrnBlackValue = crnblacklst;
    CheckedTblBlackValue = tblblacklst;
    CheckedCrnWhiteValue = crnwhitelst;
    CheckedTblWhiteValue = tblwhitelst;
    CheckedGoodsTypeValue = goodstypeLst;

    if (CheckedSupplierValue.split(",").length >= 1) {
        $(".divCheckedSupplierValue").empty();
        $(".divCheckedSupplierValue").append(CheckedSupplierValue);
        $(".divCheckedSupplierValue").attr({
            "title": CheckedSupplierValue
        });
    }
    if (CheckedGoodsTypeValue.split(",").length >= 1) {
        $(".divCheckedGoodsTypeValue").empty();
        $(".divCheckedGoodsTypeValue").append(CheckedGoodsTypeValue);
        $(".divCheckedGoodsTypeValue").attr({
            "title": CheckedGoodsTypeValue
        });
    }
    if (CheckedLocationValue.split(",").length >= 1) {
        $(".divCheckedLocationValue").empty();
        $(".divCheckedLocationValue").append(CheckedLocationValue);
        $(".divCheckedLocationValue").attr({
            "title": CheckedLocationValue
        });
    }
    if (CheckedShapeValue.split(",").length >= 1) {
        $(".divCheckedShapeValue").empty();
        $(".divCheckedShapeValue").append(CheckedShapeValue);
        $(".divCheckedShapeValue").attr({
            "title": CheckedShapeValue
        });
    }
    if (CheckedPointerValue.split(",").length >= 1) {
        $(".divCheckedCaratValue").empty();
        $(".divCheckedCaratValue").append(_.pluck(_.filter(_pointerlst, function (e) { return e.isActive == true }), 'Value').join(","));
        $(".divCheckedCaratValue").attr({
            "title": _.pluck(_.filter(_pointerlst, function (e) { return e.isActive == true }), 'Value').join(",")
        });
    }
    if (CheckedColorValue.split(",").length >= 1) {
        $(".divCheckedColorValue").empty();
        $(".divCheckedColorValue").append(CheckedColorValue);
        $(".divCheckedColorValue").attr({
            "title": CheckedColorValue
        });
    }
    if (CheckedColorValue == "") {
        Set_FancyColor();
    }

    if (Regular_All == true) {
        $(".divCheckedColorValue").empty();
        $(".divCheckedColorValue").append("<b>REGULAR ALL</b>");
        $(".divCheckedColorValue").attr({
            "title": "<b>REGULAR ALL</b>"
        });
    }
    else if (Fancy_All == true) {
        $(".divCheckedColorValue").empty();
        $(".divCheckedColorValue").append("<b>FANCY ALL</b>");
        $(".divCheckedColorValue").attr({
            "title": "<b>FANCY ALL</b>"
        });
    }
    if (CheckedClarityValue.split(",").length >= 1) {
        $(".divCheckedClarityValue").empty();
        $(".divCheckedClarityValue").append(CheckedClarityValue);
        $(".divCheckedClarityValue").attr({
            "title": CheckedClarityValue
        });
    }
    if (CheckedCutValue.split(",").length >= 1) {
        $(".divCheckedCutValue").empty();
        $(".divCheckedCutValue").append(CheckedCutValue);
        $(".divCheckedCutValue").attr({
            "title": CheckedCutValue
        });
    }
    if (CheckedPolValue.split(",").length >= 1) {
        $(".divCheckedPolValue").empty();
        $(".divCheckedPolValue").append(CheckedPolValue);
        $(".divCheckedPolValue").attr({
            "title": CheckedPolValue
        });
    }
    if (CheckedSymValue.split(",").length >= 1) {
        $(".divCheckedSymValue").empty();
        $(".divCheckedSymValue").append(CheckedSymValue);
        $(".divCheckedSymValue").attr({
            "title": CheckedSymValue
        });
    }
    if (CheckedLabValue.split(",").length >= 1) {
        $(".divCheckedLabValue").empty();
        $(".divCheckedLabValue").append(CheckedLabValue);
        $(".divCheckedLabValue").attr({
            "title": CheckedLabValue
        });
    }
    if (CheckedFLsValue.split(",").length >= 1) {
        $(".divCheckedFLsValue").empty();
        $(".divCheckedFLsValue").append(CheckedFLsValue);
        $(".divCheckedFLsValue").attr({
            "title": CheckedFLsValue
        });
    }
    if (CheckedBgmValue.split(",").length >= 1) {
        $(".divCheckedBGMValue").empty();
        $(".divCheckedBGMValue").append(CheckedBgmValue);
        $(".divCheckedBGMValue").attr({
            "title": CheckedFLsValue
        });
    }
    if (CheckedCrnBlackValue.split(",").length >= 1) {
        $(".divCheckedCrnBlackValue").empty();
        $(".divCheckedCrnBlackValue").append(CheckedCrnBlackValue);
        $(".divCheckedCrnBlackValue").attr({
            "title": CheckedCrnBlackValue
        });
    }
    if (CheckedCrnWhiteValue.split(",").length >= 1) {
        $(".divCheckedCrnWhiteValue").empty();
        $(".divCheckedCrnWhiteValue").append(CheckedCrnWhiteValue);
        $(".divCheckedCrnWhiteValue").attr({
            "title": CheckedCrnWhiteValue
        });
    }
    if (CheckedTblBlackValue.split(",").length >= 1) {
        $(".divCheckedTblBlackValue").empty();
        $(".divCheckedTblBlackValue").append(CheckedTblBlackValue);
        $(".divCheckedTblBlackValue").attr({
            "title": CheckedTblBlackValue
        });
    }
    if (CheckedTblWhiteValue.split(",").length >= 1) {
        $(".divCheckedTblWhiteValue").empty();
        $(".divCheckedTblWhiteValue").append(CheckedTblWhiteValue);
        $(".divCheckedTblWhiteValue").attr({
            "title": CheckedTblWhiteValue
        });
    }
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
function Reset_API_Filter() {
    ResetSelectedAttr('.divCheckedSupplierValue', SupplierList);
    ResetSelectedAttr('.divCheckedLocationValue', LocationList);
    ResetSelectedAttr('.divCheckedGoodsTypeValue', GoodsTypeList);
    ResetSelectedAttr('.divCheckedShapeValue', ShapeList);
    ResetCheckCarat();
    ResetCheckColors();
    Regular = true;
    Fancy = false;
    $("#Regular").addClass("btn-spn-opt-active");
    $("#Fancy").removeClass("btn-spn-opt-active");
    $("#div_Regular").show();
    $("#div_Fancy").hide();
    document.getElementById("chk_Regular_All").checked = false;
    document.getElementById("chk_Fancy_All").checked = false;
    ResetSelectedAttr('.divCheckedColorValue', ColorList);
    ResetSelectedAttr('.divCheckedClarityValue', ClarityList);
    ResetSelectedAttr('.divCheckedCutValue', CutList);
    ResetSelectedAttr('.divCheckedPolValue', PolishList);
    ResetSelectedAttr('.divCheckedSymValue', SymList);
    ResetSelectedAttr('.divCheckedFLsValue', FlsList);
    ResetSelectedAttr('.divCheckedLabValue', LabList);
    $("#FromLength").val("");
    $("#ToLength").val("");
    $("#FromWidth").val("");
    $("#ToWidth").val("");
    $("#FromDepth").val("");
    $("#ToDepth").val("");
    $("#FromDepthPer").val("");
    $("#ToDepthPer").val("");
    $("#FromTablePer").val("");
    $("#ToTablePer").val("");
    $("#FromCrAng").val("");
    $("#ToCrAng").val("");
    $("#FromCrHt").val("");
    $("#ToCrHt").val("");
    $("#FromPavAng").val("");
    $("#ToPavAng").val("");
    $("#FromPavHt").val("");
    $("#ToPavHt").val("");
    resetKeytoSymbol();
    ResetSelectedAttr('.divCheckedBGMValue', BGMList);
    ResetSelectedAttr('.divCheckedCrnBlackValue', CrownBlackList);
    ResetSelectedAttr('.divCheckedTblBlackValue', CrownWhiteList);
    ResetSelectedAttr('.divCheckedCrnWhiteValue', TableBlackList);
    ResetSelectedAttr('.divCheckedTblWhiteValue', TableWhiteList);

    $(".IgAll").prop("checked", true);
    $(".VdAll").prop("checked", true);



    for (var j = 1; j <= 2; j++) {
        $("#PricingMethod_" + j).val("");
        $("#PricingSign_" + j).val("=");

        $(".txtValue_" + j).hide();
        $(".txtDisc_" + j).show();

        $("#txtDisc_" + j + "_1").val("");
        $("#txtValue_" + j + "_1").val("");
        $("#txtValue_" + j + "_2").val("");
        $("#txtValue_" + j + "_3").val("");
        $("#txtValue_" + j + "_4").val("");

        document.getElementById("txtDisc_" + j + "_1").disabled = true;
        document.getElementById("txtValue_" + j + "_1").disabled = true;
        document.getElementById("txtValue_" + j + "_2").disabled = true;
        document.getElementById("txtValue_" + j + "_3").disabled = true;
        document.getElementById("txtValue_" + j + "_4").disabled = true;
        $("#lblPMErr_" + j).hide();
        $("#lblPMErr_" + j).html("");
    }
    FromTo_Date("1");
    //FromTo_Date("2");

    //document.getElementById("copy").checked = false;
    document.getElementById("Chk_Speci_Additional_1").checked = false;
    //document.getElementById("Chk_Speci_Additional_2").checked = false;

    supplierlst = "";
    locationLst = "";
    shapeLst = "";
    pointerlst = "";
    _pointerlst = [];
    colorLst = "";
    clarityLst = "";
    cutlst = "";
    Pollst = "";
    Symlst = "";
    labLst = "";
    flslst = "";
    bgmlst = "";
    crnblacklst = "";
    tblblacklst = "";
    crnwhitelst = "";
    tblwhitelst = "";

    CheckedSupplierValue = "";
    CheckedLocationValue = "";
    CheckedShapeValue = "";
    CheckedPointerValue = "";
    CheckedColorValue = "";
    CheckedClarityValue = "";
    CheckedCutValue = "";
    CheckedPolValue = "";
    CheckedSymValue = "";
    CheckedLabValue = "";
    CheckedFLsValue = "";
    CheckedBgmValue = "";
    CheckedCrnBlackValue = "";
    CheckedTblBlackValue = "";
    CheckedCrnWhiteValue = "";
    CheckedTblWhiteValue = "";
}
function BindKeyToSymbolList() {
    $('#searchkeytosymbol').html("");
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


    //$.ajax({
    //    url: "/User/get_key_to_symbol",
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
    //                    //itm.ACTIVE = false;
    //                    //itm.INACTIVE = false;
    //                });
    //                $('#searchkeytosymbol').append('<div class="ps-scrollbar-x-rail" style="left: 0px; bottom: 0px;"><div class="ps-scrollbar-x" tabindex="0" style="left: 0px; width: 0px;"></div></div><div class="ps-scrollbar-y-rail" style="top: 0px; right: 0px;"><div class="ps-scrollbar-y" tabindex="0" style="top: 0px; height: 0px;"></div></div>');
    //            }
    //        }
    //    },
    //    error: function (jqXHR, textStatus, errorThrown) {
    //    }
    //});
}
function Key_to_symbolShow() {
    setTimeout(function () {
        if (KTS == 0) {
            $(".carat-dropdown-main").show();
            KTS = 1;
        }
        else {
            $(".carat-dropdown-main").hide();
            KTS = 0;
        }
    }, 2);
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
}
function setFromCarat() {
    if ($('#txtfromcarat').val() != "") {
        $('#txtfromcarat').val(parseFloat($('#txtfromcarat').val()).toFixed(2));
        $('#txttocarat').val(parseFloat($('#txtfromcarat').val()).toFixed(2));
    } else {
        $('#txtfromcarat').val("");
    }
    if ($('#txttocarat').val() == "") {
        $('#txttocarat').val("");
    }
}
function setToCarat() {
    if ($('#txttocarat').val() != "") {
        $('#txttocarat').val(parseFloat($('#txttocarat').val()).toFixed(2));
    } else {
        $('#txttocarat').val("");
    }
    if ($('#txtfromcarat').val() == "") {
        $('#txtfromcarat').val("");
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
function NewSizeGroup() {
    fcarat = $('#txtfromcarat').val();
    tcarat = $('#txttocarat').val();

    if (fcarat == "" && tcarat == "" || fcarat == 0 && tcarat == 0) {
        toastr.warning("Please Enter Carat !!");
        return false;
    }
    if (fcarat == "") {
        fcarat = "0";
    }
    var _pointerlst_ = [];
    _pointerlst_.push({
        "Id": _pointerlst.length + 1,
        "Value": fcarat + '-' + tcarat,
        "isActive": true,
    });

    var lst = _.filter(_pointerlst, function (e) { return (e.Value == _pointerlst_[0].Value) });
    if (lst.length == 0) {
        var _pointerlst1 = _pointerlst;
        _pointerlst = [];
        for (var i = 0; i <= _pointerlst1.length - 1; i++) {
            _pointerlst.push({
                "Id": parseInt(i) + 1,
                "Value": _pointerlst1[i].Value,
                "isActive": _pointerlst1[i].isActive,
            });
        }

        _pointerlst.push({
            "Id": parseInt(_pointerlst.length) + 1,
            "Value": _pointerlst_[0].Value,
            "isActive": _pointerlst_[0].isActive,
        });

        $(".divCheckedPointerValue").empty();
        for (var j = 0; j <= _pointerlst.length - 1; j++) {
            $('.divCheckedPointerValue').append('<li id="C_' + _pointerlst[j].Id + '" class="carat-li-top allcrt">' + _pointerlst[j].Value + '<i class="fa fa-times-circle" aria-hidden="true" onclick="NewSizeGroupRemove(' + _pointerlst[j].Id + ');"></i></li>');
        }
        SetSearchParameter();
        $('#txtfromcarat').val("");
        $('#txttocarat').val("");
    }
    else {
        $('#txtfromcarat').val("");
        $('#txttocarat').val("");
        toastr.warning("Carat is already exist !!");
    }
    //SetSearchParameter();
}
function NewSizeGroupRemove(id) {
    $('#C_' + id).remove();
    var cList = _.reject(_pointerlst, function (e) { return e.Id == id });
    _pointerlst = cList;
    SetSearchParameter();
}
var ResetCheckColors = function () {
    colorLst = [];
    Check_Color_1 = [];
    $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
    $('#ddl_INTENSITY input[type="checkbox"]').prop('checked', false);

    Check_Color_2 = [];
    $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
    $('#ddl_OVERTONE input[type="checkbox"]').prop('checked', false);

    Check_Color_3 = [];
    $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
    $('#ddl_FANCY_COLOR input[type="checkbox"]').prop('checked', false);

    $("#sym-sec0 .carat-dropdown-main").hide();
    $("#sym-sec1 .carat-dropdown-main").hide();
    $("#sym-sec2 .carat-dropdown-main").hide();
    $("#sym-sec3 .carat-dropdown-main").hide();

    C3 = 0, C1 = 0, KTS = 0, C2 = 0;
    _.each(ColorList, function (itm) {
        itm.isActive = false;
    });
    for (var j = 0; j <= ColorList.length - 1; j++) {
        $("#li_Color_" + j).removeClass('active');
    }

    R_F_All_Only_Checkbox_Clr_Rst("1");
    SetSearchParameter();
}
function Bind_RColor() {
    $("#divCheckedColorValue1").empty();
    for (var j = 0; j <= ColorList.length - 1; j++) {
        $('#divCheckedColorValue1').append('<li id="li_Color_' + ColorList[j].Id + '" onclick="ItemClicked(\'Color\',\'' + ColorList[j].Value + '\',\'' + ColorList[j].Id + '\', this);">' + ColorList[j].Value + '</li>');
    }
}
function FcolorBind() {
    for (var i = 0; i <= INTENSITY.length - 1; i++) {
        $('#ddl_INTENSITY').append('<div class="col-12 pl-0 pr-0 ng-scope">'
            + '<ul class="row m-0">'
            + '<li class="carat-dropdown-chkbox">'
            + '<div class="main-cust-check">'
            + '<label class="cust-rdi-bx mn-check">'
            + '<input type="checkbox" class="checkradio f_clr_clk" id="CHK_I_' + i + '" name="CHK_I_' + i + '" onclick="GetCheck_INTENSITY_List(\'' + INTENSITY[i] + '\',' + i + ');">'
            + '<span class="cust-rdi-check" style="font-size:15px;">'
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
            + '<span class="cust-rdi-check" style="font-size:15px;">'
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
            + '<span class="cust-rdi-check" style="font-size:15px;">'
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
    R_F_All_Only_Checkbox_Clr_Rst("0");
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
    Set_FancyColor();
}
function GetCheck_OVERTONE_List(item, id) {
    R_F_All_Only_Checkbox_Clr_Rst("0");
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
    Set_FancyColor();
}
function GetCheck_FANCY_COLOR_List(item, id) {
    R_F_All_Only_Checkbox_Clr_Rst("0");
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
    Set_FancyColor();
}
function resetINTENSITY() {
    Check_Color_1 = [];
    $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
    $('#ddl_INTENSITY input[type="checkbox"]').prop('checked', false);
    C1 = 1;
    INTENSITYShow();
    SetSearchParameter();
}
function resetOVERTONE() {
    Check_Color_2 = [];
    $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
    $('#ddl_OVERTONE input[type="checkbox"]').prop('checked', false);
    C2 = 1;
    OVERTONEShow();
    SetSearchParameter();
}
function resetFANCY_COLOR() {
    Check_Color_3 = [];
    $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
    $('#ddl_FANCY_COLOR input[type="checkbox"]').prop('checked', false);
    C3 = 1;
    FANCY_COLORShow();
    SetSearchParameter();
}
function Set_FancyColor() {
    FC = "";
    if (Check_Color_1.length != 0) {
        FC += (FC == "" ? "" : "</br>") + "<b>INTENSITY :</b>";
        FC += _.pluck(_.filter(Check_Color_1), 'Symbol').join(",");
    }
    if (Check_Color_2.length != 0) {
        FC += (FC == "" ? "" : "</br>") + "<b>OVERTONE :</b>";
        FC += _.pluck(_.filter(Check_Color_2), 'Symbol').join(",");
    }
    if (Check_Color_3.length != 0) {
        FC += (FC == "" ? "" : "</br>") + "<b>FANCY COLOR :</b>";
        FC += _.pluck(_.filter(Check_Color_3), 'Symbol').join(",");
    }
    $(".divCheckedColorValue").empty();
    $(".divCheckedColorValue").append(FC);
    $(".divCheckedColorValue").attr({
        "title": FC
    });
}
function ActiveOrNot(id) {
    if ($("#" + id).hasClass("btn-spn-opt-active")) {
        //$("#" + id).removeClass("btn-spn-opt-active");
        //if (id == "Regular") {
        //    Regular = false;
        //}
        //if (id == "Fancy") {
        //    Fancy = false;
        //}
    }
    else {
        $("#" + id).addClass("btn-spn-opt-active");
        if (id == "Regular") {
            Regular = true;
            Fancy = false;
            $("#Fancy").removeClass("btn-spn-opt-active");
            $("#div_Regular").show();
            $("#div_Fancy").hide();
        }
        if (id == "Fancy") {
            Fancy = true;
            Regular = false;
            $("#Regular").removeClass("btn-spn-opt-active");
            $("#div_Regular").hide();
            $("#div_Fancy").show();
        }
        R_F_All_Only_Checkbox_Clr_Rst("1");
        ResetCheckColors();
    }
}
function R_F_All_Only_Checkbox_Clr_Rst(where) {
    if (where != "-1") {
        $('#chk_Regular_All').prop('checked', false);
        $('#chk_Fancy_All').prop('checked', false);
    }
    if (where == "-1") {
        where = "1";
    }
    if (where != "-0") {
        for (var j = 0; j <= ColorList.length - 1; j++) {
            if (ColorList[j].Value != "ALL") {
                ColorList[j].isActive = false;
            }
            $("#li_Color_" + ColorList[j].Id).removeClass('active');
        }
    }
    if (where == "-0") {
        where = "1";
    }

    Regular_All = false;
    Fancy_All = false;
    if (where == "1") {
        Check_Color_1 = [];
        $('#c1_spanselected').html('' + Check_Color_1.length + ' - Selected');
        $('#ddl_INTENSITY input[type="checkbox"]').prop('checked', false);
        Check_Color_2 = [];
        $('#c2_spanselected').html('' + Check_Color_2.length + ' - Selected');
        $('#ddl_OVERTONE input[type="checkbox"]').prop('checked', false);
        Check_Color_3 = [];
        $('#c3_spanselected').html('' + Check_Color_3.length + ' - Selected');
        $('#ddl_FANCY_COLOR input[type="checkbox"]').prop('checked', false);
    }


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
function color_ddl_close() {
    $("#sym-sec1 .carat-dropdown-main").hide();
    $("#sym-sec2 .carat-dropdown-main").hide();
    $("#sym-sec3 .carat-dropdown-main").hide();
}
var SetFinalColors = function () {
    //$(".divCheckedCaratValue").empty();
    //$(".divCheckedCaratValue").append(_.pluck(_.filter(_pointerlst, function (e) { return e.isActive == true }), 'Value').join(","));
    //$(".divCheckedCaratValue").attr({
    //    "title": _.pluck(_.filter(_pointerlst, function (e) { return e.isActive == true }), 'Value').join(",")
    //});
}
function numvalid(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}
function DisValValid(pricing_method, evt, type) {
    if (pricing_method == "Value") {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
    if (pricing_method == "Disc") {
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode != 46 && charCode != 45 && charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }
        return true;
    }
}
function LenCheckr(pricing_method, type) {
    var lblPMErr, txtValue1, txtValue2, txtValue3, txtValue4, txtDisc1;
    lblPMErr = "lblPMErr_" + type;
    txtValue1 = "txtValue_" + type + "_1";
    txtValue2 = "txtValue_" + type + "_2";
    txtValue3 = "txtValue_" + type + "_3";
    txtValue4 = "txtValue_" + type + "_4";
    txtDisc1 = "txtDisc_" + type + "_1";

    if (pricing_method == "Value") {
        if (parseFloat($("#" + txtValue1).val()) > 100 || parseFloat($("#" + txtValue1).val()) < 0) {
            setTimeout(function () {
                $("#" + lblPMErr).html("Allowed only 0.00 to 100 percentage.");
                $("#" + lblPMErr).show();
            }, 50);
            return $("#" + txtValue1).val("");
        }
        else if (parseFloat($("#" + txtValue1).val()) == NaN) {
            setTimeout(function () {
                $("#" + lblPMErr).html("Allowed only 0.00 to 100 percentage.");
                $("#" + lblPMErr).show();
            }, 50);
            return $("#" + txtValue1).val("");
        }
        else {
            $("#" + lblPMErr).html("");
            $("#" + lblPMErr).hide();
        }

        if (parseFloat($("#" + txtValue2).val()) > 100 || parseFloat($("#" + txtValue2).val()) < 0) {
            setTimeout(function () {
                $("#" + lblPMErr).html("Allowed only 0.00 to 100 percentage.");
                $("#" + lblPMErr).show();
            }, 50);
            return $("#" + txtValue2).val("");
        }
        else if (parseFloat($("#" + txtValue2).val()) == NaN) {
            setTimeout(function () {
                $("#" + lblPMErr).html("Allowed only 0.00 to 100 percentage.");
                $("#" + lblPMErr).show();
            }, 50);
            return $("#" + txtValue2).val("");
        }
        else {
            $("#" + lblPMErr).html("");
            $("#" + lblPMErr).hide();
        }

        if (parseFloat($("#" + txtValue3).val()) > 100 || parseFloat($("#" + txtValue3).val()) < 0) {
            setTimeout(function () {
                $("#" + lblPMErr).html("Allowed only 0.00 to 100 percentage.");
                $("#" + lblPMErr).show();
            }, 50);
            return $("#" + txtValue3).val("");
        }
        else if (parseFloat($("#" + txtValue3).val()) == NaN) {
            setTimeout(function () {
                $("#" + lblPMErr).html("Allowed only 0.00 to 100 percentage.");
                $("#" + lblPMErr).show();
            }, 50);
            return $("#" + txtValue3).val("");
        }
        else {
            $("#" + lblPMErr).html("");
            $("#" + lblPMErr).hide();
        }

        if (parseFloat($("#" + txtValue4).val()) > 100 || parseFloat($("#" + txtValue4).val()) < 0) {
            setTimeout(function () {
                $("#" + lblPMErr).html("Allowed only 0.00 to 100 percentage.");
                $("#" + lblPMErr).show();
            }, 50);
            return $("#" + txtValue4).val("");
        }
        else if (parseFloat($("#" + txtValue4).val()) == NaN) {
            setTimeout(function () {
                $("#" + lblPMErr).html("Allowed only 0.00 to 100 percentage.");
                $("#" + lblPMErr).show();
            }, 50);
            return $("#" + txtValue4).val("");
        }
        else {
            $("#" + lblPMErr).html("");
            $("#" + lblPMErr).hide();
        }
    }
    if (pricing_method == "Disc") {
        if (parseFloat($("#" + txtDisc1).val()) < -100 || parseFloat($("#" + txtDisc1).val()) > -0) {
            setTimeout(function () {
                $("#" + lblPMErr).html("Allowed only -0.00 to -100 percentage.");
                $("#" + lblPMErr).show();
            }, 50);
            return $("#" + txtDisc1).val("");
        }
        else if (parseFloat($("#" + txtDisc1).val()) == NaN) {
            setTimeout(function () {
                $("#" + lblPMErr).html("Allowed only -0.00 to -100 percentage.");
                $("#" + lblPMErr).show();
            }, 50);
            return $("#" + txtDisc1).val("");
        }
        else {
            $("#" + lblPMErr).html("");
            $("#" + lblPMErr).hide();
        }
    }
}
function generate_uuidv4() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g,
        function (c) {
            var uuid = Math.random() * 16 | 0, v = c == 'x' ? uuid : (uuid & 0x3 | 0x8);
            return uuid.toString(16);
        });
}

function HTML_CREATE(
    Supplier, Shape, Carat, Color_Type, Color, F_INTENSITY, F_OVERTONE, F_FANCY_COLOR, MixColor, Clarity, Cut, Polish, Sym, Fls, Lab,
    FromLength, ToLength, FromWidth, ToWidth, FromDepth, ToDepth, FromDepthinPer, ToDepthinPer, FromTableinPer, ToTableinPer, FromCrAng, ToCrAng,
    FromCrHt, ToCrHt, FromPavAng, ToPavAng, FromPavHt, ToPavHt,
    Keytosymbol, dCheckKTS, dUNCheckKTS, BGM, CrownBlack, TableBlack, CrownWhite, TableWhite, GoodsType, Image, Video,
    PricingMethod_1, PricingSign_1, txtDisc_1_1, txtValue_1_1, txtValue_1_2, txtValue_1_3, txtValue_1_4,
    Chk_Speci_Additional_1, txtFromDate, txtToDate,
    PricingMethod_2, PricingSign_2, txtDisc_2_1, txtValue_2_1, txtValue_2_2, txtValue_2_3, txtValue_2_4,
    new_id) {

    var html = "<tr class='tr11'>";
    html += "<th class='Row Fi-Criteria' style=''></th>";
    html += "<td><span class='Fi-Criteria Supplier'>" + Supplier + "</span></td>";
    html += "<td><span class='Fi-Criteria Shape'>" + Shape + "</span></td>";
    html += "<td><span class='Fi-Criteria Carat'>" + Carat + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria ColorType'>" + Color_Type + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria Color'>" + Color + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria dCheckINTENSITY'>" + F_INTENSITY + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria dCheckOVERTONE'>" + F_OVERTONE + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria dCheckFANCY_COLOR'>" + F_FANCY_COLOR + "</span></td>";
    html += "<td><span class='Fi-Criteria MixColor'>" + MixColor + "</span></td>";
    html += "<td><span class='Fi-Criteria Clarity'>" + Clarity + "</span></td>";
    html += "<td><span class='Fi-Criteria Cut'>" + Cut + "</span></td>";
    html += "<td><span class='Fi-Criteria Polish'>" + Polish + "</span></td>";
    html += "<td><span class='Fi-Criteria Sym'>" + Sym + "</span></td>";
    html += "<td><span class='Fi-Criteria Fls'>" + Fls + "</span></td>";
    html += "<td><span class='Fi-Criteria Lab'>" + Lab + "</span></td>";
    html += "<td><span class='Fi-Criteria FromLength'>" + FromLength + "</span></td>";
    html += "<td><span class='Fi-Criteria ToLength'>" + ToLength + "</span></td>";
    html += "<td><span class='Fi-Criteria FromWidth'>" + FromWidth + "</span></td>";
    html += "<td><span class='Fi-Criteria ToWidth'>" + ToWidth + "</span></td>";
    html += "<td><span class='Fi-Criteria FromDepth'>" + FromDepth + "</span></td>";
    html += "<td><span class='Fi-Criteria ToDepth'>" + ToDepth + "</span></td>";
    html += "<td><span class='Fi-Criteria FromDepthinPer'>" + FromDepthinPer + "</span></td>";
    html += "<td><span class='Fi-Criteria ToDepthinPer'>" + ToDepthinPer + "</span></td>";
    html += "<td><span class='Fi-Criteria FromTableinPer'>" + FromTableinPer + "</span></td>";
    html += "<td><span class='Fi-Criteria ToTableinPer'>" + ToTableinPer + "</span></td>";
    html += "<td><span class='Fi-Criteria FromCrAng'>" + FromCrAng + "</span></td>";
    html += "<td><span class='Fi-Criteria ToCrAng'>" + ToCrAng + "</span></td>";
    html += "<td><span class='Fi-Criteria FromCrHt'>" + FromCrHt + "</span></td>";
    html += "<td><span class='Fi-Criteria ToCrHt'>" + ToCrHt + "</span></td>";
    html += "<td><span class='Fi-Criteria FromPavAng'>" + FromPavAng + "</span></td>";
    html += "<td><span class='Fi-Criteria ToPavAng'>" + ToPavAng + "</span></td>";
    html += "<td><span class='Fi-Criteria FromPavHt'>" + FromPavHt + "</span></td>";
    html += "<td><span class='Fi-Criteria ToPavHt'>" + ToPavHt + "</span></td>";
    html += "<td><span class='Fi-Criteria Keytosymbol'>" + Keytosymbol + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria dCheckKTS'>" + dCheckKTS + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria dUNCheckKTS'>" + dUNCheckKTS + "</span></td>";
    html += "<td><span class='Fi-Criteria BGM'>" + BGM + "</span></td>";
    html += "<td><span class='Fi-Criteria CrownBlack'>" + CrownBlack + "</span></td>";
    html += "<td><span class='Fi-Criteria TableBlack'>" + TableBlack + "</span></td>";
    html += "<td><span class='Fi-Criteria CrownWhite'>" + CrownWhite + "</span></td>";
    html += "<td><span class='Fi-Criteria TableWhite'>" + TableWhite + "</span></td>";
    html += "<td><span class='Fi-Criteria GoodsType'>" + GoodsType + "</span></td>";
    html += "<td><span class='Fi-Criteria Image'>" + Image + "</span></td>";
    html += "<td><span class='Fi-Criteria Video'>" + Video + "</span></td>";

    html += "<td style='display:none;'><span class='Fi-Criteria PricingMethod_1'>" + PricingMethod_1 + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria PricingSign_1'>" + PricingSign_1 + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria txtDisc_1_1'>" + txtDisc_1_1 + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria txtValue_1_1'>" + txtValue_1_1 + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria txtValue_1_2'>" + txtValue_1_2 + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria txtValue_1_3'>" + txtValue_1_3 + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria txtValue_1_4'>" + txtValue_1_4 + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria Chk_Speci_Additional_1'>" + Chk_Speci_Additional_1 + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria txtFromDate'>" + txtFromDate + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria txtToDate'>" + txtToDate + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria PricingMethod_2'>" + PricingMethod_2 + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria PricingSign_2'>" + PricingSign_2 + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria txtDisc_2_1'>" + txtDisc_2_1 + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria txtValue_2_1'>" + txtValue_2_1 + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria txtValue_2_2'>" + txtValue_2_2 + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria txtValue_2_3'>" + txtValue_2_3 + "</span></td>";
    html += "<td style='display:none;'><span class='Fi-Criteria txtValue_2_4'>" + txtValue_2_4 + "</span></td>";

    //html += "<td style='display:none;'><span class='Fi-Criteria PricingMethod_3'>" + PricingMethod_3 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria PricingSign_3'>" + PricingSign_3 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria txtDisc_3_1'>" + txtDisc_3_1 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_3_1'>" + txtValue_3_1 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_3_2'>" + txtValue_3_2 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_3_3'>" + txtValue_3_3 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_3_4'>" + txtValue_3_4 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria Chk_Speci_Additional_2'>" + Chk_Speci_Additional_2 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria txtFromDate1'>" + txtFromDate1 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria txtToDate1'>" + txtToDate1 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria PricingMethod_4'>" + PricingMethod_4 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria PricingSign_4'>" + PricingSign_4 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria txtDisc_4_1'>" + txtDisc_4_1 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_4_1'>" + txtValue_4_1 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_4_2'>" + txtValue_4_2 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_4_3'>" + txtValue_4_3 + "</span></td>";
    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_4_4'>" + txtValue_4_4 + "</span></td>";

    html += "<td><span class='Fi-Criteria _PricingMethod_1'>" + (PricingMethod_1 == "Disc" ? 'Discount' : 'Value') + "</span></td>";
    html += "<td><span class='Fi-Criteria _PricingSign_1'>" + PricingSign_1 + "</span></td>";

    //html += "<td><span class='Fi-Criteria _PricingAmt_1'>" + (PricingMethod_1 == "Disc" ? txtDisc_1_1 : txtValue_1_1 + ', ' + txtValue_1_2 + ', ' + txtValue_1_3 + ', ' + txtValue_1_4) + "</span></td>";
    html += "<td>";
    html += "<div class='Pricing_1'>";
    if (PricingMethod_1 == "Disc") {
        html += (txtDisc_1_1 == "" && txtDisc_1_1 != "0" ? '<span class="Fi-Criteria _txtDisc_1_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtDisc_1_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtDisc_1_1 + '</span>');
    }
    else if (PricingMethod_1 == "Value") {
        html += (txtValue_1_1 == "" && txtValue_1_1 != "0" ? '<span class="Fi-Criteria _txtValue_1_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_1_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_1_1 + '</span>');
        html += (txtValue_1_2 == "" && txtValue_1_2 != "0" ? '<span class="Fi-Criteria _txtValue_1_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_1_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_1_2 + '</span>');
        html += (txtValue_1_3 == "" && txtValue_1_3 != "0" ? '<span class="Fi-Criteria _txtValue_1_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_1_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_1_3 + '</span>');
        html += (txtValue_1_4 == "" && txtValue_1_4 != "0" ? '<span class="Fi-Criteria _txtValue_1_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_1_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_1_4 + '</span>');
    }
    html += "</div>";
    html += "</td>";

    html += "<td><span class='Fi-Criteria _PricingAddi_2'>" + (PricingMethod_2 != "" ? (Chk_Speci_Additional_1 == true ? 'Yes' : 'No') : '') + "</span></td>";
    html += "<td><span class='Fi-Criteria _PricingFromDate_2'>" + (PricingMethod_2 != "" ? txtFromDate : '') + "</span></td>";
    html += "<td><span class='Fi-Criteria _PricingToDate_2'>" + (PricingMethod_2 != "" ? txtToDate : '') + "</span></td>";
    html += "<td><span class='Fi-Criteria _PricingMethod_2'>" + (PricingMethod_2 != "" ? (PricingMethod_2 == "Disc" ? 'Discount' : 'Value') : '') + "</span></td>";
    html += "<td><span class='Fi-Criteria _PricingSign_2'>" + (PricingMethod_2 != "" ? PricingSign_2 : '') + "</span></td>";

    //html += "<td><span class='Fi-Criteria _PricingAmt_2'>" + (PricingMethod_2 != "" ? (PricingMethod_2 == "Disc" ? txtDisc_2_1 : txtValue_2_1 + ', ' + txtValue_2_2 + ', ' + txtValue_2_3 + ', ' + txtValue_2_4) : '') + "</span></td>";
    html += "<td>";
    html += "<div class='Pricing_2'>";
    if (PricingMethod_2 == "Disc") {
        html += (txtDisc_2_1 == "" && txtDisc_2_1 != "0" ? '<span class="Fi-Criteria _txtDisc_2_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtDisc_2_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtDisc_2_1 + '</span>');
    }
    else if (PricingMethod_2 == "Value") {
        html += (txtValue_2_1 == "" && txtValue_2_1 != "0" ? '<span class="Fi-Criteria _txtValue_2_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_2_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_2_1 + '</span>');
        html += (txtValue_2_2 == "" && txtValue_2_2 != "0" ? '<span class="Fi-Criteria _txtValue_2_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_2_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_2_2 + '</span>');
        html += (txtValue_2_3 == "" && txtValue_2_3 != "0" ? '<span class="Fi-Criteria _txtValue_2_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_2_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_2_3 + '</span>');
        html += (txtValue_2_4 == "" && txtValue_2_4 != "0" ? '<span class="Fi-Criteria _txtValue_2_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_2_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_2_4 + '</span>');
    }
    html += "</div>";
    html += "</td>";


    //html += "<td><span class='Fi-Criteria _PricingMethod_3'>" + (PricingMethod_3 == "Disc" ? 'Discount' : 'Value') + "</span></td>";
    //html += "<td><span class='Fi-Criteria _PricingSign_3'>" + PricingSign_3 + "</span></td>";

    ////html += "<td><span class='Fi-Criteria _PricingAmt_3'>" + (PricingMethod_3 == "Disc" ? txtDisc_3_1 : txtValue_3_1 + ', ' + txtValue_3_2 + ', ' + txtValue_3_3 + ', ' + txtValue_3_4) + "</span></td>";
    //html += "<td>";
    //html += "<div class='Pricing_3'>";
    //if (PricingMethod_3 == "Disc") {
    //    html += (txtDisc_3_1 == "" ? '<span class="Fi-Criteria _txtDisc_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtDisc_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtDisc_3_1 + '</span>');
    //}
    //else if (PricingMethod_3 == "Value") {
    //    html += (txtValue_3_1 == "" ? '<span class="Fi-Criteria _txtValue_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_3_1 + '</span>');
    //    html += (txtValue_3_2 == "" ? '<span class="Fi-Criteria _txtValue_3_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_3_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_3_2 + '</span>');
    //    html += (txtValue_3_3 == "" ? '<span class="Fi-Criteria _txtValue_3_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_3_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_3_3 + '</span>');
    //    html += (txtValue_3_4 == "" ? '<span class="Fi-Criteria _txtValue_3_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_3_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_3_4 + '</span>');
    //}
    //html += "</div>";
    //html += "</td>";

    //html += "<td><span class='Fi-Criteria _PricingAddi_4'>" + (PricingMethod_4 != "" ? (Chk_Speci_Additional_2 == true ? 'Yes' : 'No') : '') + "</span></td>";
    //html += "<td><span class='Fi-Criteria _PricingFromDate_4'>" + (PricingMethod_4 != "" ? txtFromDate1 : '') + "</span></td>";
    //html += "<td><span class='Fi-Criteria _PricingToDate_4'>" + (PricingMethod_4 != "" ? txtToDate1 : '') + "</span></td>";
    //html += "<td><span class='Fi-Criteria _PricingMethod_4'>" + (PricingMethod_4 != "" ? (PricingMethod_4 == "Disc" ? 'Discount' : 'Value') : '') + "</span></td>";
    //html += "<td><span class='Fi-Criteria _PricingSign_4'>" + (PricingMethod_4 != "" ? PricingSign_4 : '') + "</span></td>";

    ////html += "<td><span class='Fi-Criteria _PricingAmt_4'>" + (PricingMethod_4 != "" ? (PricingMethod_4 == "Disc" ? txtDisc_4_1 : txtValue_4_1 + ', ' + txtValue_4_2 + ', ' + txtValue_4_3 + ', ' + txtValue_4_4) : '') + "</span></td>";
    //html += "<td>";
    //html += "<div class='Pricing_4'>";
    //if (PricingMethod_4 == "Disc") {
    //    html += (txtDisc_4_1 == "" ? '<span class="Fi-Criteria _txtDisc_4_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtDisc_4_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtDisc_4_1 + '</span>');
    //}
    //else if (PricingMethod_4 == "Value") {
    //    html += (txtValue_4_1 == "" ? '<span class="Fi-Criteria _txtValue_4_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_4_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_4_1 + '</span>');
    //    html += (txtValue_4_2 == "" ? '<span class="Fi-Criteria _txtValue_4_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_4_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_4_2 + '</span>');
    //    html += (txtValue_4_3 == "" ? '<span class="Fi-Criteria _txtValue_4_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_4_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_4_3 + '</span>');
    //    html += (txtValue_4_4 == "" ? '<span class="Fi-Criteria _txtValue_4_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_4_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_4_4 + '</span>');
    //}
    //html += "</div>";
    //html += "</td>";


    html += "<td style='width: 50px'>";
    html += '<input type="hidden" class="hdn_UniqueId" value="' + new_id + '" />';
    html += '<i onclick="EditCriteria(\'' + new_id + '\');" style="cursor:pointer;" class="error EditCriteria"><img src="/Content/images/edit-icon.png" style="width: 23px;"/></i>';
    html += '&nbsp;&nbsp;<i style="cursor:pointer;" class="error RemoveCriteria"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;"/></i>';
    html += "</td>";

    html += "</tr>";

    return html;
}

var ErrorMsg = [];
var GetError = function () {
    ErrorMsg = [];

    if ($("#PricingMethod_1").val() == "") {
        ErrorMsg.push({
            'Error': "Please Select CUSTOMER COST DISC > COMMON DISC > Price Method.",
        });
    }
    else {
        if ($("#PricingMethod_1").val() == "Disc") {
            if ($("#txtDisc_1_1").val() == "") {
                ErrorMsg.push({
                    'Error': "Please Enter CUSTOMER COST DISC > COMMON DISC > Discount.",
                });
            }
        }
        else if ($("#PricingMethod_1").val() == "Value") {
            if ($("#txtValue_1_1").val() == "" && $("#txtValue_1_2").val() == "" && $("#txtValue_1_3").val() == "" && $("#txtValue_1_4").val() == "") {
                ErrorMsg.push({
                    'Error': "Please Enter CUSTOMER COST DISC > COMMON DISC > Value.",
                });
            }
        }
    }
    if (document.getElementById("Chk_Speci_Additional_1").checked == true) {
        if ($("#PricingMethod_2").val() == "") {
            ErrorMsg.push({
                'Error': "Please Select CUSTOMER COST DISC > SPECIAL DISC > Price Method.",
            });
        }
    }

    if ($("#PricingMethod_2").val() != "") {
        if ($("#PricingMethod_2").val() == "Disc") {
            if ($("#txtDisc_2_1").val() == "") {
                ErrorMsg.push({
                    'Error': "Please Enter CUSTOMER COST DISC > SPECIAL DISC > Discount.",
                });
            }
        }
        else if ($("#PricingMethod_2").val() == "Value") {
            if ($("#txtValue_2_1").val() == "" && $("#txtValue_2_2").val() == "" && $("#txtValue_2_3").val() == "" && $("#txtValue_2_4").val() == "") {
                ErrorMsg.push({
                    'Error': "Please Enter CUSTOMER COST DISC > SPECIAL DISC > Value.",
                });
            }
        }
    }

    //if ($("#PricingMethod_3").val() == "") {
    //    ErrorMsg.push({
    //        'Error': "Please Select MAX DISC > COMMON DISC > Price Method.",
    //    });
    //}
    //else {
    //    if ($("#PricingMethod_3").val() == "Disc") {
    //        if ($("#txtDisc_3_1").val() == "") {
    //            ErrorMsg.push({
    //                'Error': "Please Enter MAX DISC > COMMON DISC > Discount.",
    //            });
    //        }
    //    }
    //    else if ($("#PricingMethod_3").val() == "Value") {
    //        if ($("#txtValue_3_1").val() == "" && $("#txtValue_3_2").val() == "" && $("#txtValue_3_3").val() == "" && $("#txtValue_3_4").val() == "") {
    //            ErrorMsg.push({
    //                'Error': "Please Enter MAX DISC > COMMON DISC > Value.",
    //            });
    //        }
    //    }
    //}

    //if (document.getElementById("Chk_Speci_Additional_2").checked == true) {
    //    if ($("#PricingMethod_4").val() == "") {
    //        ErrorMsg.push({
    //            'Error': "Please Select MAX DISC > SPECIAL DISC > Price Method.",
    //        });
    //    }
    //}

    //if ($("#PricingMethod_4").val() != "") {
    //    if ($("#PricingMethod_4").val() == "Disc") {
    //        if ($("#txtDisc_4_1").val() == "") {
    //            ErrorMsg.push({
    //                'Error': "Please Enter MAX DISC > SPECIAL DISC > Discount.",
    //            });
    //        }
    //    }
    //    else if ($("#PricingMethod_4").val() == "Value") {
    //        if ($("#txtValue_4_1").val() == "" && $("#txtValue_4_2").val() == "" && $("#txtValue_4_3").val() == "" && $("#txtValue_4_4").val() == "") {
    //            ErrorMsg.push({
    //                'Error': "Please Enter MAX DISC > SPECIAL DISC > Value.",
    //            });
    //        }
    //    }
    //}

    return ErrorMsg;
}
var AddNewRow = function () {
    if (EditCriteria_UniqueId == "") {
        ErrorMsg = GetError();
        if (ErrorMsg.length > 0) {
            $("#divError").empty();
            ErrorMsg.forEach(function (item) {
                $("#divError").append('<li>' + item.Error + '</li>');
            });
            $("#ErrorModel").modal("show");
        }
        else {
            var new_id = generate_uuidv4();

            var Supplier = _.pluck(_.filter(SupplierList, function (e) { return e.isActive == true }), 'Value').join(",");
            var Shape = _.pluck(_.filter(ShapeList, function (e) { return e.isActive == true }), 'Value').join(",");
            var Carat = _.pluck(_.filter(_pointerlst, function (e) { return e.isActive == true }), 'Value').join(",");
            var Color_Type = (Regular_All == true ? "Regular" : (Fancy_All == true ? "Fancy" : ""));
            var Color = _.pluck(_.filter(ColorList, function (e) { return e.isActive == true }), 'Value').join(",");
            var F_INTENSITY = _.pluck(_.filter(Check_Color_1), 'Symbol').join(",");
            var F_OVERTONE = _.pluck(_.filter(Check_Color_2), 'Symbol').join(",");
            var F_FANCY_COLOR = _.pluck(_.filter(Check_Color_3), 'Symbol').join(",");
            var MixColor = "";
            if (Color != "") {
                MixColor = Color;
            }
            else if (FC != "") {
                MixColor = FC;
            }
            if (Color_Type != "") {
                MixColor = (Color_Type == "Regular" ? "<b>REGULAR ALL</b>" : Color_Type == "Fancy" ? "<b>FANCY ALL</b>" : "");
            }
            var Clarity = _.pluck(_.filter(ClarityList, function (e) { return e.isActive == true }), 'Value').join(",");
            var Cut = _.pluck(_.filter(CutList, function (e) { return e.isActive == true }), 'Value').join(",");
            var Polish = _.pluck(_.filter(PolishList, function (e) { return e.isActive == true }), 'Value').join(",");
            var Sym = _.pluck(_.filter(SymList, function (e) { return e.isActive == true }), 'Value').join(",");
            var Fls = _.pluck(_.filter(FlsList, function (e) { return e.isActive == true }), 'Value').join(",");
            var Lab = _.pluck(_.filter(LabList, function (e) { return e.isActive == true }), 'Value').join(",");
            var FromLength = NullReplaceDecimalToFixed($("#FromLength").val());
            var ToLength = NullReplaceDecimalToFixed($("#ToLength").val());
            var FromWidth = NullReplaceDecimalToFixed($("#FromWidth").val());
            var ToWidth = NullReplaceDecimalToFixed($("#ToWidth").val());
            var FromDepth = NullReplaceDecimalToFixed($("#FromDepth").val());
            var ToDepth = NullReplaceDecimalToFixed($("#ToDepth").val());
            var FromDepthinPer = NullReplaceDecimalToFixed($("#FromDepthPer").val());
            var ToDepthinPer = NullReplaceDecimalToFixed($("#ToDepthPer").val());
            var FromTableinPer = NullReplaceDecimalToFixed($("#FromTablePer").val());
            var ToTableinPer = NullReplaceDecimalToFixed($("#ToTablePer").val());
            var FromCrAng = NullReplaceDecimalToFixed($("#FromCrAng").val());
            var ToCrAng = NullReplaceDecimalToFixed($("#ToCrAng").val());
            var FromCrHt = NullReplaceDecimalToFixed($("#FromCrHt").val());
            var ToCrHt = NullReplaceDecimalToFixed($("#ToCrHt").val());
            var FromPavAng = NullReplaceDecimalToFixed($("#FromPavAng").val());
            var ToPavAng = NullReplaceDecimalToFixed($("#ToPavAng").val());
            var FromPavHt = NullReplaceDecimalToFixed($("#FromPavHt").val());
            var ToPavHt = NullReplaceDecimalToFixed($("#ToPavHt").val());
            var KeyToSymLst_Check1 = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
            var KeyToSymLst_uncheck1 = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");
            var Keytosymbol = KeyToSymLst_Check1 + (KeyToSymLst_Check1 == "" || KeyToSymLst_uncheck1 == "" ? "" : "-") + KeyToSymLst_uncheck1;
            var dCheckKTS = KeyToSymLst_Check1;
            var dUNCheckKTS = KeyToSymLst_uncheck1;
            var BGM = _.pluck(_.filter(BGMList, function (e) { return e.isActive == true }), 'Value').join(",");
            var CrownBlack = _.pluck(_.filter(CrownBlackList, function (e) { return e.isActive == true }), 'Value').join(",");
            var TableBlack = _.pluck(_.filter(TableBlackList, function (e) { return e.isActive == true }), 'Value').join(",");
            var CrownWhite = _.pluck(_.filter(CrownWhiteList, function (e) { return e.isActive == true }), 'Value').join(",");
            var TableWhite = _.pluck(_.filter(TableWhiteList, function (e) { return e.isActive == true }), 'Value').join(",");
            var GoodsType = _.pluck(_.filter(GoodsTypeList, function (e) { return e.isActive == true }), 'Value').join(",");
            var Image = $('#Img:checked').val();
            var Video = $('#Vdo:checked').val();

            var PricingMethod_1 = $("#PricingMethod_1").val();
            var PricingSign_1 = $("#PricingSign_1").val();
            var txtDisc_1_1 = NullReplaceDecimal4ToFixed($("#txtDisc_1_1").val());
            var txtValue_1_1 = NullReplaceDecimal4ToFixed($("#txtValue_1_1").val());
            var txtValue_1_2 = NullReplaceDecimal4ToFixed($("#txtValue_1_2").val());
            var txtValue_1_3 = NullReplaceDecimal4ToFixed($("#txtValue_1_3").val());
            var txtValue_1_4 = NullReplaceDecimal4ToFixed($("#txtValue_1_4").val());
            var Chk_Speci_Additional_1 = document.getElementById("Chk_Speci_Additional_1").checked;
            var txtFromDate = $("#txtFromDate").val();
            var txtToDate = $("#txtToDate").val();
            var PricingMethod_2 = $("#PricingMethod_2").val();
            var PricingSign_2 = $("#PricingSign_2").val();
            var txtDisc_2_1 = NullReplaceDecimal4ToFixed($("#txtDisc_2_1").val());
            var txtValue_2_1 = NullReplaceDecimal4ToFixed($("#txtValue_2_1").val());
            var txtValue_2_2 = NullReplaceDecimal4ToFixed($("#txtValue_2_2").val());
            var txtValue_2_3 = NullReplaceDecimal4ToFixed($("#txtValue_2_3").val());
            var txtValue_2_4 = NullReplaceDecimal4ToFixed($("#txtValue_2_4").val());

            //var PricingMethod_3 = $("#PricingMethod_3").val();
            //var PricingSign_3 = $("#PricingSign_3").val();
            //var txtDisc_3_1 = NullReplaceDecimal4ToFixed($("#txtDisc_3_1").val());
            //var txtValue_3_1 = NullReplaceDecimal4ToFixed($("#txtValue_3_1").val());
            //var txtValue_3_2 = NullReplaceDecimal4ToFixed($("#txtValue_3_2").val());
            //var txtValue_3_3 = NullReplaceDecimal4ToFixed($("#txtValue_3_3").val());
            //var txtValue_3_4 = NullReplaceDecimal4ToFixed($("#txtValue_3_4").val());
            //var Chk_Speci_Additional_2 = document.getElementById("Chk_Speci_Additional_2").checked;
            //var txtFromDate1 = $("#txtFromDate1").val();
            //var txtToDate1 = $("#txtToDate1").val();
            //var PricingMethod_4 = $("#PricingMethod_4").val();
            //var PricingSign_4 = $("#PricingSign_4").val();
            //var txtDisc_4_1 = NullReplaceDecimal4ToFixed($("#txtDisc_4_1").val());
            //var txtValue_4_1 = NullReplaceDecimal4ToFixed($("#txtValue_4_1").val());
            //var txtValue_4_2 = NullReplaceDecimal4ToFixed($("#txtValue_4_2").val());
            //var txtValue_4_3 = NullReplaceDecimal4ToFixed($("#txtValue_4_3").val());
            //var txtValue_4_4 = NullReplaceDecimal4ToFixed($("#txtValue_4_4").val());


            //var html = "<tr class='tr11'>";
            //html += "<th class='Row Fi-Criteria' style=''></th>";
            //html += "<td><span class='Fi-Criteria Shape'>" + Shape + "</span></td>";
            //html += "<td><span class='Fi-Criteria Carat'>" + Carat + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria ColorType'>" + Color_Type + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria Color'>" + Color + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria dCheckINTENSITY'>" + F_INTENSITY + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria dCheckOVERTONE'>" + F_OVERTONE + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria dCheckFANCY_COLOR'>" + F_FANCY_COLOR + "</span></td>";
            //html += "<td><span class='Fi-Criteria MixColor'>" + MixColor + "</span></td>";
            //html += "<td><span class='Fi-Criteria Clarity'>" + Clarity + "</span></td>";
            //html += "<td><span class='Fi-Criteria Cut'>" + Cut + "</span></td>";
            //html += "<td><span class='Fi-Criteria Polish'>" + Polish + "</span></td>";
            //html += "<td><span class='Fi-Criteria Sym'>" + Sym + "</span></td>";
            //html += "<td><span class='Fi-Criteria Fls'>" + Fls + "</span></td>";
            //html += "<td><span class='Fi-Criteria Lab'>" + Lab + "</span></td>";
            //html += "<td><span class='Fi-Criteria FromLength'>" + FromLength + "</span></td>";
            //html += "<td><span class='Fi-Criteria ToLength'>" + ToLength + "</span></td>";
            //html += "<td><span class='Fi-Criteria FromWidth'>" + FromWidth + "</span></td>";
            //html += "<td><span class='Fi-Criteria ToWidth'>" + ToWidth + "</span></td>";
            //html += "<td><span class='Fi-Criteria FromDepth'>" + FromDepth + "</span></td>";
            //html += "<td><span class='Fi-Criteria ToDepth'>" + ToDepth + "</span></td>";
            //html += "<td><span class='Fi-Criteria FromDepthinPer'>" + FromDepthinPer + "</span></td>";
            //html += "<td><span class='Fi-Criteria ToDepthinPer'>" + ToDepthinPer + "</span></td>";
            //html += "<td><span class='Fi-Criteria FromTableinPer'>" + FromTableinPer + "</span></td>";
            //html += "<td><span class='Fi-Criteria ToTableinPer'>" + ToTableinPer + "</span></td>";
            //html += "<td><span class='Fi-Criteria FromCrAng'>" + FromCrAng + "</span></td>";
            //html += "<td><span class='Fi-Criteria ToCrAng'>" + ToCrAng + "</span></td>";
            //html += "<td><span class='Fi-Criteria FromCrHt'>" + FromCrHt + "</span></td>";
            //html += "<td><span class='Fi-Criteria ToCrHt'>" + ToCrHt + "</span></td>";
            //html += "<td><span class='Fi-Criteria FromPavAng'>" + FromPavAng + "</span></td>";
            //html += "<td><span class='Fi-Criteria ToPavAng'>" + ToPavAng + "</span></td>";
            //html += "<td><span class='Fi-Criteria FromPavHt'>" + FromPavHt + "</span></td>";
            //html += "<td><span class='Fi-Criteria ToPavHt'>" + ToPavHt + "</span></td>";
            //html += "<td><span class='Fi-Criteria Keytosymbol'>" + Keytosymbol + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria dCheckKTS'>" + dCheckKTS + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria dUNCheckKTS'>" + dUNCheckKTS + "</span></td>";
            //html += "<td><span class='Fi-Criteria BGM'>" + BGM + "</span></td>";
            //html += "<td><span class='Fi-Criteria CrownBlack'>" + CrownBlack + "</span></td>";
            //html += "<td><span class='Fi-Criteria TableBlack'>" + TableBlack + "</span></td>";
            //html += "<td><span class='Fi-Criteria CrownWhite'>" + CrownWhite + "</span></td>";
            //html += "<td><span class='Fi-Criteria TableWhite'>" + TableWhite + "</span></td>";
            //html += "<td><span class='Fi-Criteria GoodsType'>" + GoodsType + "</span></td>";
            //html += "<td><span class='Fi-Criteria Image'>" + Image + "</span></td>";
            //html += "<td><span class='Fi-Criteria Video'>" + Video + "</span></td>";

            //html += "<td style='display:none;'><span class='Fi-Criteria PricingMethod_1'>" + PricingMethod_1 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria PricingSign_1'>" + PricingSign_1 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtDisc_1_1'>" + txtDisc_1_1 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_1_1'>" + txtValue_1_1 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_1_2'>" + txtValue_1_2 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_1_3'>" + txtValue_1_3 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_1_4'>" + txtValue_1_4 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria Chk_Speci_Additional_1'>" + Chk_Speci_Additional_1 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtFromDate'>" + txtFromDate + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtToDate'>" + txtToDate + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria PricingMethod_2'>" + PricingMethod_2 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria PricingSign_2'>" + PricingSign_2 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtDisc_2_1'>" + txtDisc_2_1 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_2_1'>" + txtValue_2_1 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_2_2'>" + txtValue_2_2 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_2_3'>" + txtValue_2_3 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_2_4'>" + txtValue_2_4 + "</span></td>";

            //html += "<td style='display:none;'><span class='Fi-Criteria PricingMethod_3'>" + PricingMethod_3 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria PricingSign_3'>" + PricingSign_3 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtDisc_3_1'>" + txtDisc_3_1 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_3_1'>" + txtValue_3_1 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_3_2'>" + txtValue_3_2 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_3_3'>" + txtValue_3_3 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_3_4'>" + txtValue_3_4 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria Chk_Speci_Additional_2'>" + Chk_Speci_Additional_2 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtFromDate1'>" + txtFromDate1 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtToDate1'>" + txtToDate1 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria PricingMethod_4'>" + PricingMethod_4 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria PricingSign_4'>" + PricingSign_4 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtDisc_4_1'>" + txtDisc_4_1 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_4_1'>" + txtValue_4_1 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_4_2'>" + txtValue_4_2 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_4_3'>" + txtValue_4_3 + "</span></td>";
            //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_4_4'>" + txtValue_4_4 + "</span></td>";

            //html += "<td><span class='Fi-Criteria _PricingMethod_1'>" + (PricingMethod_1 == "Disc" ? 'Discount' : 'Value') + "</span></td>";
            //html += "<td><span class='Fi-Criteria _PricingSign_1'>" + PricingSign_1 + "</span></td>";

            ////html += "<td><span class='Fi-Criteria _PricingAmt_1'>" + (PricingMethod_1 == "Disc" ? txtDisc_1_1 : txtValue_1_1 + ', ' + txtValue_1_2 + ', ' + txtValue_1_3 + ', ' + txtValue_1_4) + "</span></td>";
            //html += "<td>";
            //html += "<div>";
            //if (PricingMethod_1 == "Disc") {
            //    html += (txtDisc_1_1 == "" ? '<span class="Fi-Criteria _txtDisc_1_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtDisc_1_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtDisc_1_1 + '</span>');
            //}
            //else if (PricingMethod_1 == "Value") {
            //    html += (txtValue_1_1 == "" ? '<span class="Fi-Criteria _txtValue_1_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_1_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_1_1 + '</span>');
            //    html += (txtValue_1_2 == "" ? '<span class="Fi-Criteria _txtValue_1_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_1_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_1_2 + '</span>');
            //    html += (txtValue_1_3 == "" ? '<span class="Fi-Criteria _txtValue_1_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_1_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_1_3 + '</span>');
            //    html += (txtValue_1_4 == "" ? '<span class="Fi-Criteria _txtValue_1_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_1_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_1_4 + '</span>');
            //}
            //html += "</div>";
            //html += "</td>";

            //html += "<td><span class='Fi-Criteria _PricingAddi_2'>" + (PricingMethod_2 != "" ? (Chk_Speci_Additional_1 == true ? 'Yes' : 'No') : '') + "</span></td>";
            //html += "<td><span class='Fi-Criteria _PricingFromDate_2'>" + (PricingMethod_2 != "" ? txtFromDate : '') + "</span></td>";
            //html += "<td><span class='Fi-Criteria _PricingToDate_2'>" + (PricingMethod_2 != "" ? txtToDate : '') + "</span></td>";
            //html += "<td><span class='Fi-Criteria _PricingMethod_2'>" + (PricingMethod_2 != "" ? (PricingMethod_2 == "Disc" ? 'Discount' : 'Value') : '') + "</span></td>";
            //html += "<td><span class='Fi-Criteria _PricingSign_2'>" + (PricingMethod_2 != "" ? PricingSign_2 : '') + "</span></td>";

            ////html += "<td><span class='Fi-Criteria _PricingAmt_2'>" + (PricingMethod_2 != "" ? (PricingMethod_2 == "Disc" ? txtDisc_2_1 : txtValue_2_1 + ', ' + txtValue_2_2 + ', ' + txtValue_2_3 + ', ' + txtValue_2_4) : '') + "</span></td>";
            //html += "<td>";
            //if (PricingMethod_2 != "") {
            //    html += "<div>";
            //    if (PricingMethod_2 == "Disc") {
            //        html += (txtDisc_1_1 == "" ? '<span class="Fi-Criteria _txtDisc_2_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtDisc_2_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtDisc_2_1 + '</span>');
            //    }
            //    else if (PricingMethod_2 == "Value") {
            //        html += (txtValue_2_1 == "" ? '<span class="Fi-Criteria _txtValue_2_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_2_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_2_1 + '</span>');
            //        html += (txtValue_2_2 == "" ? '<span class="Fi-Criteria _txtValue_2_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_2_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_2_2 + '</span>');
            //        html += (txtValue_2_2 == "" ? '<span class="Fi-Criteria _txtValue_2_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_2_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_2_2 + '</span>');
            //        html += (txtValue_2_4 == "" ? '<span class="Fi-Criteria _txtValue_2_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_2_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_2_4 + '</span>');
            //    }
            //    html += "</div>";
            //}
            //html += "</td>";


            //html += "<td><span class='Fi-Criteria _PricingMethod_3'>" + (PricingMethod_3 == "Disc" ? 'Discount' : 'Value') + "</span></td>";
            //html += "<td><span class='Fi-Criteria _PricingSign_3'>" + PricingSign_3 + "</span></td>";

            ////html += "<td><span class='Fi-Criteria _PricingAmt_3'>" + (PricingMethod_3 == "Disc" ? txtDisc_3_1 : txtValue_3_1 + ', ' + txtValue_3_2 + ', ' + txtValue_3_3 + ', ' + txtValue_3_4) + "</span></td>";
            //html += "<td>";
            //html += "<div>";
            //if (PricingMethod_3 == "Disc") {
            //    html += (txtDisc_3_1 == "" ? '<span class="Fi-Criteria _txtDisc_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtDisc_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtDisc_3_1 + '</span>');
            //}
            //else if (PricingMethod_3 == "Value") {
            //    html += (txtValue_3_1 == "" ? '<span class="Fi-Criteria _txtValue_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_3_1 + '</span>');
            //    html += (txtValue_3_1 == "" ? '<span class="Fi-Criteria _txtValue_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_3_1 + '</span>');
            //    html += (txtValue_3_1 == "" ? '<span class="Fi-Criteria _txtValue_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_3_1 + '</span>');
            //    html += (txtValue_3_1 == "" ? '<span class="Fi-Criteria _txtValue_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_3_1 + '</span>');
            //}
            //html += "</div>";
            //html += "</td>";

            //html += "<td><span class='Fi-Criteria _PricingAddi_4'>" + (PricingMethod_4 != "" ? (Chk_Speci_Additional_2 == true ? 'Yes' : 'No') : '') + "</span></td>";
            //html += "<td><span class='Fi-Criteria _PricingFromDate_4'>" + (PricingMethod_4 != "" ? txtFromDate1 : '') + "</span></td>";
            //html += "<td><span class='Fi-Criteria _PricingToDate_4'>" + (PricingMethod_4 != "" ? txtToDate1 : '') + "</span></td>";
            //html += "<td><span class='Fi-Criteria _PricingMethod_4'>" + (PricingMethod_4 != "" ? (PricingMethod_4 == "Disc" ? 'Discount' : 'Value') : '') + "</span></td>";
            //html += "<td><span class='Fi-Criteria _PricingSign_4'>" + (PricingMethod_4 != "" ? PricingSign_4 : '') + "</span></td>";

            ////html += "<td><span class='Fi-Criteria _PricingAmt_4'>" + (PricingMethod_4 != "" ? (PricingMethod_4 == "Disc" ? txtDisc_4_1 : txtValue_4_1 + ', ' + txtValue_4_2 + ', ' + txtValue_4_3 + ', ' + txtValue_4_4) : '') + "</span></td>";
            //html += "<td>";
            //if (PricingMethod_4 != "") {
            //    html += "<div>";
            //    if (PricingMethod_4 == "Disc") {
            //        html += (txtDisc_4_1 == "" ? '<span class="Fi-Criteria _txtDisc_4_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtDisc_4_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtDisc_4_1 + '</span>');
            //    }
            //    else if (PricingMethod_4 == "Value") {
            //        html += (txtValue_4_1 == "" ? '<span class="Fi-Criteria _txtValue_4_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_4_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_4_1 + '</span>');
            //        html += (txtValue_4_2 == "" ? '<span class="Fi-Criteria _txtValue_4_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_4_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_4_2 + '</span>');
            //        html += (txtValue_4_3 == "" ? '<span class="Fi-Criteria _txtValue_4_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_4_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_4_3 + '</span>');
            //        html += (txtValue_4_4 == "" ? '<span class="Fi-Criteria _txtValue_4_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_4_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_4_4 + '</span>');
            //    }
            //    html += "</div>";
            //}
            //html += "</td>";


            //html += "<td style='width: 50px'>";
            //html += '<input type="hidden" class="hdn_UniqueId" value="' + new_id + '" />';
            //html += '<i onclick="EditCriteria(\'' + new_id + '\');" style="cursor:pointer;" class="error EditCriteria"><img src="/Content/images/edit-icon.png" style="width: 23px;"/></i>';
            //html += '&nbsp;&nbsp;<i style="cursor:pointer;" class="error RemoveCriteria"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;"/></i>';
            //html += "</td>";

            //html += "</tr>";

            var html = HTML_CREATE(Supplier, Shape, Carat, Color_Type, Color, F_INTENSITY, F_OVERTONE, F_FANCY_COLOR, MixColor, Clarity, Cut, Polish, Sym, Fls, Lab,
                FromLength, ToLength, FromWidth, ToWidth, FromDepth, ToDepth, FromDepthinPer, ToDepthinPer, FromTableinPer, ToTableinPer, FromCrAng, ToCrAng,
                FromCrHt, ToCrHt, FromPavAng, ToPavAng, FromPavHt, ToPavHt,
                Keytosymbol, dCheckKTS, dUNCheckKTS, BGM, CrownBlack, TableBlack, CrownWhite, TableWhite, GoodsType, Image, Video,
                PricingMethod_1, PricingSign_1, txtDisc_1_1, txtValue_1_1, txtValue_1_2, txtValue_1_3, txtValue_1_4,
                Chk_Speci_Additional_1, txtFromDate, txtToDate,
                PricingMethod_2, PricingSign_2, txtDisc_2_1, txtValue_2_1, txtValue_2_2, txtValue_2_3, txtValue_2_4,
                new_id);

            $("#tblFilters #tblBodyFilters").append(html);
            $("#tblFilters").show();
            $("#lblCustNoFound").hide();

            var id1 = 1;
            $("#tblFilters #tblBodyFilters tr").each(function () {
                $(this).find("th:eq(0)").html(id1);
                id1 += 1;
            });

            Reset_API_Filter();
        }
    }
    else {
        UpdateRow();
    }
}
function UpdateRow() {
    ErrorMsg = GetError();
    if (ErrorMsg.length > 0) {
        $("#divError").empty();
        ErrorMsg.forEach(function (item) {
            $("#divError").append('<li>' + item.Error + '</li>');
        });
        $("#ErrorModel").modal("show");
    }
    else {
        $("#btn_Add_in_Stock_Disc").html('<i class="fa fa-plus"></i>Add Row');
        $(".UpdateCancelRow").hide();

        $("#tblFilters #tblBodyFilters tr").each(function () {
            if ($(this).find('.hdn_UniqueId').val() == EditCriteria_UniqueId) {
                var html = "";

                $(this)[0].className = '';
                $(this).find('.EditCriteria').show();
                $(this).find('.RemoveCriteria').show();

                var Supplier = _.pluck(_.filter(SupplierList, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.Supplier').html(Supplier);

                var Shape = _.pluck(_.filter(ShapeList, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.Shape').html(Shape);

                var Carat = _.pluck(_.filter(_pointerlst, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.Carat').html(Carat);

                var Color_Type = (Regular_All == true ? "Regular" : (Fancy_All == true ? "Fancy" : ""));
                var Color = _.pluck(_.filter(ColorList, function (e) { return e.isActive == true }), 'Value').join(",");
                var F_INTENSITY = _.pluck(_.filter(Check_Color_1), 'Symbol').join(",");
                var F_OVERTONE = _.pluck(_.filter(Check_Color_2), 'Symbol').join(",");
                var F_FANCY_COLOR = _.pluck(_.filter(Check_Color_3), 'Symbol').join(",");
                var MixColor = "";
                if (Color != "") {
                    MixColor = Color;
                }
                else if (FC != "") {
                    MixColor = FC;
                }
                if (Color_Type != "") {
                    MixColor = (Color_Type == "Regular" ? "<b>REGULAR ALL</b>" : Color_Type == "Fancy" ? "<b>FANCY ALL</b>" : "");
                }
                $(this).find('.ColorType').html(Color_Type);
                $(this).find('.Color').html(Color);
                $(this).find('.dCheckINTENSITY').html(F_INTENSITY);
                $(this).find('.dCheckOVERTONE').html(F_OVERTONE);
                $(this).find('.dCheckFANCY_COLOR').html(F_FANCY_COLOR);
                $(this).find('.MixColor').html(MixColor);

                var Clarity = _.pluck(_.filter(ClarityList, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.Clarity').html(Clarity);

                var Cut = _.pluck(_.filter(CutList, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.Cut').html(Cut);

                var Polish = _.pluck(_.filter(PolishList, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.Polish').html(Polish);

                var Sym = _.pluck(_.filter(SymList, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.Sym').html(Sym);

                var Fls = _.pluck(_.filter(FlsList, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.Fls').html(Fls);

                var Lab = _.pluck(_.filter(LabList, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.Lab').html(Lab);

                $(this).find('.FromLength').html(NullReplaceDecimalToFixed($("#FromLength").val()));
                $(this).find('.ToLength').html(NullReplaceDecimalToFixed($("#ToLength").val()));
                $(this).find('.FromWidth').html(NullReplaceDecimalToFixed($("#FromWidth").val()));
                $(this).find('.ToWidth').html(NullReplaceDecimalToFixed($("#ToWidth").val()));
                $(this).find('.FromDepth').html(NullReplaceDecimalToFixed($("#FromDepth").val()));
                $(this).find('.ToDepth').html(NullReplaceDecimalToFixed($("#ToDepth").val()));
                $(this).find('.FromDepthinPer').html(NullReplaceDecimalToFixed($("#FromDepthPer").val()));
                $(this).find('.ToDepthinPer').html(NullReplaceDecimalToFixed($("#ToDepthPer").val()));
                $(this).find('.FromTableinPer').html(NullReplaceDecimalToFixed($("#FromTablePer").val()));
                $(this).find('.ToTableinPer').html(NullReplaceDecimalToFixed($("#ToTablePer").val()));
                $(this).find('.FromCrAng').html(NullReplaceDecimalToFixed($("#FromCrAng").val()));
                $(this).find('.ToCrAng').html(NullReplaceDecimalToFixed($("#ToCrAng").val()));
                $(this).find('.FromCrHt').html(NullReplaceDecimalToFixed($("#FromCrHt").val()));
                $(this).find('.ToCrHt').html(NullReplaceDecimalToFixed($("#ToCrHt").val()));
                $(this).find('.FromPavAng').html(NullReplaceDecimalToFixed($("#FromPavAng").val()));
                $(this).find('.ToPavAng').html(NullReplaceDecimalToFixed($("#ToPavAng").val()));
                $(this).find('.FromPavHt').html(NullReplaceDecimalToFixed($("#FromPavHt").val()));
                $(this).find('.ToPavHt').html(NullReplaceDecimalToFixed($("#ToPavHt").val()));

                var KeyToSymLst_Check1 = _.pluck(CheckKeyToSymbolList, 'Symbol').join(",");
                var KeyToSymLst_uncheck1 = _.pluck(UnCheckKeyToSymbolList, 'Symbol').join(",");
                var Keytosymbol = KeyToSymLst_Check1 + (KeyToSymLst_Check1 == "" || KeyToSymLst_uncheck1 == "" ? "" : "-") + KeyToSymLst_uncheck1;
                var dCheckKTS = KeyToSymLst_Check1;
                var dUNCheckKTS = KeyToSymLst_uncheck1;

                $(this).find('.Keytosymbol').html(Keytosymbol);
                $(this).find('.dCheckKTS').html(dCheckKTS);
                $(this).find('.dUNCheckKTS').html(dUNCheckKTS);

                var BGM = _.pluck(_.filter(BGMList, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.BGM').html(BGM);

                var CrownBlack = _.pluck(_.filter(CrownBlackList, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.CrownBlack').html(CrownBlack);

                var TableBlack = _.pluck(_.filter(TableBlackList, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.TableBlack').html(TableBlack);

                var CrownWhite = _.pluck(_.filter(CrownWhiteList, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.CrownWhite').html(CrownWhite);

                var TableWhite = _.pluck(_.filter(TableWhiteList, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.TableWhite').html(TableWhite);

                var GoodsType = _.pluck(_.filter(GoodsTypeList, function (e) { return e.isActive == true }), 'Value').join(",");
                $(this).find('.GoodsType').html(GoodsType);

                $(this).find('.Image').html($('#Img:checked').val());
                $(this).find('.Video').html($('#Vdo:checked').val());


                var PricingMethod_1 = $("#PricingMethod_1").val();
                var PricingSign_1 = $("#PricingSign_1").val();
                var txtDisc_1_1 = NullReplaceDecimal4ToFixed($("#txtDisc_1_1").val());
                var txtValue_1_1 = NullReplaceDecimal4ToFixed($("#txtValue_1_1").val());
                var txtValue_1_2 = NullReplaceDecimal4ToFixed($("#txtValue_1_2").val());
                var txtValue_1_3 = NullReplaceDecimal4ToFixed($("#txtValue_1_3").val());
                var txtValue_1_4 = NullReplaceDecimal4ToFixed($("#txtValue_1_4").val());
                var Chk_Speci_Additional_1 = document.getElementById("Chk_Speci_Additional_1").checked;
                var txtFromDate = $("#txtFromDate").val();
                var txtToDate = $("#txtToDate").val();
                var PricingMethod_2 = $("#PricingMethod_2").val();
                var PricingSign_2 = $("#PricingSign_2").val();
                var txtDisc_2_1 = NullReplaceDecimal4ToFixed($("#txtDisc_2_1").val());
                var txtValue_2_1 = NullReplaceDecimal4ToFixed($("#txtValue_2_1").val());
                var txtValue_2_2 = NullReplaceDecimal4ToFixed($("#txtValue_2_2").val());
                var txtValue_2_3 = NullReplaceDecimal4ToFixed($("#txtValue_2_3").val());
                var txtValue_2_4 = NullReplaceDecimal4ToFixed($("#txtValue_2_4").val());

                //var PricingMethod_3 = $("#PricingMethod_3").val();
                //var PricingSign_3 = $("#PricingSign_3").val();
                //var txtDisc_3_1 = NullReplaceDecimal4ToFixed($("#txtDisc_3_1").val());
                //var txtValue_3_1 = NullReplaceDecimal4ToFixed($("#txtValue_3_1").val());
                //var txtValue_3_2 = NullReplaceDecimal4ToFixed($("#txtValue_3_2").val());
                //var txtValue_3_3 = NullReplaceDecimal4ToFixed($("#txtValue_3_3").val());
                //var txtValue_3_4 = NullReplaceDecimal4ToFixed($("#txtValue_3_4").val());
                //var Chk_Speci_Additional_2 = document.getElementById("Chk_Speci_Additional_2").checked;
                //var txtFromDate1 = $("#txtFromDate1").val();
                //var txtToDate1 = $("#txtToDate1").val();
                //var PricingMethod_4 = $("#PricingMethod_4").val();
                //var PricingSign_4 = $("#PricingSign_4").val();
                //var txtDisc_4_1 = NullReplaceDecimal4ToFixed($("#txtDisc_4_1").val());
                //var txtValue_4_1 = NullReplaceDecimal4ToFixed($("#txtValue_4_1").val());
                //var txtValue_4_2 = NullReplaceDecimal4ToFixed($("#txtValue_4_2").val());
                //var txtValue_4_3 = NullReplaceDecimal4ToFixed($("#txtValue_4_3").val());
                //var txtValue_4_4 = NullReplaceDecimal4ToFixed($("#txtValue_4_4").val());

                $(this).find('.PricingMethod_1').html(PricingMethod_1);
                $(this).find('.PricingSign_1').html(PricingSign_1);
                $(this).find('.txtDisc_1_1').html(txtDisc_1_1);
                $(this).find('.txtValue_1_1').html(txtValue_1_1);
                $(this).find('.txtValue_1_2').html(txtValue_1_2);
                $(this).find('.txtValue_1_3').html(txtValue_1_3);
                $(this).find('.txtValue_1_4').html(txtValue_1_4);
                $(this).find('.Chk_Speci_Additional_1').html(Chk_Speci_Additional_1);
                $(this).find('.txtFromDate').html(txtFromDate);
                $(this).find('.txtToDate').html(txtToDate);
                $(this).find('.PricingMethod_2').html(PricingMethod_2);
                $(this).find('.PricingSign_2').html(PricingSign_2);
                $(this).find('.txtDisc_2_1').html(txtDisc_2_1);
                $(this).find('.txtValue_2_1').html(txtValue_2_1);
                $(this).find('.txtValue_2_2').html(txtValue_2_2);
                $(this).find('.txtValue_2_3').html(txtValue_2_3);
                $(this).find('.txtValue_2_4').html(txtValue_2_4);

                //$(this).find('.PricingMethod_3').html(PricingMethod_3);
                //$(this).find('.PricingSign_3').html(PricingSign_3);
                //$(this).find('.txtDisc_3_1').html(txtDisc_3_1);
                //$(this).find('.txtValue_3_1').html(txtValue_3_1);
                //$(this).find('.txtValue_3_2').html(txtValue_3_2);
                //$(this).find('.txtValue_3_3').html(txtValue_3_3);
                //$(this).find('.txtValue_3_4').html(txtValue_3_4);
                //$(this).find('.Chk_Speci_Additional_2').html(Chk_Speci_Additional_2);
                //$(this).find('.txtFromDate1').html(txtFromDate1);
                //$(this).find('.txtToDate1').html(txtToDate1);
                //$(this).find('.PricingMethod_4').html(PricingMethod_4);
                //$(this).find('.PricingSign_4').html(PricingSign_4);
                //$(this).find('.txtDisc_4_1').html(txtDisc_4_1);
                //$(this).find('.txtValue_4_1').html(txtValue_4_1);
                //$(this).find('.txtValue_4_2').html(txtValue_4_2);
                //$(this).find('.txtValue_4_3').html(txtValue_4_3);
                //$(this).find('.txtValue_4_4').html(txtValue_4_4);

                $(this).find('._PricingMethod_1').html((PricingMethod_1 == "Disc" ? 'Discount' : 'Value'));
                $(this).find('._PricingSign_1').html(PricingSign_1);
                //$(this).find('._PricingAmt_1').html((PricingMethod_1 == "Disc" ? txtDisc_1_1 : txtValue_1_1 + ', ' + txtValue_1_2 + ', ' + txtValue_1_3 + ', ' + txtValue_1_4));
                html = "";
                if (PricingMethod_1 == "Disc") {
                    html += (txtDisc_1_1 == "" && txtDisc_1_1 != "0" ? '<span class="Fi-Criteria _txtDisc_1_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtDisc_1_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtDisc_1_1 + '</span>');
                }
                else if (PricingMethod_1 == "Value") {
                    html += (txtValue_1_1 == "" && txtValue_1_1 != "0" ? '<span class="Fi-Criteria _txtValue_1_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_1_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_1_1 + '</span>');
                    html += (txtValue_1_2 == "" && txtValue_1_2 != "0" ? '<span class="Fi-Criteria _txtValue_1_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_1_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_1_2 + '</span>');
                    html += (txtValue_1_3 == "" && txtValue_1_3 != "0" ? '<span class="Fi-Criteria _txtValue_1_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_1_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_1_3 + '</span>');
                    html += (txtValue_1_4 == "" && txtValue_1_4 != "0" ? '<span class="Fi-Criteria _txtValue_1_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_1_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_1_4 + '</span>');
                }
                $(this).find('.Pricing_1').html(html);

                $(this).find('._PricingAddi_2').html((PricingMethod_2 != "" ? (Chk_Speci_Additional_1 == true ? 'Yes' : 'No') : ''));
                $(this).find('._PricingFromDate_2').html((PricingMethod_2 != "" ? txtFromDate : ''));
                $(this).find('._PricingToDate_2').html((PricingMethod_2 != "" ? txtToDate : ''));
                $(this).find('._PricingMethod_2').html((PricingMethod_2 != "" ? (PricingMethod_2 == "Disc" ? 'Discount' : 'Value') : ''));
                $(this).find('._PricingSign_2').html((PricingMethod_2 != "" ? PricingSign_2 : ''));
                //$(this).find('._PricingAmt_2').html((PricingMethod_2 != "" ? (PricingMethod_2 == "Disc" ? txtDisc_2_1 : txtValue_2_1 + ', ' + txtValue_2_2 + ', ' + txtValue_2_3 + ', ' + txtValue_2_4) : ''));
                html = "";
                if (PricingMethod_2 == "Disc") {
                    html += (txtDisc_2_1 == "" && txtDisc_2_1 != "0" ? '<span class="Fi-Criteria _txtDisc_2_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtDisc_2_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtDisc_2_1 + '</span>');
                }
                else if (PricingMethod_2 == "Value") {
                    html += (txtValue_2_1 == "" && txtValue_2_1 != "0" ? '<span class="Fi-Criteria _txtValue_2_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_2_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_2_1 + '</span>');
                    html += (txtValue_2_2 == "" && txtValue_2_2 != "0" ? '<span class="Fi-Criteria _txtValue_2_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_2_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_2_2 + '</span>');
                    html += (txtValue_2_3 == "" && txtValue_2_3 != "0" ? '<span class="Fi-Criteria _txtValue_2_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_2_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_2_3 + '</span>');
                    html += (txtValue_2_4 == "" && txtValue_2_4 != "0" ? '<span class="Fi-Criteria _txtValue_2_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_2_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_2_4 + '</span>');
                }
                $(this).find('.Pricing_2').html(html);

                //$(this).find('._PricingMethod_3').html((PricingMethod_3 == "Disc" ? 'Discount' : 'Value'));
                //$(this).find('._PricingSign_3').html(PricingSign_3);
                ////$(this).find('._PricingAmt_3').html((PricingMethod_3 == "Disc" ? txtDisc_3_1 : txtValue_3_1 + ', ' + txtValue_3_2 + ', ' + txtValue_3_3 + ', ' + txtValue_3_4));
                //html = "";
                //if (PricingMethod_3 == "Disc") {
                //    html += (txtDisc_3_1 == "" ? '<span class="Fi-Criteria _txtDisc_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtDisc_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtDisc_3_1 + '</span>');
                //}
                //else if (PricingMethod_3 == "Value") {
                //    html += (txtValue_3_1 == "" ? '<span class="Fi-Criteria _txtValue_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_3_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_3_1 + '</span>');
                //    html += (txtValue_3_2 == "" ? '<span class="Fi-Criteria _txtValue_3_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_3_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_3_2 + '</span>');
                //    html += (txtValue_3_3 == "" ? '<span class="Fi-Criteria _txtValue_3_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_3_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_3_3 + '</span>');
                //    html += (txtValue_3_4 == "" ? '<span class="Fi-Criteria _txtValue_3_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_3_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_3_4 + '</span>');
                //}
                //$(this).find('.Pricing_3').html(html);

                //$(this).find('._PricingAddi_4').html((PricingMethod_4 != "" ? (Chk_Speci_Additional_2 == true ? 'Yes' : 'No') : ''));
                //$(this).find('._PricingFromDate_4').html((PricingMethod_4 != "" ? txtFromDate1 : ''));
                //$(this).find('._PricingToDate_4').html((PricingMethod_4 != "" ? txtToDate1 : ''));
                //$(this).find('._PricingMethod_4').html((PricingMethod_4 != "" ? (PricingMethod_4 == "Disc" ? 'Discount' : 'Value') : ''));
                //$(this).find('._PricingSign_4').html((PricingMethod_4 != "" ? PricingSign_4 : ''));
                ////$(this).find('._PricingAmt_4').html((PricingMethod_4 != "" ? (PricingMethod_4 == "Disc" ? txtDisc_4_1 : txtValue_4_1 + ', ' + txtValue_4_2 + ', ' + txtValue_4_3 + ', ' + txtValue_4_4) : ''));
                //html = "";
                //if (PricingMethod_4 == "Disc") {
                //    html += (txtDisc_4_1 == "" ? '<span class="Fi-Criteria _txtDisc_4_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtDisc_4_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtDisc_4_1 + '</span>');
                //}
                //else if (PricingMethod_4 == "Value") {
                //    html += (txtValue_4_1 == "" ? '<span class="Fi-Criteria _txtValue_4_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_4_1" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_4_1 + '</span>');
                //    html += (txtValue_4_2 == "" ? '<span class="Fi-Criteria _txtValue_4_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_4_2" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_4_2 + '</span>');
                //    html += (txtValue_4_3 == "" ? '<span class="Fi-Criteria _txtValue_4_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_4_3" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_4_3 + '</span>');
                //    html += (txtValue_4_4 == "" ? '<span class="Fi-Criteria _txtValue_4_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>' : '<span class="Fi-Criteria _txtValue_4_4" style="border: 1px solid #003d66;padding: 3px 4px 3px 4px;margin: 0px 3px 0px 0px;">' + txtValue_4_4 + '</span>');
                //}
                //$(this).find('.Pricing_4').html(html);

            }
        });
        EditCriteria_UniqueId = "";
        Reset_API_Filter();
    }
}

function EditCriteria(new_id) {
    loaderShow();
    setTimeout(function () {
        Reset_API_Filter();
        $("#btn_Add_in_Stock_Disc").html('<i class="fa fa-plus"></i>Update Row');
        $(".UpdateCancelRow").show();
        EditCriteria_UniqueId = new_id;

        $("#tblFilters #tblBodyFilters tr").each(function () {
            $(this)[0].className = '';
            $(this).find('.EditCriteria').show();
            $(this).find('.RemoveCriteria').show();

            if ($(this).find('.hdn_UniqueId').val() == EditCriteria_UniqueId) {
                $(this)[0].className = 'filter_tr_active';
                $(this).find('.EditCriteria').hide();
                $(this).find('.RemoveCriteria').hide();


                var Supplier = $(this).find('.Supplier').html();
                if (Supplier != "") {
                    for (var i in Supplier.split(',')) {
                        for (var j in SupplierList) {
                            if (j > 0) {
                                if (Supplier.split(',')[i] == SupplierList[j].Value) {
                                    SupplierList[j].isActive = true;
                                }
                            }
                        }
                    }
                }

                var Shape = $(this).find('.Shape').html();
                if (Shape != "") {
                    for (var i in Shape.split(',')) {
                        for (var j in ShapeList) {
                            if (j > 0) {
                                if (Shape.split(',')[i] == ShapeList[j].Value) {
                                    ShapeList[j].isActive = true;
                                }
                            }
                        }
                    }
                }

                var Carat = $(this).find('.Carat').html();
                $(".divCheckedPointerValue").empty();
                _pointerlst = [];
                if (Carat != "") {
                    for (var i in Carat.split(',')) {
                        _pointerlst.push({ Id: parseInt(i) + parseInt(1), Value: Carat.split(',')[i], isActive: true })
                        $('.divCheckedPointerValue').append('<li id="C_' + i + '" class="carat-li-top allcrt">' + Carat.split(',')[i] + '<i class="fa fa-times-circle" aria-hidden="true" onclick="NewSizeGroupRemove(' + i + ');"></i></li>');
                    }
                }

                var ColorType = $(this).find('.ColorType').html();
                Regular_All = (ColorType == "Regular" ? true : false);
                Fancy_All = (ColorType == "Fancy" ? true : false);
                var Color = $(this).find('.Color').html();
                var dCheckINTENSITY = $(this).find('.dCheckINTENSITY').html();
                var dCheckOVERTONE = $(this).find('.dCheckOVERTONE').html();
                var dCheckFANCY_COLOR = $(this).find('.dCheckFANCY_COLOR').html();

                if (Regular_All == true || Color != "") {
                    ActiveOrNot('Regular');
                    document.getElementById("chk_Regular_All").checked = true;
                    Regular_All = true;
                }
                else if (Fancy_All == true || dCheckINTENSITY != "" || dCheckOVERTONE != "" || dCheckFANCY_COLOR != "") {
                    ActiveOrNot('Fancy');
                    document.getElementById("chk_Fancy_All").checked = true;
                    Fancy_All = true;
                    Key_to_symbolShow();
                }

                if (Color != "") {
                    for (var i in Color.split(',')) {
                        for (var j in ColorList) {
                            if (j > 0) {
                                if (Color.split(',')[i] == ColorList[j].Value) {
                                    ColorList[j].isActive = true;
                                    ItemClicked('Color', ColorList[j].Value, ColorList[j].Id, this);
                                }
                            }
                        }
                    }
                }
                if (dCheckINTENSITY != "") {
                    for (var i in INTENSITY) {
                        if (i > 0) {
                            for (var j in dCheckINTENSITY.split(',')) {
                                if (dCheckINTENSITY.split(',')[j] == INTENSITY[i]) {
                                    GetCheck_INTENSITY_List(INTENSITY[i], i);
                                    document.getElementById("CHK_I_" + i).checked = true;
                                }
                            }
                        }
                    }
                }
                if (dCheckOVERTONE != "") {
                    for (var i in OVERTONE) {
                        if (i > 0) {
                            for (var j in dCheckOVERTONE.split(',')) {
                                if (dCheckOVERTONE.split(',')[j] == OVERTONE[i]) {
                                    GetCheck_OVERTONE_List(OVERTONE[i], i);
                                    document.getElementById("CHK_O_" + i).checked = true;
                                }
                            }
                        }
                    }
                }
                if (dCheckFANCY_COLOR != "") {
                    for (var i in FANCY_COLOR) {
                        if (i > 0) {
                            for (var j in dCheckFANCY_COLOR.split(',')) {
                                if (dCheckFANCY_COLOR.split(',')[j] == FANCY_COLOR[i]) {
                                    GetCheck_FANCY_COLOR_List(FANCY_COLOR[i], i);
                                    document.getElementById("CHK_F_" + i).checked = true;
                                }
                            }
                        }
                    }
                }

                var Clarity = $(this).find('.Clarity').html();
                if (Clarity != "") {
                    for (var i in Clarity.split(',')) {
                        for (var j in ClarityList) {
                            if (j > 0) {
                                if (Clarity.split(',')[i] == ClarityList[j].Value) {
                                    ClarityList[j].isActive = true;
                                }
                            }
                        }
                    }
                }

                var Cut = $(this).find('.Cut').html();
                if (Cut != "") {
                    for (var i in Cut.split(',')) {
                        for (var j in CutList) {
                            if (j > 0) {
                                if (Cut.split(',')[i] == CutList[j].Value) {
                                    CutList[j].isActive = true;
                                }
                            }
                        }
                    }
                }

                var Polish = $(this).find('.Polish').html();
                if (Polish != "") {
                    for (var i in Polish.split(',')) {
                        for (var j in PolishList) {
                            if (j > 0) {
                                if (Polish.split(',')[i] == PolishList[j].Value) {
                                    PolishList[j].isActive = true;
                                }
                            }
                        }
                    }
                }

                var Sym = $(this).find('.Sym').html();
                if (Sym != "") {
                    for (var i in Sym.split(',')) {
                        for (var j in SymList) {
                            if (j > 0) {
                                if (Sym.split(',')[i] == SymList[j].Value) {
                                    SymList[j].isActive = true;
                                }
                            }
                        }
                    }
                }

                var Fls = $(this).find('.Fls').html();
                if (Fls != "") {
                    for (var i in Fls.split(',')) {
                        for (var j in FlsList) {
                            if (j > 0) {
                                if (Fls.split(',')[i] == FlsList[j].Value) {
                                    FlsList[j].isActive = true;
                                }
                            }
                        }
                    }
                }

                var Lab = $(this).find('.Lab').html();
                if (Lab != "") {
                    for (var i in Lab.split(',')) {
                        for (var j in LabList) {
                            if (j > 0) {
                                if (Lab.split(',')[i] == LabList[j].Value) {
                                    LabList[j].isActive = true;
                                }
                            }
                        }
                    }
                }

                $("#FromLength").val(NullReplaceDecimalToFixed($(this).find('.FromLength').html()));
                $("#ToLength").val(NullReplaceDecimalToFixed($(this).find('.ToLength').html()));
                $("#FromWidth").val(NullReplaceDecimalToFixed($(this).find('.FromWidth').html()));
                $("#ToWidth").val(NullReplaceDecimalToFixed($(this).find('.ToWidth').html()));
                $("#FromDepth").val(NullReplaceDecimalToFixed($(this).find('.FromDepth').html()));
                $("#ToDepth").val(NullReplaceDecimalToFixed($(this).find('.ToDepth').html()));
                $("#FromDepthPer").val(NullReplaceDecimalToFixed($(this).find('.FromDepthinPer').html()));
                $("#ToDepthPer").val(NullReplaceDecimalToFixed($(this).find('.ToDepthinPer').html()));
                $("#FromTablePer").val(NullReplaceDecimalToFixed($(this).find('.FromTableinPer').html()));
                $("#ToTablePer").val(NullReplaceDecimalToFixed($(this).find('.ToTableinPer').html()));
                $("#FromCrAng").val(NullReplaceDecimalToFixed($(this).find('.FromCrAng').html()));
                $("#ToCrAng").val(NullReplaceDecimalToFixed($(this).find('.ToCrAng').html()));
                $("#FromCrHt").val(NullReplaceDecimalToFixed($(this).find('.FromCrHt').html()));
                $("#ToCrHt").val(NullReplaceDecimalToFixed($(this).find('.ToCrHt').html()));
                $("#FromPavAng").val(NullReplaceDecimalToFixed($(this).find('.FromPavAng').html()));
                $("#ToPavAng").val(NullReplaceDecimalToFixed($(this).find('.ToPavAng').html()));
                $("#FromPavHt").val(NullReplaceDecimalToFixed($(this).find('.FromPavHt').html()));
                $("#ToPavHt").val(NullReplaceDecimalToFixed($(this).find('.ToPavHt').html()));

                var dCheckKTS = $(this).find('.dCheckKTS').html();
                var dUNCheckKTS = $(this).find('.dUNCheckKTS').html();

                if (dCheckKTS != "") {
                    for (var i in dCheckKTS.split(',')) {
                        for (var j in KeyToSymbolList) {
                            if (dCheckKTS.split(',')[i] == KeyToSymbolList[j].Value) {
                                document.getElementById("CHK_KTS_Radio_" + (parseInt(j) + 1)).checked = true;
                                GetCheck_KTS_List(KeyToSymbolList[j].Value);
                            }
                        }
                    }
                }
                if (dUNCheckKTS != "") {
                    for (var i in dUNCheckKTS.split(',')) {
                        for (var j in KeyToSymbolList) {
                            if (dUNCheckKTS.split(',')[i] == KeyToSymbolList[j].Value) {
                                document.getElementById("UNCHK_KTS_Radio_" + (parseInt(j) + 1)).checked = true;
                                GetUnCheck_KTS_List(KeyToSymbolList[j].Value);
                            }
                        }
                    }
                }

                var BGM = $(this).find('.BGM').html();
                if (BGM != "") {
                    for (var i in BGM.split(',')) {
                        for (var j in BGMList) {
                            if (j > 0) {
                                if (BGM.split(',')[i] == BGMList[j].Value) {
                                    BGMList[j].isActive = true;
                                }
                            }
                        }
                    }
                }

                var CrownBlack = $(this).find('.CrownBlack').html();
                if (CrownBlack != "") {
                    for (var i in CrownBlack.split(',')) {
                        for (var j in CrownBlackList) {
                            if (j > 0) {
                                if (CrownBlack.split(',')[i] == CrownBlackList[j].Value) {
                                    CrownBlackList[j].isActive = true;
                                }
                            }
                        }
                    }
                }

                var TableBlack = $(this).find('.TableBlack').html();
                if (TableBlack != "") {
                    for (var i in TableBlack.split(',')) {
                        for (var j in TableBlackList) {
                            if (j > 0) {
                                if (TableBlack.split(',')[i] == TableBlackList[j].Value) {
                                    TableBlackList[j].isActive = true;
                                }
                            }
                        }
                    }
                }

                var CrownWhite = $(this).find('.CrownWhite').html();
                if (CrownWhite != "") {
                    for (var i in CrownWhite.split(',')) {
                        for (var j in CrownWhiteList) {
                            if (j > 0) {
                                if (CrownWhite.split(',')[i] == CrownWhiteList[j].Value) {
                                    CrownWhiteList[j].isActive = true;
                                }
                            }
                        }
                    }
                }

                var TableWhite = $(this).find('.TableWhite').html();
                if (TableWhite != "") {
                    for (var i in TableWhite.split(',')) {
                        for (var j in TableWhiteList) {
                            if (j > 0) {
                                if (TableWhite.split(',')[i] == TableWhiteList[j].Value) {
                                    TableWhiteList[j].isActive = true;
                                }
                            }
                        }
                    }
                }

                var GoodsType = $(this).find('.GoodsType').html();
                if (GoodsType != "") {
                    for (var i in GoodsType.split(',')) {
                        for (var j in GoodsTypeList) {
                            if (j > 0) {
                                if (GoodsType.split(',')[i] == GoodsTypeList[j].Value) {
                                    GoodsTypeList[j].isActive = true;
                                }
                            }
                        }
                    }
                }

                $(".Ig" + $(this).find('.Image').html()).prop("checked", true);
                $(".Vd" + $(this).find('.Video').html()).prop("checked", true);


                var PricingMethod_1 = $(this).find('.PricingMethod_1').html();
                var PricingSign_1 = $(this).find('.PricingSign_1').html();
                var txtDisc_1_1 = NullReplaceDecimal4ToFixed($(this).find('.txtDisc_1_1').html());
                var txtValue_1_1 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_1_1').html());
                var txtValue_1_2 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_1_2').html());
                var txtValue_1_3 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_1_3').html());
                var txtValue_1_4 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_1_4').html());

                var Chk_Speci_Additional_1 = $(this).find('.Chk_Speci_Additional_1').html();

                var txtFromDate = $(this).find('.txtFromDate').html();
                var txtToDate = $(this).find('.txtToDate').html();
                var PricingMethod_2 = $(this).find('.PricingMethod_2').html();
                var PricingSign_2 = ($(this).find('.PricingSign_2').html() == "" ? "=" : $(this).find('.PricingSign_2').html());
                var txtDisc_2_1 = NullReplaceDecimal4ToFixed($(this).find('.txtDisc_2_1').html());
                var txtValue_2_1 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_2_1').html());
                var txtValue_2_2 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_2_2').html());
                var txtValue_2_3 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_2_3').html());
                var txtValue_2_4 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_2_4').html());


                $("#PricingMethod_1").val(PricingMethod_1);
                PricingMethodDDL('1');

                $("#PricingSign_1").val(PricingSign_1);

                if (PricingMethod_1 == "Disc") {
                    $("#txtDisc_1_1").val(txtDisc_1_1);
                }
                else if (PricingMethod_1 == "Value") {
                    $("#txtValue_1_1").val(txtValue_1_1);
                    $("#txtValue_1_2").val(txtValue_1_2);
                    $("#txtValue_1_3").val(txtValue_1_3);
                    $("#txtValue_1_4").val(txtValue_1_4);
                }

                document.getElementById("Chk_Speci_Additional_1").checked = (Chk_Speci_Additional_1 == "true" ? true : false);

                if (PricingMethod_2 != "") {
                    $("#txtFromDate").val(txtFromDate);
                    $("#txtToDate").val(txtToDate);
                    againCalendarCall("1");
                }
                else {
                    FromTo_Date("1");
                }

                $("#PricingMethod_2").val(PricingMethod_2);
                PricingMethodDDL('2');

                $("#PricingSign_2").val(PricingSign_2);

                if (PricingMethod_2 == "Disc") {
                    $("#txtDisc_2_1").val(txtDisc_2_1);
                }
                else if (PricingMethod_2 == "Value") {
                    $("#txtValue_2_1").val(txtValue_2_1);
                    $("#txtValue_2_2").val(txtValue_2_2);
                    $("#txtValue_2_3").val(txtValue_2_3);
                    $("#txtValue_2_4").val(txtValue_2_4);
                }

                //var PricingMethod_3 = $(this).find('.PricingMethod_3').html();
                //var PricingSign_3 = $(this).find('.PricingSign_3').html();
                //var txtDisc_3_1 = NullReplaceDecimal4ToFixed($(this).find('.txtDisc_3_1').html());
                //var txtValue_3_1 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_3_1').html());
                //var txtValue_3_2 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_3_2').html());
                //var txtValue_3_3 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_3_3').html());
                //var txtValue_3_4 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_3_4').html());

                //var Chk_Speci_Additional_2 = $(this).find('.Chk_Speci_Additional_2').html();

                //var txtFromDate1 = $(this).find('.txtFromDate1').html();
                //var txtToDate1 = $(this).find('.txtToDate1').html();
                //var PricingMethod_4 = $(this).find('.PricingMethod_4').html();
                //var PricingSign_4 = ($(this).find('.PricingSign_4').html() == "" ? "=" : $(this).find('.PricingSign_4').html());
                //var txtDisc_4_1 = NullReplaceDecimal4ToFixed($(this).find('.txtDisc_4_1').html());
                //var txtValue_4_1 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_4_1').html());
                //var txtValue_4_2 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_4_2').html());
                //var txtValue_4_3 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_4_3').html());
                //var txtValue_4_4 = NullReplaceDecimal4ToFixed($(this).find('.txtValue_4_4').html());


                //$("#PricingMethod_3").val(PricingMethod_3);
                //PricingMethodDDL('3');

                //$("#PricingSign_3").val(PricingSign_3);

                //if (PricingMethod_3 == "Disc") {
                //    $("#txtDisc_3_1").val(txtDisc_3_1);
                //}
                //else if (PricingMethod_3 == "Value") {
                //    $("#txtValue_3_1").val(txtValue_3_1);
                //    $("#txtValue_3_2").val(txtValue_3_2);
                //    $("#txtValue_3_3").val(txtValue_3_3);
                //    $("#txtValue_3_4").val(txtValue_3_4);
                //}

                //document.getElementById("Chk_Speci_Additional_2").checked = (Chk_Speci_Additional_2 == "true" ? true : false);

                //if (PricingMethod_4 != "") {
                //    $("#txtFromDate1").val(txtFromDate1);
                //    $("#txtToDate1").val(txtToDate1);
                //    againCalendarCall("2");
                //}
                //else {
                //    FromTo_Date("2");
                //}

                //$("#PricingMethod_4").val(PricingMethod_4);
                //PricingMethodDDL('4');

                //$("#PricingSign_4").val(PricingSign_4);

                //if (PricingMethod_4 == "Disc") {
                //    $("#txtDisc_4_1").val(txtDisc_4_1);
                //}
                //else if (PricingMethod_4 == "Value") {
                //    $("#txtValue_4_1").val(txtValue_4_1);
                //    $("#txtValue_4_2").val(txtValue_4_2);
                //    $("#txtValue_4_3").val(txtValue_4_3);
                //    $("#txtValue_4_4").val(txtValue_4_4);
                //}

                SetSearchParameter();
                $(window).scrollTop(100);
            }
        });

        loaderHide();
    }, 5);
}
function UpdateCancelRow() {
    loaderShow();
    setTimeout(function () {
        Reset_API_Filter();
        $("#btn_Add_in_Stock_Disc").html('<i class="fa fa-plus"></i>Add Row');
        $(".UpdateCancelRow").hide();
        EditCriteria_UniqueId = "";
        $("#tblFilters #tblBodyFilters tr").each(function () {
            $(this)[0].className = '';
            $(this).find('.EditCriteria').show();
            $(this).find('.RemoveCriteria').show();
        });
        loaderHide();
    }, 5);
}

var GetError_1 = function () {
    ErrorMsg = [];
    if (parseInt($("#tblFilters #tblBodyFilters").find('tr').length) == 0 && Exists_Record == 0) {
        ErrorMsg.push({
            'Error': "Supplier Disc Pricing Filter Not Found.",
        });
    }
    return ErrorMsg;
}
function SaveData() {
    ErrorMsg = GetError_1();
    if (ErrorMsg.length > 0) {
        $("#divError").empty();
        ErrorMsg.forEach(function (item) {
            $("#divError").append('<li>' + item.Error + '</li>');
        });
        $("#ErrorModel").modal("show");
    }
    else {
        var list = [];
        $("#tblFilters #tblBodyFilters tr").each(function () {
            list.push({
                Supplier: $(this).find('.Supplier').html(),
                Shape: $(this).find('.Shape').html(),
                Carat: $(this).find('.Carat').html(),
                ColorType: $(this).find('.ColorType').html(),
                Color: $(this).find('.Color').html(),
                INTENSITY: $(this).find('.dCheckINTENSITY').html(),
                OVERTONE: $(this).find('.dCheckOVERTONE').html(),
                FANCY_COLOR: $(this).find('.dCheckFANCY_COLOR').html(),
                Clarity: $(this).find('.Clarity').html(),
                Cut: $(this).find('.Cut').html(),
                Polish: $(this).find('.Polish').html(),
                Sym: $(this).find('.Sym').html(),
                Fls: $(this).find('.Fls').html(),
                Lab: $(this).find('.Lab').html(),
                FromLength: $(this).find('.FromLength').html(),
                ToLength: $(this).find('.ToLength').html(),
                FromWidth: $(this).find('.FromWidth').html(),
                ToWidth: $(this).find('.ToWidth').html(),
                FromDepth: $(this).find('.FromDepth').html(),
                ToDepth: $(this).find('.ToDepth').html(),
                FromDepthinPer: $(this).find('.FromDepthinPer').html(),
                ToDepthinPer: $(this).find('.ToDepthinPer').html(),
                FromTableinPer: $(this).find('.FromTableinPer').html(),
                ToTableinPer: $(this).find('.ToTableinPer').html(),
                FromCrAng: $(this).find('.FromCrAng').html(),
                ToCrAng: $(this).find('.ToCrAng').html(),
                FromCrHt: $(this).find('.FromCrHt').html(),
                ToCrHt: $(this).find('.ToCrHt').html(),
                FromPavAng: $(this).find('.FromPavAng').html(),
                ToPavAng: $(this).find('.ToPavAng').html(),
                FromPavHt: $(this).find('.FromPavHt').html(),
                ToPavHt: $(this).find('.ToPavHt').html(),
                CheckKTS: $(this).find('.dCheckKTS').html(),
                UNCheckKTS: $(this).find('.dUNCheckKTS').html(),
                BGM: $(this).find('.BGM').html(),
                CrownBlack: $(this).find('.CrownBlack').html(),
                TableBlack: $(this).find('.TableBlack').html(),
                CrownWhite: $(this).find('.CrownWhite').html(),
                TableWhite: $(this).find('.TableWhite').html(),
                GoodsType: $(this).find('.GoodsType').html(),
                Image: $(this).find('.Image').html(),
                Video: $(this).find('.Video').html(),
                PricingMethod_1: $(this).find('.PricingMethod_1').html(),
                PricingSign_1: $(this).find('.PricingSign_1').html(),
                Disc_1_1: $(this).find('.txtDisc_1_1').html(),
                Value_1_1: $(this).find('.txtValue_1_1').html(),
                Value_1_2: $(this).find('.txtValue_1_2').html(),
                Value_1_3: $(this).find('.txtValue_1_3').html(),
                Value_1_4: $(this).find('.txtValue_1_4').html(),
                Speci_Additional_1: ($(this).find('.Chk_Speci_Additional_1').html() == 'true' ? 1 : 0),
                FromDate: ($(this).find('.PricingMethod_2').html() != "" ? $(this).find('.txtFromDate').html() : ''),
                ToDate: ($(this).find('.PricingMethod_2').html() != "" ? $(this).find('.txtToDate').html() : ''),
                PricingMethod_2: $(this).find('.PricingMethod_2').html(),
                PricingSign_2: ($(this).find('.PricingMethod_2').html() != "" ? $(this).find('.PricingSign_2').html() : ''),
                Disc_2_1: $(this).find('.txtDisc_2_1').html(),
                Value_2_1: $(this).find('.txtValue_2_1').html(),
                Value_2_2: $(this).find('.txtValue_2_2').html(),
                Value_2_3: $(this).find('.txtValue_2_3').html(),
                Value_2_4: $(this).find('.txtValue_2_4').html(),
                //PricingMethod_3: $(this).find('.PricingMethod_3').html(),
                //PricingSign_3: $(this).find('.PricingSign_3').html(),
                //Disc_3_1: $(this).find('.txtDisc_3_1').html(),
                //Value_3_1: $(this).find('.txtValue_3_1').html(),
                //Value_3_2: $(this).find('.txtValue_3_2').html(),
                //Value_3_3: $(this).find('.txtValue_3_3').html(),
                //Value_3_4: $(this).find('.txtValue_3_4').html(),
                //Speci_Additional_2: ($(this).find('.Chk_Speci_Additional_2').html() == 'true' ? 1 : 0),
                //FromDate1: ($(this).find('.PricingMethod_4').html() != "" ? $(this).find('.txtFromDate1').html() : ''),
                //ToDate1: ($(this).find('.PricingMethod_4').html() != "" ? $(this).find('.txtToDate1').html() : ''),
                //PricingMethod_4: $(this).find('.PricingMethod_4').html(),
                //PricingSign_4: ($(this).find('.PricingMethod_4').html() != "" ? $(this).find('.PricingSign_4').html() : ''),
                //Disc_4_1: $(this).find('.txtDisc_4_1').html(),
                //Value_4_1: $(this).find('.txtValue_4_1').html(),
                //Value_4_2: $(this).find('.txtValue_4_2').html(),
                //Value_4_3: $(this).find('.txtValue_4_3').html(),
                //Value_4_4: $(this).find('.txtValue_4_4').html()
            });
        });

        var obj = {};
        obj.UserId = _.pluck(_.filter(gridOptions.api.getSelectedRows()), 'UserId').join(",");
        obj.SuppDisc = list;

        loaderShow();
        $.ajax({
            url: '/User/AddUpdate_Customer_Stock_Disc',
            type: "POST",
            data: { req: obj },
            success: function (data) {
                loaderHide();
                if (data.Status == "1") {
                    toastr.success(data.Message);
                    Get_Customer_Stock_Disc();
                    $(window).scrollTop(100);
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

function Get_Customer_Stock_Disc() {
    loaderShow();
    var obj = {};
    obj.UserId = _.pluck(_.filter(gridOptions.api.getSelectedRows()), 'UserId').join(",");

    $.ajax({
        url: '/User/Get_Customer_Stock_Disc',
        type: "POST",
        data: { req: obj },
        success: function (data) {
            if (data.Status == "1" && data.Data.length > 0) {
                Exists_Record = data.Data.length;

                var html = "";
                $.each(data.Data, function (i, itm) {
                    var new_id = generate_uuidv4();

                    var Supplier = NullReplace(itm.Supplier);
                    var Shape = NullReplace(itm.Shape);
                    var Carat = NullReplace(itm.Carat);
                    var Color_Type = NullReplace(itm.ColorType);
                    var Color = NullReplace(itm.Color);
                    var F_INTENSITY = NullReplace(itm.INTENSITY);
                    var F_OVERTONE = NullReplace(itm.OVERTONE);
                    var F_FANCY_COLOR = NullReplace(itm.FANCY_COLOR);
                    var MixColor = "";

                    var _FC = "";
                    if (F_INTENSITY != "") {
                        _FC += (_FC == "" ? "" : "</br>") + "<b>INTENSITY :</b>";
                        _FC += F_INTENSITY;
                    }
                    if (F_OVERTONE != "") {
                        _FC += (_FC == "" ? "" : "</br>") + "<b>OVERTONE :</b>";
                        _FC += F_OVERTONE;
                    }
                    if (F_FANCY_COLOR != "") {
                        _FC += (_FC == "" ? "" : "</br>") + "<b>FANCY COLOR :</b>";
                        _FC += F_FANCY_COLOR;
                    }

                    if (Color != "") {
                        MixColor = Color;
                    }
                    else if (_FC != "") {
                        MixColor = _FC;
                    }
                    if (Color_Type != "") {
                        MixColor = (Color_Type == "Regular" ? "<b>REGULAR ALL</b>" : Color_Type == "Fancy" ? "<b>FANCY ALL</b>" : "");
                    }
                    var Clarity = NullReplace(itm.Clarity);
                    var Cut = NullReplace(itm.Cut);
                    var Polish = NullReplace(itm.Polish);
                    var Sym = NullReplace(itm.Sym);
                    var Fls = NullReplace(itm.Fls);
                    var Lab = NullReplace(itm.Lab);

                    var FromLength = NullReplaceDecimalToFixed(itm.FromLength);
                    var ToLength = NullReplaceDecimalToFixed(itm.ToLength);
                    var FromWidth = NullReplaceDecimalToFixed(itm.FromWidth);
                    var ToWidth = NullReplaceDecimalToFixed(itm.ToWidth);
                    var FromDepth = NullReplaceDecimalToFixed(itm.FromDepth);
                    var ToDepth = NullReplaceDecimalToFixed(itm.ToDepth);
                    var FromDepthinPer = NullReplaceDecimalToFixed(itm.FromDepthinPer);
                    var ToDepthinPer = NullReplaceDecimalToFixed(itm.ToDepthinPer);
                    var FromTableinPer = NullReplaceDecimalToFixed(itm.FromTableinPer);
                    var ToTableinPer = NullReplaceDecimalToFixed(itm.ToTableinPer);
                    var FromCrAng = NullReplaceDecimalToFixed(itm.FromCrAng);
                    var ToCrAng = NullReplaceDecimalToFixed(itm.ToCrAng);
                    var FromCrHt = NullReplaceDecimalToFixed(itm.FromCrHt);
                    var ToCrHt = NullReplaceDecimalToFixed(itm.ToCrHt);
                    var FromPavAng = NullReplaceDecimalToFixed(itm.FromPavAng);
                    var ToPavAng = NullReplaceDecimalToFixed(itm.ToPavAng);
                    var FromPavHt = NullReplaceDecimalToFixed(itm.FromPavHt);
                    var ToPavHt = NullReplaceDecimalToFixed(itm.ToPavHt);

                    var KeyToSymLst_Check1 = NullReplace(itm.CheckKTS);
                    var KeyToSymLst_uncheck1 = NullReplace(itm.UNCheckKTS);
                    var Keytosymbol = KeyToSymLst_Check1 + (KeyToSymLst_Check1 == "" || KeyToSymLst_uncheck1 == "" ? "" : "-") + KeyToSymLst_uncheck1;
                    var dCheckKTS = KeyToSymLst_Check1;
                    var dUNCheckKTS = KeyToSymLst_uncheck1;
                    var BGM = NullReplace(itm.BGM);
                    var CrownBlack = NullReplace(itm.CrownBlack);
                    var TableBlack = NullReplace(itm.TableBlack);
                    var CrownWhite = NullReplace(itm.CrownWhite);
                    var TableWhite = NullReplace(itm.TableWhite);
                    var GoodsType = NullReplace(itm.GoodsType);
                    var Image = NullReplace(itm.Image);
                    var Video = NullReplace(itm.Video);

                    var PricingMethod_1 = NullReplace(itm.PricingMethod_1);
                    var PricingSign_1 = NullReplace(itm.PricingSign_1);
                    var txtDisc_1_1 = NullReplaceDecimal4ToFixed(itm.Disc_1_1);
                    var txtValue_1_1 = NullReplaceDecimal4ToFixed(itm.Value_1_1);
                    var txtValue_1_2 = NullReplaceDecimal4ToFixed(itm.Value_1_2);
                    var txtValue_1_3 = NullReplaceDecimal4ToFixed(itm.Value_1_3);
                    var txtValue_1_4 = NullReplaceDecimal4ToFixed(itm.Value_1_4);
                    var Chk_Speci_Additional_1 = (NullReplace(itm.Speci_Additional_1) == "1" ? true : false);
                    var txtFromDate = NullReplace(itm.FromDate);
                    var txtToDate = NullReplace(itm.ToDate);
                    var PricingMethod_2 = NullReplace(itm.PricingMethod_2);
                    var PricingSign_2 = NullReplace(itm.PricingSign_2);
                    var txtDisc_2_1 = NullReplaceDecimal4ToFixed(itm.Disc_2_1);
                    var txtValue_2_1 = NullReplaceDecimal4ToFixed(itm.Value_2_1);
                    var txtValue_2_2 = NullReplaceDecimal4ToFixed(itm.Value_2_2);
                    var txtValue_2_3 = NullReplaceDecimal4ToFixed(itm.Value_2_3);
                    var txtValue_2_4 = NullReplaceDecimal4ToFixed(itm.Value_2_4);

                    //var PricingMethod_3 = NullReplace(itm.PricingMethod_3);
                    //var PricingSign_3 = NullReplace(itm.PricingSign_3);
                    //var txtDisc_3_1 = NullReplaceDecimal4ToFixed(itm.Disc_3_1);
                    //var txtValue_3_1 = NullReplaceDecimal4ToFixed(itm.Value_3_1);
                    //var txtValue_3_2 = NullReplaceDecimal4ToFixed(itm.Value_3_2);
                    //var txtValue_3_3 = NullReplaceDecimal4ToFixed(itm.Value_3_3);
                    //var txtValue_3_4 = NullReplaceDecimal4ToFixed(itm.Value_3_4);
                    //var Chk_Speci_Additional_2 = (NullReplace(itm.Speci_Additional_2) == "1" ? true : false);
                    //var txtFromDate1 = NullReplace(itm.FromDate1);
                    //var txtToDate1 = NullReplace(itm.ToDate1);
                    //var PricingMethod_4 = NullReplace(itm.PricingMethod_4);
                    //var PricingSign_4 = NullReplace(itm.PricingSign_4);
                    //var txtDisc_4_1 = NullReplaceDecimal4ToFixed(itm.Disc_4_1);
                    //var txtValue_4_1 = NullReplaceDecimal4ToFixed(itm.Value_4_1);
                    //var txtValue_4_2 = NullReplaceDecimal4ToFixed(itm.Value_4_2);
                    //var txtValue_4_3 = NullReplaceDecimal4ToFixed(itm.Value_4_3);
                    //var txtValue_4_4 = NullReplaceDecimal4ToFixed(itm.Value_4_4);

                    //html += "<tr class='tr11'>";
                    //html += "<th class='Row Fi-Criteria' style=''></th>";
                    //html += "<td><span class='Fi-Criteria Shape'>" + Shape + "</span></td>";
                    //html += "<td><span class='Fi-Criteria Carat'>" + Carat + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria ColorType'>" + Color_Type + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria Color'>" + Color + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria dCheckINTENSITY'>" + F_INTENSITY + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria dCheckOVERTONE'>" + F_OVERTONE + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria dCheckFANCY_COLOR'>" + F_FANCY_COLOR + "</span></td>";
                    //html += "<td><span class='Fi-Criteria MixColor'>" + MixColor + "</span></td>";
                    //html += "<td><span class='Fi-Criteria Clarity'>" + Clarity + "</span></td>";
                    //html += "<td><span class='Fi-Criteria Cut'>" + Cut + "</span></td>";
                    //html += "<td><span class='Fi-Criteria Polish'>" + Polish + "</span></td>";
                    //html += "<td><span class='Fi-Criteria Sym'>" + Sym + "</span></td>";
                    //html += "<td><span class='Fi-Criteria Fls'>" + Fls + "</span></td>";
                    //html += "<td><span class='Fi-Criteria Lab'>" + Lab + "</span></td>";
                    //html += "<td><span class='Fi-Criteria FromLength'>" + FromLength + "</span></td>";
                    //html += "<td><span class='Fi-Criteria ToLength'>" + ToLength + "</span></td>";
                    //html += "<td><span class='Fi-Criteria FromWidth'>" + FromWidth + "</span></td>";
                    //html += "<td><span class='Fi-Criteria ToWidth'>" + ToWidth + "</span></td>";
                    //html += "<td><span class='Fi-Criteria FromDepth'>" + FromDepth + "</span></td>";
                    //html += "<td><span class='Fi-Criteria ToDepth'>" + ToDepth + "</span></td>";
                    //html += "<td><span class='Fi-Criteria FromDepthinPer'>" + FromDepthinPer + "</span></td>";
                    //html += "<td><span class='Fi-Criteria ToDepthinPer'>" + ToDepthinPer + "</span></td>";
                    //html += "<td><span class='Fi-Criteria FromTableinPer'>" + FromTableinPer + "</span></td>";
                    //html += "<td><span class='Fi-Criteria ToTableinPer'>" + ToTableinPer + "</span></td>";
                    //html += "<td><span class='Fi-Criteria FromCrAng'>" + FromCrAng + "</span></td>";
                    //html += "<td><span class='Fi-Criteria ToCrAng'>" + ToCrAng + "</span></td>";
                    //html += "<td><span class='Fi-Criteria FromCrHt'>" + FromCrHt + "</span></td>";
                    //html += "<td><span class='Fi-Criteria ToCrHt'>" + ToCrHt + "</span></td>";
                    //html += "<td><span class='Fi-Criteria FromPavAng'>" + FromPavAng + "</span></td>";
                    //html += "<td><span class='Fi-Criteria ToPavAng'>" + ToPavAng + "</span></td>";
                    //html += "<td><span class='Fi-Criteria FromPavHt'>" + FromPavHt + "</span></td>";
                    //html += "<td><span class='Fi-Criteria ToPavHt'>" + ToPavHt + "</span></td>";
                    //html += "<td><span class='Fi-Criteria Keytosymbol'>" + Keytosymbol + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria dCheckKTS'>" + dCheckKTS + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria dUNCheckKTS'>" + dUNCheckKTS + "</span></td>";
                    //html += "<td><span class='Fi-Criteria BGM'>" + BGM + "</span></td>";
                    //html += "<td><span class='Fi-Criteria CrownBlack'>" + CrownBlack + "</span></td>";
                    //html += "<td><span class='Fi-Criteria TableBlack'>" + TableBlack + "</span></td>";
                    //html += "<td><span class='Fi-Criteria CrownWhite'>" + CrownWhite + "</span></td>";
                    //html += "<td><span class='Fi-Criteria TableWhite'>" + TableWhite + "</span></td>";
                    //html += "<td><span class='Fi-Criteria GoodsType'>" + GoodsType + "</span></td>";
                    //html += "<td><span class='Fi-Criteria Image'>" + Image + "</span></td>";
                    //html += "<td><span class='Fi-Criteria Video'>" + Video + "</span></td>";

                    //html += "<td style='display:none;'><span class='Fi-Criteria PricingMethod_1'>" + PricingMethod_1 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria PricingSign_1'>" + PricingSign_1 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtDisc_1_1'>" + txtDisc_1_1 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_1_1'>" + txtValue_1_1 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_1_2'>" + txtValue_1_2 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_1_3'>" + txtValue_1_3 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_1_4'>" + txtValue_1_4 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria Chk_Speci_Additional_1'>" + Chk_Speci_Additional_1 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtFromDate'>" + txtFromDate + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtToDate'>" + txtToDate + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria PricingMethod_2'>" + PricingMethod_2 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria PricingSign_2'>" + PricingSign_2 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtDisc_2_1'>" + txtDisc_2_1 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_2_1'>" + txtValue_2_1 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_2_2'>" + txtValue_2_2 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_2_3'>" + txtValue_2_3 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_2_4'>" + txtValue_2_4 + "</span></td>";

                    //html += "<td style='display:none;'><span class='Fi-Criteria PricingMethod_3'>" + PricingMethod_3 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria PricingSign_3'>" + PricingSign_3 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtDisc_3_1'>" + txtDisc_3_1 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_3_1'>" + txtValue_3_1 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_3_2'>" + txtValue_3_2 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_3_3'>" + txtValue_3_3 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_3_4'>" + txtValue_3_4 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria Chk_Speci_Additional_2'>" + Chk_Speci_Additional_2 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtFromDate1'>" + txtFromDate1 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtToDate1'>" + txtToDate1 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria PricingMethod_4'>" + PricingMethod_4 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria PricingSign_4'>" + PricingSign_4 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtDisc_4_1'>" + txtDisc_4_1 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_4_1'>" + txtValue_4_1 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_4_2'>" + txtValue_4_2 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_4_3'>" + txtValue_4_3 + "</span></td>";
                    //html += "<td style='display:none;'><span class='Fi-Criteria txtValue_4_4'>" + txtValue_4_4 + "</span></td>";

                    //html += "<td><span class='Fi-Criteria _PricingMethod_1'>" + (PricingMethod_1 == "Disc" ? 'Discount' : 'Value') + "</span></td>";
                    //html += "<td><span class='Fi-Criteria _PricingSign_1'>" + PricingSign_1 + "</span></td>";
                    //html += "<td><span class='Fi-Criteria _PricingAmt_1'>" + (PricingMethod_1 == "Disc" ? txtDisc_1_1 : txtValue_1_1 + ', ' + txtValue_1_2 + ', ' + txtValue_1_3 + ', ' + txtValue_1_4) + "</span></td>";

                    //html += "<td><span class='Fi-Criteria _PricingAddi_2'>" + (PricingMethod_2 != "" ? (Chk_Speci_Additional_1 == true ? 'Yes' : 'No') : '') + "</span></td>";
                    //html += "<td><span class='Fi-Criteria _PricingFromDate_2'>" + (PricingMethod_2 != "" ? txtFromDate : '') + "</span></td>";
                    //html += "<td><span class='Fi-Criteria _PricingToDate_2'>" + (PricingMethod_2 != "" ? txtToDate : '') + "</span></td>";
                    //html += "<td><span class='Fi-Criteria _PricingMethod_2'>" + (PricingMethod_2 != "" ? (PricingMethod_2 == "Disc" ? 'Discount' : 'Value') : '') + "</span></td>";
                    //html += "<td><span class='Fi-Criteria _PricingSign_2'>" + (PricingMethod_2 != "" ? PricingSign_2 : '') + "</span></td>";
                    //html += "<td><span class='Fi-Criteria _PricingAmt_2'>" + (PricingMethod_2 != "" ? (PricingMethod_2 == "Disc" ? txtDisc_2_1 : txtValue_2_1 + ', ' + txtValue_2_2 + ', ' + txtValue_2_3 + ', ' + txtValue_2_4) : '') + "</span></td>";

                    //html += "<td><span class='Fi-Criteria _PricingMethod_3'>" + (PricingMethod_3 == "Disc" ? 'Discount' : 'Value') + "</span></td>";
                    //html += "<td><span class='Fi-Criteria _PricingSign_3'>" + PricingSign_3 + "</span></td>";
                    //html += "<td><span class='Fi-Criteria _PricingAmt_3'>" + (PricingMethod_3 == "Disc" ? txtDisc_3_1 : txtValue_3_1 + ', ' + txtValue_3_2 + ', ' + txtValue_3_3 + ', ' + txtValue_3_4) + "</span></td>";

                    //html += "<td><span class='Fi-Criteria _PricingAddi_4'>" + (PricingMethod_4 != "" ? (Chk_Speci_Additional_2 == true ? 'Yes' : 'No') : '') + "</span></td>";
                    //html += "<td><span class='Fi-Criteria _PricingFromDate_4'>" + (PricingMethod_4 != "" ? txtFromDate1 : '') + "</span></td>";
                    //html += "<td><span class='Fi-Criteria _PricingToDate_4'>" + (PricingMethod_4 != "" ? txtToDate1 : '') + "</span></td>";
                    //html += "<td><span class='Fi-Criteria _PricingMethod_4'>" + (PricingMethod_4 != "" ? (PricingMethod_4 == "Disc" ? 'Discount' : 'Value') : '') + "</span></td>";
                    //html += "<td><span class='Fi-Criteria _PricingSign_4'>" + (PricingMethod_4 != "" ? PricingSign_4 : '') + "</span></td>";
                    //html += "<td><span class='Fi-Criteria _PricingAmt_4'>" + (PricingMethod_4 != "" ? (PricingMethod_4 == "Disc" ? txtDisc_4_1 : txtValue_4_1 + ', ' + txtValue_4_2 + ', ' + txtValue_4_3 + ', ' + txtValue_4_4) : '') + "</span></td>";



                    //html += "<td style='width: 50px'>";
                    //html += '<input type="hidden" class="hdn_UniqueId" value="' + new_id + '" />';
                    //html += '<i onclick="EditCriteria(\'' + new_id + '\');" style="cursor:pointer;" class="error EditCriteria"><img src="/Content/images/edit-icon.png" style="width: 23px;"/></i>';
                    //html += '&nbsp;&nbsp;<i style="cursor:pointer;" class="error RemoveCriteria"><img src="/Content/images/trash-delete-icon.png" style="width: 20px;"/></i>';
                    //html += "</td>";

                    //html += "</tr>";

                    html += HTML_CREATE(Supplier, Shape, Carat, Color_Type, Color, F_INTENSITY, F_OVERTONE, F_FANCY_COLOR, MixColor, Clarity, Cut, Polish, Sym, Fls, Lab, FromLength, ToLength,
                        FromWidth, ToWidth, FromDepth, ToDepth, FromDepthinPer, ToDepthinPer, FromTableinPer, ToTableinPer, FromCrAng, ToCrAng, FromCrHt, ToCrHt, FromPavAng,
                        ToPavAng, FromPavHt, ToPavHt, Keytosymbol, dCheckKTS, dUNCheckKTS, BGM, CrownBlack, TableBlack, CrownWhite, TableWhite, GoodsType, Image, Video,
                        PricingMethod_1, PricingSign_1, txtDisc_1_1, txtValue_1_1, txtValue_1_2, txtValue_1_3, txtValue_1_4, Chk_Speci_Additional_1, txtFromDate, txtToDate,
                        PricingMethod_2, PricingSign_2, txtDisc_2_1, txtValue_2_1, txtValue_2_2, txtValue_2_3, txtValue_2_4, new_id);
                });

                $("#tblFilters #tblBodyFilters").html(html);
                $("#tblFilters").show();
                $("#lblCustNoFound").hide();

                var id1 = 1;
                $("#tblFilters #tblBodyFilters tr").each(function () {
                    $(this).find("th:eq(0)").html(id1);
                    id1 += 1;
                });
            }
            else if (data.Status == "1" && data.Data.length == 0) {
                Exists_Record = 0;
                $("#tblFilters #tblBodyFilters").html("");
                $("#tblFilters").hide();
                $("#lblCustNoFound").show();
            }
            else {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                toastr.error(data.Message);
            }
            loaderHide();
        },
        error: function (xhr, textStatus, errorThrown) {
            loaderHide();
        }
    });
}
function chebox_fill(icolumnId) {
    if (icolumnId == "header") {
        $("#chebox_fillImg_Header").removeClass('img-block');
        $("#chebox_fillImg_Header").addClass('img-none');

        $("#chebox_emptyImg_Header").removeClass('img-none');
        $("#chebox_emptyImg_Header").addClass('img-block');

        $(".chebox-fill").addClass('img-none');
        $(".chebox-fill").removeClass('img-block');

        $(".chebox-empty").removeClass('img-none');
        $(".chebox-empty").addClass('img-block');

    } else {
        $("#chebox_fillImg_" + icolumnId).addClass('img-none');
        $("#chebox_fillImg_" + icolumnId).removeClass('img-block');

        $("#chebox_emptyImg_" + icolumnId).removeClass('img-none');
        $("#chebox_emptyImg_" + icolumnId).addClass('img-block');
    }
}
function chebox_empty(icolumnId) {
    if (icolumnId == "header") {
        $("#chebox_emptyImg_Header").removeClass('img-block');
        $("#chebox_emptyImg_Header").addClass('img-none');

        $("#chebox_fillImg_Header").removeClass('img-none');
        $("#chebox_fillImg_Header").addClass('img-block');

        $(".chebox-fill").removeClass('img-none');
        $(".chebox-fill").addClass('img-block');

        $(".chebox-empty").addClass('img-none');
        $(".chebox-empty").removeClass('img-block');

    } else {
        $("#chebox_fillImg_" + icolumnId).removeClass('img-none');
        $("#chebox_fillImg_" + icolumnId).addClass('img-block');

        $("#chebox_emptyImg_" + icolumnId).addClass('img-none');
        $("#chebox_emptyImg_" + icolumnId).removeClass('img-block');
    }
}
function SetTableOrder() {
    var OrderNo = 1;
    $("#mytable tbody tr").each(function () {
        ($(this).find(".ColumnOrder").text(OrderNo));
        OrderNo = OrderNo + 1;
    });
};
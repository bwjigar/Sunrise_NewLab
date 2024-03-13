var SavedSearchList = [], SavedSearchList_1 = [];
function redirectPage(page) {
    var url = "";
    if (page == "TotalStock") {
        url = "/User/SearchStock";
    }
    else if (page == "Cart") {
        url = "/User/MyCart";
    }
    else if (page == "Order") {
        url = "/User/OrderHistory";
    }
    location.href = url;
}
function GetDashboardCount() {
    loaderShow();
    setTimeout(function () {
        $.ajax({
            url: "/User/Get_DashboardCnt",
            type: "POST",
            async: false,
            data: null,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data != null && data.Data.length > 0) {
                    $('#cntTotalStock').html(formatNumber(data.Data[0].SearchStock_Count));
                    $('#cntCart').html(formatNumber(data.Data[0].MyCart_Count));
                    $('#cntOrder').html(formatNumber(data.Data[0].OrderHistory_Count));
                }
                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                loaderHide();
            }
        });
    }, 10);
}
function GetMyCart() {
    loaderShow();
    setTimeout(function () {
        $("#tblBodyCart").html("");
        var html = "";
        $.ajax({
            url: "/User/Get_MyCart_For_DashBoard",
            type: "POST",
            async: false,
            data: null,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data != null && data.Data.length > 0) {
                    for (var i = 0; i <= data.Data.length - 1; i++) {
                        html += '<tr>';
                        html += '<td><center><span class="Fi-Criteria">' + data.Data[i].Ref_No + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + data.Data[i].Shape + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + data.Data[i].Color + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + data.Data[i].Clarity + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + parseFloat(data.Data[i].Cts).toFixed(2) + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + data.Data[i].Cut + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + data.Data[i].Polish + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + data.Data[i].Symm + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + NullReplaceCommaPointDecimalToFixed(data.Data[i].Rap_Rate, 2) + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + NullReplaceCommaPointDecimalToFixed(data.Data[i].CUSTOMER_COST_DISC, 2) + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + NullReplaceCommaPointDecimalToFixed(data.Data[i].CUSTOMER_COST_VALUE, 2) + '</span></center></td>';
                        html += '</tr>';
                    }
                }
                else {
                    html += '<tr>';
                    html += '<td colspan="11"><center><span class="Fi-Criteria">No Data Found</span></center></td>';
                    html += '</tr>';
                }
                $("#tblBodyCart").html(html);
                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                loaderHide();
            }
        });
    }, 10);
}
function GetSaveSearch() {
    loaderShow();
    setTimeout(function () {
        SavedSearchList = [];
        $("#tblBodySaveSearch").html("");
        var html = "";
        $.ajax({
            url: "/User/Get_Save_Search_For_DashBoard",
            type: "POST",
            async: false,
            data: null,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data != null && data.Data.length > 0) {
                    SavedSearchList = data.Data;
                    for (var i = 0; i <= data.Data.length - 1; i++) {
                        var str = "";
                        if (data.Data[i].SupplierName != null) {
                            str += "<span style='font-weight: 600;'>Supplier :&nbsp;</span>" + data.Data[i].SupplierName;
                        }
                        if (data.Data[i].Shape != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Shape :&nbsp;</span>" + data.Data[i].Shape;
                        }
                        if (data.Data[i].Carat != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Carat :&nbsp;</span>" + data.Data[i].Carat;
                        }
                        if (data.Data[i].FromFinalDisc != null && data.Data[i].ToFinalDisc != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Discount % :&nbsp;</span>" + NullReplaceCommaPointDecimalToFixed(data.Data[i].FromFinalDisc, 2) + "-" + NullReplaceCommaPointDecimalToFixed(data.Data[i].ToFinalDisc,2);
                        }
                        if (data.Data[i].FromPriceCts != null && data.Data[i].ToPriceCts != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Final Price/Ct :&nbsp;</span>" + NullReplaceCommaPointDecimalToFixed(data.Data[i].FromPriceCts, 2) + "-" + NullReplaceCommaPointDecimalToFixed(data.Data[i].ToPriceCts,2);
                        }
                        if (data.Data[i].FromFinalVal != null && data.Data[i].ToFinalVal != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Amount :&nbsp;</span>" + NullReplaceCommaPointDecimalToFixed(data.Data[i].FromFinalVal, 2) + "-" + NullReplaceCommaPointDecimalToFixed(data.Data[i].ToFinalVal,2);
                        }
                        if (data.Data[i].ColorType != null) {
                            if (data.Data[i].ColorType == "Regular" && data.Data[i].Color == null) {
                                str += (str != "" ? ", " : "");
                                str += "<span style='font-weight: 600;'>Color :&nbsp;</span>Regular All";
                            }
                            else if (data.Data[i].ColorType == "Regular" && data.Data[i].Color != null) {
                                str += (str != "" ? ", " : "");
                                str += "<span style='font-weight: 600;'>Color :&nbsp;</span>Regular (" + data.Data[i].Color + ")";
                            }
                            else if (data.Data[i].ColorType == "Fancy" && data.Data[i].INTENSITY == null && data.Data[i].OVERTONE == null && data.Data[i].FANCY_COLOR == null) {
                                str += (str != "" ? ", " : "");
                                str += "<span style='font-weight: 600;'>Color :&nbsp;</span>Fancy All";
                            }
                            else if (data.Data[i].ColorType == "Fancy") {
                                if (data.Data[i].INTENSITY != null && data.Data[i].OVERTONE != null && data.Data[i].FANCY_COLOR != null) {
                                    str += (str != "" ? ", " : "");
                                    str += "<span style='font-weight: 600;'>Color :&nbsp;</span>Fancy (";

                                    if (data.Data[i].INTENSITY != null) {
                                        if (str.charAt(str.length - 1) != "(") {
                                            str += ", ";
                                        }
                                        str += "<span style='font-weight: 600;'>Intensity :&nbsp;</span>" + data.Data[i].INTENSITY;
                                    }
                                    if (data.Data[i].OVERTONE != null) {
                                        if (str.charAt(str.length - 1) != "(") {
                                            str += ", ";
                                        }
                                        str += "<span style='font-weight: 600;'>Overtone :&nbsp;</span>" + data.Data[i].OVERTONE;
                                    }
                                    if (data.Data[i].FANCY_COLOR != null) {
                                        if (str.charAt(str.length - 1) != "(") {
                                            str += ", ";
                                        }
                                        str += "<span style='font-weight: 600;'>Fancy Color :&nbsp;</span>" + data.Data[i].FANCY_COLOR;
                                    }
                                    str += ")";
                                }
                            }
                        }
                        if (data.Data[i].Clarity != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Clarity :&nbsp;</span>" + data.Data[i].Clarity;
                        }
                        if (data.Data[i].Cut != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Cut :&nbsp;</span>" + data.Data[i].Cut;
                        }
                        if (data.Data[i].Sym != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Symmetry :&nbsp;</span>" + data.Data[i].Sym;
                        }
                        if (data.Data[i].Polish != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Polish :&nbsp;</span>" + data.Data[i].Polish;
                        }
                        if (data.Data[i].Fls != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Fluorescence :&nbsp;</span>" + data.Data[i].Fls;
                        }
                        if (data.Data[i].BGM != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>BGM :&nbsp;</span>" + data.Data[i].BGM;
                        }
                        if (data.Data[i].Image != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Image :&nbsp;</span>" + data.Data[i].Image;
                        }
                        if (data.Data[i].Video != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Video :&nbsp;</span>" + data.Data[i].Video;
                        }
                        if (data.Data[i].Certi != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Certi :&nbsp;</span>" + data.Data[i].Certi;
                        }
                        if (data.Data[i].Lab != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Lab :&nbsp;</span>" + data.Data[i].Lab;
                        }
                        if (data.Data[i].KTS_IsBlank != 0 || data.Data[i].CheckKTS != null || data.Data[i].UNCheckKTS != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Key to Symbol :&nbsp;</span>";

                            if (data.Data[i].KTS_IsBlank == 1) {
                                str += "BLANK";
                            }
                            if (data.Data[i].CheckKTS != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                str += "<span style='color:green;'>" + data.Data[i].CheckKTS + "</span>";
                            }
                            if (data.Data[i].UNCheckKTS != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                if (data.Data[i].CheckKTS != null) {
                                    str += " ";
                                }
                                str += "<span style='color:red;'>" + data.Data[i].UNCheckKTS + "</span>";
                            }
                        }
                        if (data.Data[i].RComment_IsBlank != 0 || data.Data[i].CheckRComment != null || data.Data[i].UNCheckRComment != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Report Comments :&nbsp;</span>";

                            if (data.Data[i].RComment_IsBlank == 1) {
                                str += "BLANK";
                            }
                            if (data.Data[i].CheckRComment != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                str += "<span style='color:green;'>" + data.Data[i].CheckRComment + "</span>";
                            }
                            if (data.Data[i].UNCheckRComment != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                if (data.Data[i].CheckRComment != null) {
                                    str += " ";
                                }
                                str += "<span style='color:red;'>" + data.Data[i].UNCheckRComment + "</span>";
                            }
                        }
                        if (data.Data[i].Culet != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Culet :&nbsp;</span>" + data.Data[i].Culet;
                        }
                        if (data.Data[i].Location != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Location :&nbsp;</span>" + data.Data[i].Location;
                        }
                        if (data.Data[i].Length_IsBlank != 0 || (data.Data[i].FromLength != null && data.Data[i].ToLength != null)) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Length :&nbsp;</span>";

                            if (data.Data[i].Length_IsBlank == 1) {
                                str += "BLANK";
                            }
                            if (data.Data[i].FromLength != null && data.Data[i].ToLength != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                str += data.Data[i].FromLength + "-" + data.Data[i].ToLength;
                            }
                        }
                        if (data.Data[i].Width_IsBlank != 0 || (data.Data[i].FromWidth != null && data.Data[i].ToWidth != null)) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Width :&nbsp;</span>";

                            if (data.Data[i].Width_IsBlank == 1) {
                                str += "BLANK";
                            }
                            if (data.Data[i].FromWidth != null && data.Data[i].ToWidth != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                str += data.Data[i].FromWidth + "-" + data.Data[i].ToWidth;
                            }
                        }
                        if (data.Data[i].Depth_IsBlank != 0 || (data.Data[i].FromDepth != null && data.Data[i].ToDepth != null)) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Depth :&nbsp;</span>";

                            if (data.Data[i].Depth_IsBlank == 1) {
                                str += "BLANK";
                            }
                            if (data.Data[i].FromDepth != null && data.Data[i].ToDepth != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                str += data.Data[i].FromDepth + "-" + data.Data[i].ToDepth;
                            }
                        }
                        if (data.Data[i].DepthPer_IsBlank != 0 || (data.Data[i].FromDepthinPer != null && data.Data[i].ToDepthinPer != null)) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Depth % :&nbsp;</span>";

                            if (data.Data[i].DepthPer_IsBlank == 1) {
                                str += "BLANK";
                            }
                            if (data.Data[i].FromDepthinPer != null && data.Data[i].ToDepthinPer != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                str += data.Data[i].FromDepthinPer + "-" + data.Data[i].ToDepthinPer;
                            }
                        }
                        if (data.Data[i].TablePer_IsBlank != 0 || (data.Data[i].FromTableinPer != null && data.Data[i].ToTableinPer != null)) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Table % :&nbsp;</span>";

                            if (data.Data[i].TablePer_IsBlank == 1) {
                                str += "BLANK";
                            }
                            if (data.Data[i].FromTableinPer != null && data.Data[i].ToTableinPer != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                str += data.Data[i].FromTableinPer + "-" + data.Data[i].ToTableinPer;
                            }
                        }
                        if (data.Data[i].GirdlePer_IsBlank != 0 || (data.Data[i].FromGirdlePer != null && data.Data[i].ToGirdlePer != null)) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Girdle % :&nbsp;</span>";

                            if (data.Data[i].GirdlePer_IsBlank == 1) {
                                str += "BLANK";
                            }
                            if (data.Data[i].FromGirdlePer != null && data.Data[i].ToGirdlePer != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                str += data.Data[i].FromGirdlePer + "-" + data.Data[i].ToGirdlePer;
                            }
                        }
                        if (data.Data[i].CrAng_IsBlank != 0 || (data.Data[i].FromCrAng != null && data.Data[i].ToCrAng != null)) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Crown Angle :&nbsp;</span>";

                            if (data.Data[i].CrAng_IsBlank == 1) {
                                str += "BLANK";
                            }
                            if (data.Data[i].FromCrAng != null && data.Data[i].ToCrAng != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                str += data.Data[i].FromCrAng + "-" + data.Data[i].ToCrAng;
                            }
                        }
                        if (data.Data[i].CrHt_IsBlank != 0 || (data.Data[i].FromCrHt != null && data.Data[i].ToCrHt != null)) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Crown Height :&nbsp;</span>";

                            if (data.Data[i].CrHt_IsBlank == 1) {
                                str += "BLANK";
                            }
                            if (data.Data[i].FromCrHt != null && data.Data[i].ToCrHt != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                str += data.Data[i].FromCrHt + "-" + data.Data[i].ToCrHt;
                            }
                        }
                        if (data.Data[i].PavAng_IsBlank != 0 || (data.Data[i].FromPavAng != null && data.Data[i].ToPavAng != null)) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Pavilion Angle :&nbsp;</span>";

                            if (data.Data[i].PavAng_IsBlank == 1) {
                                str += "BLANK";
                            }
                            if (data.Data[i].FromPavAng != null && data.Data[i].ToPavAng != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                str += data.Data[i].FromPavAng + "-" + data.Data[i].ToPavAng;
                            }
                        }
                        if (data.Data[i].PavHt_IsBlank != 0 || (data.Data[i].FromPavHt != null && data.Data[i].ToPavHt != null)) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Pav Height :&nbsp;</span>";

                            if (data.Data[i].PavHt_IsBlank == 1) {
                                str += "BLANK";
                            }
                            if (data.Data[i].FromPavHt != null && data.Data[i].ToPavHt != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                str += data.Data[i].FromPavHt + "-" + data.Data[i].ToPavHt;
                            }
                        }
                        if (data.Data[i].StarLength_IsBlank != 0 || (data.Data[i].FromStarLength != null && data.Data[i].ToStarLength != null)) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Star Length :&nbsp;</span>";

                            if (data.Data[i].StarLength_IsBlank == 1) {
                                str += "BLANK";
                            }
                            if (data.Data[i].FromStarLength != null && data.Data[i].ToStarLength != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                str += data.Data[i].FromStarLength + "-" + data.Data[i].ToStarLength;
                            }
                        }
                        if (data.Data[i].LowerHalf_IsBlank != 0 || (data.Data[i].FromLowerHalf != null && data.Data[i].ToLowerHalf != null)) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Lower Half :&nbsp;</span>";

                            if (data.Data[i].LowerHalf_IsBlank == 1) {
                                str += "BLANK";
                            }
                            if (data.Data[i].FromLowerHalf != null && data.Data[i].ToLowerHalf != null) {
                                if (str.charAt(str.length - 1) == "K") {
                                    str += " & ";
                                }
                                str += data.Data[i].FromLowerHalf + "-" + data.Data[i].ToLowerHalf;
                            }
                        }


                        if (data.Data[i].TableBlack != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Table Black :&nbsp;</span>" + data.Data[i].TableBlack;
                        }
                        if (data.Data[i].CrownBlack != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Crown Black :&nbsp;</span>" + data.Data[i].CrownBlack;
                        }
                        if (data.Data[i].TableWhite != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Table White :&nbsp;</span>" + data.Data[i].TableWhite;
                        }
                        if (data.Data[i].CrownWhite != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Crown White :&nbsp;</span>" + data.Data[i].CrownWhite;
                        }
                        if (data.Data[i].TableOpen != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Table Open :&nbsp;</span>" + data.Data[i].TableOpen;
                        }
                        if (data.Data[i].CrownOpen != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Crown Open :&nbsp;</span>" + data.Data[i].CrownOpen;
                        }
                        if (data.Data[i].PavillionOpen != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Pav Open :&nbsp;</span>" + data.Data[i].PavillionOpen;
                        }
                        if (data.Data[i].GirdleOpen != null) {
                            str += (str != "" ? ", " : "");
                            str += "<span style='font-weight: 600;'>Girdle Open :&nbsp;</span>" + data.Data[i].GirdleOpen;
                        }

                        html += '<tr>';
                        html += '<td><center><span class="Fi-Criteria">' + data.Data[i].TransDate + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + data.Data[i].SearchName + '</span></center></td>';
                        html += '<td>' +
                            '<center>' +
                            '<span onclick="SaveSearch_LoadSearchData(' + data.Data[i].Id + ',\'Show\')" class="Fi-Criteria" style="cursor:pointer;" data-toggle="tooltip" data-html="true" data-placement="left" title="' + str + '">' +
                            'Search' +
                            '</span>' +
                            '</center>' +
                            '</td>';
                        html += '</tr>';
                    }
                }
                else {
                    html += '<tr>';
                    html += '<td colspan="3"><center><span class="Fi-Criteria">No Data Found</span></center></td>';
                    html += '</tr>';
                }
                $("#tblBodySaveSearch").html(html);
                $(function () {
                    $('[data-toggle="tooltip"]').tooltip()
                })
                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                loaderHide();
            }
        });
    }, 10);
}
function GetOrderHistory() {
    loaderShow();
    setTimeout(function () {
        $("#tblBodyOrder").html("");
        var html = "";
        $.ajax({
            url: "/User/Get_OrderHistory_For_DashBoard",
            type: "POST",
            async: false,
            data: null,
            success: function (data, textStatus, jqXHR) {
                if (data.Message.indexOf('Something Went wrong') > -1) {
                    MoveToErrorPage(0);
                }
                if (data.Data != null && data.Data.length > 0) {
                    for (var i = 0; i <= data.Data.length - 1; i++) {
                        html += '<tr>';
                        html += '<td><center><span onclick="OrderHistory_LoadSearchData(' + data.Data[i].OrderId + ')" style="cursor:pointer;" class="Fi-Criteria order">&nbsp;' + data.Data[i].OrderId + '&nbsp;</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + data.Data[i].OrderDate + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + data.Data[i].CompName + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + breakString(data.Data[i].Remarks,65) + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + data.Data[i].TotPcs + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + parseFloat(data.Data[i].TotCts).toFixed(2) + '</span></center></td>';
                        html += '<td><center><span class="Fi-Criteria">' + NullReplaceCommaPointDecimalToFixed(data.Data[i].TotAmt, 2) + '</span></center></td>';
                        html += '</tr>';
                    }
                }
                else {
                    html += '<tr>';
                    html += '<td colspan="7"><center><span class="Fi-Criteria">No Data Found</span></center></td>';
                    html += '</tr>';
                }
                $("#tblBodyOrder").html(html);
                loaderHide();
            },
            error: function (jqXHR, textStatus, errorThrown) {
                loaderHide();
            }
        });
    }, 10);
}
function SaveSearch_LoadSearchData(Id) {
    SavedSearchList_1 = [];
    for (var i = 0; i <= SavedSearchList.length - 1; i++) {
        if (SavedSearchList[i].Id == Id) {
            SavedSearchList_1 = SavedSearchList[i];
        }
    }
    var obj = {};
    if (SavedSearchList_1.Id > 0) {
        obj.SearchID = SavedSearchList_1.Id;
        obj.SearchName = SavedSearchList_1.SearchName;
        obj.Culet = SavedSearchList_1.Culet;
        obj.Location = SavedSearchList_1.Location;
        obj.SupplierId = SavedSearchList_1.SupplierId;
        obj.Shape = SavedSearchList_1.Shape;
        obj.Pointer = SavedSearchList_1.Carat;
        obj.ColorType = SavedSearchList_1.ColorType;
        if (SavedSearchList_1.ColorType == "Fancy") {
            obj.Color = null;
            obj.INTENSITY = SavedSearchList_1.INTENSITY;
            obj.OVERTONE = SavedSearchList_1.OVERTONE;
            obj.FANCY_COLOR = SavedSearchList_1.FANCY_COLOR;
        }
        else if (SavedSearchList_1.ColorType == "Regular") {
            obj.Color = SavedSearchList_1.Color;
            obj.INTENSITY = null;
            obj.OVERTONE = null;
            obj.FANCY_COLOR = null;
        }

        obj.Clarity = SavedSearchList_1.Clarity;
        obj.Cut = SavedSearchList_1.Cut;
        obj.Polish = SavedSearchList_1.Polish;
        obj.Symm = SavedSearchList_1.Sym;
        obj.Fls = SavedSearchList_1.Fls;
        obj.BGM = SavedSearchList_1.BGM;
        obj.Lab = SavedSearchList_1.Lab;
        obj.CrownBlack = SavedSearchList_1.CrownBlack;
        obj.TableBlack = SavedSearchList_1.TableBlack;
        obj.CrownWhite = SavedSearchList_1.CrownWhite;
        obj.TableWhite = SavedSearchList_1.TableWhite;

        obj.TableOpen = SavedSearchList_1.TableOpen;
        obj.CrownOpen = SavedSearchList_1.CrownOpen;
        obj.PavOpen = SavedSearchList_1.PavillionOpen;
        obj.GirdleOpen = SavedSearchList_1.GirdleOpen;

        obj.KTSBlank = (SavedSearchList_1.KTS_IsBlank == true ? true : "");
        obj.Keytosymbol = (SavedSearchList_1.CheckKTS != null ? SavedSearchList_1.CheckKTS : "") + '-' + (SavedSearchList_1.UNCheckKTS != null ? SavedSearchList_1.UNCheckKTS : "");
        obj.CheckKTS = SavedSearchList_1.CheckKTS;
        obj.UNCheckKTS = SavedSearchList_1.UNCheckKTS;

        obj.RCommentBlank = (SavedSearchList_1.RComment_IsBlank == true ? true : "");
        obj.RComment = (SavedSearchList_1.CheckRComment != null ? SavedSearchList_1.CheckRComment : "") + '-' + (SavedSearchList_1.UNCheckRComment != null ? SavedSearchList_1.UNCheckRComment : "");
        obj.CheckRComment = SavedSearchList_1.CheckRComment;
        obj.UNCheckRComment = SavedSearchList_1.UNCheckRComment;

        obj.FromDisc = SavedSearchList_1.FromFinalDisc;
        obj.ToDisc = SavedSearchList_1.ToFinalDisc;

        obj.FromPriceCts = SavedSearchList_1.FromPriceCts;
        obj.ToPriceCts = SavedSearchList_1.ToPriceCts;

        obj.FromTotAmt = SavedSearchList_1.FromFinalVal;
        obj.ToTotAmt = SavedSearchList_1.ToFinalVal;

        obj.LengthBlank = (SavedSearchList_1.Length_IsBlank == true ? true : "");
        obj.FromLength = SavedSearchList_1.FromLength;
        obj.ToLength = SavedSearchList_1.ToLength;

        obj.WidthBlank = (SavedSearchList_1.Width_IsBlank == true ? true : "");
        obj.FromWidth = SavedSearchList_1.FromWidth;
        obj.ToWidth = SavedSearchList_1.ToWidth;

        obj.DepthBlank = (SavedSearchList_1.Depth_IsBlank == true ? true : "");
        obj.FromDepth = SavedSearchList_1.FromDepth;
        obj.ToDepth = SavedSearchList_1.ToDepth;

        obj.DepthPerBlank = (SavedSearchList_1.DepthPer_IsBlank == true ? true : "");
        obj.FromDepthPer = SavedSearchList_1.FromDepthinPer;
        obj.ToDepthPer = SavedSearchList_1.ToDepthinPer;

        obj.TablePerBlank = (SavedSearchList_1.TablePer_IsBlank == true ? true : "");
        obj.FromTablePer = SavedSearchList_1.FromTableinPer;
        obj.ToTablePer = SavedSearchList_1.ToTableinPer;

        obj.GirdlePerBlank = (SavedSearchList_1.GirdlePer_IsBlank == true ? true : "");
        obj.FromGirdlePer = SavedSearchList_1.FromGirdlePer;
        obj.ToGirdlePer = SavedSearchList_1.ToGirdlePer;

        obj.StarLengthBlank = (SavedSearchList_1.StarLength_IsBlank == true ? true : "");
        obj.FromStarLength = SavedSearchList_1.FromStarLength;
        obj.ToStarLength = SavedSearchList_1.ToStarLength;

        obj.LowerHalfBlank = (SavedSearchList_1.LowerHalf_IsBlank == true ? true : "");
        obj.FromLowerHalf = SavedSearchList_1.FromLowerHalf;
        obj.ToLowerHalf = SavedSearchList_1.ToLowerHalf;


        obj.Img = SavedSearchList_1.Image != null ? "YES" : "";
        obj.Vdo = SavedSearchList_1.Video != null ? "YES" : "";
        obj.Certi = SavedSearchList_1.Certi != null ? "YES" : "";

        obj.CrAngBlank = (SavedSearchList_1.CrAng_IsBlank == true ? true : "");
        obj.FromCrAng = SavedSearchList_1.FromCrAng;
        obj.ToCrAng = SavedSearchList_1.ToCrAng;

        obj.CrHtBlank = (SavedSearchList_1.CrHt_IsBlank == true ? true : "");
        obj.FromCrHt = SavedSearchList_1.FromCrHt;
        obj.ToCrHt = SavedSearchList_1.ToCrHt;

        obj.PavAngBlank = (SavedSearchList_1.PavAng_IsBlank == true ? true : "");
        obj.FromPavAng = SavedSearchList_1.FromPavAng;
        obj.ToPavAng = SavedSearchList_1.ToPavAng;

        obj.PavHtBlank = (SavedSearchList_1.PavHt_IsBlank == true ? true : "");
        obj.FromPavHt = SavedSearchList_1.FromPavHt;
        obj.ToPavHt = SavedSearchList_1.ToPavHt;
        obj.Type = "Buyer List";

        loaderShow();
        $.ajax({
            url: "/User/SavedSearchDataSessionStore",
            async: false,
            type: "POST",
            data: { obj: obj },
            success: function (data, textStatus, jqXHR) {
                loaderHide();
                window.location.href = '/User/SearchStock?type=SaveSearch';
            },
            error: function (jqXHR, textStatus, errorThrown) {
                loaderHide();
            }
        });
    }
}
function OrderHistory_LoadSearchData(OrderId) {
    var obj = {};
    obj.OrderId = OrderId;

    loaderShow();
    $.ajax({
        url: "/User/OrderHistory_OrderDataSessionStore",
        async: false,
        type: "POST",
        data: { obj: obj },
        success: function (data, textStatus, jqXHR) {
            loaderHide();
            window.location.href = '/User/OrderHistory';
        },
        error: function (jqXHR, textStatus, errorThrown) {
            loaderHide();
        }
    });
}
function breakString(str, nxt) {
    // Use regular expression to break the string every 45 characters, preserving whole words
    const regex = new RegExp(`.{1,` + nxt +`}(\\s|$)`, 'g');
    const splitString = str.match(regex);
    var har="";
    // Now 'splitString' is an array of substrings, each containing up to 45 characters without splitting words
    splitString.forEach(part => {
        har += part +"<br/>"
    });
    return har.substring(0, har.length - 5); ;
}
function formatNumber(number) {
    return (parseInt(number)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}
function formatNumber_with_point(number) {
    return (parseFloat(number).toFixed(2)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
}


$(document).ready(function (e) {
    $("#body").addClass("bg-color");
    $("#li_Dashboard").addClass("menuActive");
    GetDashboardCount();
    GetMyCart();
    GetSaveSearch();
    GetOrderHistory();
});
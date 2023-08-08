function openTab(MenuName) {
    $(".tab").show();
    $(".tablinks").removeClass("active");
    $(".tabcontent").removeClass("active");
    $(".tabcontent").hide();
    $("." + MenuName).addClass("active");
    $("#" + MenuName).show();
    if (MenuName == "RefNoPrefix") {
        //GetSearch();
    }
    else if (MenuName == "SupplierDisc") {
        UpdateCancelRow();
        Get_Supplier_Disc();
    }
}
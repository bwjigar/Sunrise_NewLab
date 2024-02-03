using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunriseLabWeb_New.Data
{
    public class Constants
    {
        public static string UserLogin = "/Login/Login";
        public static string ForgotPassword = "/User/ForgotPassword";

        public static string KeyAccountData = "/Login/GetKeyAccountData";
        public static string IP_Wise_Login_Detail = "/Login/IP_Wise_Login_Detail";
        public static string LogoutWithoutToken = "/Login/LogoutWithoutToken";

        public static string Get_DashboardCnt = "/User/Get_DashboardCnt";
        public static string Get_MyCart_For_DashBoard = "/User/Get_MyCart_For_DashBoard";
        public static string Get_OrderHistory_For_DashBoard = "/User/Get_OrderHistory_For_DashBoard";
        public static string Get_Save_Search_For_DashBoard = "/User/Get_Save_Search_For_DashBoard";
        

        public static string FortunePartyCode_Exist = "/User/FortunePartyCode_Exist";
        public static string UserCode_Exists = "/User/UserCode_Exists";
        public static string SaveUserData = "/User/SaveUserData";
        public static string Delete_UserMas = "/User/Delete_UserMas";
        public static string get_UserType = "/User/get_UserType";

        public static string GetUsers = "/User/GetUsers";
        public static string Get_CategoryMas = "/User/Get_CategoryMas";
        public static string AddUpdate_CategoryMas = "/User/AddUpdate_CategoryMas";
        public static string Get_Category_Value = "/User/Get_Category_Value";
        public static string AddUpdate_Category_Value = "/User/AddUpdate_Category_Value";
        public static string Get_PriceListCategory = "/User/Get_PriceListCategory";

        public static string Get_Supplier_Value = "/User/Get_Supplier_Value";
        public static string AddUpdate_Supplier_Value = "/User/AddUpdate_Supplier_Value";
        public static string Delete_Supplier_Value = "/User/Delete_Supplier_Value";


        public static string Get_CategoryDet = "/User/Get_CategoryDet";
        public static string AddUpdate_CategoryDet = "/User/AddUpdate_CategoryDet";
        public static string Delete_CategoryDet = "/User/Delete_CategoryDet";
        
        public static string Get_Supplier_ForSearchStock = "/User/Get_Supplier_ForSearchStock";
        public static string Get_SupplierMaster = "/User/Get_SupplierMaster";
        public static string AddUpdate_SupplierMaster = "/User/AddUpdate_SupplierMaster";
        public static string Get_Not_Mapped_SupplierStock = "/User/Get_Not_Mapped_SupplierStock";
        public static string Get_ColumnMaster = "/User/Get_ColumnMaster";
        public static string Get_SupplierColumnSetting = "/User/Get_SupplierColumnSetting";
        public static string Get_SupplierColumnSetting_FromAPI = "/User/Get_SupplierColumnSetting_FromAPI";
        public static string AddUpdate_SupplierColumnSetting = "/User/AddUpdate_SupplierColumnSetting";
        public static string Delete_SupplierColumnSetting = "/User/Delete_SupplierColumnSetting";

        public static string Get_SheetName_From_File = "/User/Get_SheetName_From_File";
        public static string Get_Data_From_File = "/User/Get_Data_From_File";
        
        public static string Get_SupplierColumnSetting_FromFile = "/User/Get_SupplierColumnSetting_FromFile";
        public static string AddUpdate_SupplierColumnSetting_FromFile = "/User/AddUpdate_SupplierColumnSetting_FromFile";
        public static string Delete_SupplierColumnSetting_FromFile = "/User/Delete_SupplierColumnSetting_FromFile";
        public static string AddUpdate_SupplierStock_FromFile = "/User/AddUpdate_SupplierStock_FromFile";
        public static string Add_Stock_FileUpload_Request = "/User/Add_Stock_FileUpload_Request";
        


        public static string Get_FancyColor = "/User/Get_FancyColor";

        public static string AddUpdate_SupplierStock = "/User/AddUpdate_SupplierStock";
        public static string Add_StockUpload_Response = "/User/Add_StockUpload_Response";
        public static string Get_StockUpload_Response = "/User/Get_StockUpload_Response";
        
        public static string PlaceOrder = "/User/PlaceOrder";
        public static string Get_Company_PlaceOrder = "/User/Get_Company_PlaceOrder";
        public static string Get_OrderHistory = "/User/Get_OrderHistory";
        public static string Excel_OrderHistory = "/User/Excel_OrderHistory";
        
        public static string Get_LabEntry = "/User/Get_LabEntry";

        public static string Add_LabEntry_Request = "/User/Add_LabEntry_Request";
        public static string Save_LabEntry = "/User/Save_LabEntry";
        public static string Excel_LabEntry = "/User/Excel_LabEntry";
        public static string Add_MyCart = "/User/Add_MyCart";
        public static string Delete_MyCart = "/User/Delete_MyCart";
        public static string Get_MyCart = "/User/Get_MyCart";
        public static string Excel_MyCart = "/User/Excel_MyCart";

        public static string Get_LabAvailibility = "/User/Get_LabAvailibility";
        public static string Excel_LabAvailibility = "/User/Excel_LabAvailibility";
        
        public static string Add_Customer_Stock_Disc_Mas_Request = "/User/Add_Customer_Stock_Disc_Mas_Request";
        public static string Get_URL = "/User/Get_URL";



        
        public static string Auto_Excel_Download = "/User/Auto_Excel_Download";
        public static string RapaPort_Data_Upload_Ora = "/User/RapaPort_Data_Upload_Ora";
        public static string get_stock_disc_Ora = "/User/get_stock_disc_Ora";
        public static string get_stock_kts_Ora = "/User/get_stock_kts_Ora";
        public static string get_sal_disc_new_Ora = "/User/get_sal_disc_new_Ora";
        public static string get_sal_clg_new_Ora = "/User/get_sal_clg_new_Ora";
        public static string get_pur_disc_Ora = "/User/get_pur_disc_Ora";
        public static string lab_entry_notification_Ora = "/User/lab_entry_notification_Ora";

        public static string Get_ColumnSetting_UserWise = "/User/Get_ColumnSetting_UserWise";
        public static string AddUpdate_ColumnSetting_UserWise = "/User/AddUpdate_ColumnSetting_UserWise";
        public static string Get_SearchStock_ColumnSetting = "/User/Get_SearchStock_ColumnSetting";

        public static string Get_ParaMas = "/User/Get_ParaMas";
        public static string Get_PriceList_ParaMas = "/User/Get_PriceList_ParaMas";
        public static string get_key_to_symbol = "/User/get_key_to_symbol";
        public static string Get_Supplier_RefNo_Prefix = "/User/Get_Supplier_RefNo_Prefix";
        public static string AddUpdate_Supplier_RefNo_Prefix = "/User/AddUpdate_Supplier_RefNo_Prefix";
        public static string Delete_Supplier_RefNo_Prefix = "/User/Delete_Supplier_RefNo_Prefix";

        public static string AddUpdate_Supplier_Disc = "/User/AddUpdate_Supplier_Disc";
        public static string Get_Supplier_Disc = "/User/Get_Supplier_Disc";

        public static string Get_SearchStock = "/User/Get_SearchStock";
        public static string Excel_SearchStock = "/User/Excel_SearchStock";
        public static string Get_Auto_Excel_Download = "/User/Get_Auto_Excel_Download";
        public static string Email_SearchStock = "/User/Email_SearchStock";
        public static string AddUpdate_Save_Search = "/User/AddUpdate_Save_Search";


        public static string AddUpdate_Customer_Disc = "/User/AddUpdate_Customer_Disc";
        public static string Get_Customer_Disc = "/User/Get_Customer_Disc";

        public static string AddUpdate_Customer_Stock_Disc = "/User/AddUpdate_Customer_Stock_Disc";
        public static string Get_Customer_Stock_Disc = "/User/Get_Customer_Stock_Disc";
        public static string Get_Customer_Stock_Disc_Mas = "/User/Get_Customer_Stock_Disc_Mas";
        public static string Get_Customer_Stock_Disc_Count = "/User/Get_Customer_Stock_Disc_Count";
        

        public static string GetUserProfilePicture = "/User/GetUserProfilePicture";

        public static string LabDataUpload_Ora = "/LabStock/LabDataUpload_Ora";
        public static string LabStockDataDelete = "/LabStock/LabStockDataDelete";
        public static string LabStockStatusGet = "/LabStock/LabStockStatusGet";
        public static string LabData_ExcelGen = "/LabStock/LabData_ExcelGen";
        public static string LabDataGetURLApi = "/LabStock/LabDataGetURLApi";

        public static string GetSearchParameter = "/LabStock/GetListValue";
        public static string GetUserMas = "/LabStock/GetUserMas";
        public static string UserwiseCompany_select = "/LabStock/UserwiseCompany_select";
        public static string GetApiColumnsDetails = "/LabStock/GetApiColumnsDetails";
        public static string Lab_Column_Auto_Select = "/LabStock/Lab_Column_Auto_Select";
        public static string VendorInfo = "/LabStock/GetVendorInfo";
        public static string GetKeyToSymbolList = "/LabStock/GetKeyToSymbol";

        public static string SaveLab = "/LabStock/SaveLab";
        public static string GetLab = "/LabStock/GetLab";

    }
}
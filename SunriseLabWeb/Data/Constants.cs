using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SunriseLabWeb_New.Data
{
    public class Constants
    {
        public static string UserLogin = "/Login/Login";
        public static string KeyAccountData = "/Login/GetKeyAccountData";
        public static string IP_Wise_Login_Detail = "/Login/IP_Wise_Login_Detail";
        public static string LogoutWithoutToken = "/Login/LogoutWithoutToken";

        
        public static string FortunePartyCode_Exist = "/User/FortunePartyCode_Exist";
        public static string SaveUserData = "/User/SaveUserData";
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
        public static string Get_SupplierMaster = "/User/Get_SupplierMaster";
        public static string AddUpdate_SupplierMaster = "/User/AddUpdate_SupplierMaster";
        public static string Get_ColumnMaster = "/User/Get_ColumnMaster";
        public static string Get_SupplierColumnSetting = "/User/Get_SupplierColumnSetting";
        public static string Get_SupplierColumnSetting_FromAPI = "/User/Get_SupplierColumnSetting_FromAPI";
        public static string AddUpdate_SupplierColumnSetting = "/User/AddUpdate_SupplierColumnSetting";
        public static string Delete_SupplierColumnSetting = "/User/Delete_SupplierColumnSetting";
        
        public static string Get_Data_From_File = "/User/Get_Data_From_File";
        public static string Get_SupplierColumnSetting_FromFile = "/User/Get_SupplierColumnSetting_FromFile";
        public static string AddUpdate_SupplierColumnSetting_FromFile = "/User/AddUpdate_SupplierColumnSetting_FromFile";
        public static string Delete_SupplierColumnSetting_FromFile = "/User/Delete_SupplierColumnSetting_FromFile";
        public static string AddUpdate_SupplierStock_FromFile = "/User/AddUpdate_SupplierStock_FromFile";

        

        public static string Get_FancyColor = "/User/Get_FancyColor";

        public static string AddUpdate_SupplierStock = "/User/AddUpdate_SupplierStock";

        public static string Get_ParaMas = "/User/Get_ParaMas";
        public static string Get_PriceList_ParaMas = "/User/Get_PriceList_ParaMas";
        public static string get_key_to_symbol = "/User/get_key_to_symbol";
        public static string Get_Supplier_RefNo_Prefix = "/User/Get_Supplier_RefNo_Prefix";
        public static string AddUpdate_Supplier_RefNo_Prefix = "/User/AddUpdate_Supplier_RefNo_Prefix";
        public static string Delete_Supplier_RefNo_Prefix = "/User/Delete_Supplier_RefNo_Prefix";

        public static string AddUpdate_Supplier_Disc = "/User/AddUpdate_Supplier_Disc";
        public static string Get_Supplier_Disc = "/User/Get_Supplier_Disc";
        
        public static string Get_SearchStock = "/User/Get_SearchStock";
        public static string AddUpdate_Customer_Disc = "/User/AddUpdate_Customer_Disc";
        public static string Get_Customer_Disc = "/User/Get_Customer_Disc";

        public static string AddUpdate_Customer_Stock_Disc = "/User/AddUpdate_Customer_Stock_Disc";
        public static string Get_Customer_Stock_Disc = "/User/Get_Customer_Stock_Disc";

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
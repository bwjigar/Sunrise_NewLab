using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model
{
    public class Get_DashboardCnt_Res
    {
        public int MyCart_Count { get; set; }
        public int SearchStock_Count { get; set; }
        public int OrderHistory_Count { get; set; }
    }
    public class GetUsers_Req
    {
        public string OrderBy { get; set; }
        public int PgNo { get; set; }
        public int PgSize { get; set; }
        public string FilterType { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FortunePartyCode { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string CompName { get; set; }
        public string IsActive { get; set; }
        public int UserType { get; set; }
        public int UserId { get; set; }
        public string CompanyUserCustomer { get; set; }
        public int Assist_UserId { get; set; }
        public bool URL_Exists { get; set; }
    }
    public class GetUsers_Res
    {
        public int iTotalRec { get; set; }
        public long iSr { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompName { get; set; }
        public int FortunePartyCode { get; set; }
        public string EmailId { get; set; }
        public string EmailId_2 { get; set; }
        public string MobileNo { get; set; }
        public string UserTypeId { get; set; }
        public string UserType { get; set; }
        public int AssistBy { get; set; }
        public int SubAssistBy { get; set; }
        public string AssistByName { get; set; }
        public string SubAssistByName { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string LastLoginDate { get; set; }
        public string UserTypeList { get; set; }
        public int UserCode { get; set; }
        public bool View { get; set; }
        public bool Download { get; set; }
        public int StockDiscMgt_Count { get; set; }
        public string URL { get; set; }
    }
    public class Exist_Request
    {
        public int iUserId { get; set; }
        public int FortunePartyCode { get; set; }
        public int UserCode { get; set; }
    }
    public class UserDetails_Req
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Active { get; set; }
        public string UserType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public int FortunePartyCode { get; set; }
        public string EmailId { get; set; }
        public string EmailId_2 { get; set; }
        public string MobileNo { get; set; }
        public int AssistBy { get; set; }
        public int SubAssistBy { get; set; }
        public int UserCode { get; set; }
        public bool View { get; set; }
        public bool Download { get; set; }
    }
    public class UserType_Res
    {
        public int Id { get; set; }
        public string UserType { get; set; }
    }

    public class Get_CategoryMas_Req
    {
        public string OrderBy { get; set; }
        public int PgNo { get; set; }
        public int PgSize { get; set; }
        public bool Status { get; set; }
        public string Not_Col_Id { get; set; }
    }
    public class Get_CategoryMas_Res
    {
        public int iTotalRec { get; set; }
        public long iSr { get; set; }
        public int Cat_Id { get; set; }
        public string Column_Name { get; set; }
        public string Display_Name { get; set; }
        public bool Status { get; set; }
        public int Col_Id { get; set; }
        public string Col_Column_Name { get; set; }
    }
    public class Get_Category_Value_Req
    {
        public string OrderBy { get; set; }
        public int PgNo { get; set; }
        public int PgSize { get; set; }
        public string Cat_Name { get; set; }
        public int Cat_Id { get; set; }
        public int Col_Id { get; set; }
        public bool Status { get; set; }
    }
    public class Get_Category_Value_Res
    {
        public int iTotalRec { get; set; }
        public long iSr { get; set; }
        public int Cat_V_Id { get; set; }
        public string Cat_Name { get; set; }
        public string Group_Name { get; set; }
        public string Rapaport_Name { get; set; }
        public string Rapnet_name { get; set; }
        public string Synonyms { get; set; }
        public decimal Order_No { get; set; }
        public int Sort_No { get; set; }
        public bool Status { get; set; }
        public string Icon_Url { get; set; }
        public int Cat_Id { get; set; }
        public string Cat_Column_Name { get; set; }
        public string Display_Name { get; set; }
        public string Short_Name { get; set; }
        public int Col_Id { get; set; }
        public string Col_Column_Name { get; set; }
    }
    public class Get_PriceListCategory_Res
    {
        public long Id { get; set; }
        public string Value { get; set; }
        public int SORT_NO { get; set; }
        public string Type { get; set; }
        public bool isActive { get; set; }
        public int Col_Id { get; set; }
        public string Icon_Url { get; set; }
    }

    public class Get_Supplier_Value_Req
    {
        public int Sup_Id { get; set; }
        public int Col_Id { get; set; }
    }
    public class Get_Supplier_Value_Res
    {
        public long iSr { get; set; }
        public long Sr { get; set; }
        public int Sup_Id { get; set; }
        public int Cat_V_Id { get; set; }
        public string Supp_Cat_name { get; set; }
        public bool Status { get; set; }
        public int Cat_Id { get; set; }
        public string Column_Name { get; set; }
    }
    public class AddUpdate_Supplier_Value_Req
    {
        public List<Obj_AddUpdate_Supplier_Value_List> sup_val { get; set; }
        public AddUpdate_Supplier_Value_Req()
        {
            sup_val = new List<Obj_AddUpdate_Supplier_Value_List>();
        }
    }
    public class Obj_AddUpdate_Supplier_Value_List
    {
        public int Sup_Id { get; set; }
        public int Cat_V_Id { get; set; }
        public string Supp_Cat_name { get; set; }
        public bool Status { get; set; }
    }



    public class Save_CategoryMas_Req
    {
        public int Id { get; set; }
        public string Category_Name { get; set; }
    }


    public class Get_CategoryDet_Req
    {
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
    }
    public class Get_CategoryDet_Res
    {
        public long iSr { get; set; }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Category_Name { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string OurCategory { get; set; }
        public string SuppCategory { get; set; }
        public string SuppCategorySysnonym { get; set; }
        public bool IsActive { get; set; }
    }

    public class Save_CategoryDet_Req
    {
        public List<Obj_CategoryDet_List> cate { get; set; }
        public Save_CategoryDet_Req()
        {
            cate = new List<Obj_CategoryDet_List>();
        }
    }
    public class Obj_CategoryDet_List
    {
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public string OurCategory { get; set; }
        public string SuppCategory { get; set; }
        public string SuppCategorySysnonym { get; set; }
        public bool IsActive { get; set; }
    }

    public class Get_SupplierMas_Req
    {
        public string OrderBy { get; set; }
        public int PgNo { get; set; }
        public int PgSize { get; set; }
    }
    public class Get_SupplierMas_Res
    {
        public int iTotalRec { get; set; }
        public long iSr { get; set; }
        public int Id { get; set; }
        public string SupplierName { get; set; }
    }
    public class Get_SupplierMaster_Req
    {
        public long Id { get; set; }
        public string SupplierName { get; set; }
        public int iPgNo { get; set; }
        public int iPgSize { get; set; }
        public string OrderBy { get; set; }
        public bool WebAPIFTPStockUpload { get; set; }
        public bool FileStockUpload { get; set; }
        public string IsActive { get; set; }
        public string ShortName { get; set; }
        public string APIType { get; set; }
        public string LastStockUploadDateTime { get; set; }
        public string LastStockUploadDateTime_Type { get; set; }
    }
    public class Get_SupplierMaster_Res
    {
        public long iTotalRec { get; set; }
        public long iSr { get; set; }
        public long Id { get; set; }
        public string APIType { get; set; }
        public string SupplierURL { get; set; }
        public string SupplierName { get; set; }
        public string SupplierHitUrl { get; set; }
        public string SupplierResponseFormat { get; set; }
        public string FileLocation { get; set; }
        public string LocationExportType { get; set; }
        public string RepeateveryType { get; set; }
        public string Repeatevery { get; set; }
        public string AutoUploadStock { get; set; }
        public string SupplierAPIMethod { get; set; }
        public bool Active { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CreateDate { get; set; }
        public string UpdateDate { get; set; }
        public string FileName { get; set; }
        public bool DiscInverse { get; set; }
        public string NewRefNoGenerate { get; set; }
        public string NewRefNoCommonPrefix { get; set; }
        public string DataGetFrom { get; set; }
        public bool Image { get; set; }
        public bool Video { get; set; }
        public bool Certi { get; set; }
        public string DocViewType_Image1 { get; set; }
        public string DocViewType_Image2 { get; set; }
        public string DocViewType_Image3 { get; set; }
        public string DocViewType_Image4 { get; set; }
        public string DocViewType_Video { get; set; }
        public string DocViewType_Certi { get; set; }
        public string ImageURL_1 { get; set; }
        public string ImageFormat_1 { get; set; }
        public string ImageURL_2 { get; set; }
        public string ImageFormat_2 { get; set; }
        public string ImageURL_3 { get; set; }
        public string ImageFormat_3 { get; set; }
        public string ImageURL_4 { get; set; }
        public string ImageFormat_4 { get; set; }
        public string VideoURL { get; set; }
        public string VideoFormat { get; set; }
        public string CertiURL { get; set; }
        public string CertiFormat { get; set; }
        public string ShortName { get; set; }
        public string LastStockUploadDateTime { get; set; }
        public int Uploaded_Stone { get; set; }
        public int Not_Uploaded_Stone { get; set; }
        public string APIType1 { get; set; }
    }
    public class Get_ColumnMaster_Res
    {
        public int Col_Id { get; set; }
        public string Column_Name { get; set; }
        public string Display_Name { get; set; }
    }
    public class Get_SupplierColumnSetting_Res
    {
        //public long iSr { get; set; }
        //public int Id { get; set; }
        //public int SupplierId { get; set; }
        //public int ColumnId { get; set; }
        //public string SupplierColumn { get; set; }
        public long iSr { get; set; }
        public int Col_Id { get; set; }
        public string Column_Name { get; set; }
        public string SupplierColumn { get; set; }
    }
    public class Get_SupplierColumnSetting_FromAPI_Res
    {
        public int Id { get; set; }
        public string SupplierColumn { get; set; }
    }
    public class Get_SheetName_From_File_Res
    {
        public int Id { get; set; }
        public string SheetName { get; set; }
    }
    public class AddUpdate_SupplierColumnSetting_Req
    {
        public List<Obj_SupplierColumnSetting_List> col { get; set; }
        public AddUpdate_SupplierColumnSetting_Req()
        {
            col = new List<Obj_SupplierColumnSetting_List>();
        }
    }
    public class Obj_SupplierColumnSetting_List
    {
        public int SupplierId { get; set; }
        public string SupplierColumn { get; set; }
        public int ColumnId { get; set; }
    }
    public class Get_FancyColor_Res
    {
        public string ColorName { get; set; }
        public string Type { get; set; }
    }
    public class Get_ParaMas_Res
    {
        public int Id { get; set; }
        public string value { get; set; }
        public string ListType { get; set; }
        public string UrlValue { get; set; }
        public string UrlValueHov { get; set; }
    }
    public class Get_PriceList_ParaMas
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        public bool isActive { get; set; }
        public string UrlValue { get; set; }
        public string UrlValueHov { get; set; }
    }
    public class get_key_to_symbol
    {
        public string sSymbol { get; set; }
    }
    public class Get_Supplier_RefNo_Prefix_Req
    {
        public int SupplierId { get; set; }
    }
    public class Get_Supplier_RefNo_Prefix_Res
    {
        public long iSr { get; set; }
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int Shape { get; set; }
        public int Pointer { get; set; }
        public string Prefix { get; set; }
    }
    public class Save_Supplier_RefNo_Prefix_Req
    {
        public List<Obj_Supplier_RefNo_Prefix_List> refno { get; set; }
        public Save_Supplier_RefNo_Prefix_Req()
        {
            refno = new List<Obj_Supplier_RefNo_Prefix_List>();
        }
    }
    public class Obj_Supplier_RefNo_Prefix_List
    {
        public int SupplierId { get; set; }
        public int Shape { get; set; }
        public int Pointer { get; set; }
        public string Prefix { get; set; }
    }
    public class React_API_eq
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class Save_Supplier_Disc_Req
    {
        public int UserId { get; set; }
        public int SupplierId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ExportType { get; set; }
        public string URL { get; set; }
        public List<Obj_Supplier_Disc> SuppDisc { get; set; }
        public Save_Supplier_Disc_Req()
        {
            SuppDisc = new List<Obj_Supplier_Disc>();
        }
    }
    public class Obj_Supplier_Disc
    {
        public int UserId { get; set; }
        public string Supplier { get; set; }
        public string SupplierId { get; set; }
        public string Location { get; set; }
        public string Shape { get; set; }
        public string Carat { get; set; }
        public string ColorType { get; set; }
        public string Color { get; set; }
        public string INTENSITY { get; set; }
        public string OVERTONE { get; set; }
        public string FANCY_COLOR { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Sym { get; set; }
        public string Fls { get; set; }
        public string Lab { get; set; }
        public string FromLength { get; set; }
        public string ToLength { get; set; }
        public string FromWidth { get; set; }
        public string ToWidth { get; set; }
        public string FromDepth { get; set; }
        public string ToDepth { get; set; }
        public string FromDepthinPer { get; set; }
        public string ToDepthinPer { get; set; }
        public string FromTableinPer { get; set; }
        public string ToTableinPer { get; set; }
        public string FromCrAng { get; set; }
        public string ToCrAng { get; set; }
        public string FromCrHt { get; set; }
        public string ToCrHt { get; set; }
        public string FromPavAng { get; set; }
        public string ToPavAng { get; set; }
        public string FromPavHt { get; set; }
        public string ToPavHt { get; set; }
        public string FromBaseDisc { get; set; }
        public string ToBaseDisc { get; set; }
        public string FromBaseAmt { get; set; }
        public string ToBaseAmt { get; set; }
        public string FromFinalDisc { get; set; }
        public string ToFinalDisc { get; set; }
        public string FromFinalAmt { get; set; }
        public string ToFinalAmt { get; set; }
        public string CheckKTS { get; set; }
        public string UNCheckKTS { get; set; }
        public bool KTS_IsBlank { get; set; }
        public string Culet { get; set; }
        public string BGM { get; set; }
        public string CrownBlack { get; set; }
        public string TableBlack { get; set; }
        public string CrownWhite { get; set; }
        public string TableWhite { get; set; }
        public string TableOpen { get; set; }
        public string GirdleOpen { get; set; }
        public string CrownOpen { get; set; }
        public string PavillionOpen { get; set; }
        public string GoodsType { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public string PricingMethod_1 { get; set; }
        public string PricingSign_1 { get; set; }
        public string Disc_1_1 { get; set; }
        public string Value_1_1 { get; set; }
        public string Value_1_2 { get; set; }
        public string Value_1_3 { get; set; }
        public string Value_1_4 { get; set; }
        public string Value_1_5 { get; set; }
        public string Speci_Additional_1 { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string PricingMethod_2 { get; set; }
        public string PricingSign_2 { get; set; }
        public string Disc_2_1 { get; set; }
        public string Value_2_1 { get; set; }
        public string Value_2_2 { get; set; }
        public string Value_2_3 { get; set; }
        public string Value_2_4 { get; set; }
        public string Value_2_5 { get; set; }
        public string PricingMethod_3 { get; set; }
        public string PricingSign_3 { get; set; }
        public string Disc_3_1 { get; set; }
        public string Value_3_1 { get; set; }
        public string Value_3_2 { get; set; }
        public string Value_3_3 { get; set; }
        public string Value_3_4 { get; set; }
        public string Value_3_5 { get; set; }
        public string Speci_Additional_2 { get; set; }
        public string FromDate1 { get; set; }
        public string ToDate1 { get; set; }
        public string PricingMethod_4 { get; set; }
        public string PricingSign_4 { get; set; }
        public string Disc_4_1 { get; set; }
        public string Value_4_1 { get; set; }
        public string Value_4_2 { get; set; }
        public string Value_4_3 { get; set; }
        public string Value_4_4 { get; set; }
        public string Value_4_5 { get; set; }
        public bool View { get; set; }
        public bool Download { get; set; }
        public bool Length_IsBlank { get; set; }
        public bool Width_IsBlank { get; set; }
        public bool Depth_IsBlank { get; set; }
        public bool DepthPer_IsBlank { get; set; }
        public bool TablePer_IsBlank { get; set; }
        public bool CrAng_IsBlank { get; set; }
        public bool CrHt_IsBlank { get; set; }
        public bool PavAng_IsBlank { get; set; }
        public bool PavHt_IsBlank { get; set; }
    }
    public class Get_Customer_Stock_Disc_Mas_Res
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string URL { get; set; }
        public string ExportType { get; set; }
    }
    public class Get_Customer_Stock_Disc_Count
    {
        public long TotCount { get; set; }
    }
    
    public class Get_SearchStock_Req
    {
        public int SearchID { get; set; }
        public string SearchName { get; set; }
        public string SP_Name { get; set; }
        public int UserId { get; set; }
        public int PgNo { get; set; }
        public int PgSize { get; set; }
        public string OrderBy { get; set; }
        public string Location { get; set; }
        public string RefNo { get; set; }
        public string SupplierId_RefNo_SupplierRefNo { get; set; }
        public string SupplierId { get; set; }
        public string Shape { get; set; }
        public string Pointer { get; set; }
        public string Sub_Pointer { get; set; }
        public string ColorType { get; set; }
        public string Color { get; set; }
        public string INTENSITY { get; set; }
        public string OVERTONE { get; set; }
        public string FANCY_COLOR { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string Fls { get; set; }
        public string BGM { get; set; }
        public string Lab { get; set; }
        public string CrownBlack { get; set; }
        public string TableBlack { get; set; }
        public string TableWhite { get; set; }
        public string CrownWhite { get; set; }
        public string TableOpen { get; set; }
        public string CrownOpen { get; set; }
        public string PavOpen { get; set; }
        public string GirdleOpen { get; set; }

        public string KTSBlank { get; set; }
        public string Keytosymbol { get; set; }
        public string CheckKTS { get; set; }
        public string UNCheckKTS { get; set; }

        public string FromDisc { get; set; }
        public string ToDisc { get; set; }

        public string FromTotAmt { get; set; }
        public string ToTotAmt { get; set; }

        public string LengthBlank { get; set; }
        public string FromLength { get; set; }
        public string ToLength { get; set; }

        public string WidthBlank { get; set; }
        public string FromWidth { get; set; }
        public string ToWidth { get; set; }

        public string DepthBlank { get; set; }
        public string FromDepth { get; set; }
        public string ToDepth { get; set; }

        public string DepthPerBlank { get; set; }
        public string FromDepthPer { get; set; }
        public string ToDepthPer { get; set; }

        public string TablePerBlank { get; set; }
        public string FromTablePer { get; set; }
        public string ToTablePer { get; set; }

        public string Img { get; set; }
        public string Vdo { get; set; }
        public string Certi { get; set; }

        public string CrAngBlank { get; set; }
        public string FromCrAng { get; set; }
        public string ToCrAng { get; set; }

        public string CrHtBlank { get; set; }
        public string FromCrHt { get; set; }
        public string ToCrHt { get; set; }

        public string PavAngBlank { get; set; }
        public string FromPavAng { get; set; }
        public string ToPavAng { get; set; }

        public string PavHtBlank { get; set; }
        public string FromPavHt { get; set; }
        public string ToPavHt { get; set; }

        public string Type { get; set; }
        public bool View { get; set; }
        public bool Download { get; set; }

        public string PricingMethod { get; set; }
        public string PricingSign { get; set; }
        public decimal PricingDisc { get; set; }
        public string ToAddress { get; set; }
        public string Comments { get; set; }
    }
    public class SearchSummary
    {
        public int TOT_PAGE { get; set; }
        public int PAGE_SIZE { get; set; }
        public int TOT_PCS { get; set; }
        public double TOT_CTS { get; set; }
        public double TOT_RAP_AMOUNT { get; set; }
        public double TOT_NET_AMOUNT { get; set; }
        public double AVG_PRICE_PER_CTS { get; set; }
        public double AVG_SALES_DISC_PER { get; set; }
    }
    public class SearchDiamondsResponse
    {
        private List<Get_SearchStock_Res> dataList;

        public List<Get_SearchStock_Res> DataList
        {
            get { return dataList; }
            set { dataList = value; }
        }

        private SearchSummary dataSummary;

        public SearchSummary DataSummary
        {
            get { return dataSummary; }
            set { dataSummary = value; }
        }

        public SearchDiamondsResponse()
        {
            DataList = new List<Get_SearchStock_Res>();
            dataSummary = new SearchSummary();
        }
    }


    public class Get_SearchStock_Res
    {
        public long iTotalRec { get; set; }
        public long iSr { get; set; }
        public string SupplierShortName { get; set; }
        public int SupplierId { get; set; }
        public string CompName { get; set; }
        public string SupplierName { get; set; }
        public string Ref_No { get; set; }
        public string Shape { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string Fls { get; set; }
        public decimal Cts { get; set; }
        public string Pointer { get; set; }
        public string Sub_Pointer { get; set; }
        public decimal Base_Price_Cts { get; set; }
        public decimal Rap_Rate { get; set; }
        public decimal Base_Amount { get; set; }
        public string Measurement { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Depth { get; set; }
        public int Table_Per { get; set; }
        public decimal Depth_Per { get; set; }
        public string Table_Inclusion { get; set; }
        public string Crown_Inclusion { get; set; }
        public string Table_Natts { get; set; }
        public string Crown_Natts { get; set; }
        public string Side_Inclusion { get; set; }
        public string Side_Natts { get; set; }
        public string Crown_Open { get; set; }
        public string Pav_Open { get; set; }
        public string Table_Open { get; set; }
        public string Girdle_Open { get; set; }
        public decimal Crown_Angle { get; set; }
        public decimal Pav_Angle { get; set; }
        public decimal Crown_Height { get; set; }
        public decimal Pav_Height { get; set; }
        public decimal Rap_Amount { get; set; }
        public string Lab { get; set; }
        public string Certificate_URL { get; set; }
        public string Image_URL { get; set; }
        public string Image_URL_2 { get; set; }
        public string Image_URL_3 { get; set; }
        public string Image_URL_4 { get; set; }
        public string Video_URL { get; set; }
        public string DNA { get; set; }
        public string Status { get; set; }
        public string Supplier_Stone_Id { get; set; }
        public string Location { get; set; }
        public string Shade { get; set; }
        public string Luster { get; set; }
        public string Type_2A { get; set; }
        public string Milky { get; set; }
        public string BGM { get; set; }
        public string Key_To_Symboll { get; set; }
        public decimal RATIO { get; set; }
        public string Supplier_Comments { get; set; }
        public string Lab_Comments { get; set; }
        public string Culet { get; set; }
        public decimal Girdle_Per { get; set; }
        public string Girdle_Type { get; set; }
        public string Girdle_MM { get; set; }
        public string Inscription { get; set; }
        public string Culet_Condition { get; set; }
        public decimal Star_Length { get; set; }
        public decimal Lower_Halves { get; set; }
        public string Stage { get; set; }
        public string Certi_Date { get; set; }
        public decimal Disc { get; set; }
        public decimal Value { get; set; }
        public decimal Fix_Price { get; set; }
        public string Certificate_No { get; set; }
        public string Goods_Type { get; set; }
        public decimal SUPPLIER_COST_DISC { get; set; }
        public decimal SUPPLIER_COST_VALUE { get; set; }
        public decimal MAX_DISC { get; set; }
        public decimal MAX_VALUE { get; set; }
        public decimal CUSTOMER_COST_DISC { get; set; }
        public decimal CUSTOMER_COST_VALUE { get; set; }
        public decimal Shape_Sr { get; set; }
        public decimal Color_Sr { get; set; }
        public decimal Clarity_Sr { get; set; }
        public decimal Cut_Sr { get; set; }
        public decimal Polish_Sr { get; set; }
        public decimal Symm_Sr { get; set; }
        public decimal Fls_Sr { get; set; }
        public int Rank { get; set; }
        public string Supplier_Status { get; set; }
        public string Buyer_Name { get; set; }
        public decimal Bid_Disc { get; set; }
        public decimal Bid_Amt { get; set; }
        public decimal Bid_Ct { get; set; }
        public decimal Avg_Stock_Disc { get; set; }
        public int Avg_Stock_Pcs { get; set; }
        public decimal Avg_Pur_Disc { get; set; }
        public int Avg_Pur_Pcs { get; set; }
        public decimal Avg_Sales_Disc { get; set; }
        public int Sales_Pcs { get; set; }
        public string KTS_Grade { get; set; }
        public string Comm_Grade { get; set; }
        public string Zone { get; set; }
        public string Para_Grade { get; set; }
        public string QCRequire { get; set; }
        public string LabEntry_Status { get; set; }
        public int PAGE_SIZE { get; set; }
        public float TOTAL_PAGE { get; set; }
    }
    public class Data_Get_From_File_Req
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SheetName { get; set; }
        public string File { get; set; }
        public string FilePath { get; set; }
    }
    public class Get_ColumnSetting_UserWise_Res
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Column_Name { get; set; }
        public long OrderBy { get; set; }
        public bool Visible { get; set; }
        public bool Access { get; set; }
    }
    public class Save_ColumnSetting_UserWise
    {
        public List<Obj_ColumnSetting_UserWise> BUYER { get; set; }
        public List<Obj_ColumnSetting_UserWise> SUPPLIER { get; set; }
        public List<Obj_ColumnSetting_UserWise> CUSTOMER { get; set; }
        public Save_ColumnSetting_UserWise()
        {
            BUYER = new List<Obj_ColumnSetting_UserWise>();
            SUPPLIER = new List<Obj_ColumnSetting_UserWise>();
            CUSTOMER = new List<Obj_ColumnSetting_UserWise>();
        }
    }
    public class Obj_ColumnSetting_UserWise
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public int OrderBy { get; set; }
    }
    public class Get_SearchStock_ColumnSetting_Req
    {
        public int UserId { get; set; }
        public string Type { get; set; }
    }
    public class Get_SearchStock_ColumnSetting_Res
    {
        public long OrderBy { get; set; }
        public string Type { get; set; }
        public string Column_Name { get; set; }
        public decimal ExcelWidth { get; set; }
    }
    public class PlaceOrder_Req
    {
        public int UserId { get; set; }
        public string SupplierId_RefNo_SupplierRefNo { get; set; }
        public string Comments { get; set; }
        public string PricingMethod { get; set; }
        public string PricingSign { get; set; }
        public decimal PricingDisc { get; set; }
    }
    public class Get_OrderHistory_Req
    {
        public long UserId { get; set; }
        public int PgNo { get; set; }
        public int PgSize { get; set; }
        public string OrderBy { get; set; }
        public string StoneId { get; set; }
        public string OrderDetId { get; set; }
        public string UserTypeList { get; set; }

    }
    public class Get_OrderHistory_Res
    {
        public long iTotalRec { get; set; }
        public long Sr { get; set; }
        public int OrderDetId { get; set; }
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string OrderDate { get; set; }
        public string UserFullName { get; set; }
        public string CompName { get; set; }
        public string AssistBy { get; set; }
        public string SubAssistBy { get; set; }
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Ref_No { get; set; }
        public string Shape { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string Fls { get; set; }
        public decimal Cts { get; set; }
        public string Pointer { get; set; }
        public string Sub_Pointer { get; set; }
        public decimal Base_Price_Cts { get; set; }
        public decimal Rap_Rate { get; set; }
        public decimal Base_Amount { get; set; }
        public string Measurement { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Depth { get; set; }
        public int Table_Per { get; set; }
        public decimal Depth_Per { get; set; }
        public string Table_Inclusion { get; set; }
        public string Crown_Inclusion { get; set; }
        public string Table_Natts { get; set; }
        public string Crown_Natts { get; set; }
        public string Side_Inclusion { get; set; }
        public string Side_Natts { get; set; }
        public string Crown_Open { get; set; }
        public string Pav_Open { get; set; }
        public string Table_Open { get; set; }
        public string Girdle_Open { get; set; }
        public decimal Crown_Angle { get; set; }
        public decimal Pav_Angle { get; set; }
        public decimal Crown_Height { get; set; }
        public decimal Pav_Height { get; set; }
        public decimal Rap_Amount { get; set; }
        public string Lab { get; set; }
        public string Certificate_URL { get; set; }
        public string Image_URL { get; set; }
        public string Image_URL_2 { get; set; }
        public string Image_URL_3 { get; set; }
        public string Image_URL_4 { get; set; }
        public string Video_URL { get; set; }
        public string DNA { get; set; }
        public string Status { get; set; }
        public string Supplier_Stone_Id { get; set; }
        public string Location { get; set; }
        public string Shade { get; set; }
        public string Luster { get; set; }
        public string Type_2A { get; set; }
        public string Milky { get; set; }
        public string BGM { get; set; }
        public string Key_To_Symboll { get; set; }
        public decimal RATIO { get; set; }
        public string Supplier_Comments { get; set; }
        public string Lab_Comments { get; set; }
        public string Culet { get; set; }
        public decimal Girdle_Per { get; set; }
        public string Girdle_Type { get; set; }
        public string Girdle_MM { get; set; }
        public string Inscription { get; set; }
        public string Culet_Condition { get; set; }
        public decimal Star_Length { get; set; }
        public decimal Lower_Halves { get; set; }
        public string Stage { get; set; }
        public string Certi_Date { get; set; }
        public decimal Disc { get; set; }
        public decimal Value { get; set; }
        public decimal Fix_Price { get; set; }
        public string Certificate_No { get; set; }
        public string Goods_Type { get; set; }
        public decimal SUPPLIER_COST_DISC { get; set; }
        public decimal SUPPLIER_COST_VALUE { get; set; }
        public decimal MAX_DISC { get; set; }
        public decimal MAX_VALUE { get; set; }
        public decimal CUSTOMER_COST_DISC { get; set; }
        public decimal CUSTOMER_COST_VALUE { get; set; }
        public decimal Shape_Sr { get; set; }
        public decimal Color_Sr { get; set; }
        public decimal Clarity_Sr { get; set; }
        public decimal Cut_Sr { get; set; }
        public decimal Polish_Sr { get; set; }
        public decimal Symm_Sr { get; set; }
        public decimal Fls_Sr { get; set; }
        public int Rank { get; set; }
        public string Supplier_Status { get; set; }
        public string Buyer_Name { get; set; }
        public decimal Bid_Disc { get; set; }
        public decimal Bid_Amt { get; set; }
        public decimal Bid_Ct { get; set; }
        public decimal Avg_Stock_Disc { get; set; }
        public int Avg_Stock_Pcs { get; set; }
        public decimal Avg_Pur_Disc { get; set; }
        public int Avg_Pur_Pcs { get; set; }
        public decimal Avg_Sales_Disc { get; set; }
        public int Sales_Pcs { get; set; }
        public string KTS_Grade { get; set; }
        public string Comm_Grade { get; set; }
        public string Zone { get; set; }
        public string Para_Grade { get; set; }
    }
    public class Get_Company_PlaceOrder_Res
    {
        public string UserId { get; set; }
        public string CompName { get; set; }
        public string FortunePartyCode { get; set; }
        public string UserName { get; set; }
        public string CustName { get; set; }
        public string AssistBy { get; set; }
        public string SubAssistBy { get; set; }
    }
    
    public class Add_LabEntry_Res
    {
        public int Id { get; set; }
    }
    public class LabEntry_Req
    {
        public int UserId { get; set; }
        public List<LabEntry_List> LabEntry_List { get; set; }
    }
    public class LabEntry_List
    {
        public string LabDate { get; set; }
        public string UserId { get; set; }
        public string Ref_No { get; set; }
        public string SupplierId { get; set; }
        public string QC_Require { get; set; }
        public string LabEntry_Status { get; set; }
        public string SUPPLIER_COST_DISC { get; set; }
        public string SUPPLIER_COST_VALUE { get; set; }
        public string CUSTOMER_COST_DISC { get; set; }
        public string CUSTOMER_COST_VALUE { get; set; }
        public string PROFIT { get; set; }
        public string PROFIT_AMOUNT { get; set; }
    }
    public class Add_MyCart_Req
    {
        public string SupplierId_RefNo_SupplierRefNo { get; set; }
    }
    public class Get_MyCart_Req
    {
        public int PgNo { get; set; }
        public int PgSize { get; set; }
        public string OrderBy { get; set; }
        public string RefNo_SupplierRefNo { get; set; }
        public string UserTypeList { get; set; }
        public string CartId { get; set; }
    }
    public class Get_MyCart_Res
    {
        public long iTotalRec { get; set; }
        public long Sr { get; set; }
        public int CartId { get; set; }
        public string TransDate { get; set; }
        public string UserFullName { get; set; }
        public string CompName { get; set; }
        public int UserId { get; set; }
        public int SupplierId { get; set; }
        public string SupplierShortName { get; set; }
        public string SupplierName { get; set; }
        public string Ref_No { get; set; }
        public string Shape { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string Fls { get; set; }
        public decimal Cts { get; set; }
        public string Pointer { get; set; }
        public string Sub_Pointer { get; set; }
        public decimal Base_Price_Cts { get; set; }
        public decimal Rap_Rate { get; set; }
        public decimal Base_Amount { get; set; }
        public string Measurement { get; set; }
        public decimal Length { get; set; }
        public decimal Width { get; set; }
        public decimal Depth { get; set; }
        public int Table_Per { get; set; }
        public decimal Depth_Per { get; set; }
        public string Table_Inclusion { get; set; }
        public string Crown_Inclusion { get; set; }
        public string Table_Natts { get; set; }
        public string Crown_Natts { get; set; }
        public string Side_Inclusion { get; set; }
        public string Side_Natts { get; set; }
        public string Crown_Open { get; set; }
        public string Pav_Open { get; set; }
        public string Table_Open { get; set; }
        public string Girdle_Open { get; set; }
        public decimal Crown_Angle { get; set; }
        public decimal Pav_Angle { get; set; }
        public decimal Crown_Height { get; set; }
        public decimal Pav_Height { get; set; }
        public decimal Rap_Amount { get; set; }
        public string Lab { get; set; }
        public string Certificate_URL { get; set; }
        public string Image_URL { get; set; }
        public string Image_URL_2 { get; set; }
        public string Image_URL_3 { get; set; }
        public string Image_URL_4 { get; set; }
        public string Video_URL { get; set; }
        public string DNA { get; set; }
        public string Status { get; set; }
        public string Supplier_Stone_Id { get; set; }
        public string Location { get; set; }
        public string Shade { get; set; }
        public string Luster { get; set; }
        public string Type_2A { get; set; }
        public string Milky { get; set; }
        public string BGM { get; set; }
        public string Key_To_Symboll { get; set; }
        public decimal RATIO { get; set; }
        public string Supplier_Comments { get; set; }
        public string Lab_Comments { get; set; }
        public string Culet { get; set; }
        public decimal Girdle_Per { get; set; }
        public string Girdle_Type { get; set; }
        public string Girdle_MM { get; set; }
        public string Inscription { get; set; }
        public string Culet_Condition { get; set; }
        public decimal Star_Length { get; set; }
        public decimal Lower_Halves { get; set; }
        public string Stage { get; set; }
        public string Certi_Date { get; set; }
        public decimal Disc { get; set; }
        public decimal Value { get; set; }
        public decimal Fix_Price { get; set; }
        public string Certificate_No { get; set; }
        public string Goods_Type { get; set; }
        public decimal SUPPLIER_COST_DISC { get; set; }
        public decimal SUPPLIER_COST_VALUE { get; set; }
        public decimal MAX_DISC { get; set; }
        public decimal MAX_VALUE { get; set; }
        public decimal CUSTOMER_COST_DISC { get; set; }
        public decimal CUSTOMER_COST_VALUE { get; set; }
        public decimal Shape_Sr { get; set; }
        public decimal Color_Sr { get; set; }
        public decimal Clarity_Sr { get; set; }
        public decimal Cut_Sr { get; set; }
        public decimal Polish_Sr { get; set; }
        public decimal Symm_Sr { get; set; }
        public decimal Fls_Sr { get; set; }
        public int Rank { get; set; }
        public string Supplier_Status { get; set; }
        public string Buyer_Name { get; set; }
        public decimal Bid_Disc { get; set; }
        public decimal Bid_Amt { get; set; }
        public decimal Bid_Ct { get; set; }
        public decimal Avg_Stock_Disc { get; set; }
        public int Avg_Stock_Pcs { get; set; }
        public decimal Avg_Pur_Disc { get; set; }
        public int Avg_Pur_Pcs { get; set; }
        public decimal Avg_Sales_Disc { get; set; }
        public int Sales_Pcs { get; set; }
        public string KTS_Grade { get; set; }
        public string Comm_Grade { get; set; }
        public string Zone { get; set; }
        public string Para_Grade { get; set; }
    }
    public class Get_URL_Req
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int TransId { get; set; }
    }
    public class Get_SaveSearch_Res
    {
        public long Sr { get; set; }
        public int Id { get; set; }
        public string TransDate { get; set; }
        public int UserId { get; set; }
        public string SearchName { get; set; }
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string Shape { get; set; }
        public string Carat { get; set; }
        public string ColorType { get; set; }
        public string Color { get; set; }
        public string INTENSITY { get; set; }
        public string OVERTONE { get; set; }
        public string FANCY_COLOR { get; set; }
        public string Clarity { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Sym { get; set; }
        public string Fls { get; set; }
        public string BGM { get; set; }
        public string Lab { get; set; }
        public string CrownBlack { get; set; }
        public string TableBlack { get; set; }
        public string CrownWhite { get; set; }
        public string TableWhite { get; set; }
        public string TableOpen { get; set; }
        public string GirdleOpen { get; set; }
        public string CrownOpen { get; set; }
        public string PavillionOpen { get; set; }
        public string CheckKTS { get; set; }
        public string UNCheckKTS { get; set; }
        public bool KTS_IsBlank { get; set; }
        public string FromFinalDisc { get; set; }
        public string ToFinalDisc { get; set; }
        public string FromFinalVal { get; set; }
        public string ToFinalVal { get; set; }
        public string FromLength { get; set; }
        public string ToLength { get; set; }
        public bool Length_IsBlank { get; set; }
        public string FromWidth { get; set; }
        public string ToWidth { get; set; }
        public bool Width_IsBlank { get; set; }
        public string FromDepth { get; set; }
        public string ToDepth { get; set; }
        public bool Depth_IsBlank { get; set; }
        public string FromDepthinPer { get; set; }
        public string ToDepthinPer { get; set; }
        public bool DepthPer_IsBlank { get; set; }
        public string FromTableinPer { get; set; }
        public string ToTableinPer { get; set; }
        public bool TablePer_IsBlank { get; set; }
        public string Image { get; set; }
        public string Video { get; set; }
        public string Certi { get; set; }
        public string FromCrAng { get; set; }
        public string ToCrAng { get; set; }
        public bool CrAng_IsBlank { get; set; }
        public string FromCrHt { get; set; }
        public string ToCrHt { get; set; }
        public bool CrHt_IsBlank { get; set; }
        public string FromPavAng { get; set; }
        public string ToPavAng { get; set; }
        public bool PavAng_IsBlank { get; set; }
        public string FromPavHt { get; set; }
        public string ToPavHt { get; set; }
        public bool PavHt_IsBlank { get; set; }
        public string Location { get; set; }
    }
}

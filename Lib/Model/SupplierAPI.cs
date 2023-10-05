using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model
{
    public class AnkitGems
    {
        public string code { get; set; }
        public bool flag { get; set; }
        public string message { get; set; }
        public string ref_id { get; set; }
        public AnkitGems_Inner_1 data { get; set; }
    }
    public class AnkitGems_Inner_1
    {
        public AnkitGems_Inner_2 user { get; set; }
        public string accessToken { get; set; }
    }
    public class AnkitGems_Inner_2
    {
        public string name { get; set; }
        public string day_terms { get; set; }
        public string account_name { get; set; }
        public string account_short_code { get; set; }
        public string business_type { get; set; }
        public string registration_date { get; set; }
        public string is_active { get; set; }
    }

    //By Dhruv Patel-01-12-2021
    public class Dharam
    {
        public int uniqID { get; set; }
        public string company { get; set; }
        public string actCode { get; set; }
        public string selectAll { get; set; }
        public int StartIndex { get; set; }
        public int count { get; set; }
        public string columns { get; set; }
        public string finder { get; set; }
        public string sort { get; set; }

    }

    //By Dhruv Patel-02-12-2021
    public class JOY
    {
        public List<string> keys { get; set; }
        public List<List<object>> rows { get; set; }

    }

    //By Dhruv Patel-15-12-2021
    public class SGLoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class SGLoginResponse
    {
        public string UserName { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public string TokenId { get; set; }
    }


    public class SGStockRequest
    {
        public string UserId { get; set; }
        public string TokenId { get; set; }
    }

    public class SGStockResponse
    {
        public List<data> Data { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string Error { get; set; }
    }
    public class data
    {
        public string Stock_ID { get; set; }
        public string Shape { get; set; }
        public double Cts { get; set; }
        public string Color { get; set; }
        public string Clarity { get; set; }
        public double Rep_Price { get; set; }
        public string Cut { get; set; }
        public string Polish { get; set; }
        public string Symm { get; set; }
        public string Fls { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public double Depth_Per { get; set; }
        public double Table_Per { get; set; }
        public double Cr_Ang { get; set; }
        public double Cr_Ht { get; set; }
        public double Pav_Ang { get; set; }
        public double Pav_Ht { get; set; }
        public string Certi_No { get; set; }
        public string Girdle { get; set; }
        public double Disc { get; set; }
        public string Lab { get; set; }
        public string Pointer { get; set; }
        public string Status { get; set; }
        public string Shade { get; set; }
        public string Luster { get; set; }
        public string Table_Natts { get; set; }
        public string Girdle_Type { get; set; }
        public string Culet { get; set; }
        public object Table_Depth { get; set; }
        public string Inclusion { get; set; }
        public string HNA { get; set; }
        public string Side_Natts { get; set; }
        public string Table_Open { get; set; }
        public string Crown_Open { get; set; }
        public string Comments { get; set; }
        public string Key_To_Symbol { get; set; }
        public double Disc_By_Date { get; set; }
        public string Inscription { get; set; }
        public double Girdle_Per { get; set; }
        public object Revise_Disc_Flag { get; set; }
        public string Crown_Natts { get; set; }
        public string Crown_Inclusion { get; set; }
        public string Certi_Date { get; set; }
        public string BGM { get; set; }
        public string UserComments { get; set; }
        public double Group_Disc { get; set; }
        public double Rap_Amount { get; set; }
        public double Net_Price { get; set; }
        public string Table_White { get; set; }
        public string Side_White { get; set; }
        public string Milky_Grade { get; set; }
        public string Source { get; set; }
        public string Location { get; set; }
        public string Fls_Color { get; set; }
        public DateTime? Lab_Date { get; set; }
        public double Price_Per_Cts { get; set; }
        public string View_Image { get; set; }
        public string View_Video { get; set; }
        public string View_Certi { get; set; }
        public string TableOpen { get; set; }
        public string CrownOpen { get; set; }
        public string PavillionOpen { get; set; }
        public string GirdleOpen { get; set; }
    }

    public class DiamartResponse
    {
        public string Loat_NO { get; set; }
        //public string Status { get; set; }
        //public string Shape { get; set; }
        //public double Weight { get; set; }
        //public string Color { get; set; }
        //public string Clarity { get; set; }
        //public string Cut { get; set; }
        //public string Polish { get; set; }
        //public string Symmetry { get; set; }
        //public string Fluorescence { get; set; }
        //public double Length { get; set; }
        //public double Width { get; set; }
        //public double Depth { get; set; }
        //public double TotalDepth { get; set; }
        //public double Table { get; set; }
        //public double Discount { get; set; }
        //public double Rap { get; set; }
        //public string Lab { get; set; }
        //public string CertiNo { get; set; }
        //public string Inscription { get; set; }
        //public double CrownAngle { get; set; }
        //public double CrownHeight { get; set; }
        //public double PavAngle { get; set; }
        //public double PavDepth { get; set; }
        //public string KeytoSymbols { get; set; }
        //public string Natts { get; set; }
        //public string Comment { get; set; }
        //public string HNA { get; set; }
        //public string EyeClean { get; set; }
        //public string Girdle { get; set; }
        //public double GirdlePerc { get; set; }
        //public string Culet { get; set; }
        //public string GirdleCondition { get; set; }
        //public string Location { get; set; }
        //public string IMG_URL { get; set; }
        //public string VID_URL { get; set; }
        //public string CERTI_URL { get; set; }
        //public string Shade { get; set; }
        //public string Milky { get; set; }
        //public string Brown { get; set; }
        //public string Green { get; set; }
        //public string CenterBlack { get; set; }
        //public string SideBlack { get; set; }
        //public string OpenTable { get; set; }
        //public string OpenCrown { get; set; }
        //public string OpenGirdle { get; set; }
        //public string OpenPavilion { get; set; }
        //public string NaturalOnCrown { get; set; }
        //public string NaturalOnGirdle { get; set; }
        //public string NaturalOnPavillion { get; set; }
        //public string EFOC { get; set; }
        //public string EFOP { get; set; }
        //public string Bowtie { get; set; }
        //public string StarLength { get; set; }
        //public string LowerHalf { get; set; }
        //public double NetDollar { get; set; }
        //public double Dcaret { get; set; }
    }

    public class KBS_LoginRequest
    {
        public string grant_type { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    public class KBS_LoginResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string userName { get; set; }
    }
    public class BHAVYAM_LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string grant_type { get; set; }
        public string DeviceType { get; set; }
        public string IpAddress { get; set; }
    }
    public class VenusJewel_Login_Req
    {
        public string User_Name { get; set; }
        public string Password { get; set; }

    }
    public class VenusJewel_Login_Res
    {
        public string Token_Id { get; set; }
        public string Status { get; set; }
        public string Session_Time_out { get; set; }
        public string Status_Cd { get; set; }
    }
    public class JP_Login_Req
    {
        public string action { get; set; }
        public string vipid { get; set; }
        public string vippsd { get; set; }

    }
    public class JP_Login_Res
    {
        public string status { get; set; }
        public string message { get; set; }
        public obj_JP_Login_Res msgdata { get; set; }

    }
    public class obj_JP_Login_Res
    {
        public int id { get; set; }
        public string token { get; set; }
        public string vipaccount { get; set; }
        public string name { get; set; }
        public string creattime { get; set; }
        public string gender { get; set; }
        public string tellphone { get; set; }
        public string skype { get; set; }
        public string qq { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string ischeckname { get; set; }
        public string remark { get; set; }
        public string companyname { get; set; }
    }

    public class JP_Stock_Req
    {
        public string action { get; set; }
        public string token { get; set; }
        public int stockstatus { get; set; }
        public string address { get; set; }
        public string reportnos { get; set; }
        public int ispaged { get; set; }
        public int pageindex { get; set; }
    }
    public class JP_Stock_Res
    {
        public int status { get; set; }
        public string message { get; set; }
        public obj_JP_Stock_Res msgdata { get; set; }
    }
    public class obj_JP_Stock_Res
    {
        public long total { get; set; }
        public List<obj_JP_Stock_inner_Res> rows { get; set; }
    }
    public class obj_JP_Stock_inner_Res
    {
        public string stoneid { get; set; }
        public string productid { get; set; }
        public string shape { get; set; }
        public decimal carat { get; set; }
        public string color { get; set; }
        public string clarity { get; set; }
        public string cut { get; set; }
        public string polish { get; set; }
        public string symmetry { get; set; }
        public string fluorescence { get; set; }
        public string milky { get; set; }
        public string green { get; set; }
        public string black { get; set; }
        public int qcculet { get; set; }
        public string othertinge { get; set; }
        public string eyeclean { get; set; }
        public string measurement { get; set; }
        public string report { get; set; }
        public string reportno { get; set; }
        public string address { get; set; }
        public decimal rapprice { get; set; }
        public decimal saleback { get; set; }
        public decimal saledollorprice { get; set; }
        public string depth_scale { get; set; }
        public string table_scale { get; set; }
        public int stockstatus { get; set; }
        public int discountType { get; set; }
    }
    public class Sunrise_LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string grant_type { get; set; }
    }
    public class Sunrise_LoginResponse
    {
        public string access_token { get; set; }
    }
}

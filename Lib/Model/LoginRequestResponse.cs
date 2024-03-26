using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Model
{
    public class LoginFullResponse
    {
        [JsonProperty("access_token")]
        public string access_token { get; set; }

        [JsonProperty("token_type")]
        public string token_type { get; set; }

        [JsonProperty("expires_in")]
        public int expires_in { get; set; }

        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public int IsAdmin { get; set; }
        public int IsEmp { get; set; }
        public int IsGuest { get; set; }
    }

    public class LoginResponse
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string UserTypeId { get; set; }
        public string UserType { get; set; }
        public int TransID { get; set; }
    }

    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Source { get; set; }
        public string IpAddress { get; set; }
        public string UDID { get; set; }
        public string LoginMode { get; set; }
        public string DeviseType { get; set; }
        public string DeviceName { get; set; }
        public string AppVersion { get; set; }
        public string Location { get; set; }
        public string Login { get; set; }
        public string grant_type { get; set; }
        public string ProjectType { get; set; }
    }

    public class KeyAccountDataResponse
    {
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompName { get; set; }
        public int FortunePartyCode { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string UserTypeId { get; set; }
        public string UserType { get; set; }
        public int AssistBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string UserTypeList { get; set; }
        public int UserCode { get; set; }
        public bool View { get; set; }
        public bool Download { get; set; }
        public string IPAddress { get; set; }
        public string DeviceType { get; set; }
        public string MacID { get; set; }
        public bool IsPrimaryUser { get; set; }
        public bool IsSubUser { get; set; }
        public int PrimaryUserId { get; set; }
        public bool SearchStock { get; set; }
        public bool OrderHistoryAll { get; set; }
        public bool OrderHistoryByHisUser { get; set; }
        public bool PlaceOrder { get; set; }
        public bool MyCart { get; set; }
        public bool StockDownload { get; set; }
        public bool OrderHistoryDownload { get; set; }
        public bool OrderHistoryShowPricing { get; set; }
        public int SubUserCount { get; set; }
    }
    public class IP_Wise_Login_Detail
    {
        public string IPAddress { get; set; }
        public int? UserId { get; set; }
        public string Type { get; set; }
    }
}

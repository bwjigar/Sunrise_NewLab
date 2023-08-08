using Lib.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SunriseLabWeb_New.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Hosting;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Login([FromBody]JObject data)
        {
            LoginRequest loginRequest = new LoginRequest();
            try
            {
                loginRequest = JsonConvert.DeserializeObject<LoginRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, Request);
                return Ok(new LoginResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            LoginResponse resp;
            try
            {
                resp = CheckLogin(loginRequest);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(resp);
        }
        [NonAction]
        public LoginResponse CheckLogin(LoginRequest loginRequest)
        {
            String UserName, Password, IpAddress, Login = "";
            LoginResponse resp = new LoginResponse();
            try
            {
                UserName = loginRequest.UserName;
                Password = loginRequest.Password;
                IpAddress = loginRequest.IpAddress;

                string _strcheck = "";
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("Username", DbType.String, ParameterDirection.Input, UserName.ToUpper()));
                para.Add(db.CreateParam("Password", DbType.String, ParameterDirection.Input, Password));
                para.Add(db.CreateParam("IP_Add", DbType.String, ParameterDirection.Input, IpAddress));

                DataTable dt = db.ExecuteSP("Check_Login", para.ToArray(), false);

                DataTable dts = null;

                if (dt.Rows[0]["User_Code"].ToString().Length == 0)
                {
                    resp = new LoginResponse();
                    resp.Status = "0";

                    int AssistId = (dt.Rows[0]["AssistBy"] != null && dt.Rows[0]["AssistBy"].ToString() != "") ? Convert.ToInt32(dt.Rows[0]["AssistBy"].ToString()) : 0;
                    string AssistDetail = GetAssistDetail(AssistId);

                    resp.Message = "<div style=\"color:red\">User Name '" + UserName + "' or Password is Wrong, Kindly Contact : </div>" + AssistDetail;
                }
                else
                {
                    resp = new LoginResponse();
                    resp.UserName = UserName;
                    resp.Status = "1";
                    resp.Message = "SUCCESS";
                    resp.UserID = Convert.ToInt32(dt.Rows[0]["User_Code"]);
                    resp.UserTypeId = dt.Rows[0]["UserTypeId"].ToString();
                    resp.UserType = dt.Rows[0]["UserType"].ToString();
                    resp.TransID = Convert.ToInt32(dt.Rows[0]["Trans_Id"]);
                }
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, Request);
                resp.Status = "0";
                resp.Message = "Something Went wrong.\nPlease try again later";
            }
            return resp;
        }
        [NonAction]
        private string GetAssistDetail(int UserId)
        {
            string AssistDetail = string.Empty;
            if (UserId > 0)
            {
                string AssistName1 = string.Empty, AssistMobile1 = string.Empty, AssistEmail1 = string.Empty, WeChatId = string.Empty;
                Database db2 = new Database();
                List<IDbDataParameter> para2 = new List<IDbDataParameter>();
                para2.Add(db2.CreateParam("iiUserId", DbType.String, ParameterDirection.Input, Convert.ToInt32(UserId)));
                DataTable dt = db2.ExecuteSP("UserMas_SelectOne", para2.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    AssistName1 = dt.Rows[0]["FirstName"].ToString() + " " + dt.Rows[0]["LastName"].ToString();
                    AssistMobile1 = (dt.Rows[0]["MobileNo"] != null && dt.Rows[0]["MobileNo"].ToString() != "" ? dt.Rows[0]["MobileNo"].ToString() : "85227235100");
                    AssistEmail1 = (dt.Rows[0]["EmailId"] != null && dt.Rows[0]["EmailId"].ToString() != "" ? dt.Rows[0]["EmailId"].ToString() : "support@sunrisediam.com");

                    AssistDetail = "<table><tbody>";
                    AssistDetail += "<tr><td><i class=\"fa fa-user\" style=\"font-size: 20px; color: teal;\"></i></td>";
                    AssistDetail += "<td>&nbsp;" + AssistName1 + "</td>";
                    AssistDetail += "</tr><tr>";
                    AssistDetail += "<td><i class=\"fa fa-mobile\" style=\"font-size: 25px; color: #27c4cc;\"></i></td>";
                    AssistDetail += "<td>&nbsp;" + AssistMobile1 + "</td>";
                    AssistDetail += "</tr><tr>";
                    AssistDetail += "<td><i class=\"fa fa-envelope-o\" style=\"font-size: 20px; color: red;\"></i></td>";
                    AssistDetail += "<td>&nbsp;" + AssistEmail1 + "</td>";
                    AssistDetail += "</tr></tbody></table>";
                }
            }
            else
            {
                AssistDetail = "<table><tbody>";
                AssistDetail += "<tr><td><i class=\"fa fa-mobile\" style=\"font-size: 25px; color: #27c4cc;\"></i></td>";
                AssistDetail += "<td>&nbsp;+852-2723 5100</td>";
                AssistDetail += "</tr><tr>";
                AssistDetail += "<td><i class=\"fa fa-envelope-o\" style=\"font-size: 20px; color: red;\"></i></td>";
                AssistDetail += "<td>&nbsp;support@sunrisediam.com</td>";
                AssistDetail += "</tr></tbody></table>";
            }

            return AssistDetail;
        }
        [NonAction]
        private string GetUserIsActive(string AssistName, string AssistMobile, string AssistEmail)
        {
            string AssistDetail = string.Empty;
            AssistDetail = "<table><tbody>";
            AssistDetail += "<tr><td><i class=\"fa fa-mobile\" style=\"font-size: 25px; color: #27c4cc;\"></i></td>";
            AssistDetail += "<td>&nbsp;" + "+852-27235100" + "</td>";
            AssistDetail += "</tr><tr>";
            AssistDetail += "<td><i class=\"fa fa-envelope-o\" style=\"font-size: 20px; color: red;\"></i></td>";
            AssistDetail += "<td>&nbsp;" + "support@sunrisediam.com" + "</td>";
            AssistDetail += "</tr></tbody></table>";
            return "<div style=\"color:red\">User Is Not Active, Please Contact : </div>" + AssistDetail;
        }
        [HttpPost]
        public IHttpActionResult GetKeyAccountData([FromBody]JObject data)
        {
            LoginRequest loginRequest = new LoginRequest();
            try
            {
                loginRequest = JsonConvert.DeserializeObject<LoginRequest>(data.ToString());
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<LoginRequest>
                {
                    Data = new List<LoginRequest>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database(Request);
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("ssUsername", DbType.String, ParameterDirection.Input, loginRequest.UserName));
                para.Add(db.CreateParam("ssPassword", DbType.String, ParameterDirection.Input, loginRequest.Password));
                DataTable dt = db.ExecuteSP("UserMas_SelectByUsername", para.ToArray(), false);

                List<KeyAccountDataResponse> keyAccountDataResponse = DataTableExtension.ToList<KeyAccountDataResponse>(dt);
                keyAccountDataResponse.FirstOrDefault().Password = loginRequest.Password;

                return Ok(new ServiceResponse<KeyAccountDataResponse>
                {
                    Data = keyAccountDataResponse,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<KeyAccountDataResponse>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult IP_Wise_Login_Detail([FromBody]JObject data)
        {
            IP_Wise_Login_Detail ip_wise_login_detailrequest = new IP_Wise_Login_Detail();
            try
            {
                ip_wise_login_detailrequest = JsonConvert.DeserializeObject<IP_Wise_Login_Detail>(data.ToString());
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<IP_Wise_Login_Detail>
                {
                    Data = new List<IP_Wise_Login_Detail>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                if (!string.IsNullOrEmpty(ip_wise_login_detailrequest.UserId.ToString()))
                    para.Add(db.CreateParam("UserId", DbType.String, ParameterDirection.Input, ip_wise_login_detailrequest.UserId));
                else
                    para.Add(db.CreateParam("UserId", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ip_wise_login_detailrequest.IPAddress))
                    para.Add(db.CreateParam("IPAddress", DbType.String, ParameterDirection.Input, ip_wise_login_detailrequest.IPAddress));
                else
                    para.Add(db.CreateParam("IPAddress", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(ip_wise_login_detailrequest.Type))
                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, ip_wise_login_detailrequest.Type));
                else
                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, DBNull.Value));

                System.Data.DataTable dtData = db.ExecuteSP("IP_Wise_Login_Detail_Stored_CRUD", para.ToArray(), false);
                List<IP_Wise_Login_Detail> ListResponses = new List<IP_Wise_Login_Detail>();

                if (dtData != null && dtData.Rows.Count > 0) {
                   ListResponses = DataTableExtension.ToList<IP_Wise_Login_Detail>(dtData);
                }
                
                return Ok(new ServiceResponse<IP_Wise_Login_Detail>
                {
                    Data = ListResponses,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult LogoutWithoutToken([FromBody] JObject data)
        {
            try
            {
                if (System.Web.HttpContext.Current.Application["UserIdCookie"] != null)
                {
                    System.Web.HttpContext.Current.Application.Remove("UserIdCookie");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }
        }
    }
}

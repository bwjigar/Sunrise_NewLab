using Lib.Model;
using Newtonsoft.Json;
using SunriseLabWeb_New.Data;
using SunriseLabWeb.Helper;
using SunriseLabWeb_New.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Threading;

namespace SunriseLabWeb_New.Controllers
{
    public class LoginController : Controller
    {
        API _api = new API();
        Data.Common _common = new Data.Common();
        public ActionResult Index()
        {
            UserLogin _obj = new UserLogin();
            if (Request.Cookies["Username"] != null && Request.Cookies["Password"] != null && Request.Cookies["IsRemember"] != null)
            {
                _obj.Username = Request.Cookies["Username"].Value.ToString();
                _obj.Password = Request.Cookies["Password"].Value.ToString();
                _obj.isRemember = Convert.ToBoolean(Request.Cookies["IsRemember"].Value);
            }
            ViewBag.Message = "Please enter correct username and password";
            return View(_obj);
        }
        [HttpPost]
        public ActionResult Index(UserLogin _obj)
        {
            if (ModelState.IsValid)
            {
                string _ipAddress = _common.gUserIPAddresss();

                if (!String.IsNullOrEmpty(_obj.Username) && !String.IsNullOrEmpty(_obj.Password) && !String.IsNullOrEmpty(_ipAddress))
                {
                    var input = new LoginRequest
                    {
                        UserName = _obj.Username,
                        Password = _obj.Password,
                        IpAddress = _ipAddress,
                        grant_type = "password"
                    };
                    string inputJson = string.Join("&", input.GetType()
                                                                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                                .Where(p => p.GetValue(input, null) != null)
                                       .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(input).ToString())}"));


                    string _response = _api.CallAPIUrlEncoded(Constants.UserLogin, inputJson);
                    if (_response.ToLower().Contains(@"""error") && _response.ToLower().Contains(@"""error_description"))
                    {
                        OAuthErrorMsg _authErrorMsg = new OAuthErrorMsg();
                        _authErrorMsg = (new JavaScriptSerializer()).Deserialize<OAuthErrorMsg>(_response);

                        TempData["Message"] = _authErrorMsg.error_description;
                    }
                    else
                    {
                        LoginFullResponse _data = new LoginFullResponse();
                        try
                        {
                            _data = (new JavaScriptSerializer()).Deserialize<LoginFullResponse>(_response);
                        }
                        catch (WebException ex)
                        {
                            var webException = ex as WebException;
                            if ((Convert.ToString(webException.Status)).ToUpper() == "PROTOCOLERROR")
                            {
                                OAuthErrorMsg error =
                                    JsonConvert.DeserializeObject<OAuthErrorMsg>(
                                   API.ExtractResponseString(webException));
                                TempData["Message"] = error.error_description;
                            }
                            TempData["Message"] = ex.Message;
                        }
                        catch (Exception ex)
                        {
                            TempData["Message"] = ex.Message;
                        }

                        if (_data != null)
                        {
                            if (_data.UserID > 0)
                            {
                                SessionFacade.TokenNo = _data.access_token;
                                inputJson = (new JavaScriptSerializer()).Serialize(input);
                                string _keyresponse = _api.CallAPI(Constants.KeyAccountData, inputJson);
                                ServiceResponse<KeyAccountDataResponse> _objresponse = (new JavaScriptSerializer()).Deserialize<ServiceResponse<KeyAccountDataResponse>>(_keyresponse);

                                //string _imageResponse = _api.CallAPI(Constants.GetUserProfilePicture, string.Empty);

                                if (_objresponse.Data != null && _objresponse.Data.Count > 0)
                                {
                                    _objresponse.Data[0].IPAddress = _ipAddress;
                                    _objresponse.Data[0].DeviceType = "Web";
                                    _objresponse.Data[0].MacID = "";

                                    SessionFacade.UserSession = _objresponse.Data.FirstOrDefault();

                                    var obj = _objresponse.Data.FirstOrDefault();

                                    Response.Cookies["Userid_DNA"].Value = obj.UserId.Value.ToString();

                                    var _input1 = new
                                    {
                                        IPAddress = GetIpValue(),
                                        UserId = obj.UserId,
                                        Type = "STORED"
                                    };
                                    var _inputJson_1 = (new JavaScriptSerializer()).Serialize(_input1);
                                    string _Response_1 = _api.CallAPI(Constants.IP_Wise_Login_Detail, _inputJson_1);

                                }
                                if (_obj.isRemember)
                                {
                                    Response.Cookies["UserName"].Value = _obj.Username;
                                    Response.Cookies["Password"].Value = _obj.Password;
                                    Response.Cookies["IsRemember"].Value = _obj.isRemember.ToString();
                                }
                                else
                                {
                                    Response.Cookies["UserName"].Value = "";
                                    Response.Cookies["Password"].Value = "";
                                    Response.Cookies["IsRemember"].Value = "false";
                                }

                                return RedirectToAction("Index", "DashBoard");
                            }
                            else
                            {
                                TempData["Message"] = _data.Message;
                            }
                        }
                    }
                }
                else
                {
                    TempData["Message"] = "";
                    return RedirectToAction("Index", "Login");
                }
            }
            return View(_obj);
        }
        public ActionResult ForgotPassword()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var userName = Request["ForgotUsername"];
                    var input = new
                    {
                        UserName = userName
                    };
                    string inputJson = (new JavaScriptSerializer()).Serialize(input);
                    string _response = _api.CallAPIWithoutToken(Constants.ForgotPassword, inputJson);

                    CommonResponse _data = new CommonResponse();
                    _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);

                    TempData["Message"] = _data.Message;
                }
                catch (WebException ex)
                {
                    //if (ex.Status)
                    var webException = ex as WebException;
                    if ((Convert.ToString(webException.Status)).ToUpper() == "PROTOCOLERROR")
                    {
                        OAuthErrorMsg error =
                            JsonConvert.DeserializeObject<OAuthErrorMsg>(
                           API.ExtractResponseString(webException));
                        TempData["Message"] = error.error_description;
                    }
                    else
                    {
                        TempData["Message"] = ex.Message;
                    }
                }
            }
            return RedirectToAction("Index", "Login");
        }
        public string GetIpValue()
        {
            string ipAdd = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(ipAdd))
            {
                ipAdd = Request.ServerVariables["REMOTE_ADDR"];
            }
            else
            {
                // lblIPAddress.Text = ipAdd;
            }
            return ipAdd;
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            SessionFacade.Abandon();

            var _input1 = new
            {
                IPAddress = GetIpValue(),
                UserId = 0,
                Type = "EXPIRED"
            };
            var _inputJson_1 = (new JavaScriptSerializer()).Serialize(_input1);
            string _Response_1 = _api.CallAPI(Constants.IP_Wise_Login_Detail, _inputJson_1);

            string _response = _api.CallAPIWithoutToken(Constants.LogoutWithoutToken, "");

            ViewData["URL"] = ConfigurationManager.AppSettings["SunriseLabWeb_New_URL"];

            return View();
        }
        public JsonResult LoginCheck()
        {
            string _response = _api.CallAPI("/User/LoginCheck", String.Empty);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(_response);
            return Json(_data != null ? _data.Message : "UNAUTHORIZED", JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddUpdate_SupplierStock()
        {
            Thread APIGet = new Thread(AddUpdate_SupplierStock_Thread);
            APIGet.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void AddUpdate_SupplierStock_Thread()
        {
            API _api = new API();
            _api.CallAPIWithoutToken(Constants.AddUpdate_SupplierStock, string.Empty);
        }
        public JsonResult Supplier_File_Stock_Validity_Scheduler()
        {
            Thread APIGet = new Thread(Supplier_File_Stock_Validity_Scheduler_Thread);
            APIGet.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void Supplier_File_Stock_Validity_Scheduler_Thread()
        {
            API _api = new API();
            _api.CallAPIWithoutToken(Constants.Supplier_File_Stock_Validity_Scheduler, string.Empty);
        }

        public JsonResult Auto_Excel_Download()
        {
            Thread APIGet = new Thread(Auto_Excel_Download_Thread);
            APIGet.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void Auto_Excel_Download_Thread()
        {
            API _api = new API();
            _api.CallAPIWithoutToken(Constants.Auto_Excel_Download, string.Empty);
        }

        public JsonResult RapaPort_Data_Upload_Ora()
        {
            Thread APIGet = new Thread(RapaPort_Data_Upload_Ora_Thread);
            APIGet.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void RapaPort_Data_Upload_Ora_Thread()
        {
            API _api = new API();
            _api.CallAPIWithoutToken(Constants.RapaPort_Data_Upload_Ora, string.Empty);
        }

        public JsonResult API_RESPONSE_CHECK()
        {
            Thread APIGet = new Thread(API_RESPONSE_CHECK_Thread);
            APIGet.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void API_RESPONSE_CHECK_Thread()
        {
            API _api = new API();
            _api.CallAPIWithoutToken("/User/API_RESPONSE_CHECK", string.Empty);
        }

        public JsonResult get_stock_disc_Ora()
        {
            Thread APIGet = new Thread(get_stock_disc_Ora_Thread);
            APIGet.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void get_stock_disc_Ora_Thread()
        {
            API _api = new API();
            _api.CallAPIWithoutToken(Constants.get_stock_disc_Ora, string.Empty);
        }
        public JsonResult get_sal_disc_new_Ora()
        {
            Thread APIGet = new Thread(get_sal_disc_new_Ora_Thread);
            APIGet.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void get_sal_disc_new_Ora_Thread()
        {
            API _api = new API();
            _api.CallAPIWithoutToken(Constants.get_sal_disc_new_Ora, string.Empty);
        }
        public JsonResult get_stock_kts_Ora()
        {
            Thread APIGet = new Thread(get_stock_kts_Ora_Thread);
            APIGet.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void get_stock_kts_Ora_Thread()
        {
            API _api = new API();
            _api.CallAPIWithoutToken(Constants.get_stock_kts_Ora, string.Empty);
        }
        public JsonResult get_sal_clg_new_Ora()
        {
            Thread APIGet = new Thread(get_sal_clg_new_Ora_Thread);
            APIGet.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void get_sal_clg_new_Ora_Thread()
        {
            API _api = new API();
            _api.CallAPIWithoutToken(Constants.get_sal_clg_new_Ora, string.Empty);
        }
        public JsonResult get_pur_disc_Ora()
        {
            Thread APIGet = new Thread(get_pur_disc_Ora_Thread);
            APIGet.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void get_pur_disc_Ora_Thread()
        {
            API _api = new API();
            _api.CallAPIWithoutToken(Constants.get_pur_disc_Ora, string.Empty);
        }

        public JsonResult lab_entry_notification_Ora()
        {
            Thread APIGet = new Thread(lab_entry_notification_Ora_Thread);
            APIGet.Start();
            return Json("", JsonRequestBehavior.AllowGet);
        }
        public static void lab_entry_notification_Ora_Thread()
        {
            API _api = new API();
            _api.CallAPIWithoutToken(Constants.lab_entry_notification_Ora, string.Empty);
        }
    }
}
using Lib.Model;
using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using SunriseLabWeb_New.Data;
using SunriseLabWeb_New.Filter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Services.Description;

namespace SunriseLabWeb_New.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class UserController : Controller
    {
        API _api = new API();
        public ActionResult Manage()
        {
            return View();
        }
        public JsonResult GetUsers(GetUsers_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.GetUsers, inputJson);
            ServiceResponse<GetUsers_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<GetUsers_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveUserData(UserDetails_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.SaveUserData, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete_UserMas(GetUsers_Res req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Delete_UserMas, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult FortunePartyCode_Exist(Exist_Request req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.FortunePartyCode_Exist, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UserCode_Exists(Exist_Request req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.UserCode_Exists, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_UserType()
        {
            string response = _api.CallAPI(Constants.get_UserType, string.Empty);
            ServiceResponse<UserType_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<UserType_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_ColumnMaster(Get_CategoryMas_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_ColumnMaster, inputJson);
            ServiceResponse<Get_ColumnMaster_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_ColumnMaster_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Category()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Category_Master()
        {
            return PartialView("Category_Master");
        }
        public JsonResult Get_CategoryMas(Get_CategoryMas_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_CategoryMas, inputJson);
            ServiceResponse<Get_CategoryMas_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_CategoryMas_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddUpdate_CategoryMas(Get_CategoryMas_Res req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.AddUpdate_CategoryMas, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Category_Value()
        {
            return PartialView("Category_Value");
        }
        public JsonResult Get_Category_Value(Get_Category_Value_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_Category_Value, inputJson);
            ServiceResponse<Get_Category_Value_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_Category_Value_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddUpdate_Category_Value(Get_Category_Value_Res req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.AddUpdate_Category_Value, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_PriceListCategory()
        {
            string response = _api.CallAPI(Constants.Get_PriceListCategory, string.Empty);
            ServiceResponse<Get_PriceListCategory_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_PriceListCategory_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SupplierValue()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SupplierValue_AddNew()
        {
            return PartialView("SupplierValue_AddNew");
        }
        public JsonResult Get_Supplier_Value(Get_Supplier_Value_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_Supplier_Value, inputJson);
            ServiceResponse<Get_Supplier_Value_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_Supplier_Value_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddUpdate_Supplier_Value(AddUpdate_Supplier_Value_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.AddUpdate_Supplier_Value, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete_Supplier_Value(Get_Supplier_Value_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Delete_Supplier_Value, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SupplierMas()
        {
            return View();
        }
        public JsonResult Get_Supplier_ForSearchStock(Get_SupplierMaster_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_Supplier_ForSearchStock, inputJson);
            ServiceResponse<Get_SupplierMaster_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SupplierMaster_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_SupplierMaster(Get_SupplierMaster_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_SupplierMaster, inputJson);
            ServiceResponse<Get_SupplierMaster_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SupplierMaster_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddUpdate_SupplierMaster(Get_SupplierMaster_Res req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.AddUpdate_SupplierMaster, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_Not_Mapped_SupplierStock(Get_SearchStock_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_Not_Mapped_SupplierStock, inputJson);
            string data = (new JavaScriptSerializer()).Deserialize<string>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SupplierColumnSetting()
        {
            return View();
        }
        public JsonResult Get_SupplierColumnSetting(Obj_CategoryDet_List req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_SupplierColumnSetting, inputJson);
            ServiceResponse<Get_SupplierColumnSetting_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SupplierColumnSetting_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_SupplierColumnSetting_FromAPI(Obj_CategoryDet_List req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            //string response = _api.CallAPI(Constants.Get_SupplierColumnSetting_FromAPI, inputJson);
            string response = _api.CallAPIUrlEncodedWithWebReq(Constants.Get_SupplierColumnSetting_FromAPI, inputJson);
            ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddUpdate_SupplierColumnSetting(AddUpdate_SupplierColumnSetting_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.AddUpdate_SupplierColumnSetting, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete_SupplierColumnSetting(Obj_CategoryDet_List req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Delete_SupplierColumnSetting, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SupplierColumnSettingFromFile()
        {
            return View();
        }
        public JsonResult Get_SheetName_From_File(Data_Get_From_File_Req req)
        {
            ServiceResponse<Get_SheetName_From_File_Res> data = new ServiceResponse<Get_SheetName_From_File_Res>();
            try
            {
                if (Request.Files.Count > 0)
                {
                    string folder = Server.MapPath("~/Stock_File/");
                    string ProjectName = ConfigurationManager.AppSettings["ProjectName"];
                    string APIName = ConfigurationManager.AppSettings["APIName"];

                    folder = folder.Replace("\\" + ProjectName + "\\", "\\" + APIName + "\\");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname = file.FileName;
                        string NewFileName = req.SupplierId + "_SheetName_" + Guid.NewGuid() + Path.GetExtension(fname).ToLower();

                        string savePath = Path.Combine(folder, NewFileName);
                        file.SaveAs(savePath);

                        req.FilePath = savePath;
                    }
                    string inputJson = (new JavaScriptSerializer()).Serialize(req);
                    string response = _api.CallAPIUrlEncodedWithWebReq(Constants.Get_SheetName_From_File, inputJson);
                    data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SheetName_From_File_Res>>(response);
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    data.Message = "File Not Exists";
                    data.Status = "0";
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                data.Message = "Message " + ex.Message + " StackTrace " + ex.StackTrace;
                data.Status = "0";
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Get_Data_From_File(Data_Get_From_File_Req req)
        {
            ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res> data = new ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res>();
            try
            {
                if (Request.Files.Count > 0)
                {
                    string folder = Server.MapPath("~/Stock_File/");
                    string ProjectName = ConfigurationManager.AppSettings["ProjectName"];
                    string APIName = ConfigurationManager.AppSettings["APIName"];

                    folder = folder.Replace("\\" + ProjectName + "\\", "\\" + APIName + "\\");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname = file.FileName;
                        string NewFileName = req.SupplierId + "_ColSetting_" + Guid.NewGuid() + Path.GetExtension(fname).ToLower();

                        string savePath = Path.Combine(folder, NewFileName);
                        file.SaveAs(savePath);

                        req.FilePath = savePath;
                    }
                    string inputJson = (new JavaScriptSerializer()).Serialize(req);
                    string response = _api.CallAPIUrlEncodedWithWebReq(Constants.Get_Data_From_File, inputJson);
                    data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res>>(response);
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    data.Message = "File Not Exists";
                    data.Status = "0";
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                data.Message = "Message " + ex.Message + " StackTrace " + ex.StackTrace;
                data.Status = "0";
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult Get_SupplierColumnSetting_FromFile(Obj_CategoryDet_List req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_SupplierColumnSetting_FromFile, inputJson);
            ServiceResponse<Get_SupplierColumnSetting_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SupplierColumnSetting_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddUpdate_SupplierColumnSetting_FromFile(AddUpdate_SupplierColumnSetting_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.AddUpdate_SupplierColumnSetting_FromFile, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete_SupplierColumnSetting_FromFile(Obj_CategoryDet_List req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Delete_SupplierColumnSetting_FromFile, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SupplierStockUpload()
        {
            return View();
        }
        /*
        public JsonResult AddUpdate_SupplierStock_FromFile(Data_Get_From_File_Req req)
        {
            CommonResponse data = new CommonResponse();
            try
            {
                if (Request.Files.Count > 0)
                {
                    string folder = Server.MapPath("~/Stock_File/");
                    string ProjectName = ConfigurationManager.AppSettings["ProjectName"];
                    string APIName = ConfigurationManager.AppSettings["APIName"];

                    folder = folder.Replace("\\" + ProjectName + "\\", "\\" + APIName + "\\");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname = file.FileName;
                        string NewFileName = req.SupplierId + "_StockUpload_" + Guid.NewGuid() + Path.GetExtension(fname).ToLower();

                        string savePath = Path.Combine(folder, NewFileName);
                        file.SaveAs(savePath);

                        req.FilePath = savePath;
                    }
                    string inputJson = (new JavaScriptSerializer()).Serialize(req);
                    string response = _api.CallAPIUrlEncodedWithWebReq(Constants.AddUpdate_SupplierStock_FromFile, inputJson);
                    data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    data.Message = "File Not Exists";
                    data.Status = "0";
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                data.Message = "Message " + ex.Message + " StackTrace " + ex.StackTrace;
                data.Status = "0";
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        */

        public JsonResult Thread_AddUpdate_SupplierStock_FromFile(Data_Get_From_File_Req req)
        {
            CommonResponse data = new CommonResponse();
            try
            {
                if (Request.Files.Count > 0)
                {
                    string folder = Server.MapPath("~/Stock_File/");
                    string ProjectName = ConfigurationManager.AppSettings["ProjectName"];
                    string APIName = ConfigurationManager.AppSettings["APIName"];

                    folder = folder.Replace("\\" + ProjectName + "\\", "\\" + APIName + "\\");

                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }

                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname = file.FileName;
                        string NewFileName = req.SupplierId + "_StockUpload_" + Guid.NewGuid() + Path.GetExtension(fname).ToLower();

                        string savePath = Path.Combine(folder, NewFileName);
                        file.SaveAs(savePath);

                        req.FilePath = savePath;
                    }
                    string inputJson = (new JavaScriptSerializer()).Serialize(req);
                    string response = _api.CallAPI(Constants.Add_Stock_FileUpload_Request, inputJson);
                    data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);

                    String arg = req.UserId + "_" + data.Status + "_" + data.Message;

                    Thread APIGet = new Thread(AddUpdate_SupplierStock_FromFile_Thread);
                    APIGet.Start(arg);
                    return Json("1_Stock Upload Process is Started Response Message will get Shortly", JsonRequestBehavior.AllowGet);

                    //return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("0_File Not Exists", JsonRequestBehavior.AllowGet);
                    //data.Message = "File Not Exists";
                    //data.Status = "0";
                    //return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json("0_" + "Message " + ex.Message + " StackTrace " + ex.StackTrace, JsonRequestBehavior.AllowGet);
                //data.Message = "Message " + ex.Message + " StackTrace " + ex.StackTrace;
                //data.Status = "0";
                //return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        public void AddUpdate_SupplierStock_FromFile_Thread(object arg)
        {
            String arg1 = arg.ToString();
            API _api = new API();
            Data_Get_From_File_Req req = new Data_Get_From_File_Req();
            string[] Req = arg1.Split('_');
            
            if (Req[1] =="1" && Req[2] != "Failed")
            {
                req.UserId = Convert.ToInt32(Req[0]);
                req.Id = Convert.ToInt32(Req[2]);

                string inputJson = (new JavaScriptSerializer()).Serialize(req);
                string response = _api.CallAPIUrlEncodedWithWebReq(Constants.AddUpdate_SupplierStock_FromFile, inputJson);
                CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);

                StockUpload_Response_Res req1 = new StockUpload_Response_Res();
                req1.Message = data.Message;
                req1.Status = Convert.ToInt32(data.Status);
                req1.UserId = req.UserId;

                string inputJson1 = (new JavaScriptSerializer()).Serialize(req1);
                string response1 = _api.CallAPIUrlEncodedWithWebReq(Constants.Add_StockUpload_Response, inputJson1);
            }
        }

        /*
        public JsonResult Thread_AddUpdate_SupplierStock_FromSupplier(VendorResponse req)
        {
            String arg = req.SUPPLIER + '_' + req.Id;

            Thread APIGet = new Thread(AddUpdate_SupplierStock_FromSupplier_Thread);
            APIGet.Start(arg);
            return Json("Stock Upload Process is Started Response Message will get Shortly", JsonRequestBehavior.AllowGet);
        }
        public void AddUpdate_SupplierStock_FromSupplier_Thread(object arg)
        {
            String arg1 = arg.ToString();
            API _api = new API();
            VendorResponse req = new VendorResponse();
            string[] Req = arg1.Split('_');
            req.SUPPLIER = Req[0];
            req.Id = Convert.ToInt32(Req[1]);

            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPIUrlEncodedWithWebReq(Constants.AddUpdate_SupplierStock, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);

            StockUpload_Response_Res req1 = new StockUpload_Response_Res();
            req1.Message = data.Message;
            req1.Status = Convert.ToInt32(data.Status);
            req1.UserId = req.Id;

            string inputJson1 = (new JavaScriptSerializer()).Serialize(req1);
            string response1 = _api.CallAPIUrlEncodedWithWebReq(Constants.Add_StockUpload_Response, inputJson1);
        }  
        */
        public JsonResult Get_FancyColor()
        {
            string response = _api.CallAPI(Constants.Get_FancyColor, string.Empty);
            ServiceResponse<Get_FancyColor_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_FancyColor_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult SupplierPriceList()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult RefNoPrefix()
        {
            return PartialView("RefNoPrefix");
        }
        [ChildActionOnly]
        public ActionResult SupplierDisc()
        {
            return PartialView("SupplierDisc");
        }

        public JsonResult Get_ParaMas()
        {
            string response = _api.CallAPI(Constants.Get_ParaMas, string.Empty);
            ServiceResponse<Get_ParaMas_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_ParaMas_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_PriceList_ParaMas()
        {
            string response = _api.CallAPI(Constants.Get_PriceList_ParaMas, string.Empty);
            ServiceResponse<Get_PriceList_ParaMas> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_PriceList_ParaMas>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult get_key_to_symbol()
        {
            string response = _api.CallAPI(Constants.get_key_to_symbol, string.Empty);
            ServiceResponse<get_key_to_symbol> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<get_key_to_symbol>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_Supplier_RefNo_Prefix(Get_Supplier_RefNo_Prefix_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_Supplier_RefNo_Prefix, inputJson);
            ServiceResponse<Get_Supplier_RefNo_Prefix_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_Supplier_RefNo_Prefix_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddUpdate_Supplier_RefNo_Prefix(Save_Supplier_RefNo_Prefix_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.AddUpdate_Supplier_RefNo_Prefix, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete_Supplier_RefNo_Prefix(Obj_Supplier_RefNo_Prefix_List req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Delete_Supplier_RefNo_Prefix, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddUpdate_Supplier_Disc(Save_Supplier_Disc_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.AddUpdate_Supplier_Disc, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_Supplier_Disc(Save_Supplier_Disc_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_Supplier_Disc, inputJson);
            ServiceResponse<Obj_Supplier_Disc> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Obj_Supplier_Disc>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SearchStock()
        {
            return View();
        }
        public JsonResult Get_SearchStock(Get_SearchStock_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPIUrlEncodedWithWebReq(Constants.Get_SearchStock, inputJson);
            ServiceResponse<SearchDiamondsResponse> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<SearchDiamondsResponse>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Excel_SearchStock(Get_SearchStock_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPIUrlEncodedWithWebReq(Constants.Excel_SearchStock, inputJson);
            string data = (new JavaScriptSerializer()).Deserialize<string>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CustomerPriceList()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult CustomerDisc()
        {
            return PartialView("CustomerDisc");
        }
        public JsonResult AddUpdate_Customer_Disc(Save_Supplier_Disc_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.AddUpdate_Customer_Disc, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_Customer_Disc(Save_Supplier_Disc_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_Customer_Disc, inputJson);
            ServiceResponse<Obj_Supplier_Disc> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Obj_Supplier_Disc>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult StockDiscMgt()
        {
            return View();
        }
        public JsonResult AddUpdate_Customer_Stock_Disc(Save_Supplier_Disc_Req req)
        {
            Uri url = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);

            string AbsoluteUri = url.AbsoluteUri;
            string AbsolutePath = url.AbsolutePath;
            string mainurl = AbsoluteUri.Replace(AbsolutePath, "");

            string DecodedUsername = WebUtility.UrlEncode(Encrypt(req.UserName));
            //string DecodedPassword = WebUtility.UrlEncode(Encrypt(req.Password));
            req.URL = mainurl + "/User/URL?UN=" + DecodedUsername + "&TransId=";

            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.AddUpdate_Customer_Stock_Disc, inputJson);
            CommonResponse _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(_data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_Customer_Stock_Disc(Save_Supplier_Disc_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_Customer_Stock_Disc, inputJson);
            ServiceResponse<Obj_Supplier_Disc> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Obj_Supplier_Disc>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_Customer_Stock_Disc_Mas(Save_Supplier_Disc_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_Customer_Stock_Disc_Mas, inputJson);
            ServiceResponse<Get_Customer_Stock_Disc_Mas_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_Customer_Stock_Disc_Mas_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddUpdate_SupplierStock_FromSupplier(VendorResponse req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPIUrlEncodedWithWebReq(Constants.AddUpdate_SupplierStock, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Thread_AddUpdate_SupplierStock_FromSupplier(VendorResponse req)
        {
            String arg = req.SUPPLIER + '_' + req.Id;

            Thread APIGet = new Thread(AddUpdate_SupplierStock_FromSupplier_Thread);
            APIGet.Start(arg);
            return Json("Stock Upload Process is Started Response Message will get Shortly", JsonRequestBehavior.AllowGet);
        }
        public void AddUpdate_SupplierStock_FromSupplier_Thread(object arg)
        {
            String arg1 = arg.ToString();
            API _api = new API();
            VendorResponse req = new VendorResponse();
            string[] Req = arg1.Split('_');
            req.SUPPLIER = Req[0];
            req.Id = Convert.ToInt32(Req[1]);

            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPIUrlEncodedWithWebReq(Constants.AddUpdate_SupplierStock, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);

            StockUpload_Response_Res req1 = new StockUpload_Response_Res();
            req1.Message = data.Message;
            req1.Status = Convert.ToInt32(data.Status);
            req1.UserId = req.Id;

            string inputJson1 = (new JavaScriptSerializer()).Serialize(req1);
            string response1 = _api.CallAPIUrlEncodedWithWebReq(Constants.Add_StockUpload_Response, inputJson1);
        }

        public ActionResult ColumnSetting()
        {
            return View();
        }
        public JsonResult Get_ColumnSetting_UserWise(GetUsers_Res req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_ColumnSetting_UserWise, inputJson);
            ServiceResponse<Get_ColumnSetting_UserWise_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_ColumnSetting_UserWise_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddUpdate_ColumnSetting_UserWise(Save_ColumnSetting_UserWise req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.AddUpdate_ColumnSetting_UserWise, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_SearchStock_ColumnSetting(Get_SearchStock_ColumnSetting_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_SearchStock_ColumnSetting, inputJson);
            ServiceResponse<Get_SearchStock_ColumnSetting_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SearchStock_ColumnSetting_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_StockUpload_Response()
        {
            string response = _api.CallAPI(Constants.Get_StockUpload_Response, string.Empty);
            ServiceResponse<StockUpload_Response_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<StockUpload_Response_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PlaceOrder(PlaceOrder_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.PlaceOrder, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrderHistory()
        {
            return View();
        }
        public JsonResult Get_OrderHistory(Get_OrderHistory_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_OrderHistory, inputJson);
            ServiceResponse<Get_OrderHistory_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_OrderHistory_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Excel_OrderHistory(Get_OrderHistory_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Excel_OrderHistory, inputJson);
            string data = (new JavaScriptSerializer()).Deserialize<string>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LabEntry()
        {
            return View();
        }
        public JsonResult Get_LabEntry(Get_SearchStock_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPIUrlEncodedWithWebReq(Constants.Get_LabEntry, inputJson);
            ServiceResponse<Get_SearchStock_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SearchStock_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Excel_LabEntry(Get_SearchStock_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPIUrlEncodedWithWebReq(Constants.Excel_LabEntry, inputJson);
            string data = (new JavaScriptSerializer()).Deserialize<string>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public List<Get_SearchStock_Res> Get_LabEntry_By_RefNo(Get_SearchStock_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPIUrlEncodedWithWebReq(Constants.Get_LabEntry, inputJson);
            ServiceResponse<Get_SearchStock_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SearchStock_Res>>(response);
            return data.Data;
        }
        public static string CapitalizeFirstLetterAfterSpace(string input)
        {
            string[] words = input.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrEmpty(words[i]))
                {
                    words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
                }
            }

            return string.Join(" ", words);
        }
        [HttpPost]
        public JsonResult UploadExcelforLabEntry(LabEntry_Req req)
        {
            List<Get_SearchStock_Res> lst = new List<Get_SearchStock_Res>();
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string NewFileName = Guid.NewGuid() + Path.GetExtension(fileName).ToLower();
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;

                    string path = Server.MapPath("~/Upload/LabExcel/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    file.SaveAs(Server.MapPath("~/Upload/LabExcel/") + NewFileName);

                    var ep = new ExcelPackage(new FileInfo(Server.MapPath("~/Upload/LabExcel/") + NewFileName));
                    var ws = ep.Workbook.Worksheets["StoneSelection"];
                    string Error_msg = string.Empty, Error_msg_1 = string.Empty;
                    int Error_count = 0;

                    Error_msg = "<table border='1' style='font-size:12px;width: 80%;margin-top:5px;display:block;max-height:360px;overflow-y:auto;'>";
                    Error_msg += "<tbody>";
                    Error_msg += "<tr>";
                    Error_msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 7%;\"><center><b>No.</b></center></td>";
                    Error_msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 27%;\"><center><b>Ref No</b></center></td>";
                    Error_msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 25%;\"><center><b>Supplier Cost Value($)</b></center></td>";
                    Error_msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 25%;\"><center><b>Sunrise Value($)</b></center></td>";
                    Error_msg += "</tr>";

                    string RefNo = "";
                    string Fix_Lab_Status = "CONFIRM, HOLD, BIDDED, WAITING, QC PENDING, QC REJECT, BID REJECT, SOLD, TRANSIT, BUSY, CANCEL, OTHER";
                    for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                    {
                        RefNo += Convert.ToString(ws.Cells[rw, 1].Value).Trim() + ",";
                    }
                    RefNo = (RefNo != "" ? RefNo.Remove(RefNo.Length - 1, 1) : "");

                    Get_SearchStock_Req Req = new Get_SearchStock_Req();
                    Req.RefNo = RefNo;
                    Req.UserId = req.UserId;

                    List<Get_SearchStock_Res> Res = new List<Get_SearchStock_Res>();
                    Res = Get_LabEntry_By_RefNo(Req);



                    bool status_1 = false;
                    for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                    {
                        if (Convert.ToString(ws.Cells[rw, 1].Value).Trim() != "" && RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(ws.Cells[rw, 4].Value)).Trim() != "" && RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(ws.Cells[rw, 5].Value)).Trim() != "")
                        {
                            status_1 = false;

                            for (int i = 0; i < Res.Count; i++)
                            {
                                if (Convert.ToString(ws.Cells[rw, 1].Value).Trim() == Res[i].Ref_No)
                                {
                                    string QCRequire = Convert.ToString(ws.Cells[rw, 2].Value).Trim();
                                    string LabStatus = Convert.ToString(ws.Cells[rw, 3].Value).Trim();

                                    decimal Supplier_Cost_Value = Convert.ToDecimal(RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(ws.Cells[rw, 4].Value)));
                                    decimal Supplier_Cost_Disc = ((-1 * (1 - (Supplier_Cost_Value / Res[i].Rap_Amount)) * 100));

                                    decimal Offer_Value = Convert.ToDecimal(RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(ws.Cells[rw, 5].Value)));
                                    decimal Offer_Disc = ((-1 * (1 - (Offer_Value / Res[i].Rap_Amount)) * 100));


                                    if (!Fix_Lab_Status.Contains(LabStatus.ToUpper()))
                                    {
                                        LabStatus = "";
                                    }
                                    Res[i].QCRequire = QCRequire;
                                    Res[i].LabEntry_Status = CapitalizeFirstLetterAfterSpace(LabStatus);
                                    Res[i].SUPPLIER_COST_DISC = Supplier_Cost_Disc;
                                    Res[i].SUPPLIER_COST_VALUE = Supplier_Cost_Value;
                                    Res[i].CUSTOMER_COST_DISC = Offer_Disc;
                                    Res[i].CUSTOMER_COST_VALUE = Offer_Value;

                                    lst.Add(Res[i]);
                                    status_1 = true;
                                }
                            }
                            if (status_1 == false)
                            {
                                status_1 = true;
                                Error_count += 1;

                                Error_msg_1 += "<tr>";
                                Error_msg_1 += "<td><center><b>" + Error_count + "</b></center></td>";
                                Error_msg_1 += "<td><center>" + ws.Cells[rw, 1].Value + "</center></td>";
                                Error_msg_1 += "<td style='color: #003d66;font-weight:600'><center>" + string.Format("{0:N2}", Convert.ToDouble(ws.Cells[rw, 4].Value)) + "</center></td>";
                                Error_msg_1 += "<td style='color: #003d66;font-weight:600'><center>" + string.Format("{0:N2}", Convert.ToDouble(ws.Cells[rw, 5].Value)) + "</center></td>";
                                Error_msg_1 += "</tr>";
                            }
                        }
                        else
                        {
                            Error_count += 1;

                            Error_msg_1 += "<tr>";
                            Error_msg_1 += "<td>center><b>" + Error_count + "</b></center></td>";
                            Error_msg_1 += "<td>center>" + ws.Cells[rw, 1].Value + "</center></td>";
                            Error_msg_1 += "<td style='color: #003d66;font-weight:600'>center>" + string.Format("{0:N2}", Convert.ToDouble(ws.Cells[rw, 4].Value)) + "</center></td>";
                            Error_msg_1 += "<td style='color: #003d66;font-weight:600'>center>" + string.Format("{0:N2}", Convert.ToDouble(ws.Cells[rw, 5].Value)) + "</center></td>";
                            Error_msg_1 += "</tr>";
                        }
                    }

                    if (string.IsNullOrEmpty(Error_msg_1))
                    {
                        Error_msg = "";
                    }
                    else
                    {
                        Error_msg += Error_msg_1;
                        Error_msg += "</tbody>";
                        Error_msg += "</table>";
                    }

                    Get_SearchStock_Res obj2 = new Get_SearchStock_Res();
                    obj2.Lab_Comments = Error_msg;
                    lst.Add(obj2);

                    return Json(lst, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Get_SearchStock_Res obj2 = new Get_SearchStock_Res();
                    obj2.Lab_Comments = "File Not Exists";
                    obj2.Culet = "0";
                    lst.Add(obj2);

                    return Json(lst, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Get_SearchStock_Res obj2 = new Get_SearchStock_Res();
                obj2.Lab_Comments = ex.Message;
                obj2.Culet = "0";
                lst.Add(obj2);

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
        }
        public static string RemoveNonNumericAndDotAndNegativeCharacters(string input)
        {
            //return new Regex("[^0-9.-]").Replace(input, "");
            string pattern = "[^0-9.-]";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(input, "");
            result = (result == "-" ? "" : result);
            return result;
        }
        public JsonResult Save_LabEntry(LabEntry_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Add_LabEntry_Request, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);

            Add_LabEntry_Res Res = new Add_LabEntry_Res();
            CommonResponse data_1 = new CommonResponse();

            if (data.Status == "1" && data.Message != "Failed")
            {
                Res.Id = Convert.ToInt32(data.Message);

                string inputJson_1 = (new JavaScriptSerializer()).Serialize(Res);
                string response_1 = _api.CallAPIUrlEncodedWithWebReq(Constants.Save_LabEntry, inputJson_1);
                data_1 = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response_1);
            }
            else
            {
                data_1.Status = "0";
                data_1.Message = "Lab Entry Failed";
            }
            return Json(data_1, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult MyCart()
        {
            return View();
        }
        public JsonResult Add_MyCart(Add_MyCart_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Add_MyCart, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete_MyCart(Get_MyCart_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Delete_MyCart, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_MyCart(Get_MyCart_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Get_MyCart, inputJson);
            ServiceResponse<Get_MyCart_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_MyCart_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Excel_MyCart(Get_MyCart_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Excel_MyCart, inputJson);
            string data = (new JavaScriptSerializer()).Deserialize<string>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LabAvailibility()
        {
            return View();
        }
        public List<Get_SearchStock_Res> Get_LabAvailibility_By_RefNo(Get_SearchStock_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPIUrlEncodedWithWebReq(Constants.Get_LabAvailibility, inputJson);
            ServiceResponse<Get_SearchStock_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SearchStock_Res>>(response);
            return data.Data;
        }
        public JsonResult Get_LabAvailibility(Get_SearchStock_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPIUrlEncodedWithWebReq(Constants.Get_LabAvailibility, inputJson);
            ServiceResponse<Get_SearchStock_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SearchStock_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Excel_LabAvailibility(Get_SearchStock_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPIUrlEncodedWithWebReq(Constants.Excel_LabAvailibility, inputJson);
            string data = (new JavaScriptSerializer()).Deserialize<string>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UploadExcelforLabAvailibility(LabEntry_Req req)
        {
            List<Get_SearchStock_Res> lst = new List<Get_SearchStock_Res>();
            try
            {
                if (Request.Files.Count > 0)
                {
                    HttpPostedFileBase file = Request.Files[0];
                    int fileSize = file.ContentLength;
                    string fileName = file.FileName;
                    string NewFileName = Guid.NewGuid() + Path.GetExtension(fileName).ToLower();
                    string mimeType = file.ContentType;
                    System.IO.Stream fileContent = file.InputStream;

                    string path = Server.MapPath("~/Upload/LabAvailibility/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    file.SaveAs(Server.MapPath("~/Upload/LabAvailibility/") + NewFileName);

                    var ep = new ExcelPackage(new FileInfo(Server.MapPath("~/Upload/LabAvailibility/") + NewFileName));
                    var ws = ep.Workbook.Worksheets["StoneSelection"];
                    string Error_msg = string.Empty, Error_msg_1 = string.Empty;
                    int Error_count = 0;

                    Error_msg = "<table border='1' style='font-size:12px;width: 40%;margin-top:5px;display:block;max-height:360px;overflow-y:auto;'>";
                    Error_msg += "<tbody>";
                    Error_msg += "<tr>";
                    Error_msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 5%;\"><center><b>No.</b></center></td>";
                    Error_msg += "<td style=\"background-color: #003d66;color: white;padding: 3px;width: 30%;\"><center><b>Ref No / Certi No</b></center></td>";
                    Error_msg += "</tr>";

                    string RefNo = "", RefNo1 = "";
                    for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                    {
                        RefNo += Convert.ToString(ws.Cells[rw, 1].Value).Trim() + ",";
                    }
                    RefNo = (RefNo != "" ? RefNo.Remove(RefNo.Length - 1, 1) : "");

                    Get_SearchStock_Req Req = new Get_SearchStock_Req();
                    Req.RefNo = RefNo;

                    List<Get_SearchStock_Res> Res = new List<Get_SearchStock_Res>();
                    Res = Get_LabAvailibility_By_RefNo(Req);


                    bool status_1 = false;
                    for (int rw = 2; rw <= ws.Dimension.End.Row; rw++)
                    {
                        if (Convert.ToString(ws.Cells[rw, 1].Value).Trim() != "")
                        {
                            status_1 = false;

                            for (int i = 0; i < Res.Count; i++)
                            {
                                if (Convert.ToString(ws.Cells[rw, 1].Value).Trim() == Res[i].Ref_No || Convert.ToString(ws.Cells[rw, 1].Value).Trim() == Res[i].Certificate_No)
                                {
                                    RefNo1 += Convert.ToString(ws.Cells[rw, 1].Value).Trim() + ",";

                                    lst.Add(Res[i]);
                                    status_1 = true;
                                }
                            }
                            if (status_1 == false)
                            {
                                status_1 = true;
                                Error_count += 1;

                                Error_msg_1 += "<tr>";
                                Error_msg_1 += "<td><center><b>" + Error_count + "</b></center></td>";
                                Error_msg_1 += "<td><center>" + ws.Cells[rw, 1].Value + "</center></td>";
                                Error_msg_1 += "</tr>";
                            }
                        }
                        else
                        {
                            Error_count += 1;

                            Error_msg_1 += "<tr>";
                            Error_msg_1 += "<td>center><b>" + Error_count + "</b></center></td>";
                            Error_msg_1 += "<td>center>" + ws.Cells[rw, 1].Value + "</center></td>";
                            Error_msg_1 += "</tr>";
                        }
                    }

                    RefNo1 = (RefNo1 != "" ? RefNo1.Remove(RefNo1.Length - 1, 1) : "");

                    if (string.IsNullOrEmpty(Error_msg_1))
                    {
                        Error_msg = "";
                    }
                    else
                    {
                        Error_msg += Error_msg_1;
                        Error_msg += "</tbody>";
                        Error_msg += "</table>";
                    }

                    Get_SearchStock_Res obj2 = new Get_SearchStock_Res();
                    obj2.Lab_Comments = Error_msg;
                    obj2.Supplier_Comments = RefNo1;
                    lst.Add(obj2);

                    return Json(lst, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    Get_SearchStock_Res obj2 = new Get_SearchStock_Res();
                    obj2.Lab_Comments = "File Not Exists";
                    obj2.Culet = "0";
                    lst.Add(obj2);

                    return Json(lst, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                Get_SearchStock_Res obj2 = new Get_SearchStock_Res();
                obj2.Lab_Comments = ex.Message;
                obj2.Culet = "0";
                lst.Add(obj2);

                return Json(lst, JsonRequestBehavior.AllowGet);
            }
        }
        
        private static Byte[] Key_64 = { 42, 16, 93, 156, 78, 4, 218, 32 };
        private static Byte[] Iv_64 = { 55, 103, 246, 79, 36, 99, 167, 3 };
        public static string Encrypt(string cValue, bool isFile = false)
        {
            string cAsVal = Decrypt(cValue);
            if (!cAsVal.Equals(cValue))
                return cValue;
            //if (!isFile)
            //{
            //    cValue = cValue.Replace("'", "•");
            //    if (IsNumeric(cValue))
            //        cValue = "A_°" + cValue;
            //}
            DESCryptoServiceProvider CryptoProvidor = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, CryptoProvidor.CreateEncryptor(Key_64, Iv_64), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs);
            sw.Write(cValue);
            sw.Flush();
            cs.FlushFinalBlock();
            ms.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, Convert.ToInt32(ms.Length));
        }
        public static string Decrypt(string cValue, bool isFile = false)
        {
            try
            {
                DESCryptoServiceProvider CryptoProvidor = new DESCryptoServiceProvider();
                Byte[] buf = new byte[cValue.Length];
                buf = Convert.FromBase64String(cValue);
                MemoryStream ms = new MemoryStream(buf);
                CryptoStream cs = new CryptoStream(ms, CryptoProvidor.CreateDecryptor(Key_64, Iv_64), CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                string cRetVal = sr.ReadToEnd();

                //if (!isFile)
                //{
                //    cRetVal = cRetVal.Replace("•", "'");//Tejas Add On 16/09/2011
                //    if (cRetVal.StartsWith("A_°"))
                //        cRetVal = cRetVal.Replace("A_°", "");
                //}
                return cRetVal;
            }
            catch //(Exception ex)
            {
                //MessageBox.Show("Error in Decryptstring : " + ex.Message);
            }
            return cValue;
        }
        public static string EncodeServerName(string serverName)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(serverName));
        }
        public static string DecodeServerName(string encodedServername)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(encodedServername));
        }
        public JsonResult URL(string UN, int TransId)
        {
            string username = Decrypt(UN);

            Get_URL_Req Req = new Get_URL_Req();
            Req.UserName = username;
            Req.TransId = TransId;

            string inputJson = (new JavaScriptSerializer()).Serialize(Req);
            string response = _api.CallAPIWithoutToken(Constants.Add_Customer_Stock_Disc_Mas_Request, inputJson);
            CommonResponse data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response);

            Add_LabEntry_Res Res = new Add_LabEntry_Res();
            CommonResponse data_1 = new CommonResponse();

            if (data.Status == "1" && data.Message != "Failed")
            {
                Res.Id = Convert.ToInt32(data.Message);

                string inputJson_1 = (new JavaScriptSerializer()).Serialize(Res);
                string response_1 = _api.CallAPIUrlEncodedWithWebReq(Constants.Get_URL, inputJson_1);
                CommonResponse _data = new CommonResponse();
                _data = (new JavaScriptSerializer()).Deserialize<CommonResponse>(response_1);

                if (_data.Status == "1")
                {
                    string path = _data.Message;
                    string[] pathArr = path.Split('\\');
                    string[] fileArr = pathArr.Last().Split('.');
                    string fileName = fileArr.Last().ToString();

                    Response.ContentType = fileArr.Last();
                    Response.AddHeader("Content-Disposition", "attachment;filename=\"" + pathArr.Last() + "\"");
                    Response.TransmitFile(_data.Message);
                    Response.End();
                    
                    data_1.Status = "1";
                    data_1.Message = "Success";
                }
                else
                {
                    data_1.Status = "0";
                    data_1.Message = _data.Error;
                }
            }
            else
            {
                data_1.Status = "0";
                data_1.Message = "Something Went wrong.\nPlease try again later";
            }
            return Json(data_1.Message, JsonRequestBehavior.AllowGet);
        }
    }
}
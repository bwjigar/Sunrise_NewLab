using Lib.Model;
using SunriseLabWeb_New.Data;
using SunriseLabWeb_New.Filter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
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
        public JsonResult FortunePartyCode_Exist(FortunePartyCode_Exist_Request fortunepartycode_exist_request)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(fortunepartycode_exist_request);
            string response = _api.CallAPI(Constants.FortunePartyCode_Exist, inputJson);
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
            string response = _api.CallAPI(Constants.Get_SearchStock, inputJson);
            ServiceResponse<Get_SearchStock_Res> data = (new JavaScriptSerializer()).Deserialize<ServiceResponse<Get_SearchStock_Res>>(response);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Excel_SearchStock(Get_SearchStock_Req req)
        {
            string inputJson = (new JavaScriptSerializer()).Serialize(req);
            string response = _api.CallAPI(Constants.Excel_SearchStock, inputJson);
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
    }
}
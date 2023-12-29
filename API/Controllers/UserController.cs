using EpExcelExportLib;

using API.Models;
using Lib.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml;
using SunriseLabWeb_New.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Xml;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.FileIO;
using System.Data.OleDb;
using Oracle.DataAccess.Client;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System.Net.Mail;
using System.Net.Mime;

namespace API.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        [AllowAnonymous]
        public IHttpActionResult ForgotPassword([FromBody] JObject data)
        {
            try
            {
                LoginRequest userRequest = new LoginRequest();
                try
                {
                    userRequest = JsonConvert.DeserializeObject<LoginRequest>(data.ToString());
                }
                catch (Exception ex)
                {
                    Lib.Model.Common.InsertErrorLog(ex, null, Request);
                    return Ok(new CommonResponse
                    {
                        Message = "Input Parameters are not in the proper format",
                        Status = "0"
                    });
                }

                CommonResponse resp = new CommonResponse();
                MailMessage xloMail = new MailMessage();
                SmtpClient xloSmtp = new SmtpClient();
                try
                {
                    Database db = new Database(Request);
                    List<IDbDataParameter> para;
                    para = new List<IDbDataParameter>();

                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, userRequest.UserName));

                    DataTable dt = db.ExecuteSP("Forgot_PassWord", para.ToArray(), false);

                    if (dt.Rows.Count == 0)
                    {
                        resp.Status = "0";
                        resp.Message = "Username is invalid or in-active.";
                        resp.Error = "";
                        return Ok(resp);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(dt.Rows[0]["Email"].ToString()))
                        {
                            Common.EmailForgotPassword(dt.Rows[0]["Email"].ToString(), dt.Rows[0]["Full_Name"].ToString(), userRequest.UserName, dt.Rows[0]["Password"].ToString());

                            string emailAdd = dt.Rows[0]["Email"].ToString();
                            emailAdd = emailAdd.Substring(0, 3) + "*".PadLeft(emailAdd.Length - 8).Replace(" ", "*") + emailAdd.Substring(emailAdd.Length - 5);
                            resp.Status = "1";
                            resp.Message = "Your account information have been sent to you on " + emailAdd;
                            resp.Error = "";
                            return Ok(resp);
                        }
                        else
                        {
                            resp.Status = "0";
                            resp.Message = "Your email address is invalid, please contact our Administrator.";
                            resp.Error = "";
                            return Ok(resp);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Lib.Model.Common.InsertErrorLog(ex, null, Request);
                    resp.Status = "0";
                    resp.Message = ex.ToString();
                    resp.Error = ex.Message;
                    return Ok(resp);
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "FAIL",
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult AddUpdate_Category_Value([FromBody] JObject data)
        {
            Get_Category_Value_Res res = new Get_Category_Value_Res();
            try
            {
                res = JsonConvert.DeserializeObject<Get_Category_Value_Res>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                CommonResponse response = new CommonResponse();
                Database database = new Database();
                List<IDbDataParameter> list = new List<IDbDataParameter>();
                if (res.Cat_V_Id > 0)
                {
                    list.Add(database.CreateParam("Cat_V_Id", DbType.Int32, ParameterDirection.Input, res.Cat_V_Id));
                }
                else
                {
                    list.Add(database.CreateParam("Cat_V_Id", DbType.Int32, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(res.Cat_Name))
                {
                    list.Add(database.CreateParam("Cat_Name", DbType.String, ParameterDirection.Input, res.Cat_Name));
                }
                else
                {
                    list.Add(database.CreateParam("Cat_Name", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(res.Group_Name))
                {
                    list.Add(database.CreateParam("Group_Name", DbType.String, ParameterDirection.Input, res.Group_Name));
                }
                else
                {
                    list.Add(database.CreateParam("Group_Name", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(res.Rapaport_Name))
                {
                    list.Add(database.CreateParam("Rapaport_Name", DbType.String, ParameterDirection.Input, res.Rapaport_Name));
                }
                else
                {
                    list.Add(database.CreateParam("Rapaport_Name", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(res.Rapnet_name))
                {
                    list.Add(database.CreateParam("Rapnet_name", DbType.String, ParameterDirection.Input, res.Rapnet_name));
                }
                else
                {
                    list.Add(database.CreateParam("Rapnet_name", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(res.Synonyms))
                {
                    list.Add(database.CreateParam("Synonyms", DbType.String, ParameterDirection.Input, res.Synonyms));
                }
                else
                {
                    list.Add(database.CreateParam("Synonyms", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (res.Order_No > 0)
                {
                    list.Add(database.CreateParam("Order_No", DbType.Decimal, ParameterDirection.Input, res.Order_No));
                }
                else
                {
                    list.Add(database.CreateParam("Order_No", DbType.Decimal, ParameterDirection.Input, DBNull.Value));
                }
                if (res.Sort_No > 0)
                {
                    list.Add(database.CreateParam("Sort_No", DbType.Int32, ParameterDirection.Input, res.Sort_No));
                }
                else
                {
                    list.Add(database.CreateParam("Sort_No", DbType.Int32, ParameterDirection.Input, DBNull.Value));
                }
                list.Add(database.CreateParam("Status", DbType.Boolean, ParameterDirection.Input, res.Status));
                if (!string.IsNullOrEmpty(res.Icon_Url))
                {
                    list.Add(database.CreateParam("Icon_Url", DbType.String, ParameterDirection.Input, res.Icon_Url));
                }
                else
                {
                    list.Add(database.CreateParam("Icon_Url", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (res.Cat_Id > 0)
                {
                    list.Add(database.CreateParam("Cat_Id", DbType.Int32, ParameterDirection.Input, res.Cat_Id));
                }
                else
                {
                    list.Add(database.CreateParam("Cat_Id", DbType.Int32, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(res.Display_Name))
                {
                    list.Add(database.CreateParam("Display_Name", DbType.String, ParameterDirection.Input, res.Display_Name));
                }
                else
                {
                    list.Add(database.CreateParam("Display_Name", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(res.Short_Name))
                {
                    list.Add(database.CreateParam("Short_Name", DbType.String, ParameterDirection.Input, res.Short_Name));
                }
                else
                {
                    list.Add(database.CreateParam("Short_Name", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                DataTable dt = database.ExecuteSP("AddUpdate_Category_Value", list.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    response.Status = dt.Rows[0]["Status"].ToString();
                    response.Message = dt.Rows[0]["Message"].ToString();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_Category_Value([FromBody] JObject data)
        {
            Get_Category_Value_Req Req = new Get_Category_Value_Req();
            try
            {
                Req = JsonConvert.DeserializeObject<Get_Category_Value_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_Category_Value_Res>
                {
                    Data = new List<Get_Category_Value_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });

            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(Req.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, Req.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (Req.PgNo > 0)
                    para.Add(db.CreateParam("PgNo", DbType.Int32, ParameterDirection.Input, Req.PgNo));
                else
                    para.Add(db.CreateParam("PgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (Req.PgSize > 0)
                    para.Add(db.CreateParam("PgSize", DbType.Int32, ParameterDirection.Input, Req.PgSize));
                else
                    para.Add(db.CreateParam("PgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Req.Cat_Name))
                    para.Add(db.CreateParam("Cat_Name", DbType.String, ParameterDirection.Input, Req.Cat_Name));
                else
                    para.Add(db.CreateParam("Cat_Name", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (Req.Cat_Id > 0)
                    para.Add(db.CreateParam("Cat_Id", DbType.Int32, ParameterDirection.Input, Req.Cat_Id));
                else
                    para.Add(db.CreateParam("Cat_Id", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (Req.Col_Id > 0)
                    para.Add(db.CreateParam("Col_Id", DbType.Int32, ParameterDirection.Input, Req.Col_Id));
                else
                    para.Add(db.CreateParam("Col_Id", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Get_Category_Value", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_Category_Value_Res> List_Res = new List<Get_Category_Value_Res>();
                    List_Res = DataTableExtension.ToList<Get_Category_Value_Res>(dt);

                    return Ok(new ServiceResponse<Get_Category_Value_Res>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_Category_Value_Res>
                    {
                        Data = new List<Get_Category_Value_Res>(),
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_Category_Value_Res>
                {
                    Data = new List<Get_Category_Value_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult AddUpdate_CategoryMas([FromBody] JObject data)
        {
            Get_CategoryMas_Res res = new Get_CategoryMas_Res();
            try
            {
                res = JsonConvert.DeserializeObject<Get_CategoryMas_Res>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                CommonResponse response = new CommonResponse();
                Database database = new Database();
                List<IDbDataParameter> list = new List<IDbDataParameter>();
                if (res.Cat_Id > 0)
                {
                    list.Add(database.CreateParam("Cat_Id", DbType.Int32, ParameterDirection.Input, res.Cat_Id));
                }
                else
                {
                    list.Add(database.CreateParam("Cat_Id", DbType.Int32, ParameterDirection.Input, DBNull.Value));
                }
                list.Add(database.CreateParam("Column_Name", DbType.String, ParameterDirection.Input, res.Column_Name));
                list.Add(database.CreateParam("Col_Id", DbType.Int32, ParameterDirection.Input, res.Col_Id));
                list.Add(database.CreateParam("Status", DbType.Boolean, ParameterDirection.Input, res.Status));
                DataTable table = database.ExecuteSP("AddUpdate_Category_Master", list.ToArray(), false);
                if ((table != null) && (table.Rows.Count > 0))
                {
                    response.Status = table.Rows[0]["Status"].ToString();
                    response.Message = table.Rows[0]["Message"].ToString();
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_CategoryMas([FromBody] JObject data)
        {
            Get_CategoryMas_Req Req = new Get_CategoryMas_Req();
            try
            {
                Req = JsonConvert.DeserializeObject<Get_CategoryMas_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_CategoryMas_Res>
                {
                    Data = new List<Get_CategoryMas_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });

            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(Req.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, Req.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (Req.PgNo > 0)
                    para.Add(db.CreateParam("PgNo", DbType.Int32, ParameterDirection.Input, Req.PgNo));
                else
                    para.Add(db.CreateParam("PgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (Req.PgSize > 0)
                    para.Add(db.CreateParam("PgSize", DbType.Int32, ParameterDirection.Input, Req.PgSize));
                else
                    para.Add(db.CreateParam("PgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Req.Not_Col_Id))
                    para.Add(db.CreateParam("Not_Col_Id", DbType.String, ParameterDirection.Input, Req.Not_Col_Id));
                else
                    para.Add(db.CreateParam("Not_Col_Id", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Get_Category_Master", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_CategoryMas_Res> List_Res = new List<Get_CategoryMas_Res>();
                    List_Res = DataTableExtension.ToList<Get_CategoryMas_Res>(dt);

                    return Ok(new ServiceResponse<Get_CategoryMas_Res>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_CategoryMas_Res>
                    {
                        Data = new List<Get_CategoryMas_Res>(),
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_CategoryMas_Res>
                {
                    Data = new List<Get_CategoryMas_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult AddUpdate_Customer_Disc([FromBody] JObject data)
        {
            Save_Supplier_Disc_Req req = new Save_Supplier_Disc_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Save_Supplier_Disc_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                CommonResponse resp = new CommonResponse();
                Database db = new Database();
                DataTable dtData = new DataTable();
                DataTable dt = new DataTable();

                dt.Columns.Add("Supplier", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("Shape", typeof(string));
                dt.Columns.Add("Carat", typeof(string));
                dt.Columns.Add("ColorType", typeof(string));
                dt.Columns.Add("Color", typeof(string));
                dt.Columns.Add("INTENSITY", typeof(string));
                dt.Columns.Add("OVERTONE", typeof(string));
                dt.Columns.Add("FANCY_COLOR", typeof(string));
                dt.Columns.Add("Clarity", typeof(string));
                dt.Columns.Add("Cut", typeof(string));
                dt.Columns.Add("Polish", typeof(string));
                dt.Columns.Add("Sym", typeof(string));
                dt.Columns.Add("Fls", typeof(string));
                dt.Columns.Add("Lab", typeof(string));
                dt.Columns.Add("FromLength", typeof(string));
                dt.Columns.Add("ToLength", typeof(string));
                dt.Columns.Add("FromWidth", typeof(string));
                dt.Columns.Add("ToWidth", typeof(string));
                dt.Columns.Add("FromDepth", typeof(string));
                dt.Columns.Add("ToDepth", typeof(string));
                dt.Columns.Add("FromDepthinPer", typeof(string));
                dt.Columns.Add("ToDepthinPer", typeof(string));
                dt.Columns.Add("FromTableinPer", typeof(string));
                dt.Columns.Add("ToTableinPer", typeof(string));
                dt.Columns.Add("FromCrAng", typeof(string));
                dt.Columns.Add("ToCrAng", typeof(string));
                dt.Columns.Add("FromCrHt", typeof(string));
                dt.Columns.Add("ToCrHt", typeof(string));
                dt.Columns.Add("FromPavAng", typeof(string));
                dt.Columns.Add("ToPavAng", typeof(string));
                dt.Columns.Add("FromPavHt", typeof(string));
                dt.Columns.Add("ToPavHt", typeof(string));
                dt.Columns.Add("CheckKTS", typeof(string));
                dt.Columns.Add("UNCheckKTS", typeof(string));
                dt.Columns.Add("BGM", typeof(string));
                dt.Columns.Add("CrownBlack", typeof(string));
                dt.Columns.Add("TableBlack", typeof(string));
                dt.Columns.Add("CrownWhite", typeof(string));
                dt.Columns.Add("TableWhite", typeof(string));
                dt.Columns.Add("GoodsType", typeof(string));
                dt.Columns.Add("Image", typeof(string));
                dt.Columns.Add("Video", typeof(string));
                dt.Columns.Add("PricingMethod_1", typeof(string));
                dt.Columns.Add("PricingSign_1", typeof(string));
                dt.Columns.Add("Disc_1_1", typeof(string));
                dt.Columns.Add("Value_1_1", typeof(string));
                dt.Columns.Add("Value_1_2", typeof(string));
                dt.Columns.Add("Value_1_3", typeof(string));
                dt.Columns.Add("Value_1_4", typeof(string));
                dt.Columns.Add("Value_1_5", typeof(string));
                dt.Columns.Add("Speci_Additional_1", typeof(string));
                dt.Columns.Add("FromDate", typeof(string));
                dt.Columns.Add("ToDate", typeof(string));
                dt.Columns.Add("PricingMethod_2", typeof(string));
                dt.Columns.Add("PricingSign_2", typeof(string));
                dt.Columns.Add("Disc_2_1", typeof(string));
                dt.Columns.Add("Value_2_1", typeof(string));
                dt.Columns.Add("Value_2_2", typeof(string));
                dt.Columns.Add("Value_2_3", typeof(string));
                dt.Columns.Add("Value_2_4", typeof(string));
                dt.Columns.Add("Value_2_5", typeof(string));


                if (req.SuppDisc.Count() > 0)
                {
                    for (int i = 0; i < req.SuppDisc.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["Supplier"] = req.SuppDisc[i].Supplier;
                        dr["Location"] = req.SuppDisc[i].Location;
                        dr["Shape"] = req.SuppDisc[i].Shape;
                        dr["Carat"] = req.SuppDisc[i].Carat;
                        dr["ColorType"] = req.SuppDisc[i].ColorType;
                        dr["Color"] = req.SuppDisc[i].Color;
                        dr["INTENSITY"] = req.SuppDisc[i].INTENSITY;
                        dr["OVERTONE"] = req.SuppDisc[i].OVERTONE;
                        dr["FANCY_COLOR"] = req.SuppDisc[i].FANCY_COLOR;
                        dr["Clarity"] = req.SuppDisc[i].Clarity;
                        dr["Cut"] = req.SuppDisc[i].Cut;
                        dr["Polish"] = req.SuppDisc[i].Polish;
                        dr["Sym"] = req.SuppDisc[i].Sym;
                        dr["Fls"] = req.SuppDisc[i].Fls;
                        dr["Lab"] = req.SuppDisc[i].Lab;
                        dr["FromLength"] = req.SuppDisc[i].FromLength;
                        dr["ToLength"] = req.SuppDisc[i].ToLength;
                        dr["FromWidth"] = req.SuppDisc[i].FromWidth;
                        dr["ToWidth"] = req.SuppDisc[i].ToWidth;
                        dr["FromDepth"] = req.SuppDisc[i].FromDepth;
                        dr["ToDepth"] = req.SuppDisc[i].ToDepth;
                        dr["FromDepthinPer"] = req.SuppDisc[i].FromDepthinPer;
                        dr["ToDepthinPer"] = req.SuppDisc[i].ToDepthinPer;
                        dr["FromTableinPer"] = req.SuppDisc[i].FromTableinPer;
                        dr["ToTableinPer"] = req.SuppDisc[i].ToTableinPer;
                        dr["FromCrAng"] = req.SuppDisc[i].FromCrAng;
                        dr["ToCrAng"] = req.SuppDisc[i].ToCrAng;
                        dr["FromCrHt"] = req.SuppDisc[i].FromCrHt;
                        dr["ToCrHt"] = req.SuppDisc[i].ToCrHt;
                        dr["FromPavAng"] = req.SuppDisc[i].FromPavAng;
                        dr["ToPavAng"] = req.SuppDisc[i].ToPavAng;
                        dr["FromPavHt"] = req.SuppDisc[i].FromPavHt;
                        dr["ToPavHt"] = req.SuppDisc[i].ToPavHt;
                        dr["CheckKTS"] = req.SuppDisc[i].CheckKTS;
                        dr["UNCheckKTS"] = req.SuppDisc[i].UNCheckKTS;
                        dr["BGM"] = req.SuppDisc[i].BGM;
                        dr["CrownBlack"] = req.SuppDisc[i].CrownBlack;
                        dr["TableBlack"] = req.SuppDisc[i].TableBlack;
                        dr["CrownWhite"] = req.SuppDisc[i].CrownWhite;
                        dr["TableWhite"] = req.SuppDisc[i].TableWhite;
                        dr["GoodsType"] = req.SuppDisc[i].GoodsType;
                        dr["Image"] = req.SuppDisc[i].Image;
                        dr["Video"] = req.SuppDisc[i].Video;
                        dr["PricingMethod_1"] = req.SuppDisc[i].PricingMethod_1;
                        dr["PricingSign_1"] = req.SuppDisc[i].PricingSign_1;
                        dr["Disc_1_1"] = req.SuppDisc[i].Disc_1_1;
                        dr["Value_1_1"] = req.SuppDisc[i].Value_1_1;
                        dr["Value_1_2"] = req.SuppDisc[i].Value_1_2;
                        dr["Value_1_3"] = req.SuppDisc[i].Value_1_3;
                        dr["Value_1_4"] = req.SuppDisc[i].Value_1_4;
                        dr["Value_1_5"] = req.SuppDisc[i].Value_1_5;
                        dr["Speci_Additional_1"] = req.SuppDisc[i].Speci_Additional_1;
                        dr["FromDate"] = req.SuppDisc[i].FromDate;
                        dr["ToDate"] = req.SuppDisc[i].ToDate;
                        dr["PricingMethod_2"] = req.SuppDisc[i].PricingMethod_2;
                        dr["PricingSign_2"] = req.SuppDisc[i].PricingSign_2;
                        dr["Disc_2_1"] = req.SuppDisc[i].Disc_2_1;
                        dr["Value_2_1"] = req.SuppDisc[i].Value_2_1;
                        dr["Value_2_2"] = req.SuppDisc[i].Value_2_2;
                        dr["Value_2_3"] = req.SuppDisc[i].Value_2_3;
                        dr["Value_2_4"] = req.SuppDisc[i].Value_2_4;
                        dr["Value_2_5"] = req.SuppDisc[i].Value_2_5;

                        dt.Rows.Add(dr);
                    }

                    List<SqlParameter> para = new List<SqlParameter>();
                    SqlParameter param = new SqlParameter("table", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    dtData = db.ExecuteSP("AddUpdate_Customer_Disc", para.ToArray(), false);
                }
                else
                {
                    List<IDbDataParameter> para = new List<IDbDataParameter>();

                    dtData = db.ExecuteSP("Delete_Customer_Disc", para.ToArray(), false);
                }

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    resp.Status = dtData.Rows[0]["Status"].ToString();
                    resp.Message = dtData.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_Customer_Disc([FromBody] JObject data)
        {
            Save_Supplier_Disc_Req req = new Save_Supplier_Disc_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Save_Supplier_Disc_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Obj_Supplier_Disc>
                {
                    Data = new List<Obj_Supplier_Disc>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });

            }
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("Get_Customer_Disc", para.ToArray(), false);

                List<Obj_Supplier_Disc> List_Res = new List<Obj_Supplier_Disc>();

                if (dt != null && dt.Rows.Count > 0)
                {
                    List_Res = DataTableExtension.ToList<Obj_Supplier_Disc>(dt);
                }
                return Ok(new ServiceResponse<Obj_Supplier_Disc>
                {
                    Data = List_Res,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Obj_Supplier_Disc>
                {
                    Data = new List<Obj_Supplier_Disc>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult AddUpdate_Customer_Stock_Disc([FromBody] JObject data)
        {
            Save_Supplier_Disc_Req req = new Save_Supplier_Disc_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Save_Supplier_Disc_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                CommonResponse resp = new CommonResponse();
                Database db = new Database();

                DataTable dtData = new DataTable();
                DataTable dt = new DataTable();
                dt.Columns.Add("UserId", typeof(string));
                dt.Columns.Add("UserName", typeof(string));
                dt.Columns.Add("ExportType", typeof(string));
                //dt.Columns.Add("Password", typeof(string));
                dt.Columns.Add("URL", typeof(string));
                dt.Columns.Add("Supplier", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("Shape", typeof(string));
                dt.Columns.Add("Carat", typeof(string));
                dt.Columns.Add("ColorType", typeof(string));
                dt.Columns.Add("Color", typeof(string));
                dt.Columns.Add("INTENSITY", typeof(string));
                dt.Columns.Add("OVERTONE", typeof(string));
                dt.Columns.Add("FANCY_COLOR", typeof(string));
                dt.Columns.Add("Clarity", typeof(string));
                dt.Columns.Add("Cut", typeof(string));
                dt.Columns.Add("Polish", typeof(string));
                dt.Columns.Add("Sym", typeof(string));
                dt.Columns.Add("Fls", typeof(string));
                dt.Columns.Add("Lab", typeof(string));

                dt.Columns.Add("FromLength", typeof(string));
                dt.Columns.Add("ToLength", typeof(string));
                dt.Columns.Add("Length_IsBlank", typeof(string));

                dt.Columns.Add("FromWidth", typeof(string));
                dt.Columns.Add("ToWidth", typeof(string));
                dt.Columns.Add("Width_IsBlank", typeof(string));

                dt.Columns.Add("FromDepth", typeof(string));
                dt.Columns.Add("ToDepth", typeof(string));
                dt.Columns.Add("Depth_IsBlank", typeof(string));

                dt.Columns.Add("FromDepthinPer", typeof(string));
                dt.Columns.Add("ToDepthinPer", typeof(string));
                dt.Columns.Add("DepthPer_IsBlank", typeof(string));

                dt.Columns.Add("FromTableinPer", typeof(string));
                dt.Columns.Add("ToTableinPer", typeof(string));
                dt.Columns.Add("TablePer_IsBlank", typeof(string));

                dt.Columns.Add("FromCrAng", typeof(string));
                dt.Columns.Add("ToCrAng", typeof(string));
                dt.Columns.Add("CrAng_IsBlank", typeof(string));

                dt.Columns.Add("FromCrHt", typeof(string));
                dt.Columns.Add("ToCrHt", typeof(string));
                dt.Columns.Add("CrHt_IsBlank", typeof(string));

                dt.Columns.Add("FromPavAng", typeof(string));
                dt.Columns.Add("ToPavAng", typeof(string));
                dt.Columns.Add("PavAng_IsBlank", typeof(string));

                dt.Columns.Add("FromPavHt", typeof(string));
                dt.Columns.Add("ToPavHt", typeof(string));
                dt.Columns.Add("PavHt_IsBlank", typeof(string));

                dt.Columns.Add("CheckKTS", typeof(string));
                dt.Columns.Add("UNCheckKTS", typeof(string));
                dt.Columns.Add("BGM", typeof(string));
                dt.Columns.Add("CrownBlack", typeof(string));
                dt.Columns.Add("TableBlack", typeof(string));
                dt.Columns.Add("CrownWhite", typeof(string));
                dt.Columns.Add("TableWhite", typeof(string));
                dt.Columns.Add("TableOpen", typeof(string));
                dt.Columns.Add("GirdleOpen", typeof(string));
                dt.Columns.Add("CrownOpen", typeof(string));
                dt.Columns.Add("PavillionOpen", typeof(string));
                dt.Columns.Add("GoodsType", typeof(string));
                dt.Columns.Add("Image", typeof(string));
                dt.Columns.Add("Video", typeof(string));
                dt.Columns.Add("PricingMethod_1", typeof(string));
                dt.Columns.Add("PricingSign_1", typeof(string));
                dt.Columns.Add("Disc_1_1", typeof(string));
                dt.Columns.Add("Value_1_1", typeof(string));
                dt.Columns.Add("Value_1_2", typeof(string));
                dt.Columns.Add("Value_1_3", typeof(string));
                dt.Columns.Add("Value_1_4", typeof(string));
                dt.Columns.Add("Value_1_5", typeof(string));
                dt.Columns.Add("Speci_Additional_1", typeof(string));
                dt.Columns.Add("FromDate", typeof(string));
                dt.Columns.Add("ToDate", typeof(string));
                dt.Columns.Add("PricingMethod_2", typeof(string));
                dt.Columns.Add("PricingSign_2", typeof(string));
                dt.Columns.Add("Disc_2_1", typeof(string));
                dt.Columns.Add("Value_2_1", typeof(string));
                dt.Columns.Add("Value_2_2", typeof(string));
                dt.Columns.Add("Value_2_3", typeof(string));
                dt.Columns.Add("Value_2_4", typeof(string));
                dt.Columns.Add("Value_2_5", typeof(string));
                dt.Columns.Add("View", typeof(string));
                dt.Columns.Add("Download", typeof(string));

                if (req.SuppDisc.Count() > 0)
                {
                    for (int i = 0; i < req.SuppDisc.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["UserId"] = req.UserId;
                        dr["UserName"] = req.UserName;
                        dr["ExportType"] = req.ExportType;
                        //dr["Password"] = req.Password;
                        dr["URL"] = req.URL;
                        dr["Supplier"] = req.SuppDisc[i].Supplier;
                        dr["Location"] = req.SuppDisc[i].Location;
                        dr["Shape"] = req.SuppDisc[i].Shape;
                        dr["Carat"] = req.SuppDisc[i].Carat;
                        dr["ColorType"] = req.SuppDisc[i].ColorType;
                        dr["Color"] = req.SuppDisc[i].Color;
                        dr["INTENSITY"] = req.SuppDisc[i].INTENSITY;
                        dr["OVERTONE"] = req.SuppDisc[i].OVERTONE;
                        dr["FANCY_COLOR"] = req.SuppDisc[i].FANCY_COLOR;
                        dr["Clarity"] = req.SuppDisc[i].Clarity;
                        dr["Cut"] = req.SuppDisc[i].Cut;
                        dr["Polish"] = req.SuppDisc[i].Polish;
                        dr["Sym"] = req.SuppDisc[i].Sym;
                        dr["Fls"] = req.SuppDisc[i].Fls;
                        dr["Lab"] = req.SuppDisc[i].Lab;

                        dr["FromLength"] = req.SuppDisc[i].FromLength;
                        dr["ToLength"] = req.SuppDisc[i].ToLength;
                        dr["Length_IsBlank"] = req.SuppDisc[i].Length_IsBlank;

                        dr["FromWidth"] = req.SuppDisc[i].FromWidth;
                        dr["ToWidth"] = req.SuppDisc[i].ToWidth;
                        dr["Width_IsBlank"] = req.SuppDisc[i].Width_IsBlank;

                        dr["FromDepth"] = req.SuppDisc[i].FromDepth;
                        dr["ToDepth"] = req.SuppDisc[i].ToDepth;
                        dr["Depth_IsBlank"] = req.SuppDisc[i].Depth_IsBlank;

                        dr["FromDepthinPer"] = req.SuppDisc[i].FromDepthinPer;
                        dr["ToDepthinPer"] = req.SuppDisc[i].ToDepthinPer;
                        dr["DepthPer_IsBlank"] = req.SuppDisc[i].DepthPer_IsBlank;

                        dr["FromTableinPer"] = req.SuppDisc[i].FromTableinPer;
                        dr["ToTableinPer"] = req.SuppDisc[i].ToTableinPer;
                        dr["TablePer_IsBlank"] = req.SuppDisc[i].TablePer_IsBlank;

                        dr["FromCrAng"] = req.SuppDisc[i].FromCrAng;
                        dr["ToCrAng"] = req.SuppDisc[i].ToCrAng;
                        dr["CrAng_IsBlank"] = req.SuppDisc[i].CrAng_IsBlank;

                        dr["FromCrHt"] = req.SuppDisc[i].FromCrHt;
                        dr["ToCrHt"] = req.SuppDisc[i].ToCrHt;
                        dr["CrHt_IsBlank"] = req.SuppDisc[i].CrHt_IsBlank;

                        dr["FromPavAng"] = req.SuppDisc[i].FromPavAng;
                        dr["ToPavAng"] = req.SuppDisc[i].ToPavAng;
                        dr["PavAng_IsBlank"] = req.SuppDisc[i].PavAng_IsBlank;

                        dr["FromPavHt"] = req.SuppDisc[i].FromPavHt;
                        dr["ToPavHt"] = req.SuppDisc[i].ToPavHt;
                        dr["PavHt_IsBlank"] = req.SuppDisc[i].PavHt_IsBlank;

                        dr["CheckKTS"] = req.SuppDisc[i].CheckKTS;
                        dr["UNCheckKTS"] = req.SuppDisc[i].UNCheckKTS;
                        dr["BGM"] = req.SuppDisc[i].BGM;
                        dr["CrownBlack"] = req.SuppDisc[i].CrownBlack;
                        dr["TableBlack"] = req.SuppDisc[i].TableBlack;
                        dr["CrownWhite"] = req.SuppDisc[i].CrownWhite;
                        dr["TableWhite"] = req.SuppDisc[i].TableWhite;
                        dr["TableOpen"] = req.SuppDisc[i].TableOpen;
                        dr["GirdleOpen"] = req.SuppDisc[i].GirdleOpen;
                        dr["CrownOpen"] = req.SuppDisc[i].CrownOpen;
                        dr["PavillionOpen"] = req.SuppDisc[i].PavillionOpen;
                        dr["GoodsType"] = req.SuppDisc[i].GoodsType;
                        dr["Image"] = req.SuppDisc[i].Image;
                        dr["Video"] = req.SuppDisc[i].Video;
                        dr["PricingMethod_1"] = req.SuppDisc[i].PricingMethod_1;
                        dr["PricingSign_1"] = req.SuppDisc[i].PricingSign_1;
                        dr["Disc_1_1"] = req.SuppDisc[i].Disc_1_1;
                        dr["Value_1_1"] = req.SuppDisc[i].Value_1_1;
                        dr["Value_1_2"] = req.SuppDisc[i].Value_1_2;
                        dr["Value_1_3"] = req.SuppDisc[i].Value_1_3;
                        dr["Value_1_4"] = req.SuppDisc[i].Value_1_4;
                        dr["Value_1_5"] = req.SuppDisc[i].Value_1_5;
                        dr["Speci_Additional_1"] = req.SuppDisc[i].Speci_Additional_1;
                        dr["FromDate"] = req.SuppDisc[i].FromDate;
                        dr["ToDate"] = req.SuppDisc[i].ToDate;
                        dr["PricingMethod_2"] = req.SuppDisc[i].PricingMethod_2;
                        dr["PricingSign_2"] = req.SuppDisc[i].PricingSign_2;
                        dr["Disc_2_1"] = req.SuppDisc[i].Disc_2_1;
                        dr["Value_2_1"] = req.SuppDisc[i].Value_2_1;
                        dr["Value_2_2"] = req.SuppDisc[i].Value_2_2;
                        dr["Value_2_3"] = req.SuppDisc[i].Value_2_3;
                        dr["Value_2_4"] = req.SuppDisc[i].Value_2_4;
                        dr["Value_2_5"] = req.SuppDisc[i].Value_2_5;
                        dr["View"] = req.SuppDisc[i].View;
                        dr["Download"] = req.SuppDisc[i].Download;

                        dt.Rows.Add(dr);
                    }

                    List<SqlParameter> para = new List<SqlParameter>();
                    SqlParameter param = new SqlParameter("table", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    dtData = db.ExecuteSP("AddUpdate_Customer_Stock_Disc", para.ToArray(), false);
                }
                else
                {
                    List<IDbDataParameter> para = new List<IDbDataParameter>();
                    para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.UserId));

                    dtData = db.ExecuteSP("Delete_Customer_Stock_Disc", para.ToArray(), false);
                }

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    resp.Status = dtData.Rows[0]["Status"].ToString();
                    resp.Message = dtData.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_Customer_Stock_Disc([FromBody] JObject data)
        {
            Save_Supplier_Disc_Req req = new Save_Supplier_Disc_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Save_Supplier_Disc_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Obj_Supplier_Disc>
                {
                    Data = new List<Obj_Supplier_Disc>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });

            }
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (req.UserId > 0)
                    para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.UserId));
                else
                    para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Get_Customer_Stock_Disc", para.ToArray(), false);

                List<Obj_Supplier_Disc> List_Res = new List<Obj_Supplier_Disc>();

                if (dt != null && dt.Rows.Count > 0)
                {
                    List_Res = DataTableExtension.ToList<Obj_Supplier_Disc>(dt);
                }

                return Ok(new ServiceResponse<Obj_Supplier_Disc>
                {
                    Data = List_Res,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Obj_Supplier_Disc>
                {
                    Data = new List<Obj_Supplier_Disc>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_Customer_Stock_Disc_Mas([FromBody] JObject data)
        {
            Save_Supplier_Disc_Req req = new Save_Supplier_Disc_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Save_Supplier_Disc_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_Customer_Stock_Disc_Mas_Res>
                {
                    Data = new List<Get_Customer_Stock_Disc_Mas_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });

            }
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (req.UserId > 0)
                    para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.UserId));
                else
                    para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Get_Customer_Stock_Disc_Mas", para.ToArray(), false);

                List<Get_Customer_Stock_Disc_Mas_Res> List_Res = new List<Get_Customer_Stock_Disc_Mas_Res>();

                if (dt != null && dt.Rows.Count > 0)
                {
                    List_Res = DataTableExtension.ToList<Get_Customer_Stock_Disc_Mas_Res>(dt);
                }

                return Ok(new ServiceResponse<Get_Customer_Stock_Disc_Mas_Res>
                {
                    Data = List_Res,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_Customer_Stock_Disc_Mas_Res>
                {
                    Data = new List<Get_Customer_Stock_Disc_Mas_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult AddUpdate_Supplier_Disc([FromBody] JObject data)
        {
            Save_Supplier_Disc_Req req = new Save_Supplier_Disc_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Save_Supplier_Disc_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();
                Database db = new Database();

                DataTable dtData = new DataTable();
                DataTable dt = new DataTable();

                dt.Columns.Add("SupplierId", typeof(string));
                dt.Columns.Add("Location", typeof(string));
                dt.Columns.Add("Shape", typeof(string));
                dt.Columns.Add("Carat", typeof(string));
                dt.Columns.Add("ColorType", typeof(string));
                dt.Columns.Add("Color", typeof(string));
                dt.Columns.Add("INTENSITY", typeof(string));
                dt.Columns.Add("OVERTONE", typeof(string));
                dt.Columns.Add("FANCY_COLOR", typeof(string));
                dt.Columns.Add("Clarity", typeof(string));
                dt.Columns.Add("Cut", typeof(string));
                dt.Columns.Add("Polish", typeof(string));
                dt.Columns.Add("Sym", typeof(string));
                dt.Columns.Add("Fls", typeof(string));
                dt.Columns.Add("Lab", typeof(string));
                dt.Columns.Add("FromLength", typeof(string));
                dt.Columns.Add("ToLength", typeof(string));
                dt.Columns.Add("FromWidth", typeof(string));
                dt.Columns.Add("ToWidth", typeof(string));
                dt.Columns.Add("FromDepth", typeof(string));
                dt.Columns.Add("ToDepth", typeof(string));
                dt.Columns.Add("FromDepthinPer", typeof(string));
                dt.Columns.Add("ToDepthinPer", typeof(string));
                dt.Columns.Add("FromTableinPer", typeof(string));
                dt.Columns.Add("ToTableinPer", typeof(string));
                dt.Columns.Add("FromCrAng", typeof(string));
                dt.Columns.Add("ToCrAng", typeof(string));
                dt.Columns.Add("FromCrHt", typeof(string));
                dt.Columns.Add("ToCrHt", typeof(string));
                dt.Columns.Add("FromPavAng", typeof(string));
                dt.Columns.Add("ToPavAng", typeof(string));
                dt.Columns.Add("FromPavHt", typeof(string));
                dt.Columns.Add("ToPavHt", typeof(string));

                dt.Columns.Add("FromBaseDisc", typeof(string));
                dt.Columns.Add("ToBaseDisc", typeof(string));
                dt.Columns.Add("FromBaseAmt", typeof(string));
                dt.Columns.Add("ToBaseAmt", typeof(string));
                dt.Columns.Add("FromFinalDisc", typeof(string));
                dt.Columns.Add("ToFinalDisc", typeof(string));
                dt.Columns.Add("FromFinalAmt", typeof(string));
                dt.Columns.Add("ToFinalAmt", typeof(string));

                dt.Columns.Add("Culet", typeof(string));
                dt.Columns.Add("CheckKTS", typeof(string));
                dt.Columns.Add("UNCheckKTS", typeof(string));
                dt.Columns.Add("BGM", typeof(string));
                dt.Columns.Add("CrownBlack", typeof(string));
                dt.Columns.Add("TableBlack", typeof(string));
                dt.Columns.Add("CrownWhite", typeof(string));
                dt.Columns.Add("TableWhite", typeof(string));
                dt.Columns.Add("GoodsType", typeof(string));
                dt.Columns.Add("Image", typeof(string));
                dt.Columns.Add("Video", typeof(string));
                dt.Columns.Add("PricingMethod_1", typeof(string));
                dt.Columns.Add("PricingSign_1", typeof(string));
                dt.Columns.Add("Disc_1_1", typeof(string));
                dt.Columns.Add("Value_1_1", typeof(string));
                dt.Columns.Add("Value_1_2", typeof(string));
                dt.Columns.Add("Value_1_3", typeof(string));
                dt.Columns.Add("Value_1_4", typeof(string));
                dt.Columns.Add("Value_1_5", typeof(string));
                dt.Columns.Add("Speci_Additional_1", typeof(string));
                dt.Columns.Add("FromDate", typeof(string));
                dt.Columns.Add("ToDate", typeof(string));
                dt.Columns.Add("PricingMethod_2", typeof(string));
                dt.Columns.Add("PricingSign_2", typeof(string));
                dt.Columns.Add("Disc_2_1", typeof(string));
                dt.Columns.Add("Value_2_1", typeof(string));
                dt.Columns.Add("Value_2_2", typeof(string));
                dt.Columns.Add("Value_2_3", typeof(string));
                dt.Columns.Add("Value_2_4", typeof(string));
                dt.Columns.Add("Value_2_5", typeof(string));
                dt.Columns.Add("PricingMethod_3", typeof(string));
                dt.Columns.Add("PricingSign_3", typeof(string));
                dt.Columns.Add("Disc_3_1", typeof(string));
                dt.Columns.Add("Value_3_1", typeof(string));
                dt.Columns.Add("Value_3_2", typeof(string));
                dt.Columns.Add("Value_3_3", typeof(string));
                dt.Columns.Add("Value_3_4", typeof(string));
                dt.Columns.Add("Value_3_5", typeof(string));
                dt.Columns.Add("Speci_Additional_2", typeof(string));
                dt.Columns.Add("FromDate1", typeof(string));
                dt.Columns.Add("ToDate1", typeof(string));
                dt.Columns.Add("PricingMethod_4", typeof(string));
                dt.Columns.Add("PricingSign_4", typeof(string));
                dt.Columns.Add("Disc_4_1", typeof(string));
                dt.Columns.Add("Value_4_1", typeof(string));
                dt.Columns.Add("Value_4_2", typeof(string));
                dt.Columns.Add("Value_4_3", typeof(string));
                dt.Columns.Add("Value_4_4", typeof(string));
                dt.Columns.Add("Value_4_5", typeof(string));

                if (req.SuppDisc.Count() > 0)
                {
                    for (int i = 0; i < req.SuppDisc.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["SupplierId"] = req.SupplierId;
                        dr["Location"] = req.SuppDisc[i].Location;
                        dr["Shape"] = req.SuppDisc[i].Shape;
                        dr["Carat"] = req.SuppDisc[i].Carat;
                        dr["ColorType"] = req.SuppDisc[i].ColorType;
                        dr["Color"] = req.SuppDisc[i].Color;
                        dr["INTENSITY"] = req.SuppDisc[i].INTENSITY;
                        dr["OVERTONE"] = req.SuppDisc[i].OVERTONE;
                        dr["FANCY_COLOR"] = req.SuppDisc[i].FANCY_COLOR;
                        dr["Clarity"] = req.SuppDisc[i].Clarity;
                        dr["Cut"] = req.SuppDisc[i].Cut;
                        dr["Polish"] = req.SuppDisc[i].Polish;
                        dr["Sym"] = req.SuppDisc[i].Sym;
                        dr["Fls"] = req.SuppDisc[i].Fls;
                        dr["Lab"] = req.SuppDisc[i].Lab;
                        dr["FromLength"] = req.SuppDisc[i].FromLength;
                        dr["ToLength"] = req.SuppDisc[i].ToLength;
                        dr["FromWidth"] = req.SuppDisc[i].FromWidth;
                        dr["ToWidth"] = req.SuppDisc[i].ToWidth;
                        dr["FromDepth"] = req.SuppDisc[i].FromDepth;
                        dr["ToDepth"] = req.SuppDisc[i].ToDepth;
                        dr["FromDepthinPer"] = req.SuppDisc[i].FromDepthinPer;
                        dr["ToDepthinPer"] = req.SuppDisc[i].ToDepthinPer;
                        dr["FromTableinPer"] = req.SuppDisc[i].FromTableinPer;
                        dr["ToTableinPer"] = req.SuppDisc[i].ToTableinPer;
                        dr["FromCrAng"] = req.SuppDisc[i].FromCrAng;
                        dr["ToCrAng"] = req.SuppDisc[i].ToCrAng;
                        dr["FromCrHt"] = req.SuppDisc[i].FromCrHt;
                        dr["ToCrHt"] = req.SuppDisc[i].ToCrHt;
                        dr["FromPavAng"] = req.SuppDisc[i].FromPavAng;
                        dr["ToPavAng"] = req.SuppDisc[i].ToPavAng;
                        dr["FromPavHt"] = req.SuppDisc[i].FromPavHt;
                        dr["ToPavHt"] = req.SuppDisc[i].ToPavHt;
                        dr["FromBaseDisc"] = req.SuppDisc[i].FromBaseDisc;
                        dr["ToBaseDisc"] = req.SuppDisc[i].ToBaseDisc;
                        dr["FromBaseAmt"] = req.SuppDisc[i].FromBaseAmt;
                        dr["ToBaseAmt"] = req.SuppDisc[i].ToBaseAmt;
                        dr["FromFinalDisc"] = req.SuppDisc[i].FromFinalDisc;
                        dr["ToFinalDisc"] = req.SuppDisc[i].ToFinalDisc;
                        dr["FromFinalAmt"] = req.SuppDisc[i].FromFinalAmt;
                        dr["ToFinalAmt"] = req.SuppDisc[i].ToFinalAmt;
                        dr["Culet"] = req.SuppDisc[i].Culet;
                        dr["CheckKTS"] = req.SuppDisc[i].CheckKTS;
                        dr["UNCheckKTS"] = req.SuppDisc[i].UNCheckKTS;
                        dr["BGM"] = req.SuppDisc[i].BGM;
                        dr["CrownBlack"] = req.SuppDisc[i].CrownBlack;
                        dr["TableBlack"] = req.SuppDisc[i].TableBlack;
                        dr["CrownWhite"] = req.SuppDisc[i].CrownWhite;
                        dr["TableWhite"] = req.SuppDisc[i].TableWhite;
                        dr["GoodsType"] = req.SuppDisc[i].GoodsType;
                        dr["Image"] = req.SuppDisc[i].Image;
                        dr["Video"] = req.SuppDisc[i].Video;
                        dr["PricingMethod_1"] = req.SuppDisc[i].PricingMethod_1;
                        dr["PricingSign_1"] = req.SuppDisc[i].PricingSign_1;
                        dr["Disc_1_1"] = req.SuppDisc[i].Disc_1_1;
                        dr["Value_1_1"] = req.SuppDisc[i].Value_1_1;
                        dr["Value_1_2"] = req.SuppDisc[i].Value_1_2;
                        dr["Value_1_3"] = req.SuppDisc[i].Value_1_3;
                        dr["Value_1_4"] = req.SuppDisc[i].Value_1_4;
                        dr["Value_1_5"] = req.SuppDisc[i].Value_1_5;
                        dr["Speci_Additional_1"] = req.SuppDisc[i].Speci_Additional_1;
                        dr["FromDate"] = req.SuppDisc[i].FromDate;
                        dr["ToDate"] = req.SuppDisc[i].ToDate;
                        dr["PricingMethod_2"] = req.SuppDisc[i].PricingMethod_2;
                        dr["PricingSign_2"] = req.SuppDisc[i].PricingSign_2;
                        dr["Disc_2_1"] = req.SuppDisc[i].Disc_2_1;
                        dr["Value_2_1"] = req.SuppDisc[i].Value_2_1;
                        dr["Value_2_2"] = req.SuppDisc[i].Value_2_2;
                        dr["Value_2_3"] = req.SuppDisc[i].Value_2_3;
                        dr["Value_2_4"] = req.SuppDisc[i].Value_2_4;
                        dr["Value_2_5"] = req.SuppDisc[i].Value_2_5;
                        dr["PricingMethod_3"] = req.SuppDisc[i].PricingMethod_3;
                        dr["PricingSign_3"] = req.SuppDisc[i].PricingSign_3;
                        dr["Disc_3_1"] = req.SuppDisc[i].Disc_3_1;
                        dr["Value_3_1"] = req.SuppDisc[i].Value_3_1;
                        dr["Value_3_2"] = req.SuppDisc[i].Value_3_2;
                        dr["Value_3_3"] = req.SuppDisc[i].Value_3_3;
                        dr["Value_3_4"] = req.SuppDisc[i].Value_3_4;
                        dr["Value_3_5"] = req.SuppDisc[i].Value_3_5;
                        dr["Speci_Additional_2"] = req.SuppDisc[i].Speci_Additional_2;
                        dr["FromDate1"] = req.SuppDisc[i].FromDate1;
                        dr["ToDate1"] = req.SuppDisc[i].ToDate1;
                        dr["PricingMethod_4"] = req.SuppDisc[i].PricingMethod_4;
                        dr["PricingSign_4"] = req.SuppDisc[i].PricingSign_4;
                        dr["Disc_4_1"] = req.SuppDisc[i].Disc_4_1;
                        dr["Value_4_1"] = req.SuppDisc[i].Value_4_1;
                        dr["Value_4_2"] = req.SuppDisc[i].Value_4_2;
                        dr["Value_4_3"] = req.SuppDisc[i].Value_4_3;
                        dr["Value_4_4"] = req.SuppDisc[i].Value_4_4;
                        dr["Value_4_5"] = req.SuppDisc[i].Value_4_5;

                        dt.Rows.Add(dr);
                    }

                    List<SqlParameter> para = new List<SqlParameter>();
                    SqlParameter param = new SqlParameter("table", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    dtData = db.ExecuteSP("AddUpdate_Supplier_Disc", para.ToArray(), false);
                }
                else
                {
                    List<IDbDataParameter> para = new List<IDbDataParameter>();
                    para.Add(db.CreateParam("SupplierId", DbType.Int32, ParameterDirection.Input, req.SupplierId));

                    dtData = db.ExecuteSP("Delete_Supplier_Disc", para.ToArray(), false);
                }

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    resp.Status = dtData.Rows[0]["Status"].ToString();
                    resp.Message = dtData.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_Supplier_Disc([FromBody] JObject data)
        {
            Save_Supplier_Disc_Req Req = new Save_Supplier_Disc_Req();
            try
            {
                Req = JsonConvert.DeserializeObject<Save_Supplier_Disc_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Obj_Supplier_Disc>
                {
                    Data = new List<Obj_Supplier_Disc>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });

            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (Req.SupplierId > 0)
                    para.Add(db.CreateParam("SupplierId", DbType.Int32, ParameterDirection.Input, Req.SupplierId));
                else
                    para.Add(db.CreateParam("SupplierId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Get_Supplier_Disc", para.ToArray(), false);

                List<Obj_Supplier_Disc> List_Res = new List<Obj_Supplier_Disc>();

                if (dt != null && dt.Rows.Count > 0)
                {
                    List_Res = DataTableExtension.ToList<Obj_Supplier_Disc>(dt);
                }
                return Ok(new ServiceResponse<Obj_Supplier_Disc>
                {
                    Data = List_Res,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Obj_Supplier_Disc>
                {
                    Data = new List<Obj_Supplier_Disc>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult AddUpdate_Supplier_RefNo_Prefix([FromBody] JObject data)
        {
            Save_Supplier_RefNo_Prefix_Req req = new Save_Supplier_RefNo_Prefix_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Save_Supplier_RefNo_Prefix_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();

                DataTable dt = new DataTable();
                dt.Columns.Add("SupplierId", typeof(string));
                dt.Columns.Add("Shape", typeof(string));
                dt.Columns.Add("Pointer", typeof(string));
                dt.Columns.Add("Prefix", typeof(string));

                if (req.refno.Count() > 0)
                {
                    for (int i = 0; i < req.refno.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["SupplierId"] = req.refno[i].SupplierId.ToString();
                        dr["Shape"] = req.refno[i].Shape.ToString();
                        dr["Pointer"] = req.refno[i].Pointer.ToString();
                        dr["Prefix"] = req.refno[i].Prefix;

                        dt.Rows.Add(dr);
                    }
                }

                Database db = new Database();
                DataTable dtData = new DataTable();
                List<SqlParameter> para = new List<SqlParameter>();

                SqlParameter param = new SqlParameter("table", SqlDbType.Structured);
                param.Value = dt;
                para.Add(param);

                dtData = db.ExecuteSP("AddUpdate_Supplier_RefNo_Prefix", para.ToArray(), false);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    resp.Status = dtData.Rows[0]["Status"].ToString();
                    resp.Message = dtData.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_Supplier_RefNo_Prefix([FromBody] JObject data)
        {
            Get_Supplier_RefNo_Prefix_Req Req = new Get_Supplier_RefNo_Prefix_Req();
            try
            {
                Req = JsonConvert.DeserializeObject<Get_Supplier_RefNo_Prefix_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_Supplier_RefNo_Prefix_Res>
                {
                    Data = new List<Get_Supplier_RefNo_Prefix_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });

            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (Req.SupplierId > 0)
                    para.Add(db.CreateParam("SupplierId", DbType.Int32, ParameterDirection.Input, Req.SupplierId));
                else
                    para.Add(db.CreateParam("SupplierId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Get_Supplier_RefNo_Prefix", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_Supplier_RefNo_Prefix_Res> List_Res = new List<Get_Supplier_RefNo_Prefix_Res>();
                    List_Res = DataTableExtension.ToList<Get_Supplier_RefNo_Prefix_Res>(dt);

                    return Ok(new ServiceResponse<Get_Supplier_RefNo_Prefix_Res>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_Supplier_RefNo_Prefix_Res>
                    {
                        Data = new List<Get_Supplier_RefNo_Prefix_Res>(),
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_Supplier_RefNo_Prefix_Res>
                {
                    Data = new List<Get_Supplier_RefNo_Prefix_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Delete_Supplier_RefNo_Prefix([FromBody] JObject data)
        {
            Obj_Supplier_RefNo_Prefix_List req = new Obj_Supplier_RefNo_Prefix_List();
            try
            {
                req = JsonConvert.DeserializeObject<Obj_Supplier_RefNo_Prefix_List>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                CommonResponse resp = new CommonResponse();

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("SupplierId", DbType.Int32, ParameterDirection.Input, req.SupplierId));

                DataTable dt = db.ExecuteSP("Delete_Supplier_RefNo_Prefix", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    resp.Status = dt.Rows[0]["Status"].ToString();
                    resp.Message = dt.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult AddUpdate_Supplier_Value([FromBody] JObject data)
        {
            AddUpdate_Supplier_Value_Req req = new AddUpdate_Supplier_Value_Req();
            try
            {
                req = JsonConvert.DeserializeObject<AddUpdate_Supplier_Value_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();

                DataTable dt = new DataTable();
                dt.Columns.Add("Sup_Id", typeof(int));
                dt.Columns.Add("Cat_V_Id", typeof(int));
                dt.Columns.Add("Supp_Cat_name", typeof(string));
                dt.Columns.Add("Status", typeof(bool));

                if (req.sup_val.Count() > 0)
                {
                    for (int i = 0; i < req.sup_val.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["Sup_Id"] = req.sup_val[i].Sup_Id;
                        dr["Cat_V_Id"] = req.sup_val[i].Cat_V_Id;
                        dr["Supp_Cat_name"] = req.sup_val[i].Supp_Cat_name;
                        dr["Status"] = req.sup_val[i].Status;

                        dt.Rows.Add(dr);
                    }
                }

                Database db = new Database();
                DataTable dtData = new DataTable();
                List<SqlParameter> para = new List<SqlParameter>();

                SqlParameter param = new SqlParameter("table", SqlDbType.Structured);
                param.Value = dt;
                para.Add(param);

                dtData = db.ExecuteSP("AddUpdate_Supplier_Value", para.ToArray(), false);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    resp.Status = dtData.Rows[0]["Status"].ToString();
                    resp.Message = dtData.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_Supplier_Value([FromBody] JObject data)
        {
            Get_Supplier_Value_Req Req = new Get_Supplier_Value_Req();
            try
            {
                Req = JsonConvert.DeserializeObject<Get_Supplier_Value_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_Supplier_Value_Res>
                {
                    Data = new List<Get_Supplier_Value_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });

            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("Sup_Id", DbType.Int32, ParameterDirection.Input, Req.Sup_Id));
                para.Add(db.CreateParam("Col_Id", DbType.Int32, ParameterDirection.Input, Req.Col_Id));

                DataTable dt = db.ExecuteSP("Get_Supplier_Value", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_Supplier_Value_Res> List_Res = new List<Get_Supplier_Value_Res>();
                    List_Res = DataTableExtension.ToList<Get_Supplier_Value_Res>(dt);

                    return Ok(new ServiceResponse<Get_Supplier_Value_Res>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_Supplier_Value_Res>
                    {
                        Data = new List<Get_Supplier_Value_Res>(),
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_Supplier_Value_Res>
                {
                    Data = new List<Get_Supplier_Value_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Delete_Supplier_Value([FromBody] JObject data)
        {
            Get_Supplier_Value_Req req = new Get_Supplier_Value_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Get_Supplier_Value_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("Sup_Id", DbType.Int32, ParameterDirection.Input, req.Sup_Id));
                para.Add(db.CreateParam("Col_Id", DbType.Int32, ParameterDirection.Input, req.Col_Id));

                DataTable dt = db.ExecuteSP("Delete_Supplier_Value", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    resp.Status = dt.Rows[0]["Status"].ToString();
                    resp.Message = dt.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        public static int TotCount = 0;
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Get_SupplierColumnSetting_FromAPI([FromBody] JObject data)
        {
            JObject test1 = JObject.Parse(data.ToString());
            string Message = string.Empty, Exception = string.Empty;

            Obj_CategoryDet_List Req = new Obj_CategoryDet_List();
            try
            {
                Req = JsonConvert.DeserializeObject<Obj_CategoryDet_List>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());
                //Req = JsonConvert.DeserializeObject<Obj_CategoryDet_List>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            int Supplier_Mas_Id = 0;
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (Req.SupplierId > 0)
                    para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, Req.SupplierId));
                else
                    para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                DataTable dtAPI = db.ExecuteSP("Get_SupplierMaster", para.ToArray(), false);

                if (dtAPI != null && dtAPI.Rows.Count > 0)
                {
                    TotCount = dtAPI.Rows.Count;
                    try
                    {
                        Supplier_Mas_Id = Convert.ToInt32(dtAPI.Rows[0]["Id"].ToString());

                        string _API = String.Empty, UserName = String.Empty, Password = String.Empty, filename = String.Empty, filefullpath = String.Empty;

                        DataTable dt_APIRes = new DataTable();

                        //dt_APIRes = Supplier_Stock_Get_From_His_WEB_API_AND_FTP(dtAPI.Rows[0]["APIType"].ToString(),
                        //            dtAPI.Rows[0]["SupplierResponseFormat"].ToString(),
                        //            dtAPI.Rows[0]["SupplierURL"].ToString(),
                        //            dtAPI.Rows[0]["SupplierAPIMethod"].ToString(),
                        //            dtAPI.Rows[0]["UserName"].ToString(),
                        //            dtAPI.Rows[0]["Password"].ToString(),
                        //            dtAPI.Rows[0]["FileLocation"].ToString());

                        var (msg, exe, dt) = Supplier_Stock_Get_From_His_WEB_API_AND_FTP(Convert.ToInt32(dtAPI.Rows[0]["Id"]),
                                    Convert.ToString(dtAPI.Rows[0]["APIType"]),
                                    Convert.ToString(dtAPI.Rows[0]["SupplierResponseFormat"]),
                                    Convert.ToString(dtAPI.Rows[0]["SupplierURL"]),
                                    Convert.ToString(dtAPI.Rows[0]["SupplierAPIMethod"]),
                                    Convert.ToString(dtAPI.Rows[0]["UserName"]),
                                    Convert.ToString(dtAPI.Rows[0]["Password"]),
                                    Convert.ToString(dtAPI.Rows[0]["FileLocation"]));
                        Message = msg;
                        Exception = exe;
                        dt_APIRes = dt;

                        if (Message == "SUCCESS" && dt_APIRes != null && dt_APIRes.Rows.Count > 0)
                        {
                            DataTable dtResult = new DataTable();
                            int currecs = 1;

                            dtResult.Columns.Add("Id", typeof(int));
                            dtResult.Columns.Add("SupplierColumn", typeof(string));

                            foreach (DataColumn column in dt_APIRes.Columns)
                            {
                                DataRow dr = dtResult.NewRow();
                                dr["Id"] = currecs;
                                dr["SupplierColumn"] = column.ColumnName;
                                currecs += 1;

                                dtResult.Rows.Add(dr);
                            }

                            List<Get_SupplierColumnSetting_FromAPI_Res> list = new List<Get_SupplierColumnSetting_FromAPI_Res>();
                            list = DataTableExtension.ToList<Get_SupplierColumnSetting_FromAPI_Res>(dtResult);

                            return Ok(new ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res>
                            {
                                Data = list,
                                Message = "SUCCESS",
                                Status = "1"
                            });
                        }
                        else
                        {
                            return Ok(new ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res>
                            {
                                Data = null,
                                //Message = "Supplier API in Columns not found.",
                                Message = (Message != "ERROR" ? Message : "Supplier API in Columns not found"),
                                Status = "2"
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        return Ok(new ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res>
                        {
                            Data = null,
                            Message = ex.Message,
                            Status = "0"
                        });
                    }
                }
                else
                {
                    return Ok(new ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res>
                    {
                        Data = null,
                        Message = "Supplier Not Found.",
                        Status = "2"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res>
                {
                    Data = null,
                    Message = ex.Message,
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_SupplierColumnSetting([FromBody] JObject data)
        {
            Obj_CategoryDet_List Req = new Obj_CategoryDet_List();
            try
            {
                Req = JsonConvert.DeserializeObject<Obj_CategoryDet_List>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierColumnSetting_Res>
                {
                    Data = new List<Get_SupplierColumnSetting_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (Req.SupplierId > 0)
                    para.Add(db.CreateParam("SupplierId", DbType.Int64, ParameterDirection.Input, Req.SupplierId));
                else
                    para.Add(db.CreateParam("SupplierId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Get_SupplierColumnSetting", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_SupplierColumnSetting_Res> List_Res = new List<Get_SupplierColumnSetting_Res>();
                    List_Res = DataTableExtension.ToList<Get_SupplierColumnSetting_Res>(dt);

                    return Ok(new ServiceResponse<Get_SupplierColumnSetting_Res>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_SupplierColumnSetting_Res>
                    {
                        Data = new List<Get_SupplierColumnSetting_Res>(),
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierColumnSetting_Res>
                {
                    Data = new List<Get_SupplierColumnSetting_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult AddUpdate_SupplierColumnSetting([FromBody] JObject data)
        {
            AddUpdate_SupplierColumnSetting_Req Req = new AddUpdate_SupplierColumnSetting_Req();
            try
            {
                Req = JsonConvert.DeserializeObject<AddUpdate_SupplierColumnSetting_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();

                DataTable dt = new DataTable();
                dt.Columns.Add("SupplierId", typeof(string));
                dt.Columns.Add("SupplierColumn", typeof(string));
                dt.Columns.Add("ColumnId", typeof(string));

                if (Req.col.Count() > 0)
                {
                    for (int i = 0; i < Req.col.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(Req.col[i].SupplierColumn))
                        {
                            DataRow dr = dt.NewRow();

                            dr["SupplierId"] = Convert.ToString(Req.col[i].SupplierId);
                            dr["SupplierColumn"] = Req.col[i].SupplierColumn;
                            dr["ColumnId"] = Convert.ToString(Req.col[i].ColumnId);

                            dt.Rows.Add(dr);
                        }
                    }
                }

                Database db = new Database();
                DataTable dtData = new DataTable();
                List<SqlParameter> para = new List<SqlParameter>();

                SqlParameter param = new SqlParameter("tableCol", SqlDbType.Structured);
                param.Value = dt;
                para.Add(param);

                dtData = db.ExecuteSP("AddUpdate_SupplierColumnSetting", para.ToArray(), false);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    resp.Status = dtData.Rows[0]["Status"].ToString();
                    resp.Message = dtData.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Delete_SupplierColumnSetting([FromBody] JObject data)
        {
            Obj_CategoryDet_List req = new Obj_CategoryDet_List();
            try
            {
                req = JsonConvert.DeserializeObject<Obj_CategoryDet_List>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                CommonResponse resp = new CommonResponse();

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("SupplierId", DbType.Int32, ParameterDirection.Input, req.SupplierId));

                DataTable dt = db.ExecuteSP("Delete_SupplierColumnSetting", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    resp.Status = dt.Rows[0]["Status"].ToString();
                    resp.Message = dt.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Get_SheetName_From_File([FromBody] JObject data)
        {
            JObject test1 = JObject.Parse(data.ToString());
            Data_Get_From_File_Req Req = new Data_Get_From_File_Req();
            try
            {
                Req = JsonConvert.DeserializeObject<Data_Get_From_File_Req>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SheetName_From_File_Res>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                List<Get_SheetName_From_File_Res> List_Res = new List<Get_SheetName_From_File_Res>();

                string str = Path.GetExtension(Req.FilePath).ToLower();
                if (str == ".xls")
                {
                    string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Req.FilePath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
                    List_Res = Get_SheetName_From_FILE(".xls", connString);
                }
                else if (str == ".xlsx")
                {
                    string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Req.FilePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";
                    List_Res = Get_SheetName_From_FILE(".xlsx", connString);
                }

                if (List_Res != null && List_Res.Count > 0)
                {
                    return Ok(new ServiceResponse<Get_SheetName_From_File_Res>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_SheetName_From_File_Res>
                    {
                        Data = List_Res,
                        Message = "Sheet Not Found",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SheetName_From_File_Res>
                {
                    Data = new List<Get_SheetName_From_File_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Get_Data_From_File([FromBody] JObject data)
        {
            JObject test1 = JObject.Parse(data.ToString());
            Data_Get_From_File_Req Req = new Data_Get_From_File_Req();
            try
            {
                Req = JsonConvert.DeserializeObject<Data_Get_From_File_Req>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                DataTable Stock_dt = new DataTable();
                string str = Path.GetExtension(Req.FilePath).ToLower();
                if (str == ".xls")
                {
                    string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Req.FilePath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
                    //Stock_dt = ConvertXLStoDataTable("", connString);
                    Stock_dt = Convert_FILE_To_DataTable(".xls", connString, Req.SheetName, Req.SupplierId);
                }
                else if (str == ".xlsx")
                {
                    //string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Req.FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Req.FilePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";
                    //Stock_dt = ConvertXSLXtoDataTable("", connString);
                    Stock_dt = Convert_FILE_To_DataTable(".xlsx", connString, Req.SheetName, Req.SupplierId);
                }
                else if (str == ".csv")
                {
                    //Stock_dt = ConvertCSVtoDataTable(Req.FilePath);
                    Stock_dt = Convert_FILE_To_DataTable(".csv", Req.FilePath, "", Req.SupplierId);
                }

                if (Stock_dt != null && Stock_dt.Rows.Count > 0)
                {
                    Int32 VishindasHolaram_Lakhi_Id = Convert.ToInt32(ConfigurationManager.AppSettings["VishindasHolaram_Lakhi_Id"]);
                    Int32 StarRays_Id = Convert.ToInt32(ConfigurationManager.AppSettings["StarRays_Id"]);
                    Int32 JewelParadise_Id = Convert.ToInt32(ConfigurationManager.AppSettings["JewelParadise_Id"]);
                    Int32 Diamart_Id = Convert.ToInt32(ConfigurationManager.AppSettings["Diamart_Id"]);
                    Int32 Laxmi_Id = Convert.ToInt32(ConfigurationManager.AppSettings["Laxmi_Id"]);
                    Int32 JB_Id = Convert.ToInt32(ConfigurationManager.AppSettings["JB_Id"]);
                    Int32 Aspeco_Id = Convert.ToInt32(ConfigurationManager.AppSettings["Aspeco_Id"]);
                    Int32 KBS_Id = Convert.ToInt32(ConfigurationManager.AppSettings["KBS_Id"]);

                    if (Req.SupplierId == VishindasHolaram_Lakhi_Id)
                    {
                        Stock_dt = Lakhi_TableCrown_BlackWhite(Stock_dt);
                    }
                    else if (Req.SupplierId == StarRays_Id)
                    {
                        Stock_dt = StarRays_TableCrownPav_Open(Stock_dt);
                    }
                    else if (Req.SupplierId == JewelParadise_Id)
                    {
                        Stock_dt = JewelParadise_Shade(Stock_dt);
                    }
                    else if (Req.SupplierId == Diamart_Id)
                    {
                        Stock_dt = Diamart_Shade(Stock_dt);
                    }
                    else if (Req.SupplierId == Laxmi_Id)
                    {
                        Stock_dt = Laxmi_Grading(Stock_dt);
                    }
                    else if (Req.SupplierId == JB_Id)
                    {
                        Stock_dt = JB(Stock_dt);
                    }
                    else if (Req.SupplierId == Aspeco_Id)
                    {
                        Stock_dt = Aspeco(Stock_dt);
                    }
                    else if (Req.SupplierId == KBS_Id)
                    {
                        Stock_dt = KBS(Stock_dt);
                    }

                    List<Get_SupplierColumnSetting_FromAPI_Res> List_Res = new List<Get_SupplierColumnSetting_FromAPI_Res>();


                    DataTable table2 = new DataTable();
                    int num = 1;
                    table2.Columns.Add("Id", typeof(int));
                    table2.Columns.Add("SupplierColumn", typeof(string));

                    foreach (DataColumn column in Stock_dt.Columns)
                    {
                        DataRow row = table2.NewRow();
                        row["Id"] = num;
                        row["SupplierColumn"] = column.ColumnName;
                        num++;
                        table2.Rows.Add(row);
                    }

                    List_Res = DataTableExtension.ToList<Get_SupplierColumnSetting_FromAPI_Res>(table2);

                    return Ok(new ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res>
                    {
                        Data = null,
                        //Message = "Supplier " + ((str == ".xls" || str == ".xlsx") ? "Excel" : "CSV") + " File "+ ((str == ".xls" || str == ".xlsx") ? Req.SheetName.Remove(Req.SheetName.Length - 1, 1) + " Sheet " : "")+ "in Columns not found.",
                        //Message = "Columns not found From Supplier's " + ((str == ".xls" || str == ".xlsx") ? "Excel" : "CSV") + " File" + ((str == ".xls" || str == ".xlsx") ? " in " + Req.SheetName.Remove(Req.SheetName.Length - 1, 1) + " Sheet." : "."),
                        Message = "Data not Found From Supplier's " + ((str == ".xls" || str == ".xlsx") ? "Excel" : "CSV") + " File" + ((str == ".xls" || str == ".xlsx") ? " in " + Req.SheetName.Remove(Req.SheetName.Length - 1, 1) + " Sheet." : "."),
                        Status = "2"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res>
                {
                    Data = new List<Get_SupplierColumnSetting_FromAPI_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_SupplierColumnSetting_FromFile([FromBody] JObject data)
        {
            Obj_CategoryDet_List Req = new Obj_CategoryDet_List();
            try
            {
                Req = JsonConvert.DeserializeObject<Obj_CategoryDet_List>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierColumnSetting_Res>
                {
                    Data = new List<Get_SupplierColumnSetting_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (Req.SupplierId > 0)
                    para.Add(db.CreateParam("SupplierId", DbType.Int64, ParameterDirection.Input, Req.SupplierId));
                else
                    para.Add(db.CreateParam("SupplierId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Get_SupplierColumnSetting_FromFile", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_SupplierColumnSetting_Res> List_Res = new List<Get_SupplierColumnSetting_Res>();
                    List_Res = DataTableExtension.ToList<Get_SupplierColumnSetting_Res>(dt);

                    return Ok(new ServiceResponse<Get_SupplierColumnSetting_Res>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_SupplierColumnSetting_Res>
                    {
                        Data = new List<Get_SupplierColumnSetting_Res>(),
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierColumnSetting_Res>
                {
                    Data = new List<Get_SupplierColumnSetting_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult AddUpdate_SupplierColumnSetting_FromFile([FromBody] JObject data)
        {
            AddUpdate_SupplierColumnSetting_Req Req = new AddUpdate_SupplierColumnSetting_Req();
            try
            {
                Req = JsonConvert.DeserializeObject<AddUpdate_SupplierColumnSetting_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                CommonResponse resp = new CommonResponse();
                DataTable dt = new DataTable();
                dt.Columns.Add("SupplierId", typeof(string));
                dt.Columns.Add("SupplierColumn", typeof(string));
                dt.Columns.Add("ColumnId", typeof(string));

                if (Req.col.Count() > 0)
                {
                    for (int i = 0; i < Req.col.Count(); i++)
                    {
                        if (!string.IsNullOrEmpty(Req.col[i].SupplierColumn))
                        {
                            DataRow dr = dt.NewRow();

                            dr["SupplierId"] = Convert.ToString(Req.col[i].SupplierId);
                            dr["SupplierColumn"] = Req.col[i].SupplierColumn;
                            dr["ColumnId"] = Convert.ToString(Req.col[i].ColumnId);

                            dt.Rows.Add(dr);
                        }
                    }
                }
                Database db = new Database();
                DataTable dtData = new DataTable();
                List<SqlParameter> para = new List<SqlParameter>();

                SqlParameter param = new SqlParameter("tableCol", SqlDbType.Structured);
                param.Value = dt;
                para.Add(param);

                dtData = db.ExecuteSP("AddUpdate_SupplierColumnSetting_FromFile", para.ToArray(), false);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    resp.Status = dtData.Rows[0]["Status"].ToString();
                    resp.Message = dtData.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Delete_SupplierColumnSetting_FromFile([FromBody] JObject data)
        {
            Obj_CategoryDet_List req = new Obj_CategoryDet_List();
            try
            {
                req = JsonConvert.DeserializeObject<Obj_CategoryDet_List>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                CommonResponse resp = new CommonResponse();

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("SupplierId", DbType.Int32, ParameterDirection.Input, req.SupplierId));

                DataTable dt = db.ExecuteSP("Delete_SupplierColumnSetting_FromFile", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    resp.Status = dt.Rows[0]["Status"].ToString();
                    resp.Message = dt.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult Get_Supplier_ForSearchStock([FromBody] JObject data)
        {
            Get_SupplierMaster_Req Req = new Get_SupplierMaster_Req();
            try
            {
                Req = JsonConvert.DeserializeObject<Get_SupplierMaster_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierMaster_Res>
                {
                    Data = new List<Get_SupplierMaster_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("Get_Supplier_ForSearchStock", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_SupplierMaster_Res> List_Res = new List<Get_SupplierMaster_Res>();
                    List_Res = DataTableExtension.ToList<Get_SupplierMaster_Res>(dt);

                    return Ok(new ServiceResponse<Get_SupplierMaster_Res>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_SupplierMaster_Res>
                    {
                        Data = new List<Get_SupplierMaster_Res>(),
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierMaster_Res>
                {
                    Data = new List<Get_SupplierMaster_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_SupplierMaster([FromBody] JObject data)
        {
            Get_SupplierMaster_Req Req = new Get_SupplierMaster_Req();
            try
            {
                Req = JsonConvert.DeserializeObject<Get_SupplierMaster_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierMaster_Res>
                {
                    Data = new List<Get_SupplierMaster_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (Req.Id > 0)
                    para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, Req.Id));
                else
                    para.Add(db.CreateParam("Id", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Req.SupplierName))
                    para.Add(db.CreateParam("SupplierName", DbType.String, ParameterDirection.Input, Req.SupplierName));
                else
                    para.Add(db.CreateParam("SupplierName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (Req.iPgNo > 0)
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, Req.iPgNo));
                else
                    para.Add(db.CreateParam("iPgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (Req.iPgSize > 0)
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, Req.iPgSize));
                else
                    para.Add(db.CreateParam("iPgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Req.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, Req.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("WebAPIFTPStockUpload", DbType.Boolean, ParameterDirection.Input, Req.WebAPIFTPStockUpload));
                para.Add(db.CreateParam("FileStockUpload", DbType.Boolean, ParameterDirection.Input, Req.FileStockUpload));

                if (!string.IsNullOrEmpty(Req.IsActive))
                    para.Add(db.CreateParam("IsActive", DbType.Boolean, ParameterDirection.Input, Convert.ToBoolean(Convert.ToInt32(Req.IsActive))));
                else
                    para.Add(db.CreateParam("IsActive", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Get_SupplierMaster", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_SupplierMaster_Res> List_Res = new List<Get_SupplierMaster_Res>();
                    List_Res = DataTableExtension.ToList<Get_SupplierMaster_Res>(dt);

                    return Ok(new ServiceResponse<Get_SupplierMaster_Res>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_SupplierMaster_Res>
                    {
                        Data = new List<Get_SupplierMaster_Res>(),
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierMaster_Res>
                {
                    Data = new List<Get_SupplierMaster_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult AddUpdate_SupplierMaster([FromBody] JObject data)
        {
            Get_SupplierMaster_Res res = new Get_SupplierMaster_Res();
            try
            {
                res = JsonConvert.DeserializeObject<Get_SupplierMaster_Res>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                CommonResponse resp = new CommonResponse();

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (res.Id > 0)
                {
                    para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, res.Id));
                }
                else
                {
                    para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, DBNull.Value));
                }
                para.Add(db.CreateParam("APIType", DbType.String, ParameterDirection.Input, res.APIType));
                if (!string.IsNullOrEmpty(res.SupplierURL))
                {
                    para.Add(db.CreateParam("SupplierURL", DbType.String, ParameterDirection.Input, res.SupplierURL));
                }
                else
                {
                    para.Add(db.CreateParam("SupplierURL", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(res.SupplierName))
                {
                    para.Add(db.CreateParam("SupplierName", DbType.String, ParameterDirection.Input, res.SupplierName));
                }
                else
                {
                    para.Add(db.CreateParam("SupplierName", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(res.SupplierResponseFormat))
                {
                    para.Add(db.CreateParam("SupplierResponseFormat", DbType.String, ParameterDirection.Input, res.SupplierResponseFormat));
                }
                else
                {
                    para.Add(db.CreateParam("SupplierResponseFormat", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(res.FileName))
                {
                    para.Add(db.CreateParam("FileName", DbType.String, ParameterDirection.Input, res.FileName));
                }
                else
                {
                    para.Add(db.CreateParam("FileName", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(res.FileLocation))
                {
                    para.Add(db.CreateParam("FileLocation", DbType.String, ParameterDirection.Input, res.FileLocation));
                }
                else
                {
                    para.Add(db.CreateParam("FileLocation", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(res.LocationExportType))
                {
                    para.Add(db.CreateParam("LocationExportType", DbType.String, ParameterDirection.Input, res.LocationExportType));
                }
                else
                {
                    para.Add(db.CreateParam("LocationExportType", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                para.Add(db.CreateParam("RepeateveryType", DbType.String, ParameterDirection.Input, res.RepeateveryType));
                para.Add(db.CreateParam("Repeatevery", DbType.String, ParameterDirection.Input, res.Repeatevery));
                para.Add(db.CreateParam("Active", DbType.Boolean, ParameterDirection.Input, res.Active));
                para.Add(db.CreateParam("DiscInverse", DbType.Boolean, ParameterDirection.Input, res.DiscInverse));

                if (!string.IsNullOrEmpty(res.NewRefNoGenerate))
                {
                    para.Add(db.CreateParam("NewRefNoGenerate", DbType.String, ParameterDirection.Input, res.NewRefNoGenerate));
                }
                else
                {
                    para.Add(db.CreateParam("NewRefNoGenerate", DbType.String, ParameterDirection.Input, DBNull.Value));
                }

                if (!string.IsNullOrEmpty(res.NewRefNoCommonPrefix))
                {
                    para.Add(db.CreateParam("NewRefNoCommonPrefix", DbType.String, ParameterDirection.Input, res.NewRefNoCommonPrefix));
                }
                else
                {
                    para.Add(db.CreateParam("NewRefNoCommonPrefix", DbType.String, ParameterDirection.Input, DBNull.Value));
                }

                para.Add(db.CreateParam("DataGetFrom", DbType.String, ParameterDirection.Input, res.DataGetFrom));
                if (!string.IsNullOrEmpty(res.SupplierAPIMethod))
                {
                    para.Add(db.CreateParam("SupplierAPIMethod", DbType.String, ParameterDirection.Input, res.SupplierAPIMethod));
                }
                else
                {
                    para.Add(db.CreateParam("SupplierAPIMethod", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(res.UserName))
                {
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, res.UserName));
                }
                else
                {
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(res.Password))
                {
                    para.Add(db.CreateParam("Password", DbType.String, ParameterDirection.Input, res.Password));
                }
                else
                {
                    para.Add(db.CreateParam("Password", DbType.String, ParameterDirection.Input, DBNull.Value));
                }

                para.Add(db.CreateParam("Image", DbType.Boolean, ParameterDirection.Input, res.Image));
                para.Add(db.CreateParam("Video", DbType.Boolean, ParameterDirection.Input, res.Video));
                para.Add(db.CreateParam("Certi", DbType.Boolean, ParameterDirection.Input, res.Certi));

                if (!string.IsNullOrEmpty(res.DocViewType_Image1))
                    para.Add(db.CreateParam("DocViewType_Image1", DbType.String, ParameterDirection.Input, res.DocViewType_Image1));
                else
                    para.Add(db.CreateParam("DocViewType_Image1", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.DocViewType_Image2))
                    para.Add(db.CreateParam("DocViewType_Image2", DbType.String, ParameterDirection.Input, res.DocViewType_Image2));
                else
                    para.Add(db.CreateParam("DocViewType_Image2", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.DocViewType_Image3))
                    para.Add(db.CreateParam("DocViewType_Image3", DbType.String, ParameterDirection.Input, res.DocViewType_Image3));
                else
                    para.Add(db.CreateParam("DocViewType_Image3", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.DocViewType_Image4))
                    para.Add(db.CreateParam("DocViewType_Image4", DbType.String, ParameterDirection.Input, res.DocViewType_Image4));
                else
                    para.Add(db.CreateParam("DocViewType_Image4", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.DocViewType_Video))
                    para.Add(db.CreateParam("DocViewType_Video", DbType.String, ParameterDirection.Input, res.DocViewType_Video));
                else
                    para.Add(db.CreateParam("DocViewType_Video", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.DocViewType_Certi))
                    para.Add(db.CreateParam("DocViewType_Certi", DbType.String, ParameterDirection.Input, res.DocViewType_Certi));
                else
                    para.Add(db.CreateParam("DocViewType_Certi", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.ImageURL_1))
                    para.Add(db.CreateParam("ImageURL_1", DbType.String, ParameterDirection.Input, res.ImageURL_1));
                else
                    para.Add(db.CreateParam("ImageURL_1", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.ImageFormat_1))
                    para.Add(db.CreateParam("ImageFormat_1", DbType.String, ParameterDirection.Input, res.ImageFormat_1));
                else
                    para.Add(db.CreateParam("ImageFormat_1", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.ImageURL_2))
                    para.Add(db.CreateParam("ImageURL_2", DbType.String, ParameterDirection.Input, res.ImageURL_2));
                else
                    para.Add(db.CreateParam("ImageURL_2", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.ImageFormat_2))
                    para.Add(db.CreateParam("ImageFormat_2", DbType.String, ParameterDirection.Input, res.ImageFormat_2));
                else
                    para.Add(db.CreateParam("ImageFormat_2", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.ImageURL_3))
                    para.Add(db.CreateParam("ImageURL_3", DbType.String, ParameterDirection.Input, res.ImageURL_3));
                else
                    para.Add(db.CreateParam("ImageURL_3", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.ImageFormat_3))
                    para.Add(db.CreateParam("ImageFormat_3", DbType.String, ParameterDirection.Input, res.ImageFormat_3));
                else
                    para.Add(db.CreateParam("ImageFormat_3", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.ImageURL_4))
                    para.Add(db.CreateParam("ImageURL_4", DbType.String, ParameterDirection.Input, res.ImageURL_4));
                else
                    para.Add(db.CreateParam("ImageURL_4", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.ImageFormat_4))
                    para.Add(db.CreateParam("ImageFormat_4", DbType.String, ParameterDirection.Input, res.ImageFormat_4));
                else
                    para.Add(db.CreateParam("ImageFormat_4", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.VideoURL))
                    para.Add(db.CreateParam("VideoURL", DbType.String, ParameterDirection.Input, res.VideoURL));
                else
                    para.Add(db.CreateParam("VideoURL", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.VideoFormat))
                    para.Add(db.CreateParam("VideoFormat", DbType.String, ParameterDirection.Input, res.VideoFormat));
                else
                    para.Add(db.CreateParam("VideoFormat", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.CertiURL))
                    para.Add(db.CreateParam("CertiURL", DbType.String, ParameterDirection.Input, res.CertiURL));
                else
                    para.Add(db.CreateParam("CertiURL", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.CertiFormat))
                    para.Add(db.CreateParam("CertiFormat", DbType.String, ParameterDirection.Input, res.CertiFormat));
                else
                    para.Add(db.CreateParam("CertiFormat", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(res.ShortName))
                    para.Add(db.CreateParam("ShortName", DbType.String, ParameterDirection.Input, res.ShortName));
                else
                    para.Add(db.CreateParam("ShortName", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("AddUpdate_SupplierMaster", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    resp.Status = dt.Rows[0]["Status"].ToString();
                    resp.Message = dt.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_Not_Mapped_SupplierStock([FromBody] JObject data)
        {
            Get_SearchStock_Req req = new Get_SearchStock_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Get_SearchStock_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("SupplierId", DbType.Int32, ParameterDirection.Input, req.SupplierId));

                DataTable Not_Mapped_dt = db.ExecuteSP("Get_Not_Mapped_SupplierStock", para.ToArray(), false);

                if (Not_Mapped_dt != null && Not_Mapped_dt.Rows.Count > 0)
                {
                    string suppname = Convert.ToString(Not_Mapped_dt.Rows[0]["SupplierName"]);
                    string filename = ReplaceSpecialCharacters(suppname) + " Not Mapped Supplier Stock " + DateTime.Now.ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    _path = _path.Replace("Temp", "ExcelFile");
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");

                    EpExcelExport.Not_Mapped_SupplierStock_Excel(Not_Mapped_dt, suppname, realpath, realpath + filename + ".xlsx");

                    string _strxml = _path + filename + ".xlsx";
                    return Ok(_strxml);
                }
                else
                {
                    return Ok("No Stock found !");
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }
        public static string ReplaceSpecialCharacters(string input)
        {
            // Define a regular expression pattern to match special characters
            string pattern = "[^a-zA-Z0-9]+";

            // Replace special characters with underscores
            string result = Regex.Replace(input, pattern, "_");

            return result;
        }
        [HttpPost]
        public IHttpActionResult GetUsers([FromBody] JObject data)
        {
            GetUsers_Req Req = new GetUsers_Req();
            try
            {
                Req = JsonConvert.DeserializeObject<GetUsers_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<GetUsers_Res>
                {
                    Data = new List<GetUsers_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });

            }

            try
            {
                DataTable dt = GetUsers_Inner(Req);
                if (dt != null && dt.Rows.Count > 0)
                {
                    List<GetUsers_Res> List_Res = new List<GetUsers_Res>();
                    List_Res = DataTableExtension.ToList<GetUsers_Res>(dt);

                    return Ok(new ServiceResponse<GetUsers_Res>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<GetUsers_Res>
                    {
                        Data = new List<GetUsers_Res>(),
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<GetUsers_Res>
                {
                    Data = new List<GetUsers_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [NonAction]
        private DataTable GetUsers_Inner(GetUsers_Req Req)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(Req.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, Req.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (Req.PgNo > 0)
                    para.Add(db.CreateParam("PgNo", DbType.Int32, ParameterDirection.Input, Req.PgNo));
                else
                    para.Add(db.CreateParam("PgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (Req.PgSize > 0)
                    para.Add(db.CreateParam("PgSize", DbType.Int32, ParameterDirection.Input, Req.PgSize));
                else
                    para.Add(db.CreateParam("PgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Req.FilterType))
                    para.Add(db.CreateParam("FilterType", DbType.String, ParameterDirection.Input, Req.FilterType));
                else
                    para.Add(db.CreateParam("FilterType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Req.FromDate))
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, Req.FromDate));
                else
                    para.Add(db.CreateParam("FromDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Req.ToDate))
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, Req.ToDate));
                else
                    para.Add(db.CreateParam("ToDate", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Req.FortunePartyCode))
                    para.Add(db.CreateParam("FortunePartyCode", DbType.String, ParameterDirection.Input, Req.FortunePartyCode));
                else
                    para.Add(db.CreateParam("FortunePartyCode", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Req.FullName))
                    para.Add(db.CreateParam("FullName", DbType.String, ParameterDirection.Input, Req.FullName));
                else
                    para.Add(db.CreateParam("FullName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Req.UserName))
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, Req.UserName));
                else
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Req.CompName))
                    para.Add(db.CreateParam("CompName", DbType.String, ParameterDirection.Input, Req.CompName));
                else
                    para.Add(db.CreateParam("CompName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Req.IsActive))
                    para.Add(db.CreateParam("IsActive", DbType.Boolean, ParameterDirection.Input, Convert.ToBoolean(Convert.ToInt32(Req.IsActive))));
                else
                    para.Add(db.CreateParam("IsActive", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (Req.UserType > 0)
                    para.Add(db.CreateParam("UserType", DbType.Int64, ParameterDirection.Input, Req.UserType));
                else
                    para.Add(db.CreateParam("UserType", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (Req.UserId > 0)
                    para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, Req.UserId));
                else
                    para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(Req.CompanyUserCustomer))
                    para.Add(db.CreateParam("CompanyUserCustomer", DbType.String, ParameterDirection.Input, Req.CompanyUserCustomer));
                else
                    para.Add(db.CreateParam("CompanyUserCustomer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (Req.Assist_UserId > 0)
                    para.Add(db.CreateParam("Assist_UserId", DbType.Int64, ParameterDirection.Input, Req.Assist_UserId));
                else
                    para.Add(db.CreateParam("Assist_UserId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("URL_Exists", DbType.Boolean, ParameterDirection.Input, Req.URL_Exists));

                DataTable dt = db.ExecuteSP("Get_UserMas", para.ToArray(), false);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public IHttpActionResult SaveUserData([FromBody] JObject data)
        {
            UserDetails_Req req = new UserDetails_Req();
            try
            {
                req = JsonConvert.DeserializeObject<UserDetails_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();
                DataTable _dtuserchk = CheckUserName(req.UserName, req.UserId);
                if (_dtuserchk != null)
                {
                    if (_dtuserchk.Rows.Count != 0)
                    {
                        resp.Status = "0";
                        resp.Message = "Username is already exist.";
                        return Ok(resp);
                    }
                }
                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (req.UserId > 0)
                    para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.UserId));
                else
                    para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.UserName))
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, req.UserName));
                else
                    para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Password))
                    para.Add(db.CreateParam("Password", DbType.String, ParameterDirection.Input, req.Password));
                else
                    para.Add(db.CreateParam("Password", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("Active", DbType.Boolean, ParameterDirection.Input, req.Active));

                if (!string.IsNullOrEmpty(req.UserType))
                    para.Add(db.CreateParam("UserType", DbType.String, ParameterDirection.Input, req.UserType));
                else
                    para.Add(db.CreateParam("UserType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.FirstName))
                    para.Add(db.CreateParam("FirstName", DbType.String, ParameterDirection.Input, req.FirstName));
                else
                    para.Add(db.CreateParam("FirstName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.LastName))
                    para.Add(db.CreateParam("LastName", DbType.String, ParameterDirection.Input, req.LastName));
                else
                    para.Add(db.CreateParam("LastName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CompanyName))
                    para.Add(db.CreateParam("CompanyName", DbType.String, ParameterDirection.Input, req.CompanyName));
                else
                    para.Add(db.CreateParam("CompanyName", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (req.FortunePartyCode > 0)
                    para.Add(db.CreateParam("FortunePartyCode", DbType.Int32, ParameterDirection.Input, req.FortunePartyCode));
                else
                    para.Add(db.CreateParam("FortunePartyCode", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.EmailId))
                    para.Add(db.CreateParam("EmailId", DbType.String, ParameterDirection.Input, req.EmailId));
                else
                    para.Add(db.CreateParam("EmailId", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.EmailId_2))
                    para.Add(db.CreateParam("EmailId_2", DbType.String, ParameterDirection.Input, req.EmailId_2));
                else
                    para.Add(db.CreateParam("EmailId_2", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.MobileNo))
                    para.Add(db.CreateParam("MobileNo", DbType.String, ParameterDirection.Input, req.MobileNo));
                else
                    para.Add(db.CreateParam("MobileNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (req.AssistBy > 0)
                    para.Add(db.CreateParam("AssistBy", DbType.Int32, ParameterDirection.Input, req.AssistBy));
                else
                    para.Add(db.CreateParam("AssistBy", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (req.SubAssistBy > 0)
                    para.Add(db.CreateParam("SubAssistBy", DbType.Int32, ParameterDirection.Input, req.SubAssistBy));
                else
                    para.Add(db.CreateParam("SubAssistBy", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("CreatedBy", DbType.Int32, ParameterDirection.Input, userID));

                if (req.UserCode > 0)
                    para.Add(db.CreateParam("UserCode", DbType.Int32, ParameterDirection.Input, req.UserCode));
                else
                    para.Add(db.CreateParam("UserCode", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("View", DbType.Boolean, ParameterDirection.Input, req.View));
                para.Add(db.CreateParam("Download", DbType.Boolean, ParameterDirection.Input, req.Download));

                DataTable dt = db.ExecuteSP("AddUpdate_User", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    resp.Status = dt.Rows[0]["Status"].ToString();
                    resp.Message = dt.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Delete_UserMas([FromBody] JObject data)
        {
            GetUsers_Res req = new GetUsers_Res();
            try
            {
                req = JsonConvert.DeserializeObject<GetUsers_Res>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.UserId));

                DataTable dt = db.ExecuteSP("Delete_UserMas", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    resp.Status = dt.Rows[0]["Status"].ToString();
                    resp.Message = dt.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult Get_PriceList_ParaMas()
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("Get_PriceList_ParaMas", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_PriceList_ParaMas> List_Res = new List<Get_PriceList_ParaMas>();
                    List_Res = DataTableExtension.ToList<Get_PriceList_ParaMas>(dt);

                    return Ok(new ServiceResponse<Get_PriceList_ParaMas>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_PriceList_ParaMas>
                    {
                        Data = new List<Get_PriceList_ParaMas>(),
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_PriceList_ParaMas>
                {
                    Data = new List<Get_PriceList_ParaMas>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_PriceListCategory()
        {
            try
            {
                Database db = new Database();
                List<Get_PriceListCategory_Res> List_Res = new List<Get_PriceListCategory_Res>();

                List<IDbDataParameter> para = new List<IDbDataParameter>();
                DataTable dt = db.ExecuteSP("Get_PriceListCategory", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List_Res = DataTableExtension.ToList<Get_PriceListCategory_Res>(dt);
                }
                return Ok(new ServiceResponse<Get_PriceListCategory_Res>
                {
                    Data = List_Res,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_PriceListCategory_Res>
                {
                    Data = new List<Get_PriceListCategory_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [NonAction]
        private DataTable CheckUserName(string suserName, int userid)
        {
            Database db = new Database();
            List<IDbDataParameter> para = new List<IDbDataParameter>();

            if (userid > 0)
                para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, userid));
            else
                para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, DBNull.Value));
            para.Add(db.CreateParam("sUserName", DbType.String, ParameterDirection.Input, suserName));
            DataTable dt = db.ExecuteSP("get_user_Detail", para.ToArray(), false);
            return dt;
        }
        [HttpPost]
        public IHttpActionResult FortunePartyCode_Exist([FromBody] JObject data)
        {
            Exist_Request req = new Exist_Request();
            try
            {
                req = JsonConvert.DeserializeObject<Exist_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                if (req.iUserId > 0)
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, req.iUserId));
                else
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("FortunePartyCode", DbType.Int32, ParameterDirection.Input, req.FortunePartyCode));

                System.Data.DataTable dt = db.ExecuteSP("Get_FortuneCode_Exists", para.ToArray(), false);

                return Ok(new CommonResponse
                {
                    Message = dt.Rows[0]["Message"].ToString(),
                    Status = dt.Rows[0]["Status"].ToString(),
                    Error = ""
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult UserCode_Exists([FromBody] JObject data)
        {
            Exist_Request req = new Exist_Request();
            try
            {
                req = JsonConvert.DeserializeObject<Exist_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                if (req.iUserId > 0)
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, req.iUserId));
                else
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("UserCode", DbType.Int32, ParameterDirection.Input, req.UserCode));

                System.Data.DataTable dt = db.ExecuteSP("Get_UserCode_Exists", para.ToArray(), false);

                return Ok(new CommonResponse
                {
                    Message = dt.Rows[0]["Message"].ToString(),
                    Status = dt.Rows[0]["Status"].ToString(),
                    Error = ""
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<CommonResponse>
                {
                    Data = new List<CommonResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_ColumnMaster([FromBody] JObject data)
        {
            Get_CategoryMas_Req req = new Get_CategoryMas_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Get_CategoryMas_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_ColumnMaster_Res>
                {
                    Data = null,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(req.Not_Col_Id))
                    para.Add(db.CreateParam("Not_Col_Id", DbType.String, ParameterDirection.Input, req.Not_Col_Id));
                else
                    para.Add(db.CreateParam("Not_Col_Id", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Get_Column_Master", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_ColumnMaster_Res> list = new List<Get_ColumnMaster_Res>();
                    list = DataTableExtension.ToList<Get_ColumnMaster_Res>(dt);

                    return Ok(new ServiceResponse<Get_ColumnMaster_Res>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_ColumnMaster_Res>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_ColumnMaster_Res>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult Get_FancyColor()
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("Get_FancyColor", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_FancyColor_Res> list = new List<Get_FancyColor_Res>();
                    list = DataTableExtension.ToList<Get_FancyColor_Res>(dt);

                    return Ok(new ServiceResponse<Get_FancyColor_Res>
                    {
                        Data = list,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_FancyColor_Res>
                    {
                        Data = null,
                        Message = "No data found.",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_FancyColor_Res>
                {
                    Data = null,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult get_key_to_symbol()
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("get_key_to_symbol", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<get_key_to_symbol> List_Res = new List<get_key_to_symbol>();
                    List_Res = DataTableExtension.ToList<get_key_to_symbol>(dt);

                    return Ok(new ServiceResponse<get_key_to_symbol>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<get_key_to_symbol>
                    {
                        Data = new List<get_key_to_symbol>(),
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<get_key_to_symbol>
                {
                    Data = new List<get_key_to_symbol>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_ParaMas()
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("Get_ParaMas", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<Get_ParaMas_Res> List_Res = new List<Get_ParaMas_Res>();
                    List_Res = DataTableExtension.ToList<Get_ParaMas_Res>(dt);

                    return Ok(new ServiceResponse<Get_ParaMas_Res>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<Get_ParaMas_Res>
                    {
                        Data = new List<Get_ParaMas_Res>(),
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_ParaMas_Res>
                {
                    Data = new List<Get_ParaMas_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [NonAction]
        private DataTable SearchStock(Get_SearchStock_Req req)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                //int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                if (!string.IsNullOrEmpty(req.Type))
                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, req.Type));
                else
                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, req.UserId));

                if (req.PgNo > 0)
                    para.Add(db.CreateParam("PgNo", DbType.Int32, ParameterDirection.Input, req.PgNo));
                else
                    para.Add(db.CreateParam("PgNo", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (req.PgSize > 0)
                    para.Add(db.CreateParam("PgSize", DbType.Int32, ParameterDirection.Input, req.PgSize));
                else
                    para.Add(db.CreateParam("PgSize", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, req.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.RefNo))
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, req.RefNo));
                else
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.SupplierId_RefNo_SupplierRefNo))
                    para.Add(db.CreateParam("SupplierId_RefNo_SupplierRefNo", DbType.String, ParameterDirection.Input, req.SupplierId_RefNo_SupplierRefNo));
                else
                    para.Add(db.CreateParam("SupplierId_RefNo_SupplierRefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.SupplierId))
                    para.Add(db.CreateParam("SupplierId", DbType.String, ParameterDirection.Input, req.SupplierId));
                else
                    para.Add(db.CreateParam("SupplierId", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Shape))
                    para.Add(db.CreateParam("Shape", DbType.String, ParameterDirection.Input, req.Shape));
                else
                    para.Add(db.CreateParam("Shape", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Pointer))
                    para.Add(db.CreateParam("Pointer", DbType.String, ParameterDirection.Input, req.Pointer));
                else
                    para.Add(db.CreateParam("Pointer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Sub_Pointer))
                    para.Add(db.CreateParam("Sub_Pointer", DbType.String, ParameterDirection.Input, req.Sub_Pointer));
                else
                    para.Add(db.CreateParam("Sub_Pointer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ColorType))
                    para.Add(db.CreateParam("ColorType", DbType.String, ParameterDirection.Input, req.ColorType));
                else
                    para.Add(db.CreateParam("ColorType", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Color))
                    para.Add(db.CreateParam("Color", DbType.String, ParameterDirection.Input, req.Color));
                else
                    para.Add(db.CreateParam("Color", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.INTENSITY))
                    para.Add(db.CreateParam("INTENSITY", DbType.String, ParameterDirection.Input, req.INTENSITY));
                else
                    para.Add(db.CreateParam("INTENSITY", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.OVERTONE))
                    para.Add(db.CreateParam("OVERTONE", DbType.String, ParameterDirection.Input, req.OVERTONE));
                else
                    para.Add(db.CreateParam("OVERTONE", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.FANCY_COLOR))
                    para.Add(db.CreateParam("FANCY_COLOR", DbType.String, ParameterDirection.Input, req.FANCY_COLOR));
                else
                    para.Add(db.CreateParam("FANCY_COLOR", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Clarity))
                    para.Add(db.CreateParam("Clarity", DbType.String, ParameterDirection.Input, req.Clarity));
                else
                    para.Add(db.CreateParam("Clarity", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Cut))
                    para.Add(db.CreateParam("Cut", DbType.String, ParameterDirection.Input, req.Cut));
                else
                    para.Add(db.CreateParam("Cut", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Polish))
                    para.Add(db.CreateParam("Polish", DbType.String, ParameterDirection.Input, req.Polish));
                else
                    para.Add(db.CreateParam("Polish", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Symm))
                    para.Add(db.CreateParam("Symm", DbType.String, ParameterDirection.Input, req.Symm));
                else
                    para.Add(db.CreateParam("Symm", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Fls))
                    para.Add(db.CreateParam("Fls", DbType.String, ParameterDirection.Input, req.Fls));
                else
                    para.Add(db.CreateParam("Fls", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.BGM))
                    para.Add(db.CreateParam("BGM", DbType.String, ParameterDirection.Input, req.BGM));
                else
                    para.Add(db.CreateParam("BGM", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Lab))
                    para.Add(db.CreateParam("Lab", DbType.String, ParameterDirection.Input, req.Lab));
                else
                    para.Add(db.CreateParam("Lab", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CrownBlack))
                    para.Add(db.CreateParam("CrownBlack", DbType.String, ParameterDirection.Input, req.CrownBlack));
                else
                    para.Add(db.CreateParam("CrownBlack", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.TableBlack))
                    para.Add(db.CreateParam("TableBlack", DbType.String, ParameterDirection.Input, req.TableBlack));
                else
                    para.Add(db.CreateParam("TableBlack", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.TableWhite))
                    para.Add(db.CreateParam("TableWhite", DbType.String, ParameterDirection.Input, req.TableWhite));
                else
                    para.Add(db.CreateParam("TableWhite", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CrownWhite))
                    para.Add(db.CreateParam("CrownWhite", DbType.String, ParameterDirection.Input, req.CrownWhite));
                else
                    para.Add(db.CreateParam("CrownWhite", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.TableOpen))
                    para.Add(db.CreateParam("TableOpen", DbType.String, ParameterDirection.Input, req.TableOpen));
                else
                    para.Add(db.CreateParam("TableOpen", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CrownOpen))
                    para.Add(db.CreateParam("CrownOpen", DbType.String, ParameterDirection.Input, req.CrownOpen));
                else
                    para.Add(db.CreateParam("CrownOpen", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PavOpen))
                    para.Add(db.CreateParam("PavOpen", DbType.String, ParameterDirection.Input, req.PavOpen));
                else
                    para.Add(db.CreateParam("PavOpen", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.GirdleOpen))
                    para.Add(db.CreateParam("GirdleOpen", DbType.String, ParameterDirection.Input, req.GirdleOpen));
                else
                    para.Add(db.CreateParam("GirdleOpen", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.KTSBlank))
                    para.Add(db.CreateParam("KTSBlank", DbType.Boolean, ParameterDirection.Input, req.KTSBlank));
                else
                    para.Add(db.CreateParam("KTSBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Keytosymbol))
                    para.Add(db.CreateParam("Keytosymbol", DbType.String, ParameterDirection.Input, req.Keytosymbol));
                else
                    para.Add(db.CreateParam("Keytosymbol", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CheckKTS))
                    para.Add(db.CreateParam("CheckKTS", DbType.String, ParameterDirection.Input, req.CheckKTS));
                else
                    para.Add(db.CreateParam("CheckKTS", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.UNCheckKTS))
                    para.Add(db.CreateParam("UNCheckKTS", DbType.String, ParameterDirection.Input, req.UNCheckKTS));
                else
                    para.Add(db.CreateParam("UNCheckKTS", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.FromDisc))
                    para.Add(db.CreateParam("FromDisc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromDisc)));
                else
                    para.Add(db.CreateParam("FromDisc", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToDisc))
                    para.Add(db.CreateParam("ToDisc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToDisc)));
                else
                    para.Add(db.CreateParam("ToDisc", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.FromTotAmt))
                    para.Add(db.CreateParam("FromTotAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromTotAmt)));
                else
                    para.Add(db.CreateParam("FromTotAmt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToTotAmt))
                    para.Add(db.CreateParam("ToTotAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToTotAmt)));
                else
                    para.Add(db.CreateParam("ToTotAmt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.LengthBlank))
                    para.Add(db.CreateParam("LengthBlank", DbType.Boolean, ParameterDirection.Input, req.LengthBlank));
                else
                    para.Add(db.CreateParam("LengthBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.FromLength))
                    para.Add(db.CreateParam("FromLength", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromLength)));
                else
                    para.Add(db.CreateParam("FromLength", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToLength))
                    para.Add(db.CreateParam("ToLength", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToLength)));
                else
                    para.Add(db.CreateParam("ToLength", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.WidthBlank))
                    para.Add(db.CreateParam("WidthBlank", DbType.Boolean, ParameterDirection.Input, req.WidthBlank));
                else
                    para.Add(db.CreateParam("WidthBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.FromWidth))
                    para.Add(db.CreateParam("FromWidth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromWidth)));
                else
                    para.Add(db.CreateParam("FromWidth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToWidth))
                    para.Add(db.CreateParam("ToWidth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToWidth)));
                else
                    para.Add(db.CreateParam("ToWidth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.DepthBlank))
                    para.Add(db.CreateParam("DepthBlank", DbType.Boolean, ParameterDirection.Input, req.DepthBlank));
                else
                    para.Add(db.CreateParam("DepthBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.FromDepth))
                    para.Add(db.CreateParam("FromDepth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromDepth)));
                else
                    para.Add(db.CreateParam("FromDepth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToDepth))
                    para.Add(db.CreateParam("ToDepth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToDepth)));
                else
                    para.Add(db.CreateParam("ToDepth", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.DepthPerBlank))
                    para.Add(db.CreateParam("DepthPerBlank", DbType.Boolean, ParameterDirection.Input, req.DepthPerBlank));
                else
                    para.Add(db.CreateParam("DepthPerBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.FromDepthPer))
                    para.Add(db.CreateParam("FromDepthPer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromDepthPer)));
                else
                    para.Add(db.CreateParam("FromDepthPer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToDepthPer))
                    para.Add(db.CreateParam("ToDepthPer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToDepthPer)));
                else
                    para.Add(db.CreateParam("ToDepthPer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.TablePerBlank))
                    para.Add(db.CreateParam("TablePerBlank", DbType.Boolean, ParameterDirection.Input, req.TablePerBlank));
                else
                    para.Add(db.CreateParam("TablePerBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.FromTablePer))
                    para.Add(db.CreateParam("FromTablePer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromTablePer)));
                else
                    para.Add(db.CreateParam("FromTablePer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToTablePer))
                    para.Add(db.CreateParam("ToTablePer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToTablePer)));
                else
                    para.Add(db.CreateParam("ToTablePer", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Img))
                    para.Add(db.CreateParam("Img", DbType.String, ParameterDirection.Input, req.Img));
                else
                    para.Add(db.CreateParam("Img", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Vdo))
                    para.Add(db.CreateParam("Vdo", DbType.String, ParameterDirection.Input, req.Vdo));
                else
                    para.Add(db.CreateParam("Vdo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.Certi))
                    para.Add(db.CreateParam("Certi", DbType.String, ParameterDirection.Input, req.Certi));
                else
                    para.Add(db.CreateParam("Certi", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CrAngBlank))
                    para.Add(db.CreateParam("CrAngBlank", DbType.Boolean, ParameterDirection.Input, req.CrAngBlank));
                else
                    para.Add(db.CreateParam("CrAngBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.FromCrAng))
                    para.Add(db.CreateParam("FromCrAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromCrAng)));
                else
                    para.Add(db.CreateParam("FromCrAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToCrAng))
                    para.Add(db.CreateParam("ToCrAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToCrAng)));
                else
                    para.Add(db.CreateParam("ToCrAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CrHtBlank))
                    para.Add(db.CreateParam("CrHtBlank", DbType.Boolean, ParameterDirection.Input, req.CrHtBlank));
                else
                    para.Add(db.CreateParam("CrHtBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.FromCrHt))
                    para.Add(db.CreateParam("FromCrHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromCrHt)));
                else
                    para.Add(db.CreateParam("FromCrHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToCrHt))
                    para.Add(db.CreateParam("ToCrHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToCrHt)));
                else
                    para.Add(db.CreateParam("ToCrHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PavAngBlank))
                    para.Add(db.CreateParam("PavAngBlank", DbType.Boolean, ParameterDirection.Input, req.PavAngBlank));
                else
                    para.Add(db.CreateParam("PavAngBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.FromPavAng))
                    para.Add(db.CreateParam("FromPavAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromPavAng)));
                else
                    para.Add(db.CreateParam("FromPavAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToPavAng))
                    para.Add(db.CreateParam("ToPavAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToPavAng)));
                else
                    para.Add(db.CreateParam("ToPavAng", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PavHtBlank))
                    para.Add(db.CreateParam("PavHtBlank", DbType.Boolean, ParameterDirection.Input, req.PavHtBlank));
                else
                    para.Add(db.CreateParam("PavHtBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.FromPavHt))
                    para.Add(db.CreateParam("FromPavHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromPavHt)));
                else
                    para.Add(db.CreateParam("FromPavHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.ToPavHt))
                    para.Add(db.CreateParam("ToPavHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToPavHt)));
                else
                    para.Add(db.CreateParam("ToPavHt", DbType.String, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("View", DbType.Boolean, ParameterDirection.Input, req.View));

                para.Add(db.CreateParam("Download", DbType.Boolean, ParameterDirection.Input, req.Download));

                if (!string.IsNullOrEmpty(req.PricingMethod))
                    para.Add(db.CreateParam("PricingMethod", DbType.String, ParameterDirection.Input, req.PricingMethod));
                else
                    para.Add(db.CreateParam("PricingMethod", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PricingSign))
                    para.Add(db.CreateParam("PricingSign", DbType.String, ParameterDirection.Input, req.PricingSign));
                else
                    para.Add(db.CreateParam("PricingSign", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (req.PricingDisc > 0)
                    para.Add(db.CreateParam("PricingDisc", DbType.Decimal, ParameterDirection.Input, Convert.ToDecimal(req.PricingDisc)));
                else
                    para.Add(db.CreateParam("PricingDisc", DbType.Decimal, ParameterDirection.Input, DBNull.Value));

                DataTable Stock_dt = db.ExecuteSP("Get_SearchStock", para.ToArray(), false);
                return Stock_dt;
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, null);
                return null;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Get_SearchStock([FromBody] JObject data)
        {
            Get_SearchStock_Req req = new Get_SearchStock_Req();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(data)))
                {
                    JObject test1 = JObject.Parse(data.ToString());
                    req = JsonConvert.DeserializeObject<Get_SearchStock_Req>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SearchStock_Res>
                {
                    Data = new List<Get_SearchStock_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                DataTable Stock_dt = SearchStock(req);

                DataRow[] dra = Stock_dt.Select("iSr IS NULL");
                SearchSummary searchSummary = new SearchSummary();
                if (dra.Length > 0)
                {
                    searchSummary.TOT_PAGE = Convert.ToInt32(dra[0]["TOTAL_PAGE"]);
                    searchSummary.PAGE_SIZE = Convert.ToInt32(dra[0]["PAGE_SIZE"]);
                    searchSummary.TOT_PCS = Convert.ToInt32(dra[0]["Ref_No"]);
                    searchSummary.TOT_CTS = Convert.ToDouble(dra[0]["Cts"]);
                    searchSummary.TOT_RAP_AMOUNT = Convert.ToDouble((Convert.ToString(dra[0]["Rap_Amount"]) != "" && Convert.ToString(dra[0]["Rap_Amount"]) != null ? dra[0]["Rap_Amount"] : "0"));
                    searchSummary.AVG_PRICE_PER_CTS = Convert.ToDouble(dra[0]["Base_Price_Cts"]);

                    if (req.Type == "Buyer List")
                    {
                        searchSummary.AVG_SALES_DISC_PER = Convert.ToDouble((Convert.ToString(dra[0]["SUPPLIER_COST_DISC"]) != "" && Convert.ToString(dra[0]["SUPPLIER_COST_DISC"]) != null ? dra[0]["SUPPLIER_COST_DISC"] : "0"));
                        searchSummary.TOT_NET_AMOUNT = Convert.ToDouble(dra[0]["SUPPLIER_COST_VALUE"]);
                    }
                    else if (req.Type == "Supplier List" || req.Type == "Customer List")
                    {
                        searchSummary.AVG_SALES_DISC_PER = Convert.ToDouble((Convert.ToString(dra[0]["CUSTOMER_COST_DISC"]) != "" && Convert.ToString(dra[0]["CUSTOMER_COST_DISC"]) != null ? dra[0]["CUSTOMER_COST_DISC"] : "0"));
                        searchSummary.TOT_NET_AMOUNT = Convert.ToDouble(dra[0]["CUSTOMER_COST_VALUE"]);
                    }
                }

                Stock_dt.DefaultView.RowFilter = "iSr IS NOT NULL";
                Stock_dt = Stock_dt.DefaultView.ToTable();

                SearchDiamondsResponse searchDiamondsResponse = new SearchDiamondsResponse();

                List<Get_SearchStock_Res> listSearchStone = new List<Get_SearchStock_Res>();
                listSearchStone = DataTableExtension.ToList<Get_SearchStock_Res>(Stock_dt);
                List<SearchDiamondsResponse> searchDiamondsResponses = new List<SearchDiamondsResponse>();

                if (listSearchStone.Count > 0)
                {
                    searchDiamondsResponses.Add(new SearchDiamondsResponse()
                    {
                        DataList = listSearchStone,
                        DataSummary = searchSummary
                    });
                }

                return Ok(new ServiceResponse<SearchDiamondsResponse>
                {
                    Data = searchDiamondsResponses,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<SearchDiamondsResponse>
                {
                    Data = new List<SearchDiamondsResponse>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Excel_SearchStock([FromBody] JObject data)
        {
            Get_SearchStock_Req req = new Get_SearchStock_Req();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(data)))
                {
                    JObject test1 = JObject.Parse(data.ToString());
                    req = JsonConvert.DeserializeObject<Get_SearchStock_Req>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }

            try
            {
                DataTable Stock_dt = SearchStock(req);

                Stock_dt.DefaultView.RowFilter = "iSr IS NOT NULL";
                Stock_dt = Stock_dt.DefaultView.ToTable();

                if (Stock_dt != null && Stock_dt.Rows.Count > 0)
                {
                    string filename = req.Type + " " + DateTime.Now.ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    _path = _path.Replace("Temp", "ExcelFile");
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");

                    if (req.Type == "Buyer List")
                    {
                        Database db = new Database();
                        List<IDbDataParameter> para;
                        para = new List<IDbDataParameter>();

                        //int UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                        para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.UserId));
                        para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "BUYER"));

                        DataTable Col_dt = db.ExecuteSP("Get_SearchStock_ColumnSetting", para.ToArray(), false);

                        EpExcelExport.Buyer_Excel(Stock_dt, Col_dt, realpath, realpath + filename + ".xlsx");
                    }
                    else if (req.Type == "Supplier List")
                    {
                        Database db = new Database();
                        List<IDbDataParameter> para;
                        para = new List<IDbDataParameter>();

                        //int UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                        para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.UserId));
                        para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "SUPPLIER"));

                        DataTable Col_dt = db.ExecuteSP("Get_SearchStock_ColumnSetting", para.ToArray(), false);

                        EpExcelExport.Supplier_Excel(Stock_dt, Col_dt, realpath, realpath + filename + ".xlsx");
                    }
                    else if (req.Type == "Customer List")
                    {
                        Database db = new Database();
                        List<IDbDataParameter> para;
                        para = new List<IDbDataParameter>();

                        //int UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                        para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.UserId));
                        para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "CUSTOMER"));

                        DataTable Col_dt = db.ExecuteSP("Get_SearchStock_ColumnSetting", para.ToArray(), false);

                        EpExcelExport.Customer_Excel(Stock_dt, Col_dt, realpath, realpath + filename + ".xlsx");
                    }

                    string _strxml = _path + filename + ".xlsx";
                    return Ok(_strxml);
                }
                else
                {
                    return Ok("No Stock found as per filter criteria !");
                }

            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Email_SearchStock([FromBody] JObject data)
        {
            Get_SearchStock_Req req = new Get_SearchStock_Req();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(data)))
                {
                    JObject test1 = JObject.Parse(data.ToString());
                    req = JsonConvert.DeserializeObject<Get_SearchStock_Req>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                DataTable Stock_dt = SearchStock(req);

                Stock_dt.DefaultView.RowFilter = "iSr IS NOT NULL";
                Stock_dt = Stock_dt.DefaultView.ToTable();
                CommonResponse resp = new CommonResponse();

                if (Stock_dt != null && Stock_dt.Rows.Count > 0)
                {
                    string filename = req.Type + " " + DateTime.Now.ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    _path = _path.Replace("Temp", "ExcelFile");
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");

                    if (req.Type == "Buyer List")
                    {
                        Database db = new Database();
                        List<IDbDataParameter> para;
                        para = new List<IDbDataParameter>();

                        //int UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                        para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.UserId));
                        para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "BUYER"));

                        DataTable Col_dt = db.ExecuteSP("Get_SearchStock_ColumnSetting", para.ToArray(), false);

                        EpExcelExport.Buyer_Excel(Stock_dt, Col_dt, realpath, realpath + filename + ".xlsx");
                    }
                    else if (req.Type == "Supplier List")
                    {
                        Database db = new Database();
                        List<IDbDataParameter> para;
                        para = new List<IDbDataParameter>();

                        //int UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                        para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.UserId));
                        para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "SUPPLIER"));

                        DataTable Col_dt = db.ExecuteSP("Get_SearchStock_ColumnSetting", para.ToArray(), false);

                        EpExcelExport.Supplier_Excel(Stock_dt, Col_dt, realpath, realpath + filename + ".xlsx");
                    }
                    else if (req.Type == "Customer List")
                    {
                        Database db = new Database();
                        List<IDbDataParameter> para;
                        para = new List<IDbDataParameter>();

                        //int UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                        para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.UserId));
                        para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "CUSTOMER"));

                        DataTable Col_dt = db.ExecuteSP("Get_SearchStock_ColumnSetting", para.ToArray(), false);

                        EpExcelExport.Customer_Excel(Stock_dt, Col_dt, realpath, realpath + filename + ".xlsx");
                    }

                    string _strxml = _path + filename + ".xlsx";

                    MailMessage xloMail = new MailMessage();
                    SmtpClient xloSmtp = new SmtpClient();

                    xloMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Connect Gia");
                    xloMail.Bcc.Add("hardik@brainwaves.co.in");
                    if (req.ToAddress.EndsWith(","))
                        req.ToAddress = req.ToAddress.Remove(req.ToAddress.Length - 1);

                    xloMail.To.Add(req.ToAddress);
                    xloMail.Subject = "Stone Selection";
                    xloMail.IsBodyHtml = false;
                    AlternateView av = AlternateView.CreateAlternateViewFromString(((string.IsNullOrEmpty(req.Comments)) ? "" : req.Comments), null, "");
                    xloMail.AlternateViews.Add(av);

                    ContentType contentType = new System.Net.Mime.ContentType();
                    contentType.MediaType = System.Net.Mime.MediaTypeNames.Application.Octet;
                    contentType.Name = filename + ".xlsx";
                    Attachment attachFile = new Attachment(realpath + filename + ".xlsx", contentType);
                    xloMail.Attachments.Add(attachFile);

                    xloSmtp.Timeout = 500000;
                    xloSmtp.Send(xloMail);

                    xloMail.Attachments.Dispose();
                    xloMail.AlternateViews.Dispose();
                    xloMail.Dispose();

                    if (filename.Length > 0)
                        if (System.IO.File.Exists(filename))
                            System.IO.File.Delete(filename);

                    resp.Status = "1";
                    resp.Message = "Mail sent successfully.";
                    resp.Error = "";

                    return Ok(resp);
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = "No Stock found as per filter criteria !";
                    resp.Error = "";

                    return Ok(resp);
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult get_UserType()
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                DataTable dt = db.ExecuteSP("get_UserType", para.ToArray(), false);
                List<UserType_Res> List_Res = new List<UserType_Res>();

                if (dt != null && dt.Rows.Count > 0)
                {
                    List_Res = DataTableExtension.ToList<UserType_Res>(dt);
                }

                return Ok(new ServiceResponse<UserType_Res>
                {
                    Data = List_Res,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<UserType_Res>
                {
                    Data = new List<UserType_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult LoginCheck()
        {
            try
            {
                return Ok(new CommonResponse
                {
                    Message = "OK",
                    Status = "1",
                    Error = ""
                });
            }
            catch (Exception ex)
            {
                return Ok(new CommonResponse
                {
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0",
                    Error = ex.StackTrace
                });
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
        public static void removingGreenTagWarning(ExcelWorksheet template1, string address)
        {
            var xdoc = template1.WorksheetXml;
            //Create the import nodes (note the plural vs singular
            var ignoredErrors = xdoc.CreateNode(System.Xml.XmlNodeType.Element, "ignoredErrors", xdoc.DocumentElement.NamespaceURI);
            var ignoredError = xdoc.CreateNode(System.Xml.XmlNodeType.Element, "ignoredError", xdoc.DocumentElement.NamespaceURI);
            ignoredErrors.AppendChild(ignoredError);

            //Attributes for the INNER node
            var sqrefAtt = xdoc.CreateAttribute("sqref");
            sqrefAtt.Value = address;// Or whatever range is needed....

            var flagAtt = xdoc.CreateAttribute("numberStoredAsText");
            flagAtt.Value = "1";

            ignoredError.Attributes.Append(sqrefAtt);
            ignoredError.Attributes.Append(flagAtt);

            //Now put the OUTER node into the worksheet xml
            xdoc.LastChild.AppendChild(ignoredErrors);
        }

        public static void ApiLog(int SupplierId, bool FileTransfer, string message)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("SupplierId", DbType.Int64, ParameterDirection.Input, SupplierId));
                para.Add(db.CreateParam("FileTransfer", DbType.Boolean, ParameterDirection.Input, FileTransfer));
                para.Add(db.CreateParam("Message", DbType.String, ParameterDirection.Input, message));

                DataTable dt = db.ExecuteSP("ApiLog", para.ToArray(), false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void Supplier_Start_End(int SupplierId, string type)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("SupplierId", DbType.Int64, ParameterDirection.Input, SupplierId));
                para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, type));

                DataTable dt = db.ExecuteSP("Start_End_SupplierAPIData", para.ToArray(), false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public (string, string, DataTable) Supplier_Stock_Get_From_His_WEB_API_AND_FTP(int SupplierId, string APIType, string SupplierResponseFormat, string SupplierURL, string SupplierAPIMethod, string UserName, string Password, string FileLocation)
        {
            DataTable dt_APIRes = new DataTable();
            string address = string.Empty;

            ConvertJsonStringToDataTable jDt = new ConvertJsonStringToDataTable();
            ConvertStringArrayToDatatable saDt = new ConvertStringArrayToDatatable();
            ConvertJsonObjectToDataTable jodt = new ConvertJsonObjectToDataTable();
            try
            {
                if (APIType == "FTP")
                {
                    string str2 = Path.GetExtension(FileLocation).ToLower();
                    if (str2 == ".xls")
                    {
                        try
                        {
                            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileLocation + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
                            //dt_APIRes = ConvertXLStoDataTable("", connString);
                            dt_APIRes = Convert_FILE_To_DataTable(".xls", connString, "", SupplierId);
                        }
                        catch (Exception ex)
                        {
                            return ("Import Data from .xls File", ex.Message, null);
                        }
                    }
                    else if (str2 == ".xlsx")
                    {
                        try
                        {
                            //string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileLocation + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";
                            //dt_APIRes = ConvertXSLXtoDataTable("", connString);
                            dt_APIRes = Convert_FILE_To_DataTable(".xlsx", connString, "", SupplierId);
                        }
                        catch (Exception ex)
                        {
                            return ("Import Data from .xlsx File", ex.Message, null);
                        }
                    }
                    else if (str2 == ".csv")
                    {
                        try
                        {
                            //dt_APIRes = ConvertCSVtoDataTable(FileLocation);
                            dt_APIRes = Convert_FILE_To_DataTable(".csv", FileLocation, "", SupplierId);
                        }
                        catch (Exception ex)
                        {
                            return ("Import Data from .csv File", ex.Message, null);
                        }
                    }
                }
                else
                {
                    address = SupplierURL;
                    string[] strArray = address.Split('?');
                    string data = string.Empty;
                    if (strArray.Length == 2)
                    {
                        data = strArray[1].ToString();
                    }
                    string tempPath = FileLocation,
                           _API = String.Empty, filename = String.Empty, filefullpath = String.Empty;

                    if (SupplierResponseFormat.ToUpper() == "XML")
                    {
                        WebClient client = new WebClient();
                        client.Headers["Content-type"] = "application/x-www-form-urlencoded";
                        client.Encoding = Encoding.UTF8;
                        ServicePointManager.Expect100Continue = false;
                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                        if (SupplierURL.ToUpper() == "HTTP://61.93.195.114/KRISHDIAMONDSERVICE/KRISHDIAMONDSERVICE.ASMX/GETSTONEDATA?USERNAME=SUNRISE&PASSWORD=04321")
                        {
                            try
                            {
                                _API = SupplierURL;
                                string[] words = _API.Split('?');
                                String InputPara = string.Empty;
                                if (words.Length == 2)
                                {
                                    InputPara = words[1].ToString();
                                }

                                string xml = client.UploadString(_API, InputPara);
                                client.Dispose();

                                ConvertXmlStringToDataTable xDt = new ConvertXmlStringToDataTable();
                                XmlDocument doc = new XmlDocument();
                                doc.LoadXml(xml);
                                XmlElement root = doc.DocumentElement;
                                XmlNodeList elemList = root.GetElementsByTagName("Row");

                                if (elemList.Count > 0)
                                {
                                    dt_APIRes = xDt.ConvertXmlNodeListToDataTable(elemList);
                                }
                                else
                                {
                                    return ("Data not Found", string.Empty, null);
                                }
                            }
                            catch (Exception ex)
                            {
                                return ("API Not Working", ex.Message, null);
                            }
                        }
                    }
                    else if (SupplierResponseFormat.ToUpper() == "JSON")
                    {
                        if (SupplierAPIMethod.ToUpper() == "POST")
                        {
                            string json = string.Empty, Token = string.Empty;
                            _API = SupplierURL;
                            string[] words = _API.Split('?');
                            String InputPara = string.Empty;
                            if (words.Length == 2)
                            {
                                InputPara = words[1].ToString();
                            }

                            if (SupplierURL.ToUpper() == "HTTPS://SUNRISEDIAMONDS.COM.HK:8122/API/STOCK/STOCK_GET")
                            {
                                string inputJson = string.Empty;
                                try
                                {
                                    string Name = UserName;
                                    string password = Password;

                                    Sunrise_LoginRequest sun_l = new Sunrise_LoginRequest();
                                    sun_l.UserName = Name;
                                    sun_l.Password = password;
                                    sun_l.grant_type = "password";

                                    String InputLRJson = string.Join("&", sun_l.GetType()
                                                                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                                .Where(p => p.GetValue(sun_l, null) != null)
                                        .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(sun_l).ToString())}"));

                                    //WebClient client = new WebClient();
                                    //client.Headers["Content-type"] = "application/x-www-form-urlencoded";
                                    //client.Encoding = Encoding.UTF8;
                                    //ServicePointManager.Expect100Continue = false;
                                    //ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                    //json = client.UploadString("https://sunrisediamonds.com.hk:8122/api/User/Login", InputLRJson);

                                    //WebClient client = new WebClient();
                                    ////client.Headers.Add("Content-type", "application/json");
                                    //client.Encoding = Encoding.UTF8;
                                    //json = client.UploadString("https://sunrisediamonds.com.hk:8122/api/User/Login", "POST", InputLRJson);
                                    //client.Dispose();

                                    WebRequest request = WebRequest.Create("https://sunrisediamonds.com.hk:8122/api/User/Login");
                                    request.Method = "POST";
                                    request.Timeout = 7200000; //2 Hour in milliseconds
                                    byte[] byteArray = Encoding.UTF8.GetBytes(InputLRJson);
                                    request.ContentType = "application/x-www-form-urlencoded";
                                    //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                    request.ContentLength = byteArray.Length;

                                    //Here is the Business end of the code...
                                    Stream dataStream = request.GetRequestStream();
                                    dataStream.Write(byteArray, 0, byteArray.Length);
                                    dataStream.Close();

                                    //and here is the response.
                                    WebResponse response = request.GetResponse();

                                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                    dataStream = response.GetResponseStream();
                                    StreamReader reader = new StreamReader(dataStream);
                                    json = reader.ReadToEnd();
                                    Console.WriteLine(json);
                                    reader.Close();
                                    dataStream.Close();
                                    response.Close();
                                    request.Abort();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                Sunrise_LoginResponse sglr = new Sunrise_LoginResponse();


                                try
                                {
                                    sglr = (new JavaScriptSerializer()).Deserialize<Sunrise_LoginResponse>(json);

                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }

                                if (!string.IsNullOrEmpty(sglr.access_token))
                                {

                                    try
                                    {
                                        //SGStockRequest sgr = new SGStockRequest();
                                        //sgr.UserId = sglr.UserId;
                                        //sgr.TokenId = sglr.TokenId;

                                        //String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                        //WebClient client1 = new WebClient();
                                        //client1.Headers.Add("Content-type", "application/json");
                                        //client1.Encoding = Encoding.UTF8;
                                        //json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataIndia", "POST", InputSRJson);
                                        //client1.Dispose();

                                        WebRequest request1 = WebRequest.Create("https://sunrisediamonds.com.hk:8122/api/Stock/Stock_GET");
                                        request1.Method = "POST";
                                        request1.Timeout = 7200000; //2 Hour in milliseconds
                                        //byte[] byteArray1 = Encoding.UTF8.GetBytes(InputSRJson);
                                        //request1.ContentType = "application/json";
                                        //request1.ContentType = "application/x-www-form-urlencoded";
                                        request1.Headers.Add("Authorization", "Bearer " + sglr.access_token);
                                        //request1.ContentLength = byteArray1.Length;

                                        //Here is the Business end of the code...
                                        Stream dataStream1 = request1.GetRequestStream();
                                        //dataStream1.Write(byteArray1, 0, byteArray1.Length);
                                        dataStream1.Close();

                                        //and here is the response.
                                        WebResponse response1 = request1.GetResponse();

                                        Console.WriteLine(((HttpWebResponse)response1).StatusDescription);
                                        dataStream1 = response1.GetResponseStream();
                                        StreamReader reader1 = new StreamReader(dataStream1);
                                        json = reader1.ReadToEnd();
                                        Console.WriteLine(json);
                                        reader1.Close();
                                        dataStream1.Close();
                                        response1.Close();
                                        request1.Abort();
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("API Not Working", ex.Message, null);
                                    }

                                    try
                                    {
                                        JObject o = JObject.Parse(json);
                                        var t = string.Empty;
                                        if (o != null)
                                        {
                                            var test = o;
                                            if (test != null)
                                            {
                                                var test2 = test;
                                                if (test2 != null)
                                                {
                                                    Console.Write(test2);
                                                    t = test2.Root.First.First.ToString();
                                                }
                                            }
                                        }
                                        var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        json = JsonConvert.SerializeObject(json_1);
                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");

                                        if (!string.IsNullOrEmpty(json))
                                        {
                                            dt_APIRes = jDt.JsonStringToDataTable(json);
                                        }
                                        else
                                        {
                                            return ("Data not Found", string.Empty, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("Data Response Format Changed", ex.Message, null);
                                    }
                                }
                                else
                                {
                                    return ("Token is not Exists", string.Empty, null);
                                }

                            }
                            else if (SupplierURL.ToUpper() == "HTTP://WWW.JPDIAM.COM/PLUGIN/APITOOL")
                            {
                                WebClient client = new WebClient();
                                string inputJson = string.Empty;
                                try
                                {
                                    JP_Login_Req lgn_req = new JP_Login_Req();
                                    lgn_req.action = "viplogin";
                                    lgn_req.vipid = "sunrise";
                                    //lgn_req.vippsd = "goodluck";
                                    lgn_req.vippsd = "Sunrise@1041";

                                    inputJson = string.Join("&", lgn_req.GetType()
                                                                                    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                                                    .Where(p => p.GetValue(lgn_req, null) != null)
                                                           .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(lgn_req).ToString())}"));


                                    client = new WebClient();
                                    client.Headers["Content-type"] = "application/x-www-form-urlencoded";
                                    client.Encoding = Encoding.UTF8;
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                    json = client.UploadString("https://www.jpdiam.com/plugin/apitool", inputJson);
                                    client.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    JP_Login_Res lgn_res = new JP_Login_Res();
                                    lgn_res = (new JavaScriptSerializer()).Deserialize<JP_Login_Res>(json);
                                    Token = lgn_res.msgdata.token;
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }

                                if (!string.IsNullOrEmpty(Token))
                                {
                                    try
                                    {
                                        JP_Stock_Req stk_req = new JP_Stock_Req();
                                        stk_req.action = "queryalldiamondstock";
                                        stk_req.token = Token;
                                        stk_req.ispaged = 0;
                                        stk_req.pageindex = 1;

                                        inputJson = string.Join("&", stk_req.GetType()
                                                                              .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                                              .Where(p => p.GetValue(stk_req, null) != null)
                                                     .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(stk_req).ToString())}"));

                                        client = new WebClient();
                                        client.Headers["Content-type"] = "application/x-www-form-urlencoded";
                                        client.Encoding = Encoding.UTF8;
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                        json = client.UploadString("https://www.jpdiam.com/plugin/apitool", inputJson);
                                        client.Dispose();
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("API Not Working", ex.Message, null);
                                    }

                                    try
                                    {
                                        JObject o = JObject.Parse(json);
                                        var t = string.Empty;
                                        if (o != null)
                                        {
                                            var test = o.Last;
                                            if (test != null)
                                            {
                                                var test2 = test.Last.Last;
                                                if (test2 != null)
                                                {
                                                    t = test2.First.ToString();
                                                }
                                            }
                                        }
                                        var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        json = JsonConvert.SerializeObject(json_1);

                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");

                                        if (!string.IsNullOrEmpty(json))
                                        {
                                            dt_APIRes = jDt.JsonStringToDataTable(json);
                                        }
                                        else
                                        {
                                            return ("Data not Found", string.Empty, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("Data Response Format Changed", ex.Message, null);
                                    }
                                }
                                else
                                {
                                    return ("Token is not Exists", string.Empty, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTPS://API1.ANKITGEMS.COM:4443/APIUSER/LOGINCHECK")
                            {
                                try
                                {
                                    string Name = UserName;
                                    string password = Password;

                                    WebRequest request = WebRequest.Create("https://api1.ankitgems.com:4443/apiuser/logincheck?Name=" + Name + "&password=" + password);
                                    request.Method = "POST";
                                    request.Timeout = 7200000; //2 Hour in milliseconds
                                    byte[] byteArray = Encoding.UTF8.GetBytes(InputPara);
                                    request.ContentType = "application/x-www-form-urlencoded";
                                    //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                    request.ContentLength = byteArray.Length;

                                    //Here is the Business end of the code...
                                    Stream dataStream = request.GetRequestStream();
                                    dataStream.Write(byteArray, 0, byteArray.Length);
                                    dataStream.Close();

                                    //and here is the response.
                                    WebResponse response = request.GetResponse();

                                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                    dataStream = response.GetResponseStream();
                                    StreamReader reader = new StreamReader(dataStream);
                                    json = reader.ReadToEnd();
                                    Console.WriteLine(json);
                                    reader.Close();
                                    dataStream.Close();
                                    response.Close();
                                    request.Abort();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    AnkitGems _data = new AnkitGems();
                                    _data = (new JavaScriptSerializer()).Deserialize<AnkitGems>(json);
                                    Token = _data.data.accessToken;
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }

                                if (!string.IsNullOrEmpty(Token))
                                {
                                    try
                                    {
                                        WebRequest request1 = WebRequest.Create("https://api1.ankitgems.com:4443/apistock/stockdetail?page=1&limit=100000");
                                        request1.Method = "POST";
                                        request1.Timeout = 7200000; //2 Hour in milliseconds
                                        byte[] byteArray1 = Encoding.UTF8.GetBytes(InputPara);
                                        request1.ContentType = "application/x-www-form-urlencoded";
                                        request1.Headers.Add("Authorization", "Bearer " + Token);
                                        request1.ContentLength = byteArray1.Length;

                                        //Here is the Business end of the code...
                                        Stream dataStream1 = request1.GetRequestStream();
                                        dataStream1.Write(byteArray1, 0, byteArray1.Length);
                                        dataStream1.Close();

                                        //and here is the response.
                                        WebResponse response1 = request1.GetResponse();

                                        Console.WriteLine(((HttpWebResponse)response1).StatusDescription);
                                        dataStream1 = response1.GetResponseStream();
                                        StreamReader reader1 = new StreamReader(dataStream1);
                                        json = reader1.ReadToEnd();
                                        Console.WriteLine(json);
                                        reader1.Close();
                                        dataStream1.Close();
                                        response1.Close();
                                        request1.Abort();
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("API Not Working", ex.Message, null);
                                    }

                                    try
                                    {
                                        JObject o = JObject.Parse(json);
                                        var t = string.Empty;
                                        if (o != null)
                                        {
                                            var test = o.First;
                                            if (test != null)
                                            {
                                                var test2 = test.First;
                                                if (test2 != null)
                                                {
                                                    Console.Write(test2);
                                                    t = test2.Root.Last.First.First.First.ToString();
                                                }
                                            }
                                        }
                                        var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        json = JsonConvert.SerializeObject(json_1);
                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");

                                        if (!string.IsNullOrEmpty(json))
                                        {
                                            dt_APIRes = jDt.JsonStringToDataTable(json);
                                        }
                                        else
                                        {
                                            return ("Data not Found", string.Empty, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("Data Response Format Changed", ex.Message, null);
                                    }
                                }
                                else
                                {
                                    return ("Token is not Exists", string.Empty, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYERV2/GETSTOCKDATA")
                            {

                                string Name = UserName;
                                string password = Password;
                                string inputJson = "";
                                try
                                {
                                    SGLoginRequest l_req = new SGLoginRequest();
                                    l_req.UserName = UserName; // "samit_gandhi";
                                    l_req.Password = Password; // "missme@hk123";

                                    inputJson = JsonConvert.SerializeObject(l_req);

                                    WebClient client = new WebClient();
                                    client.Headers.Add("Content-type", "application/json");
                                    client.Encoding = Encoding.UTF8;
                                    json = client.UploadString("https://shairugems.net:8011/api/buyerv2/login", "POST", inputJson);
                                    client.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                SGLoginResponse l_res = new SGLoginResponse();
                                try
                                {
                                    l_res = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }


                                if (!string.IsNullOrEmpty(l_res.TokenId) && !string.IsNullOrEmpty(l_res.UserId))
                                {

                                    try
                                    {
                                        SGStockRequest stock_req = new SGStockRequest();
                                        stock_req.UserId = l_res.UserId;
                                        stock_req.TokenId = l_res.TokenId;
                                        //stock_req.StoneId = StoneId;

                                        inputJson = JsonConvert.SerializeObject(stock_req);

                                        WebClient client1 = new WebClient();
                                        client1.Headers.Add("Content-type", "application/json");
                                        client1.Encoding = Encoding.UTF8;
                                        json = client1.UploadString("https://shairugems.net:8011/api/buyerv2/getstockdata", "POST", inputJson);
                                        client1.Dispose();
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("API Not Working", ex.Message, null);
                                    }

                                    try
                                    {
                                        JObject o = JObject.Parse(json);
                                        var t = string.Empty;
                                        if (o != null)
                                        {
                                            var test = o;
                                            if (test != null)
                                            {
                                                var test2 = test;
                                                if (test2 != null)
                                                {
                                                    Console.Write(test2);
                                                    t = test2.Root.First.First.ToString();
                                                }
                                            }
                                        }
                                        var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                        json = JsonConvert.SerializeObject(json_1);
                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");



                                        //var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                        //var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                        ////json_1=json_1.r
                                        //json = JsonConvert.SerializeObject(json_1.Data, settings);
                                        //json = json.Replace("[", "").Replace("]", "");
                                        //json = json.Replace("null", "");

                                        if (!string.IsNullOrEmpty(json))
                                        {
                                            dt_APIRes = jDt.JsonStringToDataTable(json);
                                        }
                                        else
                                        {
                                            return ("Data not Found", string.Empty, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("Data Response Format Changed", ex.Message, null);
                                    }
                                }
                                else
                                {
                                    return ("Token is not Exists", string.Empty, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATA")
                            {
                                try
                                {
                                    string Name = UserName;
                                    string password = Password;

                                    SGLoginRequest sgl = new SGLoginRequest();
                                    sgl.UserName = Name;
                                    sgl.Password = password;

                                    String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                                    //WebClient client = new WebClient();
                                    //client.Headers.Add("Content-type", "application/json");
                                    //client.Encoding = Encoding.UTF8;
                                    //json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);
                                    //client.Dispose();

                                    WebRequest request = WebRequest.Create("https://shairugems.net:8011/api/Buyer/login");
                                    request.Method = "POST";
                                    request.Timeout = 7200000; //2 Hour in milliseconds
                                    byte[] byteArray = Encoding.UTF8.GetBytes(InputLRJson);
                                    //request.ContentType = "application/x-www-form-urlencoded";
                                    request.ContentType = "application/json";
                                    //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                    request.ContentLength = byteArray.Length;

                                    //Here is the Business end of the code...
                                    Stream dataStream = request.GetRequestStream();
                                    dataStream.Write(byteArray, 0, byteArray.Length);
                                    dataStream.Close();

                                    //and here is the response.
                                    WebResponse response = request.GetResponse();

                                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                    dataStream = response.GetResponseStream();
                                    StreamReader reader = new StreamReader(dataStream);
                                    json = reader.ReadToEnd();
                                    Console.WriteLine(json);
                                    reader.Close();
                                    dataStream.Close();
                                    response.Close();
                                    request.Abort();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                SGLoginResponse sglr = new SGLoginResponse();


                                try
                                {
                                    sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }

                                if (!string.IsNullOrEmpty(sglr.TokenId))
                                {

                                    try
                                    {
                                        SGStockRequest sgr = new SGStockRequest();
                                        sgr.UserId = sglr.UserId;
                                        sgr.TokenId = sglr.TokenId;

                                        String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                        //WebClient client1 = new WebClient();
                                        //client1.Headers.Add("Content-type", "application/json");
                                        //client1.Encoding = Encoding.UTF8;
                                        //json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockData", "POST", InputSRJson);
                                        //client1.Dispose();

                                        WebRequest request1 = WebRequest.Create("https://shairugems.net:8011/api/Buyer/GetStockData");
                                        request1.Method = "POST";
                                        request1.Timeout = 7200000; //2 Hour in milliseconds
                                        byte[] byteArray1 = Encoding.UTF8.GetBytes(InputSRJson);
                                        request1.ContentType = "application/json";
                                        //request1.ContentType = "application/x-www-form-urlencoded";
                                        //request1.Headers.Add("Authorization", "Bearer " + Token);
                                        request1.ContentLength = byteArray1.Length;

                                        //Here is the Business end of the code...
                                        Stream dataStream1 = request1.GetRequestStream();
                                        dataStream1.Write(byteArray1, 0, byteArray1.Length);
                                        dataStream1.Close();

                                        //and here is the response.
                                        WebResponse response1 = request1.GetResponse();

                                        Console.WriteLine(((HttpWebResponse)response1).StatusDescription);
                                        dataStream1 = response1.GetResponseStream();
                                        StreamReader reader1 = new StreamReader(dataStream1);
                                        json = reader1.ReadToEnd();
                                        Console.WriteLine(json);
                                        reader1.Close();
                                        dataStream1.Close();
                                        response1.Close();
                                        request1.Abort();
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("API Not Working", ex.Message, null);
                                    }

                                    try
                                    {
                                        var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                        var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                        //json_1=json_1.r
                                        json = JsonConvert.SerializeObject(json_1.Data, settings);
                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");

                                        if (!string.IsNullOrEmpty(json))
                                        {
                                            dt_APIRes = jDt.JsonStringToDataTable(json);
                                        }
                                        else
                                        {
                                            return ("Data not Found", string.Empty, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("Data Response Format Changed", ex.Message, null);
                                    }
                                }
                                else
                                {
                                    return ("Token is not Exists", string.Empty, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATAINDIA")
                            {
                                try
                                {
                                    string Name = UserName;
                                    string password = Password;

                                    SGLoginRequest sgl = new SGLoginRequest();
                                    sgl.UserName = Name;
                                    sgl.Password = password;

                                    String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                                    //WebClient client = new WebClient();
                                    //client.Headers.Add("Content-type", "application/json");
                                    //client.Encoding = Encoding.UTF8;
                                    //json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);
                                    //client.Dispose();

                                    WebRequest request = WebRequest.Create("https://shairugems.net:8011/api/Buyer/login");
                                    request.Method = "POST";
                                    request.Timeout = 7200000; //2 Hour in milliseconds
                                    byte[] byteArray = Encoding.UTF8.GetBytes(InputLRJson);
                                    //request.ContentType = "application/x-www-form-urlencoded";
                                    request.ContentType = "application/json";
                                    //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                    request.ContentLength = byteArray.Length;

                                    //Here is the Business end of the code...
                                    Stream dataStream = request.GetRequestStream();
                                    dataStream.Write(byteArray, 0, byteArray.Length);
                                    dataStream.Close();

                                    //and here is the response.
                                    WebResponse response = request.GetResponse();

                                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                    dataStream = response.GetResponseStream();
                                    StreamReader reader = new StreamReader(dataStream);
                                    json = reader.ReadToEnd();
                                    Console.WriteLine(json);
                                    reader.Close();
                                    dataStream.Close();
                                    response.Close();
                                    request.Abort();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                SGLoginResponse sglr = new SGLoginResponse();


                                try
                                {
                                    sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }

                                if (!string.IsNullOrEmpty(sglr.TokenId))
                                {

                                    try
                                    {
                                        SGStockRequest sgr = new SGStockRequest();
                                        sgr.UserId = sglr.UserId;
                                        sgr.TokenId = sglr.TokenId;

                                        String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                        //WebClient client1 = new WebClient();
                                        //client1.Headers.Add("Content-type", "application/json");
                                        //client1.Encoding = Encoding.UTF8;
                                        //json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataIndia", "POST", InputSRJson);
                                        //client1.Dispose();

                                        WebRequest request1 = WebRequest.Create("https://shairugems.net:8011/api/Buyer/GetStockDataIndia");
                                        request1.Method = "POST";
                                        request1.Timeout = 7200000; //2 Hour in milliseconds
                                        byte[] byteArray1 = Encoding.UTF8.GetBytes(InputSRJson);
                                        request1.ContentType = "application/json";
                                        //request1.ContentType = "application/x-www-form-urlencoded";
                                        //request1.Headers.Add("Authorization", "Bearer " + Token);
                                        request1.ContentLength = byteArray1.Length;

                                        //Here is the Business end of the code...
                                        Stream dataStream1 = request1.GetRequestStream();
                                        dataStream1.Write(byteArray1, 0, byteArray1.Length);
                                        dataStream1.Close();

                                        //and here is the response.
                                        WebResponse response1 = request1.GetResponse();

                                        Console.WriteLine(((HttpWebResponse)response1).StatusDescription);
                                        dataStream1 = response1.GetResponseStream();
                                        StreamReader reader1 = new StreamReader(dataStream1);
                                        json = reader1.ReadToEnd();
                                        Console.WriteLine(json);
                                        reader1.Close();
                                        dataStream1.Close();
                                        response1.Close();
                                        request1.Abort();
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("API Not Working", ex.Message, null);
                                    }

                                    try
                                    {
                                        var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                        var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                        //json_1=json_1.r
                                        json = JsonConvert.SerializeObject(json_1.Data, settings);
                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");

                                        if (!string.IsNullOrEmpty(json))
                                        {
                                            dt_APIRes = jDt.JsonStringToDataTable(json);
                                        }
                                        else
                                        {
                                            return ("Data not Found", string.Empty, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("Data Response Format Changed", ex.Message, null);
                                    }
                                }
                                else
                                {
                                    return ("Token is not Exists", string.Empty, null);
                                }



                                //try
                                //{
                                //    string Name = UserName;
                                //    string password = Password;

                                //    SGLoginRequest sgl = new SGLoginRequest();
                                //    sgl.UserName = Name;
                                //    sgl.Password = password;

                                //    String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                                //    WebClient client = new WebClient();
                                //    client.Headers.Add("Content-type", "application/json");
                                //    client.Encoding = Encoding.UTF8;
                                //    json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);
                                //    client.Dispose();
                                //}
                                //catch (Exception ex)
                                //{
                                //    return ("API Not Working", ex.Message, null);
                                //}

                                //SGLoginResponse sglr = new SGLoginResponse();
                                //try
                                //{
                                //    sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);
                                //}
                                //catch (Exception ex)
                                //{
                                //    return ("Data Response Format Changed", ex.Message, null);
                                //}

                                //if (!string.IsNullOrEmpty(sglr.TokenId))
                                //{
                                //    try
                                //    {
                                //        SGStockRequest sgr = new SGStockRequest();
                                //        sgr.UserId = sglr.UserId;
                                //        sgr.TokenId = sglr.TokenId;

                                //        String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                //        WebClient client1 = new WebClient();
                                //        client1.Headers.Add("Content-type", "application/json");
                                //        client1.Encoding = Encoding.UTF8;
                                //        json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataIndia", "POST", InputSRJson);
                                //        client1.Dispose();
                                //    }
                                //    catch (Exception ex)
                                //    {
                                //        return ("API Not Working", ex.Message, null);
                                //    }

                                //    try
                                //    {
                                //        var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                //        var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                //        //json_1=json_1.r
                                //        json = JsonConvert.SerializeObject(json_1.Data, settings);
                                //        json = json.Replace("[", "").Replace("]", "");
                                //        json = json.Replace("null", "");

                                //        if (!string.IsNullOrEmpty(json))
                                //        {
                                //            dt_APIRes = jDt.JsonStringToDataTable(json);
                                //        }
                                //        else
                                //        {
                                //            return ("Data not Found", string.Empty, null);
                                //        }
                                //    }
                                //    catch (Exception ex)
                                //    {
                                //        return ("Data Response Format Changed", ex.Message, null);
                                //    }
                                //}
                                //else
                                //{
                                //    return ("Token is not Exists", string.Empty, null);
                                //}
                            }
                            else if (SupplierURL.ToUpper() == "HTTPS://SHAIRUGEMS.NET:8011/API/BUYER/GETSTOCKDATADUBAI")
                            {
                                try
                                {
                                    string Name = UserName;
                                    string password = Password;

                                    SGLoginRequest sgl = new SGLoginRequest();
                                    sgl.UserName = Name;
                                    sgl.Password = password;

                                    String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                                    //WebClient client = new WebClient();
                                    //client.Headers.Add("Content-type", "application/json");
                                    //client.Encoding = Encoding.UTF8;
                                    //json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);
                                    //client.Dispose();

                                    WebRequest request = WebRequest.Create("https://shairugems.net:8011/api/Buyer/login");
                                    request.Method = "POST";
                                    request.Timeout = 7200000; //2 Hour in milliseconds
                                    byte[] byteArray = Encoding.UTF8.GetBytes(InputLRJson);
                                    //request.ContentType = "application/x-www-form-urlencoded";
                                    request.ContentType = "application/json";
                                    //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                    request.ContentLength = byteArray.Length;

                                    //Here is the Business end of the code...
                                    Stream dataStream = request.GetRequestStream();
                                    dataStream.Write(byteArray, 0, byteArray.Length);
                                    dataStream.Close();

                                    //and here is the response.
                                    WebResponse response = request.GetResponse();

                                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                    dataStream = response.GetResponseStream();
                                    StreamReader reader = new StreamReader(dataStream);
                                    json = reader.ReadToEnd();
                                    Console.WriteLine(json);
                                    reader.Close();
                                    dataStream.Close();
                                    response.Close();
                                    request.Abort();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                SGLoginResponse sglr = new SGLoginResponse();


                                try
                                {
                                    sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);

                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }

                                if (!string.IsNullOrEmpty(sglr.TokenId))
                                {

                                    try
                                    {
                                        SGStockRequest sgr = new SGStockRequest();
                                        sgr.UserId = sglr.UserId;
                                        sgr.TokenId = sglr.TokenId;

                                        String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                        //WebClient client1 = new WebClient();
                                        //client1.Headers.Add("Content-type", "application/json");
                                        //client1.Encoding = Encoding.UTF8;
                                        //json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataDubai", "POST", InputSRJson);
                                        //client1.Dispose();

                                        WebRequest request1 = WebRequest.Create("https://shairugems.net:8011/api/Buyer/GetStockDataDubai");
                                        request1.Method = "POST";
                                        request1.Timeout = 7200000; //2 Hour in milliseconds
                                        byte[] byteArray1 = Encoding.UTF8.GetBytes(InputSRJson);
                                        request1.ContentType = "application/json";
                                        //request1.ContentType = "application/x-www-form-urlencoded";
                                        //request1.Headers.Add("Authorization", "Bearer " + Token);
                                        request1.ContentLength = byteArray1.Length;

                                        //Here is the Business end of the code...
                                        Stream dataStream1 = request1.GetRequestStream();
                                        dataStream1.Write(byteArray1, 0, byteArray1.Length);
                                        dataStream1.Close();

                                        //and here is the response.
                                        WebResponse response1 = request1.GetResponse();

                                        Console.WriteLine(((HttpWebResponse)response1).StatusDescription);
                                        dataStream1 = response1.GetResponseStream();
                                        StreamReader reader1 = new StreamReader(dataStream1);
                                        json = reader1.ReadToEnd();
                                        Console.WriteLine(json);
                                        reader1.Close();
                                        dataStream1.Close();
                                        response1.Close();
                                        request1.Abort();
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("API Not Working", ex.Message, null);
                                    }

                                    try
                                    {
                                        var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                        var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                        //json_1=json_1.r
                                        json = JsonConvert.SerializeObject(json_1.Data, settings);
                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");

                                        if (!string.IsNullOrEmpty(json))
                                        {
                                            dt_APIRes = jDt.JsonStringToDataTable(json);
                                        }
                                        else
                                        {
                                            return ("Data not Found", string.Empty, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("Data Response Format Changed", ex.Message, null);
                                    }
                                }
                                else
                                {
                                    return ("Token is not Exists", string.Empty, null);
                                }



                                //try
                                //{
                                //    string Name = UserName;
                                //    string password = Password;

                                //    SGLoginRequest sgl = new SGLoginRequest();
                                //    sgl.UserName = Name;
                                //    sgl.Password = password;

                                //    String InputLRJson = (new JavaScriptSerializer()).Serialize(sgl);

                                //    WebClient client = new WebClient();
                                //    client.Headers.Add("Content-type", "application/json");
                                //    client.Encoding = Encoding.UTF8;
                                //    json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);
                                //    client.Dispose();
                                //}
                                //catch (Exception ex)
                                //{
                                //    return ("API Not Working", ex.Message, null);
                                //}

                                //SGLoginResponse sglr = new SGLoginResponse();
                                //try
                                //{
                                //    sglr = (new JavaScriptSerializer()).Deserialize<SGLoginResponse>(json);
                                //}
                                //catch (Exception ex)
                                //{
                                //    return ("Data Response Format Changed", ex.Message, null);
                                //}

                                //if (!string.IsNullOrEmpty(sglr.TokenId))
                                //{
                                //    try
                                //    {
                                //        SGStockRequest sgr = new SGStockRequest();
                                //        sgr.UserId = sglr.UserId;
                                //        sgr.TokenId = sglr.TokenId;

                                //        String InputSRJson = (new JavaScriptSerializer()).Serialize(sgr);

                                //        WebClient client1 = new WebClient();
                                //        client1.Headers.Add("Content-type", "application/json");
                                //        client1.Encoding = Encoding.UTF8;
                                //        json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataDubai", "POST", InputSRJson);
                                //        client1.Dispose();
                                //    }
                                //    catch (Exception ex)
                                //    {
                                //        return ("API Not Working", ex.Message, null);
                                //    }

                                //    try
                                //    {
                                //        var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                                //        var json_1 = JsonConvert.DeserializeObject<SGStockResponse>(json, settings);

                                //        //json_1=json_1.r
                                //        json = JsonConvert.SerializeObject(json_1.Data, settings);
                                //        json = json.Replace("[", "").Replace("]", "");
                                //        json = json.Replace("null", "");

                                //        if (!string.IsNullOrEmpty(json))
                                //        {
                                //            dt_APIRes = jDt.JsonStringToDataTable(json);
                                //        }
                                //        else
                                //        {
                                //            return ("Data not Found", string.Empty, null);
                                //        }
                                //    }
                                //    catch (Exception ex)
                                //    {
                                //        return ("Data Response Format Changed", ex.Message, null);
                                //    }
                                //}
                                //else
                                //{
                                //    return ("Token is not Exists", string.Empty, null);
                                //}
                            }
                            else if (SupplierURL.ToUpper() == "HTTP://PDHK.DIAMX.NET/API/STOCKSEARCH?APITOKEN=3C0DB41E-7B79-48C4-8CBD-1F718DB7263A")
                            {
                                try
                                {
                                    WebRequest request = WebRequest.Create("http://pdhk.diamx.net/API/StockSearch?APIToken=3c0db41e-7b79-48c4-8cbd-1f718db7263a");
                                    request.Method = "POST";
                                    request.Timeout = 7200000; //2 Hour in milliseconds
                                    byte[] byteArray = Encoding.UTF8.GetBytes("");
                                    request.ContentType = "application/json";
                                    //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                    request.ContentLength = byteArray.Length;

                                    //Here is the Business end of the code...
                                    Stream dataStream = request.GetRequestStream();
                                    dataStream.Write(byteArray, 0, byteArray.Length);
                                    dataStream.Close();

                                    //and here is the response.
                                    WebResponse response = request.GetResponse();

                                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                    dataStream = response.GetResponseStream();
                                    StreamReader reader = new StreamReader(dataStream);
                                    json = reader.ReadToEnd();
                                    Console.WriteLine(json);
                                    reader.Close();
                                    dataStream.Close();
                                    response.Close();
                                    request.Abort();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    JObject o = JObject.Parse(json);
                                    var t = string.Empty;
                                    if (o != null)
                                    {
                                        var test = o.First;
                                        if (test != null)
                                        {
                                            var test2 = test.First;
                                            if (test2 != null)
                                            {
                                                Console.Write(test2);
                                                t = test2.Root.Last.First.ToString();
                                            }
                                        }
                                    }
                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                    json = JsonConvert.SerializeObject(json_1);
                                    json = json.Replace("[", "").Replace("]", "");
                                    json = json.Replace("null", "");

                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTPS://STOCK.DDPL.COM/DHARAMWEBAPI/API/STOCKDISPAPI/GETDIAMONDDATA")
                            {
                                try
                                {
                                    Dharam _data = new Dharam();
                                    _data.uniqID = 23835;
                                    _data.company = "SUNRISE DIAMONDS LTD";
                                    _data.actCode = "Su@D123#4nd23";
                                    _data.selectAll = "";
                                    _data.StartIndex = 1;
                                    _data.count = 80000;
                                    _data.columns = "";
                                    _data.finder = "";
                                    _data.sort = "";

                                    string inputJson = (new JavaScriptSerializer()).Serialize(_data);

                                    WebRequest request = WebRequest.Create("https://stock.ddpl.com/DharamWebApi/api/stockdispapi/getDiamondData");
                                    request.Method = "POST";
                                    request.Timeout = 7200000; //2 Hour in milliseconds
                                    byte[] byteArray = Encoding.UTF8.GetBytes(inputJson);
                                    request.ContentType = "application/json";
                                    //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                    request.ContentLength = byteArray.Length;

                                    //Here is the Business end of the code...
                                    Stream dataStream = request.GetRequestStream();
                                    dataStream.Write(byteArray, 0, byteArray.Length);
                                    dataStream.Close();

                                    //and here is the response.
                                    WebResponse response = request.GetResponse();

                                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                    dataStream = response.GetResponseStream();
                                    StreamReader reader = new StreamReader(dataStream);
                                    json = reader.ReadToEnd();
                                    Console.WriteLine(json);
                                    reader.Close();
                                    dataStream.Close();
                                    response.Close();
                                    request.Abort();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }
                                try
                                {
                                    JObject o = JObject.Parse(json);
                                    var t = string.Empty;
                                    if (o != null)
                                    {
                                        var test = o.First;
                                        if (test != null)
                                        {
                                            var test2 = test.First;
                                            if (test2 != null)
                                            {
                                                Console.Write(test2);
                                                t = test2.Root.Last.First.ToString();
                                            }
                                        }
                                    }
                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                    json = JsonConvert.SerializeObject(json_1);
                                    json = json.Replace("[", "").Replace("]", "");
                                    json = json.Replace("null", "");

                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTP://KRINALGEMS.DIAMX.NET/API/STOCKSEARCH")
                            {
                                try
                                {
                                    WebRequest request = WebRequest.Create("http://krinalgems.diamx.net/API/StockSearch?APIToken=e161dd39-44ed-4b67-8a48-8406da883892");
                                    request.Method = "POST";
                                    request.Timeout = 7200000; //2 Hour in milliseconds
                                    byte[] byteArray = Encoding.UTF8.GetBytes("");
                                    request.ContentType = "application/json";
                                    //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                    request.ContentLength = byteArray.Length;

                                    //Here is the Business end of the code...
                                    Stream dataStream = request.GetRequestStream();
                                    dataStream.Write(byteArray, 0, byteArray.Length);
                                    dataStream.Close();

                                    //and here is the response.
                                    WebResponse response = request.GetResponse();

                                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                    dataStream = response.GetResponseStream();
                                    StreamReader reader = new StreamReader(dataStream);
                                    json = reader.ReadToEnd();
                                    Console.WriteLine(json);
                                    reader.Close();
                                    dataStream.Close();
                                    response.Close();
                                    request.Abort();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }
                                try
                                {
                                    JObject o = JObject.Parse(json);
                                    var t = string.Empty;
                                    if (o != null)
                                    {
                                        var test = o.First;
                                        if (test != null)
                                        {
                                            var test2 = test.Next.First.ToString();
                                            t = test2;
                                        }
                                    }
                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                    json = JsonConvert.SerializeObject(json_1);
                                    json = json.Replace("[", "").Replace("]", "");
                                    json = json.Replace("null", "");

                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTPS://VAIBHAVGEMS.CO/PROVIDESTOCK.SVC/GETSTOCK")
                            {
                                try
                                {
                                    WebRequest request = WebRequest.Create(_API);
                                    request.Method = "POST";
                                    request.Timeout = 7200000; //2 Hour in milliseconds
                                    byte[] byteArray = Encoding.UTF8.GetBytes(InputPara);
                                    request.ContentType = "application/x-www-form-urlencoded";
                                    //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                    request.ContentLength = byteArray.Length;

                                    //Here is the Business end of the code...
                                    Stream dataStream = request.GetRequestStream();
                                    dataStream.Write(byteArray, 0, byteArray.Length);
                                    dataStream.Close();

                                    //and here is the response.
                                    WebResponse response = request.GetResponse();

                                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                    dataStream = response.GetResponseStream();
                                    StreamReader reader = new StreamReader(dataStream);
                                    json = reader.ReadToEnd();
                                    Console.WriteLine(json);
                                    reader.Close();
                                    dataStream.Close();
                                    response.Close();
                                    request.Abort();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    JObject o = JObject.Parse(json);
                                    var t = string.Empty;
                                    if (o != null)
                                    {
                                        var test = o.First;
                                        if (test != null)
                                        {
                                            var test2 = test.First;
                                            if (test2 != null)
                                            {
                                                Console.Write(test2);
                                                t = test2.First.First.ToString();
                                            }
                                        }
                                    }
                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                    json = JsonConvert.SerializeObject(json_1);
                                    json = json.Replace("[", "").Replace("]", "");
                                    json = json.Replace("null", "");

                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }

                            }
                            else if (SupplierURL.ToUpper() == "HTTP://JODHANI.IN/WEB_SERVICES/JODHANI.ASMX?OP=STOCK_API")
                            {
                                try
                                {
                                    string xml = "<?xml version='1.0' encoding='utf-8'?>" +
                                    "<soap:Envelope xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema' xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/'>" +
                                      "<soap:Body>" +
                                        "<Stock_API xmlns='http://tempuri.org/'>" +
                                          "<user_name>sunrise</user_name>" +
                                          "<password>sun@123</password>" +
                                        "</Stock_API>" +
                                      "</soap:Body>" +
                                    "</soap:Envelope>";

                                    WebRequest request = WebRequest.Create("http://jodhani.in/web_services/jodhani.asmx?op=Stock_API");
                                    request.Method = "POST";
                                    request.Timeout = 7200000; //2 Hour in milliseconds
                                    byte[] byteArray = Encoding.UTF8.GetBytes(xml);
                                    request.ContentType = "text/xml;";
                                    //request.Headers.Add("Authorization", "Bearer " + AccessToken);
                                    request.ContentLength = byteArray.Length;

                                    //Here is the Business end of the code...
                                    Stream dataStream = request.GetRequestStream();
                                    dataStream.Write(byteArray, 0, byteArray.Length);
                                    dataStream.Close();


                                    //and here is the response.
                                    WebResponse response = request.GetResponse();

                                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                    dataStream = response.GetResponseStream();
                                    StreamReader reader = new StreamReader(dataStream);
                                    json = reader.ReadToEnd();
                                    Console.WriteLine(json);
                                    reader.Close();
                                    dataStream.Close();
                                    response.Close();
                                    request.Abort();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    string xml1 = "<?xml version='1.0' encoding='utf-8'?><soap:Envelope xmlns:soap='http://schemas.xmlsoap.org/soap/envelope/' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xmlns:xsd='http://www.w3.org/2001/XMLSchema'><soap:Body><Stock_APIResponse xmlns='http://tempuri.org/' /></soap:Body></soap:Envelope>";
                                    xml1 = xml1.Replace("'", "\"");
                                    json = json.Replace(xml1, "");

                                    JArray arrayList = JArray.Parse(json);

                                    var t1 = arrayList.Last.Last.First.First.Parent.First.Parent.ToString();

                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t1);
                                    json = JsonConvert.SerializeObject(json_1);
                                    json = json.Replace("[", "").Replace("]", "");
                                    json = json.Replace("null", "");

                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTPS://API.EVENUSJEWEL.COM/API/GETSTOCK")
                            {
                                try
                                {
                                    VenusJewel_Login_Req lgn_req = new VenusJewel_Login_Req();
                                    lgn_req.User_Name = "sunriseapi";
                                    lgn_req.Password = "sunriseapi290220";

                                    string inputJson = (new JavaScriptSerializer()).Serialize(lgn_req);

                                    WebRequest request = WebRequest.Create("https://api.evenusjewel.com/api/login");
                                    request.Method = "POST";
                                    request.Timeout = 7200000; //2 Hour in milliseconds
                                    byte[] byteArray = Encoding.UTF8.GetBytes(inputJson);
                                    request.ContentType = "application/json";
                                    request.ContentLength = byteArray.Length;

                                    //Here is the Business end of the code...
                                    Stream dataStream = request.GetRequestStream();
                                    dataStream.Write(byteArray, 0, byteArray.Length);
                                    dataStream.Close();

                                    //and here is the response.
                                    WebResponse response = request.GetResponse();

                                    Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                                    dataStream = response.GetResponseStream();
                                    StreamReader reader = new StreamReader(dataStream);
                                    json = reader.ReadToEnd();
                                    Console.WriteLine(json);
                                    reader.Close();
                                    dataStream.Close();
                                    response.Close();
                                    request.Abort();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                VenusJewel_Login_Res lgn_res = new VenusJewel_Login_Res();
                                try
                                {
                                    lgn_res = (new JavaScriptSerializer()).Deserialize<VenusJewel_Login_Res>(json);
                                    Token = lgn_res.Token_Id;
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }

                                if (!string.IsNullOrEmpty(lgn_res.Token_Id))
                                {
                                    try
                                    {
                                        HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("https://api.evenusjewel.com/api/GetStock");
                                        request1.Method = "GET";
                                        request1.Timeout = 7200000; //2 Hour in milliseconds
                                        request1.ContentType = "application/json";
                                        request1.Headers.Add("Authorization", Token);
                                        request1.Headers.Add("api_version", "Version = 2");

                                        WebResponse response1 = request1.GetResponse();
                                        using (var reader1 = new StreamReader(response1.GetResponseStream()))
                                        {
                                            json = reader1.ReadToEnd();
                                            try
                                            {
                                                JObject o = JObject.Parse(json);
                                                var t = string.Empty;
                                                if (o != null)
                                                {
                                                    var test = o.Last;
                                                    if (test != null)
                                                    {
                                                        var test2 = test.First;
                                                        if (test2 != null)
                                                        {
                                                            t = test2.ToString();
                                                        }
                                                    }
                                                }
                                                var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                                json = JsonConvert.SerializeObject(json_1);
                                                json = json.Replace("[", "").Replace("]", "");
                                                json = json.Replace("null", "");

                                                if (!string.IsNullOrEmpty(json))
                                                {
                                                    dt_APIRes = jDt.JsonStringToDataTable(json);
                                                }
                                                else
                                                {
                                                    return ("Data not Found", string.Empty, null);
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                return ("Data Response Format Changed", ex.Message, null);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("API Not Working", ex.Message, null);
                                    }
                                }
                                else
                                {
                                    return ("Token is not Exists", string.Empty, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTP://KBSHK.FEEDCENTER.NET:7788/TOKEN")
                            {
                                try
                                {
                                    var input = new KBS_LoginRequest
                                    {
                                        grant_type = "password",
                                        username = "ShairuGems",
                                        password = "skT6#4dilkeu&@"

                                    };
                                    string InputPara1 = string.Join("&", input.GetType()
                                                                                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                                                .Where(p => p.GetValue(input, null) != null)
                                                       .Select(p => $"{p.Name}={Uri.EscapeDataString(p.GetValue(input).ToString())}"));

                                    WebClient client = new WebClient();
                                    client.Headers["Content-type"] = "application/x-www-form-urlencoded";
                                    client.Encoding = Encoding.UTF8;
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                    json = client.UploadString("http://kbshk.feedcenter.net:7788/token", InputPara1);
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                KBS_LoginResponse PGLoginRes = new KBS_LoginResponse();
                                try
                                {
                                    PGLoginRes = (new JavaScriptSerializer()).Deserialize<KBS_LoginResponse>(json);
                                    Token = PGLoginRes.access_token;
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }

                                if (!string.IsNullOrEmpty(Token))
                                {
                                    try
                                    {
                                        WebClient client1 = new WebClient();
                                        client1.Headers["Authorization"] = "Bearer " + Token;
                                        client1.Headers["Content-type"] = "application/x-www-form-urlencoded";
                                        client1.Encoding = Encoding.UTF8;
                                        ServicePointManager.Expect100Continue = false;
                                        ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                                        json = client1.DownloadString("http://kbshk.feedcenter.net:7788/api/GetStonesBySIteID/1022");
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("API Not Working", ex.Message, null);
                                    }

                                    try
                                    {
                                        json = json.Replace("[", "").Replace("]", "");

                                        if (!string.IsNullOrEmpty(json))
                                        {
                                            dt_APIRes = jDt.JsonStringToDataTable(json);
                                        }
                                        else
                                        {
                                            return ("Data not Found", string.Empty, null);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        return ("Data Response Format Changed", ex.Message, null);
                                    }
                                }
                                else
                                {
                                    return ("Token is not Exists", string.Empty, null);
                                }
                            }
                        }
                        else
                        {
                            _API = SupplierURL;

                            string json = "";

                            if (SupplierURL.ToUpper() == "HTTPS://WEBSVR.JBBROS.COM/JBAPI.ASPX")
                            {
                                try
                                {
                                    WebClient client = new WebClient();
                                    client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                    json = client.DownloadString("https://websvr.jbbros.com/jbapi.aspx?UserId=SUNRISEDIAMONDS&APIKey=90F2D641-7968-4BB4-BA69-E323F732AF01&Action=FJ&Shape=ALL&CaratFrom=0.01&CaratTo=99.99&Color=ALL&Purity=ALL&Lab=GIA,HRD,IGI");
                                    client.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    json = json.Replace("[", "").Replace("]", "");
                                    json = json.Replace("null", "");

                                    if (!string.IsNullOrEmpty(json) && !json.Contains("Un-Authorize Action - Invalid IpAddress"))
                                    {
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTP://WWW.STARLIGHTDIAMONDS.IN/API/GETSTOCK")
                            {
                                try
                                {
                                    WebClient client = new WebClient();
                                    client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                    json = client.DownloadString("http://www.starlightdiamonds.in/api/getstock?user=sFAnZcJtofnlrU/URZaYnj3R8yeB8nUOxp6LlFMC3X0=&key=EEQMOjwlGGmJSk4P9aRmgmfO6fuhJUU+NPC3UAjYaaI=&type=json");
                                    client.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    json = json.Replace("[", "").Replace("]", "");
                                    json = json.Replace("null", "");
                                    string str = "";

                                    json = json.Replace("<!DOCTYPE html>", "");

                                    str = "<html lang='en'>";
                                    str = str.Replace("'", "\"");
                                    json = json.Replace(str, "");

                                    json = json.Replace("<head><title>", "");
                                    json = json.Replace("</title></head>", "");
                                    json = json.Replace("<body>", "");
                                    json = json.Replace("<body>", "");
                                    json = json.Replace("<body>", "");

                                    str = "<form method='post' action='./getstock?user=sFAnZcJtofnlrU%2fURZaYnj3R8yeB8nUOxp6LlFMC3X0%3d&amp;key=EEQMOjwlGGmJSk4P9aRmgmfO6fuhJUU+NPC3UAjYaaI%3d&amp;type=json' id='form1'>";
                                    str = str.Replace("'", "\"");
                                    json = json.Replace(str, "");

                                    str = "<div class='aspNetHidden'>";
                                    str = str.Replace("'", "\"");
                                    json = json.Replace(str, "");

                                    str = "<input type='hidden' name='__VIEWSTATE' id='__VIEWSTATE' value='/wEPDwULLTE2MTY2ODcyMjlkZLlQNdj1yLtIK3dSkT5LRNbAx4SUWXDdXZ/TLBZck00E' />";
                                    str = str.Replace("'", "\"");
                                    json = json.Replace(str, "");

                                    json = json.Replace("</div>", "");

                                    str = "<div class='aspNetHidden'>";
                                    str = str.Replace("'", "\"");
                                    json = json.Replace(str, "");

                                    str = "<input type='hidden' name='__VIEWSTATEGENERATOR' id='__VIEWSTATEGENERATOR' value='ED942F36' />";
                                    str = str.Replace("'", "\"");
                                    json = json.Replace(str, "");

                                    json = json.Replace("</div>", "");
                                    json = json.Replace("<div>", "");
                                    json = json.Replace("</div>", "");
                                    json = json.Replace("</form>", "");
                                    json = json.Replace("</body>", "");
                                    json = json.Replace("</html>", "");

                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTPS://API.FINESTARDIAMONDS.COM/API/V1/DIAMOND/PAGINATE")
                            {
                                try
                                {
                                    WebClient client = new WebClient();
                                    client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                    json = client.DownloadString("https://api.finestardiamonds.com/api/v1/diamond/paginate?username=list@sunrisediam.com&password=Sunrise1041");
                                    client.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    json = json.Replace("[", "").Replace("]", "");
                                    json = json.Replace("null", "");

                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTP://PB.PROLANCEIT.IN/API/USER/TOKEN")
                            {
                                try
                                {
                                    WebClient client1 = new WebClient();
                                    //client1.Headers.Add("apiKey", "dhfasgdfksdiw232343fsdfchsdkhf==");   //testing key
                                    client1.Headers.Add("apiKey", "Sunadiw4thvihth32hf554u23LtdGerrjschcfnr==");   //live key
                                    client1.Encoding = Encoding.UTF8;
                                    json = client1.DownloadString("http://PB.prolanceit.in/api/user/token");
                                    client1.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }
                                try
                                {
                                    JObject o = JObject.Parse(json);
                                    var token = string.Empty;

                                    if (o != null)
                                    {
                                        token = ((Newtonsoft.Json.Linq.JValue)o.Last.Last).Value.ToString();
                                    }
                                    if (!string.IsNullOrEmpty(token))
                                    {
                                        json = "";
                                        WebClient client2 = new WebClient();
                                        client2.Headers.Add("token", token);
                                        client2.Encoding = Encoding.UTF8;
                                        json = client2.DownloadString("http://PB.prolanceit.in/api/stones");
                                        client2.Dispose();

                                        json = json.Replace("[", "").Replace("]", "");
                                        json = json.Replace("null", "");

                                        if (!string.IsNullOrEmpty(json))
                                        {
                                            dt_APIRes = jDt.JsonStringToDataTable(json);
                                        }
                                        else
                                        {
                                            return ("Data not Found", string.Empty, null);
                                        }
                                    }
                                    else
                                    {
                                        return ("Token is not Exists", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTPS://APICHN.NAROLA.IN/ADMIN/STOCKSHARE/STOCKSHAREAPIRESULT?USERNAME=SUNRISEDIAMONDS&ACCESS_KEY=IXL8-1KGS-SA3C-E6HW-BRBA-IW4G-DSTU")
                            {
                                try
                                {
                                    WebClient client = new WebClient();
                                    client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                    json = client.DownloadString(_API);
                                    client.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    JObject o = JObject.Parse(json);
                                    var t = string.Empty;
                                    if (o != null)
                                    {
                                        t = o.Last.Last.First.Parent.ToString();
                                    }

                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                    json = JsonConvert.SerializeObject(json_1);

                                    json = json.Substring(1, json.Length - 2);

                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTP://WWW.DIAMJOY.COM/API/USER/STOCK/11229/729F7B484FA22A5276B0CDADABC75147/?LANG=EN")
                            {
                                try
                                {
                                    WebClient client = new WebClient();
                                    client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                    json = client.DownloadString(_API);
                                    client.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }
                                try
                                {
                                    JOY _data = (new JavaScriptSerializer()).Deserialize<JOY>(json);

                                    if (_data.rows.Count > 0)
                                    {
                                        dt_APIRes = jodt.StringArrayToDataTable(_data.keys, _data.rows);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTPS://FRONTOFFICEAPI.DIAMANTO.CO/API/CHANNELPARTNER/GETINVENTORY/SUNRISE/SUNRISE@1401")
                            {
                                try
                                {
                                    WebClient client = new WebClient();
                                    client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                    json = client.DownloadString(_API);
                                    client.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }
                                try
                                {
                                    JToken objectData = JToken.Parse(json);
                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(objectData.ToString());
                                    json = JsonConvert.SerializeObject(json_1);
                                    json = json.Replace("[", "").Replace("]", "");

                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTPS://SJWORLDAPI.AZUREWEBSITES.NET/SHARE/SJAPI.ASMX/GETDATA?LOGINNAME=SUNRISE&PASSWORD=SUNRISE321")
                            {
                                try
                                {
                                    WebClient client = new WebClient();
                                    client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                    json = client.DownloadString(_API);
                                    client.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    JObject o = JObject.Parse(json);
                                    var t = string.Empty;
                                    if (o != null)
                                    {
                                        var test = o.First;
                                        if (test != null)
                                        {
                                            var test2 = test.First;
                                            if (test2 != null)
                                            {
                                                Console.Write(test2);
                                                t = o.Last.Last.ToString();
                                            }
                                        }
                                    }
                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                    json = JsonConvert.SerializeObject(json_1);
                                    json = json.Replace("[", "").Replace("]", "");
                                    json = json.Replace("null", "");

                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTPS://API.RRAJESH.CO/API/V1/DIAMOND/PAGINATE?USERNAME=SUNRISE&PASSWORD=SUN@321&PAGE=1&LIMIT=99999")
                            {
                                try
                                {
                                    WebClient client = new WebClient();
                                    client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                    json = client.DownloadString(_API);
                                    client.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    JObject o = JObject.Parse(json);
                                    var t = string.Empty;
                                    if (o != null)
                                    {
                                        var test = o.First;
                                        if (test != null)
                                        {
                                            var test2 = test.Next.First.ToString();
                                            t = test2;
                                        }
                                    }
                                    var json_1 = JsonConvert.DeserializeObject<List<dynamic>>(t);
                                    json = JsonConvert.SerializeObject(json_1);
                                    json = json.Replace("[", "").Replace("]", "");
                                    json = json.Replace("null", "");

                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                            else if (SupplierURL.ToUpper() == "HTTP://ANGELSTAR.HK/ANGELSTAR-STOCK")
                            {
                                try
                                {
                                    WebClient client = new WebClient();
                                    client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                    json = client.DownloadString(_API);
                                    client.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    json = json.Replace("[", "").Replace("]", "");
                                    json = json.Replace("null", "");

                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        dt_APIRes = jDt.JsonStringToDataTable(json);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                            else
                            {
                                try
                                {
                                    WebClient client = new WebClient();
                                    client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                    json = client.DownloadString(_API);
                                    client.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }


                                if (!string.IsNullOrEmpty(json))
                                {
                                    dt_APIRes = jDt.JsonStringToDataTable(json);
                                }
                                else
                                {
                                    return ("Data not Found", string.Empty, null);
                                }
                            }

                        }
                    }
                    else if (SupplierResponseFormat.ToUpper() == "HTML")
                    {
                        if (SupplierAPIMethod.ToUpper() == "GET")
                        {
                            if (SupplierURL.ToUpper() == "HTTPS://WWW.1314PG.COM/API/USER/STOCK/11738/8789AE77D94A9CFB109C1BA5143ABAB6/")
                            {
                                string response = string.Empty;
                                try
                                {
                                    _API = SupplierURL;
                                    WebClient client = new WebClient();
                                    client.Headers["User-Agent"] = @"Mozilla/4.0 (Compatible; Windows NT 5.1;MSIE 6.0) (compatible; MSIE 6.0; Windows NT 5.1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
                                    ServicePointManager.Expect100Continue = false;
                                    ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                                    response = client.DownloadString(_API);
                                    client.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    string[] res = response.Split('\n');

                                    string[] columns = res.Where(w => w == res[0]).ToArray();

                                    string[] rows = res.Where(w => w != res[0]).ToArray();

                                    if (columns.Length > 0 && rows.Length > 0)
                                    {
                                        dt_APIRes = saDt.StringArrayToDataTable(columns, rows);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                        }
                    }
                    else if (SupplierResponseFormat.ToUpper() == "TEXT")
                    {
                        string json = "";
                        if (SupplierAPIMethod.ToUpper() == "POST")
                        {
                            if (SupplierURL.ToUpper() == "HTTPS://SS.SRK.BEST/V1/STOCKSHAING%20/SERVICES/")
                            {
                                try
                                {
                                    HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("https://ss.srk.best/v1/stockSharing/services/0e4d83a5-b4b0-46e3-859e-5e09e2f3b343");
                                    request1.Method = "POST";
                                    request1.Timeout = 7200000; //2 Hour in milliseconds
                                    request1.ContentType = "text/plain";
                                    request1.Headers.Add("X-ACCESS-KEY", "627d44bd-c286-49cc-ab95-ce83fdb12934");


                                    WebResponse response1 = request1.GetResponse();
                                    using (var reader1 = new StreamReader(response1.GetResponseStream()))
                                    {
                                        json = reader1.ReadToEnd();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        //dt_APIRes = ConvertCSVtoDataTable(json);
                                        dt_APIRes = Convert_FILE_To_DataTable(".csv", json, "", SupplierId);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                        }
                        else if (SupplierAPIMethod.ToUpper() == "GET")
                        {
                            if (SupplierURL.ToUpper() == "HTTPS://LAXMIDIAMOND.COM/HOME/STOCK?I=J%2BQYW5HXVP%2B1VEWRXXAQSAOJHYY%2B2OQBQBTHX%2F8LQDTM5WEFVAAGRDZHQ52COL87")
                            {
                                try
                                {
                                    HttpWebRequest request1 = (HttpWebRequest)WebRequest.Create("https://laxmidiamond.com/Home/stock?i=J%2BQyw5hxVp%2B1VeWRXxaQsAOjhYY%2B2oQbqbtHx%2F8lqdtM5WEFVaAgrDZHq52col87");
                                    request1.Method = "GET";
                                    request1.Timeout = 7200000; //2 Hour in milliseconds
                                    //request1.ContentType = "text/plain";

                                    WebResponse response1 = request1.GetResponse();
                                    using (var reader1 = new StreamReader(response1.GetResponseStream()))
                                    {
                                        json = reader1.ReadToEnd();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("API Not Working", ex.Message, null);
                                }

                                try
                                {
                                    if (!string.IsNullOrEmpty(json))
                                    {
                                        dt_APIRes = ConvertCSVtoDataTable(json);
                                    }
                                    else
                                    {
                                        return ("Data not Found", string.Empty, null);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    return ("Data Response Format Changed", ex.Message, null);
                                }
                            }
                        }
                    }

                    if (dt_APIRes != null && dt_APIRes.Rows.Count == 0 && dt_APIRes.Columns.Count == 0)
                    {
                        return ("3rd party API has been not Integrate", string.Empty, dt_APIRes);
                    }
                }
            }
            catch (Exception ex)
            {
                return ("ERROR", ex.Message, null);
            }

            if (dt_APIRes != null && dt_APIRes.Rows.Count > 0)
            {
                Int32 VishindasHolaram_Lakhi_Id = Convert.ToInt32(ConfigurationManager.AppSettings["VishindasHolaram_Lakhi_Id"]);
                Int32 StarRays_Id = Convert.ToInt32(ConfigurationManager.AppSettings["StarRays_Id"]);
                Int32 JewelParadise_Id = Convert.ToInt32(ConfigurationManager.AppSettings["JewelParadise_Id"]);
                Int32 Diamart_Id = Convert.ToInt32(ConfigurationManager.AppSettings["Diamart_Id"]);
                Int32 Laxmi_Id = Convert.ToInt32(ConfigurationManager.AppSettings["Laxmi_Id"]);
                Int32 JB_Id = Convert.ToInt32(ConfigurationManager.AppSettings["JB_Id"]);
                Int32 Aspeco_Id = Convert.ToInt32(ConfigurationManager.AppSettings["Aspeco_Id"]);
                Int32 KBS_Id = Convert.ToInt32(ConfigurationManager.AppSettings["KBS_Id"]);

                if (SupplierId == VishindasHolaram_Lakhi_Id)
                {
                    dt_APIRes = Lakhi_TableCrown_BlackWhite(dt_APIRes);
                }
                else if (SupplierId == StarRays_Id)
                {
                    dt_APIRes = StarRays_TableCrownPav_Open(dt_APIRes);
                }
                else if (SupplierId == JewelParadise_Id)
                {
                    dt_APIRes = JewelParadise_Shade(dt_APIRes);
                }
                else if (SupplierId == Diamart_Id)
                {
                    dt_APIRes = Diamart_Shade(dt_APIRes);
                }
                else if (SupplierId == Laxmi_Id)
                {
                    dt_APIRes = Laxmi_Grading(dt_APIRes);
                }
                else if (SupplierId == JB_Id)
                {
                    dt_APIRes = JB(dt_APIRes);
                }
                else if (SupplierId == Aspeco_Id)
                {
                    dt_APIRes = Aspeco(dt_APIRes);
                }
                else if (SupplierId == KBS_Id)
                {
                    dt_APIRes = KBS(dt_APIRes);
                }

                return ("SUCCESS", string.Empty, dt_APIRes);
            }
            else
            {
                return ("Data Not Found", string.Empty, dt_APIRes);
            }
        }
        public DataTable Lakhi_TableCrown_BlackWhite(DataTable Stock_dt)
        {
            Stock_dt.Columns.Add("Table White", typeof(string));
            Stock_dt.Columns.Add("Crown White", typeof(string));
            Stock_dt.Columns.Add("Table Black", typeof(string));
            Stock_dt.Columns.Add("Crown Black", typeof(string));

            foreach (DataRow row in Stock_dt.Rows)
            {
                var (TableWhite, CrownWhite) = Center_Inclusion(Convert.ToString(row["Center Inclusion"]));
                row["Table White"] = TableWhite;
                row["Crown White"] = CrownWhite;

                var (TableBlack, CrownBlack) = Black_Inclusion(Convert.ToString(row["Black Inclusion   "]));
                row["Table Black"] = TableBlack;
                row["Crown Black"] = CrownBlack;
            }
            return Stock_dt;
        }
        public (string, string) Center_Inclusion(string CenterInclusion)
        {
            if (CenterInclusion == "NONE")
            {
                return ("NN", "NN");
            }
            else
            {
                string[] strArray = CenterInclusion.Split(',');

                Int32 ok_Len = Convert.ToInt32(strArray.Length) - 1;
                Int32 Exist_Len = 0;

                if (strArray[0] == "T1" || strArray[0] == "T2")
                {
                    if (strArray.Length == 2)
                    {
                        foreach (string str in strArray)
                        {
                            if (str.Trim() == "TS" || str.Trim() == "TC")
                            {
                                Exist_Len += 1;
                            }
                        }
                        if (Exist_Len == ok_Len)
                        {
                            return ("T1", "NN");
                        }

                        foreach (string str in strArray)
                        {
                            if (str.Trim() == "CC" || str.Trim() == "CG")
                            {
                                Exist_Len += 1;
                            }
                        }
                        if (Exist_Len == ok_Len)
                        {
                            return ("NN", "C1");
                        }
                    }
                    else if (strArray.Length == 3)
                    {
                        foreach (string str in strArray)
                        {
                            if (str.Trim() == "TS" || str.Trim() == "TC" || str.Trim() == "CC" || str.Trim() == "CG")
                            {
                                Exist_Len += 1;
                            }
                        }
                        if (Exist_Len == ok_Len)
                        {
                            return ("T1", "C1");
                        }
                    }
                }
                else if (strArray[0] == "T3")
                {
                    if (strArray.Length == 2)
                    {
                        foreach (string str in strArray)
                        {
                            if (str.Trim() == "TS" || str.Trim() == "TC")
                            {
                                Exist_Len += 1;
                            }
                        }
                        if (Exist_Len == ok_Len)
                        {
                            return ("T2", "NN");
                        }

                        foreach (string str in strArray)
                        {
                            if (str.Trim() == "CC" || str.Trim() == "CG")
                            {
                                Exist_Len += 1;
                            }
                        }
                        if (Exist_Len == ok_Len)
                        {
                            return ("NN", "C2");
                        }
                    }
                    else if (strArray.Length == 3)
                    {
                        foreach (string str in strArray)
                        {
                            if (str.Trim() == "TS" || str.Trim() == "TC" || str.Trim() == "CC" || str.Trim() == "CG")
                            {
                                Exist_Len += 1;
                            }
                        }
                        if (Exist_Len == ok_Len)
                        {
                            return ("T2", "C2");
                        }
                    }
                }
            }
            return (null, null);
        }
        public (string, string) Black_Inclusion(string BlackInclusion)
        {
            if (BlackInclusion == "NONE")
            {
                return ("NN", "NN");
            }
            else
            {
                string[] strArray = BlackInclusion.Split(',');

                Int32 ok_Len = Convert.ToInt32(strArray.Length) - 1;
                Int32 Exist_Len = 0;

                if (strArray[0] == "N1" || strArray[0] == "N2")
                {
                    if (strArray.Length == 2)
                    {
                        foreach (string str in strArray)
                        {
                            if (str.Trim() == "TS" || str.Trim() == "TC")
                            {
                                Exist_Len += 1;
                            }
                        }
                        if (Exist_Len == ok_Len)
                        {
                            return ("BT1", "NN");
                        }

                        foreach (string str in strArray)
                        {
                            if (str.Trim() == "CC" || str.Trim() == "CG")
                            {
                                Exist_Len += 1;
                            }
                        }
                        if (Exist_Len == ok_Len)
                        {
                            return ("NN", "BC1");
                        }
                    }
                    else if (strArray.Length == 3)
                    {
                        foreach (string str in strArray)
                        {
                            if (str.Trim() == "TS" || str.Trim() == "TC" || str.Trim() == "CC" || str.Trim() == "CG")
                            {
                                Exist_Len += 1;
                            }
                        }
                        if (Exist_Len == ok_Len)
                        {
                            return ("BT1", "BC1");
                        }
                    }
                }
                else if (strArray[0] == "N3")
                {
                    if (strArray.Length == 2)
                    {
                        foreach (string str in strArray)
                        {
                            if (str.Trim() == "TS" || str.Trim() == "TC")
                            {
                                Exist_Len += 1;
                            }
                        }
                        if (Exist_Len == ok_Len)
                        {
                            return ("BT2", "NN");
                        }

                        foreach (string str in strArray)
                        {
                            if (str.Trim() == "CC" || str.Trim() == "CG")
                            {
                                Exist_Len += 1;
                            }
                        }
                        if (Exist_Len == ok_Len)
                        {
                            return ("NN", "BC2");
                        }
                    }
                    else if (strArray.Length == 3)
                    {
                        foreach (string str in strArray)
                        {
                            if (str.Trim() == "TS" || str.Trim() == "TC" || str.Trim() == "CC" || str.Trim() == "CG")
                            {
                                Exist_Len += 1;
                            }
                        }
                        if (Exist_Len == ok_Len)
                        {
                            return ("BT2", "BC2");
                        }
                    }
                }
            }
            return (null, null);
        }
        public DataTable StarRays_TableCrownPav_Open(DataTable Stock_dt)
        {
            Stock_dt.Columns.Add("Table Open", typeof(string));
            Stock_dt.Columns.Add("Crown Open", typeof(string));
            Stock_dt.Columns.Add("Pav Open", typeof(string));

            foreach (DataRow row in Stock_dt.Rows)
            {
                string[] strArray = Convert.ToString(row["Open Inclusion"]).Split('/');

                if (strArray.Length >= 3)
                {
                    row["Table Open"] = strArray[0];
                    row["Crown Open"] = strArray[1];
                    row["Pav Open"] = strArray[2];
                }
            }
            return Stock_dt;
        }
        public DataTable JewelParadise_Shade(DataTable Stock_dt)
        {
            Stock_dt.Columns.Add("Shade", typeof(string));

            foreach (DataRow row in Stock_dt.Rows)
            {
                string colsh = Convert.ToString(row["colsh"]);
                string green = Convert.ToString(row["green"]);
                string othertinge = Convert.ToString(row["othertinge"]);

                if (colsh == "B1" || colsh == "B2")
                {
                    row["Shade"] = "Brown";
                }
                else if (green == "G1" || green == "G2")
                {
                    row["Shade"] = "Mix Tinge";
                }
                else if (othertinge == "OT1" || othertinge == "OT2")
                {
                    row["Shade"] = "Mix Tinge";
                }
                else
                {
                    row["Shade"] = "White";
                }
            }
            return Stock_dt;
        }
        public DataTable Diamart_Shade(DataTable Stock_dt)
        {
            foreach (DataRow row in Stock_dt.Rows)
            {
                string Shade = Convert.ToString(row["Shade"]);
                string Brown = Convert.ToString(row["Brown"]);
                string Green = Convert.ToString(row["Green"]);

                if (Shade == "FMT" || Shade == "HMT" || Shade == "LMT" || Shade == "MMT")
                {
                    row["Shade"] = "Mix Tinge";
                }
                else if (Brown == "FBR" || Brown == "HBR" || Brown == "LBR" || Brown == "MBR")
                {
                    row["Shade"] = "Brown";
                }
                else if (Green == "FGR" || Green == "HGR" || Green == "LGR" || Green == "MGR")
                {
                    row["Shade"] = "Mix Tinge";
                }
                else
                {
                    row["Shade"] = "White";
                }
            }
            return Stock_dt;
        }
        public DataTable Laxmi_Grading(DataTable Stock_dt)
        {
            Stock_dt.Columns.Add("Table Open", typeof(string));
            Stock_dt.Columns.Add("Crown Open", typeof(string));
            Stock_dt.Columns.Add("Pav Open", typeof(string));
            Stock_dt.Columns.Add("Girdle Open", typeof(string));
            Stock_dt.Columns.Add("Table White", typeof(string));
            Stock_dt.Columns.Add("Crown White", typeof(string));
            Stock_dt.Columns.Add("Table Black", typeof(string));
            Stock_dt.Columns.Add("Crown Black", typeof(string));

            foreach (DataRow row in Stock_dt.Rows)
            {
                string BlackInc = Convert.ToString(row["Black Inc"]).ToUpper();
                string OpenInc = Convert.ToString(row["Open Inc"]).ToUpper();
                string SideInc = Convert.ToString(row["Side Inc"]).ToUpper();
                string TableInc = Convert.ToString(row["Table Inc"]).ToUpper();

                if (OpenInc == "NO")
                {
                    row["Table Open"] = "NN";
                    row["Crown Open"] = "NN";
                    row["Pav Open"] = "NN";
                    row["Girdle Open"] = "NN";
                }
                else if (OpenInc == "")
                {
                    row["Table Open"] = "BLANK";
                    row["Crown Open"] = "BLANK";
                    row["Pav Open"] = "BLANK";
                    row["Girdle Open"] = "BLANK";
                }
                else
                {
                    string[] strArray = OpenInc.Split(',');
                    foreach (string str in strArray)
                    {
                        if (str.Trim() == "TO01" || str.Trim() == "THL" || str.Trim() == "TO1")
                        {
                            row["Table Open"] = "TO1";
                        }
                        else if (str.Trim() == "TO2")
                        {
                            row["Table Open"] = "TO2";
                        }

                        else if (str.Trim() == "CO01" || str.Trim() == "CHL" || str.Trim() == "CO1")
                        {
                            row["Crown Open"] = "CO1";
                        }
                        else if (str.Trim() == "CO2")
                        {
                            row["Crown Open"] = "CO2";
                        }

                        else if (str.Trim() == "PO01" || str.Trim() == "PHL" || str.Trim() == "PO1")
                        {
                            row["Pav Open"] = "PO1";
                        }
                        else if (str.Trim() == "PO2")
                        {
                            row["Pav Open"] = "PO2";
                        }

                        else if (str.Trim() == "GO01" || str.Trim() == "GHL" || str.Trim() == "GO1")
                        {
                            row["Girdle Open"] = "GO1";
                        }
                        else if (str.Trim() == "GO2")
                        {
                            row["Girdle Open"] = "GO2";
                        }
                    }
                }

                if (string.IsNullOrEmpty(Convert.ToString(row["Table Open"])))
                {
                    row["Table Open"] = "NN";
                }
                if (string.IsNullOrEmpty(Convert.ToString(row["Crown Open"])))
                {
                    row["Crown Open"] = "NN";
                }
                if (string.IsNullOrEmpty(Convert.ToString(row["Pav Open"])))
                {
                    row["Pav Open"] = "NN";
                }
                if (string.IsNullOrEmpty(Convert.ToString(row["Girdle Open"])))
                {
                    row["Girdle Open"] = "NN";
                }

                row["Table Open"] = Convert.ToString(row["Table Open"]).Replace("BLANK", null);
                row["Crown Open"] = Convert.ToString(row["Crown Open"]).Replace("BLANK", null);
                row["Pav Open"] = Convert.ToString(row["Pav Open"]).Replace("BLANK", null);
                row["Girdle Open"] = Convert.ToString(row["Girdle Open"]).Replace("BLANK", null);


                if (TableInc == "NO")
                {
                    row["Table White"] = "NN";
                }
                else if (TableInc == "")
                {
                    row["Table White"] = "";
                }
                else
                {
                    string[] strArray_1 = TableInc.Split(',');
                    foreach (string str in strArray_1)
                    {
                        if (str.Trim() == "TB3" || str.Trim() == "TF3" || str.Trim() == "TCL3" || str.Trim() == "TCR3")
                        {
                            row["Table White"] = row["Table White"] + ",3";
                        }
                        else if (str.Trim() == "TB2" || str.Trim() == "TF2" || str.Trim() == "TCL2" || str.Trim() == "TCR2")
                        {
                            row["Table White"] = row["Table White"] + ",2";
                        }
                        else if (str.Trim() == "TB1" || str.Trim() == "TF1" || str.Trim() == "TCL1" || str.Trim() == "TCR1" || str.Trim() == "TB01" || str.Trim() == "TF01" || str.Trim() == "TCL01" || str.Trim() == "TCR01")
                        {
                            row["Table White"] = row["Table White"] + ",1";
                        }
                    }
                    if (Convert.ToString(row["Table White"]).Contains("3"))
                    {
                        row["Table White"] = "T3";
                    }
                    else if (Convert.ToString(row["Table White"]).Contains("2"))
                    {
                        row["Table White"] = "T2";
                    }
                    else if (Convert.ToString(row["Table White"]).Contains("1"))
                    {
                        row["Table White"] = "T1";
                    }
                }


                if (SideInc == "NO")
                {
                    row["Crown White"] = "NN";
                }
                else if (SideInc == "")
                {
                    row["Crown White"] = "";
                }
                else
                {
                    string[] strArray_1 = SideInc.Split(',');
                    foreach (string str in strArray_1)
                    {
                        if (str.Trim() == "SB3" || str.Trim() == "SF3" || str.Trim() == "SCL3" || str.Trim() == "SCR3")
                        {
                            row["Crown White"] = row["Crown White"] + ",3";
                        }
                        else if (str.Trim() == "SB2" || str.Trim() == "SF2" || str.Trim() == "SCL2" || str.Trim() == "SCR2")
                        {
                            row["Crown White"] = row["Crown White"] + ",2";
                        }
                        else if (str.Trim() == "SB1" || str.Trim() == "SF1" || str.Trim() == "SCL1" || str.Trim() == "SCR1" || str.Trim() == "SB01" || str.Trim() == "SF01" || str.Trim() == "SCL01" || str.Trim() == "SCR01")
                        {
                            row["Crown White"] = row["Crown White"] + ",1";
                        }
                    }
                    if (Convert.ToString(row["Crown White"]).Contains("3"))
                    {
                        row["Crown White"] = "C3";
                    }
                    else if (Convert.ToString(row["Crown White"]).Contains("2"))
                    {
                        row["Crown White"] = "C2";
                    }
                    else if (Convert.ToString(row["Crown White"]).Contains("1"))
                    {
                        row["Crown White"] = "C1";
                    }
                }


                if (BlackInc == "NO")
                {
                    row["Table Black"] = "NN";
                    row["Crown Black"] = "NN";
                }
                else if (BlackInc == "")
                {
                    row["Table Black"] = "";
                    row["Crown Black"] = "";
                }
                else
                {
                    string[] strArray_1 = BlackInc.Split(',');
                    foreach (string str in strArray_1)
                    {
                        if (str.Trim() == "TB3" || str.Trim() == "TF3" || str.Trim() == "TCL3" || str.Trim() == "TCR3")
                        {
                            row["Table Black"] = row["Table Black"] + ",3";
                        }
                        else if (str.Trim() == "TB2" || str.Trim() == "TF2" || str.Trim() == "TCL2" || str.Trim() == "TCR2")
                        {
                            row["Table Black"] = row["Table Black"] + ",2";
                        }
                        else if (str.Trim() == "TB1" || str.Trim() == "TF1" || str.Trim() == "TCL1" || str.Trim() == "TCR1" || str.Trim() == "TB01" || str.Trim() == "TF01" || str.Trim() == "TCL01" || str.Trim() == "TCR01")
                        {
                            row["Table Black"] = row["Table Black"] + ",1";
                        }

                        if (str.Trim() == "SB3" || str.Trim() == "SF3" || str.Trim() == "SCL3" || str.Trim() == "SCR3")
                        {
                            row["Crown Black"] = row["Crown Black"] + ",3";
                        }
                        else if (str.Trim() == "SB2" || str.Trim() == "SF2" || str.Trim() == "SCL2" || str.Trim() == "SCR2")
                        {
                            row["Crown Black"] = row["Crown Black"] + ",2";
                        }
                        else if (str.Trim() == "SB1" || str.Trim() == "SF1" || str.Trim() == "SCL1" || str.Trim() == "SCR1" || str.Trim() == "SB01" || str.Trim() == "SF01" || str.Trim() == "SCL01" || str.Trim() == "SCR01")
                        {
                            row["Crown Black"] = row["Crown Black"] + ",1";
                        }
                    }

                    if (Convert.ToString(row["Table Black"]).Contains("3"))
                    {
                        row["Table Black"] = "BT3";
                    }
                    else if (Convert.ToString(row["Table Black"]).Contains("2"))
                    {
                        row["Table Black"] = "BT2";
                    }
                    else if (Convert.ToString(row["Table Black"]).Contains("1"))
                    {
                        row["Table Black"] = "BT1";
                    }

                    if (Convert.ToString(row["Crown Black"]).Contains("3"))
                    {
                        row["Crown Black"] = "BC3";
                    }
                    else if (Convert.ToString(row["Crown Black"]).Contains("2"))
                    {
                        row["Crown Black"] = "BC2";
                    }
                    else if (Convert.ToString(row["Crown Black"]).Contains("1"))
                    {
                        row["Crown Black"] = "BC1";
                    }
                }
            }

            return Stock_dt;
        }
        public DataTable JB(DataTable Stock_dt)
        {
            Stock_dt.DefaultView.RowFilter = "[QC] <> 'YES'";
            Stock_dt = Stock_dt.DefaultView.ToTable();

            return Stock_dt;
        }
        public DataTable Aspeco(DataTable Stock_dt)
        {
            Stock_dt.DefaultView.RowFilter = "[Cash Price] <> '0.00'";
            Stock_dt = Stock_dt.DefaultView.ToTable();

            return Stock_dt;
        }
        public DataTable KBS(DataTable Stock_dt)
        {
            Stock_dt.DefaultView.RowFilter = "[Asking%] <> '0.00'";
            Stock_dt = Stock_dt.DefaultView.ToTable();

            return Stock_dt;
        }
        public DataTable ColumnMapping_In_SupplierStock(string StockFrom, int SupplierId, DataTable dt_APIRes, DataTable dtSupplCol)
        {
            DataTable Final_dt = new DataTable();
            Database db = new Database();
            List<IDbDataParameter> list = new List<IDbDataParameter>();
            try
            {
                Final_dt = new DataTable();
                Final_dt.Columns.Add("Stock Type", typeof(string));
                Final_dt.Columns.Add("Not Mapped Column", typeof(string));
                Final_dt.Columns.Add("Stock From", typeof(string));
                Final_dt.Columns.Add("SupplierId", typeof(string));
                Final_dt.Columns.Add("Supplier Ref No", typeof(string));
                Final_dt.Columns.Add("Cert No", typeof(string));
                Final_dt.Columns.Add("Shape", typeof(string));
                Final_dt.Columns.Add("Color", typeof(string));
                Final_dt.Columns.Add("Clarity", typeof(string));
                Final_dt.Columns.Add("Cts", typeof(string));
                Final_dt.Columns.Add("Cut", typeof(string));
                Final_dt.Columns.Add("Polish", typeof(string));
                Final_dt.Columns.Add("Symm", typeof(string));
                Final_dt.Columns.Add("Fls", typeof(string));
                Final_dt.Columns.Add("Base Dis", typeof(string));
                Final_dt.Columns.Add("Base Price/Ct", typeof(string));
                Final_dt.Columns.Add("Base Amt", typeof(string));
                Final_dt.Columns.Add("Length", typeof(string));
                Final_dt.Columns.Add("Width", typeof(string));
                Final_dt.Columns.Add("Depth", typeof(string));
                Final_dt.Columns.Add("Measurement", typeof(string));
                Final_dt.Columns.Add("Depth %", typeof(string));
                Final_dt.Columns.Add("Table %", typeof(string));
                Final_dt.Columns.Add("Girdle %", typeof(string));
                Final_dt.Columns.Add("Key to Symbol", typeof(string));
                Final_dt.Columns.Add("Comment", typeof(string));
                Final_dt.Columns.Add("Supplier Comment", typeof(string));
                Final_dt.Columns.Add("Lab", typeof(string));
                Final_dt.Columns.Add("Crown Angle", typeof(string));
                Final_dt.Columns.Add("Pav Angle", typeof(string));
                Final_dt.Columns.Add("Crown Height", typeof(string));
                Final_dt.Columns.Add("Pav Height", typeof(string));
                Final_dt.Columns.Add("Shade", typeof(string));
                Final_dt.Columns.Add("Milky", typeof(string));
                Final_dt.Columns.Add("Luster", typeof(string));
                Final_dt.Columns.Add("Table White", typeof(string));
                Final_dt.Columns.Add("Crown White", typeof(string));
                Final_dt.Columns.Add("Table Black", typeof(string));
                Final_dt.Columns.Add("Crown Black", typeof(string));
                Final_dt.Columns.Add("Table Open", typeof(string));
                Final_dt.Columns.Add("Crown Open", typeof(string));
                Final_dt.Columns.Add("Pav Open", typeof(string));
                Final_dt.Columns.Add("Girdle Open", typeof(string));
                Final_dt.Columns.Add("Location", typeof(string));
                Final_dt.Columns.Add("Status", typeof(string));
                Final_dt.Columns.Add("BGM", typeof(string));
                Final_dt.Columns.Add("Culet", typeof(string));
                Final_dt.Columns.Add("Girdle Type", typeof(string));
                Final_dt.Columns.Add("Girdle To", typeof(string));
                Final_dt.Columns.Add("Girdle From", typeof(string));
                Final_dt.Columns.Add("Girdle", typeof(string));
                Final_dt.Columns.Add("Star Length", typeof(string));
                Final_dt.Columns.Add("Lower HF", typeof(string));
                Final_dt.Columns.Add("Certi Date", typeof(string));
                Final_dt.Columns.Add("Laser Inscription", typeof(string));
                Final_dt.Columns.Add("Fls Color", typeof(string));
                Final_dt.Columns.Add("Fancy Color", typeof(string));
                Final_dt.Columns.Add("Fancy Intensity", typeof(string));
                Final_dt.Columns.Add("Fancy Overtone", typeof(string));
                Final_dt.Columns.Add("Cert Type", typeof(string));
                Final_dt.Columns.Add("Country of Origin", typeof(string));
                Final_dt.Columns.Add("H&A", typeof(string));
                Final_dt.Columns.Add("Lab URL", typeof(string));
                Final_dt.Columns.Add("Image Real", typeof(string));
                Final_dt.Columns.Add("Image Asset", typeof(string));
                Final_dt.Columns.Add("Image Heart", typeof(string));
                Final_dt.Columns.Add("Image Arrow", typeof(string));
                Final_dt.Columns.Add("Image URL 1", typeof(string));
                Final_dt.Columns.Add("Image URL 2", typeof(string));
                Final_dt.Columns.Add("Video URL", typeof(string));
                Final_dt.Columns.Add("Video MP4", typeof(string));
                Final_dt.Columns.Add("DNA", typeof(string));
                Final_dt.Columns.Add("Goods Type", typeof(string));


                //db = new Database();
                //list = new List<IDbDataParameter>();
                //DataTable Col_dt = db.ExecuteSP("Get_Column_Master", list.ToArray(), false);

                foreach (DataRow row in dt_APIRes.Rows)
                {
                    DataRow Final_row = Final_dt.NewRow();

                    Final_row["Stock Type"] = "I";
                    Final_row["Not Mapped Column"] = "";
                    Final_row["Stock From"] = StockFrom;
                    Final_row["SupplierId"] = Convert.ToString(SupplierId);

                    foreach (DataRow SuppCol_row in dtSupplCol.Rows)
                    {
                        Final_row["Supplier Ref No"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Supplier Ref No") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Supplier Ref No"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Supplier Ref No"] = (Convert.ToString(Final_row["Supplier Ref No"]) == "") ? null : Convert.ToString(Final_row["Supplier Ref No"]);

                        Final_row["Cert No"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Cert No") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Cert No"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Cert No"] = (Convert.ToString(Final_row["Cert No"]) == "") ? null : Convert.ToString(Final_row["Cert No"]);

                        Final_row["Shape"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Shape") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Shape"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Shape"] = (Convert.ToString(Final_row["Shape"]) == "") ? null : Convert.ToString(Final_row["Shape"]);

                        Final_row["Color"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Color") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Color"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Color"] = (Convert.ToString(Final_row["Color"]) == "") ? null : Convert.ToString(Final_row["Color"]);

                        Final_row["Clarity"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Clarity") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Clarity"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Clarity"] = (Convert.ToString(Final_row["Clarity"]) == "") ? null : Convert.ToString(Final_row["Clarity"]);

                        Final_row["Cts"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Cts") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Cts"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Cts"] = (Convert.ToString(Final_row["Cts"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Cts"]));

                        Final_row["Cut"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Cut") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Cut"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Cut"] = (Convert.ToString(Final_row["Cut"]) == "") ? null : Convert.ToString(Final_row["Cut"]);

                        Final_row["Polish"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Polish") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Polish"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Polish"] = (Convert.ToString(Final_row["Polish"]) == "") ? null : Convert.ToString(Final_row["Polish"]);

                        Final_row["Symm"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Symm") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Symm"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Symm"] = (Convert.ToString(Final_row["Symm"]) == "") ? null : Convert.ToString(Final_row["Symm"]);

                        Final_row["Fls"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Fls") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Fls"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Fls"] = (Convert.ToString(Final_row["Fls"]) == "") ? null : Convert.ToString(Final_row["Fls"]);

                        Final_row["Base Dis"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Base Dis") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Base Dis"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Base Dis"] = (Convert.ToString(Final_row["Base Dis"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Base Dis"]));

                        Final_row["Base Price/Ct"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Base Price/Ct") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Base Price/Ct"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Base Price/Ct"] = (Convert.ToString(Final_row["Base Price/Ct"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Base Price/Ct"]));

                        Final_row["Base Amt"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Base Amt") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Base Amt"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Base Amt"] = (Convert.ToString(Final_row["Base Amt"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Base Amt"]));

                        Final_row["Length"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Length") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Length"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Length"] = (Convert.ToString(Final_row["Length"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Length"]));

                        Final_row["Width"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Width") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Width"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Width"] = (Convert.ToString(Final_row["Width"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Width"]));

                        Final_row["Depth"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Depth") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Depth"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Depth"] = (Convert.ToString(Final_row["Depth"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Depth"]));

                        Final_row["Measurement"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Measurement") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Measurement"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Measurement"] = (Convert.ToString(Final_row["Measurement"]) == "") ? null : Convert.ToString(Final_row["Measurement"]);

                        Final_row["Depth %"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Depth %") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Depth %"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Depth %"] = (Convert.ToString(Final_row["Depth %"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Depth %"]));

                        Final_row["Table %"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Table %") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Table %"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Table %"] = (Convert.ToString(Final_row["Table %"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Table %"]));

                        Final_row["Girdle %"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Girdle %") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Girdle %"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Girdle %"] = (Convert.ToString(Final_row["Girdle %"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Girdle %"]));

                        Final_row["Key to Symbol"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Key to Symbol") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Key to Symbol"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Key to Symbol"] = (Convert.ToString(Final_row["Key to Symbol"]) == "") ? null : Convert.ToString(Final_row["Key to Symbol"]);

                        Final_row["Comment"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Comment") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Comment"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Comment"] = (Convert.ToString(Final_row["Comment"]) == "") ? null : Convert.ToString(Final_row["Comment"]);

                        Final_row["Supplier Comment"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Supplier Comment") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Supplier Comment"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Supplier Comment"] = (Convert.ToString(Final_row["Supplier Comment"]) == "") ? null : Convert.ToString(Final_row["Supplier Comment"]);

                        Final_row["Lab"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Lab") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Lab"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Lab"] = (Convert.ToString(Final_row["Lab"]) == "") ? null : Convert.ToString(Final_row["Lab"]);

                        Final_row["Crown Angle"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Crown Angle") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Crown Angle"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Crown Angle"] = (Convert.ToString(Final_row["Crown Angle"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Crown Angle"]));

                        Final_row["Pav Angle"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Pav Angle") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Pav Angle"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Pav Angle"] = (Convert.ToString(Final_row["Pav Angle"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Pav Angle"]));

                        Final_row["Crown Height"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Crown Height") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Crown Height"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Crown Height"] = (Convert.ToString(Final_row["Crown Height"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Crown Height"]));

                        Final_row["Pav Height"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Pav Height") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Pav Height"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Pav Height"] = (Convert.ToString(Final_row["Pav Height"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Pav Height"]));

                        Final_row["Shade"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Shade") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Shade"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Shade"] = (Convert.ToString(Final_row["Shade"]) == "") ? null : Convert.ToString(Final_row["Shade"]);

                        Final_row["Milky"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Milky") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Milky"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Milky"] = (Convert.ToString(Final_row["Milky"]) == "") ? null : Convert.ToString(Final_row["Milky"]);

                        Final_row["Luster"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Luster") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Luster"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Luster"] = (Convert.ToString(Final_row["Luster"]) == "") ? null : Convert.ToString(Final_row["Luster"]);

                        Final_row["Table White"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Table White") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Table White"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Table White"] = (Convert.ToString(Final_row["Table White"]) == "") ? null : Convert.ToString(Final_row["Table White"]);

                        Final_row["Crown White"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Crown White") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Crown White"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Crown White"] = (Convert.ToString(Final_row["Crown White"]) == "") ? null : Convert.ToString(Final_row["Crown White"]);

                        Final_row["Table Black"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Table Black") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Table Black"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Table Black"] = (Convert.ToString(Final_row["Table Black"]) == "") ? null : Convert.ToString(Final_row["Table Black"]);

                        Final_row["Crown Black"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Crown Black") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Crown Black"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Crown Black"] = (Convert.ToString(Final_row["Crown Black"]) == "") ? null : Convert.ToString(Final_row["Crown Black"]);

                        Final_row["Table Open"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Table Open") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Table Open"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Table Open"] = (Convert.ToString(Final_row["Table Open"]) == "") ? null : Convert.ToString(Final_row["Table Open"]);

                        Final_row["Crown Open"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Crown Open") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Crown Open"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Crown Open"] = (Convert.ToString(Final_row["Crown Open"]) == "") ? null : Convert.ToString(Final_row["Crown Open"]);

                        Final_row["Pav Open"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Pav Open") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Pav Open"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Pav Open"] = (Convert.ToString(Final_row["Pav Open"]) == "") ? null : Convert.ToString(Final_row["Pav Open"]);

                        Final_row["Girdle Open"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Girdle Open") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Girdle Open"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Girdle Open"] = (Convert.ToString(Final_row["Girdle Open"]) == "") ? null : Convert.ToString(Final_row["Girdle Open"]);

                        Final_row["Location"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Location") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Location"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Location"] = (Convert.ToString(Final_row["Location"]) == "") ? null : Convert.ToString(Final_row["Location"]);

                        Final_row["Status"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Status") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Status"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Status"] = (Convert.ToString(Final_row["Status"]) == "") ? null : Convert.ToString(Final_row["Status"]);

                        Final_row["BGM"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "BGM") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["BGM"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["BGM"] = (Convert.ToString(Final_row["BGM"]) == "") ? null : Convert.ToString(Final_row["BGM"]);

                        Final_row["Culet"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Culet") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Culet"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Culet"] = (Convert.ToString(Final_row["Culet"]) == "") ? null : Convert.ToString(Final_row["Culet"]);

                        Final_row["Girdle Type"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Girdle Type") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Girdle Type"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Girdle Type"] = (Convert.ToString(Final_row["Girdle Type"]) == "") ? null : Convert.ToString(Final_row["Girdle Type"]);

                        Final_row["Girdle To"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Girdle To") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Girdle To"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Girdle To"] = (Convert.ToString(Final_row["Girdle To"]) == "") ? null : Convert.ToString(Final_row["Girdle To"]);

                        Final_row["Girdle From"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Girdle From") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Girdle From"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Girdle From"] = (Convert.ToString(Final_row["Girdle From"]) == "") ? null : Convert.ToString(Final_row["Girdle From"]);

                        Final_row["Girdle"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Girdle") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Girdle"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Girdle"] = (Convert.ToString(Final_row["Girdle"]) == "") ? null : Convert.ToString(Final_row["Girdle"]);

                        Final_row["Star Length"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Star Length") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Star Length"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Star Length"] = (Convert.ToString(Final_row["Star Length"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Star Length"]));

                        Final_row["Lower HF"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Lower HF") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Lower HF"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Lower HF"] = (Convert.ToString(Final_row["Lower HF"]) == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Lower HF"]));

                        Final_row["Certi Date"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Certi Date") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Certi Date"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Certi Date"] = (Convert.ToString(Final_row["Certi Date"]) == "") ? null : Convert.ToString(Final_row["Certi Date"]);

                        Final_row["Laser Inscription"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Laser Inscription") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Laser Inscription"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Laser Inscription"] = (Convert.ToString(Final_row["Laser Inscription"]) == "") ? null : Convert.ToString(Final_row["Laser Inscription"]);

                        Final_row["Fls Color"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Fls Color") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Fls Color"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Fls Color"] = (Convert.ToString(Final_row["Fls Color"]) == "") ? null : Convert.ToString(Final_row["Fls Color"]);

                        Final_row["Fancy Color"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Fancy Color") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Fancy Color"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Fancy Color"] = (Convert.ToString(Final_row["Fancy Color"]) == "") ? null : Convert.ToString(Final_row["Fancy Color"]);

                        Final_row["Fancy Intensity"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Fancy Intensity") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Fancy Intensity"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Fancy Intensity"] = (Convert.ToString(Final_row["Fancy Intensity"]) == "") ? null : Convert.ToString(Final_row["Fancy Intensity"]);

                        Final_row["Fancy Overtone"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Fancy Overtone") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Fancy Overtone"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Fancy Overtone"] = (Convert.ToString(Final_row["Fancy Overtone"]) == "") ? null : Convert.ToString(Final_row["Fancy Overtone"]);

                        Final_row["Cert Type"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Cert Type") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Cert Type"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Cert Type"] = (Convert.ToString(Final_row["Cert Type"]) == "") ? null : Convert.ToString(Final_row["Cert Type"]);

                        Final_row["Country of Origin"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Country of Origin") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Country of Origin"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Country of Origin"] = (Convert.ToString(Final_row["Country of Origin"]) == "") ? null : Convert.ToString(Final_row["Country of Origin"]);

                        Final_row["H&A"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "H&A") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["H&A"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["H&A"] = (Convert.ToString(Final_row["H&A"]) == "") ? null : Convert.ToString(Final_row["H&A"]);

                        Final_row["Lab URL"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Lab URL") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Lab URL"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Lab URL"] = (Convert.ToString(Final_row["Lab URL"]) == "") ? null : Convert.ToString(Final_row["Lab URL"]);

                        Final_row["Image Real"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Image Real") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Image Real"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Image Real"] = (Convert.ToString(Final_row["Image Real"]) == "") ? null : Convert.ToString(Final_row["Image Real"]);

                        Final_row["Image Asset"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Image Asset") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Image Asset"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Image Asset"] = (Convert.ToString(Final_row["Image Asset"]) == "") ? null : Convert.ToString(Final_row["Image Asset"]);

                        Final_row["Image Heart"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Image Heart") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Image Heart"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Image Heart"] = (Convert.ToString(Final_row["Image Heart"]) == "") ? null : Convert.ToString(Final_row["Image Heart"]);

                        Final_row["Image Arrow"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Image Arrow") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Image Arrow"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Image Arrow"] = (Convert.ToString(Final_row["Image Arrow"]) == "") ? null : Convert.ToString(Final_row["Image Arrow"]);

                        Final_row["Image URL 1"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Image URL 1") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Image URL 1"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Image URL 1"] = (Convert.ToString(Final_row["Image URL 1"]) == "") ? null : Convert.ToString(Final_row["Image URL 1"]);

                        Final_row["Image URL 2"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Image URL 2") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Image URL 2"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Image URL 2"] = (Convert.ToString(Final_row["Image URL 2"]) == "") ? null : Convert.ToString(Final_row["Image URL 2"]);

                        Final_row["Video URL"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Video URL") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Video URL"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Video URL"] = (Convert.ToString(Final_row["Video URL"]) == "") ? null : Convert.ToString(Final_row["Video URL"]);

                        Final_row["Video MP4"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Video MP4") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Video MP4"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Video MP4"] = (Convert.ToString(Final_row["Video MP4"]) == "") ? null : Convert.ToString(Final_row["Video MP4"]);

                        Final_row["DNA"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "DNA") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["DNA"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["DNA"] = (Convert.ToString(Final_row["DNA"]) == "") ? null : Convert.ToString(Final_row["DNA"]);

                        Final_row["Goods Type"] = ((Convert.ToString(SuppCol_row["Column_Name"]) != "Goods Type") || (Convert.ToString(SuppCol_row["SupplierColumn"]) == "")) ? Convert.ToString(Final_row["Goods Type"]) : row[Convert.ToString(SuppCol_row["SupplierColumn"])];
                        Final_row["Goods Type"] = (Convert.ToString(Final_row["Goods Type"]) == "") ? null : Convert.ToString(Final_row["Goods Type"]);
                    }
                    Final_dt.Rows.Add(Final_row);
                }

                //DataRow[] dra = Final_dt.Select("[Cert No] = '2428377211'");

                if (Final_dt != null && Final_dt.Rows.Count > 0)
                {
                    db = new Database();
                    List<IDbDataParameter> para = new List<IDbDataParameter>();

                    para.Add(db.CreateParam("SupplierId", DbType.Int64, ParameterDirection.Input, SupplierId));
                    para.Add(db.CreateParam("StockFrom", DbType.String, ParameterDirection.Input, StockFrom));

                    DataTable CatColMas_Map_dt = db.ExecuteSP("Get_Category_Master_For_Val_Mapping", para.ToArray(), false);

                    foreach (DataRow CatColMas_Map_Row in CatColMas_Map_dt.Rows)
                    {
                        db = new Database();
                        para = new List<IDbDataParameter>();
                        para.Add(db.CreateParam("Column_Name", DbType.String, ParameterDirection.Input, Convert.ToString(CatColMas_Map_Row["Column_Name"])));
                        para.Add(db.CreateParam("SupplierId", DbType.Int64, ParameterDirection.Input, SupplierId));

                        DataTable Synonym_dt = db.ExecuteSP("Get_Synonyms_From_Cat_Sup_Val", para.ToArray(), false);

                        if (Synonym_dt != null && Synonym_dt.Rows.Count > 0)
                        {
                            foreach (DataRow Final_row in Final_dt.Rows)
                            {
                                bool Record_Map = false;
                                foreach (DataRow dt_row in Synonym_dt.Rows)
                                {
                                    string str = Convert.ToString(Final_row[Convert.ToString(CatColMas_Map_Row["Column_Name"])]);
                                    str = (string.IsNullOrEmpty(str) ? "BLANK" : str);

                                    string str2 = (Convert.ToString(dt_row["Cat_Name"]).Trim().ToUpper() == "BLANK" ? null : Convert.ToString(dt_row["Cat_Name"]));
                                    string str3 = Convert.ToString(dt_row["Synonyms"]);

                                    string[] strArray = str3.Split(',');

                                    foreach (string str4 in strArray)
                                    {
                                        //if (str.Trim().ToUpper() == str4.Trim().ToUpper() || str4.Trim().ToUpper() == "BLANK")
                                        if (str.Trim().ToUpper() == str4.Trim().ToUpper())
                                        {
                                            Final_row[Convert.ToString(CatColMas_Map_Row["Column_Name"])] = str2;
                                            Record_Map = true;
                                        }
                                    }
                                }
                                if (Record_Map == false)
                                {
                                    Final_row["Stock Type"] = "D";
                                    Final_row["Not Mapped Column"] += (Convert.ToString(Final_row["Not Mapped Column"]) == "" ? "" : ", ") + Convert.ToString(CatColMas_Map_Row["Column_Name"]);
                                }
                            }
                        }
                    }
                }
                //DataRow[] dra1 = Final_dt.Select("[Cert No] = '2428377211'");
                return Final_dt;
            }
            catch
            {
                return null;
            }
        }

        public DataTable ConvertCSVtoDataTable(string csvText)
        {
            DataTable dataTable = new DataTable();

            using (TextFieldParser parser = new TextFieldParser(new StringReader(csvText)))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Read the first row as column headers
                if (!parser.EndOfData)
                {
                    string[] headers = parser.ReadFields();
                    foreach (string header in headers)
                    {
                        dataTable.Columns.Add(header);
                    }
                }
                // Read the remaining rows as data
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    dataTable.Rows.Add(fields);
                }
            }

            return dataTable;
        }

        public static DataTable Convert_FILE_To_DataTable(string filetype, string connString, string SheetName, int SupplierId)
        {
            DataTable table = new DataTable();

            if (filetype == ".xls" || filetype == ".xlsx")
            {
                using (OleDbConnection connection = new OleDbConnection(connString))
                {
                    connection.Open();

                    if (SheetName == "")
                    {
                        DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        foreach (DataRow row in dt.Rows)
                        {
                            string _sheetName = Convert.ToString(row["TABLE_NAME"]);
                            if (_sheetName.EndsWith("$"))
                            {
                                // Assuming you want to keep sheets that don't end with "_Deleted"
                                if (!_sheetName.EndsWith("_Deleted$"))
                                {
                                    // Use OleDbDataAdapter to fetch the data from the sheet
                                    OleDbDataAdapter adapter = new OleDbDataAdapter($"SELECT * FROM [{_sheetName}]", connection);
                                    adapter.Fill(table);

                                    // You have the data from the sheet, you can now process it as needed
                                }
                            }
                        }
                    }
                    else
                    {
                        if (SheetName == "_ALL_SHEET_$")
                        {
                            List<Get_SheetName_From_File_Res> List_Res = new List<Get_SheetName_From_File_Res>();
                            int num = 0;

                            DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            foreach (DataRow row1 in dt.Rows)
                            {
                                string sheetName = Convert.ToString(row1["TABLE_NAME"]);
                                if (!sheetName.EndsWith("_Deleted$") && sheetName.EndsWith("$"))
                                {
                                    Get_SheetName_From_File_Res Res = new Get_SheetName_From_File_Res();
                                    Res.Id = num + 1;
                                    Res.SheetName = sheetName;
                                    List_Res.Add(Res);
                                }
                                num++;
                            }
                            foreach (Get_SheetName_From_File_Res row in List_Res)
                            {
                                DataTable new_table = new DataTable();
                                OleDbDataAdapter adapter = new OleDbDataAdapter($"SELECT * FROM [{row.SheetName}]", connection);
                                adapter.Fill(new_table);

                                if (new_table != null && new_table.Rows.Count > 0)
                                {
                                    table.Merge(new_table);
                                }
                            }
                        }
                        else
                        {
                            OleDbDataAdapter adapter = new OleDbDataAdapter($"SELECT * FROM [{SheetName}]", connection);
                            adapter.Fill(table);
                        }
                    }
                    connection.Close();

                    // Get the list of sheet names
                    /*
                    DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    foreach (DataRow row in dt.Rows)
                    {
                        string sheetName = Convert.ToString(row["TABLE_NAME"]);
                        if (sheetName.EndsWith("$"))
                        {
                            // Assuming you want to keep sheets that don't end with "_Deleted"
                            if (!sheetName.EndsWith("_Deleted$"))
                            {
                                // Use OleDbDataAdapter to fetch the data from the sheet
                                //OleDbDataAdapter adapter = new OleDbDataAdapter($"SELECT * FROM [{sheetName}]", connection);
                                OleDbDataAdapter adapter = new OleDbDataAdapter($"SELECT * FROM [{SheetName}]", connection);

                                adapter.Fill(table);

                                // You have the data from the sheet, you can now process it as needed
                            }
                        }
                    }
                    */
                }
            }
            else if (filetype == ".csv")
            {
                string[] headers = null; 
                string[] fields = null;
                
                Int32 KGK_Id = Convert.ToInt32(ConfigurationManager.AppSettings["KGK_Id"]);

                // Use TextFieldParser to read the CSV file
                using (TextFieldParser parser = new TextFieldParser(connString))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(",");
                    try
                    {
                        headers = parser.ReadFields();
                    }
                    catch (MalformedLineException ex)
                    {
                        headers = null;
                    }
                    
                    if (headers != null)
                    {
                        foreach (string header in headers)
                        {
                            table.Columns.Add(header);
                        }
                        if (SupplierId == KGK_Id)
                        {
                            table.Columns.Add("Blank 1");
                        }

                        while (!parser.EndOfData)
                        {
                            try
                            {
                                fields = parser.ReadFields();
                            }
                            catch (MalformedLineException ex)
                            {
                                fields = null;
                            }

                            if (fields != null)
                            {
                                DataRow row = table.NewRow();
                                for (int i = 0; i < table.Columns.Count; i++)
                                {
                                    if (fields.Length > i)
                                    {
                                        row[i] = fields[i];
                                    }
                                }
                                table.Rows.Add(row);
                            }
                        }
                    }
                }


                /*
                using (TextFieldParser parser = new TextFieldParser(connString))
                {
                    string[] delimiters = new string[] { "," };
                    parser.SetDelimiters(delimiters);
                    parser.HasFieldsEnclosedInQuotes = true;
                    string[] strArray2 = parser.ReadFields();
                    int index = 0;
                    while (true)
                    {
                        if (index >= strArray2.Length)
                        {
                            if (SupplierId == KGK_Id)
                            {
                                table.Columns.Add("Blank 1");
                            }
                            while (!parser.EndOfData)
                            {
                                string[] strArray3 = parser.ReadFields();
                                object[] values = strArray3;
                                table.Rows.Add(values);
                            }
                            break;
                        }
                        string columnName = strArray2[index];
                        table.Columns.Add(columnName);
                        index++;
                    }
                }
                */
            }
            return table;
        }

        public static List<Get_SheetName_From_File_Res> Get_SheetName_From_FILE(string filetype, string connString)
        {
            List<Get_SheetName_From_File_Res> List_Res = new List<Get_SheetName_From_File_Res>();

            if (filetype == ".xls" || filetype == ".xlsx")
            {
                using (OleDbConnection connection = new OleDbConnection(connString))
                {
                    int num = 0;
                    connection.Open();
                    DataTable dt = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    foreach (DataRow row1 in dt.Rows)
                    {
                        string sheetName = Convert.ToString(row1["TABLE_NAME"]);
                        if (!sheetName.EndsWith("_Deleted$") && sheetName.EndsWith("$"))
                        {
                            Get_SheetName_From_File_Res Res = new Get_SheetName_From_File_Res();
                            Res.Id = num + 1;
                            Res.SheetName = sheetName.Remove(sheetName.Length - 1, 1);
                            List_Res.Add(Res);
                        }
                        num++;
                    }
                }
            }
            return List_Res;
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult AddUpdate_SupplierStock([FromBody] JObject data)
        {
            VendorResponse obj = new VendorResponse();

            if (!string.IsNullOrEmpty(Convert.ToString(data)))
            {
                JObject test1 = JObject.Parse(data.ToString());
                obj = JsonConvert.DeserializeObject<VendorResponse>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());
            }

            string Return_Msg = "", Return_Status = "";
            int SupplierId = 0;
            long dt_SuppRes_COUNT = 0;
            string Message = string.Empty, Exception = string.Empty;

            string path = HttpContext.Current.Server.MapPath("~/Supplier_Stock_Upload.txt");

            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();

            try
            {
                DataTable table = new DataTable();
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (!string.IsNullOrEmpty(obj.SUPPLIER))
                    para.Add(db.CreateParam("SupplierId", DbType.Int32, ParameterDirection.Input, obj.SUPPLIER));
                else
                    para.Add(db.CreateParam("SupplierId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                DataTable dtSuppl = db.ExecuteSP("Get_SupplierMasterScheduler", para.ToArray(), false);

                if (dtSuppl != null && dtSuppl.Rows.Count > 0)
                {
                    TotCount = dtSuppl.Rows.Count;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("SupplierId", typeof(int));

                    for (int j = 0; j < dtSuppl.Rows.Count; j++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["SupplierId"] = Convert.ToInt32(dtSuppl.Rows[j]["Id"]);
                        dt.Rows.Add(dr);
                    }

                    db = new Database();
                    List<SqlParameter> dtpara = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("table", SqlDbType.Structured);
                    param.Value = dt;
                    dtpara.Add(param);

                    DataTable dtData = db.ExecuteSP("Add_SupplierAPI_Running_Status", dtpara.ToArray(), false);

                    for (int i = 0; i < dtSuppl.Rows.Count; i++)
                    {
                        string stockFrom = (Convert.ToString(dtSuppl.Rows[i]["APIType"]) == "WEB_API") ? "Web API" : "FTP";
                        try
                        {
                            SupplierId = Convert.ToInt32(dtSuppl.Rows[i]["Id"]);

                            db = new Database();
                            para = new List<IDbDataParameter>();

                            para.Add(db.CreateParam("SupplierId", DbType.Int64, ParameterDirection.Input, SupplierId));
                            DataTable dtSupplCol = db.ExecuteSP("Get_SupplierCol_OurCol_Merge", para.ToArray(), false);

                            if (dtSupplCol != null && dtSupplCol.Rows.Count > 0)
                            {
                                Supplier_Start_End(SupplierId, "Start");

                                string tempPath = Convert.ToString(dtSuppl.Rows[i]["FileLocation"]),
                                    APIFileName = Convert.ToString(dtSuppl.Rows[i]["FileName"]),
                                _API = String.Empty, UserName = String.Empty, Password = String.Empty, filename = String.Empty, filefullpath = String.Empty;

                                DataTable dt_APIRes = new DataTable();
                                DataTable dt_ColMap_Stk = new DataTable();



                                //dt_APIRes = Supplier_Stock_Get_From_His_WEB_API_AND_FTP(dtSuppl.Rows[i]["APIType"].ToString(),
                                //    dtSuppl.Rows[i]["SupplierResponseFormat"].ToString(),
                                //    dtSuppl.Rows[i]["SupplierURL"].ToString(),
                                //    dtSuppl.Rows[i]["SupplierAPIMethod"].ToString(),
                                //    dtSuppl.Rows[i]["UserName"].ToString(),
                                //    dtSuppl.Rows[i]["Password"].ToString(),
                                //    dtSuppl.Rows[i]["FileLocation"].ToString());

                                var (msg, ex, dt1) = Supplier_Stock_Get_From_His_WEB_API_AND_FTP(Convert.ToInt32(dtSuppl.Rows[i]["Id"]),
                                    Convert.ToString(dtSuppl.Rows[i]["APIType"]),
                                    Convert.ToString(dtSuppl.Rows[i]["SupplierResponseFormat"]),
                                    Convert.ToString(dtSuppl.Rows[i]["SupplierURL"]),
                                    Convert.ToString(dtSuppl.Rows[i]["SupplierAPIMethod"]),
                                    Convert.ToString(dtSuppl.Rows[i]["UserName"]),
                                    Convert.ToString(dtSuppl.Rows[i]["Password"]),
                                    Convert.ToString(dtSuppl.Rows[i]["FileLocation"]));

                                Message = msg;
                                Exception = ex;
                                dt_APIRes = dt1;

                                if (Message == "SUCCESS" && dt_APIRes != null && dt_APIRes.Rows.Count > 0)
                                {
                                    dt_SuppRes_COUNT = dt_APIRes.Rows.Count;
                                    dt_ColMap_Stk = ColumnMapping_In_SupplierStock(stockFrom, SupplierId, dt_APIRes, dtSupplCol);

                                    if (dt_ColMap_Stk != null && dt_ColMap_Stk.Rows.Count > 0)
                                    {
                                        db = new Database();
                                        dtpara = new List<SqlParameter>();

                                        SqlParameter param1 = new SqlParameter("tabledt", SqlDbType.Structured);
                                        param1.Value = dt_ColMap_Stk;
                                        dtpara.Add(param1);

                                        DataTable SupStkUploadDT = db.ExecuteSP("AddUpdate_SupplierStock", dtpara.ToArray(), false);

                                        if (SupStkUploadDT != null && SupStkUploadDT.Rows.Count > 0)
                                        {
                                            Return_Msg = Convert.ToString(SupStkUploadDT.Rows[0]["Message"]);

                                            sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                            sb.Append(Return_Msg + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                            sb.AppendLine("");
                                            File.AppendAllText(path, sb.ToString());
                                            sb.Clear();

                                            if (Convert.ToString(SupStkUploadDT.Rows[0]["Status"]) == "1")
                                            {
                                                if (!string.IsNullOrEmpty(Return_Msg))
                                                {
                                                    Return_Msg = RemoveBeforeWord(Return_Msg, "Stock");
                                                }
                                                Return_Status = "1";

                                                if (Convert.ToString(dtSuppl.Rows[i]["APIType"]) == "WEB_API")
                                                {
                                                    if (!Directory.Exists(tempPath))
                                                    {
                                                        Directory.CreateDirectory(tempPath);
                                                    }

                                                    string _tempPath = HostingEnvironment.MapPath("~/Temp/API/");
                                                    if (!Directory.Exists(_tempPath))
                                                    {
                                                        Directory.CreateDirectory(_tempPath);
                                                    }

                                                    if (dtSuppl.Rows[i]["LocationExportType"].ToString().ToUpper() == "XML")
                                                    {
                                                        filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".xml";
                                                        filefullpath = _tempPath + filename;
                                                        APIFileName = APIFileName + ".xml";

                                                        if (File.Exists(filefullpath))
                                                        {
                                                            File.Delete(filefullpath);
                                                        }

                                                        dt_APIRes.TableName = "Records";
                                                        dt_APIRes.WriteXml(filefullpath);
                                                    }
                                                    else if (dtSuppl.Rows[i]["LocationExportType"].ToString().ToUpper() == "CSV")
                                                    {
                                                        filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".csv";
                                                        filefullpath = _tempPath + filename;
                                                        APIFileName = APIFileName + ".csv";

                                                        if (File.Exists(filefullpath))
                                                        {
                                                            File.Delete(filefullpath);
                                                        }

                                                        StringBuilder sb1 = new StringBuilder();
                                                        IEnumerable<string> columnNames = dt_APIRes.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                                                        sb1.AppendLine(string.Join(",", columnNames));

                                                        foreach (DataRow row in dt_APIRes.Rows)
                                                        {
                                                            IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString().Replace(",", " "));
                                                            sb1.AppendLine(string.Join(",", fields));
                                                        }
                                                        File.WriteAllText(filefullpath, sb1.ToString());
                                                    }
                                                    else if (dtSuppl.Rows[i]["LocationExportType"].ToString().ToUpper() == "EXCEL (.XLSX)" || dtSuppl.Rows[i]["LocationExportType"].ToString().ToUpper() == "EXCEL (.XLS)")
                                                    {
                                                        if (dtSuppl.Rows[i]["LocationExportType"].ToString().ToUpper() == "EXCEL (.XLSX)")
                                                        {
                                                            filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".xlsx";
                                                            filefullpath = _tempPath + filename;
                                                            APIFileName = APIFileName + ".xlsx";
                                                        }
                                                        else if (dtSuppl.Rows[i]["LocationExportType"].ToString().ToUpper() == "EXCEL (.XLS)")
                                                        {
                                                            filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".xls";
                                                            filefullpath = _tempPath + filename;
                                                            APIFileName = APIFileName + ".xls";
                                                        }

                                                        if (File.Exists(filefullpath))
                                                        {
                                                            File.Delete(filefullpath);
                                                        }

                                                        FileInfo newFile = new FileInfo(filefullpath);
                                                        using (ExcelPackage pck = new ExcelPackage(newFile))
                                                        {
                                                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(APIFileName);
                                                            pck.Workbook.Properties.Title = "API";
                                                            ws.Cells["A1"].LoadFromDataTable(dt_APIRes, true);

                                                            ws.View.FreezePanes(2, 1);
                                                            var allCells = ws.Cells[ws.Dimension.Address];
                                                            allCells.AutoFilter = true;
                                                            allCells.AutoFitColumns();

                                                            int rowStart = ws.Dimension.Start.Row;
                                                            int rowEnd = ws.Dimension.End.Row;
                                                            removingGreenTagWarning(ws, ws.Cells[1, 1, rowEnd, 100].Address);

                                                            var headerCells = ws.Cells[1, 1, 1, ws.Dimension.Columns];
                                                            headerCells.Style.Font.Bold = true;
                                                            headerCells.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                                            headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                                            headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSkyBlue);
                                                            pck.Save();
                                                        }
                                                    }
                                                    else if (dtSuppl.Rows[i]["LocationExportType"].ToString().ToUpper() == "JSON (FILE)")
                                                    {
                                                        filename = DateTime.Now.ToString("dd-MM-yyyy HHmmssfff") + ".json";
                                                        filefullpath = _tempPath + filename;
                                                        APIFileName = APIFileName + ".json";

                                                        if (File.Exists(filefullpath))
                                                        {
                                                            File.Delete(filefullpath);
                                                        }
                                                        string json = DataTableToConvert.DataTableToJSONWithStringBuilder(dt_APIRes);
                                                        File.WriteAllText(filefullpath, json);
                                                    }

                                                    if (File.Exists(filefullpath))
                                                    {
                                                        FileInfo fi = new FileInfo(filefullpath);
                                                        long size = fi.Length / 1024;
                                                        if (size > 1)
                                                        {
                                                            File.Copy(filefullpath, tempPath + "\\" + APIFileName, true);

                                                            File.Delete(filefullpath);

                                                            ApiLog(SupplierId, true, SupStkUploadDT.Rows[0]["Message"].ToString());
                                                        }
                                                        else
                                                        {
                                                            ApiLog(SupplierId, false, "Total Record " + dt_SuppRes_COUNT + ", File Created " + size + " KB");
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    ApiLog(SupplierId, true, SupStkUploadDT.Rows[0]["Message"].ToString());
                                                }
                                            }
                                            else
                                            {
                                                Return_Status = "0";
                                                ApiLog(SupplierId, false, SupStkUploadDT.Rows[0]["Message"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            Return_Msg = dtSuppl.Rows[i]["SupplierName"].ToString() + " Supplier Stock Upload Failed";
                                            Return_Status = "0";

                                            ApiLog(SupplierId, false, Return_Msg);

                                            sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                            sb.Append(Return_Msg + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                            sb.AppendLine("");
                                            File.AppendAllText(path, sb.ToString());
                                            sb.Clear();
                                        }
                                    }
                                    else
                                    {
                                        Return_Msg = "Column Setting Mapping Failed From " + dtSuppl.Rows[i]["SupplierName"].ToString() + " Supplier's " + stockFrom;
                                        Return_Status = "0";
                                        ApiLog(SupplierId, false, Return_Msg);

                                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                        sb.Append(Return_Msg + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                        sb.AppendLine("");
                                        File.AppendAllText(path, sb.ToString());
                                        sb.Clear();
                                    }
                                }
                                else
                                {
                                    string _msg = ((Message != "ERROR") ? (Message + ((!string.IsNullOrEmpty(Exception)) ? " " + Exception + " " : "") + " From " + dtSuppl.Rows[i]["SupplierName"].ToString() + " Supplier's " + stockFrom) : ("Stock Not Found From " + dtSuppl.Rows[i]["SupplierName"].ToString() + " Supplier's " + stockFrom));
                                    Return_Msg = _msg;
                                    Return_Status = "0";
                                    ApiLog(SupplierId, false, _msg);

                                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                    sb.Append(_msg + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                    sb.AppendLine("");
                                    File.AppendAllText(path, sb.ToString());
                                    sb.Clear();
                                }
                            }
                            else
                            {
                                Return_Msg = "Column Setting Not Found From " + dtSuppl.Rows[i]["SupplierName"].ToString() + " Supplier's " + stockFrom;
                                Return_Status = "0";
                                ApiLog(SupplierId, false, Return_Msg);

                                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                sb.Append(Return_Msg + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                sb.AppendLine("");
                                File.AppendAllText(path, sb.ToString());
                                sb.Clear();
                            }

                        }
                        catch (Exception ex)
                        {
                            Return_Msg = ex.Message.ToString() + " " + ex.StackTrace.ToString() + " From " + stockFrom;
                            Return_Status = "0";
                            Supplier_Start_End(SupplierId, "End");
                            ApiLog(SupplierId, false, ex.Message.ToString() + " " + ex.StackTrace.ToString() + " From " + stockFrom);

                            sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                            sb.Append(ex.Message.ToString() + " " + ex.StackTrace.ToString() + " From " + stockFrom + ", Log Time: " + DateTime.Now.ToString("dd -MM-yyyy hh:mm:ss tt"));
                            sb.AppendLine("");
                            File.AppendAllText(path, sb.ToString());
                            sb.Clear();
                        }

                    }


                }
            }
            catch (Exception ex)
            {
                Return_Msg = ex.Message.ToString() + " " + ex.StackTrace.ToString();
                Return_Status = "0";
                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                sb.Append(ex.Message.ToString() + " " + ex.StackTrace.ToString() + ", Log Time: " + DateTime.Now.ToString("dd -MM-yyyy hh:mm:ss tt"));
                sb.AppendLine("");
                File.AppendAllText(path, sb.ToString());
                sb.Clear();
            }

            return Ok(new CommonResponse
            {
                Message = Return_Msg,
                Status = Return_Status,
                Error = ""
            });
        }
        public static string RemoveBeforeWord(string input, string word)
        {
            int index = input.IndexOf(word);

            if (index >= 0)
            {
                return input.Substring(index);
            }
            else
            {
                return input;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult AddUpdate_SupplierStock_FromFile([FromBody] JObject data)
        {
            Data_Get_From_File_Req req = new Data_Get_From_File_Req();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(data)))
                {
                    JObject test1 = JObject.Parse(data.ToString());
                    req = JsonConvert.DeserializeObject<Data_Get_From_File_Req>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());

                    Database db = new Database();
                    List<IDbDataParameter> para;
                    para = new List<IDbDataParameter>();

                    para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, req.Id));

                    DataTable dt = db.ExecuteSP("Get_Stock_FileUpload_Request", para.ToArray(), false);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string Request = Convert.ToString(dt.Rows[0]["Stock_FileUpload_Request"]);
                        req = JsonConvert.DeserializeObject<Data_Get_From_File_Req>(Request.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                string path = HttpContext.Current.Server.MapPath("~/Supplier_Stock_Upload.txt");

                if (!File.Exists(@"" + path + ""))
                {
                    File.Create(@"" + path + "").Dispose();
                }
                StringBuilder sb = new StringBuilder();

                DataTable Stock_dt = new DataTable();
                string str2 = Path.GetExtension(req.FilePath).ToLower();
                if (str2 == ".xls")
                {
                    string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + req.FilePath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
                    //Stock_dt = ConvertXLStoDataTable("", connString);
                    Stock_dt = Convert_FILE_To_DataTable(".xls", connString, req.SheetName, req.SupplierId);
                }
                else if (str2 == ".xlsx")
                {
                    //string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + req.FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + req.FilePath + ";Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;'";
                    //Stock_dt = ConvertXSLXtoDataTable("", connString);
                    Stock_dt = Convert_FILE_To_DataTable(".xlsx", connString, req.SheetName, req.SupplierId);
                }
                else if (str2 == ".csv")
                {
                    //Stock_dt = ConvertCSVtoDataTable(req.FilePath);
                    Stock_dt = Convert_FILE_To_DataTable(".csv", req.FilePath, "", req.SupplierId);
                }
                req.SheetName = req.SheetName.Replace("_ALL_SHEET_$", "ALL$");

                Int32 VishindasHolaram_Lakhi_Id = Convert.ToInt32(ConfigurationManager.AppSettings["VishindasHolaram_Lakhi_Id"]);
                Int32 StarRays_Id = Convert.ToInt32(ConfigurationManager.AppSettings["StarRays_Id"]);
                Int32 JewelParadise_Id = Convert.ToInt32(ConfigurationManager.AppSettings["JewelParadise_Id"]);
                Int32 Diamart_Id = Convert.ToInt32(ConfigurationManager.AppSettings["Diamart_Id"]);
                Int32 Laxmi_Id = Convert.ToInt32(ConfigurationManager.AppSettings["Laxmi_Id"]);
                Int32 JB_Id = Convert.ToInt32(ConfigurationManager.AppSettings["JB_Id"]);
                Int32 Aspeco_Id = Convert.ToInt32(ConfigurationManager.AppSettings["Aspeco_Id"]);
                Int32 KBS_Id = Convert.ToInt32(ConfigurationManager.AppSettings["KBS_Id"]);

                if (req.SupplierId == VishindasHolaram_Lakhi_Id)
                {
                    Stock_dt = Lakhi_TableCrown_BlackWhite(Stock_dt);
                }
                else if (req.SupplierId == StarRays_Id)
                {
                    Stock_dt = StarRays_TableCrownPav_Open(Stock_dt);
                }
                else if (req.SupplierId == JewelParadise_Id)
                {
                    Stock_dt = JewelParadise_Shade(Stock_dt);
                }
                else if (req.SupplierId == Diamart_Id)
                {
                    Stock_dt = Diamart_Shade(Stock_dt);
                }
                else if (req.SupplierId == Laxmi_Id)
                {
                    Stock_dt = Laxmi_Grading(Stock_dt);
                }
                else if (req.SupplierId == JB_Id)
                {
                    Stock_dt = JB(Stock_dt);
                }
                else if (req.SupplierId == Aspeco_Id)
                {
                    Stock_dt = Aspeco(Stock_dt);
                }
                else if (req.SupplierId == KBS_Id)
                {
                    Stock_dt = KBS(Stock_dt);
                }

                if (Stock_dt != null && Stock_dt.Rows.Count > 0)
                {
                    Database db = new Database();
                    List<IDbDataParameter> para = new List<IDbDataParameter>();

                    if (req.SupplierId > 0)
                        para.Add(db.CreateParam("SupplierId", DbType.Int64, ParameterDirection.Input, req.SupplierId));
                    else
                        para.Add(db.CreateParam("SupplierId", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                    DataTable dtSupplCol = db.ExecuteSP("Get_SupplierCol_OurCol_Merge_FromFile", para.ToArray(), false);

                    if (dtSupplCol != null && dtSupplCol.Rows.Count > 0)
                    {
                        DataTable dt_ColMap_Stk = ColumnMapping_In_SupplierStock("File", req.SupplierId, Stock_dt, dtSupplCol);

                        if (dt_ColMap_Stk != null && dt_ColMap_Stk.Rows.Count > 0)
                        {
                            db = new Database();
                            List<SqlParameter> dtpara = new List<SqlParameter>();

                            SqlParameter param1 = new SqlParameter("tabledt", SqlDbType.Structured);
                            param1.Value = dt_ColMap_Stk;
                            dtpara.Add(param1);

                            DataTable SupStkUploadDT = db.ExecuteSP("AddUpdate_SupplierStock", dtpara.ToArray(), false);

                            if (SupStkUploadDT != null && SupStkUploadDT.Rows.Count > 0)
                            {
                                ApiLog(req.SupplierId, true, SupStkUploadDT.Rows[0]["Message"].ToString());

                                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                sb.Append(SupStkUploadDT.Rows[0]["Message"].ToString() + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                sb.AppendLine("");
                                File.AppendAllText(path, sb.ToString());
                                sb.Clear();

                                if (SupStkUploadDT.Rows[0]["Message"].ToString().Contains("SUCCESS"))
                                {
                                    return Ok(new CommonResponse
                                    {
                                        Error = "",
                                        Message = "Stock Upload Has Been Successfully From " + req.SupplierName + " Supplier's File" + ((str2 == ".xls" || str2 == ".xlsx") ? " in " + req.SheetName.Remove(req.SheetName.Length - 1, 1) + " Sheet." : "."),
                                        Status = "1"
                                    });
                                }
                                else
                                {
                                    return Ok(new CommonResponse
                                    {
                                        Error = "",
                                        Message = SupStkUploadDT.Rows[0]["Message"].ToString(),
                                        Status = "0"
                                    });
                                }
                            }
                            else
                            {
                                ApiLog(req.SupplierId, false, "Stock Upload Failed From " + req.SupplierName + " Supplier's File" + ((str2 == ".xls" || str2 == ".xlsx") ? " in " + req.SheetName.Remove(req.SheetName.Length - 1, 1) + " Sheet." : "."));

                                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                sb.Append("Stock Upload Failed From " + req.SupplierName + " Supplier's File" + ((str2 == ".xls" || str2 == ".xlsx") ? " in " + req.SheetName.Remove(req.SheetName.Length - 1, 1) + " Sheet." : ".") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                sb.AppendLine("");
                                File.AppendAllText(path, sb.ToString());
                                sb.Clear();

                                return Ok(new CommonResponse
                                {
                                    Error = "",
                                    Message = "Stock Upload Failed From " + req.SupplierName + " Supplier's File" + ((str2 == ".xls" || str2 == ".xlsx") ? " in " + req.SheetName.Remove(req.SheetName.Length - 1, 1) + " Sheet." : "."),
                                    Status = "0"
                                });
                            }
                        }
                        else
                        {
                            ApiLog(req.SupplierId, false, "Column Setting Mapping Failed From " + req.SupplierName + " Supplier's File" + ((str2 == ".xls" || str2 == ".xlsx") ? " in " + req.SheetName.Remove(req.SheetName.Length - 1, 1) + " Sheet." : "."));
                            sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                            sb.Append("Column Setting Mapping Failed From " + req.SupplierName + " Supplier's File" + ((str2 == ".xls" || str2 == ".xlsx") ? " in " + req.SheetName.Remove(req.SheetName.Length - 1, 1) + " Sheet." : ".") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                            sb.AppendLine("");
                            File.AppendAllText(path, sb.ToString());
                            sb.Clear();

                            return Ok(new CommonResponse
                            {
                                Error = "",
                                Message = "Column Setting Mapping Failed From " + req.SupplierName + " Supplier's File" + ((str2 == ".xls" || str2 == ".xlsx") ? " in " + req.SheetName.Remove(req.SheetName.Length - 1, 1) + " Sheet." : "."),
                                Status = "0"
                            });
                        }
                    }
                    else
                    {
                        ApiLog(req.SupplierId, false, "Column Setting Not Found From " + req.SupplierName + " Supplier's File" + ((str2 == ".xls" || str2 == ".xlsx") ? " in " + req.SheetName.Remove(req.SheetName.Length - 1, 1) + " Sheet." : "."));

                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append("Column Setting Not Found From " + req.SupplierName + " Supplier's File" + ((str2 == ".xls" || str2 == ".xlsx") ? " in " + req.SheetName.Remove(req.SheetName.Length - 1, 1) + " Sheet." : ".") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();

                        return Ok(new CommonResponse
                        {
                            Error = "",
                            Message = "Column Setting Not Found From " + req.SupplierName + " Supplier's File" + ((str2 == ".xls" || str2 == ".xlsx") ? " in " + req.SheetName.Remove(req.SheetName.Length - 1, 1) + " Sheet." : "."),
                            Status = "0"
                        });
                    }
                }
                else
                {
                    //ApiLog(req.SupplierId, false, "Stock Not Found From " + req.SupplierName + " Supplier's File");
                    ApiLog(req.SupplierId, false, "Stock not found From " + req.SupplierName + " Supplier's File" + ((str2 == ".xls" || str2 == ".xlsx") ? " in " + req.SheetName.Remove(req.SheetName.Length - 1, 1) + " Sheet." : "."));

                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    //sb.Append("Stock Not Found From " + req.SupplierName + " Supplier's File, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.Append("Stock not found From " + req.SupplierName + " Supplier's File" + ((str2 == ".xls" || str2 == ".xlsx") ? " in " + req.SheetName.Remove(req.SheetName.Length - 1, 1) + " Sheet." : ".") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();

                    return Ok(new CommonResponse
                    {
                        Error = "",
                        Message = "Stock not found From " + req.SupplierName + " Supplier's File" + ((str2 == ".xls" || str2 == ".xlsx") ? " in " + req.SheetName.Remove(req.SheetName.Length - 1, 1) + " Sheet." : "."),
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult API_RESPONSE_CHECK()
        {
            var (msg, exe, dt) = Supplier_Stock_Get_From_His_WEB_API_AND_FTP(
                                            5,
                                            "WEB_API", //APIType
                                            "JSON", //SupplierResponseFormat
                                            "http://www.starlightdiamonds.in/api/getstock", //SupplierURL
                                            "GET", //SupplierAPIMethod
                                            "", //UserName
                                            "", //Password
                                            "" //FileLocation
                                            );

            return Ok(new CommonResponse
            {
                Error = "",
                Message = "Message :: " + msg + " Exception :: " + exe + " " + " Total Record " + (dt != null ? dt.Rows.Count : 0),
                Status = "1"
            });
        }



        [HttpPost]
        public IHttpActionResult Get_ColumnSetting_UserWise([FromBody] JObject data)
        {
            GetUsers_Res req = new GetUsers_Res();
            try
            {
                req = JsonConvert.DeserializeObject<GetUsers_Res>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_ColumnSetting_UserWise_Res>
                {
                    Data = new List<Get_ColumnSetting_UserWise_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });

            }
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                if (req.UserId > 0)
                    para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.UserId));
                else
                    para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Get_ColumnSetting_UserWise", para.ToArray(), false);

                List<Get_ColumnSetting_UserWise_Res> List_Res = new List<Get_ColumnSetting_UserWise_Res>();

                if (dt != null && dt.Rows.Count > 0)
                {
                    List_Res = DataTableExtension.ToList<Get_ColumnSetting_UserWise_Res>(dt);
                }

                return Ok(new ServiceResponse<Get_ColumnSetting_UserWise_Res>
                {
                    Data = List_Res,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_ColumnSetting_UserWise_Res>
                {
                    Data = new List<Get_ColumnSetting_UserWise_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult AddUpdate_ColumnSetting_UserWise([FromBody] JObject data)
        {
            Save_ColumnSetting_UserWise req = new Save_ColumnSetting_UserWise();
            try
            {
                req = JsonConvert.DeserializeObject<Save_ColumnSetting_UserWise>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                Database db = new Database();
                CommonResponse resp = new CommonResponse();
                List<IDbDataParameter> para;

                DataTable dt = new DataTable();

                dt.Columns.Add("UserId", typeof(int));
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("OrderBy", typeof(int));

                if (req.BUYER.Count() > 0)
                {
                    db = new Database();
                    para = new List<IDbDataParameter>();
                    para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.BUYER[0].UserId));
                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "BUYER"));
                    db.ExecuteSP("Delete_ColumnSetting_UserWise", para.ToArray(), false);

                    for (int i = 0; i < req.BUYER.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["UserId"] = req.BUYER[i].UserId;
                        dr["Id"] = req.BUYER[i].Id;
                        dr["OrderBy"] = req.BUYER[i].OrderBy;

                        dt.Rows.Add(dr);
                    }
                }

                if (req.SUPPLIER.Count() > 0)
                {
                    db = new Database();
                    para = new List<IDbDataParameter>();
                    para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.SUPPLIER[0].UserId));
                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "SUPPLIER"));
                    db.ExecuteSP("Delete_ColumnSetting_UserWise", para.ToArray(), false);

                    for (int i = 0; i < req.SUPPLIER.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["UserId"] = req.SUPPLIER[i].UserId;
                        dr["Id"] = req.SUPPLIER[i].Id;
                        dr["OrderBy"] = req.SUPPLIER[i].OrderBy;

                        dt.Rows.Add(dr);
                    }
                }

                if (req.CUSTOMER.Count() > 0)
                {
                    db = new Database();
                    para = new List<IDbDataParameter>();
                    para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.CUSTOMER[0].UserId));
                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "CUSTOMER"));
                    db.ExecuteSP("Delete_ColumnSetting_UserWise", para.ToArray(), false);

                    for (int i = 0; i < req.CUSTOMER.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["UserId"] = req.CUSTOMER[i].UserId;
                        dr["Id"] = req.CUSTOMER[i].Id;
                        dr["OrderBy"] = req.CUSTOMER[i].OrderBy;

                        dt.Rows.Add(dr);
                    }
                }


                DataTable dtData = new DataTable();
                List<SqlParameter> para1 = new List<SqlParameter>();
                db = new Database();
                SqlParameter param = new SqlParameter("table", SqlDbType.Structured);
                param.Value = dt;
                para1.Add(param);

                dtData = db.ExecuteSP("AddUpdate_ColumnSetting_UserWise", para1.ToArray(), false);

                if (dtData != null && dtData.Rows.Count > 0)
                {
                    resp.Status = dtData.Rows[0]["Status"].ToString();
                    resp.Message = dtData.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Get_SearchStock_ColumnSetting([FromBody] JObject data)
        {
            Get_SearchStock_ColumnSetting_Req req = new Get_SearchStock_ColumnSetting_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Get_SearchStock_ColumnSetting_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SearchStock_ColumnSetting_Res>
                {
                    Data = new List<Get_SearchStock_ColumnSetting_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                req.UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.UserId));
                para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, req.Type));

                DataTable dt = db.ExecuteSP("Get_SearchStock_ColumnSetting", para.ToArray(), false);

                List<Get_SearchStock_ColumnSetting_Res> List_Res = new List<Get_SearchStock_ColumnSetting_Res>();

                if (dt != null && dt.Rows.Count > 0)
                {
                    List_Res = DataTableExtension.ToList<Get_SearchStock_ColumnSetting_Res>(dt);
                }

                return Ok(new ServiceResponse<Get_SearchStock_ColumnSetting_Res>
                {
                    Data = List_Res,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SearchStock_ColumnSetting_Res>
                {
                    Data = new List<Get_SearchStock_ColumnSetting_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        #region Task Scheduler : Data Upload From Oracle

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult RapaPort_Data_Upload_Ora()
        {
            string path = HttpContext.Current.Server.MapPath("~/RapaPort_Data_Upload_From_Oracle_Log.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();

            try
            {
                string fromtime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                Database db = new Database(Request);

                Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
                List<OracleParameter> paramList = new List<OracleParameter>();

                OracleParameter param1 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param1.Direction = ParameterDirection.Output;
                paramList.Add(param1);

                System.Data.DataTable dt = oracleDbAccess.CallSP("get_rap", paramList);

                int Count = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    Count = dt.Rows.Count;

                    List<SqlParameter> para = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("tableInq", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    DataTable dt1 = db.ExecuteSP("RapaPort_Data_Ora_Insert", para.ToArray(), false);

                    string Message = string.Empty;
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        Message = dt1.Rows[0]["Message"].ToString();
                    }
                    string totime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                    if (Message == "SUCCESS")
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append(Message + " " + Count + " RapaPort Data Found, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = Message + " " + Count + " RapaPort Data Found, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                    else
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append("RapaPort Data Upload in Issue" + (!string.IsNullOrEmpty(Message) ? " " + Message : "") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = "RapaPort Data Upload in Issue" + (!string.IsNullOrEmpty(Message) ? " " + Message : "") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                }
                else
                {
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append("No RapaPort Data Found From Oracle, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();
                    return Ok(new CommonResponse
                    {
                        Message = "No RapaPort Data Found From Oracle",
                        Status = "1",
                        Error = ""
                    });
                }

            }
            catch (Exception ex)
            {
                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                sb.Append(ex.Message + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                sb.AppendLine("");
                File.AppendAllText(path, sb.ToString());
                sb.Clear();
                return Ok(new CommonResponse
                {
                    Message = ex.Message,
                    Status = "0",
                    Error = ex.StackTrace
                });
            }

        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult get_stock_disc_Ora()
        {
            string path = HttpContext.Current.Server.MapPath("~/get_stock_disc_Upload_From_Oracle_Log.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();

            try
            {
                string fromtime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);
                DateTime date = DateTime.Now;

                Database db = new Database(Request);

                Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
                List<OracleParameter> paramList = new List<OracleParameter>();

                OracleParameter param1 = new OracleParameter("p_for_comp", OracleDbType.Int32);
                param1.Value = 1;
                paramList.Add(param1);

                param1 = new OracleParameter("p_for_date", OracleDbType.Date);
                param1.Value = string.Format("{0:dd-MMM-yyyy}", date);
                paramList.Add(param1);

                param1 = new OracleParameter("p_pointer_flag", OracleDbType.NVarchar2);
                param1.Value = "N";
                paramList.Add(param1);

                param1 = new OracleParameter("p_color_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_purity_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_cut_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_fls_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_shade_grp", OracleDbType.NVarchar2);
                param1.Value = "WHITE";
                paramList.Add(param1);

                param1 = new OracleParameter("p_shape", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_sub_pointer", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_kts", OracleDbType.NVarchar2);
                param1.Value = "N";
                paramList.Add(param1);

                param1 = new OracleParameter("p_kts_word", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_length", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_length", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_width", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_width", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_depth", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_depth", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_table_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_table_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_depth_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_depth_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_clarity_grade", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param1.Direction = ParameterDirection.Output;
                paramList.Add(param1);

                param1 = new OracleParameter("p_luster_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_shade_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_all_luster", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_kts_grade_flag", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_stone_clarity_flag", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                System.Data.DataTable dt = oracleDbAccess.CallSP("get_stock_disc", paramList);
                //stock_disc_gt_clg

                int Count = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    Count = dt.Rows.Count;

                    List<SqlParameter> para = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("tableInq", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    DataTable dt1 = db.ExecuteSP("get_stock_disc_ora_Insert", para.ToArray(), false);

                    string Message = string.Empty;
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        Message = dt1.Rows[0]["Message"].ToString();
                    }
                    string totime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                    if (Message == "SUCCESS")
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append(Message + " " + Count + " get_stock_disc Data Found, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = Message + " " + Count + " get_stock_disc Data Found, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                    else
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append("get_stock_disc Data Upload in Issue" + (!string.IsNullOrEmpty(Message) ? " " + Message : "") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = "get_stock_disc Data Upload in Issue" + (!string.IsNullOrEmpty(Message) ? " " + Message : "") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                }
                else
                {
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append("No get_stock_disc Data Found From Oracle, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();
                    return Ok(new CommonResponse
                    {
                        Message = "No get_stock_disc Data Found From Oracle",
                        Status = "1",
                        Error = ""
                    });
                }

            }
            catch (Exception ex)
            {
                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                sb.Append(ex.Message + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                sb.AppendLine("");
                File.AppendAllText(path, sb.ToString());
                sb.Clear();
                return Ok(new CommonResponse
                {
                    Message = ex.Message,
                    Status = "0",
                    Error = ex.StackTrace
                });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult get_stock_kts_Ora()
        {
            string path = HttpContext.Current.Server.MapPath("~/get_stock_kts_Upload_From_Oracle_Log.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();

            try
            {
                string fromtime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);
                DateTime date = DateTime.Now;

                Database db = new Database(Request);

                Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
                List<OracleParameter> paramList = new List<OracleParameter>();

                OracleParameter param1 = new OracleParameter("p_for_comp", OracleDbType.Int32);
                param1.Value = 1;
                paramList.Add(param1);

                param1 = new OracleParameter("p_for_date", OracleDbType.Date);
                param1.Value = string.Format("{0:dd-MMM-yyyy}", date);
                paramList.Add(param1);

                param1 = new OracleParameter("p_pointer_flag", OracleDbType.NVarchar2);
                param1.Value = "N";
                paramList.Add(param1);

                param1 = new OracleParameter("p_color_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_purity_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_cut_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_fls_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_shade_grp", OracleDbType.NVarchar2);
                param1.Value = "WHITE";
                paramList.Add(param1);

                param1 = new OracleParameter("p_shape", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_sub_pointer", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_kts", OracleDbType.NVarchar2);
                param1.Value = "N";
                paramList.Add(param1);

                param1 = new OracleParameter("p_kts_word", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_length", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_length", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_width", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_width", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_depth", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_depth", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_table_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_table_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_depth_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_depth_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_clarity_grade", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param1.Direction = ParameterDirection.Output;
                paramList.Add(param1);

                param1 = new OracleParameter("p_luster_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_shade_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_all_luster", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_kts_grade_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_stone_clarity_flag", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                System.Data.DataTable dt = oracleDbAccess.CallSP("get_stock_disc", paramList);
                //stock_disc_gt_clg

                int Count = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    Count = dt.Rows.Count;

                    List<SqlParameter> para = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("tableInq", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    DataTable dt1 = db.ExecuteSP("get_stock_kts_ora_Insert", para.ToArray(), false);

                    string Message = string.Empty;
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        Message = dt1.Rows[0]["Message"].ToString();
                    }
                    string totime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                    if (Message == "SUCCESS")
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append(Message + " " + Count + " get_stock_kts Data Found, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = Message + " " + Count + " get_stock_kts Data Found, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                    else
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append("get_stock_kts Data Upload in Issue" + (!string.IsNullOrEmpty(Message) ? " " + Message : "") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = "get_stock_kts Data Upload in Issue" + (!string.IsNullOrEmpty(Message) ? " " + Message : "") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                }
                else
                {
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append("No get_stock_kts Data Found From Oracle, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();
                    return Ok(new CommonResponse
                    {
                        Message = "No get_stock_kts Data Found From Oracle",
                        Status = "1",
                        Error = ""
                    });
                }

            }
            catch (Exception ex)
            {
                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                sb.Append(ex.Message + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                sb.AppendLine("");
                File.AppendAllText(path, sb.ToString());
                sb.Clear();
                return Ok(new CommonResponse
                {
                    Message = ex.Message,
                    Status = "0",
                    Error = ex.StackTrace
                });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult get_sal_disc_new_Ora()
        {
            string path = HttpContext.Current.Server.MapPath("~/get_sal_disc_new_Upload_From_Oracle_Log.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();

            try
            {
                string fromtime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);
                DateTime from_date = DateTime.Now.AddDays(-31);
                DateTime to_date = DateTime.Now;

                Database db = new Database(Request);

                Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
                List<OracleParameter> paramList = new List<OracleParameter>();

                OracleParameter param1 = new OracleParameter("p_from_date", OracleDbType.Date);
                param1.Value = string.Format("{0:dd-MMM-yyyy}", from_date);
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_date", OracleDbType.Date);
                param1.Value = string.Format("{0:dd-MMM-yyyy}", to_date);
                paramList.Add(param1);

                param1 = new OracleParameter("p_pointer_flag", OracleDbType.NVarchar2);
                param1.Value = "N";
                paramList.Add(param1);

                param1 = new OracleParameter("p_color_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_purity_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_cut_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_fls_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_shade_grp", OracleDbType.NVarchar2);
                param1.Value = "WHITE";
                paramList.Add(param1);

                param1 = new OracleParameter("p_shape", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_subpointer_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_kts", OracleDbType.NVarchar2);
                param1.Value = "N";
                paramList.Add(param1);

                param1 = new OracleParameter("p_kts_word", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_length", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_length", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_width", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_width", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_depth", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_depth", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_table_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_table_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_depth_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_depth_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_clarity_grade", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param1.Direction = ParameterDirection.Output;
                paramList.Add(param1);

                param1 = new OracleParameter("p_kts_grd_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_stone_clarity_flag", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_shade_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_luster_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_pre_sold_flag", OracleDbType.NVarchar2);
                param1.Value = "B";
                paramList.Add(param1);

                System.Data.DataTable dt = oracleDbAccess.CallSP("get_sal_disc_new", paramList);

                int Count = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    Count = dt.Rows.Count;

                    List<SqlParameter> para = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("tableInq", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    DataTable dt1 = db.ExecuteSP("get_sal_disc_new_ora_Insert", para.ToArray(), false);

                    string Message = string.Empty;
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        Message = dt1.Rows[0]["Message"].ToString();
                    }
                    string totime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                    if (Message == "SUCCESS")
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append(Message + " " + Count + " get_sal_disc_new Data Found, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = Message + " " + Count + " get_sal_disc_new Data Found, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                    else
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append("get_sal_disc_new Data Upload in Issue" + (!string.IsNullOrEmpty(Message) ? " " + Message : "") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = "get_sal_disc_new Data Upload in Issue" + (!string.IsNullOrEmpty(Message) ? " " + Message : "") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                }
                else
                {
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append("No get_sal_disc_new Data Found From Oracle, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();
                    return Ok(new CommonResponse
                    {
                        Message = "No get_sal_disc_new Data Found From Oracle",
                        Status = "1",
                        Error = ""
                    });
                }

            }
            catch (Exception ex)
            {
                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                sb.Append(ex.Message + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                sb.AppendLine("");
                File.AppendAllText(path, sb.ToString());
                sb.Clear();
                return Ok(new CommonResponse
                {
                    Message = ex.Message,
                    Status = "0",
                    Error = ex.StackTrace
                });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult get_sal_clg_new_Ora()
        {
            string path = HttpContext.Current.Server.MapPath("~/get_sal_clg_new_Upload_From_Oracle_Log.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();

            try
            {
                string fromtime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);
                DateTime from_date = DateTime.Now.AddDays(-31);
                DateTime to_date = DateTime.Now;

                Database db = new Database(Request);

                Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
                List<OracleParameter> paramList = new List<OracleParameter>();

                OracleParameter param1 = new OracleParameter("p_from_date", OracleDbType.Date);
                param1.Value = string.Format("{0:dd-MMM-yyyy}", from_date);
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_date", OracleDbType.Date);
                param1.Value = string.Format("{0:dd-MMM-yyyy}", to_date);
                paramList.Add(param1);

                param1 = new OracleParameter("p_pointer_flag", OracleDbType.NVarchar2);
                param1.Value = "N";
                paramList.Add(param1);

                param1 = new OracleParameter("p_color_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_purity_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_cut_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_fls_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_shade_grp", OracleDbType.NVarchar2);
                param1.Value = "WHITE";
                paramList.Add(param1);

                param1 = new OracleParameter("p_shape", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_subpointer_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_kts", OracleDbType.NVarchar2);
                param1.Value = "N";
                paramList.Add(param1);

                param1 = new OracleParameter("p_kts_word", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_length", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_length", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_width", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_width", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_depth", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_depth", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_table_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_table_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_from_depth_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_to_depth_per", OracleDbType.Int32);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_clarity_grade", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param1.Direction = ParameterDirection.Output;
                paramList.Add(param1);

                param1 = new OracleParameter("p_kts_grd_flag", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_stone_clarity_flag", OracleDbType.NVarchar2);
                param1.Value = DBNull.Value;
                paramList.Add(param1);

                param1 = new OracleParameter("p_shade_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_luster_flag", OracleDbType.NVarchar2);
                param1.Value = "Y";
                paramList.Add(param1);

                param1 = new OracleParameter("p_pre_sold_flag", OracleDbType.NVarchar2);
                param1.Value = "B";
                paramList.Add(param1);

                System.Data.DataTable dt = oracleDbAccess.CallSP("get_sal_disc_new", paramList);

                int Count = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    Count = dt.Rows.Count;

                    List<SqlParameter> para = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("tableInq", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    DataTable dt1 = db.ExecuteSP("get_sal_clg_new_ora_Insert", para.ToArray(), false);

                    string Message = string.Empty;
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        Message = dt1.Rows[0]["Message"].ToString();
                    }
                    string totime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                    if (Message == "SUCCESS")
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append(Message + " " + Count + " get_sal_clg_new Data Found, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = Message + " " + Count + " get_sal_clg_new Data Found, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                    else
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append("get_sal_clg_new Data Upload in Issue" + (!string.IsNullOrEmpty(Message) ? " " + Message : "") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = "get_sal_clg_new Data Upload in Issue" + (!string.IsNullOrEmpty(Message) ? " " + Message : "") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                }
                else
                {
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append("No get_sal_clg_new Data Found From Oracle, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();
                    return Ok(new CommonResponse
                    {
                        Message = "No get_sal_clg_new Data Found From Oracle",
                        Status = "1",
                        Error = ""
                    });
                }

            }
            catch (Exception ex)
            {
                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                sb.Append(ex.Message + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                sb.AppendLine("");
                File.AppendAllText(path, sb.ToString());
                sb.Clear();
                return Ok(new CommonResponse
                {
                    Message = ex.Message,
                    Status = "0",
                    Error = ex.StackTrace
                });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult get_pur_disc_Ora()
        {
            string path = HttpContext.Current.Server.MapPath("~/get_pur_disc_Upload_From_Oracle_Log.txt");
            if (!File.Exists(@"" + path + ""))
            {
                File.Create(@"" + path + "").Dispose();
            }
            StringBuilder sb = new StringBuilder();

            try
            {
                string fromtime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                Database db = new Database(Request);

                Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
                List<OracleParameter> paramList = new List<OracleParameter>();

                OracleParameter param1 = new OracleParameter("vrec", OracleDbType.RefCursor);
                param1.Direction = ParameterDirection.Output;
                paramList.Add(param1);

                System.Data.DataTable dt = oracleDbAccess.CallSP("get_pur_disc", paramList);

                int Count = 0;
                if (dt != null && dt.Rows.Count > 0)
                {
                    Count = dt.Rows.Count;

                    List<SqlParameter> para = new List<SqlParameter>();

                    SqlParameter param = new SqlParameter("tableInq", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    DataTable dt1 = db.ExecuteSP("get_pur_disc_ora_Insert", para.ToArray(), false);

                    string Message = string.Empty;
                    if (dt1 != null && dt1.Rows.Count > 0)
                    {
                        Message = dt1.Rows[0]["Message"].ToString();
                    }
                    string totime = string.Format("{0:dd-MMM-yyyy hh:mm:ss tt}", DateTime.Now);

                    if (Message == "SUCCESS")
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append(Message + " " + Count + " get_pur_disc Data Found, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = Message + " " + Count + " get_pur_disc Data Found, process time " + fromtime + " to " + totime + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                    else
                    {
                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append("get_pur_disc Data Upload in Issue" + (!string.IsNullOrEmpty(Message) ? " " + Message : "") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();
                        return Ok(new CommonResponse
                        {
                            Message = "get_pur_disc Data Upload in Issue" + (!string.IsNullOrEmpty(Message) ? " " + Message : "") + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"),
                            Status = "1",
                            Error = ""
                        });
                    }
                }
                else
                {
                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append("No get_pur_disc Data Found From Oracle, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();
                    return Ok(new CommonResponse
                    {
                        Message = "No get_pur_disc Data Found From Oracle",
                        Status = "1",
                        Error = ""
                    });
                }

            }
            catch (Exception ex)
            {
                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                sb.Append(ex.Message + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                sb.AppendLine("");
                File.AppendAllText(path, sb.ToString());
                sb.Clear();
                return Ok(new CommonResponse
                {
                    Message = ex.Message,
                    Status = "0",
                    Error = ex.StackTrace
                });
            }
        }


        #endregion

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Add_StockUpload_Response([FromBody] JObject data)
        {
            StockUpload_Response_Res res = new StockUpload_Response_Res();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(data)))
                {
                    JObject test1 = JObject.Parse(data.ToString());
                    res = JsonConvert.DeserializeObject<StockUpload_Response_Res>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                CommonResponse resp = new CommonResponse();

                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, res.UserId));

                if (!string.IsNullOrEmpty(res.Message))
                    para.Add(db.CreateParam("Message", DbType.String, ParameterDirection.Input, res.Message));
                else
                    para.Add(db.CreateParam("Message", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (res.Status > 0)
                    para.Add(db.CreateParam("Status", DbType.Int32, ParameterDirection.Input, res.Status));
                else
                    para.Add(db.CreateParam("Status", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("Add_StockUpload_Response", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    resp.Status = dt.Rows[0]["Status"].ToString();
                    resp.Message = dt.Rows[0]["Message"].ToString();

                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult Get_StockUpload_Response()
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, userID));

                DataTable dt = db.ExecuteSP("Get_StockUpload_Response", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    List<StockUpload_Response_Res> List_Res = new List<StockUpload_Response_Res>();
                    List_Res = DataTableExtension.ToList<StockUpload_Response_Res>(dt);

                    return Ok(new ServiceResponse<StockUpload_Response_Res>
                    {
                        Data = List_Res,
                        Message = "SUCCESS",
                        Status = "1"
                    });
                }
                else
                {
                    return Ok(new ServiceResponse<StockUpload_Response_Res>
                    {
                        Data = new List<StockUpload_Response_Res>(),
                        Message = "No records found.",
                        Status = "1"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<StockUpload_Response_Res>
                {
                    Data = new List<StockUpload_Response_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult PlaceOrder([FromBody] JObject data)
        {
            PlaceOrder_Req req = new PlaceOrder_Req();
            try
            {
                req = JsonConvert.DeserializeObject<PlaceOrder_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });

            }
            try
            {
                CommonResponse resp = new CommonResponse();
                Int32 OrderId;
                DateTime OrderDate;

                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                int UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, UserId));
                para.Add(db.CreateParam("Comments", DbType.String, ParameterDirection.Input, req.Comments));
                para.Add(db.CreateParam("SupplierId_RefNo_SupplierRefNo", DbType.String, ParameterDirection.Input, req.SupplierId_RefNo_SupplierRefNo));

                if (!string.IsNullOrEmpty(req.PricingMethod))
                    para.Add(db.CreateParam("PricingMethod", DbType.String, ParameterDirection.Input, req.PricingMethod));
                else
                    para.Add(db.CreateParam("PricingMethod", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.PricingSign))
                    para.Add(db.CreateParam("PricingSign", DbType.String, ParameterDirection.Input, req.PricingSign));
                else
                    para.Add(db.CreateParam("PricingSign", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (req.PricingDisc > 0)
                    para.Add(db.CreateParam("PricingDisc", DbType.Decimal, ParameterDirection.Input, Convert.ToDecimal(req.PricingDisc)));
                else
                    para.Add(db.CreateParam("PricingDisc", DbType.Decimal, ParameterDirection.Input, DBNull.Value));

                DataTable dt = db.ExecuteSP("PlaceOrder", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    OrderId = Convert.ToInt32(dt.Rows[0]["OrderId"].ToString());
                    resp.Status = dt.Rows[0]["Status"].ToString();
                    resp.Message = dt.Rows[0]["Message"].ToString();
                    OrderDate = DateTime.Now;

                    if (resp.Status == "1" && OrderId > 0)
                    {
                        SendOrderMail(OrderId, req.Comments, UserId, OrderDate, "Customer");
                        SendOrderMail(OrderId, req.Comments, UserId, OrderDate, "Employee");
                    }
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = "Order Placed Failed";
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [NonAction]
        private static bool SendOrderMail(Int32 OrderId, String Comments, Int32 UserId, DateTime OrderDate, String EmailType)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                para.Clear();

                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, UserId));
                DataTable dtUserDetail = db.ExecuteSP("Get_UserMas", para.ToArray(), false);

                StringBuilder loSb = new StringBuilder();
                //loSb.Append(EmailHeader());
                loSb.Append(@"<table cellpadding=""0"" cellspacing=""0"" width=""100%"">");
                if (dtUserDetail.Rows[0]["CompName"] != null && dtUserDetail.Rows[0]["CompName"].ToString() != "")
                {
                    loSb.Append(@"<tr><td>Company Name:</td><td>" + dtUserDetail.Rows[0]["CompName"].ToString() + "</td></tr>");
                }
                if (dtUserDetail.Rows[0]["UserTypeList"] != null && dtUserDetail.Rows[0]["UserTypeList"].ToString() == "Customer")
                {
                    string Fname = "", Lname = "";
                    if (dtUserDetail.Rows[0]["FirstName"] != null && dtUserDetail.Rows[0]["FirstName"].ToString() != "")
                    {
                        Fname = dtUserDetail.Rows[0]["FirstName"].ToString();
                    }
                    if (dtUserDetail.Rows[0]["LastName"] != null && dtUserDetail.Rows[0]["LastName"].ToString() != "")
                    {
                        Lname = dtUserDetail.Rows[0]["LastName"].ToString();
                    }
                    loSb.Append(@"<tr><td>Buyer:</td><td>" + Fname + " " + Lname + "</td></tr>");
                }
                if (dtUserDetail.Rows[0]["AssistByName"] != null && dtUserDetail.Rows[0]["AssistByName"].ToString() != "")
                {
                    loSb.Append(@"<tr><td>Sales Person:</td><td>" + dtUserDetail.Rows[0]["AssistByName"].ToString() + "</td></tr>");
                }
                if (EmailType == "Customer")
                {
                    if (dtUserDetail.Rows[0]["MobileNo"] != null && dtUserDetail.Rows[0]["MobileNo"].ToString() != "")
                    {
                        loSb.Append(@"<tr><td>Mobile/Whatsapp:</td><td>" + dtUserDetail.Rows[0]["MobileNo"].ToString() + "</td></tr>");
                    }
                    if (dtUserDetail.Rows[0]["EmailId"] != null && dtUserDetail.Rows[0]["EmailId"].ToString() != "")
                    {
                        loSb.Append(@"<tr><td>Email:</td><td>" + dtUserDetail.Rows[0]["EmailId"].ToString() + "</td></tr>");
                    }
                }
                else if (EmailType == "Employee")
                {
                    if (dtUserDetail.Rows[0]["AssistByMobileNo"] != null && dtUserDetail.Rows[0]["AssistByMobileNo"].ToString() != "")
                    {
                        loSb.Append(@"<tr><td>Mobile/Whatsapp:</td><td>" + dtUserDetail.Rows[0]["AssistByMobileNo"].ToString() + "</td></tr>");
                    }
                    if (dtUserDetail.Rows[0]["AssistByEmailId"] != null && dtUserDetail.Rows[0]["AssistByEmailId"].ToString() != "")
                    {
                        loSb.Append(@"<tr><td>Email:</td><td>" + dtUserDetail.Rows[0]["AssistByEmailId"].ToString() + "</td></tr>");
                    }
                }

                loSb.Append(@"<tr><td>Order Date:</td><td>" + OrderDate.ToString("dd-MMM-yyyy") + "</td></tr>");

                loSb.Append(@"<tr><td width=""170px"">Order No:</td><td>" + OrderId.ToString() + "</td></tr>");
                loSb.Append(@"<tr><td width=""170px"">Customer Note:</td><td>" + Comments.ToString() + "</td></tr>");


                loSb.Append("</table>");
                loSb.Append("<br/> <br/>");



                db = new Database();
                para.Clear();
                para.Add(db.CreateParam("OrderId", DbType.Int32, ParameterDirection.Input, OrderId));
                DataTable dtOrderDetail = db.ExecuteSP("OrderDet_SelectAllByOrderId_Email", para.ToArray(), false);

                dtOrderDetail.Columns.Remove("Sr");


                loSb.Append("<table border = '1' style='overflow-x:scroll !important; width:1500px !important;'>");

                loSb.Append("<tr>");

                string _strfont = "\"font-size: 12px; font-family: Tahoma;text-align:center; background-color: #83CAFF;\"";
                foreach (DataColumn column in dtOrderDetail.Columns)
                {
                    loSb.Append("<th style = " + _strfont + ">");
                    loSb.Append(column.ColumnName);
                    loSb.Append("</th>");
                }
                loSb.Append("</tr>");

                _strfont = "\"font-size: 10px; font-family: Tahoma;text-align:center;white-space: nowrap; \"";
                //Building the Data rows.
                foreach (DataRow row in dtOrderDetail.Rows)
                {
                    loSb.Append("<tr>");
                    foreach (DataColumn column in dtOrderDetail.Columns)
                    {
                        string _strcheck = "";
                        //if (column.ColumnName.ToString() == "Disc %" || column.ColumnName.ToString() == "Net Amt($)")
                        if (column.ColumnName.ToString() == "Offer Disc.(%)" || column.ColumnName.ToString() == "Offer Value($)")
                        {
                            string _strstyle = "\"font-size: 10px; font-family: Tahoma;text-align:center;font-weight:bold;background-color: #ade0e9;color:red;white-space: nowrap;\"";
                            loSb.Append("<td style = " + _strstyle + ">");
                        }

                        else if (column.ColumnName.ToString() == "Status" && row["Stock Id"].ToString() != "Total")
                        {
                            if (row["Status"].ToString().ToLower() == "confirmed")
                            {
                                string _strstyle = "\"font-size: 10px; font-family: Tahoma;text-align:center;font-weight:bold;background-color: #c6ffbe;white-space: nowrap;\"";
                                loSb.Append("<td style = " + _strstyle + ">");
                            }
                            else
                            {
                                string _strstyle = "\"font-size: 10px; font-family: Tahoma;text-align:center;font-weight:bold;background-color: yellow;color:red;white-space: nowrap;\"";
                                loSb.Append("<td style = " + _strstyle + ">");
                            }
                        }
                        else if (column.ColumnName.ToString() == "Cut" || column.ColumnName.ToString() == "Polish" || column.ColumnName.ToString() == "Symm")
                        {
                            loSb.Append("<td style = " + _strfont + ">");
                            if (row["Cut"].ToString() == "3EX" && row["Polish"].ToString() == "EX" && row["Symm"].ToString() == "EX")
                            {
                                loSb.Append("<b>" + row[column.ColumnName] + "<b>");
                                _strcheck = "Y";
                            }
                        }
                        else
                        {
                            loSb.Append("<td style = " + _strfont + ">");
                        }

                        if (_strcheck != "Y")
                        {
                            if (column.ColumnName.ToString() == "Rap Price($)" || column.ColumnName.ToString() == "Rap Amount($)" || column.ColumnName.ToString() == "Offer Value($)")
                            {
                                if (row[column.ColumnName].ToString() != "")
                                {

                                    if (column.ColumnName.ToString() == "Rap Price($)")
                                    {
                                        loSb.Append(Convert.ToInt32(row[column.ColumnName]).ToString("C", new System.Globalization.CultureInfo("en-US")).Replace("$", "").Replace("(", "").Replace(")", "").Replace(".00", ""));
                                    }
                                    else
                                    {
                                        loSb.Append(Convert.ToDecimal(row[column.ColumnName]).ToString("C", new System.Globalization.CultureInfo("en-US")).Replace("(", "").Replace(")", "").Replace("$", ""));
                                    }
                                }
                                else
                                {
                                    loSb.Append(row[column.ColumnName]);
                                }
                            }
                            else if (column.ColumnName.ToString() == "Cts")
                            {
                                loSb.Append(String.Format("{0:0.00}", Convert.ToDecimal(row[column.ColumnName])));
                            }
                            else if (column.ColumnName.ToString() == "Image")
                            {
                                if (row["image"].ToString() != "")
                                {
                                    loSb.Append(string.Format("<a href='" + row[column.ColumnName] + "'>Image</a>"));
                                }
                            }

                            else if (column.ColumnName.ToString() == "Video")
                            {
                                if (row["video"].ToString() != "")
                                {
                                    loSb.Append(string.Format("<a href='" + row[column.ColumnName] + "'>Video</a>"));
                                }
                            }

                            else if (column.ColumnName.ToString() == "DNA")
                            {
                                if (row["dna"].ToString() != "")
                                {
                                    loSb.Append(string.Format("<a href='" + row[column.ColumnName] + "'>Dna</a>"));
                                }
                            }
                            else
                            {
                                loSb.Append(row[column.ColumnName]);
                            }
                        }
                        loSb.Append("</td>");

                    }
                    loSb.Append("</tr>");
                }

                loSb.Append("</table>");

                loSb.Append(@"<p>Thank you for placing order</p>");


                if (EmailType == "Customer" && Convert.ToString(dtUserDetail.Rows[0]["EmailId"]) != "")
                {
                    Common.SendMail(Convert.ToString(dtUserDetail.Rows[0]["EmailId"]), "Connect Gia – Order Confirmation – " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss") + " - " + OrderId.ToString(), Convert.ToString(loSb), OrderId, UserId);
                }
                else if (EmailType == "Employee")
                {
                    string email = Convert.ToString(dtUserDetail.Rows[0]["AssistByEmailId"]);
                    if (Convert.ToString(dtUserDetail.Rows[0]["SubAssistByEmailId"]) != "")
                    {
                        if (email != "")
                        {
                            email += ",";
                        }
                        email += Convert.ToString(dtUserDetail.Rows[0]["SubAssistByEmailId"]);
                    }
                    Common.SendMail(email, "Connect Gia – Order Confirmation – " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss") + " - " + OrderId.ToString(), Convert.ToString(loSb), OrderId, UserId);
                }


                return true;
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, null);
                return false;
            }
        }
        [NonAction]
        private DataTable OrderHistory(Get_OrderHistory_Req req)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, userID));

                if (req.PgNo > 0)
                    para.Add(db.CreateParam("PgNo", DbType.Int64, ParameterDirection.Input, req.PgNo));
                else
                    para.Add(db.CreateParam("PgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (req.PgSize > 0)
                    para.Add(db.CreateParam("PgSize", DbType.Int64, ParameterDirection.Input, req.PgSize));
                else
                    para.Add(db.CreateParam("PgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, req.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.StoneId))
                    para.Add(db.CreateParam("StoneId", DbType.String, ParameterDirection.Input, req.StoneId));
                else
                    para.Add(db.CreateParam("StoneId", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.OrderDetId))
                    para.Add(db.CreateParam("OrderDetId", DbType.String, ParameterDirection.Input, req.OrderDetId));
                else
                    para.Add(db.CreateParam("OrderDetId", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable Order_dt = db.ExecuteSP("Get_OrderHistory", para.ToArray(), false);
                return Order_dt;
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, null);
                return null;
            }
        }
        [HttpPost]
        public IHttpActionResult Get_OrderHistory([FromBody] JObject data)
        {
            Get_OrderHistory_Req req = new Get_OrderHistory_Req();

            try
            {
                req = JsonConvert.DeserializeObject<Get_OrderHistory_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_OrderHistory_Res>
                {
                    Data = new List<Get_OrderHistory_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                DataTable Order_dt = OrderHistory(req);

                List<Get_OrderHistory_Res> List_Res = new List<Get_OrderHistory_Res>();
                if (Order_dt != null && Order_dt.Rows.Count > 0)
                {
                    List_Res = Order_dt.ToList<Get_OrderHistory_Res>();
                }

                return Ok(new ServiceResponse<Get_OrderHistory_Res>
                {
                    Data = List_Res,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_OrderHistory_Res>
                {
                    Data = new List<Get_OrderHistory_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Excel_OrderHistory([FromBody] JObject data)
        {
            Get_OrderHistory_Req req = new Get_OrderHistory_Req();

            try
            {
                req = JsonConvert.DeserializeObject<Get_OrderHistory_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }
            try
            {
                DataTable Order_dt = OrderHistory(req);

                if (Order_dt != null && Order_dt.Rows.Count > 0)
                {
                    string filename = "Order History " + DateTime.Now.ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    _path = _path.Replace("Temp", "ExcelFile");
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");

                    EpExcelExport.OrderHistory_Excel(Order_dt, realpath, realpath + filename + ".xlsx", req.UserTypeList);

                    string _strxml = _path + filename + ".xlsx";
                    return Ok(_strxml);
                }
                else
                {
                    return Ok("No Stock found as per filter criteria !");
                }

            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }

        [NonAction]
        private DataTable LabEntry(Get_SearchStock_Req req)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, req.UserId));

                if (req.PgNo > 0)
                    para.Add(db.CreateParam("PgNo", DbType.Int64, ParameterDirection.Input, req.PgNo));
                else
                    para.Add(db.CreateParam("PgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (req.PgSize > 0)
                    para.Add(db.CreateParam("PgSize", DbType.Int64, ParameterDirection.Input, req.PgSize));
                else
                    para.Add(db.CreateParam("PgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, req.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.RefNo))
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, req.RefNo));
                else
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.SupplierId_RefNo_SupplierRefNo))
                    para.Add(db.CreateParam("SupplierId_RefNo_SupplierRefNo", DbType.String, ParameterDirection.Input, req.SupplierId_RefNo_SupplierRefNo));
                else
                    para.Add(db.CreateParam("SupplierId_RefNo_SupplierRefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable Stock_dt = db.ExecuteSP("Get_LabEntry", para.ToArray(), false);
                return Stock_dt;
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, null);
                return null;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Get_LabEntry([FromBody] JObject data)
        {
            Get_SearchStock_Req req = new Get_SearchStock_Req();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(data)))
                {
                    JObject test1 = JObject.Parse(data.ToString());
                    req = JsonConvert.DeserializeObject<Get_SearchStock_Req>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SearchStock_Res>
                {
                    Data = new List<Get_SearchStock_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                DataTable Stock_dt = LabEntry(req);

                List<Get_SearchStock_Res> List_Res = new List<Get_SearchStock_Res>();
                if (Stock_dt != null && Stock_dt.Rows.Count > 0)
                {
                    List_Res = Stock_dt.ToList<Get_SearchStock_Res>();
                }

                return Ok(new ServiceResponse<Get_SearchStock_Res>
                {
                    Data = List_Res,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SearchStock_Res>
                {
                    Data = new List<Get_SearchStock_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Excel_LabEntry([FromBody] JObject data)
        {
            Get_SearchStock_Req req = new Get_SearchStock_Req();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(data)))
                {
                    JObject test1 = JObject.Parse(data.ToString());
                    req = JsonConvert.DeserializeObject<Get_SearchStock_Req>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }
            try
            {
                DataTable Stock_dt = LabEntry(req);

                if (Stock_dt != null && Stock_dt.Rows.Count > 0)
                {
                    string filename = "Lab Entry " + DateTime.Now.ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    _path = _path.Replace("Temp", "ExcelFile");
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");

                    EpExcelExport.LabEntry_Excel(Stock_dt, realpath, realpath + filename + ".xlsx");

                    string _strxml = _path + filename + ".xlsx";
                    return Ok(_strxml);
                }
                else
                {
                    return Ok("No Data Found");
                }

            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }

        [HttpPost]
        public IHttpActionResult Add_LabEntry_Request([FromBody] JObject data)
        {
            try
            {
                CommonResponse resp = new CommonResponse();

                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("LabEntry_Request", DbType.String, ParameterDirection.Input, data.ToString()));

                DataTable dt = db.ExecuteSP("Add_LabEntry_Request", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    resp.Status = "1";
                    resp.Message = Convert.ToString(dt.Rows[0]["Id"]);
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = "Failed";
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Save_LabEntry([FromBody] JObject data)
        {
            Add_LabEntry_Res req = new Add_LabEntry_Res();
            LabEntry_Req req_1 = new LabEntry_Req();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(data)))
                {
                    JObject test1 = JObject.Parse(data.ToString());
                    req = JsonConvert.DeserializeObject<Add_LabEntry_Res>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());


                    Database db = new Database();
                    List<IDbDataParameter> para;
                    para = new List<IDbDataParameter>();

                    para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, req.Id));

                    DataTable dt = db.ExecuteSP("Get_LabEntry_Request", para.ToArray(), false);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string Request = Convert.ToString(dt.Rows[0]["LabEntry_Request"]);
                        req_1 = JsonConvert.DeserializeObject<LabEntry_Req>(Request.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                CommonResponse resp = new CommonResponse();
                Database db = new Database();
                DataTable dtData = new DataTable();
                DataTable dt = new DataTable();

                int LabId = 0, UserId = 0, Assist_UserId = 0, vuser_code = 0, vparty_code = 0;
                string vEntry_type = "W", vparty_name = "", lab_trans_status = "";


                dt.Columns.Add("LabDate", typeof(string));
                dt.Columns.Add("UserId", typeof(string));
                dt.Columns.Add("Ref_No", typeof(string));
                dt.Columns.Add("SupplierId", typeof(string));
                dt.Columns.Add("QC_Require", typeof(string));
                dt.Columns.Add("LabEntry_Status", typeof(string));
                dt.Columns.Add("SUPPLIER_COST_DISC", typeof(string));
                dt.Columns.Add("SUPPLIER_COST_VALUE", typeof(string));
                dt.Columns.Add("CUSTOMER_COST_DISC", typeof(string));
                dt.Columns.Add("CUSTOMER_COST_VALUE", typeof(string));
                dt.Columns.Add("PROFIT", typeof(string));
                dt.Columns.Add("PROFIT_AMOUNT", typeof(string));

                if (req_1.LabEntry_List.Count() > 0)
                {
                    for (int i = 0; i < req_1.LabEntry_List.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        UserId = Convert.ToInt32(req_1.LabEntry_List[i].UserId);

                        dr["LabDate"] = req_1.LabEntry_List[i].LabDate;
                        dr["UserId"] = req_1.LabEntry_List[i].UserId;
                        dr["Ref_No"] = req_1.LabEntry_List[i].Ref_No;
                        dr["SupplierId"] = req_1.LabEntry_List[i].SupplierId;
                        dr["QC_Require"] = req_1.LabEntry_List[i].QC_Require;
                        dr["LabEntry_Status"] = req_1.LabEntry_List[i].LabEntry_Status;
                        dr["SUPPLIER_COST_DISC"] = req_1.LabEntry_List[i].SUPPLIER_COST_DISC;
                        dr["SUPPLIER_COST_VALUE"] = req_1.LabEntry_List[i].SUPPLIER_COST_VALUE;
                        dr["CUSTOMER_COST_DISC"] = req_1.LabEntry_List[i].CUSTOMER_COST_DISC;
                        dr["CUSTOMER_COST_VALUE"] = req_1.LabEntry_List[i].CUSTOMER_COST_VALUE;
                        dr["PROFIT"] = req_1.LabEntry_List[i].PROFIT;
                        dr["PROFIT_AMOUNT"] = req_1.LabEntry_List[i].PROFIT_AMOUNT;

                        dt.Rows.Add(dr);
                    }

                    List<SqlParameter> para = new List<SqlParameter>();
                    SqlParameter param = new SqlParameter("table", SqlDbType.Structured);
                    param.Value = dt;
                    para.Add(param);

                    dtData = db.ExecuteSP("Add_LabEntry", para.ToArray(), false);

                    if (dtData != null && dtData.Rows.Count > 0)
                    {
                        LabId = Convert.ToInt32(dtData.Rows[0]["LabId"]);
                        resp.Status = Convert.ToString(dtData.Rows[0]["Status"]);
                        resp.Message = Convert.ToString(dtData.Rows[0]["Message"]);

                        if (resp.Status == "1" && LabId > 0)
                        {
                            db = new Database();
                            List<IDbDataParameter> para1 = new List<IDbDataParameter>();

                            para1.Add(db.CreateParam("LabId", DbType.Int32, ParameterDirection.Input, LabId));

                            DataTable LabDetail_dt = db.ExecuteSP("Get_LabDetail", para1.ToArray(), false);

                            if (LabDetail_dt != null && LabDetail_dt.Rows.Count > 0)
                            {
                                GetUsers_Req Req = new GetUsers_Req();

                                Req.UserId = UserId;

                                DataTable U_dt = GetUsers_Inner(Req);

                                if (U_dt != null && U_dt.Rows.Count > 0)
                                {
                                    Assist_UserId = Convert.ToInt32(Convert.ToString(U_dt.Rows[0]["AssistBy"]) == "" ? "0" : Convert.ToString(U_dt.Rows[0]["AssistBy"]));
                                    vparty_code = Convert.ToInt32(Convert.ToString(U_dt.Rows[0]["FortunePartyCode"]) == "" ? "0" : Convert.ToString(U_dt.Rows[0]["FortunePartyCode"]));
                                    vparty_name = Convert.ToString(U_dt.Rows[0]["CompName"]);
                                }

                                if (vparty_code > 0 && Assist_UserId > 0)
                                {
                                    Req.UserId = Assist_UserId;

                                    DataTable A_dt = GetUsers_Inner(Req);

                                    if (A_dt != null && A_dt.Rows.Count > 0)
                                    {
                                        vuser_code = Convert.ToInt32(Convert.ToString(A_dt.Rows[0]["UserCode"]) == "" ? "0" : Convert.ToString(A_dt.Rows[0]["UserCode"]));
                                        //vparty_code = Convert.ToInt32(Convert.ToString(A_dt.Rows[0]["FortunePartyCode"]) == "" ? "0" : Convert.ToString(A_dt.Rows[0]["FortunePartyCode"]));

                                        if (vuser_code != 0 && vparty_code != 0 && vparty_name != "")
                                        {
                                            db = new Database(Request);

                                            Oracle_DBAccess oracleDbAccess = new Oracle_DBAccess();
                                            List<OracleParameter> paramList = new List<OracleParameter>();

                                            OracleParameter param1 = new OracleParameter("vuser_code", OracleDbType.Int32);
                                            param1.Value = vuser_code;
                                            paramList.Add(param1);

                                            param1 = new OracleParameter("vrec", OracleDbType.RefCursor);
                                            param1.Direction = ParameterDirection.Output;
                                            paramList.Add(param1);

                                            param1 = new OracleParameter("vEntry_type", OracleDbType.NVarchar2);
                                            param1.Value = vEntry_type;
                                            paramList.Add(param1);

                                            param1 = new OracleParameter("vparty_name", OracleDbType.NVarchar2);
                                            param1.Value = vparty_name;
                                            paramList.Add(param1);

                                            param1 = new OracleParameter("vparty_code", OracleDbType.Int32);
                                            param1.Value = vparty_code;
                                            paramList.Add(param1);

                                            DataTable mas_dt = oracleDbAccess.CallSP("web_trans.lab_trans", paramList);

                                            if (mas_dt != null && mas_dt.Rows.Count > 0)
                                            {
                                                lab_trans_status = Convert.ToString(mas_dt.Rows[0]["STATUS"]);
                                            }
                                            if (!string.IsNullOrEmpty(lab_trans_status))
                                            {
                                                db = new Database();
                                                List<IDbDataParameter> para2 = new List<IDbDataParameter>();

                                                para2.Add(db.CreateParam("LabId", DbType.Int32, ParameterDirection.Input, Convert.ToInt32(LabDetail_dt.Rows[0]["LabId"])));
                                                para2.Add(db.CreateParam("TransId", DbType.String, ParameterDirection.Input, lab_trans_status));

                                                DataTable LabStatus_dt = db.ExecuteSP("Update_LabEntry", para2.ToArray(), false);

                                                for (int j = 0; j < LabDetail_dt.Rows.Count; j++)
                                                {
                                                    oracleDbAccess = new Oracle_DBAccess();
                                                    paramList = new List<OracleParameter>();

                                                    param1 = new OracleParameter("vtrans_id", OracleDbType.Int32);
                                                    param1.Value = lab_trans_status;
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vREF_NO", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Ref No"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vLAB", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Lab"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vSHAPE", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Shape"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vCTS", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Cts"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vCOLOR", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Color"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vPURITY", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Clarity"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vCUT", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Cut"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vPOLISH", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Polish"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vSYMM", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Symm"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vFLS", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Fls"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vSUPP_OFFER_PER", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Base Dis"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vOFFER", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["SUPPLIER COST DISC"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vSOURCE_PARTY", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(U_dt.Rows[0]["FullName"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vcerti_no", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Cert No"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vSUPP_BASE_VALUE", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Base Amt"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vRAP_PRICE", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Rap Rate"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vRAP_VALUE", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Rap Amount"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vLENGTH", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Length"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vWIDTH", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Width"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vDEPTH", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Depth"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vDEPTH_PER", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Depth %"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vCROWN_ANGEL", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Crown Angle"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vCROWN_HEIGHT", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Crown Height"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vPAV_ANGEL", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Pav Angle"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vPAV_HEIGHT", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Pav Height"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vculet", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Culet"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vSYMBOL", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Key to Symbol"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vTABLE_PER", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Table %"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vCROWN_NATTS", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Crown Black"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vCROWN_INCLUSION", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Crown White"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vTABLE_NATTS", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Table Black"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vTABLE_INCLUSION", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Table White"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vFINAL_VALUE", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["CUSTOMER COST VALUE"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vFINAL_DISC_PER", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["CUSTOMER COST DISC"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vMONTH", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(DateTime.Now.ToString("MMMM"));
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vFILE_DATE", OracleDbType.Date);
                                                    param1.Value = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vIMG_PATH", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Image Real"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vVDO_PATH", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Video URL"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vDNA_PATH", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["DNA"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vPARTY_STONE_NO", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Supplier Ref No"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vSUPPLIER_NAME", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["SupplierName"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vSUPP_FINAL_VALUE", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["SUPPLIER COST VALUE"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vSUPP_FINAL_DISC", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["SUPPLIER COST DISC"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vGIRDLE_PER", OracleDbType.Double);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Girdle %"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vGIRDLE_TYPE", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Girdle Type"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vCOMMENTS", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Comment"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vSTR_LN", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Star Length"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vLR_HALF", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Lower HF"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vGIRDLE", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Girdle"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vSHADE", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Shade"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vluster", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Luster"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vLASER_INCLUSION", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Laser Inscription"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vcer_path", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Lab URL"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vTABLE_OPEN", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Table Open"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vGIRDLE_OPEN", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Girdle Open"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vCROWN_OPEN", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Crown Open"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vPAV_OPEN", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["Pav Open"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vBGM", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["BGM"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vSTATUS", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["LabEntry_Status"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vQC_REQUIRE", OracleDbType.NVarchar2);
                                                    param1.Value = Convert.ToString(LabDetail_dt.Rows[j]["QC_Require"]);
                                                    paramList.Add(param1);

                                                    param1 = new OracleParameter("vrec", OracleDbType.RefCursor);
                                                    param1.Direction = ParameterDirection.Output;
                                                    paramList.Add(param1);

                                                    DataTable det_dt = oracleDbAccess.CallSP("web_trans.lab_trans_det", paramList);
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        resp.Status = "0";
                        resp.Message = "Lab Entry Failed";
                    }
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = "Lab Entry Failed";
                }

                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }


        [HttpPost]
        public IHttpActionResult Add_MyCart([FromBody] JObject data)
        {
            Add_MyCart_Req req = new Add_MyCart_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Add_MyCart_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                CommonResponse resp = new CommonResponse();

                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                int UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("SupplierId_RefNo_SupplierRefNo", DbType.String, ParameterDirection.Input, req.SupplierId_RefNo_SupplierRefNo));
                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, UserId));

                DataTable dt = db.ExecuteSP("Add_MyCart", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    resp.Status = dt.Rows[0]["Status"].ToString();
                    resp.Message = dt.Rows[0]["Message"].ToString();
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = "Add to Cart Failed";
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Delete_MyCart([FromBody] JObject data)
        {
            Get_MyCart_Req req = new Get_MyCart_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Get_MyCart_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                CommonResponse resp = new CommonResponse();

                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                int UserId = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, UserId));
                para.Add(db.CreateParam("CartId", DbType.String, ParameterDirection.Input, req.CartId));

                DataTable dt = db.ExecuteSP("Delete_MyCart", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    resp.Status = dt.Rows[0]["Status"].ToString();
                    resp.Message = dt.Rows[0]["Message"].ToString();
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = "My Cart Remove Failed";
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [NonAction]
        private DataTable MyCart(Get_MyCart_Req req)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                int userID = Convert.ToInt32((Request.GetRequestContext().Principal as ClaimsPrincipal).Claims.Where(e => e.Type == "UserID").FirstOrDefault().Value);

                para.Add(db.CreateParam("UserId", DbType.Int64, ParameterDirection.Input, userID));

                if (req.PgNo > 0)
                    para.Add(db.CreateParam("PgNo", DbType.Int64, ParameterDirection.Input, req.PgNo));
                else
                    para.Add(db.CreateParam("PgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (req.PgSize > 0)
                    para.Add(db.CreateParam("PgSize", DbType.Int64, ParameterDirection.Input, req.PgSize));
                else
                    para.Add(db.CreateParam("PgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, req.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.RefNo_SupplierRefNo))
                    para.Add(db.CreateParam("RefNo_SupplierRefNo", DbType.String, ParameterDirection.Input, req.RefNo_SupplierRefNo));
                else
                    para.Add(db.CreateParam("RefNo_SupplierRefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.CartId))
                    para.Add(db.CreateParam("CartId", DbType.String, ParameterDirection.Input, req.CartId));
                else
                    para.Add(db.CreateParam("CartId", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable Cart_dt = db.ExecuteSP("Get_MyCart", para.ToArray(), false);
                return Cart_dt;
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, null);
                return null;
            }
        }
        [HttpPost]
        public IHttpActionResult Get_MyCart([FromBody] JObject data)
        {
            Get_MyCart_Req req = new Get_MyCart_Req();

            try
            {
                req = JsonConvert.DeserializeObject<Get_MyCart_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_MyCart_Res>
                {
                    Data = new List<Get_MyCart_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                DataTable Cart_dt = MyCart(req);

                List<Get_MyCart_Res> List_Res = new List<Get_MyCart_Res>();
                if (Cart_dt != null && Cart_dt.Rows.Count > 0)
                {
                    List_Res = Cart_dt.ToList<Get_MyCart_Res>();
                }

                return Ok(new ServiceResponse<Get_MyCart_Res>
                {
                    Data = List_Res,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_MyCart_Res>
                {
                    Data = new List<Get_MyCart_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Excel_MyCart([FromBody] JObject data)
        {
            Get_MyCart_Req req = new Get_MyCart_Req();

            try
            {
                req = JsonConvert.DeserializeObject<Get_MyCart_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }

            try
            {
                DataTable Cart_dt = MyCart(req);

                if (Cart_dt != null && Cart_dt.Rows.Count > 0)
                {
                    string filename = "My Cart " + DateTime.Now.ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    _path = _path.Replace("Temp", "ExcelFile");
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");

                    EpExcelExport.MyCart_Excel(Cart_dt, realpath, realpath + filename + ".xlsx", req.UserTypeList);

                    string _strxml = _path + filename + ".xlsx";
                    return Ok(_strxml);
                }
                else
                {
                    return Ok("No Data Found");
                }

            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }

        [NonAction]
        private DataTable LabAvailibility(Get_SearchStock_Req req)
        {
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();

                if (req.PgNo > 0)
                    para.Add(db.CreateParam("PgNo", DbType.Int64, ParameterDirection.Input, req.PgNo));
                else
                    para.Add(db.CreateParam("PgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (req.PgSize > 0)
                    para.Add(db.CreateParam("PgSize", DbType.Int64, ParameterDirection.Input, req.PgSize));
                else
                    para.Add(db.CreateParam("PgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.OrderBy))
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, req.OrderBy));
                else
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.RefNo))
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, req.RefNo));
                else
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                if (!string.IsNullOrEmpty(req.SupplierId_RefNo_SupplierRefNo))
                    para.Add(db.CreateParam("SupplierId_RefNo_SupplierRefNo", DbType.String, ParameterDirection.Input, req.SupplierId_RefNo_SupplierRefNo));
                else
                    para.Add(db.CreateParam("SupplierId_RefNo_SupplierRefNo", DbType.String, ParameterDirection.Input, DBNull.Value));

                DataTable Stock_dt = db.ExecuteSP("Get_LabAvailibility", para.ToArray(), false);
                return Stock_dt;
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, null);
                return null;
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Get_LabAvailibility([FromBody] JObject data)
        {
            Get_SearchStock_Req req = new Get_SearchStock_Req();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(data)))
                {
                    JObject test1 = JObject.Parse(data.ToString());
                    req = JsonConvert.DeserializeObject<Get_SearchStock_Req>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SearchStock_Res>
                {
                    Data = new List<Get_SearchStock_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                DataTable Stock_dt = LabAvailibility(req);

                List<Get_SearchStock_Res> List_Res = new List<Get_SearchStock_Res>();
                if (Stock_dt != null && Stock_dt.Rows.Count > 0)
                {
                    List_Res = Stock_dt.ToList<Get_SearchStock_Res>();
                }

                return Ok(new ServiceResponse<Get_SearchStock_Res>
                {
                    Data = List_Res,
                    Message = "SUCCESS",
                    Status = "1"
                });
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SearchStock_Res>
                {
                    Data = new List<Get_SearchStock_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Excel_LabAvailibility([FromBody] JObject data)
        {
            Get_SearchStock_Req req = new Get_SearchStock_Req();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(data)))
                {
                    JObject test1 = JObject.Parse(data.ToString());
                    req = JsonConvert.DeserializeObject<Get_SearchStock_Req>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok("Input Parameters are not in the proper format");
            }

            try
            {
                DataTable Stock_dt = LabAvailibility(req);

                if (Stock_dt != null && Stock_dt.Rows.Count > 0)
                {
                    string filename = req.Type + " " + DateTime.Now.ToString("ddMMyyyy-HHmmss");
                    string _path = ConfigurationManager.AppSettings["data"];
                    _path = _path.Replace("Temp", "ExcelFile");
                    string realpath = HostingEnvironment.MapPath("~/ExcelFile/");

                    if (req.Type == "Buyer List")
                    {
                        Database db = new Database();
                        List<IDbDataParameter> para;
                        para = new List<IDbDataParameter>();

                        para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, 0));
                        para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "BUYER"));

                        DataTable Col_dt = db.ExecuteSP("Get_SearchStock_ColumnSetting", para.ToArray(), false);

                        EpExcelExport.Buyer_Excel(Stock_dt, Col_dt, realpath, realpath + filename + ".xlsx");
                    }
                    else if (req.Type == "Supplier List")
                    {
                        Database db = new Database();
                        List<IDbDataParameter> para;
                        para = new List<IDbDataParameter>();

                        para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, 0));
                        para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "SUPPLIER"));

                        DataTable Col_dt = db.ExecuteSP("Get_SearchStock_ColumnSetting", para.ToArray(), false);

                        EpExcelExport.Supplier_Excel(Stock_dt, Col_dt, realpath, realpath + filename + ".xlsx");
                    }
                    else if (req.Type == "Customer List")
                    {
                        Database db = new Database();
                        List<IDbDataParameter> para;
                        para = new List<IDbDataParameter>();

                        para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, 0));
                        para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "CUSTOMER"));

                        DataTable Col_dt = db.ExecuteSP("Get_SearchStock_ColumnSetting", para.ToArray(), false);

                        EpExcelExport.Customer_Excel(Stock_dt, Col_dt, realpath, realpath + filename + ".xlsx");
                    }

                    string _strxml = _path + filename + ".xlsx";
                    return Ok(_strxml);
                }
                else
                {
                    return Ok("No Stock found as per filter criteria !");
                }

            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                throw ex;
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Add_Customer_Stock_Disc_Mas_Request([FromBody] JObject data)
        {
            try
            {
                CommonResponse resp = new CommonResponse();

                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("Request", DbType.String, ParameterDirection.Input, data.ToString()));

                DataTable dt = db.ExecuteSP("Add_Customer_Stock_Disc_Mas_Request", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    resp.Status = "1";
                    resp.Message = Convert.ToString(dt.Rows[0]["Id"]);
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = "Failed";
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Get_URL([FromBody] JObject data)
        {
            Add_LabEntry_Res Req_0 = new Add_LabEntry_Res();
            Get_URL_Req Req = new Get_URL_Req();

            try
            {
                if (!string.IsNullOrEmpty(Convert.ToString(data)))
                {
                    JObject test1 = JObject.Parse(data.ToString());
                    Req_0 = JsonConvert.DeserializeObject<Add_LabEntry_Res>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());

                    Database db = new Database();
                    List<IDbDataParameter> para;
                    para = new List<IDbDataParameter>();

                    para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, Req_0.Id));

                    DataTable dt = db.ExecuteSP("Get_Customer_Stock_Disc_Mas_Request", para.ToArray(), false);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        string Request = Convert.ToString(dt.Rows[0]["Request"]);
                        Req = JsonConvert.DeserializeObject<Get_URL_Req>(Request.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }

            try
            {
                CommonResponse resp = new CommonResponse();

                if (!String.IsNullOrEmpty(Req.UserName) && Req.TransId > 0)
                {
                    Database db = new Database();
                    List<IDbDataParameter> para = new List<IDbDataParameter>();

                    if (!String.IsNullOrEmpty(Req.UserName))
                        para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, Req.UserName));
                    else
                        para.Add(db.CreateParam("UserName", DbType.String, ParameterDirection.Input, DBNull.Value));

                    if (Req.TransId > 0)
                        para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, Req.TransId));
                    else
                        para.Add(db.CreateParam("Id", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                    DataTable dt = db.ExecuteSP("Get_Customer_Stock_Disc_Mas", para.ToArray(), false);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[0]["StockDiscMgt_Count"]) > 0)
                        {
                            Get_SearchStock_Req req = new Get_SearchStock_Req();
                            req.UserId = Convert.ToInt32(dt.Rows[0]["UserId"]);
                            req.View = false;
                            req.Download = true;
                            req.Type = "Customer List";

                            DataTable dt_Result = SearchStock(req);

                            dt_Result.DefaultView.RowFilter = "iSr IS NOT NULL";
                            dt_Result = dt_Result.DefaultView.ToTable();

                            if (dt_Result != null && dt_Result.Rows.Count > 0)
                            {
                                string tempPath = HostingEnvironment.MapPath("~/ExcelFile/StockDisc_URL_EXPORT/");

                                DateTime now = DateTime.Now;
                                string DATE = " " + now.Day + "" + now.Month + "" + now.Year + "" + now.Hour + "" + now.Minute + "" + now.Second;

                                if (!Directory.Exists(tempPath))
                                {
                                    Directory.CreateDirectory(tempPath);
                                }

                                string filename = "";
                                if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[0]["ExportType"])))
                                {
                                    db = new Database();
                                    para = new List<IDbDataParameter>();

                                    para.Add(db.CreateParam("UserId", DbType.Int32, ParameterDirection.Input, req.UserId));
                                    para.Add(db.CreateParam("Type", DbType.String, ParameterDirection.Input, "CUSTOMER"));

                                    DataTable Col_dt = db.ExecuteSP("Get_SearchStock_ColumnSetting", para.ToArray(), false);
                                    DataTable dt_data = new DataTable();

                                    for (int j = 0; j < Col_dt.Rows.Count; j++)
                                    {
                                        string Column_Name = Convert.ToString(Col_dt.Rows[j]["Column_Name"]);
                                        double AutoFitColumns = Convert.ToDouble(Col_dt.Rows[j]["ExcelWidth"]);

                                        if (Column_Name == "Image-Video")
                                        {
                                            dt_data.Columns.Add("Image", typeof(string));
                                            dt_data.Columns.Add("Video", typeof(string));
                                            dt_data.Columns.Add("Certi", typeof(string));
                                        }
                                        else
                                        {
                                            dt_data.Columns.Add(Column_Name, typeof(string));
                                        }
                                    }

                                    for (int j = 0; j < dt_Result.Rows.Count; j++)
                                    {
                                        DataRow dr = dt_data.NewRow();

                                        for (int k = 0; k < Col_dt.Rows.Count; k++)
                                        {
                                            string Column_Name = Convert.ToString(Col_dt.Rows[k]["Column_Name"]);

                                            if (Column_Name == "Ref No")
                                            {
                                                dr["Ref No"] = Convert.ToString(dt_Result.Rows[j]["Ref_No"]);
                                            }
                                            else if (Column_Name == "Lab")
                                            {
                                                dr["Lab"] = Convert.ToString(dt_Result.Rows[j]["Lab"]);
                                            }
                                            else if (Column_Name == "Image-Video")
                                            {
                                                dr["Image"] = Convert.ToString(dt_Result.Rows[j]["Image_URL"]);
                                                dr["Video"] = Convert.ToString(dt_Result.Rows[j]["Video_URL"]);
                                                dr["Certi"] = Convert.ToString(dt_Result.Rows[j]["Certificate_URL"]);
                                            }
                                            else if (Column_Name == "Shape")
                                            {
                                                dr["Shape"] = Convert.ToString(dt_Result.Rows[j]["Shape"]);
                                            }
                                            else if (Column_Name == "Pointer")
                                            {
                                                dr["Pointer"] = Convert.ToString(dt_Result.Rows[j]["Pointer"]);
                                            }
                                            else if (Column_Name == "BGM")
                                            {
                                                dr["BGM"] = Convert.ToString(dt_Result.Rows[j]["BGM"]);
                                            }
                                            else if (Column_Name == "Color")
                                            {
                                                dr["Color"] = Convert.ToString(dt_Result.Rows[j]["Color"]);
                                            }
                                            else if (Column_Name == "Clarity")
                                            {
                                                dr["Clarity"] = Convert.ToString(dt_Result.Rows[j]["Clarity"]);
                                            }
                                            else if (Column_Name == "Cts")
                                            {
                                                dr["Cts"] = ((dt_Result.Rows[j]["Cts"] != null) ?
                                                           (dt_Result.Rows[j]["Cts"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["Cts"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Rap Rate($)")
                                            {
                                                dr["Rap Rate($)"] = ((dt_Result.Rows[j]["Rap_Rate"] != null) ?
                                                           (dt_Result.Rows[j]["Rap_Rate"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["Rap_Rate"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Rap Amount($)")
                                            {
                                                dr["Rap Amount($)"] = ((dt_Result.Rows[j]["Rap_Amount"] != null) ?
                                                           (dt_Result.Rows[j]["Rap_Amount"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["Rap_Amount"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Offer Disc(%)")
                                            {
                                                dr["Offer Disc(%)"] = ((dt_Result.Rows[j]["CUSTOMER_COST_DISC"] != null) ?
                                                           (dt_Result.Rows[j]["CUSTOMER_COST_DISC"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["CUSTOMER_COST_DISC"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Offer Value($)")
                                            {
                                                dr["Offer Value($)"] = ((dt_Result.Rows[j]["CUSTOMER_COST_VALUE"] != null) ?
                                                           (dt_Result.Rows[j]["CUSTOMER_COST_VALUE"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["CUSTOMER_COST_VALUE"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Price / Cts")
                                            {
                                                dr["Price / Cts"] = ((dt_Result.Rows[j]["Base_Price_Cts"] != null) ?
                                                           (dt_Result.Rows[j]["Base_Price_Cts"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["Base_Price_Cts"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Cut")
                                            {
                                                dr["Cut"] = Convert.ToString(dt_Result.Rows[j]["Cut"]);
                                            }
                                            else if (Column_Name == "Polish")
                                            {
                                                dr["Polish"] = Convert.ToString(dt_Result.Rows[j]["Polish"]);
                                            }
                                            else if (Column_Name == "Symm")
                                            {
                                                dr["Symm"] = Convert.ToString(dt_Result.Rows[j]["Symm"]);
                                            }
                                            else if (Column_Name == "Fls")
                                            {
                                                dr["Fls"] = Convert.ToString(dt_Result.Rows[j]["Fls"]);
                                            }
                                            else if (Column_Name == "RATIO")
                                            {
                                                dr["RATIO"] = ((dt_Result.Rows[j]["RATIO"] != null) ?
                                                           (dt_Result.Rows[j]["RATIO"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["RATIO"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Length")
                                            {
                                                dr["Length"] = ((dt_Result.Rows[j]["Length"] != null) ?
                                                           (dt_Result.Rows[j]["Length"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["Length"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Width")
                                            {
                                                dr["Width"] = ((dt_Result.Rows[j]["Width"] != null) ?
                                                           (dt_Result.Rows[j]["Width"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["Width"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Depth")
                                            {
                                                dr["Depth"] = ((dt_Result.Rows[j]["Depth"] != null) ?
                                                           (dt_Result.Rows[j]["Depth"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["Depth"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Depth(%)")
                                            {
                                                dr["Depth(%)"] = ((dt_Result.Rows[j]["Depth_Per"] != null) ?
                                                           (dt_Result.Rows[j]["Depth_Per"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["Depth_Per"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Table(%)")
                                            {
                                                dr["Table(%)"] = ((dt_Result.Rows[j]["Table_Per"] != null) ?
                                                           (dt_Result.Rows[j]["Table_Per"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["Table_Per"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Key To Symbol")
                                            {
                                                dr["Key To Symbol"] = Convert.ToString(dt_Result.Rows[j]["Key_To_Symboll"]);
                                            }
                                            else if (Column_Name == "Comment")
                                            {
                                                dr["Comment"] = Convert.ToString(dt_Result.Rows[j]["Lab_Comments"]);
                                            }
                                            else if (Column_Name == "Girdle(%)")
                                            {
                                                dr["Girdle(%)"] = ((dt_Result.Rows[j]["Girdle_Per"] != null) ?
                                                           (dt_Result.Rows[j]["Girdle_Per"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["Girdle_Per"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Crown Angle")
                                            {
                                                dr["Crown Angle"] = ((dt_Result.Rows[j]["Crown_Angle"] != null) ?
                                                           (dt_Result.Rows[j]["Crown_Angle"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["Crown_Angle"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Crown Height")
                                            {
                                                dr["Crown Height"] = ((dt_Result.Rows[j]["Crown_Height"] != null) ?
                                                           (dt_Result.Rows[j]["Crown_Height"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["Crown_Height"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Pav Angle")
                                            {
                                                dr["Pav Angle"] = ((dt_Result.Rows[j]["Pav_Angle"] != null) ?
                                                           (dt_Result.Rows[j]["Pav_Angle"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["Pav_Angle"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Pav Height")
                                            {
                                                dr["Pav Height"] = ((dt_Result.Rows[j]["Pav_Height"] != null) ?
                                                           (dt_Result.Rows[j]["Pav_Height"].GetType().Name != "DBNull" ?
                                                           Convert.ToDouble(dt_Result.Rows[j]["Pav_Height"]) : ((Double?)null)) : null);
                                            }
                                            else if (Column_Name == "Table Black")
                                            {
                                                dr["Table Black"] = Convert.ToString(dt_Result.Rows[j]["Table_Natts"]);
                                            }
                                            else if (Column_Name == "Crown Black")
                                            {
                                                dr["Crown Black"] = Convert.ToString(dt_Result.Rows[j]["Crown_Natts"]);
                                            }
                                            else if (Column_Name == "Table White")
                                            {
                                                dr["Table White"] = Convert.ToString(dt_Result.Rows[j]["Table_Inclusion"]);
                                            }
                                            else if (Column_Name == "Crown White")
                                            {
                                                dr["Crown White"] = Convert.ToString(dt_Result.Rows[j]["Crown_Inclusion"]);
                                            }
                                            else if (Column_Name == "Culet")
                                            {
                                                dr["Culet"] = Convert.ToString(dt_Result.Rows[j]["Culet"]);
                                            }
                                            else if (Column_Name == "Table Open")
                                            {
                                                dr["Table Open"] = Convert.ToString(dt_Result.Rows[j]["Table_Open"]);
                                            }
                                            else if (Column_Name == "Crown Open")
                                            {
                                                dr["Crown Open"] = Convert.ToString(dt_Result.Rows[j]["Crown_Open"]);
                                            }
                                            else if (Column_Name == "Pavilion Open")
                                            {
                                                dr["Pavilion Open"] = Convert.ToString(dt_Result.Rows[j]["Pav_Open"]);
                                            }
                                            else if (Column_Name == "Girdle Open")
                                            {
                                                dr["Girdle Open"] = Convert.ToString(dt_Result.Rows[j]["Girdle_Open"]);
                                            }
                                        }
                                        dt_data.Rows.Add(dr);
                                    }


                                    if (Convert.ToString(dt.Rows[0]["ExportType"]).ToUpper() == "XML")
                                    {
                                        filename = tempPath + Convert.ToString(dt.Rows[0]["UserName"]) + DATE + ".xml";
                                        if (File.Exists(filename))
                                        {
                                            File.Delete(filename);
                                        }

                                        dt_data.TableName = "Records";
                                        dt_data.WriteXml(filename);
                                    }
                                    else if (Convert.ToString(dt.Rows[0]["ExportType"]).ToUpper() == "CSV")
                                    {
                                        filename = tempPath + Convert.ToString(dt.Rows[0]["UserName"]) + DATE + ".csv";
                                        if (File.Exists(filename))
                                        {
                                            File.Delete(filename);
                                        }

                                        StringBuilder sb = new StringBuilder();
                                        IEnumerable<string> columnNames = dt_data.Columns.Cast<DataColumn>().Select(column => column.ColumnName);
                                        sb.AppendLine(string.Join(",", columnNames));

                                        foreach (DataRow row in dt_data.Rows)
                                        {
                                            IEnumerable<string> fields = row.ItemArray.Select(field => field.ToString().Replace(",", " "));
                                            sb.AppendLine(string.Join(",", fields));
                                        }
                                        File.WriteAllText(filename, sb.ToString());
                                    }
                                    else if (Convert.ToString(dt.Rows[0]["ExportType"]).ToUpper() == "EXCEL(.XLSX)" || Convert.ToString(dt.Rows[0]["ExportType"]).ToUpper() == "EXCEL(.XLS)")
                                    {
                                        if (Convert.ToString(dt.Rows[0]["ExportType"]).ToUpper() == "EXCEL(.XLSX)")
                                        {
                                            filename = tempPath + Convert.ToString(dt.Rows[0]["UserName"]) + DATE + ".xlsx";
                                        }
                                        else
                                        {
                                            filename = tempPath + Convert.ToString(dt.Rows[0]["UserName"]) + DATE + ".xls";
                                        }

                                        if (File.Exists(filename))
                                        {
                                            File.Delete(filename);
                                        }

                                        FileInfo newFile = new FileInfo(filename);
                                        using (ExcelPackage pck = new ExcelPackage(newFile))
                                        {
                                            ExcelWorksheet ws = pck.Workbook.Worksheets.Add(Convert.ToString(dt.Rows[0]["UserName"]));
                                            pck.Workbook.Properties.Title = "API";
                                            ws.Cells["A1"].LoadFromDataTable(dt_data, true);

                                            ws.View.FreezePanes(2, 1);
                                            var allCells = ws.Cells[ws.Dimension.Address];
                                            allCells.AutoFilter = true;
                                            allCells.AutoFitColumns();

                                            int rowStart = ws.Dimension.Start.Row;
                                            int rowEnd = ws.Dimension.End.Row;
                                            removingGreenTagWarning(ws, ws.Cells[1, 1, rowEnd, 100].Address);

                                            var headerCells = ws.Cells[1, 1, 1, ws.Dimension.Columns];
                                            headerCells.Style.Font.Bold = true;
                                            headerCells.Style.Font.Color.SetColor(System.Drawing.Color.Black);
                                            headerCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                            headerCells.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightSkyBlue);
                                            pck.Save();
                                        }
                                    }
                                    else if (Convert.ToString(dt.Rows[0]["ExportType"]).ToUpper() == "JSON")
                                    {
                                        filename = tempPath + Convert.ToString(dt.Rows[0]["UserName"]) + DATE + ".json";
                                        if (File.Exists(filename))
                                        {
                                            File.Delete(filename);
                                        }
                                        string json = Lib.Model.Common.DataTableToJSONWithStringBuilder(dt_data);
                                        File.WriteAllText(filename, json);
                                    }

                                    return Ok(new CommonResponse
                                    {
                                        Message = filename,
                                        Status = "1",
                                        Error = ""
                                    });
                                }
                                else
                                {
                                    return Ok(new CommonResponse
                                    {
                                        Message = "",
                                        Status = "0",
                                        Error = "404 Export Type Not Found"
                                    });
                                }
                            }
                            else
                            {
                                return Ok(new CommonResponse
                                {
                                    Message = "",
                                    Status = "0",
                                    Error = "404 Stock Not Found"
                                });
                            }
                        }
                        else
                        {
                            return Ok(new CommonResponse
                            {
                                Message = "",
                                Status = "0",
                                Error = "404 Stock Not Found"
                            });
                        }
                    }
                    else
                    {
                        return Ok(new CommonResponse
                        {
                            Message = "",
                            Status = "0",
                            Error = "401 Unauthorized request"
                        });
                    }
                }
                else
                {
                    return Ok(new CommonResponse
                    {
                        Message = "",
                        Status = "0",
                        Error = "400 Bad Request"
                    });
                }
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Message = "",
                    Status = "0",
                    Error = ex.Message
                });
            }
        }
        [HttpPost]
        public IHttpActionResult Add_Stock_FileUpload_Request([FromBody] JObject data)
        {
            try
            {
                CommonResponse resp = new CommonResponse();

                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

                para.Add(db.CreateParam("Stock_FileUpload_Request", DbType.String, ParameterDirection.Input, data.ToString()));

                DataTable dt = db.ExecuteSP("Add_Stock_FileUpload_Request", para.ToArray(), false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    resp.Status = "1";
                    resp.Message = Convert.ToString(dt.Rows[0]["Id"]);
                }
                else
                {
                    resp.Status = "0";
                    resp.Message = "Failed";
                }
                return Ok(resp);
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = "",
                    Message = "Failed",
                    Status = "0"
                });
            }
        }
    }
}
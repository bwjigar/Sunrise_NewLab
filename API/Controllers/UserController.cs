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
 


namespace API.Controllers
{
    [Authorize]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
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
                Common.InsertErrorLog(ex, null, Request);
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
                    list.Add(database.CreateParam("Order_No", DbType.Int32, ParameterDirection.Input, res.Order_No));
                }
                else
                {
                    list.Add(database.CreateParam("Order_No", DbType.Int32, ParameterDirection.Input, DBNull.Value));
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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


                if (req.SuppDisc.Count() > 0)
                {
                    for (int i = 0; i < req.SuppDisc.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["Supplier"] = req.SuppDisc[i].Supplier;
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                dt.Columns.Add("Supplier", typeof(string));
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

                if (req.SuppDisc.Count() > 0)
                {
                    for (int i = 0; i < req.SuppDisc.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["UserId"] = req.UserId;
                        dr["Supplier"] = req.SuppDisc[i].Supplier;
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Obj_Supplier_Disc>
                {
                    Data = new List<Obj_Supplier_Disc>(),
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
                Common.InsertErrorLog(ex, null, Request);
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
                dt.Columns.Add("PricingMethod_3", typeof(string));
                dt.Columns.Add("PricingSign_3", typeof(string));
                dt.Columns.Add("Disc_3_1", typeof(string));
                dt.Columns.Add("Value_3_1", typeof(string));
                dt.Columns.Add("Value_3_2", typeof(string));
                dt.Columns.Add("Value_3_3", typeof(string));
                dt.Columns.Add("Value_3_4", typeof(string));
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

                if (req.SuppDisc.Count() > 0)
                {
                    for (int i = 0; i < req.SuppDisc.Count(); i++)
                    {
                        DataRow dr = dt.NewRow();

                        dr["SupplierId"] = req.SupplierId;
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
                        dr["PricingMethod_3"] = req.SuppDisc[i].PricingMethod_3;
                        dr["PricingSign_3"] = req.SuppDisc[i].PricingSign_3;
                        dr["Disc_3_1"] = req.SuppDisc[i].Disc_3_1;
                        dr["Value_3_1"] = req.SuppDisc[i].Value_3_1;
                        dr["Value_3_2"] = req.SuppDisc[i].Value_3_2;
                        dr["Value_3_3"] = req.SuppDisc[i].Value_3_3;
                        dr["Value_3_4"] = req.SuppDisc[i].Value_3_4;
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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

                        var (msg, exe, dt) = Supplier_Stock_Get_From_His_WEB_API_AND_FTP(dtAPI.Rows[0]["APIType"].ToString(),
                                    dtAPI.Rows[0]["SupplierResponseFormat"].ToString(),
                                    dtAPI.Rows[0]["SupplierURL"].ToString(),
                                    dtAPI.Rows[0]["SupplierAPIMethod"].ToString(),
                                    dtAPI.Rows[0]["UserName"].ToString(),
                                    dtAPI.Rows[0]["Password"].ToString(),
                                    dtAPI.Rows[0]["FileLocation"].ToString());
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                    Stock_dt = ConvertXLStoDataTable("", connString);
                }
                else if (str == ".xlsx")
                {
                    string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Req.FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    Stock_dt = ConvertXSLXtoDataTable("", connString);
                }
                else if (str == ".csv")
                {
                    Stock_dt = ConvertCSVtoDataTable(Req.FilePath);
                }

                List<Get_SupplierColumnSetting_FromAPI_Res> List_Res = new List<Get_SupplierColumnSetting_FromAPI_Res>();

                if (Stock_dt != null && Stock_dt.Rows.Count > 0)
                {
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
                        Message = "Supplier " + str + " File in Columns not found.",
                        Status = "2"
                    });
                }
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SupplierColumnSetting_FromAPI_Res>
                {
                    Data = new List<Get_SupplierColumnSetting_FromAPI_Res>(),
                    Message = "Input Parameters are not in the proper format",
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                para.Add(db.CreateParam("NewRefNoGenerate", DbType.Boolean, ParameterDirection.Input, res.NewRefNoGenerate));
                para.Add(db.CreateParam("NewDiscGenerate", DbType.Boolean, ParameterDirection.Input, res.NewDiscGenerate));
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
                Common.InsertErrorLog(ex, null, Request);
                return Ok(new CommonResponse
                {
                    Error = ex.StackTrace,
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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

                if (!string.IsNullOrEmpty(req.FortunePartyCode))
                    para.Add(db.CreateParam("FortunePartyCode", DbType.String, ParameterDirection.Input, req.FortunePartyCode));
                else
                    para.Add(db.CreateParam("FortunePartyCode", DbType.String, ParameterDirection.Input, DBNull.Value));

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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
            FortunePartyCode_Exist_Request fortunepartycode_exist_request = new FortunePartyCode_Exist_Request();
            try
            {
                fortunepartycode_exist_request = JsonConvert.DeserializeObject<FortunePartyCode_Exist_Request>(data.ToString());
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, Request);
                return Ok();
            }

            try
            {
                Database db = new Database();
                System.Collections.Generic.List<System.Data.IDbDataParameter> para;
                para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

                if (fortunepartycode_exist_request.iUserId > 0)
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, fortunepartycode_exist_request.iUserId));
                else
                    para.Add(db.CreateParam("iUserId", DbType.Int32, ParameterDirection.Input, DBNull.Value));

                para.Add(db.CreateParam("FortunePartyCode", DbType.Int32, ParameterDirection.Input, fortunepartycode_exist_request.FortunePartyCode));

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
                Common.InsertErrorLog(ex, null, Request);
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
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para;
                para = new List<IDbDataParameter>();

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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
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
                Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_ParaMas_Res>
                {
                    Data = new List<Get_ParaMas_Res>(),
                    Message = "Something Went wrong.\nPlease try again later",
                    Status = "0"
                });
            }
        }

        [HttpPost]
        public IHttpActionResult Get_SearchStock([FromBody] JObject data)
        {
            Get_SearchStock_Req req = new Get_SearchStock_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Get_SearchStock_Req>(data.ToString());
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_SearchStock_Res>
                {
                    Data = new List<Get_SearchStock_Res>(),
                    Message = "Input Parameters are not in the proper format",
                    Status = "0"
                });
            }
            try
            {
                Database db = new Database();
                List<IDbDataParameter> para = new List<IDbDataParameter>();
                if (req.PgNo > 0)
                {
                    para.Add(db.CreateParam("PgNo", DbType.Int64, ParameterDirection.Input, req.PgNo));
                }
                else
                {
                    para.Add(db.CreateParam("PgNo", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                }
                if (req.PgSize > 0)
                {
                    para.Add(db.CreateParam("PgSize", DbType.Int64, ParameterDirection.Input, req.PgSize));
                }
                else
                {
                    para.Add(db.CreateParam("PgSize", DbType.Int64, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.OrderBy))
                {
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, req.OrderBy));
                }
                else
                {
                    para.Add(db.CreateParam("OrderBy", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.RefNo))
                {
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, req.RefNo));
                }
                else
                {
                    para.Add(db.CreateParam("RefNo", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.SupplierId))
                {
                    para.Add(db.CreateParam("SupplierId", DbType.String, ParameterDirection.Input, req.SupplierId));
                }
                else
                {
                    para.Add(db.CreateParam("SupplierId", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.Shape))
                {
                    para.Add(db.CreateParam("Shape", DbType.String, ParameterDirection.Input, req.Shape));
                }
                else
                {
                    para.Add(db.CreateParam("Shape", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.Pointer))
                {
                    para.Add(db.CreateParam("Pointer", DbType.String, ParameterDirection.Input, req.Pointer));
                }
                else
                {
                    para.Add(db.CreateParam("Pointer", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.ColorType))
                {
                    para.Add(db.CreateParam("ColorType", DbType.String, ParameterDirection.Input, req.ColorType));
                }
                else
                {
                    para.Add(db.CreateParam("ColorType", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.Color))
                {
                    para.Add(db.CreateParam("Color", DbType.String, ParameterDirection.Input, req.Color));
                }
                else
                {
                    para.Add(db.CreateParam("Color", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.INTENSITY))
                {
                    para.Add(db.CreateParam("INTENSITY", DbType.String, ParameterDirection.Input, req.INTENSITY));
                }
                else
                {
                    para.Add(db.CreateParam("INTENSITY", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.OVERTONE))
                {
                    para.Add(db.CreateParam("OVERTONE", DbType.String, ParameterDirection.Input, req.OVERTONE));
                }
                else
                {
                    para.Add(db.CreateParam("OVERTONE", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.FANCY_COLOR))
                {
                    para.Add(db.CreateParam("FANCY_COLOR", DbType.String, ParameterDirection.Input, req.FANCY_COLOR));
                }
                else
                {
                    para.Add(db.CreateParam("FANCY_COLOR", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.Clarity))
                {
                    para.Add(db.CreateParam("Clarity", DbType.String, ParameterDirection.Input, req.Clarity));
                }
                else
                {
                    para.Add(db.CreateParam("Clarity", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.Cut))
                {
                    para.Add(db.CreateParam("Cut", DbType.String, ParameterDirection.Input, req.Cut));
                }
                else
                {
                    para.Add(db.CreateParam("Cut", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.Polish))
                {
                    para.Add(db.CreateParam("Polish", DbType.String, ParameterDirection.Input, req.Polish));
                }
                else
                {
                    para.Add(db.CreateParam("Polish", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.Symm))
                {
                    para.Add(db.CreateParam("Symm", DbType.String, ParameterDirection.Input, req.Symm));
                }
                else
                {
                    para.Add(db.CreateParam("Symm", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.Fls))
                {
                    para.Add(db.CreateParam("Fls", DbType.String, ParameterDirection.Input, req.Fls));
                }
                else
                {
                    para.Add(db.CreateParam("Fls", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.BGM))
                {
                    para.Add(db.CreateParam("BGM", DbType.String, ParameterDirection.Input, req.BGM));
                }
                else
                {
                    para.Add(db.CreateParam("BGM", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.Lab))
                {
                    para.Add(db.CreateParam("Lab", DbType.String, ParameterDirection.Input, req.Lab));
                }
                else
                {
                    para.Add(db.CreateParam("Lab", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.CrownBlack))
                {
                    para.Add(db.CreateParam("CrownBlack", DbType.String, ParameterDirection.Input, req.CrownBlack));
                }
                else
                {
                    para.Add(db.CreateParam("CrownBlack", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.TableBlack))
                {
                    para.Add(db.CreateParam("TableBlack", DbType.String, ParameterDirection.Input, req.TableBlack));
                }
                else
                {
                    para.Add(db.CreateParam("TableBlack", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.TableWhite))
                {
                    para.Add(db.CreateParam("TableWhite", DbType.String, ParameterDirection.Input, req.TableWhite));
                }
                else
                {
                    para.Add(db.CreateParam("TableWhite", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.CrownWhite))
                {
                    para.Add(db.CreateParam("CrownWhite", DbType.String, ParameterDirection.Input, req.CrownWhite));
                }
                else
                {
                    para.Add(db.CreateParam("CrownWhite", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.TableOpen))
                {
                    para.Add(db.CreateParam("TableOpen", DbType.String, ParameterDirection.Input, req.TableOpen));
                }
                else
                {
                    para.Add(db.CreateParam("TableOpen", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.CrownOpen))
                {
                    para.Add(db.CreateParam("CrownOpen", DbType.String, ParameterDirection.Input, req.CrownOpen));
                }
                else
                {
                    para.Add(db.CreateParam("CrownOpen", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.PavOpen))
                {
                    para.Add(db.CreateParam("PavOpen", DbType.String, ParameterDirection.Input, req.PavOpen));
                }
                else
                {
                    para.Add(db.CreateParam("PavOpen", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.GirdleOpen))
                {
                    para.Add(db.CreateParam("GirdleOpen", DbType.String, ParameterDirection.Input, req.GirdleOpen));
                }
                else
                {
                    para.Add(db.CreateParam("GirdleOpen", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.KTSBlank))
                {
                    para.Add(db.CreateParam("KTSBlank", DbType.Boolean, ParameterDirection.Input, req.KTSBlank));
                }
                else
                {
                    para.Add(db.CreateParam("KTSBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.Keytosymbol))
                {
                    para.Add(db.CreateParam("Keytosymbol", DbType.String, ParameterDirection.Input, req.Keytosymbol));
                }
                else
                {
                    para.Add(db.CreateParam("Keytosymbol", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.CheckKTS))
                {
                    para.Add(db.CreateParam("CheckKTS", DbType.String, ParameterDirection.Input, req.CheckKTS));
                }
                else
                {
                    para.Add(db.CreateParam("CheckKTS", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.UNCheckKTS))
                {
                    para.Add(db.CreateParam("UNCheckKTS", DbType.String, ParameterDirection.Input, req.UNCheckKTS));
                }
                else
                {
                    para.Add(db.CreateParam("UNCheckKTS", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.FromDisc))
                {
                    para.Add(db.CreateParam("FromDisc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromDisc)));
                }
                else
                {
                    para.Add(db.CreateParam("FromDisc", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.ToDisc))
                {
                    para.Add(db.CreateParam("ToDisc", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToDisc)));
                }
                else
                {
                    para.Add(db.CreateParam("ToDisc", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.FromTotAmt))
                {
                    para.Add(db.CreateParam("FromTotAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromTotAmt)));
                }
                else
                {
                    para.Add(db.CreateParam("FromTotAmt", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.ToTotAmt))
                {
                    para.Add(db.CreateParam("ToTotAmt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToTotAmt)));
                }
                else
                {
                    para.Add(db.CreateParam("ToTotAmt", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.LengthBlank))
                {
                    para.Add(db.CreateParam("LengthBlank", DbType.Boolean, ParameterDirection.Input, req.LengthBlank));
                }
                else
                {
                    para.Add(db.CreateParam("LengthBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.FromLength))
                {
                    para.Add(db.CreateParam("FromLength", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromLength)));
                }
                else
                {
                    para.Add(db.CreateParam("FromLength", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.ToLength))
                {
                    para.Add(db.CreateParam("ToLength", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToLength)));
                }
                else
                {
                    para.Add(db.CreateParam("ToLength", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.WidthBlank))
                {
                    para.Add(db.CreateParam("WidthBlank", DbType.Boolean, ParameterDirection.Input, req.WidthBlank));
                }
                else
                {
                    para.Add(db.CreateParam("WidthBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.FromWidth))
                {
                    para.Add(db.CreateParam("FromWidth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromWidth)));
                }
                else
                {
                    para.Add(db.CreateParam("FromWidth", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.ToWidth))
                {
                    para.Add(db.CreateParam("ToWidth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToWidth)));
                }
                else
                {
                    para.Add(db.CreateParam("ToWidth", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.DepthBlank))
                {
                    para.Add(db.CreateParam("DepthBlank", DbType.Boolean, ParameterDirection.Input, req.DepthBlank));
                }
                else
                {
                    para.Add(db.CreateParam("DepthBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.FromDepth))
                {
                    para.Add(db.CreateParam("FromDepth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromDepth)));
                }
                else
                {
                    para.Add(db.CreateParam("FromDepth", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.ToDepth))
                {
                    para.Add(db.CreateParam("ToDepth", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToDepth)));
                }
                else
                {
                    para.Add(db.CreateParam("ToDepth", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.DepthPerBlank))
                {
                    para.Add(db.CreateParam("DepthPerBlank", DbType.Boolean, ParameterDirection.Input, req.DepthPerBlank));
                }
                else
                {
                    para.Add(db.CreateParam("DepthPerBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.FromDepthPer))
                {
                    para.Add(db.CreateParam("FromDepthPer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromDepthPer)));
                }
                else
                {
                    para.Add(db.CreateParam("FromDepthPer", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.ToDepthPer))
                {
                    para.Add(db.CreateParam("ToDepthPer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToDepthPer)));
                }
                else
                {
                    para.Add(db.CreateParam("ToDepthPer", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.TablePerBlank))
                {
                    para.Add(db.CreateParam("TablePerBlank", DbType.Boolean, ParameterDirection.Input, req.TablePerBlank));
                }
                else
                {
                    para.Add(db.CreateParam("TablePerBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.FromTablePer))
                {
                    para.Add(db.CreateParam("FromTablePer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromTablePer)));
                }
                else
                {
                    para.Add(db.CreateParam("FromTablePer", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.ToTablePer))
                {
                    para.Add(db.CreateParam("ToTablePer", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToTablePer)));
                }
                else
                {
                    para.Add(db.CreateParam("ToTablePer", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.Img))
                {
                    para.Add(db.CreateParam("Img", DbType.String, ParameterDirection.Input, req.Img));
                }
                else
                {
                    para.Add(db.CreateParam("Img", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.Vdo))
                {
                    para.Add(db.CreateParam("Vdo", DbType.String, ParameterDirection.Input, req.Vdo));
                }
                else
                {
                    para.Add(db.CreateParam("Vdo", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.Certi))
                {
                    para.Add(db.CreateParam("Certi", DbType.String, ParameterDirection.Input, req.Certi));
                }
                else
                {
                    para.Add(db.CreateParam("Certi", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.CrAngBlank))
                {
                    para.Add(db.CreateParam("CrAngBlank", DbType.Boolean, ParameterDirection.Input, req.CrAngBlank));
                }
                else
                {
                    para.Add(db.CreateParam("CrAngBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.FromCrAng))
                {
                    para.Add(db.CreateParam("FromCrAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromCrAng)));
                }
                else
                {
                    para.Add(db.CreateParam("FromCrAng", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.ToCrAng))
                {
                    para.Add(db.CreateParam("ToCrAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToCrAng)));
                }
                else
                {
                    para.Add(db.CreateParam("ToCrAng", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.CrHtBlank))
                {
                    para.Add(db.CreateParam("CrHtBlank", DbType.Boolean, ParameterDirection.Input, req.CrHtBlank));
                }
                else
                {
                    para.Add(db.CreateParam("CrHtBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.FromCrHt))
                {
                    para.Add(db.CreateParam("FromCrHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromCrHt)));
                }
                else
                {
                    para.Add(db.CreateParam("FromCrHt", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.ToCrHt))
                {
                    para.Add(db.CreateParam("ToCrHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToCrHt)));
                }
                else
                {
                    para.Add(db.CreateParam("ToCrHt", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.PavAngBlank))
                {
                    para.Add(db.CreateParam("PavAngBlank", DbType.Boolean, ParameterDirection.Input, req.PavAngBlank));
                }
                else
                {
                    para.Add(db.CreateParam("PavAngBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.FromPavAng))
                {
                    para.Add(db.CreateParam("FromPavAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromPavAng)));
                }
                else
                {
                    para.Add(db.CreateParam("FromPavAng", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.ToPavAng))
                {
                    para.Add(db.CreateParam("ToPavAng", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToPavAng)));
                }
                else
                {
                    para.Add(db.CreateParam("ToPavAng", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.PavHtBlank))
                {
                    para.Add(db.CreateParam("PavHtBlank", DbType.Boolean, ParameterDirection.Input, req.PavHtBlank));
                }
                else
                {
                    para.Add(db.CreateParam("PavHtBlank", DbType.Boolean, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.FromPavHt))
                {
                    para.Add(db.CreateParam("FromPavHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.FromPavHt)));
                }
                else
                {
                    para.Add(db.CreateParam("FromPavHt", DbType.String, ParameterDirection.Input, DBNull.Value));
                }
                if (!string.IsNullOrEmpty(req.ToPavHt))
                {
                    para.Add(db.CreateParam("ToPavHt", DbType.String, ParameterDirection.Input, Convert.ToDecimal(req.ToPavHt)));
                }
                else
                {
                    para.Add(db.CreateParam("ToPavHt", DbType.String, ParameterDirection.Input, DBNull.Value));
                }

                DataTable Stock_dt = db.ExecuteSP("Get_SearchStock", para.ToArray(), false);

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
                Common.InsertErrorLog(ex, null, Request);
                return Ok(new ServiceResponse<Get_PriceListCategory_Res>
                {
                    Data = new List<Get_PriceListCategory_Res>(),
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
                Common.InsertErrorLog(ex, null, Request);
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
            return new Regex("[^0-9.-]").Replace(input, "");
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
        public (string, string, DataTable) Supplier_Stock_Get_From_His_WEB_API_AND_FTP(string APIType, string SupplierResponseFormat, string SupplierURL, string SupplierAPIMethod, string UserName, string Password, string FileLocation)
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
                            dt_APIRes = ConvertXLStoDataTable("", connString);
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
                            string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            dt_APIRes = ConvertXSLXtoDataTable("", connString);
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
                            dt_APIRes = ConvertCSVtoDataTable(FileLocation);
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

                            if (SupplierURL.ToUpper() == "HTTP://WWW.JPDIAM.COM/PLUGIN/APITOOL")
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

                                    WebClient client = new WebClient();
                                    client.Headers.Add("Content-type", "application/json");
                                    client.Encoding = Encoding.UTF8;
                                    json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);
                                    client.Dispose();
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

                                        WebClient client1 = new WebClient();
                                        client1.Headers.Add("Content-type", "application/json");
                                        client1.Encoding = Encoding.UTF8;
                                        json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockData", "POST", InputSRJson);
                                        client1.Dispose();
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

                                    WebClient client = new WebClient();
                                    client.Headers.Add("Content-type", "application/json");
                                    client.Encoding = Encoding.UTF8;
                                    json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);
                                    client.Dispose();
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

                                        WebClient client1 = new WebClient();
                                        client1.Headers.Add("Content-type", "application/json");
                                        client1.Encoding = Encoding.UTF8;
                                        json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataIndia", "POST", InputSRJson);
                                        client1.Dispose();
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

                                    WebClient client = new WebClient();
                                    client.Headers.Add("Content-type", "application/json");
                                    client.Encoding = Encoding.UTF8;
                                    json = client.UploadString("https://shairugems.net:8011/api/Buyer/login", "POST", InputLRJson);
                                    client.Dispose();
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

                                        WebClient client1 = new WebClient();
                                        client1.Headers.Add("Content-type", "application/json");
                                        client1.Encoding = Encoding.UTF8;
                                        json = client1.UploadString("https://shairugems.net:8011/api/Buyer/GetStockDataDubai", "POST", InputSRJson);
                                        client1.Dispose();
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

                            if (SupplierURL.ToUpper() == "HTTP://WWW.STARLIGHTDIAMONDS.IN/API/GETSTOCK")
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
                        if (SupplierAPIMethod.ToUpper() == "POST")
                        {
                            string json = "";
                            if (SupplierURL.ToUpper() == "HTTPS://SS.SRK.BEST/V1/STOCKSHARING/SERVICES")
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
            if (dt_APIRes != null)
            {
                return ("SUCCESS", string.Empty, dt_APIRes);
            }
            else
            {
                return ("Data Not Found", string.Empty, dt_APIRes);
            }
        }
        public DataTable ColumnMapping_In_SupplierStock(string StockFrom, int SupplierId, DataTable dt_APIRes, DataTable dtSupplCol)
        {
            DataTable Final_dt = new DataTable();
            Database db = new Database();
            List<IDbDataParameter> list = new List<IDbDataParameter>();
            try
            {
                Final_dt = new DataTable();
                Final_dt.Columns.Add("Stock From", typeof(string));
                Final_dt.Columns.Add("SupplierId", typeof(string));
                Final_dt.Columns.Add("Shape", typeof(string));
                Final_dt.Columns.Add("Color", typeof(string));
                Final_dt.Columns.Add("Clarity", typeof(string));
                Final_dt.Columns.Add("Cut", typeof(string));
                Final_dt.Columns.Add("Polish", typeof(string));
                Final_dt.Columns.Add("Symm", typeof(string));
                Final_dt.Columns.Add("Fls", typeof(string));
                Final_dt.Columns.Add("Cts", typeof(string));
                Final_dt.Columns.Add("Pointer", typeof(string));
                Final_dt.Columns.Add("Sub Pointer", typeof(string));
                Final_dt.Columns.Add("Base Price Cts", typeof(string));
                Final_dt.Columns.Add("Rap Rate", typeof(string));
                Final_dt.Columns.Add("Base Amount", typeof(string));
                Final_dt.Columns.Add("Measurement", typeof(string));
                Final_dt.Columns.Add("Length", typeof(string));
                Final_dt.Columns.Add("Width", typeof(string));
                Final_dt.Columns.Add("Depth", typeof(string));
                Final_dt.Columns.Add("Table Per", typeof(string));
                Final_dt.Columns.Add("Depth Per", typeof(string));
                Final_dt.Columns.Add("Table Inclusion", typeof(string));
                Final_dt.Columns.Add("Crown Inclusion", typeof(string));
                Final_dt.Columns.Add("Table Natts", typeof(string));
                Final_dt.Columns.Add("Crown Natts", typeof(string));
                Final_dt.Columns.Add("Side Inclusion", typeof(string));
                Final_dt.Columns.Add("Side Natts", typeof(string));
                Final_dt.Columns.Add("Crown Open", typeof(string));
                Final_dt.Columns.Add("Pav Open", typeof(string));
                Final_dt.Columns.Add("Table Open", typeof(string));
                Final_dt.Columns.Add("Girdle Open", typeof(string));
                Final_dt.Columns.Add("Crown Angle", typeof(string));
                Final_dt.Columns.Add("Pav Angle", typeof(string));
                Final_dt.Columns.Add("Crown Height", typeof(string));
                Final_dt.Columns.Add("Pav Height", typeof(string));
                Final_dt.Columns.Add("Rap Amount", typeof(string));
                Final_dt.Columns.Add("Lab", typeof(string));
                Final_dt.Columns.Add("Certificate URL", typeof(string));
                Final_dt.Columns.Add("Image URL", typeof(string));
                Final_dt.Columns.Add("Image URL 2", typeof(string));
                Final_dt.Columns.Add("Image URL 3", typeof(string));
                Final_dt.Columns.Add("Image URL 4", typeof(string));
                Final_dt.Columns.Add("Video URL", typeof(string));
                Final_dt.Columns.Add("Video URL 2", typeof(string));
                Final_dt.Columns.Add("Status", typeof(string));
                Final_dt.Columns.Add("Supplier Stone Id", typeof(string));
                Final_dt.Columns.Add("Location", typeof(string));
                Final_dt.Columns.Add("Shade", typeof(string));
                Final_dt.Columns.Add("Luster", typeof(string));
                Final_dt.Columns.Add("Type 2A", typeof(string));
                Final_dt.Columns.Add("Milky", typeof(string));
                Final_dt.Columns.Add("BGM", typeof(string));
                Final_dt.Columns.Add("Key To Symboll", typeof(string));
                Final_dt.Columns.Add("RATIO", typeof(string));
                Final_dt.Columns.Add("Supplier Comments", typeof(string));
                Final_dt.Columns.Add("Lab Comments", typeof(string));
                Final_dt.Columns.Add("Culet", typeof(string));
                Final_dt.Columns.Add("Girdle Per", typeof(string));
                Final_dt.Columns.Add("Girdle Type", typeof(string));
                Final_dt.Columns.Add("Girdle MM", typeof(string));
                Final_dt.Columns.Add("Inscription", typeof(string));
                Final_dt.Columns.Add("Culet Condition", typeof(string));
                Final_dt.Columns.Add("Star Length", typeof(string));
                Final_dt.Columns.Add("Lower Halves", typeof(string));
                Final_dt.Columns.Add("Stage", typeof(string));
                Final_dt.Columns.Add("Certi Date", typeof(string));
                Final_dt.Columns.Add("Disc", typeof(string));
                Final_dt.Columns.Add("Fix Price", typeof(string));
                Final_dt.Columns.Add("Certificate No", typeof(string));
                Final_dt.Columns.Add("Ref No", typeof(string));
                Final_dt.Columns.Add("Goods Type", typeof(string));
                Final_dt.Columns.Add("Origin", typeof(string));
                Final_dt.Columns.Add("Girdle", typeof(string));
                Final_dt.Columns.Add("HNA", typeof(string));
                Final_dt.Columns.Add("Fls Color", typeof(string));
                Final_dt.Columns.Add("Fancy Color", typeof(string));
                Final_dt.Columns.Add("Fancy Overtone", typeof(string));
                Final_dt.Columns.Add("Fancy Intensity", typeof(string));

                db = new Database();
                list = new List<IDbDataParameter>();
                DataTable Col_dt = db.ExecuteSP("Get_Column_Master", list.ToArray(), false);

                foreach (DataRow row in dt_APIRes.Rows)
                {
                    DataRow Final_row = Final_dt.NewRow();

                    Final_row["Stock From"] = StockFrom;
                    Final_row["SupplierId"] = SupplierId.ToString();

                    foreach (DataRow SuppCol_row in dtSupplCol.Rows)
                    {
                        Final_row["Shape"] = ((SuppCol_row["Column_Name"].ToString() != "Shape") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Shape"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Shape"] = (Final_row["Shape"].ToString() == "") ? null : Final_row["Shape"];

                        Final_row["Color"] = ((SuppCol_row["Column_Name"].ToString() != "Color") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Color"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Color"] = (Final_row["Color"].ToString() == "") ? null : Final_row["Color"];

                        Final_row["Clarity"] = ((SuppCol_row["Column_Name"].ToString() != "Clarity") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Clarity"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Clarity"] = (Final_row["Clarity"].ToString() == "") ? null : Final_row["Clarity"];

                        Final_row["Cut"] = ((SuppCol_row["Column_Name"].ToString() != "Cut") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Cut"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Cut"] = (Final_row["Cut"].ToString() == "") ? null : Final_row["Cut"];

                        Final_row["Polish"] = ((SuppCol_row["Column_Name"].ToString() != "Polish") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Polish"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Polish"] = (Final_row["Polish"].ToString() == "") ? null : Final_row["Polish"];

                        Final_row["Symm"] = ((SuppCol_row["Column_Name"].ToString() != "Symm") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Symm"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Symm"] = (Final_row["Symm"].ToString() == "") ? null : Final_row["Symm"];

                        Final_row["Fls"] = ((SuppCol_row["Column_Name"].ToString() != "Fls") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Fls"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Fls"] = (Final_row["Fls"].ToString() == "") ? null : Final_row["Fls"];

                        Final_row["Cts"] = ((SuppCol_row["Column_Name"].ToString() != "Cts") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Cts"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Cts"] = (Final_row["Cts"].ToString() == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Cts"]));

                        Final_row["Pointer"] = ((SuppCol_row["Column_Name"].ToString() != "Pointer") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Pointer"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Pointer"] = (Final_row["Pointer"].ToString() == "") ? null : Final_row["Pointer"];

                        Final_row["Sub Pointer"] = ((SuppCol_row["Column_Name"].ToString() != "Sub Pointer") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Sub Pointer"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Sub Pointer"] = (Final_row["Sub Pointer"].ToString() == "") ? null : Final_row["Sub Pointer"];

                        Final_row["Base Price Cts"] = ((SuppCol_row["Column_Name"].ToString() != "Base Price Cts") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Base Price Cts"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Base Price Cts"] = (Final_row["Base Price Cts"].ToString() == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Base Price Cts"]));

                        Final_row["Rap Rate"] = ((SuppCol_row["Column_Name"].ToString() != "Rap Rate") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Rap Rate"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Rap Rate"] = (Final_row["Rap Rate"].ToString() == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Rap Rate"]));

                        Final_row["Base Amount"] = ((SuppCol_row["Column_Name"].ToString() != "Base Amount") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Base Amount"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Base Amount"] = (Final_row["Base Amount"].ToString() == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Base Amount"]));

                        Final_row["Measurement"] = ((SuppCol_row["Column_Name"].ToString() != "Measurement") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Measurement"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Measurement"] = (Final_row["Measurement"].ToString() == "") ? null : Final_row["Measurement"];

                        Final_row["Length"] = ((SuppCol_row["Column_Name"].ToString() != "Length") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Length"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Length"] = (Final_row["Length"].ToString() == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Length"]));

                        Final_row["Width"] = ((SuppCol_row["Column_Name"].ToString() != "Width") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Width"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Width"] = (Final_row["Width"].ToString() == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Width"]));

                        Final_row["Depth"] = ((SuppCol_row["Column_Name"].ToString() != "Depth") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Depth"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Depth"] = (Final_row["Depth"].ToString() == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Depth"]));

                        Final_row["Table Per"] = ((SuppCol_row["Column_Name"].ToString() != "Table Per") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Table Per"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Table Per"] = (Final_row["Table Per"].ToString() == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Table Per"]));

                        Final_row["Depth Per"] = ((SuppCol_row["Column_Name"].ToString() != "Depth Per") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Depth Per"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Depth Per"] = (Final_row["Depth Per"].ToString() == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Depth Per"]));

                        Final_row["Table Inclusion"] = ((SuppCol_row["Column_Name"].ToString() != "Table Inclusion") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Table Inclusion"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Table Inclusion"] = (Final_row["Table Inclusion"].ToString() == "") ? null : Final_row["Table Inclusion"];

                        Final_row["Crown Inclusion"] = ((SuppCol_row["Column_Name"].ToString() != "Crown Inclusion") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Crown Inclusion"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Crown Inclusion"] = (Final_row["Crown Inclusion"].ToString() == "") ? null : Final_row["Crown Inclusion"];

                        Final_row["Table Natts"] = ((SuppCol_row["Column_Name"].ToString() != "Table Natts") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Table Natts"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Table Natts"] = (Final_row["Table Natts"].ToString() == "") ? null : Final_row["Table Natts"];

                        Final_row["Crown Natts"] = ((SuppCol_row["Column_Name"].ToString() != "Crown Natts") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Crown Natts"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Crown Natts"] = (Final_row["Crown Natts"].ToString() == "") ? null : Final_row["Crown Natts"];

                        Final_row["Side Inclusion"] = ((SuppCol_row["Column_Name"].ToString() != "Side Inclusion") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Side Inclusion"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Side Inclusion"] = (Final_row["Side Inclusion"].ToString() == "") ? null : Final_row["Side Inclusion"];

                        Final_row["Side Natts"] = ((SuppCol_row["Column_Name"].ToString() != "Side Natts") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Side Natts"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Side Natts"] = (Final_row["Side Natts"].ToString() == "") ? null : Final_row["Side Natts"];

                        Final_row["Crown Open"] = ((SuppCol_row["Column_Name"].ToString() != "Crown Open") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Crown Open"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Crown Open"] = (Final_row["Crown Open"].ToString() == "") ? null : Final_row["Crown Open"];

                        Final_row["Pav Open"] = ((SuppCol_row["Column_Name"].ToString() != "Pav Open") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Pav Open"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Pav Open"] = (Final_row["Pav Open"].ToString() == "") ? null : Final_row["Pav Open"];

                        Final_row["Table Open"] = ((SuppCol_row["Column_Name"].ToString() != "Table Open") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Table Open"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Table Open"] = (Final_row["Table Open"].ToString() == "") ? null : Final_row["Table Open"];

                        Final_row["Girdle Open"] = ((SuppCol_row["Column_Name"].ToString() != "Girdle Open") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Girdle Open"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Girdle Open"] = (Final_row["Girdle Open"].ToString() == "") ? null : Final_row["Girdle Open"];

                        Final_row["Crown Angle"] = ((SuppCol_row["Column_Name"].ToString() != "Crown Angle") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Crown Angle"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Crown Angle"] = (Final_row["Crown Angle"].ToString() == "") ? null : Final_row["Crown Angle"];

                        Final_row["Pav Angle"] = ((SuppCol_row["Column_Name"].ToString() != "Pav Angle") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Pav Angle"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Pav Angle"] = (Final_row["Pav Angle"].ToString() == "") ? null : Final_row["Pav Angle"];

                        Final_row["Crown Height"] = ((SuppCol_row["Column_Name"].ToString() != "Crown Height") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Crown Height"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Crown Height"] = (Final_row["Crown Height"].ToString() == "") ? null : Final_row["Crown Height"];

                        Final_row["Pav Height"] = ((SuppCol_row["Column_Name"].ToString() != "Pav Height") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Pav Height"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Pav Height"] = (Final_row["Pav Height"].ToString() == "") ? null : Final_row["Pav Height"];

                        Final_row["Rap Amount"] = ((SuppCol_row["Column_Name"].ToString() != "Rap Amount") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Rap Amount"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Rap Amount"] = (Final_row["Rap Amount"].ToString() == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Rap Amount"]));

                        Final_row["Lab"] = ((SuppCol_row["Column_Name"].ToString() != "Lab") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Lab"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Lab"] = (Final_row["Lab"].ToString() == "") ? null : Final_row["Lab"];

                        Final_row["Certificate URL"] = ((SuppCol_row["Column_Name"].ToString() != "Certificate URL") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Certificate URL"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Certificate URL"] = (Final_row["Certificate URL"].ToString() == "") ? null : Final_row["Certificate URL"];

                        Final_row["Image URL"] = ((SuppCol_row["Column_Name"].ToString() != "Image URL") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Image URL"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Image URL"] = (Final_row["Image URL"].ToString() == "") ? null : Final_row["Image URL"];

                        Final_row["Image URL 2"] = ((SuppCol_row["Column_Name"].ToString() != "Image URL 2") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Image URL 2"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Image URL 2"] = (Final_row["Image URL 2"].ToString() == "") ? null : Final_row["Image URL 2"];

                        Final_row["Image URL 3"] = ((SuppCol_row["Column_Name"].ToString() != "Image URL 3") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Image URL 3"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Image URL 3"] = (Final_row["Image URL 3"].ToString() == "") ? null : Final_row["Image URL 3"];

                        Final_row["Image URL 4"] = ((SuppCol_row["Column_Name"].ToString() != "Image URL 4") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Image URL 4"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Image URL 4"] = (Final_row["Image URL 4"].ToString() == "") ? null : Final_row["Image URL 4"];

                        Final_row["Video URL"] = ((SuppCol_row["Column_Name"].ToString() != "Video URL") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Video URL"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Video URL"] = (Final_row["Video URL"].ToString() == "") ? null : Final_row["Video URL"];

                        Final_row["Video URL 2"] = ((SuppCol_row["Column_Name"].ToString() != "Video URL 2") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Video URL 2"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Video URL 2"] = (Final_row["Video URL 2"].ToString() == "") ? null : Final_row["Video URL 2"];

                        Final_row["Status"] = ((SuppCol_row["Column_Name"].ToString() != "Status") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Status"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Status"] = (Final_row["Status"].ToString() == "") ? null : Final_row["Status"];

                        Final_row["Supplier Stone Id"] = ((SuppCol_row["Column_Name"].ToString() != "Supplier Stone Id") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Supplier Stone Id"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Supplier Stone Id"] = (Final_row["Supplier Stone Id"].ToString() == "") ? null : Final_row["Supplier Stone Id"];

                        Final_row["Location"] = ((SuppCol_row["Column_Name"].ToString() != "Location") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Location"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Location"] = (Final_row["Location"].ToString() == "") ? null : Final_row["Location"];

                        Final_row["Shade"] = ((SuppCol_row["Column_Name"].ToString() != "Shade") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Shade"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Shade"] = (Final_row["Shade"].ToString() == "") ? null : Final_row["Shade"];

                        Final_row["Luster"] = ((SuppCol_row["Column_Name"].ToString() != "Luster") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Luster"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Luster"] = (Final_row["Luster"].ToString() == "") ? null : Final_row["Luster"];

                        Final_row["Type 2A"] = ((SuppCol_row["Column_Name"].ToString() != "Type 2A") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Type 2A"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Type 2A"] = (Final_row["Type 2A"].ToString() == "") ? null : Final_row["Type 2A"];

                        Final_row["Milky"] = ((SuppCol_row["Column_Name"].ToString() != "Milky") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Milky"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Milky"] = (Final_row["Milky"].ToString() == "") ? null : Final_row["Milky"];

                        Final_row["BGM"] = ((SuppCol_row["Column_Name"].ToString() != "BGM") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["BGM"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["BGM"] = (Final_row["BGM"].ToString() == "") ? null : Final_row["BGM"];

                        Final_row["Key To Symboll"] = ((SuppCol_row["Column_Name"].ToString() != "Key To Symboll") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Key To Symboll"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Key To Symboll"] = (Final_row["Key To Symboll"].ToString() == "") ? null : Final_row["Key To Symboll"];

                        Final_row["RATIO"] = ((SuppCol_row["Column_Name"].ToString() != "RATIO") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["RATIO"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["RATIO"] = (Final_row["RATIO"].ToString() == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["RATIO"]));

                        Final_row["Supplier Comments"] = ((SuppCol_row["Column_Name"].ToString() != "Supplier Comments") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Supplier Comments"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Supplier Comments"] = (Final_row["Supplier Comments"].ToString() == "") ? null : Final_row["Supplier Comments"];

                        Final_row["Lab Comments"] = ((SuppCol_row["Column_Name"].ToString() != "Lab Comments") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Lab Comments"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Lab Comments"] = (Final_row["Lab Comments"].ToString() == "") ? null : Final_row["Lab Comments"];

                        Final_row["Culet"] = ((SuppCol_row["Column_Name"].ToString() != "Culet") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Culet"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Culet"] = (Final_row["Culet"].ToString() == "") ? null : Final_row["Culet"];

                        Final_row["Girdle Per"] = ((SuppCol_row["Column_Name"].ToString() != "Girdle Per") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Girdle Per"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Girdle Per"] = (Final_row["Girdle Per"].ToString() == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Girdle Per"]));

                        Final_row["Girdle Type"] = ((SuppCol_row["Column_Name"].ToString() != "Girdle Type") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Girdle Type"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Girdle Type"] = (Final_row["Girdle Type"].ToString() == "") ? null : Final_row["Girdle Type"];

                        Final_row["Girdle MM"] = ((SuppCol_row["Column_Name"].ToString() != "Girdle MM") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Girdle MM"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Girdle MM"] = (Final_row["Girdle MM"].ToString() == "") ? null : Final_row["Girdle MM"];

                        Final_row["Inscription"] = ((SuppCol_row["Column_Name"].ToString() != "Inscription") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Inscription"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Inscription"] = (Final_row["Inscription"].ToString() == "") ? null : Final_row["Inscription"];

                        Final_row["Culet Condition"] = ((SuppCol_row["Column_Name"].ToString() != "Culet Condition") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Culet Condition"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Culet Condition"] = (Final_row["Culet Condition"].ToString() == "") ? null : Final_row["Culet Condition"];

                        Final_row["Star Length"] = ((SuppCol_row["Column_Name"].ToString() != "Star Length") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Star Length"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Star Length"] = (Final_row["Star Length"].ToString() == "") ? null : Final_row["Star Length"];

                        Final_row["Lower Halves"] = ((SuppCol_row["Column_Name"].ToString() != "Lower Halves") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Lower Halves"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Lower Halves"] = (Final_row["Lower Halves"].ToString() == "") ? null : Final_row["Lower Halves"];

                        Final_row["Stage"] = ((SuppCol_row["Column_Name"].ToString() != "Stage") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Stage"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Stage"] = (Final_row["Stage"].ToString() == "") ? null : Final_row["Stage"];

                        Final_row["Certi Date"] = ((SuppCol_row["Column_Name"].ToString() != "Certi Date") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Certi Date"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Certi Date"] = (Final_row["Certi Date"].ToString() == "") ? null : Final_row["Certi Date"];

                        Final_row["Disc"] = ((SuppCol_row["Column_Name"].ToString() != "Disc") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Disc"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Disc"] = (Final_row["Disc"].ToString() == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Disc"]));

                        Final_row["Fix Price"] = ((SuppCol_row["Column_Name"].ToString() != "Fix Price") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Fix Price"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Fix Price"] = (Final_row["Fix Price"].ToString() == "") ? null : RemoveNonNumericAndDotAndNegativeCharacters(Convert.ToString(Final_row["Fix Price"]));

                        Final_row["Certificate No"] = ((SuppCol_row["Column_Name"].ToString() != "Certificate No") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Certificate No"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Certificate No"] = (Final_row["Certificate No"].ToString() == "") ? null : Final_row["Certificate No"];

                        Final_row["Ref No"] = ((SuppCol_row["Column_Name"].ToString() != "Ref No") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Ref No"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Ref No"] = (Final_row["Ref No"].ToString() == "") ? null : Final_row["Ref No"];

                        Final_row["Goods Type"] = ((SuppCol_row["Column_Name"].ToString() != "Goods Type") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Goods Type"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Goods Type"] = (Final_row["Goods Type"].ToString() == "") ? null : Final_row["Goods Type"];

                        Final_row["Origin"] = ((SuppCol_row["Column_Name"].ToString() != "Origin") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Origin"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Origin"] = (Final_row["Origin"].ToString() == "") ? null : Final_row["Origin"];

                        Final_row["Girdle"] = ((SuppCol_row["Column_Name"].ToString() != "Girdle") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Girdle"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Girdle"] = (Final_row["Girdle"].ToString() == "") ? null : Final_row["Girdle"];

                        Final_row["HNA"] = ((SuppCol_row["Column_Name"].ToString() != "HNA") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["HNA"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["HNA"] = (Final_row["HNA"].ToString() == "") ? null : Final_row["HNA"];

                        Final_row["Fls Color"] = ((SuppCol_row["Column_Name"].ToString() != "Fls Color") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Fls Color"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Fls Color"] = (Final_row["Fls Color"].ToString() == "") ? null : Final_row["Fls Color"];

                        Final_row["Fancy Color"] = ((SuppCol_row["Column_Name"].ToString() != "Fancy Color") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Fancy Color"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Fancy Color"] = (Final_row["Fancy Color"].ToString() == "") ? null : Final_row["Fancy Color"];

                        Final_row["Fancy Overtone"] = ((SuppCol_row["Column_Name"].ToString() != "Fancy Overtone") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Fancy Overtone"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Fancy Overtone"] = (Final_row["Fancy Overtone"].ToString() == "") ? null : Final_row["Fancy Overtone"];

                        Final_row["Fancy Intensity"] = ((SuppCol_row["Column_Name"].ToString() != "Fancy Intensity") || (SuppCol_row["SupplierColumn"].ToString() == "")) ? Final_row["Fancy Intensity"].ToString() : row[SuppCol_row["SupplierColumn"].ToString()].ToString();
                        Final_row["Fancy Intensity"] = (Final_row["Fancy Intensity"].ToString() == "") ? null : Final_row["Fancy Intensity"];
                    }
                    Final_dt.Rows.Add(Final_row);
                }

                if (Final_dt != null && Final_dt.Rows.Count > 0)
                {
                    foreach (DataColumn column in Final_dt.Columns)
                    {
                        db = new Database();
                        List<IDbDataParameter> para = new List<IDbDataParameter>();
                        para.Add(db.CreateParam("Column_Name", DbType.String, ParameterDirection.Input, column.ColumnName));
                        para.Add(db.CreateParam("SupplierId", DbType.Int64, ParameterDirection.Input, SupplierId));

                        DataTable Synonym_dt = db.ExecuteSP("Get_Synonyms_From_Cat_Sup_Val", para.ToArray(), false);

                        if (Synonym_dt != null && Synonym_dt.Rows.Count > 0)
                        {
                            foreach (DataRow Final_row in Final_dt.Rows)
                            {
                                foreach (DataRow SuppCol_row in Synonym_dt.Rows)
                                {
                                    string str = Final_row[column.ColumnName].ToString();
                                    string str2 = SuppCol_row["Cat_Name"].ToString();
                                    string str3 = SuppCol_row["Synonyms"].ToString();

                                    string[] strArray = str3.Split(',');

                                    foreach (string str4 in strArray)
                                    {
                                        if (str.Contains(str4))
                                        {
                                            Final_row[column.ColumnName] = str2;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return Final_dt;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public static DataTable ConvertCSVtoDataTable(string csvFilePath)
        {
            DataTable table = new DataTable();
            using (TextFieldParser parser = new TextFieldParser(csvFilePath))
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
            return table;
        }
        public static DataTable ConvertXLStoDataTable(string strFilePath, string connString)
        {
            DataTable dataTable = new DataTable();

            using (OleDbConnection connection = new OleDbConnection(connString))
            {
                connection.Open();
                string str = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter("SELECT * FROM [" + str + "]", connection))
                {
                    adapter.Fill(dataTable);
                }
                connection.Close();
            }

            return dataTable;
        }
        public static DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        {
            OleDbConnection connection = new OleDbConnection(connString);
            DataTable table = new DataTable();
            connection.Open();
            object[] restrictions = new object[4];
            restrictions[3] = "TABLE";
            string str = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions).Rows[0]["TABLE_NAME"].ToString();
            using (OleDbCommand command = new OleDbCommand("SELECT * FROM [" + str + "]", connection))
            {
                OleDbDataAdapter adapter = new OleDbDataAdapter
                {
                    SelectCommand = command
                };
                DataSet dataSet = new DataSet();
                adapter.Fill(dataSet);
                table = dataSet.Tables[0];
            }
            connection.Close();

            return table;
        }


        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult AddUpdate_SupplierStock()
        {
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

                DataTable dtSuppl = db.ExecuteSP("Get_SupplierMasterScheduler", para.ToArray(), false);

                if (dtSuppl != null && dtSuppl.Rows.Count > 0)
                {
                    TotCount = dtSuppl.Rows.Count;

                    DataTable dt = new DataTable();
                    dt.Columns.Add("SupplierId", typeof(int));

                    for (int j = 0; j < dtSuppl.Rows.Count; j++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["SupplierId"] = Convert.ToInt32(dtSuppl.Rows[j]["Id"].ToString());
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
                            SupplierId = Convert.ToInt32(dtSuppl.Rows[i]["Id"].ToString());

                            db = new Database();
                            para = new List<IDbDataParameter>();

                            para.Add(db.CreateParam("SupplierId", DbType.Int64, ParameterDirection.Input, SupplierId));
                            DataTable dtSupplCol = db.ExecuteSP("Get_SupplierCol_OurCol_Merge", para.ToArray(), false);

                            if (dtSupplCol != null && dtSupplCol.Rows.Count > 0)
                            {
                                Supplier_Start_End(SupplierId, "Start");

                                string tempPath = dtSuppl.Rows[i]["FileLocation"].ToString(),
                                    APIFileName = dtSuppl.Rows[i]["FileName"].ToString(),
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

                                var (msg, ex, dt1) = Supplier_Stock_Get_From_His_WEB_API_AND_FTP(dtSuppl.Rows[i]["APIType"].ToString(),
                                    dtSuppl.Rows[i]["SupplierResponseFormat"].ToString(),
                                    dtSuppl.Rows[i]["SupplierURL"].ToString(),
                                    dtSuppl.Rows[i]["SupplierAPIMethod"].ToString(),
                                    dtSuppl.Rows[i]["UserName"].ToString(),
                                    dtSuppl.Rows[i]["Password"].ToString(),
                                    dtSuppl.Rows[i]["FileLocation"].ToString());

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
                                            sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                            sb.Append(SupStkUploadDT.Rows[0]["Message"].ToString() + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                            sb.AppendLine("");
                                            File.AppendAllText(path, sb.ToString());
                                            sb.Clear();

                                            if (SupStkUploadDT.Rows[0]["Status"].ToString() == "1")
                                            {
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
                                                ApiLog(SupplierId, true, SupStkUploadDT.Rows[0]["Message"].ToString());
                                            }
                                        }
                                        else
                                        {
                                            ApiLog(SupplierId, false, dtSuppl.Rows[i]["SupplierName"].ToString() + " Supplier Stock Upload Failed");

                                            sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                            sb.Append(dtSuppl.Rows[i]["SupplierName"].ToString() + " Supplier Stock Upload Failed, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                            sb.AppendLine("");
                                            File.AppendAllText(path, sb.ToString());
                                            sb.Clear();
                                        }
                                    }
                                    else
                                    {
                                        ApiLog(SupplierId, false, "Column Setting Mapping Failed From " + dtSuppl.Rows[i]["SupplierName"].ToString() + " Supplier's " + stockFrom);
                                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                        sb.Append("Column Setting Mapping Failed From " + dtSuppl.Rows[i]["SupplierName"].ToString() + " Supplier's " + stockFrom + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                        sb.AppendLine("");
                                        File.AppendAllText(path, sb.ToString());
                                        sb.Clear();
                                    }
                                }
                                else
                                {
                                    string _msg = ((Message != "ERROR") ? (Message + ((!string.IsNullOrEmpty(Exception)) ? " " + Exception + " " : "") + " From " + dtSuppl.Rows[i]["SupplierName"].ToString() + " Supplier's " + stockFrom) : ("Stock Not Found From " + dtSuppl.Rows[i]["SupplierName"].ToString() + " Supplier's " + stockFrom));

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
                                ApiLog(SupplierId, false, "Column Setting Not Found From " + dtSuppl.Rows[i]["SupplierName"].ToString() + " Supplier's " + stockFrom);
                                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                sb.Append("Column Setting Not Found From " + dtSuppl.Rows[i]["SupplierName"].ToString() + " Supplier's " + stockFrom + ", Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                sb.AppendLine("");
                                File.AppendAllText(path, sb.ToString());
                                sb.Clear();
                            }

                        }
                        catch (Exception ex)
                        {
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
                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                sb.Append(ex.Message.ToString() + " " + ex.StackTrace.ToString() + ", Log Time: " + DateTime.Now.ToString("dd -MM-yyyy hh:mm:ss tt"));
                sb.AppendLine("");
                File.AppendAllText(path, sb.ToString());
                sb.Clear();
            }
            return Ok(new CommonResponse
            {
                Message = "",
                Status = "",
                Error = ""
            });
        }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult AddUpdate_SupplierStock_FromFile([FromBody] JObject data)
        {
            JObject test1 = JObject.Parse(data.ToString());
            Data_Get_From_File_Req req = new Data_Get_From_File_Req();
            try
            {
                req = JsonConvert.DeserializeObject<Data_Get_From_File_Req>(((Newtonsoft.Json.Linq.JProperty)test1.Last).Name.ToString());
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, Request);
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
                    Stock_dt = ConvertXLStoDataTable("", connString);
                }
                else if (str2 == ".xlsx")
                {
                    string connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + req.FilePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    Stock_dt = ConvertXSLXtoDataTable("", connString);
                }
                else if (str2 == ".csv")
                {
                    Stock_dt = ConvertCSVtoDataTable(req.FilePath);
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
                                        Message = "Stock Upload Has Been Successfully From " + req.SupplierName + " Supplier's File",
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
                                ApiLog(req.SupplierId, false, "Stock Upload Failed From " + req.SupplierName + " Supplier's File");

                                sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                                sb.Append("Stock Upload Failed From " + req.SupplierName + " Supplier's File, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                                sb.AppendLine("");
                                File.AppendAllText(path, sb.ToString());
                                sb.Clear();

                                return Ok(new CommonResponse
                                {
                                    Error = "",
                                    Message = "Stock Upload Failed From " + req.SupplierName + " Supplier's File",
                                    Status = "0"
                                });
                            }
                        }
                        else
                        {
                            ApiLog(req.SupplierId, false, "Column Setting Mapping Failed From " + req.SupplierName + " Supplier's File");
                            sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                            sb.Append("Column Setting Mapping Failed From " + req.SupplierName + " Supplier's File, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                            sb.AppendLine("");
                            File.AppendAllText(path, sb.ToString());
                            sb.Clear();

                            return Ok(new CommonResponse
                            {
                                Error = "",
                                Message = "Column Setting Mapping Failed From " + req.SupplierName + " Supplier's File",
                                Status = "0"
                            });
                        }
                    }
                    else
                    {
                        ApiLog(req.SupplierId, false, "Column Setting Not Found From " + req.SupplierName + " Supplier's File");

                        sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                        sb.Append("Column Setting Not Found From " + req.SupplierName + " Supplier's File, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                        sb.AppendLine("");
                        File.AppendAllText(path, sb.ToString());
                        sb.Clear();

                        return Ok(new CommonResponse
                        {
                            Error = "",
                            Message = "Column Setting Not Found From " + req.SupplierName + " Supplier's File",
                            Status = "0"
                        });
                    }
                }
                else
                {
                    ApiLog(req.SupplierId, false, "Stock Not Found From " + req.SupplierName + " Supplier's File");

                    sb.AppendLine("= = = = = = = = = = = = = = = = = = = = = = = = = = = ");
                    sb.Append("Stock Not Found From " + req.SupplierName + " Supplier's File, Log Time : " + DateTime.Now.ToString("dd-MM-yyyy hh:mm:ss tt"));
                    sb.AppendLine("");
                    File.AppendAllText(path, sb.ToString());
                    sb.Clear();

                    return Ok(new CommonResponse
                    {
                        Error = "",
                        Message = "Stock Not Found From " + req.SupplierName + " Supplier's File",
                        Status = "0"
                    });
                }
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, Request);
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
    }
}
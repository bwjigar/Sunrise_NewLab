using System;
using EpExcelExportLib;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Configuration;
using System.Web;
using System.Threading;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Security.Claims;
using System.Net.Http;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Globalization;

namespace Lib.Model
{
    public static class Common
    {
        
        public static DateTime GetHKTime()
        {
            DateTime dt = DateTime.Now.ToUniversalTime();
            dt = TimeZoneInfo.ConvertTimeFromUtc(dt, TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));

            return dt;
        }

        public static DateTime GetGMTime(DateTime ust)
        {
            DateTime dt;//= ust.ToUniversalTime();
            dt = ust.AddHours(5);
            return dt;
        }
        private static void SendMail(string fsToAdd, string fsSubject, string fsMsgBody, string fsCCAdd, int? fiOrderId, bool AdminMail, Int64? UserId, string MailFrom, bool bIsOrder)
        {
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                loMail.To.Add(fsToAdd);
                if (!string.IsNullOrEmpty(fsCCAdd))
                    loMail.Bcc.Add(fsCCAdd);
                if (bIsOrder == false)
                {
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["CCEmail"]))
                        loMail.Bcc.Add(ConfigurationManager.AppSettings["CCEmail"]);
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["BCCEmail"]))
                        loMail.Bcc.Add(ConfigurationManager.AppSettings["BCCEmail"]);
                }
                loMail.Subject = fsSubject;
                loMail.IsBodyHtml = true;

                AlternateView av = AlternateView.CreateAlternateViewFromString(fsMsgBody, null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);

                Thread email = new Thread(delegate ()
                {
                    loSmtp.Send(loMail);
                });

                email.IsBackground = true;
                email.Start();
                if (!email.IsAlive)
                {
                    email.Abort();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static bool EmailOfSuspendedUser(string ToAssEmail, string Name, string Username, string CompName)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear Sir/Madam,</p>");
                loSb.Append(@"<p>" + Username + " [ " + CompName + " ] has tried to login on our website [ Sunrise Lab Website ].<br />");
                loSb.Append(@" As per our company policy his/her account is suspended.<br /></p>");

                loSb.Append(EmailSignature());

                SendMail(ToAssEmail, "Unauthorised Login Attemt.", Convert.ToString(loSb), null, null, false, 0, "SuspendedUser", false);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static Boolean InsertErrorLog(Exception ex, string Message, HttpRequestMessage Request)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            para.Add(db.CreateParam("dtErrorDate", System.Data.DbType.DateTime, System.Data.ParameterDirection.Input, GetHKTime()));

            if (ex != null)
                para.Add(db.CreateParam("sErrorTrace", System.Data.DbType.String, System.Data.ParameterDirection.Input, ex.ToString()));
            else
                para.Add(db.CreateParam("sErrorTrace", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));
            if (ex != null)
                para.Add(db.CreateParam("sErrorMsg", System.Data.DbType.String, System.Data.ParameterDirection.Input, ex.Message.ToString() + Message));
            else if (Message != "")
                para.Add(db.CreateParam("sErrorMsg", System.Data.DbType.String, System.Data.ParameterDirection.Input, Message));
            else
                para.Add(db.CreateParam("sErrorMsg", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

            if (Request != null)
            {
                ClaimsPrincipal principal = Request.GetRequestContext().Principal as ClaimsPrincipal;
                List<Claim> claims = principal.Claims.ToList();

                if (claims.Count > 0)
                {
                    para.Add(db.CreateParam("iUserId", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToInt32(claims.Where(cl => cl.Type == "UserID").FirstOrDefault().Value)));
                    para.Add(db.CreateParam("sIPAddress", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToString(claims.Where(cl => cl.Type == "IpAddress").FirstOrDefault().Value)));
                    para.Add(db.CreateParam("sErrorSite", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToString(claims.Where(cl => cl.Type == "DeviseType").FirstOrDefault().Value)));
                }
            }
            para.Add(db.CreateParam("sErrorPage", System.Data.DbType.String, System.Data.ParameterDirection.Input, null));

            db.ExecuteSP("ErrorLog_Insert", para.ToArray(), false);
            return true;
        }
        public static string ToXML<T>(T obj)
        {
            using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString().Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");
            }
        }
        public static string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }



        public static String EmailHeader()
        {
            return @"<html><head><style type=""text/css"">body{font-family: Verdana,'sans-serif';font-size:12px;}p{text-align:justify;margin:10px 0 !important;}
                a{color:#1a4e94;text-decoration:none;font-weight:bold;}a:hover{color:#3c92fe;}table td{font-family: Verdana,'sans-serif' !important;font-size:12px;padding:3px;border-bottom:1px solid #dddddd;}
                </style></head><body>
                <div style=""width:100%; margin:5px auto;font-family: Verdana,'sans-serif';font-size:12px;line-height:20px; background-color:#f2f2f2;"">
                <div style=""padding:10px;"">";
        }

        public static String EmailSignature()
        {
            return @"<p>Please do let us know if you have any questions. Email us on <a href=""mailto:support@sunrisediamonds.com.hk"">support@sunrisediamonds.com.hk</a></p>
                <p>Thanks and Regards,<br />Sunrise Diamond Team,<br />Room 1,14/F, Peninsula Square<br/>East Wing, 18 Sung On Street<br/>Hunghom, Kowloon<br/>Hong Kong<br/>
                <a href=""https://sunrisediamonds.com.hk"">www.sunrisediamonds.com.hk</a></p>
                </div></div></body></html>";
        }

        public static bool EmailForgotPassword(string fsToAdd, string fsName, string fsUsername, string fsPassword)
        {
            try
            {
                StringBuilder loSb = new StringBuilder();
                loSb.Append(EmailHeader());

                loSb.Append(@"<p style=""font-size:18px; color:#1a4e94;"">Dear " + fsName + ",</p>");
                loSb.Append(@"<p>Thank you for requesting your account detail.</p>");
                loSb.Append(@"<p>Please store below information for further communication.<br />");
                loSb.Append(@"<b>Username: </b>" + fsUsername + "<br />");
                loSb.Append("<b>Password: </b>" + fsPassword + "<br /></p>");

                //loSb.Append(EmailSignature());

                SendMail(fsToAdd, "Sunrise Diamonds – Forgot Password – " + DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss"),
                    Convert.ToString(loSb));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static void SendMail(string fsToAdd, string fsSubject, string fsMsgBody)
        {
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");
                loMail.To.Add(fsToAdd);
                loMail.Bcc.Add("hardik@brainwaves.co.in");

                loMail.Subject = fsSubject;
                loMail.IsBodyHtml = true;

                AlternateView av = AlternateView.CreateAlternateViewFromString(fsMsgBody, null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);


                Thread email = new Thread(delegate ()
                {
                    loSmtp.Send(loMail);
                });

                email.IsBackground = true;
                email.Start();
                if (!email.IsAlive)
                {
                    email.Abort();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        public static void SendMail(string ToAdd, string Subject, string MsgBody, int? OrderId, int? UserCode)
        {
            MailMessage loMail = new MailMessage();
            SmtpClient loSmtp = new SmtpClient();
            try
            {
                loMail.From = new MailAddress(ConfigurationManager.AppSettings["FromEmail"], "Sunrise Diamonds");

                loMail.To.Add(ToAdd);
                loMail.Bcc.Add("hardik@brainwaves.co.in");
                loMail.Subject = Subject;
                loMail.IsBodyHtml = true;

                MsgBody = EmailHeader() + MsgBody;

                AlternateView av = AlternateView.CreateAlternateViewFromString(MsgBody, null, MediaTypeNames.Text.Html);
                loMail.AlternateViews.Add(av);


                System.IO.MemoryStream ms = ExportToStreamEpPlus(Convert.ToInt32(OrderId));
                Attachment attachFile = new Attachment(ms, "Order_" + DateTime.Now.ToString("dd-MMM-yyyy") + "-" + OrderId + ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                loMail.Attachments.Add(attachFile);

                Thread email = new Thread(delegate ()
                {
                    loSmtp.Send(loMail);
                });

                email.IsBackground = true;
                email.Start();
                if (!email.IsAlive)
                {
                    email.Abort();
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        private static System.IO.MemoryStream ExportToStreamEpPlus(int OrderId)
        {
            Database db = new Database();
            System.Collections.Generic.List<System.Data.IDbDataParameter> para;
            para = new System.Collections.Generic.List<System.Data.IDbDataParameter>();

            para.Add(db.CreateParam("OrderId", System.Data.DbType.String, System.Data.ParameterDirection.Input, Convert.ToInt32(OrderId)));
            System.Data.DataTable dtOrderDetail = db.ExecuteSP("OrderDet_SelectAllByOrderId_EmailExcel", para.ToArray(), false);

            //string iUserType = dtOrderDetail.Rows[0]["iUserType"].ToString();

            System.Web.UI.WebControls.GridView gvData = new System.Web.UI.WebControls.GridView();
            gvData.AutoGenerateColumns = false;
            gvData.ShowFooter = true;

            BoundField sStoneStatus = new BoundField(); sStoneStatus.HeaderText = "Order Status"; sStoneStatus.DataField = "sStoneStatus";
            gvData.Columns.Add(sStoneStatus);

            BoundField sSupplLocation = new BoundField(); sSupplLocation.HeaderText = "Location"; sSupplLocation.DataField = "sSupplLocation";
            gvData.Columns.Add(sSupplLocation);

            BoundField sstatus = new BoundField(); sstatus.HeaderText = "Status"; sstatus.DataField = "sStatus";
            gvData.Columns.Add(sstatus);

            BoundField cRefNo = new BoundField(); cRefNo.DataField = "sRefNo"; cRefNo.HeaderText = "Ref. No.";
            gvData.Columns.Add(cRefNo);

            HyperLinkField cImage = new HyperLinkField();
            cImage.HeaderText = "Image";
            cImage.DataTextField = "img";
            cImage.DataNavigateUrlFields = new String[] { "image_url" };
            cImage.DataNavigateUrlFormatString = "{0}";
            gvData.Columns.Add(cImage);

            HyperLinkField cHd = new HyperLinkField();
            cHd.HeaderText = "Video";
            cHd.DataTextField = "movie";
            cHd.DataNavigateUrlFields = new String[] { "movie_url" };
            cHd.DataNavigateUrlFormatString = "{0}";
            gvData.Columns.Add(cHd);

            BoundField cBGM = new BoundField(); cBGM.DataField = "BGM"; cBGM.HeaderText = "BGM";
            gvData.Columns.Add(cBGM);

            BoundField cShape = new BoundField(); cShape.DataField = "sShape"; cShape.HeaderText = "Shape";
            gvData.Columns.Add(cShape);

            BoundField cColor = new BoundField(); cColor.DataField = "sColor"; cColor.HeaderText = "Color";
            cColor.FooterText = "Pcs";
            gvData.Columns.Add(cColor);

            BoundField cClarity = new BoundField(); cClarity.DataField = "sClarity"; cClarity.HeaderText = "Clarity";
            cClarity.FooterText = dtOrderDetail.Compute("COUNT(sClarity)", "").ToString();
            cClarity.FooterStyle.HorizontalAlign = HorizontalAlign.Left;
            gvData.Columns.Add(cClarity);

            BoundField cCertiNo = new BoundField(); cCertiNo.DataField = "sCertiNo"; cCertiNo.HeaderText = "Certi No";
            gvData.Columns.Add(cCertiNo);

            BoundField cPointer = new BoundField(); cPointer.DataField = "sPointer"; cPointer.HeaderText = "Pointer";
            gvData.Columns.Add(cPointer);

            BoundField cCarats = new BoundField(); cCarats.DataField = "dCts"; cCarats.HeaderText = "Cts";
            cCarats.FooterText = Convert.ToDecimal(dtOrderDetail.Compute("sum(dCts)", "")).ToString("#,##0.00");
            cCarats.ItemStyle.CssClass = "twoDigit";
            cCarats.FooterStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cCarats);


            BoundField cRepPrice = new BoundField(); cRepPrice.DataField = "dRepPrice"; cRepPrice.HeaderText = "Rap Price($)";
            cRepPrice.ItemStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cRepPrice);

            //decimal dlTotalRapAmount = Convert.ToDecimal(dtOrderDetail.Compute("sum(dRapAmount)", ""));
            decimal dlTotalRapAmount = Convert.ToDecimal((dtOrderDetail.Compute("sum(dRapAmount)", "").ToString() != "" && dtOrderDetail.Compute("sum(dRapAmount)", "").ToString() != null ? dtOrderDetail.Compute("sum(dRapAmount)", "") : 0));
            decimal dlTotaldNetPrice = Convert.ToDecimal((dtOrderDetail.Compute("sum(dNetPrice)", "").ToString() != "" && dtOrderDetail.Compute("sum(dNetPrice)", "").ToString() != null ? dtOrderDetail.Compute("sum(dNetPrice)", "") : 0));

            BoundField cRepAmount = new BoundField(); cRepAmount.DataField = "dRapAmount"; cRepAmount.HeaderText = "Rap Amount($)";
            cRepAmount.ItemStyle.CssClass = "twoDigit";
            cRepAmount.FooterStyle.CssClass = "twoDigit";

            cRepAmount.FooterText = dlTotalRapAmount.ToString("#,##0.00");
            gvData.Columns.Add(cRepAmount);

            BoundField cDisc = new BoundField(); cDisc.DataField = "dDisc"; cDisc.HeaderText = "Offer Disc.(%)"; //"Disc(%)";
            if (dlTotalRapAmount == 0)
                cDisc.FooterText = @"0.00";
            else
                //cDisc.FooterText = ((dlTotalRapAmount - loOrderDet.Sum(r => r.dNetPrice).Value) * -100 / dlTotalRapAmount).ToString("0.00");
                //cDisc.FooterText = ((dlTotalRapAmount - Convert.ToDecimal(dtOrderDetail.Compute("sum(dNetPrice)", ""))) * -100 / dlTotalRapAmount).ToString("0.00");
                cDisc.FooterText = ((dlTotalRapAmount - dlTotaldNetPrice) * -100 / dlTotalRapAmount).ToString("0.00");

            cDisc.ItemStyle.CssClass = "twoDigit-red";
            cDisc.FooterStyle.CssClass = "twoDigit-red";
            gvData.Columns.Add(cDisc);


            BoundField cNetPrice = new BoundField(); cNetPrice.DataField = "dNetPrice"; cNetPrice.HeaderText = "Offer Value($)"; //"Net Amt($)";
            //cNetPrice.FooterText = Convert.ToDecimal(dtOrderDetail.Compute("sum(dNetPrice)", "")).ToString("#,##0.00");
            cNetPrice.FooterText = Convert.ToDecimal((dtOrderDetail.Compute("sum(dNetPrice)", "").ToString() != "" && dtOrderDetail.Compute("sum(dNetPrice)", "").ToString() != null ? dtOrderDetail.Compute("sum(dNetPrice)", "") : 0)).ToString("#,##0.00");
            cNetPrice.ItemStyle.CssClass = "twoDigit";
            cNetPrice.FooterStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cNetPrice);

          


            HyperLinkField cLab = new HyperLinkField();
            cLab.HeaderText = "Lab";
            cLab.DataTextField = "sLab";
            cLab.DataNavigateUrlFields = new String[] { "sVerifyCertiUrl" };
            cLab.DataNavigateUrlFormatString = "{0}";
            gvData.Columns.Add(cLab);

            HyperLinkField ccerti_type = new HyperLinkField();
            ccerti_type.HeaderText = "Certi Type";
            ccerti_type.DataTextField = "certi_type";
            ccerti_type.DataNavigateUrlFields = new String[] { "CertiTypeLink" };
            ccerti_type.DataNavigateUrlFormatString = "{0}";
            gvData.Columns.Add(ccerti_type);

            BoundField cCut = new BoundField(); cCut.DataField = "sCut"; cCut.HeaderText = "Cut";
            cCut.ItemStyle.CssClass = @"'<%# Convert.ToString(Eval(""sCut""))==""3EX"" ? ""bold"":"""" %>'";
            gvData.Columns.Add(cCut);

            BoundField cPolish = new BoundField(); cPolish.DataField = "sPolish"; cPolish.HeaderText = "Polish";
            gvData.Columns.Add(cPolish);

            BoundField cSymm = new BoundField(); cSymm.DataField = "sSymm"; cSymm.HeaderText = "Symm";
            gvData.Columns.Add(cSymm);

            BoundField cFls = new BoundField(); cFls.DataField = "sFls"; cFls.HeaderText = "Fls";
            gvData.Columns.Add(cFls);

            BoundField cRATIO = new BoundField(); cRATIO.DataField = "RATIO"; cRATIO.HeaderText = "Ratio";
            cRATIO.ItemStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cRATIO);

            BoundField cLength = new BoundField(); cLength.DataField = "dLength"; cLength.HeaderText = "Length";
            cLength.ItemStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cLength);

            BoundField cWidth = new BoundField(); cWidth.DataField = "dWidth"; cWidth.HeaderText = "Width";
            cWidth.ItemStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cWidth);

            BoundField cDepth = new BoundField(); cDepth.DataField = "dDepth"; cDepth.HeaderText = "Depth";
            cDepth.ItemStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cDepth);

            BoundField cDepthPer = new BoundField(); cDepthPer.DataField = "dDepthPer"; cDepthPer.HeaderText = "Depth(%)";
            cDepthPer.ItemStyle.CssClass = "oneDigit";
            gvData.Columns.Add(cDepthPer);

            BoundField cTablePer = new BoundField(); cTablePer.DataField = "dTablePer"; cTablePer.HeaderText = "Table(%)";
            gvData.Columns.Add(cTablePer);

            BoundField cSymbol = new BoundField(); cSymbol.DataField = "sSymbol"; cSymbol.HeaderText = "Key To Symbol";
            gvData.Columns.Add(cSymbol);

            BoundField cInclusion = new BoundField(); cInclusion.DataField = "sInclusion"; cInclusion.HeaderText = "Table Incl.";
            gvData.Columns.Add(cInclusion);

            BoundField cCrownInclusion = new BoundField(); cCrownInclusion.DataField = "sCrownInclusion"; cCrownInclusion.HeaderText = "Crown Incl.";
            gvData.Columns.Add(cCrownInclusion);

            BoundField cTableNatts = new BoundField(); cTableNatts.DataField = "sTableNatts"; cTableNatts.HeaderText = "Table Natts";
            gvData.Columns.Add(cTableNatts);

            //--By Aniket [11-06-15]          

            BoundField cCrownNatts = new BoundField(); cCrownNatts.DataField = "sCrownNatts"; cCrownNatts.HeaderText = "Crown Natts";
            gvData.Columns.Add(cCrownNatts);


            //--Over [11-06-15]


            BoundField cCrAng = new BoundField(); cCrAng.DataField = "dCrAng"; cCrAng.HeaderText = "Cr Ang";
            cCrAng.ItemStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cCrAng);

            BoundField cCrHt = new BoundField(); cCrHt.DataField = "dCrHt"; cCrHt.HeaderText = "Cr Ht";
            cCrHt.ItemStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cCrHt);

            BoundField cPavAng = new BoundField(); cPavAng.DataField = "dPavAng"; cPavAng.HeaderText = "Pav Ang";
            cPavAng.ItemStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cPavAng);

            BoundField cPavHt = new BoundField(); cPavHt.DataField = "dPavHt"; cPavHt.HeaderText = "Pav Ht";
            cPavHt.ItemStyle.CssClass = "twoDigit";
            gvData.Columns.Add(cPavHt);

            //if (iUserType == "3")
            //{
            BoundField Table_Open = new BoundField(); Table_Open.DataField = "Table_Open"; Table_Open.HeaderText = "Table Open";
            gvData.Columns.Add(Table_Open);

            BoundField Crown_Open = new BoundField(); Crown_Open.DataField = "Crown_Open"; Crown_Open.HeaderText = "Crown Open";
            gvData.Columns.Add(Crown_Open);

            BoundField Pav_Open = new BoundField(); Pav_Open.DataField = "Pav_Open"; Pav_Open.HeaderText = "Pav Open";
            gvData.Columns.Add(Pav_Open);

            BoundField Girdle_Open = new BoundField(); Girdle_Open.DataField = "Girdle_Open"; Girdle_Open.HeaderText = "Girdle Open";
            gvData.Columns.Add(Girdle_Open);
            //}

            BoundField cGirdle = new BoundField(); cGirdle.DataField = "sGirdleType"; cGirdle.HeaderText = "Girdle Type";
            gvData.Columns.Add(cGirdle);

            //if (iUserType == "3")
            //{
            BoundField sInscription = new BoundField(); sInscription.DataField = "sInscription"; sInscription.HeaderText = "Laser Insc.";
            gvData.Columns.Add(sInscription);
            //}


            gvData.DataSource = dtOrderDetail;

            gvData.DataBind();

            foreach (GridViewRow gvr in gvData.Rows)
            {
                if (gvr.RowType == DataControlRowType.DataRow)
                {
                    var data = gvr.DataItem as DataRowView;
                    //if (data.DataView.Table.Columns["Cut"] != null)
                    //{
                    int cutIndex = GetColumnIndexByName(gvr, "sCut");
                    string cellContent = gvr.Cells[cutIndex].Text;
                    if (cellContent.ToLower() == "3ex")
                    {
                        int polishIndex = GetColumnIndexByName(gvr, "sPolish");
                        gvr.Cells[polishIndex].CssClass = "bold";

                        int symmIndex = GetColumnIndexByName(gvr, "sSymm");
                        gvr.Cells[symmIndex].CssClass = "bold";
                        // }
                    }
                }
            }
            gvData.FooterStyle.Font.Bold = true;
            gvData.HeaderStyle.Font.Bold = true;

            GridViewEpExcelExport ep_ge;
            ep_ge = new GridViewEpExcelExport(gvData, "Order", "Order");

            ep_ge.BeforeCreateColumnEvent += Ep_BeforeCreateColumnEventHandler;
            ep_ge.AfterCreateCellEvent += Ep_AfterCreateCellEventHandler;
            ep_ge.FillingWorksheetEvent += Ep_FillingWorksheetEventHandler;
            //change by Hitesh on [31-03-2016] as per [Doc No 201]
            ep_ge.AddHeaderEvent += Ep_AddHeaderEventHandler;
            //End by Hitesh on [31-03-2016] as per [Doc No 201]

            MemoryStream ms = new MemoryStream();
            string parentpath = HttpContext.Current.Server.MapPath("~/Temp/Excel/");
            if (System.Configuration.ConfigurationManager.AppSettings["ConnMode"] == "Oracle")
                parentpath = @"C:\inetpub\wwwroot\Temp\";
            ep_ge.CreateExcel(ms, parentpath);

            System.IO.MemoryStream memn = new System.IO.MemoryStream();

            byte[] byteDatan = ms.ToArray();
            memn.Write(byteDatan, 0, byteDatan.Length);
            memn.Flush();
            memn.Position = 0;
            //memn.Close();
            return memn;
        }
        private static int GetColumnIndexByName(GridViewRow row, string columnName)
        {
            int columnIndex = 0;
            foreach (DataControlFieldCell cell in row.Cells)
            {
                if (cell.ContainingField is BoundField)
                    if (((BoundField)cell.ContainingField).DataField.Equals(columnName))
                        break;
                columnIndex++; // keep adding 1 while we don't have the correct name
            }
            return columnIndex;
        }
        private static void Ep_BeforeCreateColumnEventHandler(object sender, ref EpExcelExport.ExcelHeader e)
        {

            //if (e.ColName == "iSr")
            if (e.Caption == "Sr.")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 4;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Count;
                e.NumFormat = "#,##0";
            }
            if (e.Caption == "Order Status")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            if (e.Caption == "Location")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 10;
            }
            //if (e.ColName == "sRefNo")
            if (e.Caption == "Ref. No.")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 12;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Count;
            }
            //if (e.ColName == "sLab")
            if (e.Caption == "Lab")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 4;
            }
            //if (e.ColName == "sShape")
            if (e.Caption == "Shape")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6.86;
            }
            //if (e.ColName == "sPointer")
            if (e.Caption == "Pointer")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 8.14;
            }
            //if (e.ColName == "sCertiNo")
            if (e.Caption == "Certi No")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 11;
            }
            //if (e.ColName == "sColor")
            if (e.Caption == "Color")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6;
            }
            //if (e.ColName == "sClarity")
            if (e.Caption == "Clarity")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6;
            }
            //if (e.ColName == "dCts")
            if (e.Caption == "Cts")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 5.71;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dRepPrice")
            if (e.Caption == "Rap Price($)")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 10.86;
                e.NumFormat = "#,##0";
            }
            //if (e.ColName == "dRepAmount")
            if (e.Caption == "Rap Amount($)")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15.71;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#,##0";
            }
            //if (e.ColName == "dDisc")
            if (e.Caption == "Disc(%)")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6.86;
                e.NumFormat = "#0.00";

                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Custom;
                e.SummFormula = "(1- (" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Net Amt($)", EpExcelExport.TotalsRowFunctionValues.Sum) +
                                    "/" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.Sum) + " ))*-100";

            }
            if (e.Caption == "Org Disc(%)")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6.86;
                e.NumFormat = "#0.00";

                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Custom;
                e.SummFormula = "if(" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Org Disc(%)", EpExcelExport.TotalsRowFunctionValues.Sum) + "=0,0.00,(1- (" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Org Value($)", EpExcelExport.TotalsRowFunctionValues.Sum) +
                                    "/" + ((EpExcelExportLib.GridViewEpExcelExport)sender).GetSummFormula("Rap Amount($)", EpExcelExport.TotalsRowFunctionValues.SumIf, "Org Disc(%)", "<100") + " ))*-100)";
            }

            //if (e.ColName == "dNetPrice")
            if (e.Caption == "Net Amt($)")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 11;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#,##0.00";
            }
            if (e.Caption == "Org Value($)")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 11;
                e.SummFunction = EpExcelExport.TotalsRowFunctionValues.Sum;
                e.NumFormat = "#,##0.00";
            }
            //if (e.ColName == "sCut")
            if (e.Caption == "Cut")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 5;
            }
            //if (e.ColName == "sPolish")
            if (e.Caption == "Polish")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 5.71;
            }
            //if (e.ColName == "sSymm")
            if (e.Caption == "Symm")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 5.71;
            }
            //if (e.ColName == "sFls")
            if (e.Caption == "Fls")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 5.71;
            }
            if (e.Caption == "Length")
            //if (e.ColName == "dLength")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6.70;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dWidth")
            if (e.Caption == "Width")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6.14;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dDepth")
            if (e.Caption == "Depth")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 6.14;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dDepthPer")
            if (e.Caption == "Depth(%)")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 8.43;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dTablePer")
            if (e.Caption == "Table(%)")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 8.43;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "sSymbol")
            if (e.Caption == "Key To Symbol")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 35;
            }
            //if (e.ColName == "sInclusion")
            if (e.Caption == "Table Incl.")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            //if (e.ColName == "sTableNatts")
            if (e.Caption == "Table Natts")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            //--By Aniket [11-06-15]
            //if (e.ColName == "sCrownInclusion")
            if (e.Caption == "Crown Incl.")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            //if (e.ColName == "sCrownNatts")
            if (e.Caption == "Crown Natts")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }

            //if (e.ColName == "sLuster")
            if (e.Caption == "Luster/Milky")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.Width = 15;
            }
            //-- Over [11-06-15]
            //if (e.ColName == "sShade")
            if (e.Caption == "Shade")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            //if (e.ColName == "dCrAng")
            if (e.Caption == "Cr Ang")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dCrHt")
            if (e.Caption == "Cr Ht")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dPavAng")
            if (e.Caption == "Pav Ang")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "dPavHt")
            if (e.Caption == "Pav Ht")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.Number;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.NumFormat = "#0.00";
            }
            //if (e.ColName == "sGirdle")
            if (e.Caption == "Girdle Type")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            //if (e.ColName == "sStatus")
            if (e.Caption == "Status")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            //change by Hitesh on [31-03-2016] as per [Doc No 201]
            if (e.Caption == "DNA")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            if (e.Caption == "HD Movie")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            if (e.Caption == "Image")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            if (e.Caption == "Laser Insc")
            {
                e.ColDataType = OfficeOpenXml.eDataTypes.String;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            //End by Hitesh on [31-03-2016] as per [Doc No 201]
        }

        private static void Ep_AfterCreateCellEventHandler(object sender, ref EpExcelExport.ExcelCellFormat e)
        {
            if (e.tableArea == EpExcelExport.TableArea.Header)
            {
                e.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.FromArgb(131, 202, 255).ToArgb());
                //e.HorizontalAllign = DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center;
                e.HorizontalAllign = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                e.isbold = true;
            }
            else if (e.tableArea == EpExcelExport.TableArea.Detail)
            {

                //if (e.ColumnName == "dDisc")
                if (e.ColumnName == "Disc(%)")
                {
                    e.StyleInd = DiscNormalStyleindex;
                }

                else if (e.ColumnName == "Org Disc(%)")
                {
                    e.StyleInd = DiscNormalStyleindex;
                }
                //else if (e.ColumnName == "sCut")
                else if (e.ColumnName == "Cut")
                {
                    if (e.Text == "3EX")
                        e.StyleInd = CutNormalStyleindex;
                }//priyanka on date [28-05-15]
                else if (e.ColumnName == "Order Status")
                {
                    if (e.Text == "NOT AVAILABLE" || e.Text == "CHECKING AVAIBILITY")
                        e.StyleInd = STatusBkgrndIndx;
                }//********
                //change by Hitesh on [31-03-2016] as per [Doc No 201]
                else if (e.ColumnName == "Net Amt($)")
                {
                    e.StyleInd = DiscNormalStyleindex;
                }
                else if (e.ColumnName == "Laser Insc")
                {
                    e.StyleInd = InscStyleindex;
                }
                //End by Hitesh on [31-03-2016] as per [Doc No 201]

            }
            else if (e.tableArea == EpExcelExport.TableArea.Footer)
            {
                e.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.FromArgb(131, 202, 255).ToArgb());
                e.isbold = true;
                //e.ul = DocumentFormat.OpenXml.Spreadsheet.UnderlineValues.None;

                if (e.ColumnName == "Disc(%)")
                {
                    //e.StyleInd = DiscNormalStyleindex;
                }
            }

        }

        private static void Ep_AddHeaderEventHandler(object sender, ref EpExcelExportLib.EpExcelExport.AddHeaderEventArgs e)
        {
            EpExcelExport ee = (EpExcelExport)sender;
            ee.AddNewRow("A1");
        }
        private static UInt32 DiscNormalStyleindex;
        private static UInt32 CutNormalStyleindex;
        //priyanka on date [28-05-15]
        private static UInt32 STatusBkgrndIndx;
        ///
        private static UInt32 InscStyleindex; 
        private static void Ep_FillingWorksheetEventHandler(object sender, ref EpExcelExport.FillingWorksheetEventArgs e)
        {
            EpExcelExport ee = (EpExcelExport)sender;
            EpExcelExport.ExcelFormat format = new EpExcelExport.ExcelFormat();

            format = new EpExcelExport.ExcelFormat();
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            format.isbold = true;
            DiscNormalStyleindex = ee.AddStyle(format);

            format = new EpExcelExport.ExcelFormat();
            format.isbold = true;
            CutNormalStyleindex = ee.AddStyle(format);
            //priyanka on date [28-05-15]
            format = new EpExcelExport.ExcelFormat();
            format.backgroundArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Yellow.ToArgb());
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Red.ToArgb());
            STatusBkgrndIndx = ee.AddStyle(format);
            /////////

            //change by Hitesh on [31-03-2016] as per [Doc No 201]
            format = new EpExcelExportLib.EpExcelExport.ExcelFormat();
            format.forColorArgb = EpExcelExport.GetHexValue(System.Drawing.Color.Blue.ToArgb());
            format.isbold = true;
            InscStyleindex = ee.AddStyle(format);
            //End by Hitesh on [31-03-2016] as per [Doc No 201]
        }
    }
}

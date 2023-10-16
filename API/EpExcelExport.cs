using Lib.Model;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace API
{
    public class EpExcelExport
    {
        public static void Buyer_Excel(DataTable dtDiamonds, DataTable Col_dt, string _strFolderPath, string _strFilePath)
        {
            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    int Row_Count = Col_dt.Rows.Count;
                    int inStartIndex = 3;
                    int inwrkrow = 3;
                    int inEndCounter = dtDiamonds.Rows.Count + inStartIndex;
                    int TotalRow = dtDiamonds.Rows.Count;
                    int i;
                    Int64 number_1;
                    bool success1;

                    Color colFromHex_Pointer = System.Drawing.ColorTranslator.FromHtml("#c6e0b4");
                    Color colFromHex_Dis = System.Drawing.ColorTranslator.FromHtml("#ccffff");
                    Color colFromHexTotal = System.Drawing.ColorTranslator.FromHtml("#d9e1f2");
                    Color tcpg_bg_clr = System.Drawing.ColorTranslator.FromHtml("#fff2cc");

                    #region Company Detail on Header

                    p.Workbook.Properties.Author = "SUNRISE DIAMOND";
                    p.Workbook.Properties.Title = "SUNRISE DIAMOND PVT. LTD.";
                    p.Workbook.Worksheets.Add("Buyer Stock");

                    ExcelWorksheet worksheet = p.Workbook.Worksheets[1];
                    worksheet.Name = DateTime.Now.ToString("dd-MM-yyyy");
                    worksheet.Cells.Style.Font.Size = 11;
                    worksheet.Cells.Style.Font.Name = "Calibri";

                    Color colFromHex_H1 = System.Drawing.ColorTranslator.FromHtml("#8497b0");
                    Color col_color_Red = System.Drawing.ColorTranslator.FromHtml("#ff0000");

                    Color H = System.Drawing.ColorTranslator.FromHtml("#93c5f7");
                    Color I = System.Drawing.ColorTranslator.FromHtml("#c4bd97");
                    Color T_U = System.Drawing.ColorTranslator.FromHtml("#c0c0c0");
                    Color V_W = System.Drawing.ColorTranslator.FromHtml("#ffff00");
                    Color X_Y = System.Drawing.ColorTranslator.FromHtml("#ff99ff");
                    Color Z_AA_AB = System.Drawing.ColorTranslator.FromHtml("#ccecff");
                    Color AE_AF = System.Drawing.ColorTranslator.FromHtml("#fcd5b4");
                    Color AG_AH = System.Drawing.ColorTranslator.FromHtml("#66ffcc");
                    Color AI_AJ = System.Drawing.ColorTranslator.FromHtml("#e4dfec");
                    Color AK = System.Drawing.ColorTranslator.FromHtml("#daeef3");
                    Color AL = System.Drawing.ColorTranslator.FromHtml("#99cc00");
                    Color AM = System.Drawing.ColorTranslator.FromHtml("#f2dc13");
                    Color AN = System.Drawing.ColorTranslator.FromHtml("#00ffff");
                    Color BL = System.Drawing.ColorTranslator.FromHtml("#daeef3");

                    worksheet.Row(1).Height = 40;
                    worksheet.Row(2).Height = 40;
                    worksheet.Row(2).Style.WrapText = true;

                    #endregion

                    #region Header Name Declaration

                    //worksheet.Cells[2, 1].Value = "Sr. No";
                    //worksheet.Cells[2, 1].AutoFitColumns(7);
                    //Row_Count += 1;

                    //int k = 1;
                    int k = 0;
                    for (int j = 0; j < Col_dt.Rows.Count; j++)
                    {
                        string Column_Name = Convert.ToString(Col_dt.Rows[j]["Column_Name"]);
                        double AutoFitColumns = Convert.ToDouble(Col_dt.Rows[j]["ExcelWidth"]);

                        if (Column_Name == "Image-Video-Certi")
                        {
                            Row_Count += 2;

                            k += 1;
                            worksheet.Cells[2, k].Value = "Image";
                            worksheet.Cells[2, k].AutoFitColumns(7);

                            k += 1;
                            worksheet.Cells[2, k].Value = "Video";
                            worksheet.Cells[2, k].AutoFitColumns(7);

                            k += 1;
                            worksheet.Cells[2, k].Value = "Certi";
                            worksheet.Cells[2, k].AutoFitColumns(6);
                        }
                        else
                        {
                            k += 1;
                            worksheet.Cells[2, k].Value = Column_Name;
                            worksheet.Cells[2, k].AutoFitColumns(AutoFitColumns);

                            if (Column_Name == "Pointer" || Column_Name == "Sub Pointer")
                            {
                                worksheet.Cells[2, k].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[2, k].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);
                            }
                        }
                    }

                    worksheet.Cells[1, 1].Value = "Total";
                    worksheet.Cells[1, 1, 1, Row_Count].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, 1, Row_Count].Style.Font.Size = 11;
                    worksheet.Cells[1, 1, 1, Row_Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, 1, Row_Count].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[1, 1, 1, Row_Count].Style.Font.Size = 11;

                    worksheet.Cells[2, 1, 2, Row_Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[2, 1, 2, Row_Count].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    worksheet.Cells[2, 1, 2, Row_Count].Style.Font.Size = 10;
                    worksheet.Cells[2, 1, 2, Row_Count].Style.Font.Bold = true;

                    worksheet.Cells[2, 1, 2, Row_Count].AutoFilter = true;

                    var cellBackgroundColor1 = worksheet.Cells[2, 1, 2, Row_Count].Style.Fill;
                    cellBackgroundColor1.PatternType = ExcelFillStyle.Solid;
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                    cellBackgroundColor1.BackgroundColor.SetColor(colFromHex);

                    ExcelStyle cellStyleHeader1 = worksheet.Cells[2, 1, 2, Row_Count].Style;
                    cellStyleHeader1.Border.Left.Style = cellStyleHeader1.Border.Right.Style
                            = cellStyleHeader1.Border.Top.Style = cellStyleHeader1.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    #endregion

                    #region Set AutoFit and Decimal Number Format

                    worksheet.View.FreezePanes(3, 1);
                    worksheet.Cells[inStartIndex, 1, inEndCounter, Row_Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    #endregion
                    var asTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo;


                    for (i = inStartIndex; i < inEndCounter; i++)
                    {
                        #region Assigns Value to Cell

                        //worksheet.Cells[inwrkrow, 1].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["iSr"]);

                        int kk = 0;
                        for (int j = 0; j < Col_dt.Rows.Count; j++)
                        {
                            string Column_Name = Convert.ToString(Col_dt.Rows[j]["Column_Name"]);
                            double AutoFitColumns = Convert.ToDouble(Col_dt.Rows[j]["ExcelWidth"]);

                            if (Column_Name == "Image-Video-Certi")
                            {
                                kk += 1;

                                string Image_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image_URL"]);
                                if (!string.IsNullOrEmpty(Image_URL))
                                {
                                    worksheet.Cells[inwrkrow, kk].Formula = "=HYPERLINK(\"" + Image_URL + "\",\" Image \")";
                                    worksheet.Cells[inwrkrow, kk].Style.Font.UnderLine = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(Color.Blue);
                                }

                                kk += 1;

                                string Video_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Video_URL"]);
                                if (!string.IsNullOrEmpty(Video_URL))
                                {
                                    worksheet.Cells[inwrkrow, kk].Formula = "=HYPERLINK(\"" + Video_URL + "\",\" Video \")";
                                    worksheet.Cells[inwrkrow, kk].Style.Font.UnderLine = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(Color.Blue);
                                }

                                kk += 1;

                                string Certificate_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_URL"]);
                                if (!string.IsNullOrEmpty(Certificate_URL))
                                {
                                    worksheet.Cells[inwrkrow, kk].Formula = "=HYPERLINK(\"" + Certificate_URL + "\",\" Certi \")";
                                    worksheet.Cells[inwrkrow, kk].Style.Font.UnderLine = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(Color.Blue);
                                }
                            }
                            else
                            {
                                kk += 1;

                                if (Column_Name == "Lab")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]);
                                    string URL = "";
                                    if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "GIA")
                                    {
                                        URL = "http://www.gia.edu/cs/Satellite?pagename=GST%2FDispatcher&childpagename=GIA%2FPage%2FReportCheck&c=Page&cid=1355954554547&reportno="+ Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    }
                                    else if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "HRD")
                                    {
                                        URL = "https://my.hrdantwerp.com/?id=34&record_number=" + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    }
                                    else if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "IGI")
                                    {
                                        URL = "https://www.igi.org/reports/verify-your-report?r=" + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    }
                                    if (URL != "")
                                    {
                                        worksheet.Cells[inwrkrow, kk].Formula = "=HYPERLINK(\"" + Convert.ToString(URL) + "\",\" " + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) + " \")";
                                        worksheet.Cells[inwrkrow, kk].Style.Font.UnderLine = true;
                                        worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(Color.Blue);
                                    }
                                }
                                else if (Column_Name == "Supplier Name")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["SupplierName"]);
                                }
                                else if (Column_Name == "Rank")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rank"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Rank"].GetType().Name != "DBNull" ?
                                      Convert.ToInt32(dtDiamonds.Rows[i - inStartIndex]["Rank"]) : ((Int32?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(AM);
                                }
                                else if (Column_Name == "Supplier Status")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Supplier_Status"]);
                                }
                                else if (Column_Name == "Buyer Name")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Buyer_Name"]);

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(H);
                                }
                                else if (Column_Name == "Status")
                                {
                                    //worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Status"]);
                                    worksheet.Cells[inwrkrow, kk].Value = "";

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(I);
                                }
                                else if (Column_Name == "Supplier Ref No")
                                {
                                    string Supplier_Stone_Id = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Supplier_Stone_Id"]);
                                    success1 = Int64.TryParse(Supplier_Stone_Id, out number_1);
                                    if (success1)
                                    {
                                        worksheet.Cells[inwrkrow, kk].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Supplier_Stone_Id"]);
                                    }
                                    else
                                    {
                                        worksheet.Cells[inwrkrow, kk].Value = Supplier_Stone_Id;
                                    }
                                }
                                else if (Column_Name == "Cert No")
                                {
                                    string Certificate_No = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    success1 = Int64.TryParse(Certificate_No, out number_1);
                                    if (success1)
                                    {
                                        worksheet.Cells[inwrkrow, kk].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    }
                                    else
                                    {
                                        worksheet.Cells[inwrkrow, kk].Value = Certificate_No;
                                    }
                                }
                                else if (Column_Name == "Shape")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shape"]);
                                }
                                else if (Column_Name == "Pointer")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pointer"]);
                                }
                                else if (Column_Name == "Sub Pointer")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Sub_Pointer"]);
                                }
                                else if (Column_Name == "Color")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Color"]);
                                }
                                else if (Column_Name == "Clarity")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Clarity"]);
                                }
                                else if (Column_Name == "Cts")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Cts"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Cts"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Cts"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                }
                                else if (Column_Name == "Rap Rate($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                }
                                else if (Column_Name == "Rap Amount($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                }
                                else if (Column_Name == "Supplier Base Offer(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Disc"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Disc"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Disc"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(T_U);
                                }
                                else if (Column_Name == "Supplier Base Offer Value($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Value"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Value"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Value"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(T_U);
                                }
                                else if (Column_Name == "Supplier Final Disc(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(V_W);
                                }
                                else if (Column_Name == "Supplier Final Value($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(V_W);
                                }
                                else if (Column_Name == "Supplier Final Disc. With Max Slab(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["MAX_DISC"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["MAX_DISC"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["MAX_DISC"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(X_Y);
                                }
                                else if (Column_Name == "Supplier Final Value With Max Slab($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["MAX_VALUE"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["MAX_VALUE"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["MAX_VALUE"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(X_Y);
                                }
                                else if (Column_Name == "Bid Disc(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Bid_Disc"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Bid_Disc"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Bid_Disc"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(Z_AA_AB);
                                }
                                else if (Column_Name == "Bid Amt")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Bid_Amt"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Bid_Amt"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Bid_Amt"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(Z_AA_AB);
                                }
                                else if (Column_Name == "Bid/Ct")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Bid_Ct"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Bid_Ct"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Bid_Ct"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(Z_AA_AB);
                                }
                                else if (Column_Name == "Avg. Stock Disc(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Disc"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Disc"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Disc"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(AE_AF);
                                }
                                else if (Column_Name == "Avg. Stock Pcs")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Pcs"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Pcs"].GetType().Name != "DBNull" ?
                                      Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Pcs"]) : ((Int64?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(AE_AF);
                                }
                                else if (Column_Name == "Avg. Pur. Disc(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Disc"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Disc"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Disc"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(AG_AH);
                                }
                                else if (Column_Name == "Avg. Pur. Pcs")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Pcs"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Pcs"].GetType().Name != "DBNull" ?
                                      Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Pcs"]) : ((Int64?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(AG_AH);
                                }
                                else if (Column_Name == "Avg. Sales Disc(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Sales_Disc"] != null) ?
                                        (dtDiamonds.Rows[i - inStartIndex]["Avg_Sales_Disc"].GetType().Name != "DBNull" ?
                                        Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Avg_Sales_Disc"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(AI_AJ);
                                }
                                else if (Column_Name == "Sales Pcs")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Sales_Pcs"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Sales_Pcs"].GetType().Name != "DBNull" ?
                                     Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Sales_Pcs"]) : ((Int64?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(AI_AJ);
                                }
                                else if (Column_Name == "KTS Grade")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["KTS_Grade"]);
                                    if (worksheet.Cells[inwrkrow, kk].Value.ToString().ToUpper() == "K3")
                                    {
                                        worksheet.Cells[inwrkrow, 1, inwrkrow, Row_Count].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                    }
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(AK);
                                }
                                else if (Column_Name == "Comm. Grade")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Comm_Grade"]);

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(AL);
                                }
                                else if (Column_Name == "Zone")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Zone"]);

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(AM);
                                }
                                else if (Column_Name == "Para. Grade")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Para_Grade"]);

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(AN);
                                }
                                else if (Column_Name == "Cut")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]);
                                    if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]).ToUpper() == "3EX")
                                    {
                                        worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    }
                                }
                                else if (Column_Name == "Polish")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Polish"]);
                                    if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]).ToUpper() == "3EX")
                                    {
                                        worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    }
                                }
                                else if (Column_Name == "Symm")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Symm"]);
                                    if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]).ToUpper() == "3EX")
                                    {
                                        worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    }
                                }
                                else if (Column_Name == "Fls")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fls"]);
                                }
                                else if (Column_Name == "Key To Symbol")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Key_To_Symboll"]);
                                }
                                else if (Column_Name == "Ratio")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["RATIO"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["RATIO"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["RATIO"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Length")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Length"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Length"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Length"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Width")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Width"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Width"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Width"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Depth")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Depth"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Depth(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Depth_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth_Per"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Table(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Table_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Table_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Table_Per"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Crown Angle")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Crown Height")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Height"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Crown_Height"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Height"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Pavilion Angle")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Pavilion Height")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Height"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Height"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Height"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Girdle(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Luster")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Luster"]);
                                }
                                else if (Column_Name == "Cert Type")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Type_2A"]);
                                }
                                else if (Column_Name == "Table White")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Inclusion"]);
                                }
                                else if (Column_Name == "Crown White")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Inclusion"]);
                                }
                                else if (Column_Name == "Table Black")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Natts"]);
                                }
                                else if (Column_Name == "Crown Black")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"]);
                                }
                                else if (Column_Name == "Culet")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Culet"]);
                                }
                                else if (Column_Name == "Comment")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab_Comments"]);

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(BL);
                                }
                                else if (Column_Name == "Supplier Comment")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Supplier_Comments"]);
                                }
                                else if (Column_Name == "Table Open")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Open"]);
                                }
                                else if (Column_Name == "Crown Open")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Open"]);
                                }
                                else if (Column_Name == "Pav Open")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pav_Open"]);
                                }
                                else if (Column_Name == "Girdle Open")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle_Open"]);
                                }
                                else if (Column_Name == "Shade")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shade"]);
                                }
                                else if (Column_Name == "Milky")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Milky"]);
                                }
                            }
                        }
                        inwrkrow++;

                        #endregion
                    }

                    worksheet.Cells[inStartIndex, 1, (inwrkrow - 1), Row_Count].Style.Font.Size = 9;

                    int kkk = 0;
                    for (int j = 0; j < Col_dt.Rows.Count; j++)
                    {
                        string Column_Name = Convert.ToString(Col_dt.Rows[j]["Column_Name"]);

                        if (Column_Name == "Image-Video-Certi")
                        {
                            kkk += 3;
                        }
                        else
                        {
                            kkk += 1;

                            if (Column_Name == "Supplier Ref No")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(103," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##";

                                ExcelStyle cellStyleHeader_Total = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_Total.Border.Left.Style = cellStyleHeader_Total.Border.Right.Style
                                        = cellStyleHeader_Total.Border.Top.Style = cellStyleHeader_Total.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                            else if (Column_Name == "Cts")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0.00";

                                ExcelStyle cellStyleHeader_Totalcarat = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_Totalcarat.Border.Left.Style = cellStyleHeader_Totalcarat.Border.Right.Style
                                        = cellStyleHeader_Totalcarat.Border.Top.Style = cellStyleHeader_Totalcarat.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                            else if (Column_Name == "Rap Amount($)")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0";

                                ExcelStyle cellStyleHeader_RapAmt = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_RapAmt.Border.Left.Style = cellStyleHeader_RapAmt.Border.Right.Style
                                        = cellStyleHeader_RapAmt.Border.Top.Style = cellStyleHeader_RapAmt.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                            else if (Column_Name == "Supplier Base Offer(%)")
                            {
                                int Image_Video_Certi = 0, Rap_Amount = 0, Supplier_Base_Offer_Value_Doller = 0;
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video-Certi'");
                                if (dra.Length > 0)
                                {
                                    Image_Video_Certi = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Rap Amount($)'");
                                if (dra.Length > 0)
                                {
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 2 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Supplier Base Offer Value($)'");
                                if (dra.Length > 0)
                                {
                                    Supplier_Base_Offer_Value_Doller = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Supplier_Base_Offer_Value_Doller = ((Image_Video_Certi != 0 && Supplier_Base_Offer_Value_Doller > Image_Video_Certi) ? Supplier_Base_Offer_Value_Doller + 2 : Supplier_Base_Offer_Value_Doller); ;
                                }
                                
                                if (Rap_Amount != 0 && Supplier_Base_Offer_Value_Doller != 0)
                                {
                                    worksheet.Cells[1, kkk].Formula = "IF(SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ":" + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109," + GetExcelColumnLetter(Supplier_Base_Offer_Value_Doller) + "" + inStartIndex + ":" + GetExcelColumnLetter(Supplier_Base_Offer_Value_Doller) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ":" + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")))*(-100),2))";
                                    worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0.00";

                                    ExcelStyle cellStyleHeader_TotalDis = worksheet.Cells[1, kkk].Style;
                                    cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                                            = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                                            = ExcelBorderStyle.Medium;
                                }
                            }
                            else if (Column_Name == "Supplier Base Offer Value($)")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0";

                                ExcelStyle cellStyleHeader_TotalAmt = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_TotalAmt.Border.Left.Style = cellStyleHeader_TotalAmt.Border.Right.Style
                                        = cellStyleHeader_TotalAmt.Border.Top.Style = cellStyleHeader_TotalAmt.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                            else if (Column_Name == "Supplier Final Disc(%)")
                            {
                                int Image_Video_Certi = 0, Rap_Amount = 0, Supplier_Final_Value_Doller = 0;
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video-Certi'");
                                if (dra.Length > 0)
                                {
                                    Image_Video_Certi = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Rap Amount($)'");
                                if (dra.Length > 0)
                                {
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 2 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Supplier Final Value($)'");
                                if (dra.Length > 0)
                                {
                                    Supplier_Final_Value_Doller = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Supplier_Final_Value_Doller = ((Image_Video_Certi != 0 && Supplier_Final_Value_Doller > Image_Video_Certi) ? Supplier_Final_Value_Doller + 2 : Supplier_Final_Value_Doller); ;
                                }

                                if (Rap_Amount != 0 && Supplier_Final_Value_Doller != 0)
                                {
                                    worksheet.Cells[1, kkk].Formula = "IF(SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ": " + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109," + GetExcelColumnLetter(Supplier_Final_Value_Doller) + "" + inStartIndex + ":" + GetExcelColumnLetter(Supplier_Final_Value_Doller) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ":" + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")))*(-100),2))";
                                    worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0.00";

                                    ExcelStyle cellStyleHeader_TotalDis_1 = worksheet.Cells[1, kkk].Style;
                                    cellStyleHeader_TotalDis_1.Border.Left.Style = cellStyleHeader_TotalDis_1.Border.Right.Style
                                            = cellStyleHeader_TotalDis_1.Border.Top.Style = cellStyleHeader_TotalDis_1.Border.Bottom.Style
                                            = ExcelBorderStyle.Medium;
                                }
                            }
                            else if (Column_Name == "Supplier Final Value($)")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0";

                                ExcelStyle cellStyleHeader_TotalAmt_1 = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_TotalAmt_1.Border.Left.Style = cellStyleHeader_TotalAmt_1.Border.Right.Style
                                        = cellStyleHeader_TotalAmt_1.Border.Top.Style = cellStyleHeader_TotalAmt_1.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                            else if (Column_Name == "Supplier Final Disc. With Max Slab(%)")
                            {
                                int Image_Video_Certi = 0, Rap_Amount = 0, Supplier_Final_Value_With_Max_Slab_Doller = 0;
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video-Certi'");
                                if (dra.Length > 0)
                                {
                                    Image_Video_Certi = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Rap Amount($)'");
                                if (dra.Length > 0)
                                {
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 2 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Supplier Final Value With Max Slab($)'");
                                if (dra.Length > 0)
                                {
                                    Supplier_Final_Value_With_Max_Slab_Doller = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Supplier_Final_Value_With_Max_Slab_Doller = ((Image_Video_Certi != 0 && Supplier_Final_Value_With_Max_Slab_Doller > Image_Video_Certi) ? Supplier_Final_Value_With_Max_Slab_Doller + 2 : Supplier_Final_Value_With_Max_Slab_Doller); ;
                                }

                                if (Rap_Amount != 0 && Supplier_Final_Value_With_Max_Slab_Doller != 0)
                                {
                                    worksheet.Cells[1, kkk].Formula = "IF(SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ":" + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109," + GetExcelColumnLetter(Supplier_Final_Value_With_Max_Slab_Doller) + "" + inStartIndex + ":" + GetExcelColumnLetter(Supplier_Final_Value_With_Max_Slab_Doller) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ":" + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")))*(-100),2))";
                                    worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0.00";

                                    ExcelStyle cellStyleHeader_TotalDis_2 = worksheet.Cells[1, kkk].Style;
                                    cellStyleHeader_TotalDis_2.Border.Left.Style = cellStyleHeader_TotalDis_2.Border.Right.Style
                                            = cellStyleHeader_TotalDis_2.Border.Top.Style = cellStyleHeader_TotalDis_2.Border.Bottom.Style
                                            = ExcelBorderStyle.Medium;
                                }
                            }
                            else if (Column_Name == "Supplier Final Value With Max Slab($)")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0";

                                ExcelStyle cellStyleHeader_TotalAmt_2 = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_TotalAmt_2.Border.Left.Style = cellStyleHeader_TotalAmt_2.Border.Right.Style
                                        = cellStyleHeader_TotalAmt_2.Border.Top.Style = cellStyleHeader_TotalAmt_2.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;

                            }
                            else if (Column_Name == "Bid Disc(%)")
                            {
                                int Image_Video_Certi = 0, Rap_Amount = 0, Bid_Amt = 0;
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video-Certi'");
                                if (dra.Length > 0)
                                {
                                    Image_Video_Certi = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Rap Amount($)'");
                                if (dra.Length > 0)
                                {
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 2 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Bid Amt'");
                                if (dra.Length > 0)
                                {
                                    Bid_Amt = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Bid_Amt = ((Image_Video_Certi != 0 && Bid_Amt > Image_Video_Certi) ? Bid_Amt + 2 : Bid_Amt); ;
                                }

                                if (Rap_Amount != 0 && Bid_Amt != 0)
                                {
                                    worksheet.Cells[1, kkk].Formula = "IF(SUBTOTAL(109," + GetExcelColumnLetter(Bid_Amt) + "" + inStartIndex + ":" + GetExcelColumnLetter(Bid_Amt) + "" + (inwrkrow - 1) + ")=0,0, IF(SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ":" + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109," + GetExcelColumnLetter(Bid_Amt) + "" + inStartIndex + ":" + GetExcelColumnLetter(Bid_Amt) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ":" + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")))*(-100),2)))";
                                    worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0.00";

                                    ExcelStyle cellStyleHeader_TotalDis = worksheet.Cells[1, kkk].Style;
                                    cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                                            = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                                            = ExcelBorderStyle.Medium;
                                }
                            }
                            else if (Column_Name == "Bid Amt")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0";

                                ExcelStyle cellStyleHeader_TotalAmt = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_TotalAmt.Border.Left.Style = cellStyleHeader_TotalAmt.Border.Right.Style
                                        = cellStyleHeader_TotalAmt.Border.Top.Style = cellStyleHeader_TotalAmt.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                            else if (Column_Name == "Bid/Ct")
                            {
                                int Image_Video_Certi = 0, Cts = 0, Bid_Amt = 0;
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video-Certi'");
                                if (dra.Length > 0)
                                {
                                    Image_Video_Certi = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Cts'");
                                if (dra.Length > 0)
                                {
                                    Cts = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Cts = ((Image_Video_Certi != 0 && Cts > Image_Video_Certi) ? Cts + 2 : Cts);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Bid Amt'");
                                if (dra.Length > 0)
                                {
                                    Bid_Amt = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Bid_Amt = ((Image_Video_Certi != 0 && Bid_Amt > Image_Video_Certi) ? Bid_Amt + 2 : Bid_Amt); ;
                                }

                                if (Cts != 0 && Bid_Amt != 0)
                                {
                                    worksheet.Cells[1, kkk].Formula = "=+SUBTOTAL(109," + GetExcelColumnLetter(Bid_Amt) + "" + inStartIndex + ":" + GetExcelColumnLetter(Bid_Amt) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109," + GetExcelColumnLetter(Cts) + "" + inStartIndex + ":" + GetExcelColumnLetter(Cts) + "" + (inwrkrow - 1) + ")";
                                    worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0.00";

                                    ExcelStyle cellStyleHeader_TotalDis = worksheet.Cells[1, kkk].Style;
                                    cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                                            = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                                            = ExcelBorderStyle.Medium;
                                }
                            }
                        }
                    }

                    int rowEnd = worksheet.Dimension.End.Row;
                    removingGreenTagWarning(worksheet, worksheet.Cells[1, 1, rowEnd, 100].Address);

                    Byte[] bin = p.GetAsByteArray();

                    if (!Directory.Exists(_strFolderPath))
                    {
                        Directory.CreateDirectory(_strFolderPath);
                    }

                    System.IO.File.WriteAllBytes(_strFilePath, bin);

                }
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }
        public static void Supplier_Excel(DataTable dtDiamonds, DataTable Col_dt, string _strFolderPath, string _strFilePath)
        {
            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    int Row_Count = Col_dt.Rows.Count;
                    int inStartIndex = 3;
                    int inwrkrow = 3;
                    int inEndCounter = dtDiamonds.Rows.Count + inStartIndex;
                    int TotalRow = dtDiamonds.Rows.Count;
                    int i;
                    Int64 number_1;
                    bool success1;

                    Color colFromHex_Pointer = System.Drawing.ColorTranslator.FromHtml("#c6e0b4");
                    Color colFromHex_Dis = System.Drawing.ColorTranslator.FromHtml("#ccffff");
                    Color colFromHexTotal = System.Drawing.ColorTranslator.FromHtml("#d9e1f2");
                    Color tcpg_bg_clr = System.Drawing.ColorTranslator.FromHtml("#fff2cc");
                    Color cellBg = System.Drawing.ColorTranslator.FromHtml("#ccffff");
                    Color cellBg1 = System.Drawing.ColorTranslator.FromHtml("#ff99cc");

                    #region Company Detail on Header

                    p.Workbook.Properties.Author = "SUNRISE DIAMOND";
                    p.Workbook.Properties.Title = "SUNRISE DIAMOND PVT. LTD.";
                    p.Workbook.Worksheets.Add("Buyer Stock");

                    ExcelWorksheet worksheet = p.Workbook.Worksheets[1];
                    worksheet.Name = DateTime.Now.ToString("dd-MM-yyyy");
                    worksheet.Cells.Style.Font.Size = 11;
                    worksheet.Cells.Style.Font.Name = "Calibri";

                    Color colFromHex_H1 = System.Drawing.ColorTranslator.FromHtml("#8497b0");
                    Color col_color_Red = System.Drawing.ColorTranslator.FromHtml("#ff0000");

                    worksheet.Row(1).Height = 40;
                    worksheet.Row(2).Height = 40;
                    worksheet.Row(2).Style.WrapText = true;

                    #endregion

                    #region Header Name Declaration

                    //worksheet.Cells[2, 1].Value = "Sr. No";
                    //worksheet.Cells[2, 1].AutoFitColumns(7);
                    //Row_Count += 1;

                    //int k = 1;
                    int k = 0;
                    for (int j = 0; j < Col_dt.Rows.Count; j++)
                    {
                        string Column_Name = Convert.ToString(Col_dt.Rows[j]["Column_Name"]);
                        double AutoFitColumns = Convert.ToDouble(Col_dt.Rows[j]["ExcelWidth"]);

                        if (Column_Name == "Image-Video")
                        {
                            Row_Count += 1;

                            k += 1;
                            worksheet.Cells[2, k].Value = "Image";
                            worksheet.Cells[2, k].AutoFitColumns(7);

                            k += 1;
                            worksheet.Cells[2, k].Value = "Video";
                            worksheet.Cells[2, k].AutoFitColumns(7);
                        }
                        else
                        {
                            k += 1;
                            worksheet.Cells[2, k].Value = Column_Name;
                            worksheet.Cells[2, k].AutoFitColumns(AutoFitColumns);

                            if (Column_Name == "Pointer")
                            {
                                worksheet.Cells[2, k].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[2, k].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);
                            }
                        }
                    }

                    worksheet.Cells[1, 1].Value = "Total";
                    worksheet.Cells[1, 1, 1, Row_Count].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, 1, Row_Count].Style.Font.Size = 11;
                    worksheet.Cells[1, 1, 1, Row_Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, 1, Row_Count].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[1, 1, 1, Row_Count].Style.Font.Size = 11;

                    worksheet.Cells[2, 1, 2, Row_Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[2, 1, 2, Row_Count].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    worksheet.Cells[2, 1, 2, Row_Count].Style.Font.Size = 10;
                    worksheet.Cells[2, 1, 2, Row_Count].Style.Font.Bold = true;

                    worksheet.Cells[2, 1, 2, Row_Count].AutoFilter = true;

                    var cellBackgroundColor1 = worksheet.Cells[2, 1, 2, Row_Count].Style.Fill;
                    cellBackgroundColor1.PatternType = ExcelFillStyle.Solid;
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                    cellBackgroundColor1.BackgroundColor.SetColor(colFromHex);

                    ExcelStyle cellStyleHeader1 = worksheet.Cells[2, 1, 2, Row_Count].Style;
                    cellStyleHeader1.Border.Left.Style = cellStyleHeader1.Border.Right.Style
                            = cellStyleHeader1.Border.Top.Style = cellStyleHeader1.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    #endregion

                    #region Set AutoFit and Decimal Number Format

                    worksheet.View.FreezePanes(3, 1);
                    worksheet.Cells[inStartIndex, 1, inEndCounter, Row_Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    #endregion
                    var asTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo;


                    for (i = inStartIndex; i < inEndCounter; i++)
                    {
                        #region Assigns Value to Cell

                        worksheet.Cells[inwrkrow, 1].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["iSr"]);

                        int kk = 0;
                        for (int j = 0; j < Col_dt.Rows.Count; j++)
                        {
                            string Column_Name = Convert.ToString(Col_dt.Rows[j]["Column_Name"]);
                            double AutoFitColumns = Convert.ToDouble(Col_dt.Rows[j]["ExcelWidth"]);

                            if (Column_Name == "Image-Video")
                            {
                                kk += 1;

                                string Image_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image_URL"]);
                                if (!string.IsNullOrEmpty(Image_URL))
                                {
                                    worksheet.Cells[inwrkrow, kk].Formula = "=HYPERLINK(\"" + Image_URL + "\",\" Image \")";
                                    worksheet.Cells[inwrkrow, kk].Style.Font.UnderLine = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(Color.Blue);
                                }

                                kk += 1;

                                string Video_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Video_URL"]);
                                if (!string.IsNullOrEmpty(Video_URL))
                                {
                                    worksheet.Cells[inwrkrow, kk].Formula = "=HYPERLINK(\"" + Video_URL + "\",\" Video \")";
                                    worksheet.Cells[inwrkrow, kk].Style.Font.UnderLine = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(Color.Blue);
                                }
                            }
                            else
                            {
                                kk += 1;
                                if (Column_Name == "Ref No")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Ref_No"]);
                                }
                                else if (Column_Name == "Lab")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]);
                                    string URL = "";
                                    if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "GIA")
                                    {
                                        URL = "http://www.gia.edu/cs/Satellite?pagename=GST%2FDispatcher&childpagename=GIA%2FPage%2FReportCheck&c=Page&cid=1355954554547&reportno=" + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    }
                                    else if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "HRD")
                                    {
                                        URL = "https://my.hrdantwerp.com/?id=34&record_number=" + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    }
                                    else if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "IGI")
                                    {
                                        URL = "https://www.igi.org/reports/verify-your-report?r=" + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    }
                                    if (URL != "")
                                    {
                                        worksheet.Cells[inwrkrow, kk].Formula = "=HYPERLINK(\"" + Convert.ToString(URL) + "\",\" " + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) + " \")";
                                        worksheet.Cells[inwrkrow, kk].Style.Font.UnderLine = true;
                                        worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(Color.Blue);
                                    }
                                }
                                else if (Column_Name == "Supplier Ref No")
                                {
                                    string Supplier_Stone_Id = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Supplier_Stone_Id"]);
                                    success1 = Int64.TryParse(Supplier_Stone_Id, out number_1);
                                    if (success1)
                                    {
                                        worksheet.Cells[inwrkrow, kk].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Supplier_Stone_Id"]);
                                    }
                                    else
                                    {
                                        worksheet.Cells[inwrkrow, kk].Value = Supplier_Stone_Id;
                                    }
                                }
                                else if (Column_Name == "Cert No")
                                {
                                    string Certificate_No = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    success1 = Int64.TryParse(Certificate_No, out number_1);
                                    if (success1)
                                    {
                                        worksheet.Cells[inwrkrow, kk].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    }
                                    else
                                    {
                                        worksheet.Cells[inwrkrow, kk].Value = Certificate_No;
                                    }
                                }
                                else if (Column_Name == "Supplier Name")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["SupplierName"]);
                                }
                                else if (Column_Name == "Shape")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shape"]);
                                }
                                else if (Column_Name == "Pointer")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pointer"]);
                                }
                                else if (Column_Name == "BGM")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["BGM"]);
                                }
                                else if (Column_Name == "Color")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Color"]);
                                }
                                else if (Column_Name == "Clarity")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Clarity"]);
                                }
                                else if (Column_Name == "Cts")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Cts"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Cts"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Cts"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                }
                                else if (Column_Name == "Rap Rate($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                }
                                else if (Column_Name == "Rap Amount($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                }
                                else if (Column_Name == "Supplier Cost Disc(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"] != null) ?
                                        (dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"].GetType().Name != "DBNull" ?
                                        Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(cellBg1);
                                }
                                else if (Column_Name == "Supplier Cost Value($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(cellBg1);
                                }
                                else if (Column_Name == "Sunrise Disc(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(cellBg);
                                }
                                else if (Column_Name == "Sunrise Value US($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(cellBg);
                                }
                                else if (Column_Name == "Supplier Base Offer(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Disc"] != null) ?
                                    (dtDiamonds.Rows[i - inStartIndex]["Disc"].GetType().Name != "DBNull" ?
                                    Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Disc"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                }
                                else if (Column_Name == "Supplier Base Offer Value($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Value"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Value"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Value"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                }
                                else if (Column_Name == "Cut")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]);
                                    if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]).ToUpper() == "3EX")
                                    {
                                        worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    }
                                }
                                else if (Column_Name == "Polish")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Polish"]);
                                    if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]).ToUpper() == "3EX")
                                    {
                                        worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    }
                                }
                                else if (Column_Name == "Symm")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Symm"]);
                                    if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]).ToUpper() == "3EX")
                                    {
                                        worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    }
                                }
                                else if (Column_Name == "Fls")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fls"]);
                                }
                                else if (Column_Name == "Length")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Length"] != null) ?
                                   (dtDiamonds.Rows[i - inStartIndex]["Length"].GetType().Name != "DBNull" ?
                                   Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Length"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Width")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Width"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Width"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Width"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Depth")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Depth"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Depth(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth_Per"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Depth_Per"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth_Per"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Table(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Table_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Table_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Table_Per"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Key To Symbol")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Key_To_Symboll"]);
                                }
                                else if (Column_Name == "Comment")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab_Comments"]);
                                }
                                else if (Column_Name == "Girdle(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Crown Angle")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Crown Height")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Height"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Crown_Height"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Height"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Pavilion Angle")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Pavilion Height")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Height"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Height"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Height"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Table Black")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Natts"]);
                                }
                                else if (Column_Name == "Crown Black")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"]);
                                }
                                else if (Column_Name == "Table White")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Inclusion"]);
                                }
                                else if (Column_Name == "Crown White")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Inclusion"]);
                                }
                                else if (Column_Name == "Culet")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Culet"]);
                                }
                                else if (Column_Name == "Table Open")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Open"]);
                                }
                                else if (Column_Name == "Crown Open")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Open"]);
                                }
                                else if (Column_Name == "Pav Open")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pav_Open"]);
                                }
                                else if (Column_Name == "Girdle Open")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle_Open"]);
                                }
                            }
                        }

                        inwrkrow++;

                        #endregion
                    }

                    worksheet.Cells[inStartIndex, 1, (inwrkrow - 1), Row_Count].Style.Font.Size = 9;

                    int kkk = 0;
                    for (int j = 0; j < Col_dt.Rows.Count; j++)
                    {
                        string Column_Name = Convert.ToString(Col_dt.Rows[j]["Column_Name"]);

                        if (Column_Name == "Image-Video")
                        {
                            kkk += 2;
                        }
                        else
                        {
                            kkk += 1;

                            if (Column_Name == "Supplier Ref No")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(103," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##";

                                ExcelStyle cellStyleHeader_Total = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_Total.Border.Left.Style = cellStyleHeader_Total.Border.Right.Style
                                        = cellStyleHeader_Total.Border.Top.Style = cellStyleHeader_Total.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                            else if (Column_Name == "Cts")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0.00";

                                ExcelStyle cellStyleHeader_Totalcarat = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_Totalcarat.Border.Left.Style = cellStyleHeader_Totalcarat.Border.Right.Style
                                        = cellStyleHeader_Totalcarat.Border.Top.Style = cellStyleHeader_Totalcarat.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                            else if (Column_Name == "Rap Amount($)")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0";

                                ExcelStyle cellStyleHeader_RapAmt = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_RapAmt.Border.Left.Style = cellStyleHeader_RapAmt.Border.Right.Style
                                        = cellStyleHeader_RapAmt.Border.Top.Style = cellStyleHeader_RapAmt.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                            else if (Column_Name == "Supplier Cost Disc(%)")
                            {
                                int Image_Video = 0, Rap_Amount = 0, Supplier_Cost_Value_Doller = 0;
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video'");
                                if (dra.Length > 0)
                                {
                                    Image_Video = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Rap Amount($)'");
                                if (dra.Length > 0)
                                {
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Rap_Amount = ((Image_Video != 0 && Rap_Amount > Image_Video) ? Rap_Amount + 1 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Supplier Cost Value($)'");
                                if (dra.Length > 0)
                                {
                                    Supplier_Cost_Value_Doller = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Supplier_Cost_Value_Doller = ((Image_Video != 0 && Supplier_Cost_Value_Doller > Image_Video) ? Supplier_Cost_Value_Doller + 1 : Supplier_Cost_Value_Doller); ;
                                }

                                if (Rap_Amount != 0 && Supplier_Cost_Value_Doller != 0)
                                {
                                    worksheet.Cells[1, kkk].Formula = "IF(SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ":" + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109," + GetExcelColumnLetter(Supplier_Cost_Value_Doller) + "" + inStartIndex + ":" + GetExcelColumnLetter(Supplier_Cost_Value_Doller) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ":" + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")))*(-100),2))";
                                    worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0.00";

                                    ExcelStyle cellStyleHeader_TotalDis = worksheet.Cells[1, kkk].Style;
                                    cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                                            = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                                            = ExcelBorderStyle.Medium;
                                }
                            }
                            else if (Column_Name == "Supplier Cost Value($)")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0";

                                ExcelStyle cellStyleHeader_TotalAmt = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_TotalAmt.Border.Left.Style = cellStyleHeader_TotalAmt.Border.Right.Style
                                        = cellStyleHeader_TotalAmt.Border.Top.Style = cellStyleHeader_TotalAmt.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                            else if (Column_Name == "Sunrise Disc(%)")
                            {
                                int Image_Video = 0, Rap_Amount = 0, Sunrise_Value_US_Doller = 0;
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video'");
                                if (dra.Length > 0)
                                {
                                    Image_Video = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Rap Amount($)'");
                                if (dra.Length > 0)
                                {
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Rap_Amount = ((Image_Video != 0 && Rap_Amount > Image_Video) ? Rap_Amount + 1 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Sunrise Value US($)'");
                                if (dra.Length > 0)
                                {
                                    Sunrise_Value_US_Doller = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Sunrise_Value_US_Doller = ((Image_Video != 0 && Sunrise_Value_US_Doller > Image_Video) ? Sunrise_Value_US_Doller + 1 : Sunrise_Value_US_Doller); ;
                                }

                                if (Rap_Amount != 0 && Sunrise_Value_US_Doller != 0)
                                {
                                    worksheet.Cells[1, kkk].Formula = "IF(SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ": " + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109," + GetExcelColumnLetter(Sunrise_Value_US_Doller) + "" + inStartIndex + ":" + GetExcelColumnLetter(Sunrise_Value_US_Doller) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ":" + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")))*(-100),2))";
                                    worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0.00";

                                    ExcelStyle cellStyleHeader_TotalDis_1 = worksheet.Cells[1, kkk].Style;
                                    cellStyleHeader_TotalDis_1.Border.Left.Style = cellStyleHeader_TotalDis_1.Border.Right.Style
                                            = cellStyleHeader_TotalDis_1.Border.Top.Style = cellStyleHeader_TotalDis_1.Border.Bottom.Style
                                            = ExcelBorderStyle.Medium;
                                }
                            }
                            else if (Column_Name == "Sunrise Value US($)")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0";

                                ExcelStyle cellStyleHeader_TotalAmt_1 = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_TotalAmt_1.Border.Left.Style = cellStyleHeader_TotalAmt_1.Border.Right.Style
                                        = cellStyleHeader_TotalAmt_1.Border.Top.Style = cellStyleHeader_TotalAmt_1.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                            else if (Column_Name == "Supplier Base Offer(%)")
                            {
                                int Image_Video = 0, Rap_Amount = 0, Supplier_Base_Offer_Value_Doller = 0;
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video'");
                                if (dra.Length > 0)
                                {
                                    Image_Video = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Rap Amount($)'");
                                if (dra.Length > 0)
                                {
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Rap_Amount = ((Image_Video != 0 && Rap_Amount > Image_Video) ? Rap_Amount + 1 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Supplier Base Offer Value($)'");
                                if (dra.Length > 0)
                                {
                                    Supplier_Base_Offer_Value_Doller = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Supplier_Base_Offer_Value_Doller = ((Image_Video != 0 && Supplier_Base_Offer_Value_Doller > Image_Video) ? Supplier_Base_Offer_Value_Doller + 1 : Supplier_Base_Offer_Value_Doller); ;
                                }

                                if (Rap_Amount != 0 && Supplier_Base_Offer_Value_Doller != 0)
                                {
                                    worksheet.Cells[1, kkk].Formula = "IF(SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ":" + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109," + GetExcelColumnLetter(Supplier_Base_Offer_Value_Doller) + "" + inStartIndex + ":" + GetExcelColumnLetter(Supplier_Base_Offer_Value_Doller) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ":" + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")))*(-100),2))";
                                    worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0.00";

                                    ExcelStyle cellStyleHeader_TotalDis_2 = worksheet.Cells[1, kkk].Style;
                                    cellStyleHeader_TotalDis_2.Border.Left.Style = cellStyleHeader_TotalDis_2.Border.Right.Style
                                            = cellStyleHeader_TotalDis_2.Border.Top.Style = cellStyleHeader_TotalDis_2.Border.Bottom.Style
                                            = ExcelBorderStyle.Medium;
                                }
                            }
                            else if (Column_Name == "Supplier Base Offer Value($)")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0";

                                ExcelStyle cellStyleHeader_TotalAmt_2 = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_TotalAmt_2.Border.Left.Style = cellStyleHeader_TotalAmt_2.Border.Right.Style
                                        = cellStyleHeader_TotalAmt_2.Border.Top.Style = cellStyleHeader_TotalAmt_2.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                        }
                    }

                    int rowEnd = worksheet.Dimension.End.Row;
                    removingGreenTagWarning(worksheet, worksheet.Cells[1, 1, rowEnd, 100].Address);

                    Byte[] bin = p.GetAsByteArray();

                    if (!Directory.Exists(_strFolderPath))
                    {
                        Directory.CreateDirectory(_strFolderPath);
                    }

                    System.IO.File.WriteAllBytes(_strFilePath, bin);

                }
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }
        public static void Customer_Excel(DataTable dtDiamonds, DataTable Col_dt, string _strFolderPath, string _strFilePath)
        {
            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    int Row_Count = Col_dt.Rows.Count;
                    int inStartIndex = 3;
                    int inwrkrow = 3;
                    int inEndCounter = dtDiamonds.Rows.Count + inStartIndex;
                    int TotalRow = dtDiamonds.Rows.Count;
                    int i;
                    Int64 number_1;
                    bool success1;

                    Color colFromHex_Pointer = System.Drawing.ColorTranslator.FromHtml("#c6e0b4");
                    Color colFromHex_Dis = System.Drawing.ColorTranslator.FromHtml("#ccffff");
                    Color colFromHexTotal = System.Drawing.ColorTranslator.FromHtml("#d9e1f2");
                    Color tcpg_bg_clr = System.Drawing.ColorTranslator.FromHtml("#fff2cc");
                    Color cellBg = System.Drawing.ColorTranslator.FromHtml("#ccffff");
                    Color ppc_bg = System.Drawing.ColorTranslator.FromHtml("#c6e0b4");
                    Color ratio_bg = System.Drawing.ColorTranslator.FromHtml("#bdd7ee");

                    #region Company Detail on Header

                    p.Workbook.Properties.Author = "SUNRISE DIAMOND";
                    p.Workbook.Properties.Title = "SUNRISE DIAMOND PVT. LTD.";
                    p.Workbook.Worksheets.Add("Customer Stock");

                    ExcelWorksheet worksheet = p.Workbook.Worksheets[1];
                    worksheet.Name = DateTime.Now.ToString("dd-MM-yyyy");
                    worksheet.Cells.Style.Font.Size = 11;
                    worksheet.Cells.Style.Font.Name = "Calibri";

                    Color colFromHex_H1 = System.Drawing.ColorTranslator.FromHtml("#8497b0");
                    Color col_color_Red = System.Drawing.ColorTranslator.FromHtml("#ff0000");

                    worksheet.Row(1).Height = 40;
                    worksheet.Row(2).Height = 40;
                    worksheet.Row(2).Style.WrapText = true;

                    #endregion

                    #region Header Name Declaration

                    //worksheet.Cells[2, 1].Value = "Sr. No";
                    //worksheet.Cells[2, 1].AutoFitColumns(7);
                    //Row_Count += 1;

                    int k = 0;
                    for (int j = 0; j < Col_dt.Rows.Count; j++)
                    {
                        string Column_Name = Convert.ToString(Col_dt.Rows[j]["Column_Name"]);
                        double AutoFitColumns = Convert.ToDouble(Col_dt.Rows[j]["ExcelWidth"]);

                        if (Column_Name == "Image-Video-Certi")
                        {
                            Row_Count += 2;

                            k += 1;
                            worksheet.Cells[2, k].Value = "Image";
                            worksheet.Cells[2, k].AutoFitColumns(7);

                            k += 1;
                            worksheet.Cells[2, k].Value = "Video";
                            worksheet.Cells[2, k].AutoFitColumns(7);

                            k += 1;
                            worksheet.Cells[2, k].Value = "Certi";
                            worksheet.Cells[2, k].AutoFitColumns(6);
                        }
                        else
                        {
                            k += 1;
                            worksheet.Cells[2, k].Value = Column_Name;
                            worksheet.Cells[2, k].AutoFitColumns(AutoFitColumns);

                            if (Column_Name == "Pointer")
                            {
                                worksheet.Cells[2, k].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[2, k].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);
                            }
                        }
                    }

                    worksheet.Cells[1, 1].Value = "Total";
                    worksheet.Cells[1, 1, 1, Row_Count].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, 1, Row_Count].Style.Font.Size = 11;
                    worksheet.Cells[1, 1, 1, Row_Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, 1, Row_Count].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[1, 1, 1, Row_Count].Style.Font.Size = 11;

                    worksheet.Cells[2, 1, 2, Row_Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[2, 1, 2, Row_Count].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    worksheet.Cells[2, 1, 2, Row_Count].Style.Font.Size = 10;
                    worksheet.Cells[2, 1, 2, Row_Count].Style.Font.Bold = true;

                    worksheet.Cells[2, 1, 2, Row_Count].AutoFilter = true;

                    var cellBackgroundColor1 = worksheet.Cells[2, 1, 2, Row_Count].Style.Fill;
                    cellBackgroundColor1.PatternType = ExcelFillStyle.Solid;
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                    cellBackgroundColor1.BackgroundColor.SetColor(colFromHex);

                    ExcelStyle cellStyleHeader1 = worksheet.Cells[2, 1, 2, Row_Count].Style;
                    cellStyleHeader1.Border.Left.Style = cellStyleHeader1.Border.Right.Style
                            = cellStyleHeader1.Border.Top.Style = cellStyleHeader1.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    #endregion

                    #region Set AutoFit and Decimal Number Format

                    worksheet.View.FreezePanes(3, 1);
                    worksheet.Cells[inStartIndex, 1, inEndCounter, Row_Count].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    #endregion
                    var asTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo;


                    for (i = inStartIndex; i < inEndCounter; i++)
                    {
                        #region Assigns Value to Cell

                        //worksheet.Cells[inwrkrow, 1].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["iSr"]);

                        int kk = 0;
                        for (int j = 0; j < Col_dt.Rows.Count; j++)
                        {
                            string Column_Name = Convert.ToString(Col_dt.Rows[j]["Column_Name"]);
                            double AutoFitColumns = Convert.ToDouble(Col_dt.Rows[j]["ExcelWidth"]);

                            if (Column_Name == "Image-Video-Certi")
                            {
                                kk += 1;

                                string Image_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image_URL"]);
                                if (!string.IsNullOrEmpty(Image_URL))
                                {
                                    worksheet.Cells[inwrkrow, kk].Formula = "=HYPERLINK(\"" + Image_URL + "\",\" Image \")";
                                    worksheet.Cells[inwrkrow, kk].Style.Font.UnderLine = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(Color.Blue);
                                }

                                kk += 1;

                                string Video_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Video_URL"]);
                                if (!string.IsNullOrEmpty(Video_URL))
                                {
                                    worksheet.Cells[inwrkrow, kk].Formula = "=HYPERLINK(\"" + Video_URL + "\",\" Video \")";
                                    worksheet.Cells[inwrkrow, kk].Style.Font.UnderLine = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(Color.Blue);
                                }

                                kk += 1;

                                string Certificate_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_URL"]);
                                if (!string.IsNullOrEmpty(Certificate_URL))
                                {
                                    worksheet.Cells[inwrkrow, kk].Formula = "=HYPERLINK(\"" + Certificate_URL + "\",\" Certi \")";
                                    worksheet.Cells[inwrkrow, kk].Style.Font.UnderLine = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(Color.Blue);
                                }
                            }
                            else
                            {
                                kk += 1;

                                if (Column_Name == "Ref No")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Ref_No"]);
                                }
                                else if (Column_Name == "Cert No")
                                {
                                    string Certificate_No = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    success1 = Int64.TryParse(Certificate_No, out number_1);
                                    if (success1)
                                    {
                                        worksheet.Cells[inwrkrow, kk].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    }
                                    else
                                    {
                                        worksheet.Cells[inwrkrow, kk].Value = Certificate_No;
                                    }
                                }
                                else if (Column_Name == "Lab")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]);
                                    string URL = "";
                                    if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "GIA")
                                    {
                                        URL = "http://www.gia.edu/cs/Satellite?pagename=GST%2FDispatcher&childpagename=GIA%2FPage%2FReportCheck&c=Page&cid=1355954554547&reportno=" + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    }
                                    else if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "HRD")
                                    {
                                        URL = "https://my.hrdantwerp.com/?id=34&record_number=" + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    }
                                    else if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "IGI")
                                    {
                                        URL = "https://www.igi.org/reports/verify-your-report?r=" + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                                    }
                                    if (URL != "")
                                    {
                                        worksheet.Cells[inwrkrow, kk].Formula = "=HYPERLINK(\"" + Convert.ToString(URL) + "\",\" " + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) + " \")";
                                        worksheet.Cells[inwrkrow, kk].Style.Font.UnderLine = true;
                                        worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(Color.Blue);
                                    }
                                }
                                else if (Column_Name == "Shape")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shape"]);
                                }
                                else if (Column_Name == "Pointer")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pointer"]);

                                    //worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    //worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);
                                }
                                else if (Column_Name == "BGM")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["BGM"]);
                                }
                                else if (Column_Name == "Color")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Color"]);
                                }
                                else if (Column_Name == "Clarity")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Clarity"]);
                                }
                                else if (Column_Name == "Cts")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Cts"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Cts"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Cts"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                }
                                else if (Column_Name == "Rap Rate($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                }
                                else if (Column_Name == "Rap Amount($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                }
                                else if (Column_Name == "Offer Disc(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(cellBg);
                                }
                                else if (Column_Name == "Offer Value($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(cellBg);
                                }
                                else if (Column_Name == "Price Cts")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(ppc_bg);
                                }
                                else if (Column_Name == "Cut")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]);
                                    if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]).ToUpper() == "3EX")
                                    {
                                        worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    }
                                }
                                else if (Column_Name == "Polish")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Polish"]);
                                    if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]).ToUpper() == "3EX")
                                    {
                                        worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    }
                                }
                                else if (Column_Name == "Symm")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Symm"]);
                                    if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]).ToUpper() == "3EX")
                                    {
                                        worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    }
                                }
                                else if (Column_Name == "Fls")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fls"]);
                                }
                                else if (Column_Name == "RATIO")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["RATIO"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["RATIO"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["RATIO"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(ratio_bg);
                                }
                                else if (Column_Name == "Length")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Length"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Length"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Length"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Width")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Width"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Width"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Width"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Depth")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Depth"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Depth(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Depth_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth_Per"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Table(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Table_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Table_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Table_Per"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Comment")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab_Comments"]);
                                }
                                else if (Column_Name == "Girdle(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Crown Angle")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Crown Height")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Height"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Crown_Height"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Height"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Pavilion Angle")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Pavilion Height")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Height"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Height"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Height"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Table Black")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Natts"]);
                                }
                                else if (Column_Name == "Crown Black")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"]);
                                }
                                else if (Column_Name == "Table White")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Inclusion"]);
                                }
                                else if (Column_Name == "Crown White")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Inclusion"]);
                                }
                                else if (Column_Name == "Culet")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Culet"]);
                                }
                                else if (Column_Name == "Table Open")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Open"]);
                                }
                                else if (Column_Name == "Girdle Open")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle_Open"]);
                                }
                                else if (Column_Name == "Crown Open")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Open"]);
                                }
                                else if (Column_Name == "Pav Open")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pav_Open"]);
                                }
                            }
                        }

                        inwrkrow++;

                        #endregion
                    }

                    worksheet.Cells[inStartIndex, 1, (inwrkrow - 1), Row_Count].Style.Font.Size = 9;

                    int kkk = 0;
                    for (int j = 0; j < Col_dt.Rows.Count; j++)
                    {
                        string Column_Name = Convert.ToString(Col_dt.Rows[j]["Column_Name"]);

                        if (Column_Name == "Image-Video-Certi")
                        {
                            kkk += 3;
                        }
                        else
                        {
                            kkk += 1;

                            if (Column_Name == "Ref No")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(103," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##";

                                ExcelStyle cellStyleHeader_Total = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_Total.Border.Left.Style = cellStyleHeader_Total.Border.Right.Style
                                        = cellStyleHeader_Total.Border.Top.Style = cellStyleHeader_Total.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                            else if (Column_Name == "Cts")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0.00";

                                ExcelStyle cellStyleHeader_Totalcarat = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_Totalcarat.Border.Left.Style = cellStyleHeader_Totalcarat.Border.Right.Style
                                        = cellStyleHeader_Totalcarat.Border.Top.Style = cellStyleHeader_Totalcarat.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                            else if (Column_Name == "Offer Disc(%)")
                            {
                                int Image_Video_Certi = 0, Rap_Amount = 0, Offer_Value_Doller = 0;
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video-Certi'");
                                if (dra.Length > 0)
                                {
                                    Image_Video_Certi = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Rap Amount($)'");
                                if (dra.Length > 0)
                                {
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 2 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Offer Value($)'");
                                if (dra.Length > 0)
                                {
                                    Offer_Value_Doller = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Offer_Value_Doller = ((Image_Video_Certi != 0 && Offer_Value_Doller > Image_Video_Certi) ? Offer_Value_Doller + 2 : Offer_Value_Doller); ;
                                }

                                if (Rap_Amount != 0 && Offer_Value_Doller != 0)
                                {
                                    worksheet.Cells[1, kkk].Formula = "IF(SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ":" + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109," + GetExcelColumnLetter(Offer_Value_Doller) + "" + inStartIndex + ":" + GetExcelColumnLetter(Offer_Value_Doller) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109," + GetExcelColumnLetter(Rap_Amount) + "" + inStartIndex + ":" + GetExcelColumnLetter(Rap_Amount) + "" + (inwrkrow - 1) + ")))*(-100),2))";
                                    worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0.00";

                                    ExcelStyle cellStyleHeader_TotalDis = worksheet.Cells[1, kkk].Style;
                                    cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                                            = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                                            = ExcelBorderStyle.Medium;
                                }
                            }
                            else if (Column_Name == "Offer Value($)")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0";

                                ExcelStyle cellStyleHeader_TotalAmt = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_TotalAmt.Border.Left.Style = cellStyleHeader_TotalAmt.Border.Right.Style
                                        = cellStyleHeader_TotalAmt.Border.Top.Style = cellStyleHeader_TotalAmt.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                            else if (Column_Name == "Rap Amount($)")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(kkk) + "" + inStartIndex + ":" + GetExcelColumnLetter(kkk) + "" + (inwrkrow - 1) + "),2)";
                                worksheet.Cells[1, kkk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                worksheet.Cells[1, kkk].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                                worksheet.Cells[1, kkk].Style.Numberformat.Format = "#,##0";

                                ExcelStyle cellStyleHeader_RapAmt = worksheet.Cells[1, kkk].Style;
                                cellStyleHeader_RapAmt.Border.Left.Style = cellStyleHeader_RapAmt.Border.Right.Style
                                        = cellStyleHeader_RapAmt.Border.Top.Style = cellStyleHeader_RapAmt.Border.Bottom.Style
                                        = ExcelBorderStyle.Medium;
                            }
                        }
                    }

                    int rowEnd = worksheet.Dimension.End.Row;
                    removingGreenTagWarning(worksheet, worksheet.Cells[1, 1, rowEnd, 100].Address);

                    Byte[] bin = p.GetAsByteArray();

                    if (!Directory.Exists(_strFolderPath))
                    {
                        Directory.CreateDirectory(_strFolderPath);
                    }

                    System.IO.File.WriteAllBytes(_strFilePath, bin);

                }
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }
        private static void removingGreenTagWarning(ExcelWorksheet template1, string address)
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
        static string GetExcelColumnLetter(int columnNumber)
        {
            string columnLetter = "";

            while (columnNumber > 0)
            {
                int remainder = (columnNumber - 1) % 26;
                char letter = (char)('A' + remainder);
                columnLetter = letter + columnLetter;
                columnNumber = (columnNumber - 1) / 26;
            }

            return columnLetter;
        }
        public static void Not_Mapped_SupplierStock_Excel(DataTable dtDiamonds, string suppName, string _strFolderPath, string _strFilePath)
        {
            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    Color red_font = System.Drawing.ColorTranslator.FromHtml("#ff0000");
                    Color red_bg = System.Drawing.ColorTranslator.FromHtml("#ffc1c1");

                    int inStartIndex = 4;
                    int inwrkrow = 4;
                    int inEndCounter = dtDiamonds.Rows.Count + inStartIndex;
                    int TotalRow = dtDiamonds.Rows.Count;
                    int i;

                    #region Company Detail on Header

                    p.Workbook.Properties.Author = "SUNRISE DIAMOND";
                    p.Workbook.Properties.Title = "SUNRISE DIAMOND PVT. LTD.";
                    p.Workbook.Worksheets.Add(suppName);

                    ExcelWorksheet worksheet = p.Workbook.Worksheets[1];
                    worksheet.Name = DateTime.Now.ToString("dd-MM-yyyy");
                    worksheet.Cells.Style.Font.Size = 11;
                    worksheet.Cells.Style.Font.Name = "Calibri";
                    worksheet.Cells[1, 3, 3, 12].Style.Font.Bold = true;

                    worksheet.Row(3).Height = 40;
                    worksheet.Row(3).Style.WrapText = true;


                    worksheet.Cells[3, 1, 3, 71].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[3, 1, 3, 71].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    worksheet.Cells[3, 1, 3, 71].Style.Font.Size = 10;
                    worksheet.Cells[3, 1, 3, 71].Style.Font.Bold = true;

                    worksheet.Cells[3, 1, 3, 71].AutoFilter = true;

                    var cellBackgroundColor1 = worksheet.Cells[3, 1, 3, 71].Style.Fill;
                    cellBackgroundColor1.PatternType = ExcelFillStyle.Solid;
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                    cellBackgroundColor1.BackgroundColor.SetColor(colFromHex);

                    #endregion

                    #region Header Name Declaration

                    worksheet.Cells[3, 1].Value = "Sr. No";
                    worksheet.Cells[3, 2].Value = "Not Mapped Column";
                    worksheet.Cells[3, 3].Value = "Shape";
                    worksheet.Cells[3, 4].Value = "Color";
                    worksheet.Cells[3, 5].Value = "Clarity";
                    worksheet.Cells[3, 6].Value = "Cut";
                    worksheet.Cells[3, 7].Value = "Polish";
                    worksheet.Cells[3, 8].Value = "Symm";
                    worksheet.Cells[3, 9].Value = "Fls";
                    worksheet.Cells[3, 10].Value = "Cts";
                    //worksheet.Cells[3, 11].Value = "Pointer";
                    //worksheet.Cells[3, 12].Value = "Sub Pointer";
                    worksheet.Cells[3, 11].Value = "Base Price/Ct";
                    //worksheet.Cells[3, 14].Value = "Rap Rate";
                    worksheet.Cells[3, 12].Value = "Base Amt";
                    worksheet.Cells[3, 13].Value = "Measurement";
                    worksheet.Cells[3, 14].Value = "Length";
                    worksheet.Cells[3, 15].Value = "Width";
                    worksheet.Cells[3, 16].Value = "Depth";
                    worksheet.Cells[3, 17].Value = "Table %";
                    worksheet.Cells[3, 18].Value = "Depth %";
                    worksheet.Cells[3, 19].Value = "Table White";
                    worksheet.Cells[3, 20].Value = "Crown White";
                    worksheet.Cells[3, 21].Value = "Table Black";
                    worksheet.Cells[3, 22].Value = "Crown Black";
                    //worksheet.Cells[3, 26].Value = "Side Inclusion";
                    //worksheet.Cells[3, 27].Value = "Side Natts";
                    worksheet.Cells[3, 23].Value = "Crown Open";
                    worksheet.Cells[3, 24].Value = "Pav Open";
                    worksheet.Cells[3, 25].Value = "Table Open";
                    worksheet.Cells[3, 26].Value = "Girdle Open";
                    worksheet.Cells[3, 27].Value = "Crown Angle";
                    worksheet.Cells[3, 28].Value = "Pav Angle";
                    worksheet.Cells[3, 29].Value = "Crown Height";
                    worksheet.Cells[3, 30].Value = "Pav Height";
                    //worksheet.Cells[3, 36].Value = "Rap Amount";
                    worksheet.Cells[3, 31].Value = "Lab";
                    worksheet.Cells[3, 32].Value = "Lab URL";
                    worksheet.Cells[3, 33].Value = "Image Real";
                    worksheet.Cells[3, 34].Value = "Image Asset";
                    worksheet.Cells[3, 35].Value = "Image Heart";
                    worksheet.Cells[3, 36].Value = "Image Arrow";
                    worksheet.Cells[3, 37].Value = "Video URL";
                    worksheet.Cells[3, 38].Value = "DNA";
                    worksheet.Cells[3, 39].Value = "Status";
                    worksheet.Cells[3, 40].Value = "Supplier Ref No";
                    worksheet.Cells[3, 41].Value = "Location";
                    worksheet.Cells[3, 42].Value = "Shade";
                    worksheet.Cells[3, 43].Value = "Luster";
                    worksheet.Cells[3, 44].Value = "Cert Type";
                    worksheet.Cells[3, 45].Value = "Milky";
                    worksheet.Cells[3, 46].Value = "BGM";
                    worksheet.Cells[3, 47].Value = "Key to Symbol";
                    //worksheet.Cells[3, 54].Value = "RATIO";
                    worksheet.Cells[3, 48].Value = "Supplier Comment";
                    worksheet.Cells[3, 49].Value = "Comment";
                    worksheet.Cells[3, 50].Value = "Culet";
                    worksheet.Cells[3, 51].Value = "Girdle %";
                    worksheet.Cells[3, 52].Value = "Girdle Type";
                    worksheet.Cells[3, 53].Value = "Girdle From";
                    worksheet.Cells[3, 54].Value = "Laser Inscription";
                    //worksheet.Cells[3, 62].Value = "Culet Condition";
                    worksheet.Cells[3, 55].Value = "Star Length";
                    worksheet.Cells[3, 56].Value = "Lower HF";
                    //worksheet.Cells[3, 65].Value = "Stage";
                    worksheet.Cells[3, 57].Value = "Certi Date";
                    worksheet.Cells[3, 58].Value = "Base Dis";
                    //worksheet.Cells[3, 68].Value = "Fix Price";
                    worksheet.Cells[3, 59].Value = "Cert No";
                    //worksheet.Cells[3, 70].Value = "Ref No";
                    //worksheet.Cells[3, 71].Value = "Goods Type";
                    worksheet.Cells[3, 60].Value = "Country of Origin";
                    worksheet.Cells[3, 61].Value = "Girdle";
                    worksheet.Cells[3, 62].Value = "H&A";
                    worksheet.Cells[3, 63].Value = "Fls Color";
                    worksheet.Cells[3, 64].Value = "Fancy Color";
                    worksheet.Cells[3, 65].Value = "Fancy Overtone";
                    worksheet.Cells[3, 66].Value = "Fancy Intensity";
                    worksheet.Cells[3, 67].Value = "Girdle To";
                    worksheet.Cells[3, 68].Value = "Image URL 1";
                    worksheet.Cells[3, 69].Value = "Image URL 2";
                    worksheet.Cells[3, 70].Value = "Video MP4";
                    worksheet.Cells[3, 71].Value = "Stock Upload Using";

                    ExcelStyle cellStyleHeader1 = worksheet.Cells[3, 1, 3, 71].Style;
                    cellStyleHeader1.Border.Left.Style = cellStyleHeader1.Border.Right.Style
                            = cellStyleHeader1.Border.Top.Style = cellStyleHeader1.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;
                                       
                    #endregion         
                                       
                    #region Set AutoFit and Decimal Number Format

                    worksheet.View.FreezePanes(4, 1);

                    worksheet.Cells[3, 1].AutoFitColumns(5);
                    worksheet.Cells[3, 2].AutoFitColumns(30);
                    worksheet.Cells[3, 3].AutoFitColumns(15);
                    worksheet.Cells[3, 4].AutoFitColumns(15);
                    worksheet.Cells[3, 5].AutoFitColumns(15);
                    worksheet.Cells[3, 6].AutoFitColumns(15);
                    worksheet.Cells[3, 7].AutoFitColumns(15);
                    worksheet.Cells[3, 8].AutoFitColumns(15);
                    worksheet.Cells[3, 9].AutoFitColumns(15);
                    worksheet.Cells[3, 10].AutoFitColumns(15);
                    //worksheet.Cells[3, 11].AutoFitColumns(15);
                    //worksheet.Cells[3, 12].AutoFitColumns(15);
                    worksheet.Cells[3, 11].AutoFitColumns(15);
                    //worksheet.Cells[3, 14].AutoFitColumns(15);
                    worksheet.Cells[3, 12].AutoFitColumns(15);
                    worksheet.Cells[3, 13].AutoFitColumns(15);
                    worksheet.Cells[3, 14].AutoFitColumns(15);
                    worksheet.Cells[3, 15].AutoFitColumns(15);
                    worksheet.Cells[3, 16].AutoFitColumns(15);
                    worksheet.Cells[3, 17].AutoFitColumns(15);
                    worksheet.Cells[3, 18].AutoFitColumns(15);
                    worksheet.Cells[3, 19].AutoFitColumns(15);
                    worksheet.Cells[3, 20].AutoFitColumns(15);
                    worksheet.Cells[3, 21].AutoFitColumns(15);
                    worksheet.Cells[3, 22].AutoFitColumns(15);
                    //worksheet.Cells[3, 26].AutoFitColumns(15);
                    //worksheet.Cells[3, 27].AutoFitColumns(15);
                    worksheet.Cells[3, 23].AutoFitColumns(15);
                    worksheet.Cells[3, 24].AutoFitColumns(15);
                    worksheet.Cells[3, 25].AutoFitColumns(15);
                    worksheet.Cells[3, 26].AutoFitColumns(15);
                    worksheet.Cells[3, 27].AutoFitColumns(15);
                    worksheet.Cells[3, 28].AutoFitColumns(15);
                    worksheet.Cells[3, 29].AutoFitColumns(15);
                    worksheet.Cells[3, 30].AutoFitColumns(15);
                    //worksheet.Cells[3, 36].AutoFitColumns(15);
                    worksheet.Cells[3, 31].AutoFitColumns(15);
                    worksheet.Cells[3, 32].AutoFitColumns(15);
                    worksheet.Cells[3, 33].AutoFitColumns(15);
                    worksheet.Cells[3, 34].AutoFitColumns(15);
                    worksheet.Cells[3, 35].AutoFitColumns(15);
                    worksheet.Cells[3, 36].AutoFitColumns(15);
                    worksheet.Cells[3, 37].AutoFitColumns(15);
                    worksheet.Cells[3, 38].AutoFitColumns(15);
                    worksheet.Cells[3, 39].AutoFitColumns(15);
                    worksheet.Cells[3, 40].AutoFitColumns(15);
                    worksheet.Cells[3, 41].AutoFitColumns(15);
                    worksheet.Cells[3, 42].AutoFitColumns(15);
                    worksheet.Cells[3, 43].AutoFitColumns(15);
                    worksheet.Cells[3, 44].AutoFitColumns(15);
                    worksheet.Cells[3, 45].AutoFitColumns(15);
                    worksheet.Cells[3, 46].AutoFitColumns(15);
                    worksheet.Cells[3, 47].AutoFitColumns(15);
                    //worksheet.Cells[3, 54].AutoFitColumns(15);
                    worksheet.Cells[3, 48].AutoFitColumns(15);
                    worksheet.Cells[3, 49].AutoFitColumns(15);
                    worksheet.Cells[3, 50].AutoFitColumns(15);
                    worksheet.Cells[3, 51].AutoFitColumns(15);
                    worksheet.Cells[3, 52].AutoFitColumns(15);
                    worksheet.Cells[3, 53].AutoFitColumns(15);
                    worksheet.Cells[3, 54].AutoFitColumns(15);
                    //worksheet.Cells[3, 62].AutoFitColumns(15);
                    worksheet.Cells[3, 55].AutoFitColumns(15);
                    worksheet.Cells[3, 56].AutoFitColumns(15);
                    //worksheet.Cells[3, 65].AutoFitColumns(15);
                    worksheet.Cells[3, 57].AutoFitColumns(15);
                    worksheet.Cells[3, 58].AutoFitColumns(15);
                    //worksheet.Cells[3, 68].AutoFitColumns(15);
                    worksheet.Cells[3, 59].AutoFitColumns(15);
                    //worksheet.Cells[3, 70].AutoFitColumns(15);
                    //worksheet.Cells[3, 71].AutoFitColumns(15);
                    worksheet.Cells[3, 60].AutoFitColumns(15);
                    worksheet.Cells[3, 61].AutoFitColumns(15);
                    worksheet.Cells[3, 62].AutoFitColumns(15);
                    worksheet.Cells[3, 63].AutoFitColumns(15);
                    worksheet.Cells[3, 64].AutoFitColumns(15);
                    worksheet.Cells[3, 65].AutoFitColumns(15);
                    worksheet.Cells[3, 66].AutoFitColumns(15);
                    worksheet.Cells[3, 67].AutoFitColumns(15);
                    worksheet.Cells[3, 68].AutoFitColumns(15);
                    worksheet.Cells[3, 69].AutoFitColumns(15);
                    worksheet.Cells[3, 70].AutoFitColumns(15);
                    worksheet.Cells[3, 71].AutoFitColumns(15);

                    worksheet.Cells[inStartIndex, 1, inEndCounter, 71].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    #endregion
                    var asTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo;

                    worksheet.Cells[1, 1, 1, 2].Merge = true;
                    worksheet.Cells[1, 1].Value = "Trans Date : " + Convert.ToDateTime(dtDiamonds.Rows[0]["TransDate"]).ToString("dd-MMM-yyyy hh:mm:ss tt");

                    worksheet.Cells[1, 3, 1, 10].Merge = true;
                    worksheet.Cells[1, 3].Value = Convert.ToString(suppName);
                    worksheet.Cells[1, 3, 1, 10].Style.Font.Size = 18;
                    worksheet.Cells[1, 3, 1, 10].Style.Font.Bold = true;

                    worksheet.Cells[1, 1, 1, 10].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, 1, 10].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                    for (i = inStartIndex; i < inEndCounter; i++)
                    {
                        #region Assigns Value to Cell
                        string[] NotMappedColumn = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Not Mapped Column"]).Split(',');

                        worksheet.Cells[inwrkrow, 1].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Sr"]);
                        worksheet.Cells[inwrkrow, 2].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Not Mapped Column"]);

                        worksheet.Cells[inwrkrow, 3].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shape"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Shape"))
                        {
                            worksheet.Cells[inwrkrow, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 3].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 4].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Color"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Color"))
                        {
                            worksheet.Cells[inwrkrow, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 4].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 5].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Clarity"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Clarity"))
                        {
                            worksheet.Cells[inwrkrow, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 5].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 6].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Cut"))
                        {
                            worksheet.Cells[inwrkrow, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 6].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 7].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Polish"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Polish"))
                        {
                            worksheet.Cells[inwrkrow, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 7].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 8].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Symm"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Symm"))
                        {
                            worksheet.Cells[inwrkrow, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 8].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 9].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fls"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Fls"))
                        {
                            worksheet.Cells[inwrkrow, 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 9].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 10].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cts"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Cts"))
                        {
                            worksheet.Cells[inwrkrow, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 10].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        //worksheet.Cells[inwrkrow, 11].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pointer"]);
                        //if (Array.Exists(NotMappedColumn, element => element.Trim() == "Pointer"))
                        //{
                        //    worksheet.Cells[inwrkrow, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //    worksheet.Cells[inwrkrow, 11].Style.Fill.BackgroundColor.SetColor(red_bg);
                        //}

                        //worksheet.Cells[inwrkrow, 12].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Sub Pointer"]);
                        //if (Array.Exists(NotMappedColumn, element => element.Trim() == "Sub Pointer"))
                        //{
                        //    worksheet.Cells[inwrkrow, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //    worksheet.Cells[inwrkrow, 12].Style.Fill.BackgroundColor.SetColor(red_bg);
                        //}

                        worksheet.Cells[inwrkrow, 11].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Base Price/Ct"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Base Price/Ct"))
                        {
                            worksheet.Cells[inwrkrow, 11].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 11].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        //worksheet.Cells[inwrkrow, 14].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Rap Rate"]);
                        //if (Array.Exists(NotMappedColumn, element => element.Trim() == "Rap Rate"))
                        //{
                        //    worksheet.Cells[inwrkrow, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //    worksheet.Cells[inwrkrow, 14].Style.Fill.BackgroundColor.SetColor(red_bg);
                        //}

                        worksheet.Cells[inwrkrow, 12].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Base Amt"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Base Amt"))
                        {
                            worksheet.Cells[inwrkrow, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 12].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 13].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Measurement"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Measurement"))
                        {
                            worksheet.Cells[inwrkrow, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 13].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 14].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Length"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Length"))
                        {
                            worksheet.Cells[inwrkrow, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 14].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 15].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Width"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Width"))
                        {
                            worksheet.Cells[inwrkrow, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 15].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 16].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Depth"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Depth"))
                        {
                            worksheet.Cells[inwrkrow, 16].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 16].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 17].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table %"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Table %"))
                        {
                            worksheet.Cells[inwrkrow, 17].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 17].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 18].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Depth %"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Depth %"))
                        {
                            worksheet.Cells[inwrkrow, 18].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 18].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 19].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table White"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Table White"))
                        {
                            worksheet.Cells[inwrkrow, 19].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 19].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 20].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown White"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Crown White"))
                        {
                            worksheet.Cells[inwrkrow, 20].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 20].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 21].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table Black"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Table Black"))
                        {
                            worksheet.Cells[inwrkrow, 21].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 21].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 22].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown Black"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Crown Black"))
                        {
                            worksheet.Cells[inwrkrow, 22].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 22].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        //worksheet.Cells[inwrkrow, 26].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Side Inclusion"]);
                        //if (Array.Exists(NotMappedColumn, element => element.Trim() == "Side Inclusion"))
                        //{
                        //    worksheet.Cells[inwrkrow, 26].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //    worksheet.Cells[inwrkrow, 26].Style.Fill.BackgroundColor.SetColor(red_bg);
                        //}

                        //worksheet.Cells[inwrkrow, 27].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Side Natts"]);
                        //if (Array.Exists(NotMappedColumn, element => element.Trim() == "Side Natts"))
                        //{
                        //    worksheet.Cells[inwrkrow, 27].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //    worksheet.Cells[inwrkrow, 27].Style.Fill.BackgroundColor.SetColor(red_bg);
                        //}

                        worksheet.Cells[inwrkrow, 23].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown Open"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Crown Open"))
                        {
                            worksheet.Cells[inwrkrow, 23].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 23].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 24].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pav Open"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Pav Open"))
                        {
                            worksheet.Cells[inwrkrow, 24].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 24].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 25].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table Open"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Table Open"))
                        {
                            worksheet.Cells[inwrkrow, 25].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 25].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 26].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle Open"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Girdle Open"))
                        {
                            worksheet.Cells[inwrkrow, 26].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 26].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 27].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown Angle"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Crown Angle"))
                        {
                            worksheet.Cells[inwrkrow, 27].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 27].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 28].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pav Angle"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Pav Angle"))
                        {
                            worksheet.Cells[inwrkrow, 28].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 28].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 29].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown Height"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Crown Height"))
                        {
                            worksheet.Cells[inwrkrow, 29].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 29].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 30].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pav Height"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Pav Height"))
                        {
                            worksheet.Cells[inwrkrow, 30].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 30].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        //worksheet.Cells[inwrkrow, 36].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Rap Amount"]);
                        //if (Array.Exists(NotMappedColumn, element => element.Trim() == "Rap Amount"))
                        //{
                        //    worksheet.Cells[inwrkrow, 36].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //    worksheet.Cells[inwrkrow, 36].Style.Fill.BackgroundColor.SetColor(red_bg);
                        //}

                        worksheet.Cells[inwrkrow, 31].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Lab"))
                        {
                            worksheet.Cells[inwrkrow, 31].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 31].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 32].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab URL"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Lab URL"))
                        {
                            worksheet.Cells[inwrkrow, 32].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 32].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 33].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image Real"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Image Real"))
                        {
                            worksheet.Cells[inwrkrow, 33].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 33].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 34].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image Asset"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Image Asset"))
                        {
                            worksheet.Cells[inwrkrow, 34].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 34].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 35].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image Heart"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Image Heart"))
                        {
                            worksheet.Cells[inwrkrow, 35].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 35].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 36].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image Arrow"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Image Arrow"))
                        {
                            worksheet.Cells[inwrkrow, 36].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 36].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 37].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Video URL"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Video URL"))
                        {
                            worksheet.Cells[inwrkrow, 37].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 37].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 38].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["DNA"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "DNA"))
                        {
                            worksheet.Cells[inwrkrow, 38].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 38].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 39].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Status"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Status"))
                        {
                            worksheet.Cells[inwrkrow, 39].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 39].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 40].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Supplier Ref No"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Supplier Ref No"))
                        {
                            worksheet.Cells[inwrkrow, 40].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 40].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 41].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Location"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Location"))
                        {
                            worksheet.Cells[inwrkrow, 41].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 41].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 42].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shade"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Shade"))
                        {
                            worksheet.Cells[inwrkrow, 42].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 42].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 43].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Luster"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Luster"))
                        {
                            worksheet.Cells[inwrkrow, 43].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 43].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 44].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cert Type"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Cert Type"))
                        {
                            worksheet.Cells[inwrkrow, 44].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 44].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 45].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Milky"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Milky"))
                        {
                            worksheet.Cells[inwrkrow, 45].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 45].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 46].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["BGM"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "BGM"))
                        {
                            worksheet.Cells[inwrkrow, 46].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 46].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 47].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Key to Symbol"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Key to Symbol"))
                        {
                            worksheet.Cells[inwrkrow, 47].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 47].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        //worksheet.Cells[inwrkrow, 54].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["RATIO"]);
                        //if (Array.Exists(NotMappedColumn, element => element.Trim() == "RATIO"))
                        //{
                        //    worksheet.Cells[inwrkrow, 54].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //    worksheet.Cells[inwrkrow, 54].Style.Fill.BackgroundColor.SetColor(red_bg);
                        //}

                        worksheet.Cells[inwrkrow, 48].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Supplier Comment"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Supplier Comment"))
                        {
                            worksheet.Cells[inwrkrow, 48].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 48].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 49].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Comment"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Comment"))
                        {
                            worksheet.Cells[inwrkrow, 49].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 49].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 50].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Culet"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Culet"))
                        {
                            worksheet.Cells[inwrkrow, 50].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 50].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 51].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle %"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Girdle %"))
                        {
                            worksheet.Cells[inwrkrow, 51].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 51].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 52].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle Type"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Girdle Type"))
                        {
                            worksheet.Cells[inwrkrow, 52].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 52].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 53].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle From"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Girdle From"))
                        {
                            worksheet.Cells[inwrkrow, 53].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 53].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 54].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Laser Inscription"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Laser Inscription"))
                        {
                            worksheet.Cells[inwrkrow, 54].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 54].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        //worksheet.Cells[inwrkrow, 62].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Culet Condition"]);
                        //if (Array.Exists(NotMappedColumn, element => element.Trim() == "Culet Condition"))
                        //{
                        //    worksheet.Cells[inwrkrow, 62].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //    worksheet.Cells[inwrkrow, 62].Style.Fill.BackgroundColor.SetColor(red_bg);
                        //}

                        worksheet.Cells[inwrkrow, 55].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Star Length"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Star Length"))
                        {
                            worksheet.Cells[inwrkrow, 55].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 55].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 56].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lower HF"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Lower HF"))
                        {
                            worksheet.Cells[inwrkrow, 56].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 56].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        //worksheet.Cells[inwrkrow, 65].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Stage"]);
                        //if (Array.Exists(NotMappedColumn, element => element.Trim() == "Stage"))
                        //{
                        //    worksheet.Cells[inwrkrow, 65].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //    worksheet.Cells[inwrkrow, 65].Style.Fill.BackgroundColor.SetColor(red_bg);
                        //}

                        worksheet.Cells[inwrkrow, 57].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certi Date"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Certi Date"))
                        {
                            worksheet.Cells[inwrkrow, 57].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 57].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 58].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Base Dis"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Base Dis"))
                        {
                            worksheet.Cells[inwrkrow, 58].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 58].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        //worksheet.Cells[inwrkrow, 68].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fix Price"]);
                        //if (Array.Exists(NotMappedColumn, element => element.Trim() == "Fix Price"))
                        //{
                        //    worksheet.Cells[inwrkrow, 68].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //    worksheet.Cells[inwrkrow, 68].Style.Fill.BackgroundColor.SetColor(red_bg);
                        //}

                        worksheet.Cells[inwrkrow, 59].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cert No"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Cert No"))
                        {
                            worksheet.Cells[inwrkrow, 59].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 59].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        //worksheet.Cells[inwrkrow, 70].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Ref No"]);
                        //if (Array.Exists(NotMappedColumn, element => element.Trim() == "Ref No"))
                        //{
                        //    worksheet.Cells[inwrkrow, 70].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //    worksheet.Cells[inwrkrow, 70].Style.Fill.BackgroundColor.SetColor(red_bg);
                        //}

                        //worksheet.Cells[inwrkrow, 71].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Goods Type"]);
                        //if (Array.Exists(NotMappedColumn, element => element.Trim() == "Goods Type"))
                        //{
                        //    worksheet.Cells[inwrkrow, 71].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        //    worksheet.Cells[inwrkrow, 71].Style.Fill.BackgroundColor.SetColor(red_bg);
                        //}

                        worksheet.Cells[inwrkrow, 60].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Country of Origin"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Country of Origin"))
                        {
                            worksheet.Cells[inwrkrow, 60].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 60].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 61].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Girdle"))
                        {
                            worksheet.Cells[inwrkrow, 61].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 61].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 62].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["H&A"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "H&A"))
                        {
                            worksheet.Cells[inwrkrow, 62].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 62].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 63].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fls Color"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Fls Color"))
                        {
                            worksheet.Cells[inwrkrow, 63].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 63].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 64].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fancy Color"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Fancy Color"))
                        {
                            worksheet.Cells[inwrkrow, 64].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 64].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 65].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fancy Overtone"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Fancy Overtone"))
                        {
                            worksheet.Cells[inwrkrow, 65].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 65].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 66].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fancy Intensity"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Fancy Intensity"))
                        {
                            worksheet.Cells[inwrkrow, 66].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 66].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 67].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle To"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Girdle To"))
                        {
                            worksheet.Cells[inwrkrow, 67].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 67].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 68].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image URL 1"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Image URL 1"))
                        {
                            worksheet.Cells[inwrkrow, 68].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 68].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 69].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image URL 2"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Image URL 2"))
                        {
                            worksheet.Cells[inwrkrow, 69].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 69].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 70].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Video MP4"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Video MP4"))
                        {
                            worksheet.Cells[inwrkrow, 70].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 70].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        worksheet.Cells[inwrkrow, 71].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Stock From"]);
                        if (Array.Exists(NotMappedColumn, element => element.Trim() == "Stock From"))
                        {
                            worksheet.Cells[inwrkrow, 71].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            worksheet.Cells[inwrkrow, 71].Style.Fill.BackgroundColor.SetColor(red_bg);
                        }

                        inwrkrow++;

                        #endregion
                    }

                    worksheet.Cells[inStartIndex, 1, (inwrkrow - 1), 71].Style.Font.Size = 9; 

                    worksheet.Cells[inStartIndex, 2, (inwrkrow - 1), 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[inStartIndex, 2, (inwrkrow - 1), 2].Style.Font.Color.SetColor(red_font);
                    worksheet.Cells[inStartIndex, 2, (inwrkrow - 1), 2].Style.Font.Size = 10;
                    worksheet.Cells[inStartIndex, 2, (inwrkrow - 1), 2].Style.Font.Bold = true;



                    int rowEnd = worksheet.Dimension.End.Row;
                    removingGreenTagWarning(worksheet, worksheet.Cells[1, 1, rowEnd, 100].Address);

                    Byte[] bin = p.GetAsByteArray();

                    if (!Directory.Exists(_strFolderPath))
                    {
                        Directory.CreateDirectory(_strFolderPath);
                    }

                    System.IO.File.WriteAllBytes(_strFilePath, bin);

                }
            }
            catch (Exception ex)
            {
                Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }
    }
}
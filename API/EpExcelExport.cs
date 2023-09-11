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

                    worksheet.Cells[2, 1].Value = "Sr. No";
                    worksheet.Cells[2, 1].AutoFitColumns(7);
                    Row_Count += 1;

                    int k = 1;
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

                        worksheet.Cells[inwrkrow, 1].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["iSr"]);

                        int kk = 1;
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
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Status"]);

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(I);
                                }
                                else if (Column_Name == "Supplier Stone Id")
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
                                else if (Column_Name == "Certificate No")
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
                                else if (Column_Name == "Type 2A")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Type_2A"]);
                                }
                                else if (Column_Name == "Table Inclusion")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Inclusion"]);
                                }
                                else if (Column_Name == "Crown Inclusion")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Inclusion"]);
                                }
                                else if (Column_Name == "Table Natts")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Natts"]);
                                }
                                else if (Column_Name == "Crown Natts")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"]);
                                }
                                else if (Column_Name == "Culet")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Culet"]);
                                }
                                else if (Column_Name == "Lab Comments")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab_Comments"]);

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(BL);
                                }
                                else if (Column_Name == "Supplier Comments")
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

                    int kkk = 1;
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

                            if (Column_Name == "Supplier Stone Id")
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
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 2 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Supplier Base Offer Value($)'");
                                if (dra.Length > 0)
                                {
                                    Supplier_Base_Offer_Value_Doller = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
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
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 2 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Supplier Final Value($)'");
                                if (dra.Length > 0)
                                {
                                    Supplier_Final_Value_Doller = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
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
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 2 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Supplier Final Value With Max Slab($)'");
                                if (dra.Length > 0)
                                {
                                    Supplier_Final_Value_With_Max_Slab_Doller = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
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
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 2 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Bid Amt'");
                                if (dra.Length > 0)
                                {
                                    Bid_Amt = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
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
                                    Cts = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
                                    Cts = ((Image_Video_Certi != 0 && Cts > Image_Video_Certi) ? Cts + 2 : Cts);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Bid Amt'");
                                if (dra.Length > 0)
                                {
                                    Bid_Amt = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
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

                    worksheet.Cells[2, 1].Value = "Sr. No";
                    worksheet.Cells[2, 1].AutoFitColumns(7);
                    Row_Count += 1;

                    int k = 1;
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

                        int kk = 1;
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
                                else if (Column_Name == "Supplier Stone Id")
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
                                else if (Column_Name == "Certificate No")
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
                                else if (Column_Name == "Lab Comments")
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
                                else if (Column_Name == "Table Natts")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Natts"]);
                                }
                                else if (Column_Name == "Crown Natts")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"]);
                                }
                                else if (Column_Name == "Table Inclusion")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Inclusion"]);
                                }
                                else if (Column_Name == "Crown Inclusion")
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

                    int kkk = 1;
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

                            if (Column_Name == "Ref No")
                            {
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(102," + GetExcelColumnLetter(1) + "" + inStartIndex + ":" + GetExcelColumnLetter(1) + "" + (inwrkrow - 1) + "),2)";
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
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
                                    Rap_Amount = ((Image_Video != 0 && Rap_Amount > Image_Video) ? Rap_Amount + 1 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Supplier Cost Value($)'");
                                if (dra.Length > 0)
                                {
                                    Supplier_Cost_Value_Doller = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
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
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
                                    Rap_Amount = ((Image_Video != 0 && Rap_Amount > Image_Video) ? Rap_Amount + 1 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Sunrise Value US($)'");
                                if (dra.Length > 0)
                                {
                                    Sunrise_Value_US_Doller = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
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
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
                                    Rap_Amount = ((Image_Video != 0 && Rap_Amount > Image_Video) ? Rap_Amount + 1 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Supplier Base Offer Value($)'");
                                if (dra.Length > 0)
                                {
                                    Supplier_Base_Offer_Value_Doller = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
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

                    worksheet.Cells[2, 1].Value = "Sr. No";
                    worksheet.Cells[2, 1].AutoFitColumns(7);
                    Row_Count += 1;

                    int k = 1;
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

                        worksheet.Cells[inwrkrow, 1].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["iSr"]);

                        int kk = 1;
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
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Disc"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Disc"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Disc"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.00";
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(cellBg);
                                }
                                else if (Column_Name == "Offer Value($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Value"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Value"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Value"]) : ((Double?)null)) : null);

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
                                else if (Column_Name == "Lab Comments")
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
                                else if (Column_Name == "Table Natts")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Natts"]);
                                }
                                else if (Column_Name == "Crown Natts")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"]);
                                }
                                else if (Column_Name == "Table Inclusion")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Inclusion"]);
                                }
                                else if (Column_Name == "Crown Inclusion")
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

                    int kkk = 1;
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
                                worksheet.Cells[1, kkk].Formula = "ROUND(SUBTOTAL(102," + GetExcelColumnLetter(1) + "" + inStartIndex + ":" + GetExcelColumnLetter(1) + "" + (inwrkrow - 1) + "),2)";
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
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 2 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Offer Value($)'");
                                if (dra.Length > 0)
                                {
                                    Offer_Value_Doller = Convert.ToInt32(dra[0]["OrderBy"]) + 1;
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
    }
}
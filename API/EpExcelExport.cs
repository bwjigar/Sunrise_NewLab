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
        public static void Buyer_Excel(DataTable dtDiamonds, string _strFolderPath, string _strFilePath)
        {
            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
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
                    p.Workbook.Worksheets.Add("SearchStock");

                    ExcelWorksheet worksheet = p.Workbook.Worksheets[1];
                    worksheet.Name = DateTime.Now.ToString("dd-MM-yyyy");
                    worksheet.Cells.Style.Font.Size = 11;
                    worksheet.Cells.Style.Font.Name = "Calibri";
                    //worksheet.Cells[1, 3, 3, 12].Style.Font.Bold = true;

                    //worksheet.Cells[1, 6].Value = "SUNRISE DIAMONDS INVENTORY FOR THE DATE " + " " + DateTime.Now.ToString("dd-MM-yyyy");
                    //worksheet.Cells[1, 6].Style.Font.Size = 24;
                    //worksheet.Cells[1, 6].Style.Font.Bold = true;

                    Color colFromHex_H1 = System.Drawing.ColorTranslator.FromHtml("#8497b0");
                    //worksheet.Cells[1, 6].Style.Font.Color.SetColor(colFromHex_H1);
                    Color col_color_Red = System.Drawing.ColorTranslator.FromHtml("#ff0000");

                    //worksheet.Row(5).Height = 40;
                    //worksheet.Row(6).Height = 40;
                    //worksheet.Row(6).Style.WrapText = true;
                    worksheet.Row(1).Height = 40;
                    worksheet.Row(2).Height = 40;
                    worksheet.Row(2).Style.WrapText = true;

                    //worksheet.Cells[2, 2].Value = "All Prices are final Selling Cash Price";
                    //worksheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[2, 2].Style.Font.Size = 11;
                    //worksheet.Cells[2, 2].Style.Font.Bold = true;
                    //worksheet.Cells[2, 2].Style.Font.Color.SetColor(col_color_Red);

                    //worksheet.Cells[2, 6].Value = "UNIT 1, 14/F, PENINSULA SQUARE, EAST WING, 18 SUNG ON STREET, HUNG HOM, KOWLOON, HONG KONG TEL : +852 - 27235100    FAX : +852 - 2314 9100";
                    //worksheet.Cells[2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[2, 6].Style.Font.Size = 11;
                    //worksheet.Cells[2, 6].Style.Font.Bold = true;
                    //worksheet.Cells[2, 6].Style.Font.Color.SetColor(colFromHex_H1);

                    //worksheet.Cells[3, 6].Value = "Email Id : sales@sunrisediam.com    Web : www.sunrisediamonds.com.hk . Download Apps on Android, IOS and Windows";
                    //worksheet.Cells[3, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[3, 6].Style.Font.Size = 11;
                    //worksheet.Cells[3, 6].Style.Font.Bold = true;
                    //worksheet.Cells[3, 6].Style.Font.Color.SetColor(colFromHex_H1);

                    //worksheet.Cells[4, 2].Value = "Table & Crown Inclusion = White Inclusion";
                    //worksheet.Cells[4, 2, 4, 5].Merge = true;
                    //worksheet.Cells[4, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[4, 2].Style.Font.Size = 9;
                    //worksheet.Cells[4, 6].Value = "Table & Crown Natts = Black Inclusion";
                    //worksheet.Cells[4, 6, 4, 9].Merge = true;
                    //worksheet.Cells[4, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[4, 6].Style.Font.Size = 9;

                    worksheet.Cells[1, 1].Value = "Total";
                    worksheet.Cells[1, 1, 1, 70].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, 1, 70].Style.Font.Size = 11;
                    worksheet.Cells[1, 1, 1, 70].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, 1, 70].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[1, 1, 1, 70].Style.Font.Size = 11;

                    worksheet.Cells[2, 1, 2, 70].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[2, 1, 2, 70].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    worksheet.Cells[2, 1, 2, 70].Style.Font.Size = 10;
                    worksheet.Cells[2, 1, 2, 70].Style.Font.Bold = true;

                    worksheet.Cells[2, 1, 2, 70].AutoFilter = true;
                    //worksheet.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[3, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    var cellBackgroundColor1 = worksheet.Cells[2, 1, 2, 70].Style.Fill;
                    cellBackgroundColor1.PatternType = ExcelFillStyle.Solid;
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                    cellBackgroundColor1.BackgroundColor.SetColor(colFromHex);

                    #endregion

                    #region Header Name Declaration

                    worksheet.Cells[2, 1].Value = "Sr. No";
                    worksheet.Cells[2, 2].Value = "Image";
                    worksheet.Cells[2, 3].Value = "Video";
                    worksheet.Cells[2, 4].Value = "Certi";
                    worksheet.Cells[2, 5].Value = "Lab";
                    worksheet.Cells[2, 6].Value = "Supplier Name";
                    worksheet.Cells[2, 7].Value = "Rank";
                    worksheet.Cells[2, 8].Value = "Supplier Status";
                    worksheet.Cells[2, 9].Value = "Buyer Name";
                    worksheet.Cells[2, 10].Value = "Status";
                    worksheet.Cells[2, 11].Value = "Supplier Stone Id";
                    worksheet.Cells[2, 12].Value = "Certificate No";
                    worksheet.Cells[2, 13].Value = "Shape";
                    worksheet.Cells[2, 14].Value = "Pointer";
                    worksheet.Cells[2, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[2, 14].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);
                    worksheet.Cells[2, 15].Value = "Sub Pointer";
                    worksheet.Cells[2, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[2, 15].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);
                    worksheet.Cells[2, 16].Value = "Color";
                    worksheet.Cells[2, 17].Value = "Clarity";
                    worksheet.Cells[2, 18].Value = "Cts";
                    worksheet.Cells[2, 19].Value = "Rap Rate($)";
                    worksheet.Cells[2, 20].Value = "Rap Amount($)";
                    worksheet.Cells[2, 21].Value = "Supplier Base Offer(%)";
                    worksheet.Cells[2, 22].Value = "Supplier Base Offer Value($)";
                    worksheet.Cells[2, 23].Value = "Supplier Final Disc(%)";
                    worksheet.Cells[2, 24].Value = "Supplier Final Value($)";
                    worksheet.Cells[2, 25].Value = "Supplier Final Disc. With Max Slab(%)";
                    worksheet.Cells[2, 26].Value = "Supplier Final Value With Max Slab($)";
                    worksheet.Cells[2, 27].Value = "Bid Disc(%)";
                    worksheet.Cells[2, 28].Value = "Bid Amt";
                    worksheet.Cells[2, 29].Value = "Bid/Ct";
                    worksheet.Cells[2, 30].Value = "Avg. Stock Disc(%)";
                    worksheet.Cells[2, 31].Value = "Avg. Stock Pcs";
                    worksheet.Cells[2, 32].Value = "Avg. Pur. Disc(%)";
                    worksheet.Cells[2, 33].Value = "Avg. Pur. Pcs";
                    worksheet.Cells[2, 34].Value = "Avg. Sales Disc(%)";
                    worksheet.Cells[2, 35].Value = "Sales Pcs";
                    worksheet.Cells[2, 36].Value = "KTS Grade";
                    worksheet.Cells[2, 37].Value = "Comm. Grade";
                    worksheet.Cells[2, 38].Value = "Zone";
                    worksheet.Cells[2, 39].Value = "Para. Grade";
                    worksheet.Cells[2, 40].Value = "Cut";
                    worksheet.Cells[2, 41].Value = "Polish";
                    worksheet.Cells[2, 42].Value = "Symm";
                    worksheet.Cells[2, 43].Value = "Fls";
                    worksheet.Cells[2, 44].Value = "Key To Symbol";
                    worksheet.Cells[2, 45].Value = "Ratio";
                    worksheet.Cells[2, 46].Value = "Length";
                    worksheet.Cells[2, 47].Value = "Width";
                    worksheet.Cells[2, 48].Value = "Depth";
                    worksheet.Cells[2, 49].Value = "Depth(%)";
                    worksheet.Cells[2, 50].Value = "Table(%)";
                    worksheet.Cells[2, 51].Value = "Crown Angle";
                    worksheet.Cells[2, 52].Value = "Crown Height";
                    worksheet.Cells[2, 53].Value = "Pavilion Angle";
                    worksheet.Cells[2, 54].Value = "Pavilion Height";
                    worksheet.Cells[2, 55].Value = "Girdle(%)";
                    worksheet.Cells[2, 56].Value = "Luster";
                    worksheet.Cells[2, 57].Value = "Type 2A";
                    worksheet.Cells[2, 58].Value = "Table Inclusion";
                    worksheet.Cells[2, 59].Value = "Crown Inclusion";
                    worksheet.Cells[2, 60].Value = "Table Natts";
                    worksheet.Cells[2, 61].Value = "Crown Natts";
                    worksheet.Cells[2, 62].Value = "Culet";
                    worksheet.Cells[2, 63].Value = "Lab Comments";
                    worksheet.Cells[2, 64].Value = "Supplier Comments";
                    worksheet.Cells[2, 65].Value = "Table Open";
                    worksheet.Cells[2, 66].Value = "Crown Open";
                    worksheet.Cells[2, 67].Value = "Pav Open";
                    worksheet.Cells[2, 68].Value = "Girdle Open";
                    worksheet.Cells[2, 69].Value = "Shade";
                    worksheet.Cells[2, 70].Value = "Milky";

                    ExcelStyle cellStyleHeader1 = worksheet.Cells[2, 1, 2, 70].Style;
                    cellStyleHeader1.Border.Left.Style = cellStyleHeader1.Border.Right.Style
                            = cellStyleHeader1.Border.Top.Style = cellStyleHeader1.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    #endregion

                    #region Set AutoFit and Decimal Number Format

                    worksheet.View.FreezePanes(3, 1);

                    worksheet.Cells[2, 1].AutoFitColumns(7);                     //"Sr. No";
                    worksheet.Cells[2, 2].AutoFitColumns(8);                        //"Image";
                    worksheet.Cells[2, 3].AutoFitColumns(8);                        //"Video";
                    worksheet.Cells[2, 4].AutoFitColumns(8);                        //"Certi";
                    worksheet.Cells[2, 5].AutoFitColumns(6);                       //"Lab";
                    worksheet.Cells[2, 6].AutoFitColumns(30);                    //"Supplier Name";
                    worksheet.Cells[2, 7].AutoFitColumns(6);                     //"Rank";
                    worksheet.Cells[2, 8].AutoFitColumns(10);                     //"Supplier Status";
                    worksheet.Cells[2, 9].AutoFitColumns(30);                        //"Buyer Name";
                    worksheet.Cells[2, 10].AutoFitColumns(6);              // "Status";
                    worksheet.Cells[2, 11].AutoFitColumns(14);              // "Supplier Stone Id";
                    worksheet.Cells[2, 12].AutoFitColumns(14);                    // "Certificate No";
                    worksheet.Cells[2, 13].AutoFitColumns(11);                    // "Shape";
                    worksheet.Cells[2, 14].AutoFitColumns(10);                    // "Pointer";
                    worksheet.Cells[2, 15].AutoFitColumns(10);                    // "Sub Pointer";
                    worksheet.Cells[2, 16].AutoFitColumns(8);                    // "Color";
                    worksheet.Cells[2, 17].AutoFitColumns(8);                      // "Clarity";
                    worksheet.Cells[2, 18].AutoFitColumns(11);                      // "Cts";
                    worksheet.Cells[2, 19].AutoFitColumns(13);                   // "Rap Rate($)";
                    worksheet.Cells[2, 20].AutoFitColumns(13);                      // "Rap Amount($)";
                    worksheet.Cells[2, 21].AutoFitColumns(13);                       // "Supplier Base Offer(%)";
                    worksheet.Cells[2, 22].AutoFitColumns(13);                    // "Supplier Base Offer Value($)";
                    worksheet.Cells[2, 23].AutoFitColumns(13);                    // "Supplier Final Disc(%)";
                    worksheet.Cells[2, 24].AutoFitColumns(13);                    // "Supplier Final Value($)";
                    worksheet.Cells[2, 25].AutoFitColumns(13);                    // "Supplier Final Disc. With Max Slab(%)";
                    worksheet.Cells[2, 26].AutoFitColumns(13);                    // "Supplier Final Value With Max Slab($)";
                    worksheet.Cells[2, 27].AutoFitColumns(13);                    // "Bid Disc(%)";
                    worksheet.Cells[2, 28].AutoFitColumns(13);                    // "Bid Amt";
                    worksheet.Cells[2, 29].AutoFitColumns(13);                    // "Bid/Ct";
                    worksheet.Cells[2, 30].AutoFitColumns(13);                       // "Avg. Stock Disc(%)";
                    worksheet.Cells[2, 31].AutoFitColumns(9);                     // "Avg. Stock Pcs";
                    worksheet.Cells[2, 32].AutoFitColumns(13);                   // "Avg. Pur. Disc(%)";
                    worksheet.Cells[2, 33].AutoFitColumns(9);                      // "Avg. Pur. Pcs";
                    worksheet.Cells[2, 34].AutoFitColumns(13);                    // "Avg. Sales Disc(%)";
                    worksheet.Cells[2, 35].AutoFitColumns(9);                    // "Sales Pcs";
                    worksheet.Cells[2, 36].AutoFitColumns(8);                    // "KTS Grade";
                    worksheet.Cells[2, 37].AutoFitColumns(8);                    // "Comm. Grade";
                    worksheet.Cells[2, 38].AutoFitColumns(8);                    // "Zone";
                    worksheet.Cells[2, 39].AutoFitColumns(8);                    // "Para. Grade";
                    worksheet.Cells[2, 40].AutoFitColumns(8);                    // "Cut";
                    worksheet.Cells[2, 41].AutoFitColumns(8);                    // "Polish";
                    worksheet.Cells[2, 42].AutoFitColumns(8);                    // "Symm";
                    worksheet.Cells[2, 43].AutoFitColumns(8);                    // "Fls";
                    worksheet.Cells[2, 44].AutoFitColumns(30);                    // "Key To Symbol";
                    worksheet.Cells[2, 45].AutoFitColumns(8);                    // "Ratio";
                    worksheet.Cells[2, 46].AutoFitColumns(8);                    // "Length";
                    worksheet.Cells[2, 47].AutoFitColumns(8);                    // "Width";
                    worksheet.Cells[2, 48].AutoFitColumns(8);                    // "Depth";
                    worksheet.Cells[2, 49].AutoFitColumns(8);                    // "Depth(%)";
                    worksheet.Cells[2, 50].AutoFitColumns(8);                    // "Table(%)";
                    worksheet.Cells[2, 51].AutoFitColumns(8);                    // "Crown Angle";
                    worksheet.Cells[2, 52].AutoFitColumns(8);                    // "Crown Height";
                    worksheet.Cells[2, 53].AutoFitColumns(8);                    // "Pavilion Angle";
                    worksheet.Cells[2, 54].AutoFitColumns(8);                    // "Pavilion Height";
                    worksheet.Cells[2, 55].AutoFitColumns(8);                    // "Girdle(%)";
                    worksheet.Cells[2, 56].AutoFitColumns(8);                    // "Luster";
                    worksheet.Cells[2, 57].AutoFitColumns(8);                    // "Type 2A";
                    worksheet.Cells[2, 58].AutoFitColumns(8);                    // "Table Inclusion";
                    worksheet.Cells[2, 59].AutoFitColumns(8);                    // "Crown Inclusion";
                    worksheet.Cells[2, 60].AutoFitColumns(8);                    // "Table Natts";
                    worksheet.Cells[2, 61].AutoFitColumns(8);                    // "Crown Natts";
                    worksheet.Cells[2, 62].AutoFitColumns(8);                    // "Culet";
                    worksheet.Cells[2, 63].AutoFitColumns(30);                    // "Lab Comments";
                    worksheet.Cells[2, 64].AutoFitColumns(30);                    // "Supplier Comments";
                    worksheet.Cells[2, 65].AutoFitColumns(8);                    // "Table Open";
                    worksheet.Cells[2, 66].AutoFitColumns(8);                    // "Crown Open";
                    worksheet.Cells[2, 67].AutoFitColumns(8);                    // "Pav Open";
                    worksheet.Cells[2, 68].AutoFitColumns(8);                    // "Girdle Open";
                    worksheet.Cells[2, 69].AutoFitColumns(8);                    // "Shade";
                    worksheet.Cells[2, 70].AutoFitColumns(8);                    // "Milky";


                    //Set Cell Faoat value with Alignment
                    worksheet.Cells[inStartIndex, 1, inEndCounter, 70].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    #endregion
                    var asTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo;


                    for (i = inStartIndex; i < inEndCounter; i++)
                    {
                        #region Assigns Value to Cell

                        worksheet.Cells[inwrkrow, 1].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["iSr"]);

                        string Image_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image_URL"]);
                        if (!string.IsNullOrEmpty(Image_URL))
                        {
                            worksheet.Cells[inwrkrow, 2].Formula = "=HYPERLINK(\"" + Image_URL + "\",\" Image \")";
                            worksheet.Cells[inwrkrow, 2].Style.Font.UnderLine = true;
                            worksheet.Cells[inwrkrow, 2].Style.Font.Color.SetColor(Color.Blue);
                        }

                        string Video_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Video_URL"]);
                        if (!string.IsNullOrEmpty(Video_URL))
                        {
                            worksheet.Cells[inwrkrow, 3].Formula = "=HYPERLINK(\"" + Video_URL + "\",\" Video \")";
                            worksheet.Cells[inwrkrow, 3].Style.Font.UnderLine = true;
                            worksheet.Cells[inwrkrow, 3].Style.Font.Color.SetColor(Color.Blue);
                        }

                        string Certificate_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_URL"]);
                        if (!string.IsNullOrEmpty(Certificate_URL))
                        {
                            worksheet.Cells[inwrkrow, 4].Formula = "=HYPERLINK(\"" + Certificate_URL + "\",\" Certi \")";
                            worksheet.Cells[inwrkrow, 4].Style.Font.UnderLine = true;
                            worksheet.Cells[inwrkrow, 4].Style.Font.Color.SetColor(Color.Blue);
                        }

                        worksheet.Cells[inwrkrow, 5].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]);
                        worksheet.Cells[inwrkrow, 6].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["SupplierName"]);
                        worksheet.Cells[inwrkrow, 7].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Rank"]);
                        worksheet.Cells[inwrkrow, 8].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Supplier_Status"]);
                        worksheet.Cells[inwrkrow, 9].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Buyer_Name"]);
                        worksheet.Cells[inwrkrow, 10].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Status"]);

                        string Supplier_Stone_Id = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Supplier_Stone_Id"]);
                        success1 = Int64.TryParse(Supplier_Stone_Id, out number_1);
                        if (success1)
                        {
                            worksheet.Cells[inwrkrow, 11].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Supplier_Stone_Id"]);
                        }
                        else
                        {
                            worksheet.Cells[inwrkrow, 11].Value = Supplier_Stone_Id;
                        }

                        string Certificate_No = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                        success1 = Int64.TryParse(Certificate_No, out number_1);
                        if (success1)
                        {
                            worksheet.Cells[inwrkrow, 12].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                        }
                        else
                        {
                            worksheet.Cells[inwrkrow, 12].Value = Certificate_No;
                        }

                        worksheet.Cells[inwrkrow, 13].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shape"]);
                        worksheet.Cells[inwrkrow, 14].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pointer"]);
                        worksheet.Cells[inwrkrow, 15].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Sub_Pointer"]);
                        worksheet.Cells[inwrkrow, 16].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Color"]);
                        worksheet.Cells[inwrkrow, 17].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Clarity"]);

                        worksheet.Cells[inwrkrow, 18].Value = ((dtDiamonds.Rows[i - inStartIndex]["Cts"] != null) ?
                               (dtDiamonds.Rows[i - inStartIndex]["Cts"].GetType().Name != "DBNull" ?
                               Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Cts"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 19].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"] != null) ?
                               (dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"].GetType().Name != "DBNull" ?
                               Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 20].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"] != null) ?
                             (dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"].GetType().Name != "DBNull" ?
                             Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 21].Value = ((dtDiamonds.Rows[i - inStartIndex]["Disc"] != null) ?
                             (dtDiamonds.Rows[i - inStartIndex]["Disc"].GetType().Name != "DBNull" ?
                             Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Disc"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 22].Value = ((dtDiamonds.Rows[i - inStartIndex]["Value"] != null) ?
                             (dtDiamonds.Rows[i - inStartIndex]["Value"].GetType().Name != "DBNull" ?
                             Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Value"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 23].Value = ((dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"] != null) ?
                             (dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"].GetType().Name != "DBNull" ?
                             Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 24].Value = ((dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"] != null) ?
                             (dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"].GetType().Name != "DBNull" ?
                             Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 25].Value = ((dtDiamonds.Rows[i - inStartIndex]["MAX_DISC"] != null) ?
                             (dtDiamonds.Rows[i - inStartIndex]["MAX_DISC"].GetType().Name != "DBNull" ?
                             Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["MAX_DISC"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 26].Value = ((dtDiamonds.Rows[i - inStartIndex]["MAX_VALUE"] != null) ?
                             (dtDiamonds.Rows[i - inStartIndex]["MAX_VALUE"].GetType().Name != "DBNull" ?
                             Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["MAX_VALUE"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 27].Value = ((dtDiamonds.Rows[i - inStartIndex]["Bid_Disc"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Bid_Disc"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Bid_Disc"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 28].Value = ((dtDiamonds.Rows[i - inStartIndex]["Bid_Amt"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Bid_Amt"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Bid_Amt"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 29].Value = ((dtDiamonds.Rows[i - inStartIndex]["Bid_Ct"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Bid_Ct"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Bid_Ct"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 30].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Disc"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Disc"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Disc"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 31].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Pcs"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Pcs"].GetType().Name != "DBNull" ?
                           Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Pcs"]) : ((Int64?)null)) : null);

                        worksheet.Cells[inwrkrow, 32].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Disc"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Disc"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Disc"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 33].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Pcs"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Pcs"].GetType().Name != "DBNull" ?
                           Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Pcs"]) : ((Int64?)null)) : null);

                        worksheet.Cells[inwrkrow, 34].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Sales_Disc"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Avg_Sales_Disc"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Avg_Sales_Disc"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 35].Value = ((dtDiamonds.Rows[i - inStartIndex]["Sales_Pcs"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Sales_Pcs"].GetType().Name != "DBNull" ?
                           Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Sales_Pcs"]) : ((Int64?)null)) : null);


                        worksheet.Cells[inwrkrow, 36].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["KTS_Grade"]);
                        worksheet.Cells[inwrkrow, 37].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Comm_Grade"]);
                        worksheet.Cells[inwrkrow, 38].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Zone"]);
                        worksheet.Cells[inwrkrow, 39].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Para_Grade"]);

                        worksheet.Cells[inwrkrow, 40].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]);
                        worksheet.Cells[inwrkrow, 41].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Polish"]);
                        worksheet.Cells[inwrkrow, 42].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Symm"]);

                        if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]).ToUpper() == "3EX")
                        {
                            worksheet.Cells[inwrkrow, 40].Style.Font.Bold = true;
                            worksheet.Cells[inwrkrow, 41].Style.Font.Bold = true;
                            worksheet.Cells[inwrkrow, 42].Style.Font.Bold = true;
                        }

                        worksheet.Cells[inwrkrow, 43].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fls"]);
                        worksheet.Cells[inwrkrow, 44].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Key_To_Symboll"]);

                        worksheet.Cells[inwrkrow, 45].Value = ((dtDiamonds.Rows[i - inStartIndex]["RATIO"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["RATIO"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["RATIO"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 46].Value = ((dtDiamonds.Rows[i - inStartIndex]["Length"] != null) ?
                            (dtDiamonds.Rows[i - inStartIndex]["Length"].GetType().Name != "DBNull" ?
                            Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Length"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 47].Value = ((dtDiamonds.Rows[i - inStartIndex]["Width"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Width"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Width"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 48].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Depth"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 49].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth_Per"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Depth_Per"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth_Per"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 50].Value = ((dtDiamonds.Rows[i - inStartIndex]["Table_Per"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Table_Per"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Table_Per"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 51].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 52].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Height"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Crown_Height"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Height"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 53].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 54].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Height"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Pav_Height"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Height"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 55].Value = ((dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 56].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Luster"]);
                        worksheet.Cells[inwrkrow, 57].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Type_2A"]);
                        worksheet.Cells[inwrkrow, 58].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Inclusion"]);
                        worksheet.Cells[inwrkrow, 59].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Inclusion"]);
                        worksheet.Cells[inwrkrow, 60].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Natts"]);
                        worksheet.Cells[inwrkrow, 61].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"]);
                        worksheet.Cells[inwrkrow, 62].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Culet"]);
                        worksheet.Cells[inwrkrow, 63].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab_Comments"]);
                        worksheet.Cells[inwrkrow, 64].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Supplier_Comments"]);
                        worksheet.Cells[inwrkrow, 65].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Open"]);
                        worksheet.Cells[inwrkrow, 66].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Open"]);
                        worksheet.Cells[inwrkrow, 67].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pav_Open"]);
                        worksheet.Cells[inwrkrow, 68].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle_Open"]);
                        worksheet.Cells[inwrkrow, 69].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shade"]);
                        worksheet.Cells[inwrkrow, 70].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Milky"]);

                        inwrkrow++;

                        #endregion
                    }

                    worksheet.Cells[inStartIndex, 1, (inwrkrow - 1), 70].Style.Font.Size = 9;
                    worksheet.Cells[inStartIndex, 18, (inwrkrow - 1), 20].Style.Numberformat.Format = "#,##0.00";
                    worksheet.Cells[inStartIndex, 21, (inwrkrow - 1), 30].Style.Numberformat.Format = "#,##0.0000";
                    worksheet.Cells[inStartIndex, 32, (inwrkrow - 1), 32].Style.Numberformat.Format = "#,##0.0000";
                    worksheet.Cells[inStartIndex, 34, (inwrkrow - 1), 34].Style.Numberformat.Format = "#,##0.0000";

                    worksheet.Cells[inStartIndex, 14, (inwrkrow - 1), 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[inStartIndex, 14, (inwrkrow - 1), 15].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);

                    worksheet.Cells[inStartIndex, 21, (inwrkrow - 1), 26].Style.Font.Bold = true;
                    worksheet.Cells[inStartIndex, 21, (inwrkrow - 1), 26].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[inStartIndex, 21, (inwrkrow - 1), 26].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                    worksheet.Cells[inStartIndex, 21, (inwrkrow - 1), 26].Style.Font.Color.SetColor(System.Drawing.Color.Red);

                    worksheet.Cells[inStartIndex, 45, (inwrkrow - 1), 50].Style.Numberformat.Format = "0.00";
                    worksheet.Cells[inStartIndex, 51, (inwrkrow - 1), 54].Style.Numberformat.Format = "0.00";
                    worksheet.Cells[inStartIndex, 55, (inwrkrow - 1), 55].Style.Numberformat.Format = "0.00";

                    worksheet.Cells[1, 12].Formula = "ROUND(SUBTOTAL(102,A" + inStartIndex + ":A" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[1, 12].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 12].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[1, 12].Style.Numberformat.Format = "#,##";

                    ExcelStyle cellStyleHeader_Total = worksheet.Cells[1, 12].Style;
                    cellStyleHeader_Total.Border.Left.Style = cellStyleHeader_Total.Border.Right.Style
                            = cellStyleHeader_Total.Border.Top.Style = cellStyleHeader_Total.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[1, 18].Formula = "ROUND(SUBTOTAL(109,R" + inStartIndex + ":R" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[1, 18].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 18].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[1, 18].Style.Numberformat.Format = "#,##0.00";

                    ExcelStyle cellStyleHeader_Totalcarat = worksheet.Cells[1, 18].Style;
                    cellStyleHeader_Totalcarat.Border.Left.Style = cellStyleHeader_Totalcarat.Border.Right.Style
                            = cellStyleHeader_Totalcarat.Border.Top.Style = cellStyleHeader_Totalcarat.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[1, 21].Formula = "IF(SUBTOTAL(109,T" + inStartIndex + ": T" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109,V" + inStartIndex + ":V" + (inwrkrow - 1) + ")/SUBTOTAL(109,T" + inStartIndex + ":T" + (inwrkrow - 1) + ")))*(-100),2))";
                    worksheet.Cells[1, 21].Style.Numberformat.Format = "#,##0.00";

                    ExcelStyle cellStyleHeader_TotalDis = worksheet.Cells[1, 21].Style;
                    cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                            = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[1, 22].Formula = "ROUND(SUBTOTAL(109,V" + inStartIndex + ":V" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[1, 22].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 22].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[1, 22].Style.Numberformat.Format = "#,##0";

                    ExcelStyle cellStyleHeader_TotalAmt = worksheet.Cells[1, 22].Style;
                    cellStyleHeader_TotalAmt.Border.Left.Style = cellStyleHeader_TotalAmt.Border.Right.Style
                            = cellStyleHeader_TotalAmt.Border.Top.Style = cellStyleHeader_TotalAmt.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[1, 23].Formula = "IF(SUBTOTAL(109,T" + inStartIndex + ": T" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109,X" + inStartIndex + ":X" + (inwrkrow - 1) + ")/SUBTOTAL(109,T" + inStartIndex + ":T" + (inwrkrow - 1) + ")))*(-100),2))";
                    worksheet.Cells[1, 23].Style.Numberformat.Format = "#,##0.00";

                    ExcelStyle cellStyleHeader_TotalDis_1 = worksheet.Cells[1, 23].Style;
                    cellStyleHeader_TotalDis_1.Border.Left.Style = cellStyleHeader_TotalDis_1.Border.Right.Style
                            = cellStyleHeader_TotalDis_1.Border.Top.Style = cellStyleHeader_TotalDis_1.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[1, 24].Formula = "ROUND(SUBTOTAL(109,X" + inStartIndex + ":X" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[1, 24].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 24].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[1, 24].Style.Numberformat.Format = "#,##0";

                    ExcelStyle cellStyleHeader_TotalAmt_1 = worksheet.Cells[1, 24].Style;
                    cellStyleHeader_TotalAmt_1.Border.Left.Style = cellStyleHeader_TotalAmt_1.Border.Right.Style
                            = cellStyleHeader_TotalAmt_1.Border.Top.Style = cellStyleHeader_TotalAmt_1.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[1, 25].Formula = "IF(SUBTOTAL(109,T" + inStartIndex + ": T" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109,Z" + inStartIndex + ":Z" + (inwrkrow - 1) + ")/SUBTOTAL(109,T" + inStartIndex + ":T" + (inwrkrow - 1) + ")))*(-100),2))";
                    worksheet.Cells[1, 25].Style.Numberformat.Format = "#,##0.00";

                    ExcelStyle cellStyleHeader_TotalDis_2 = worksheet.Cells[1, 25].Style;
                    cellStyleHeader_TotalDis_2.Border.Left.Style = cellStyleHeader_TotalDis_2.Border.Right.Style
                            = cellStyleHeader_TotalDis_2.Border.Top.Style = cellStyleHeader_TotalDis_2.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[1, 26].Formula = "ROUND(SUBTOTAL(109,Z" + inStartIndex + ":Z" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[1, 26].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 26].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[1, 26].Style.Numberformat.Format = "#,##0";

                    ExcelStyle cellStyleHeader_TotalAmt_2 = worksheet.Cells[1, 26].Style;
                    cellStyleHeader_TotalAmt_2.Border.Left.Style = cellStyleHeader_TotalAmt_2.Border.Right.Style
                            = cellStyleHeader_TotalAmt_2.Border.Top.Style = cellStyleHeader_TotalAmt_2.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

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
        public static void Buyer_Excel_New(DataTable dtDiamonds, DataTable Col_dt, string _strFolderPath, string _strFilePath)
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
                            worksheet.Cells[2, k].AutoFitColumns(8);

                            k += 1;
                            worksheet.Cells[2, k].Value = "Video";
                            worksheet.Cells[2, k].AutoFitColumns(8);

                            k += 1;
                            worksheet.Cells[2, k].Value = "Certi";
                            worksheet.Cells[2, k].AutoFitColumns(8);
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
                                }
                                else if (Column_Name == "Supplier Name")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["SupplierName"]);
                                }
                                else if (Column_Name == "Rank")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Rank"]);
                                }
                                else if (Column_Name == "Supplier Status")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Supplier_Status"]);
                                }
                                else if (Column_Name == "Buyer Name")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Buyer_Name"]);
                                }
                                else if (Column_Name == "Status")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Status"]);
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

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);
                                }
                                else if (Column_Name == "Sub Pointer")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Sub_Pointer"]);

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);
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

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";

                                    worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                }
                                else if (Column_Name == "Supplier Base Offer Value($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Value"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Value"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Value"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";

                                    worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                }
                                else if (Column_Name == "Supplier Final Disc(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";

                                    worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                }
                                else if (Column_Name == "Supplier Final Value($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";

                                    worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                }
                                else if (Column_Name == "Supplier Final Disc. With Max Slab(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["MAX_DISC"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["MAX_DISC"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["MAX_DISC"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";

                                    worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                }
                                else if (Column_Name == "Supplier Final Value With Max Slab($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["MAX_VALUE"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["MAX_VALUE"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["MAX_VALUE"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";

                                    worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                }
                                else if (Column_Name == "Bid Disc(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Bid_Disc"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Bid_Disc"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Bid_Disc"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";
                                }
                                else if (Column_Name == "Bid Amt")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Bid_Amt"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Bid_Amt"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Bid_Amt"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";
                                }
                                else if (Column_Name == "Bid/Ct")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Bid_Ct"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Bid_Ct"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Bid_Ct"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";
                                }
                                else if (Column_Name == "Avg. Stock Disc(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Disc"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Disc"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Disc"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";
                                }
                                else if (Column_Name == "Avg. Stock Pcs")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Pcs"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Pcs"].GetType().Name != "DBNull" ?
                                      Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Avg_Stock_Pcs"]) : ((Int64?)null)) : null);
                                }
                                else if (Column_Name == "Avg. Pur. Disc(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Disc"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Disc"].GetType().Name != "DBNull" ?
                                      Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Disc"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";
                                }
                                else if (Column_Name == "Avg. Pur. Pcs")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Pcs"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Pcs"].GetType().Name != "DBNull" ?
                                      Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Avg_Pur_Pcs"]) : ((Int64?)null)) : null);
                                }
                                else if (Column_Name == "Avg. Sales Disc(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Avg_Sales_Disc"] != null) ?
                                        (dtDiamonds.Rows[i - inStartIndex]["Avg_Sales_Disc"].GetType().Name != "DBNull" ?
                                        Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Avg_Sales_Disc"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";
                                }
                                else if (Column_Name == "Sales Pcs")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Sales_Pcs"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Sales_Pcs"].GetType().Name != "DBNull" ?
                                     Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Sales_Pcs"]) : ((Int64?)null)) : null);
                                }
                                else if (Column_Name == "KTS Grade")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["KTS_Grade"]);
                                }
                                else if (Column_Name == "Comm. Grade")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Comm_Grade"]);
                                }
                                else if (Column_Name == "Zone")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Zone"]);
                                }
                                else if (Column_Name == "Para. Grade")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Para_Grade"]);
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

                            if (Column_Name == "Certificate No")
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
        public static void Supplier_Excel(DataTable dtDiamonds, string _strFolderPath, string _strFilePath)
        {
            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
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
                    p.Workbook.Worksheets.Add("SearchStock");

                    ExcelWorksheet worksheet = p.Workbook.Worksheets[1];
                    worksheet.Name = DateTime.Now.ToString("dd-MM-yyyy");
                    worksheet.Cells.Style.Font.Size = 11;
                    worksheet.Cells.Style.Font.Name = "Calibri";
                    //worksheet.Cells[1, 3, 3, 12].Style.Font.Bold = true;

                    //worksheet.Cells[1, 6].Value = "SUNRISE DIAMONDS INVENTORY FOR THE DATE " + " " + DateTime.Now.ToString("dd-MM-yyyy");
                    //worksheet.Cells[1, 6].Style.Font.Size = 24;
                    //worksheet.Cells[1, 6].Style.Font.Bold = true;

                    Color colFromHex_H1 = System.Drawing.ColorTranslator.FromHtml("#8497b0");
                    //worksheet.Cells[1, 6].Style.Font.Color.SetColor(colFromHex_H1);
                    Color col_color_Red = System.Drawing.ColorTranslator.FromHtml("#ff0000");

                    //worksheet.Row(5).Height = 40;
                    //worksheet.Row(6).Height = 40;
                    //worksheet.Row(6).Style.WrapText = true;
                    worksheet.Row(1).Height = 40;
                    worksheet.Row(2).Height = 40;
                    worksheet.Row(2).Style.WrapText = true;

                    //worksheet.Cells[2, 2].Value = "All Prices are final Selling Cash Price";
                    //worksheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[2, 2].Style.Font.Size = 11;
                    //worksheet.Cells[2, 2].Style.Font.Bold = true;
                    //worksheet.Cells[2, 2].Style.Font.Color.SetColor(col_color_Red);

                    //worksheet.Cells[2, 6].Value = "UNIT 1, 14/F, PENINSULA SQUARE, EAST WING, 18 SUNG ON STREET, HUNG HOM, KOWLOON, HONG KONG TEL : +852 - 27235100    FAX : +852 - 2314 9100";
                    //worksheet.Cells[2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[2, 6].Style.Font.Size = 11;
                    //worksheet.Cells[2, 6].Style.Font.Bold = true;
                    //worksheet.Cells[2, 6].Style.Font.Color.SetColor(colFromHex_H1);

                    //worksheet.Cells[3, 6].Value = "Email Id : sales@sunrisediam.com    Web : www.sunrisediamonds.com.hk . Download Apps on Android, IOS and Windows";
                    //worksheet.Cells[3, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[3, 6].Style.Font.Size = 11;
                    //worksheet.Cells[3, 6].Style.Font.Bold = true;
                    //worksheet.Cells[3, 6].Style.Font.Color.SetColor(colFromHex_H1);

                    //worksheet.Cells[4, 2].Value = "Table & Crown Inclusion = White Inclusion";
                    //worksheet.Cells[4, 2, 4, 5].Merge = true;
                    //worksheet.Cells[4, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[4, 2].Style.Font.Size = 9;
                    //worksheet.Cells[4, 6].Value = "Table & Crown Natts = Black Inclusion";
                    //worksheet.Cells[4, 6, 4, 9].Merge = true;
                    //worksheet.Cells[4, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[4, 6].Style.Font.Size = 9;

                    worksheet.Cells[1, 1].Value = "Total";
                    worksheet.Cells[1, 1, 1, 47].Style.Font.Bold = true;
                    worksheet.Cells[1, 1, 1, 47].Style.Font.Size = 11;
                    worksheet.Cells[1, 1, 1, 47].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, 1, 47].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[1, 1, 1, 47].Style.Font.Size = 11;

                    worksheet.Cells[2, 1, 2, 47].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[2, 1, 2, 47].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    worksheet.Cells[2, 1, 2, 47].Style.Font.Size = 10;
                    worksheet.Cells[2, 1, 2, 47].Style.Font.Bold = true;

                    worksheet.Cells[2, 1, 2, 47].AutoFilter = true;
                    //worksheet.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[3, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    var cellBackgroundColor1 = worksheet.Cells[2, 1, 2, 47].Style.Fill;
                    cellBackgroundColor1.PatternType = ExcelFillStyle.Solid;
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                    cellBackgroundColor1.BackgroundColor.SetColor(colFromHex);

                    #endregion

                    #region Header Name Declaration

                    worksheet.Cells[2, 1].Value = "Sr. No";
                    worksheet.Cells[2, 2].Value = "Ref No";
                    worksheet.Cells[2, 3].Value = "Lab";
                    worksheet.Cells[2, 4].Value = "Image";
                    worksheet.Cells[2, 5].Value = "Video";
                    worksheet.Cells[2, 6].Value = "Supplier Stone Id";
                    worksheet.Cells[2, 7].Value = "Certificate No";
                    worksheet.Cells[2, 8].Value = "Supplier Name";
                    worksheet.Cells[2, 9].Value = "Shape";
                    worksheet.Cells[2, 10].Value = "Pointer";
                    worksheet.Cells[2, 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[2, 10].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);
                    worksheet.Cells[2, 11].Value = "BGM";
                    worksheet.Cells[2, 12].Value = "Color";
                    worksheet.Cells[2, 13].Value = "Clarity";
                    worksheet.Cells[2, 14].Value = "Cts";
                    worksheet.Cells[2, 15].Value = "Rap Rate($)";
                    worksheet.Cells[2, 16].Value = "Rap Amount($)";
                    worksheet.Cells[2, 17].Value = "Supplier Cost Disc(%)";
                    worksheet.Cells[2, 18].Value = "Supplier Cost Value($)";
                    worksheet.Cells[2, 19].Value = "Sunrise Disc(%)";
                    worksheet.Cells[2, 20].Value = "Sunrise Value US($)";
                    worksheet.Cells[2, 21].Value = "Supplier Base Offer(%)";
                    worksheet.Cells[2, 22].Value = "Supplier Base Offer Value($)";
                    worksheet.Cells[2, 23].Value = "Cut";
                    worksheet.Cells[2, 24].Value = "Polish";
                    worksheet.Cells[2, 25].Value = "Symm";
                    worksheet.Cells[2, 26].Value = "Fls";
                    worksheet.Cells[2, 27].Value = "Length";
                    worksheet.Cells[2, 28].Value = "Width";
                    worksheet.Cells[2, 29].Value = "Depth";
                    worksheet.Cells[2, 30].Value = "Depth(%)";
                    worksheet.Cells[2, 31].Value = "Table(%)";
                    worksheet.Cells[2, 32].Value = "Key To Symbol";
                    worksheet.Cells[2, 33].Value = "Lab Comments";
                    worksheet.Cells[2, 34].Value = "Girdle(%)";
                    worksheet.Cells[2, 35].Value = "Crown Angle";
                    worksheet.Cells[2, 36].Value = "Crown Height";
                    worksheet.Cells[2, 37].Value = "Pavilion Angle";
                    worksheet.Cells[2, 38].Value = "Pavilion Height";
                    worksheet.Cells[2, 39].Value = "Table Natts";
                    worksheet.Cells[2, 40].Value = "Crown Natts";
                    worksheet.Cells[2, 41].Value = "Table Inclusion";
                    worksheet.Cells[2, 42].Value = "Crown Inclusion";
                    worksheet.Cells[2, 43].Value = "Culet";
                    worksheet.Cells[2, 44].Value = "Table Open";
                    worksheet.Cells[2, 45].Value = "Crown Open";
                    worksheet.Cells[2, 46].Value = "Pav Open";
                    worksheet.Cells[2, 47].Value = "Girdle Open";


                    ExcelStyle cellStyleHeader1 = worksheet.Cells[2, 1, 2, 47].Style;
                    cellStyleHeader1.Border.Left.Style = cellStyleHeader1.Border.Right.Style
                            = cellStyleHeader1.Border.Top.Style = cellStyleHeader1.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    #endregion

                    #region Set AutoFit and Decimal Number Format

                    worksheet.View.FreezePanes(3, 1);

                    worksheet.Cells[2, 1].AutoFitColumns(7);    //"Sr. No";            
                    worksheet.Cells[2, 2].AutoFitColumns(14);   //"Ref No";            
                    worksheet.Cells[2, 3].AutoFitColumns(6);    //"Lab";             
                    worksheet.Cells[2, 4].AutoFitColumns(8);    //"Image";            
                    worksheet.Cells[2, 5].AutoFitColumns(8);    //"Video";            
                    worksheet.Cells[2, 6].AutoFitColumns(14);   //"Supplier Stone Id";            
                    worksheet.Cells[2, 7].AutoFitColumns(14);   //"Certificate No";            
                    worksheet.Cells[2, 8].AutoFitColumns(30);   //"Supplier Name";            
                    worksheet.Cells[2, 9].AutoFitColumns(11);   //"Shape";            
                    worksheet.Cells[2, 10].AutoFitColumns(10);  //"Pointer";            
                    worksheet.Cells[2, 11].AutoFitColumns(9);   //"BGM";            
                    worksheet.Cells[2, 12].AutoFitColumns(8);   //"Color";            
                    worksheet.Cells[2, 13].AutoFitColumns(8);   //"Clarity";            
                    worksheet.Cells[2, 14].AutoFitColumns(11);  //"Cts";            
                    worksheet.Cells[2, 15].AutoFitColumns(13);  //"Rap Rate($)";            
                    worksheet.Cells[2, 16].AutoFitColumns(13);  //"Rap Amount($)";            
                    worksheet.Cells[2, 17].AutoFitColumns(13);  //"Supplier Cost Disc(%)";            
                    worksheet.Cells[2, 18].AutoFitColumns(13);  //"Supplier Cost Value($)";            
                    worksheet.Cells[2, 19].AutoFitColumns(13);  //"Sunrise Disc(%)";            
                    worksheet.Cells[2, 20].AutoFitColumns(13);  //"Sunrise Value US($)";            
                    worksheet.Cells[2, 21].AutoFitColumns(13);  //"Supplier Base Offer(%)";            
                    worksheet.Cells[2, 22].AutoFitColumns(13);  //"Supplier Base Offer Value($)";            
                    worksheet.Cells[2, 23].AutoFitColumns(8);   //"Cut";            
                    worksheet.Cells[2, 24].AutoFitColumns(8);   //"Polish";            
                    worksheet.Cells[2, 25].AutoFitColumns(8);   //"Symm";            
                    worksheet.Cells[2, 26].AutoFitColumns(8);   //"Fls";            
                    worksheet.Cells[2, 27].AutoFitColumns(8);   //"Length";            
                    worksheet.Cells[2, 28].AutoFitColumns(8);   //"Width";            
                    worksheet.Cells[2, 29].AutoFitColumns(8);   //"Depth";            
                    worksheet.Cells[2, 30].AutoFitColumns(8);   //"Depth(%)";            
                    worksheet.Cells[2, 31].AutoFitColumns(8);   //"Table(%)";            
                    worksheet.Cells[2, 32].AutoFitColumns(30);  //"Key To Symbol";            
                    worksheet.Cells[2, 33].AutoFitColumns(30);  //"Lab Comments";            
                    worksheet.Cells[2, 34].AutoFitColumns(8);   //"Girdle(%)";            
                    worksheet.Cells[2, 35].AutoFitColumns(8);   //"Crown Angle";            
                    worksheet.Cells[2, 36].AutoFitColumns(8);   //"Crown Height";            
                    worksheet.Cells[2, 37].AutoFitColumns(8);   //"Pavilion Angle";            
                    worksheet.Cells[2, 38].AutoFitColumns(8);   //"Pavilion Height";            
                    worksheet.Cells[2, 39].AutoFitColumns(8);   //"Table Natts";            
                    worksheet.Cells[2, 40].AutoFitColumns(8);   //"Crown Natts";            
                    worksheet.Cells[2, 41].AutoFitColumns(8);   //"Table Inclusion";            
                    worksheet.Cells[2, 42].AutoFitColumns(8);   //"Crown Inclusion";            
                    worksheet.Cells[2, 43].AutoFitColumns(8);   //"Culet";            
                    worksheet.Cells[2, 44].AutoFitColumns(8);   //"Table Open";            
                    worksheet.Cells[2, 45].AutoFitColumns(8);   //"Crown Open";            
                    worksheet.Cells[2, 46].AutoFitColumns(8);   //"Pav Open";            
                    worksheet.Cells[2, 47].AutoFitColumns(8);   //"Girdle Open";            


                    //Set Cell Faoat value with Alignment
                    worksheet.Cells[inStartIndex, 1, inEndCounter, 47].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    #endregion
                    var asTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo;


                    for (i = inStartIndex; i < inEndCounter; i++)
                    {
                        #region Assigns Value to Cell

                        worksheet.Cells[inwrkrow, 1].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["iSr"]);
                        worksheet.Cells[inwrkrow, 2].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Ref_No"]);
                        worksheet.Cells[inwrkrow, 3].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]);

                        string Image_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image_URL"]);
                        if (!string.IsNullOrEmpty(Image_URL))
                        {
                            worksheet.Cells[inwrkrow, 4].Formula = "=HYPERLINK(\"" + Image_URL + "\",\" Image \")";
                            worksheet.Cells[inwrkrow, 4].Style.Font.UnderLine = true;
                            worksheet.Cells[inwrkrow, 4].Style.Font.Color.SetColor(Color.Blue);
                        }

                        string Video_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Video_URL"]);
                        if (!string.IsNullOrEmpty(Video_URL))
                        {
                            worksheet.Cells[inwrkrow, 5].Formula = "=HYPERLINK(\"" + Video_URL + "\",\" Video \")";
                            worksheet.Cells[inwrkrow, 5].Style.Font.UnderLine = true;
                            worksheet.Cells[inwrkrow, 5].Style.Font.Color.SetColor(Color.Blue);
                        }

                        string Supplier_Stone_Id = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Supplier_Stone_Id"]);
                        success1 = Int64.TryParse(Supplier_Stone_Id, out number_1);
                        if (success1)
                        {
                            worksheet.Cells[inwrkrow, 6].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Supplier_Stone_Id"]);
                        }
                        else
                        {
                            worksheet.Cells[inwrkrow, 6].Value = Supplier_Stone_Id;
                        }

                        string Certificate_No = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                        success1 = Int64.TryParse(Certificate_No, out number_1);
                        if (success1)
                        {
                            worksheet.Cells[inwrkrow, 7].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                        }
                        else
                        {
                            worksheet.Cells[inwrkrow, 7].Value = Certificate_No;
                        }

                        worksheet.Cells[inwrkrow, 8].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["SupplierName"]);
                        worksheet.Cells[inwrkrow, 9].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shape"]);
                        worksheet.Cells[inwrkrow, 10].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pointer"]);
                        worksheet.Cells[inwrkrow, 11].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["BGM"]);
                        worksheet.Cells[inwrkrow, 12].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Color"]);
                        worksheet.Cells[inwrkrow, 13].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Clarity"]);

                        worksheet.Cells[inwrkrow, 14].Value = ((dtDiamonds.Rows[i - inStartIndex]["Cts"] != null) ?
                               (dtDiamonds.Rows[i - inStartIndex]["Cts"].GetType().Name != "DBNull" ?
                               Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Cts"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 15].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"] != null) ?
                               (dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"].GetType().Name != "DBNull" ?
                               Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 16].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"] != null) ?
                             (dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"].GetType().Name != "DBNull" ?
                             Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 17].Value = ((dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"] != null) ?
                             (dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"].GetType().Name != "DBNull" ?
                             Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 18].Value = ((dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"] != null) ?
                             (dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"].GetType().Name != "DBNull" ?
                             Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 19].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"] != null) ?
                             (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"].GetType().Name != "DBNull" ?
                             Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 20].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"] != null) ?
                             (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"].GetType().Name != "DBNull" ?
                             Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 21].Value = ((dtDiamonds.Rows[i - inStartIndex]["Disc"] != null) ?
                             (dtDiamonds.Rows[i - inStartIndex]["Disc"].GetType().Name != "DBNull" ?
                             Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Disc"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 22].Value = ((dtDiamonds.Rows[i - inStartIndex]["Value"] != null) ?
                             (dtDiamonds.Rows[i - inStartIndex]["Value"].GetType().Name != "DBNull" ?
                             Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Value"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 23].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]);
                        worksheet.Cells[inwrkrow, 24].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Polish"]);
                        worksheet.Cells[inwrkrow, 25].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Symm"]);

                        if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]).ToUpper() == "3EX")
                        {
                            worksheet.Cells[inwrkrow, 23].Style.Font.Bold = true;
                            worksheet.Cells[inwrkrow, 24].Style.Font.Bold = true;
                            worksheet.Cells[inwrkrow, 25].Style.Font.Bold = true;
                        }

                        worksheet.Cells[inwrkrow, 26].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fls"]);

                        worksheet.Cells[inwrkrow, 27].Value = ((dtDiamonds.Rows[i - inStartIndex]["Length"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Length"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Length"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 28].Value = ((dtDiamonds.Rows[i - inStartIndex]["Width"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Width"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Width"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 29].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Depth"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 30].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth_Per"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Depth_Per"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth_Per"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 31].Value = ((dtDiamonds.Rows[i - inStartIndex]["Table_Per"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Table_Per"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Table_Per"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 32].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Key_To_Symboll"]);
                        worksheet.Cells[inwrkrow, 33].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab_Comments"]);

                        worksheet.Cells[inwrkrow, 34].Value = ((dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 35].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 36].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Height"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Crown_Height"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Height"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 37].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 38].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Height"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["Pav_Height"].GetType().Name != "DBNull" ?
                           Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Height"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 39].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Natts"]);
                        worksheet.Cells[inwrkrow, 40].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"]);
                        worksheet.Cells[inwrkrow, 41].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Inclusion"]);
                        worksheet.Cells[inwrkrow, 42].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Inclusion"]);
                        worksheet.Cells[inwrkrow, 43].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Culet"]);
                        worksheet.Cells[inwrkrow, 44].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Open"]);
                        worksheet.Cells[inwrkrow, 45].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Open"]);
                        worksheet.Cells[inwrkrow, 46].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pav_Open"]);
                        worksheet.Cells[inwrkrow, 47].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle_Open"]);

                        inwrkrow++;

                        #endregion
                    }

                    worksheet.Cells[inStartIndex, 1, (inwrkrow - 1), 47].Style.Font.Size = 9;
                    worksheet.Cells[inStartIndex, 14, (inwrkrow - 1), 16].Style.Numberformat.Format = "#,##0.00";
                    worksheet.Cells[inStartIndex, 17, (inwrkrow - 1), 22].Style.Numberformat.Format = "#,##0.0000";

                    worksheet.Cells[inStartIndex, 10, (inwrkrow - 1), 10].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[inStartIndex, 10, (inwrkrow - 1), 10].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);

                    worksheet.Cells[inStartIndex, 17, (inwrkrow - 1), 22].Style.Font.Bold = true;
                    worksheet.Cells[inStartIndex, 17, (inwrkrow - 1), 22].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[inStartIndex, 17, (inwrkrow - 1), 22].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                    worksheet.Cells[inStartIndex, 17, (inwrkrow - 1), 22].Style.Font.Color.SetColor(System.Drawing.Color.Red);

                    worksheet.Cells[inStartIndex, 27, (inwrkrow - 1), 31].Style.Numberformat.Format = "0.00";
                    worksheet.Cells[inStartIndex, 35, (inwrkrow - 1), 38].Style.Numberformat.Format = "0.00";
                    worksheet.Cells[inStartIndex, 34, (inwrkrow - 1), 34].Style.Numberformat.Format = "0.00";

                    worksheet.Cells[1, 2].Formula = "ROUND(SUBTOTAL(102,A" + inStartIndex + ":A" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[1, 2].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 2].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[1, 2].Style.Numberformat.Format = "#,##";

                    ExcelStyle cellStyleHeader_Total = worksheet.Cells[1, 2].Style;
                    cellStyleHeader_Total.Border.Left.Style = cellStyleHeader_Total.Border.Right.Style
                            = cellStyleHeader_Total.Border.Top.Style = cellStyleHeader_Total.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[1, 14].Formula = "ROUND(SUBTOTAL(109,N" + inStartIndex + ":N" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[1, 14].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 14].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[1, 14].Style.Numberformat.Format = "#,##0.00";

                    ExcelStyle cellStyleHeader_Totalcarat = worksheet.Cells[1, 14].Style;
                    cellStyleHeader_Totalcarat.Border.Left.Style = cellStyleHeader_Totalcarat.Border.Right.Style
                            = cellStyleHeader_Totalcarat.Border.Top.Style = cellStyleHeader_Totalcarat.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[1, 17].Formula = "IF(SUBTOTAL(109,P" + inStartIndex + ": P" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109,R" + inStartIndex + ":R" + (inwrkrow - 1) + ")/SUBTOTAL(109,P" + inStartIndex + ":P" + (inwrkrow - 1) + ")))*(-100),2))";
                    worksheet.Cells[1, 17].Style.Numberformat.Format = "#,##0.00";

                    ExcelStyle cellStyleHeader_TotalDis = worksheet.Cells[1, 17].Style;
                    cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                            = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[1, 18].Formula = "ROUND(SUBTOTAL(109,R" + inStartIndex + ":R" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[1, 18].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 18].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[1, 18].Style.Numberformat.Format = "#,##0";

                    ExcelStyle cellStyleHeader_TotalAmt = worksheet.Cells[1, 18].Style;
                    cellStyleHeader_TotalAmt.Border.Left.Style = cellStyleHeader_TotalAmt.Border.Right.Style
                            = cellStyleHeader_TotalAmt.Border.Top.Style = cellStyleHeader_TotalAmt.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[1, 19].Formula = "IF(SUBTOTAL(109,P" + inStartIndex + ": P" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109,T" + inStartIndex + ":T" + (inwrkrow - 1) + ")/SUBTOTAL(109,P" + inStartIndex + ":P" + (inwrkrow - 1) + ")))*(-100),2))";
                    worksheet.Cells[1, 19].Style.Numberformat.Format = "#,##0.00";

                    ExcelStyle cellStyleHeader_TotalDis_1 = worksheet.Cells[1, 19].Style;
                    cellStyleHeader_TotalDis_1.Border.Left.Style = cellStyleHeader_TotalDis_1.Border.Right.Style
                            = cellStyleHeader_TotalDis_1.Border.Top.Style = cellStyleHeader_TotalDis_1.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[1, 20].Formula = "ROUND(SUBTOTAL(109,T" + inStartIndex + ":T" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[1, 20].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 20].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[1, 20].Style.Numberformat.Format = "#,##0";

                    ExcelStyle cellStyleHeader_TotalAmt_1 = worksheet.Cells[1, 20].Style;
                    cellStyleHeader_TotalAmt_1.Border.Left.Style = cellStyleHeader_TotalAmt_1.Border.Right.Style
                            = cellStyleHeader_TotalAmt_1.Border.Top.Style = cellStyleHeader_TotalAmt_1.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[1, 21].Formula = "IF(SUBTOTAL(109,P" + inStartIndex + ": P" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109,V" + inStartIndex + ":V" + (inwrkrow - 1) + ")/SUBTOTAL(109,P" + inStartIndex + ":P" + (inwrkrow - 1) + ")))*(-100),2))";
                    worksheet.Cells[1, 21].Style.Numberformat.Format = "#,##0.00";

                    ExcelStyle cellStyleHeader_TotalDis_2 = worksheet.Cells[1, 21].Style;
                    cellStyleHeader_TotalDis_2.Border.Left.Style = cellStyleHeader_TotalDis_2.Border.Right.Style
                            = cellStyleHeader_TotalDis_2.Border.Top.Style = cellStyleHeader_TotalDis_2.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[1, 22].Formula = "ROUND(SUBTOTAL(109,V" + inStartIndex + ":V" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[1, 22].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[1, 22].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[1, 22].Style.Numberformat.Format = "#,##0";

                    ExcelStyle cellStyleHeader_TotalAmt_2 = worksheet.Cells[1, 22].Style;
                    cellStyleHeader_TotalAmt_2.Border.Left.Style = cellStyleHeader_TotalAmt_2.Border.Right.Style
                            = cellStyleHeader_TotalAmt_2.Border.Top.Style = cellStyleHeader_TotalAmt_2.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

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
        public static void Supplier_Excel_New(DataTable dtDiamonds, DataTable Col_dt, string _strFolderPath, string _strFilePath)
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
                            worksheet.Cells[2, k].AutoFitColumns(8);

                            k += 1;
                            worksheet.Cells[2, k].Value = "Video";
                            worksheet.Cells[2, k].AutoFitColumns(8);
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

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);
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

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";

                                    worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                }
                                else if (Column_Name == "Supplier Cost Value($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";

                                    worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                }
                                else if (Column_Name == "Sunrise Disc(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";

                                    worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                }
                                else if (Column_Name == "Sunrise Value US($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";

                                    worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                }
                                else if (Column_Name == "Supplier Base Offer(%)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Disc"] != null) ?
                                    (dtDiamonds.Rows[i - inStartIndex]["Disc"].GetType().Name != "DBNull" ?
                                    Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Disc"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";

                                    worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                }
                                else if (Column_Name == "Supplier Base Offer Value($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Value"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Value"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Value"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";

                                    worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(System.Drawing.Color.Red);
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
        public static void Customer_Excel_New(DataTable dtDiamonds, DataTable Col_dt, string _strFolderPath, string _strFilePath)
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
                            worksheet.Cells[2, k].AutoFitColumns(8);

                            k += 1;
                            worksheet.Cells[2, k].Value = "Video";
                            worksheet.Cells[2, k].AutoFitColumns(8);

                            k += 1;
                            worksheet.Cells[2, k].Value = "Certi";
                            worksheet.Cells[2, k].AutoFitColumns(8);
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
                                }
                                else if (Column_Name == "Shape")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shape"]);
                                }
                                else if (Column_Name == "Pointer")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pointer"]);

                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);
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

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";

                                    worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                }
                                else if (Column_Name == "Offer Value($)")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Value"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Value"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Value"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "#,##0.0000";

                                    worksheet.Cells[inwrkrow, kk].Style.Font.Bold = true;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.PatternType = ExcelFillStyle.Solid;
                                    worksheet.Cells[inwrkrow, kk].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                                    worksheet.Cells[inwrkrow, kk].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                                }
                                else if (Column_Name == "Price Cts")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"] != null) ?
                                     (dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"].GetType().Name != "DBNull" ?
                                     Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"]) : ((Double?)null)) : null);
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
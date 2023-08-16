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
                    int inStartIndex = 2;
                    int inwrkrow = 2;
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
                    worksheet.Row(1).Height = 40;
                    worksheet.Row(1).Style.WrapText = true;

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

                    //worksheet.Cells[5, 1].Value = "Total";
                    //worksheet.Cells[5, 1, 5, 70].Style.Font.Bold = true;
                    //worksheet.Cells[5, 1, 5, 70].Style.Font.Size = 11;
                    //worksheet.Cells[5, 1, 5, 70].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    //worksheet.Cells[5, 1, 5, 70].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    //worksheet.Cells[5, 1, 5, 70].Style.Font.Size = 11;

                    worksheet.Cells[1, 1, 1, 70].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[1, 1, 1, 70].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    worksheet.Cells[1, 1, 1, 70].Style.Font.Size = 10;
                    worksheet.Cells[1, 1, 1, 70].Style.Font.Bold = true;

                    worksheet.Cells[1, 1, 1, 70].AutoFilter = true;
                    //worksheet.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[3, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    var cellBackgroundColor1 = worksheet.Cells[1, 1, 1, 70].Style.Fill;
                    cellBackgroundColor1.PatternType = ExcelFillStyle.Solid;
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                    cellBackgroundColor1.BackgroundColor.SetColor(colFromHex);

                    #endregion

                    #region Header Name Declaration

                    worksheet.Cells[1, 1].Value = "Sr. No";
                    worksheet.Cells[1, 2].Value = "Image";
                    worksheet.Cells[1, 3].Value = "Video";
                    worksheet.Cells[1, 4].Value = "Certi";
                    worksheet.Cells[1, 5].Value = "Lab";
                    worksheet.Cells[1, 6].Value = "Supplier Name";
                    worksheet.Cells[1, 7].Value = "Rank";
                    worksheet.Cells[1, 8].Value = "Supplier Status";
                    worksheet.Cells[1, 9].Value = "Buyer Name";
                    worksheet.Cells[1, 10].Value = "Status";
                    worksheet.Cells[1, 11].Value = "Supplier Stone Id";
                    worksheet.Cells[1, 12].Value = "Certificate No";
                    worksheet.Cells[1, 13].Value = "Shape";
                    worksheet.Cells[1, 14].Value = "Pointer";
                    worksheet.Cells[1, 15].Value = "Sub Pointer";
                    worksheet.Cells[1, 16].Value = "Color";
                    worksheet.Cells[1, 17].Value = "Clarity";
                    worksheet.Cells[1, 18].Value = "Cts";
                    worksheet.Cells[1, 19].Value = "Rap Rate";
                    worksheet.Cells[1, 20].Value = "Rap Amount";
                    worksheet.Cells[1, 21].Value = "Supplier Base Offer(%)";
                    worksheet.Cells[1, 22].Value = "Supplier Base Offer Value($)";
                    worksheet.Cells[1, 23].Value = "Supplier Final Disc(%)";
                    worksheet.Cells[1, 24].Value = "Supplier Final Value($)";
                    worksheet.Cells[1, 25].Value = "Supplier Final Disc. With Max Slab(%)";
                    worksheet.Cells[1, 26].Value = "Supplier Final Value With Max Slab($)";
                    worksheet.Cells[1, 27].Value = "Bid Disc(%)";
                    worksheet.Cells[1, 28].Value = "Bid Amt";
                    worksheet.Cells[1, 29].Value = "Bid/Ct";
                    worksheet.Cells[1, 30].Value = "Avg. Stock Disc(%)";
                    worksheet.Cells[1, 31].Value = "Avg. Stock Pcs";
                    worksheet.Cells[1, 32].Value = "Avg. Pur. Disc(%)";
                    worksheet.Cells[1, 33].Value = "Avg. Pur. Pcs";
                    worksheet.Cells[1, 34].Value = "Avg. Sales Disc(%)";
                    worksheet.Cells[1, 35].Value = "Sales Pcs";
                    worksheet.Cells[1, 36].Value = "KTS Grade";
                    worksheet.Cells[1, 37].Value = "Comm. Grade";
                    worksheet.Cells[1, 38].Value = "Zone";
                    worksheet.Cells[1, 39].Value = "Para. Grade";
                    worksheet.Cells[1, 40].Value = "Cut";
                    worksheet.Cells[1, 41].Value = "Polish";
                    worksheet.Cells[1, 42].Value = "Symm";
                    worksheet.Cells[1, 43].Value = "Fls";
                    worksheet.Cells[1, 44].Value = "Key To Symbol";
                    worksheet.Cells[1, 45].Value = "Ratio";
                    worksheet.Cells[1, 46].Value = "Length";
                    worksheet.Cells[1, 47].Value = "Width";
                    worksheet.Cells[1, 48].Value = "Depth";
                    worksheet.Cells[1, 49].Value = "Depth(%)";
                    worksheet.Cells[1, 50].Value = "Table(%)";
                    worksheet.Cells[1, 51].Value = "Crown Ang";
                    worksheet.Cells[1, 52].Value = "Crown Hgt";
                    worksheet.Cells[1, 53].Value = "Pavilion Ang";
                    worksheet.Cells[1, 54].Value = "Pavilion Hgt";
                    worksheet.Cells[1, 55].Value = "Girdle(%)";
                    worksheet.Cells[1, 56].Value = "Luster";
                    worksheet.Cells[1, 57].Value = "Type 2A";
                    worksheet.Cells[1, 58].Value = "Table Inclusion";
                    worksheet.Cells[1, 59].Value = "Crown Inclusion";
                    worksheet.Cells[1, 60].Value = "Table Natts";
                    worksheet.Cells[1, 61].Value = "Crown Natts";
                    worksheet.Cells[1, 62].Value = "Culet";
                    worksheet.Cells[1, 63].Value = "Lab Comments";
                    worksheet.Cells[1, 64].Value = "Supplier Comments";
                    worksheet.Cells[1, 65].Value = "Table Open";
                    worksheet.Cells[1, 66].Value = "Crown Open";
                    worksheet.Cells[1, 67].Value = "Pav Open";
                    worksheet.Cells[1, 68].Value = "Girdle Open";
                    worksheet.Cells[1, 69].Value = "Shade";
                    worksheet.Cells[1, 70].Value = "Milky";

                    ExcelStyle cellStyleHeader1 = worksheet.Cells[1, 1, 1, 70].Style;
                    cellStyleHeader1.Border.Left.Style = cellStyleHeader1.Border.Right.Style
                            = cellStyleHeader1.Border.Top.Style = cellStyleHeader1.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    #endregion

                    #region Set AutoFit and Decimal Number Format

                    worksheet.View.FreezePanes(2, 1);

                    worksheet.Cells[1, 1].AutoFitColumns(5.43);                     //"Sr. No";
                    worksheet.Cells[1, 2].AutoFitColumns(9);                        //"Image";
                    worksheet.Cells[1, 3].AutoFitColumns(9);                        //"Video";
                    worksheet.Cells[1, 4].AutoFitColumns(9);                        //"Certi";
                    worksheet.Cells[1, 5].AutoFitColumns(9);                       //"Lab";
                    worksheet.Cells[1, 6].AutoFitColumns(15);                    //"Supplier Name";
                    worksheet.Cells[1, 7].AutoFitColumns(6);                     //"Rank";
                    worksheet.Cells[1, 8].AutoFitColumns(10);                     //"Supplier Status";
                    worksheet.Cells[1, 9].AutoFitColumns(15);                        //"Buyer Name";
                    worksheet.Cells[1, 10].AutoFitColumns(6);              // "Status";
                    worksheet.Cells[1, 11].AutoFitColumns(13.5);              // "Supplier Stone Id";
                    worksheet.Cells[1, 12].AutoFitColumns(13.5);                    // "Certificate No";
                    worksheet.Cells[1, 13].AutoFitColumns(9);                    // "Shape";
                    worksheet.Cells[1, 14].AutoFitColumns(8);                    // "Pointer";
                    worksheet.Cells[1, 15].AutoFitColumns(8);                    // "Sub Pointer";
                    worksheet.Cells[1, 16].AutoFitColumns(8);                    // "Color";
                    worksheet.Cells[1, 17].AutoFitColumns(8);                      // "Clarity";
                    worksheet.Cells[1, 18].AutoFitColumns(8);                      // "Cts";
                    worksheet.Cells[1, 19].AutoFitColumns(13);                   // "Rap Rate";
                    worksheet.Cells[1, 20].AutoFitColumns(13);                      // "Rap Amount";
                    worksheet.Cells[1, 21].AutoFitColumns(13);                       // "Supplier Base Offer(%)";
                    worksheet.Cells[1, 22].AutoFitColumns(13);                    // "Supplier Base Offer Value($)";
                    worksheet.Cells[1, 23].AutoFitColumns(13);                    // "Supplier Final Disc(%)";
                    worksheet.Cells[1, 24].AutoFitColumns(13);                    // "Supplier Final Value($)";
                    worksheet.Cells[1, 25].AutoFitColumns(13);                    // "Supplier Final Disc. With Max Slab(%)";
                    worksheet.Cells[1, 26].AutoFitColumns(13);                    // "Supplier Final Value With Max Slab($)";
                    worksheet.Cells[1, 27].AutoFitColumns(13);                    // "Bid Disc(%)";
                    worksheet.Cells[1, 28].AutoFitColumns(13);                    // "Bid Amt";
                    worksheet.Cells[1, 29].AutoFitColumns(13);                    // "Bid/Ct";
                    worksheet.Cells[1, 30].AutoFitColumns(13);                       // "Avg. Stock Disc(%)";
                    worksheet.Cells[1, 31].AutoFitColumns(9);                     // "Avg. Stock Pcs";
                    worksheet.Cells[1, 32].AutoFitColumns(13);                   // "Avg. Pur. Disc(%)";
                    worksheet.Cells[1, 33].AutoFitColumns(9);                      // "Avg. Pur. Pcs";
                    worksheet.Cells[1, 34].AutoFitColumns(13);                    // "Avg. Sales Disc(%)";
                    worksheet.Cells[1, 35].AutoFitColumns(9);                    // "Sales Pcs";
                    worksheet.Cells[1, 36].AutoFitColumns(7.86);                    // "KTS Grade";
                    worksheet.Cells[1, 37].AutoFitColumns(7.86);                    // "Comm. Grade";
                    worksheet.Cells[1, 38].AutoFitColumns(7.86);                    // "Zone";
                    worksheet.Cells[1, 39].AutoFitColumns(7.86);                    // "Para. Grade";
                    worksheet.Cells[1, 40].AutoFitColumns(7.86);                    // "Cut";
                    worksheet.Cells[1, 41].AutoFitColumns(7.86);                    // "Polish";
                    worksheet.Cells[1, 42].AutoFitColumns(7.86);                    // "Symm";
                    worksheet.Cells[1, 43].AutoFitColumns(7.86);                    // "Fls";
                    worksheet.Cells[1, 44].AutoFitColumns(15);                    // "Key To Symbol";
                    worksheet.Cells[1, 45].AutoFitColumns(7.86);                    // "Ratio";
                    worksheet.Cells[1, 46].AutoFitColumns(7.86);                    // "Length";
                    worksheet.Cells[1, 47].AutoFitColumns(7.86);                    // "Width";
                    worksheet.Cells[1, 48].AutoFitColumns(7.86);                    // "Depth";
                    worksheet.Cells[1, 49].AutoFitColumns(7.86);                    // "Depth(%)";
                    worksheet.Cells[1, 50].AutoFitColumns(7.86);                    // "Table(%)";
                    worksheet.Cells[1, 51].AutoFitColumns(7.86);                    // "Crown Ang";
                    worksheet.Cells[1, 52].AutoFitColumns(7.86);                    // "Crown Hgt";
                    worksheet.Cells[1, 53].AutoFitColumns(7.86);                    // "Pavilion Ang";
                    worksheet.Cells[1, 54].AutoFitColumns(7.86);                    // "Pavilion Hgt";
                    worksheet.Cells[1, 55].AutoFitColumns(7.86);                    // "Girdle(%)";
                    worksheet.Cells[1, 56].AutoFitColumns(7.86);                    // "Luster";
                    worksheet.Cells[1, 57].AutoFitColumns(7.86);                    // "Type 2A";
                    worksheet.Cells[1, 58].AutoFitColumns(7.86);                    // "Table Inclusion";
                    worksheet.Cells[1, 59].AutoFitColumns(7.86);                    // "Crown Inclusion";
                    worksheet.Cells[1, 60].AutoFitColumns(7.86);                    // "Table Natts";
                    worksheet.Cells[1, 61].AutoFitColumns(7.86);                    // "Crown Natts";
                    worksheet.Cells[1, 62].AutoFitColumns(7.86);                    // "Culet";
                    worksheet.Cells[1, 63].AutoFitColumns(15);                    // "Lab Comments";
                    worksheet.Cells[1, 64].AutoFitColumns(15);                    // "Supplier Comments";
                    worksheet.Cells[1, 65].AutoFitColumns(7.86);                    // "Table Open";
                    worksheet.Cells[1, 66].AutoFitColumns(7.86);                    // "Crown Open";
                    worksheet.Cells[1, 67].AutoFitColumns(7.86);                    // "Pav Open";
                    worksheet.Cells[1, 68].AutoFitColumns(7.86);                    // "Girdle Open";
                    worksheet.Cells[1, 69].AutoFitColumns(7.86);                    // "Shade";
                    worksheet.Cells[1, 70].AutoFitColumns(7.86);                    // "Milky";


                    //Set Cell Faoat value with Alignment
                    worksheet.Cells[inStartIndex, 1, inEndCounter, 70].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    #endregion
                    var asTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo;

                    
                    for (i = inStartIndex; i < inEndCounter; i++)
                    {
                        #region Assigns Value to Cell

                        worksheet.Cells[inwrkrow, 1].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["iSr"]);

                        string Image_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image_URL"]);
                        if (Image_URL != "")
                        {
                            worksheet.Cells[inwrkrow, 2].Formula = "=HYPERLINK(\"" + Image_URL + "\",\" Image \")";
                            worksheet.Cells[inwrkrow, 2].Style.Font.UnderLine = true;
                            worksheet.Cells[inwrkrow, 2].Style.Font.Color.SetColor(Color.Blue);
                        }

                        string Video_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Video_URL"]);
                        if (Video_URL != "")
                        {
                            worksheet.Cells[inwrkrow, 3].Formula = "=HYPERLINK(\"" + Video_URL + "\",\" Video \")";
                            worksheet.Cells[inwrkrow, 3].Style.Font.UnderLine = true;
                            worksheet.Cells[inwrkrow, 3].Style.Font.Color.SetColor(Color.Blue);
                        }

                        string Certificate_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_URL"]);
                        if (Certificate_URL != "")
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

                        string Supplier_Stone_Id = dtDiamonds.Rows[i - inStartIndex]["Supplier_Stone_Id"].ToString();
                        success1 = Int64.TryParse(Supplier_Stone_Id, out number_1);
                        if (success1)
                        {
                            worksheet.Cells[inwrkrow, 11].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Supplier_Stone_Id"]).ToString();
                        }
                        else
                        {
                            worksheet.Cells[inwrkrow, 11].Value = Supplier_Stone_Id;
                        }

                        string Certificate_No = dtDiamonds.Rows[i - inStartIndex]["Certificate_No"].ToString();
                        success1 = Int64.TryParse(Certificate_No, out number_1);
                        if (success1)
                        {
                            worksheet.Cells[inwrkrow, 12].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]).ToString();
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

                        worksheet.Cells[inwrkrow, 18].Value = Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Cts"]);

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
                        worksheet.Cells[inwrkrow, 43].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fls"]);
                        worksheet.Cells[inwrkrow, 44].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Key_To_Symboll"]);

                        worksheet.Cells[inwrkrow, 45].Value = ((dtDiamonds.Rows[i - inStartIndex]["RATIO"] != null) ?
                           (dtDiamonds.Rows[i - inStartIndex]["RATIO"].GetType().Name != "DBNull" ?
                           Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["RATIO"]) : ((Int64?)null)) : null);

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

                    //worksheet.Cells[inStartIndex, 9, (inwrkrow - 1), 9].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //worksheet.Cells[inStartIndex, 9, (inwrkrow - 1), 9].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);

                    //worksheet.Cells[inStartIndex, 19, (inwrkrow - 1), 20].Style.Font.Bold = true;

                    //worksheet.Cells[inStartIndex, 19, (inwrkrow - 1), 20].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //worksheet.Cells[inStartIndex, 19, (inwrkrow - 1), 20].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);
                    //worksheet.Cells[inStartIndex, 19, (inwrkrow - 1), 20].Style.Font.Color.SetColor(System.Drawing.Color.Red);

                    //worksheet.Cells[inStartIndex, 27, (inwrkrow - 1), 31].Style.Numberformat.Format = "0.00";
                    //worksheet.Cells[inStartIndex, 39, (inwrkrow - 1), 42].Style.Numberformat.Format = "0.00";
                    //worksheet.Cells[inStartIndex, 47, (inwrkrow - 1), 47].Style.Numberformat.Format = "0.00";

                    //worksheet.Cells[5, 5].Formula = "ROUND(SUBTOTAL(102,A" + inStartIndex + ":A" + (inwrkrow - 1) + "),2)";
                    //worksheet.Cells[5, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //worksheet.Cells[5, 5].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    //worksheet.Cells[5, 5].Style.Numberformat.Format = "#,##";

                    //ExcelStyle cellStyleHeader_Total = worksheet.Cells[5, 5].Style;
                    //cellStyleHeader_Total.Border.Left.Style = cellStyleHeader_Total.Border.Right.Style
                    //        = cellStyleHeader_Total.Border.Top.Style = cellStyleHeader_Total.Border.Bottom.Style
                    //        = ExcelBorderStyle.Medium;

                    //worksheet.Cells[5, 16].Formula = "ROUND(SUBTOTAL(109,P" + inStartIndex + ":P" + (inwrkrow - 1) + "),2)";
                    //worksheet.Cells[5, 16].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //worksheet.Cells[5, 16].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    //worksheet.Cells[5, 16].Style.Numberformat.Format = "#,##0.00";

                    //ExcelStyle cellStyleHeader_Totalcarat = worksheet.Cells[5, 16].Style;
                    //cellStyleHeader_Totalcarat.Border.Left.Style = cellStyleHeader_Totalcarat.Border.Right.Style
                    //        = cellStyleHeader_Totalcarat.Border.Top.Style = cellStyleHeader_Totalcarat.Border.Bottom.Style
                    //        = ExcelBorderStyle.Medium;

                    //worksheet.Cells[5, 18].Formula = "ROUND(SUBTOTAL(109,R" + inStartIndex + ":R" + (inwrkrow - 1) + "),2)";
                    //worksheet.Cells[5, 18].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //worksheet.Cells[5, 18].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    //worksheet.Cells[5, 18].Style.Numberformat.Format = "#,##0";

                    //ExcelStyle cellStyleHeader_TotalAmt = worksheet.Cells[5, 18].Style;
                    //cellStyleHeader_TotalAmt.Border.Left.Style = cellStyleHeader_TotalAmt.Border.Right.Style
                    //        = cellStyleHeader_TotalAmt.Border.Top.Style = cellStyleHeader_TotalAmt.Border.Bottom.Style
                    //        = ExcelBorderStyle.Medium;

                    ////=IF(SUBTOTAL(109,Q7: Q1020)=0,0,ROUND((1-(SUBTOTAL(109,S7:S12347)/SUBTOTAL(109,Q7:Q12347)))*(-100),2))
                    //worksheet.Cells[5, 19].Formula = "IF(SUBTOTAL(109,R" + inStartIndex + ": R" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109,T" + inStartIndex + ":T" + (inwrkrow - 1) + ")/SUBTOTAL(109,R" + inStartIndex + ":R" + (inwrkrow - 1) + ")))*(-100),2))";
                    //worksheet.Cells[5, 19].Style.Numberformat.Format = "#,##0.00";

                    //ExcelStyle cellStyleHeader_TotalDis = worksheet.Cells[5, 19].Style;
                    //cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                    //        = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                    //        = ExcelBorderStyle.Medium;

                    //worksheet.Cells[5, 20].Formula = "ROUND(SUBTOTAL(109,T" + inStartIndex + ":T" + (inwrkrow - 1) + "),2)";
                    //worksheet.Cells[5, 20].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    //worksheet.Cells[5, 20].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    //worksheet.Cells[5, 20].Style.Numberformat.Format = "#,##0";

                    //ExcelStyle cellStyleHeader_TotalNet = worksheet.Cells[5, 20].Style;
                    //cellStyleHeader_TotalNet.Border.Left.Style = cellStyleHeader_TotalNet.Border.Right.Style
                    //        = cellStyleHeader_TotalNet.Border.Top.Style = cellStyleHeader_TotalNet.Border.Bottom.Style
                    //        = ExcelBorderStyle.Medium;

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
    }
}
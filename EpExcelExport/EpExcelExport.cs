using OfficeOpenXml;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
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
using System.Windows.Media.Media3D;

namespace EpExcelExportLib
{
    public class EpExcelExport
    {

        public delegate void FillingWorksheetEventHandler(object sender, ref FillingWorksheetEventArgs e);
        public delegate void BeforeCreateColumnEventHandler(object sender, ref ExcelHeader e);
        public delegate void BeforeCreateColumnEventHandler1(object sender, ref ExcelHeader e, List<ApiColSettings> columnsSettings);

        public delegate void AddHeaderEventHandler(object sender, ref AddHeaderEventArgs e);
        public delegate void AddFooterEventHandler(object sender, ref AddFooterEventArgs e);
        public delegate void BeforeCreateCellEventHandler(object sender, ref BeforeCreateCellEventArgs e);
        public delegate void AfterCreateCellEventHandler(object sender, ref ExcelCellFormat e);

        public event FillingWorksheetEventHandler FillingWorksheetEvent;
        public event AddHeaderEventHandler AddHeaderEvent;
        public event AddFooterEventHandler AddFooterEvent;


        public event BeforeCreateCellEventHandler BeforeCreateCellEvent;
        public event BeforeCreateColumnEventHandler BeforeCreateColumnEvent;
        public event BeforeCreateColumnEventHandler1 BeforeCreateColumnEvent1;
        public event AfterCreateCellEventHandler AfterCreateCellEvent;

        public const String cFORMULA_START_INDEX = "~FORMULA_START_INDEX~";
        public const String cFORMULA_END_INDEX = "~FORMULA_END_INDEX~";
        public const String External_ImageURL = "https://4e0s0i2r4n0u1s0.com/img/";
        public const String External_CertiTypeURL = "https://4e0s0i2r4n0u1s0.com:8121/DNA/CertiType?StoneNo=";

        public const String ImageURL = "https://4e0s0i2r4n0u1s0.com:8121/ViewImageVideoCerti?T=I&StoneId=";
        public const String VideoURL = "https://4e0s0i2r4n0u1s0.com:8121/ViewImageVideoCerti?T=V&StoneId=";
        public const String CertiURL = "https://4e0s0i2r4n0u1s0.com:8121/ViewImageVideoCerti?T=C&StoneId=";

        String _SheetName = "";
        String _TableName = "";
        protected ExcelWorksheet _worksheet;

        public UInt32 DefaultStyleindex;

        protected int TableHeaderStartRow;
        protected int TableDetailStartRow;
        protected int TableFooterStartRow;

        protected UInt32 CurrentRowCount;
        protected List<ExcelHeader> AllColumns;
        protected System.Collections.Generic.SortedList<UInt32, ExcelFormat> StyleList;

        protected int VisibleColumn;

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

                                if (Column_Name == "Lab")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]);
                                    string URL = "";
                                    string Certificate_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_URL"]);

                                    if (!string.IsNullOrEmpty(Certificate_URL))
                                    {
                                        URL = Certificate_URL;
                                    }
                                    else if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "GIA")
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
                            else if (Column_Name == "Supplier Base Offer(%)")
                            {
                                int Image_Video_Certi = 0, Rap_Amount = 0, Supplier_Base_Offer_Value_Doller = 0;
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video'");
                                if (dra.Length > 0)
                                {
                                    Image_Video_Certi = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Rap Amount($)'");
                                if (dra.Length > 0)
                                {
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 1 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Supplier Base Offer Value($)'");
                                if (dra.Length > 0)
                                {
                                    Supplier_Base_Offer_Value_Doller = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Supplier_Base_Offer_Value_Doller = ((Image_Video_Certi != 0 && Supplier_Base_Offer_Value_Doller > Image_Video_Certi) ? Supplier_Base_Offer_Value_Doller + 1 : Supplier_Base_Offer_Value_Doller); ;
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
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video'");
                                if (dra.Length > 0)
                                {
                                    Image_Video_Certi = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Rap Amount($)'");
                                if (dra.Length > 0)
                                {
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 1 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Supplier Final Value($)'");
                                if (dra.Length > 0)
                                {
                                    Supplier_Final_Value_Doller = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Supplier_Final_Value_Doller = ((Image_Video_Certi != 0 && Supplier_Final_Value_Doller > Image_Video_Certi) ? Supplier_Final_Value_Doller + 1 : Supplier_Final_Value_Doller); ;
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
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video'");
                                if (dra.Length > 0)
                                {
                                    Image_Video_Certi = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Rap Amount($)'");
                                if (dra.Length > 0)
                                {
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 1 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Supplier Final Value With Max Slab($)'");
                                if (dra.Length > 0)
                                {
                                    Supplier_Final_Value_With_Max_Slab_Doller = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Supplier_Final_Value_With_Max_Slab_Doller = ((Image_Video_Certi != 0 && Supplier_Final_Value_With_Max_Slab_Doller > Image_Video_Certi) ? Supplier_Final_Value_With_Max_Slab_Doller + 1 : Supplier_Final_Value_With_Max_Slab_Doller); ;
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
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video'");
                                if (dra.Length > 0)
                                {
                                    Image_Video_Certi = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Rap Amount($)'");
                                if (dra.Length > 0)
                                {
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 1 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Bid Amt'");
                                if (dra.Length > 0)
                                {
                                    Bid_Amt = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Bid_Amt = ((Image_Video_Certi != 0 && Bid_Amt > Image_Video_Certi) ? Bid_Amt + 1 : Bid_Amt); ;
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
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video'");
                                if (dra.Length > 0)
                                {
                                    Image_Video_Certi = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Cts'");
                                if (dra.Length > 0)
                                {
                                    Cts = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Cts = ((Image_Video_Certi != 0 && Cts > Image_Video_Certi) ? Cts + 1 : Cts);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Bid Amt'");
                                if (dra.Length > 0)
                                {
                                    Bid_Amt = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Bid_Amt = ((Image_Video_Certi != 0 && Bid_Amt > Image_Video_Certi) ? Bid_Amt + 1 : Bid_Amt); ;
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
                Lib.Model.Common.InsertErrorLog(ex, null, null);
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
                                    string Certificate_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_URL"]);

                                    if (!string.IsNullOrEmpty(Certificate_URL))
                                    {
                                        URL = Certificate_URL;
                                    }
                                    else if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "GIA")
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
                Lib.Model.Common.InsertErrorLog(ex, null, null);
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

                        //worksheet.Cells[inwrkrow, 1].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["iSr"]);

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
                                    string Certificate_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_URL"]);

                                    if (!string.IsNullOrEmpty(Certificate_URL))
                                    {
                                        URL = Certificate_URL;
                                    }
                                    else if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "GIA")
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
                                else if (Column_Name == "Key To Symbol")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Key_To_Symboll"]);
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
                                else if (Column_Name == "Pav Angle")
                                {
                                    worksheet.Cells[inwrkrow, kk].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"]) : ((Double?)null)) : null);

                                    worksheet.Cells[inwrkrow, kk].Style.Numberformat.Format = "0.00";
                                }
                                else if (Column_Name == "Pav Height")
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

                        if (Column_Name == "Image-Video")
                        {
                            kkk += 2;
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
                                DataRow[] dra = Col_dt.Select("[Column_Name] = 'Image-Video'");
                                if (dra.Length > 0)
                                {
                                    Image_Video_Certi = Convert.ToInt32(dra[0]["OrderBy"]);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Rap Amount($)'");
                                if (dra.Length > 0)
                                {
                                    Rap_Amount = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Rap_Amount = ((Image_Video_Certi != 0 && Rap_Amount > Image_Video_Certi) ? Rap_Amount + 1 : Rap_Amount);
                                }
                                dra = Col_dt.Select("[Column_Name] = 'Offer Value($)'");
                                if (dra.Length > 0)
                                {
                                    Offer_Value_Doller = Convert.ToInt32(dra[0]["OrderBy"]);
                                    Offer_Value_Doller = ((Image_Video_Certi != 0 && Offer_Value_Doller > Image_Video_Certi) ? Offer_Value_Doller + 1 : Offer_Value_Doller); ;
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
                Lib.Model.Common.InsertErrorLog(ex, null, null);
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
                Lib.Model.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }
        public static void LabEntry_Excel(DataTable dtDiamonds, string _strFolderPath, string _strFilePath)
        {
            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    int inStartIndex = 4;
                    int inwrkrow = 4;
                    int inEndCounter = dtDiamonds.Rows.Count + inStartIndex;
                    int TotalRow = dtDiamonds.Rows.Count;
                    int i;
                    string values_1, Image_URL, Video_URL, cut, status, ForCust_Hold;
                    Int64 number_1;
                    bool success1;

                    Color colFromHex_Pointer = System.Drawing.ColorTranslator.FromHtml("#c6e0b4");
                    Color colFromHex_Dis = System.Drawing.ColorTranslator.FromHtml("#ccffff");
                    Color colFromHexTotal = System.Drawing.ColorTranslator.FromHtml("#d9e1f2");
                    Color tcpg_bg_clr = System.Drawing.ColorTranslator.FromHtml("#fff2cc");

                    #region Company Detail on Header

                    p.Workbook.Properties.Author = "SUNRISE DIAMOND";
                    p.Workbook.Properties.Title = "SUNRISE DIAMOND PVT. LTD.";
                    p.Workbook.Worksheets.Add("LabEntry");

                    ExcelWorksheet worksheet = p.Workbook.Worksheets[1];
                    worksheet.Name = DateTime.Now.ToString("dd-MM-yyyy");
                    worksheet.Cells.Style.Font.Size = 11;
                    worksheet.Cells.Style.Font.Name = "Calibri";
                    

                    Color colFromHex_H1 = System.Drawing.ColorTranslator.FromHtml("#8497b0");
                    Color col_color_Red = System.Drawing.ColorTranslator.FromHtml("#ff0000");

                    worksheet.Row(2).Height = 40;
                    worksheet.Row(3).Height = 40;
                    worksheet.Row(3).Style.WrapText = true;

                    worksheet.Cells[2, 1].Value = "Total";
                    worksheet.Cells[2, 1, 2, 39].Style.Font.Bold = true;
                    worksheet.Cells[2, 1, 2, 39].Style.Font.Size = 11;
                    worksheet.Cells[2, 1, 2, 39].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[2, 1, 2, 39].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[2, 1, 2, 39].Style.Font.Size = 11;

                    worksheet.Cells[3, 1, 3, 39].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[3, 1, 3, 39].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    worksheet.Cells[3, 1, 3, 39].Style.Font.Size = 10;
                    worksheet.Cells[3, 1, 3, 39].Style.Font.Bold = true;

                    worksheet.Cells[3, 1, 3, 39].AutoFilter = true;

                    var cellBackgroundColor1 = worksheet.Cells[3, 1, 3, 39].Style.Fill;
                    cellBackgroundColor1.PatternType = ExcelFillStyle.Solid;
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                    cellBackgroundColor1.BackgroundColor.SetColor(colFromHex);

                    #endregion

                    #region Header Name Declaration

                    worksheet.Cells[3, 1].Value = "Image";
                    worksheet.Cells[3, 2].Value = "Video";
                    worksheet.Cells[3, 3].Value = "Ref No";
                    worksheet.Cells[3, 4].Value = "Shape";
                    worksheet.Cells[3, 5].Value = "Pointer";
                    worksheet.Cells[3, 6].Value = "Color";
                    worksheet.Cells[3, 7].Value = "Clarity";
                    worksheet.Cells[3, 8].Value = "Cts";
                    worksheet.Cells[3, 9].Value = "Cut";
                    worksheet.Cells[3, 10].Value = "Polish";
                    worksheet.Cells[3, 11].Value = "Symm";
                    worksheet.Cells[3, 12].Value = "Fls";
                    worksheet.Cells[3, 13].Value = "Cert No";
                    worksheet.Cells[3, 14].Value = "Supplier Name";
                    worksheet.Cells[3, 15].Value = "Supplier Stone Id";
                    worksheet.Cells[3, 16].Value = "Rap Rate($)";
                    worksheet.Cells[3, 17].Value = "Rap Amount($)";
                    worksheet.Cells[3, 18].Value = "Supplier Cost Disc(%)";
                    worksheet.Cells[3, 19].Value = "Supplier Cost Value($)";
                    worksheet.Cells[3, 20].Value = "Offer Disc(%)";
                    worksheet.Cells[3, 21].Value = "Offer Value($)";
                    worksheet.Cells[3, 22].Value = "Length";
                    worksheet.Cells[3, 23].Value = "Width";
                    worksheet.Cells[3, 24].Value = "Depth";
                    worksheet.Cells[3, 25].Value = "Depth (%)";
                    worksheet.Cells[3, 26].Value = "Table (%)";
                    worksheet.Cells[3, 27].Value = "Key To Symbol";
                    worksheet.Cells[3, 28].Value = "Crown Angle";
                    worksheet.Cells[3, 29].Value = "Crown Height";
                    worksheet.Cells[3, 30].Value = "Pav Angle";
                    worksheet.Cells[3, 31].Value = "Pav Height";
                    worksheet.Cells[3, 32].Value = "Table Natts";
                    worksheet.Cells[3, 33].Value = "Crown Natts";
                    worksheet.Cells[3, 34].Value = "Table Inclusion";
                    worksheet.Cells[3, 35].Value = "Crown Inclusion";
                    worksheet.Cells[3, 36].Value = "Table Open";
                    worksheet.Cells[3, 37].Value = "Girdle Open";
                    worksheet.Cells[3, 38].Value = "Crown Open";
                    worksheet.Cells[3, 39].Value = "Pavilion Open";


                    ExcelStyle cellStyleHeader1 = worksheet.Cells[3, 1, 3, 39].Style;
                    cellStyleHeader1.Border.Left.Style = cellStyleHeader1.Border.Right.Style
                            = cellStyleHeader1.Border.Top.Style = cellStyleHeader1.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    #endregion

                    #region Set AutoFit and Decimal Number Format

                    worksheet.View.FreezePanes(4, 1);

                    worksheet.Cells[3, 1].AutoFitColumns(7);               
                    worksheet.Cells[3, 2].AutoFitColumns(7);               
                    worksheet.Cells[3, 3].AutoFitColumns(13);              
                    worksheet.Cells[3, 4].AutoFitColumns(12);              
                    worksheet.Cells[3, 5].AutoFitColumns(9);              
                    worksheet.Cells[3, 6].AutoFitColumns(7);               
                    worksheet.Cells[3, 7].AutoFitColumns(7);               
                    worksheet.Cells[3, 8].AutoFitColumns(7);               
                    worksheet.Cells[3, 9].AutoFitColumns(7);               
                    worksheet.Cells[3, 10].AutoFitColumns(7);              
                    worksheet.Cells[3, 11].AutoFitColumns(7);
                    worksheet.Cells[3, 12].AutoFitColumns(7);
                    worksheet.Cells[3, 13].AutoFitColumns(12);
                    worksheet.Cells[3, 14].AutoFitColumns(23);
                    worksheet.Cells[3, 15].AutoFitColumns(13);
                    worksheet.Cells[3, 16].AutoFitColumns(10);
                    worksheet.Cells[3, 17].AutoFitColumns(10);
                    worksheet.Cells[3, 18].AutoFitColumns(13);
                    worksheet.Cells[3, 19].AutoFitColumns(13);
                    worksheet.Cells[3, 20].AutoFitColumns(13);
                    worksheet.Cells[3, 21].AutoFitColumns(13);
                    worksheet.Cells[3, 22].AutoFitColumns(6.5);
                    worksheet.Cells[3, 23].AutoFitColumns(6.5);
                    worksheet.Cells[3, 24].AutoFitColumns(6.5);
                    worksheet.Cells[3, 25].AutoFitColumns(6.5);
                    worksheet.Cells[3, 26].AutoFitColumns(6.5);
                    worksheet.Cells[3, 27].AutoFitColumns(25);
                    worksheet.Cells[3, 28].AutoFitColumns(7);
                    worksheet.Cells[3, 29].AutoFitColumns(7);
                    worksheet.Cells[3, 30].AutoFitColumns(7);
                    worksheet.Cells[3, 31].AutoFitColumns(7);
                    worksheet.Cells[3, 32].AutoFitColumns(7.5);
                    worksheet.Cells[3, 33].AutoFitColumns(7.5);
                    worksheet.Cells[3, 34].AutoFitColumns(7.5);
                    worksheet.Cells[3, 35].AutoFitColumns(7.5);
                    worksheet.Cells[3, 36].AutoFitColumns(7);
                    worksheet.Cells[3, 37].AutoFitColumns(7);
                    worksheet.Cells[3, 38].AutoFitColumns(7);
                    worksheet.Cells[3, 39].AutoFitColumns(7);


                    //Set Cell Faoat value with Alignment
                    worksheet.Cells[inStartIndex, 1, inEndCounter, 39].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    #endregion

                    var asTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo;

                    int pairNo, tempPairNo = 0;
                    bool PairLastColumn = false;
                    for (i = inStartIndex; i < inEndCounter; i++)
                    {
                        #region Assigns Value to Cell


                        Image_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image_URL"]);
                        if (Image_URL != "")
                        {
                            worksheet.Cells[inwrkrow, 1].Formula = "=HYPERLINK(\"" + Image_URL + "\",\" Image \")";
                            worksheet.Cells[inwrkrow, 1].Style.Font.UnderLine = true;
                            worksheet.Cells[inwrkrow, 1].Style.Font.Color.SetColor(Color.Blue);
                        }

                        Video_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Video_URL"]);
                        if (Video_URL != "")
                        {
                            worksheet.Cells[inwrkrow, 2].Formula = "=HYPERLINK(\"" + Video_URL + "\",\" Video \")";
                            worksheet.Cells[inwrkrow, 2].Style.Font.UnderLine = true;
                            worksheet.Cells[inwrkrow, 2].Style.Font.Color.SetColor(Color.Blue);
                        }

                        worksheet.Cells[inwrkrow, 3].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Ref_No"]);
                        worksheet.Cells[inwrkrow, 4].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shape"]);
                        worksheet.Cells[inwrkrow, 5].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pointer"]);
                        worksheet.Cells[inwrkrow, 6].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Color"]);
                        worksheet.Cells[inwrkrow, 7].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Clarity"]);
                        worksheet.Cells[inwrkrow, 8].Value = ((dtDiamonds.Rows[i - inStartIndex]["Cts"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Cts"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Cts"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 9].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]);
                        worksheet.Cells[inwrkrow, 10].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Polish"]);
                        worksheet.Cells[inwrkrow, 11].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Symm"]);
                        worksheet.Cells[inwrkrow, 12].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fls"]);
                        worksheet.Cells[inwrkrow, 13].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                        worksheet.Cells[inwrkrow, 14].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["SupplierName"]);
                        worksheet.Cells[inwrkrow, 15].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Supplier_Stone_Id"]);
                        worksheet.Cells[inwrkrow, 16].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 17].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 18].Value = ((dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_DISC"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 19].Value = ((dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["SUPPLIER_COST_VALUE"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 20].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 21].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 22].Value = ((dtDiamonds.Rows[i - inStartIndex]["Length"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Length"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Length"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 23].Value = ((dtDiamonds.Rows[i - inStartIndex]["Width"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Width"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Width"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 24].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Depth"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 25].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Depth_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth_Per"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 26].Value = ((dtDiamonds.Rows[i - inStartIndex]["Table_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Table_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Table_Per"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 27].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Key_To_Symboll"]);

                        worksheet.Cells[inwrkrow, 28].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 29].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Height"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Crown_Height"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Height"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 30].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 31].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Height"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Height"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Height"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 32].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Natts"]);
                        worksheet.Cells[inwrkrow, 33].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"]);
                        worksheet.Cells[inwrkrow, 34].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Inclusion"]);
                        worksheet.Cells[inwrkrow, 35].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Inclusion"]);
                        worksheet.Cells[inwrkrow, 36].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Open"]);
                        worksheet.Cells[inwrkrow, 37].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle_Open"]);
                        worksheet.Cells[inwrkrow, 38].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Open"]);
                        worksheet.Cells[inwrkrow, 39].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pav_Open"]);


                        inwrkrow++;

                        #endregion
                    }

                    worksheet.Cells[inStartIndex, 1, (inwrkrow - 1), 39].Style.Font.Size = 9;
                    worksheet.Cells[inStartIndex, 18, (inwrkrow - 1), 21].Style.Font.Bold = true;

                    worksheet.Cells[inStartIndex, 8, (inwrkrow - 1), 8].Style.Numberformat.Format = "0.00";
                    worksheet.Cells[inStartIndex, 16, (inwrkrow - 1), 21].Style.Numberformat.Format = "#,##0.00"; 
                    worksheet.Cells[inStartIndex, 28, (inwrkrow - 1), 31].Style.Numberformat.Format = "0.00";
                    worksheet.Cells[inStartIndex, 22, (inwrkrow - 1), 26].Style.Numberformat.Format = "0.00";
                    


                    worksheet.Cells[2, 3].Formula = "ROUND(SUBTOTAL(102,"+ GetExcelColumnLetter(8) + "" + inStartIndex + ":"+ GetExcelColumnLetter(8) + "" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[2, 3].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[2, 3].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[2, 3].Style.Numberformat.Format = "#,##";

                    ExcelStyle cellStyleHeader_Total = worksheet.Cells[2, 3].Style;
                    cellStyleHeader_Total.Border.Left.Style = cellStyleHeader_Total.Border.Right.Style
                            = cellStyleHeader_Total.Border.Top.Style = cellStyleHeader_Total.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[2, 8].Formula = "ROUND(SUBTOTAL(109,"+ GetExcelColumnLetter(8) + "" + inStartIndex + ":"+ GetExcelColumnLetter(8) + "" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[2, 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[2, 8].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[2, 8].Style.Numberformat.Format = "#,##0.00";

                    ExcelStyle cellStyleHeader_Totalcarat = worksheet.Cells[2, 8].Style;
                    cellStyleHeader_Totalcarat.Border.Left.Style = cellStyleHeader_Totalcarat.Border.Right.Style
                            = cellStyleHeader_Totalcarat.Border.Top.Style = cellStyleHeader_Totalcarat.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    
                    worksheet.Cells[2, 18].Formula = "IF(SUBTOTAL(109,"+ GetExcelColumnLetter(17) + "" + inStartIndex + ": "+ GetExcelColumnLetter(17) + "" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109,"+ GetExcelColumnLetter(19) + "" + inStartIndex + ":"+ GetExcelColumnLetter(19) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109,"+ GetExcelColumnLetter(17) + "" + inStartIndex + ":"+ GetExcelColumnLetter(17) + "" + (inwrkrow - 1) + ")))*(-100),2))";
                    worksheet.Cells[2, 18].Style.Numberformat.Format = "#,##0.00";

                    ExcelStyle cellStyleHeader_TotalDis = worksheet.Cells[2, 18].Style;
                    cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                            = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[2, 19].Formula = "ROUND(SUBTOTAL(109,"+ GetExcelColumnLetter(19) + "" + inStartIndex + ":"+ GetExcelColumnLetter(19) + "" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[2, 19].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[2, 19].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[2, 19].Style.Numberformat.Format = "#,##0";

                    ExcelStyle cellStyleHeader_TotalNet = worksheet.Cells[2, 19].Style;
                    cellStyleHeader_TotalNet.Border.Left.Style = cellStyleHeader_TotalNet.Border.Right.Style
                            = cellStyleHeader_TotalNet.Border.Top.Style = cellStyleHeader_TotalNet.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;


                    worksheet.Cells[2, 20].Formula = "IF(SUBTOTAL(109," + GetExcelColumnLetter(17) + "" + inStartIndex + ": " + GetExcelColumnLetter(17) + "" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109," + GetExcelColumnLetter(21) + "" + inStartIndex + ":" + GetExcelColumnLetter(21) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109," + GetExcelColumnLetter(17) + "" + inStartIndex + ":" + GetExcelColumnLetter(17) + "" + (inwrkrow - 1) + ")))*(-100),2))";
                    worksheet.Cells[2, 20].Style.Numberformat.Format = "#,##0.00";

                    cellStyleHeader_TotalDis = worksheet.Cells[2, 20].Style;
                    cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                            = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[2, 21].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(21) + "" + inStartIndex + ":" + GetExcelColumnLetter(21) + "" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[2, 21].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[2, 21].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[2, 21].Style.Numberformat.Format = "#,##0";

                    cellStyleHeader_TotalNet = worksheet.Cells[2, 21].Style;
                    cellStyleHeader_TotalNet.Border.Left.Style = cellStyleHeader_TotalNet.Border.Right.Style
                            = cellStyleHeader_TotalNet.Border.Top.Style = cellStyleHeader_TotalNet.Border.Bottom.Style
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
                Lib.Model.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }
        public static void OrderHistory_Excel(DataTable dtDiamonds, string _strFolderPath, string _strFilePath, string UserTypeList)
        {
            try
            {
                if (UserTypeList == "Admin" || UserTypeList == "Employee" || UserTypeList == "Buyer")
                {
                    using (ExcelPackage p = new ExcelPackage())
                    {
                        int inStartIndex = 4;
                        int inwrkrow = 4;
                        int inEndCounter = dtDiamonds.Rows.Count + inStartIndex;
                        int TotalRow = dtDiamonds.Rows.Count;
                        int i;
                        string values_1, Image_URL, Video_URL, cut, status, ForCust_Hold;
                        Int64 number_1;
                        bool success1;

                        Color colFromHex_Pointer = System.Drawing.ColorTranslator.FromHtml("#c6e0b4");
                        Color colFromHex_Dis = System.Drawing.ColorTranslator.FromHtml("#ccffff");
                        Color colFromHexTotal = System.Drawing.ColorTranslator.FromHtml("#d9e1f2");
                        Color tcpg_bg_clr = System.Drawing.ColorTranslator.FromHtml("#fff2cc");

                        #region Company Detail on Header

                        p.Workbook.Properties.Author = "SUNRISE DIAMOND";
                        p.Workbook.Properties.Title = "SUNRISE DIAMOND PVT. LTD.";
                        p.Workbook.Worksheets.Add("OrderHistory");

                        ExcelWorksheet worksheet = p.Workbook.Worksheets[1];
                        worksheet.Name = DateTime.Now.ToString("dd-MM-yyyy");
                        worksheet.Cells.Style.Font.Size = 11;
                        worksheet.Cells.Style.Font.Name = "Calibri";


                        Color colFromHex_H1 = System.Drawing.ColorTranslator.FromHtml("#8497b0");
                        Color col_color_Red = System.Drawing.ColorTranslator.FromHtml("#ff0000");

                        worksheet.Row(2).Height = 40;
                        worksheet.Row(3).Height = 40;
                        worksheet.Row(3).Style.WrapText = true;

                        worksheet.Cells[2, 1].Value = "Total";
                        worksheet.Cells[2, 1, 2, 46].Style.Font.Bold = true;
                        worksheet.Cells[2, 1, 2, 46].Style.Font.Size = 11;
                        worksheet.Cells[2, 1, 2, 46].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[2, 1, 2, 46].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[2, 1, 2, 46].Style.Font.Size = 11;

                        worksheet.Cells[3, 1, 3, 46].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[3, 1, 3, 46].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        worksheet.Cells[3, 1, 3, 46].Style.Font.Size = 10;
                        worksheet.Cells[3, 1, 3, 46].Style.Font.Bold = true;

                        worksheet.Cells[3, 1, 3, 46].AutoFilter = true;

                        var cellBackgroundColor1 = worksheet.Cells[3, 1, 3, 46].Style.Fill;
                        cellBackgroundColor1.PatternType = ExcelFillStyle.Solid;
                        Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                        cellBackgroundColor1.BackgroundColor.SetColor(colFromHex);

                        #endregion

                        #region Header Name Declaration

                        worksheet.Cells[3, 1].Value = "Image";
                        worksheet.Cells[3, 2].Value = "Video";
                        worksheet.Cells[3, 3].Value = "Order Date";
                        worksheet.Cells[3, 4].Value = "Order No";
                        worksheet.Cells[3, 5].Value = "Customer Name";
                        worksheet.Cells[3, 6].Value = "Company Name";
                        worksheet.Cells[3, 7].Value = "Ref No";
                        worksheet.Cells[3, 8].Value = "Lab";
                        worksheet.Cells[3, 9].Value = "Cert No";
                        worksheet.Cells[3, 10].Value = "Shape";
                        worksheet.Cells[3, 11].Value = "Pointer";
                        worksheet.Cells[3, 12].Value = "BGM";
                        worksheet.Cells[3, 13].Value = "Color";
                        worksheet.Cells[3, 14].Value = "Clarity";
                        worksheet.Cells[3, 15].Value = "Cts";
                        worksheet.Cells[3, 16].Value = "Rap Rate($)";
                        worksheet.Cells[3, 17].Value = "Rap Amount($)";
                        worksheet.Cells[3, 18].Value = "Offer Disc(%)";
                        worksheet.Cells[3, 19].Value = "Offer Value($)";
                        worksheet.Cells[3, 20].Value = "Price Cts";
                        worksheet.Cells[3, 21].Value = "Cut";
                        worksheet.Cells[3, 22].Value = "Polish";
                        worksheet.Cells[3, 23].Value = "Symm";
                        worksheet.Cells[3, 24].Value = "Fls";
                        worksheet.Cells[3, 25].Value = "RATIO";
                        worksheet.Cells[3, 26].Value = "Key To Symbol";
                        worksheet.Cells[3, 27].Value = "Length";
                        worksheet.Cells[3, 28].Value = "Width";
                        worksheet.Cells[3, 29].Value = "Depth";
                        worksheet.Cells[3, 30].Value = "Depth (%)";
                        worksheet.Cells[3, 31].Value = "Table (%)";
                        worksheet.Cells[3, 32].Value = "Comment";
                        worksheet.Cells[3, 33].Value = "Girdle(%)";
                        worksheet.Cells[3, 34].Value = "Crown Angle";
                        worksheet.Cells[3, 35].Value = "Crown Height";
                        worksheet.Cells[3, 36].Value = "Pav Angle";
                        worksheet.Cells[3, 37].Value = "Pav Height";
                        worksheet.Cells[3, 38].Value = "Table Natts";
                        worksheet.Cells[3, 39].Value = "Crown Natts";
                        worksheet.Cells[3, 40].Value = "Table Inclusion";
                        worksheet.Cells[3, 41].Value = "Crown Inclusion";
                        worksheet.Cells[3, 42].Value = "Culet";
                        worksheet.Cells[3, 43].Value = "Table Open";
                        worksheet.Cells[3, 44].Value = "Girdle Open";
                        worksheet.Cells[3, 45].Value = "Crown Open";
                        worksheet.Cells[3, 46].Value = "Pavilion Open";


                        ExcelStyle cellStyleHeader1 = worksheet.Cells[3, 1, 3, 46].Style;
                        cellStyleHeader1.Border.Left.Style = cellStyleHeader1.Border.Right.Style
                                = cellStyleHeader1.Border.Top.Style = cellStyleHeader1.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;

                        #endregion

                        #region Set AutoFit and Decimal Number Format

                        worksheet.View.FreezePanes(4, 1);

                        worksheet.Cells[3, 1].AutoFitColumns(7);
                        worksheet.Cells[3, 2].AutoFitColumns(7);
                        worksheet.Cells[3, 3].AutoFitColumns(10);
                        worksheet.Cells[3, 4].AutoFitColumns(8);
                        worksheet.Cells[3, 5].AutoFitColumns(18);
                        worksheet.Cells[3, 6].AutoFitColumns(23);
                        worksheet.Cells[3, 7].AutoFitColumns(13);
                        worksheet.Cells[3, 8].AutoFitColumns(7);
                        worksheet.Cells[3, 9].AutoFitColumns(12);
                        worksheet.Cells[3, 10].AutoFitColumns(12);
                        worksheet.Cells[3, 11].AutoFitColumns(9);
                        worksheet.Cells[3, 12].AutoFitColumns(8.5);
                        worksheet.Cells[3, 13].AutoFitColumns(7);
                        worksheet.Cells[3, 14].AutoFitColumns(7);
                        worksheet.Cells[3, 15].AutoFitColumns(7);
                        worksheet.Cells[3, 16].AutoFitColumns(10);
                        worksheet.Cells[3, 17].AutoFitColumns(10);
                        worksheet.Cells[3, 18].AutoFitColumns(13);
                        worksheet.Cells[3, 19].AutoFitColumns(13);
                        worksheet.Cells[3, 20].AutoFitColumns(10);
                        worksheet.Cells[3, 21].AutoFitColumns(7);
                        worksheet.Cells[3, 22].AutoFitColumns(7);
                        worksheet.Cells[3, 23].AutoFitColumns(7);
                        worksheet.Cells[3, 24].AutoFitColumns(7);
                        worksheet.Cells[3, 25].AutoFitColumns(7);
                        worksheet.Cells[3, 26].AutoFitColumns(25);
                        worksheet.Cells[3, 27].AutoFitColumns(6.5);
                        worksheet.Cells[3, 28].AutoFitColumns(6.5);
                        worksheet.Cells[3, 29].AutoFitColumns(6.5);
                        worksheet.Cells[3, 30].AutoFitColumns(6.5);
                        worksheet.Cells[3, 31].AutoFitColumns(6.5);
                        worksheet.Cells[3, 32].AutoFitColumns(25);
                        worksheet.Cells[3, 33].AutoFitColumns(7);
                        worksheet.Cells[3, 34].AutoFitColumns(7);
                        worksheet.Cells[3, 35].AutoFitColumns(7);
                        worksheet.Cells[3, 36].AutoFitColumns(7);
                        worksheet.Cells[3, 37].AutoFitColumns(7);
                        worksheet.Cells[3, 38].AutoFitColumns(7.5);
                        worksheet.Cells[3, 39].AutoFitColumns(7.5);
                        worksheet.Cells[3, 40].AutoFitColumns(7.5);
                        worksheet.Cells[3, 41].AutoFitColumns(7.5);
                        worksheet.Cells[3, 42].AutoFitColumns(7);
                        worksheet.Cells[3, 43].AutoFitColumns(7);
                        worksheet.Cells[3, 44].AutoFitColumns(7);
                        worksheet.Cells[3, 45].AutoFitColumns(7);
                        worksheet.Cells[3, 46].AutoFitColumns(7);


                        //Set Cell Faoat value with Alignment
                        worksheet.Cells[inStartIndex, 1, inEndCounter, 46].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        #endregion

                        var asTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo;

                        int pairNo, tempPairNo = 0;
                        bool PairLastColumn = false;
                        for (i = inStartIndex; i < inEndCounter; i++)
                        {
                            #region Assigns Value to Cell


                            Image_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image_URL"]);
                            if (Image_URL != "")
                            {
                                worksheet.Cells[inwrkrow, 1].Formula = "=HYPERLINK(\"" + Image_URL + "\",\" Image \")";
                                worksheet.Cells[inwrkrow, 1].Style.Font.UnderLine = true;
                                worksheet.Cells[inwrkrow, 1].Style.Font.Color.SetColor(Color.Blue);
                            }

                            Video_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Video_URL"]);
                            if (Video_URL != "")
                            {
                                worksheet.Cells[inwrkrow, 2].Formula = "=HYPERLINK(\"" + Video_URL + "\",\" Video \")";
                                worksheet.Cells[inwrkrow, 2].Style.Font.UnderLine = true;
                                worksheet.Cells[inwrkrow, 2].Style.Font.Color.SetColor(Color.Blue);
                            }

                            worksheet.Cells[inwrkrow, 3].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["OrderDate"]);

                            worksheet.Cells[inwrkrow, 4].Value = ((dtDiamonds.Rows[i - inStartIndex]["OrderId"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["OrderId"].GetType().Name != "DBNull" ?
                                      Convert.ToInt32(dtDiamonds.Rows[i - inStartIndex]["OrderId"]) : ((Int32?)null)) : null);

                            worksheet.Cells[inwrkrow, 5].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["UserFullName"]);
                            worksheet.Cells[inwrkrow, 6].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["CompName"]);
                            worksheet.Cells[inwrkrow, 7].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Ref_No"]);

                            worksheet.Cells[inwrkrow, 8].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]);
                            string URL = "";
                            string Certificate_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_URL"]);

                            if (!string.IsNullOrEmpty(Certificate_URL))
                            {
                                URL = Certificate_URL;
                            }
                            else if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "GIA")
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
                                worksheet.Cells[inwrkrow, 8].Formula = "=HYPERLINK(\"" + Convert.ToString(URL) + "\",\" " + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) + " \")";
                                worksheet.Cells[inwrkrow, 8].Style.Font.UnderLine = true;
                                worksheet.Cells[inwrkrow, 8].Style.Font.Color.SetColor(Color.Blue);
                            }

                            worksheet.Cells[inwrkrow, 9].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                            worksheet.Cells[inwrkrow, 10].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shape"]);
                            worksheet.Cells[inwrkrow, 11].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pointer"]);
                            worksheet.Cells[inwrkrow, 12].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["BGM"]);
                            worksheet.Cells[inwrkrow, 13].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Color"]);
                            worksheet.Cells[inwrkrow, 14].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Clarity"]);
                            worksheet.Cells[inwrkrow, 15].Value = ((dtDiamonds.Rows[i - inStartIndex]["Cts"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Cts"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Cts"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 16].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 17].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 18].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 19].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 20].Value = ((dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 21].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]);
                            worksheet.Cells[inwrkrow, 22].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Polish"]);
                            worksheet.Cells[inwrkrow, 23].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Symm"]);
                            worksheet.Cells[inwrkrow, 24].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fls"]);
                            worksheet.Cells[inwrkrow, 25].Value = ((dtDiamonds.Rows[i - inStartIndex]["RATIO"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["RATIO"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["RATIO"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 26].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Key_To_Symboll"]);
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

                            worksheet.Cells[inwrkrow, 32].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab_Comments"]);
                            worksheet.Cells[inwrkrow, 33].Value = ((dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 34].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 35].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Height"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Crown_Height"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Height"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 36].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 37].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Height"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Height"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Height"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 38].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Natts"]);
                            worksheet.Cells[inwrkrow, 39].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"]);
                            worksheet.Cells[inwrkrow, 40].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Inclusion"]);
                            worksheet.Cells[inwrkrow, 41].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Inclusion"]);
                            worksheet.Cells[inwrkrow, 42].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Culet"]);
                            worksheet.Cells[inwrkrow, 43].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Open"]);
                            worksheet.Cells[inwrkrow, 44].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle_Open"]);
                            worksheet.Cells[inwrkrow, 45].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Open"]);
                            worksheet.Cells[inwrkrow, 46].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pav_Open"]);


                            inwrkrow++;

                            #endregion
                        }

                        worksheet.Cells[inStartIndex, 1, (inwrkrow - 1), 46].Style.Font.Size = 9;
                        worksheet.Cells[inStartIndex, 18, (inwrkrow - 1), 19].Style.Font.Bold = true;

                        worksheet.Cells[inStartIndex, 15, (inwrkrow - 1), 20].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[inStartIndex, 25, (inwrkrow - 1), 25].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[inStartIndex, 16, (inwrkrow - 1), 19].Style.Numberformat.Format = "#,##0.00";

                        worksheet.Cells[inStartIndex, 27, (inwrkrow - 1), 31].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[inStartIndex, 33, (inwrkrow - 1), 37].Style.Numberformat.Format = "0.00";



                        worksheet.Cells[2, 7].Formula = "ROUND(SUBTOTAL(102," + GetExcelColumnLetter(15) + "" + inStartIndex + ":" + GetExcelColumnLetter(15) + "" + (inwrkrow - 1) + "),2)";
                        worksheet.Cells[2, 7].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, 7].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                        worksheet.Cells[2, 7].Style.Numberformat.Format = "#,##";

                        ExcelStyle cellStyleHeader_Total = worksheet.Cells[2, 7].Style;
                        cellStyleHeader_Total.Border.Left.Style = cellStyleHeader_Total.Border.Right.Style
                                = cellStyleHeader_Total.Border.Top.Style = cellStyleHeader_Total.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;

                        worksheet.Cells[2, 15].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(15) + "" + inStartIndex + ":" + GetExcelColumnLetter(15) + "" + (inwrkrow - 1) + "),2)";
                        worksheet.Cells[2, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, 15].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                        worksheet.Cells[2, 15].Style.Numberformat.Format = "#,##0.00";

                        ExcelStyle cellStyleHeader_Totalcarat = worksheet.Cells[2, 15].Style;
                        cellStyleHeader_Totalcarat.Border.Left.Style = cellStyleHeader_Totalcarat.Border.Right.Style
                                = cellStyleHeader_Totalcarat.Border.Top.Style = cellStyleHeader_Totalcarat.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;


                        worksheet.Cells[2, 18].Formula = "IF(SUBTOTAL(109," + GetExcelColumnLetter(17) + "" + inStartIndex + ": " + GetExcelColumnLetter(17) + "" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109," + GetExcelColumnLetter(19) + "" + inStartIndex + ":" + GetExcelColumnLetter(19) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109," + GetExcelColumnLetter(17) + "" + inStartIndex + ":" + GetExcelColumnLetter(17) + "" + (inwrkrow - 1) + ")))*(-100),2))";
                        worksheet.Cells[2, 18].Style.Numberformat.Format = "#,##0.00";

                        ExcelStyle cellStyleHeader_TotalDis = worksheet.Cells[2, 18].Style;
                        cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                                = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;

                        worksheet.Cells[2, 19].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(19) + "" + inStartIndex + ":" + GetExcelColumnLetter(19) + "" + (inwrkrow - 1) + "),2)";
                        worksheet.Cells[2, 19].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, 19].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                        worksheet.Cells[2, 19].Style.Numberformat.Format = "#,##0";

                        ExcelStyle cellStyleHeader_TotalNet = worksheet.Cells[2, 19].Style;
                        cellStyleHeader_TotalNet.Border.Left.Style = cellStyleHeader_TotalNet.Border.Right.Style
                                = cellStyleHeader_TotalNet.Border.Top.Style = cellStyleHeader_TotalNet.Border.Bottom.Style
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
                else
                {
                    using (ExcelPackage p = new ExcelPackage())
                    {
                        int inStartIndex = 4;
                        int inwrkrow = 4;
                        int inEndCounter = dtDiamonds.Rows.Count + inStartIndex;
                        int TotalRow = dtDiamonds.Rows.Count;
                        int i;
                        string values_1, Image_URL, Video_URL, cut, status, ForCust_Hold;
                        Int64 number_1;
                        bool success1;

                        Color colFromHex_Pointer = System.Drawing.ColorTranslator.FromHtml("#c6e0b4");
                        Color colFromHex_Dis = System.Drawing.ColorTranslator.FromHtml("#ccffff");
                        Color colFromHexTotal = System.Drawing.ColorTranslator.FromHtml("#d9e1f2");
                        Color tcpg_bg_clr = System.Drawing.ColorTranslator.FromHtml("#fff2cc");

                        #region Company Detail on Header

                        p.Workbook.Properties.Author = "SUNRISE DIAMOND";
                        p.Workbook.Properties.Title = "SUNRISE DIAMOND PVT. LTD.";
                        p.Workbook.Worksheets.Add("OrderHistory");

                        ExcelWorksheet worksheet = p.Workbook.Worksheets[1];
                        worksheet.Name = DateTime.Now.ToString("dd-MM-yyyy");
                        worksheet.Cells.Style.Font.Size = 11;
                        worksheet.Cells.Style.Font.Name = "Calibri";


                        Color colFromHex_H1 = System.Drawing.ColorTranslator.FromHtml("#8497b0");
                        Color col_color_Red = System.Drawing.ColorTranslator.FromHtml("#ff0000");

                        worksheet.Row(2).Height = 40;
                        worksheet.Row(3).Height = 40;
                        worksheet.Row(3).Style.WrapText = true;

                        worksheet.Cells[2, 1].Value = "Total";
                        worksheet.Cells[2, 1, 2, 44].Style.Font.Bold = true;
                        worksheet.Cells[2, 1, 2, 44].Style.Font.Size = 11;
                        worksheet.Cells[2, 1, 2, 44].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[2, 1, 2, 44].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[2, 1, 2, 44].Style.Font.Size = 11;

                        worksheet.Cells[3, 1, 3, 44].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[3, 1, 3, 44].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        worksheet.Cells[3, 1, 3, 44].Style.Font.Size = 10;
                        worksheet.Cells[3, 1, 3, 44].Style.Font.Bold = true;

                        worksheet.Cells[3, 1, 3, 44].AutoFilter = true;

                        var cellBackgroundColor1 = worksheet.Cells[3, 1, 3, 44].Style.Fill;
                        cellBackgroundColor1.PatternType = ExcelFillStyle.Solid;
                        Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                        cellBackgroundColor1.BackgroundColor.SetColor(colFromHex);

                        #endregion

                        #region Header Name Declaration

                        worksheet.Cells[3, 1].Value = "Image";
                        worksheet.Cells[3, 2].Value = "Video";
                        worksheet.Cells[3, 3].Value = "Order Date";
                        worksheet.Cells[3, 4].Value = "Order No";
                        worksheet.Cells[3, 5].Value = "Ref No";
                        worksheet.Cells[3, 6].Value = "Lab";
                        worksheet.Cells[3, 7].Value = "Cert No";
                        worksheet.Cells[3, 8].Value = "Shape";
                        worksheet.Cells[3, 9].Value = "Pointer";
                        worksheet.Cells[3, 10].Value = "BGM";
                        worksheet.Cells[3, 11].Value = "Color";
                        worksheet.Cells[3, 12].Value = "Clarity";
                        worksheet.Cells[3, 13].Value = "Cts";
                        worksheet.Cells[3, 14].Value = "Rap Rate($)";
                        worksheet.Cells[3, 15].Value = "Rap Amount($)";
                        worksheet.Cells[3, 16].Value = "Offer Disc(%)";
                        worksheet.Cells[3, 17].Value = "Offer Value($)";
                        worksheet.Cells[3, 18].Value = "Price Cts";
                        worksheet.Cells[3, 19].Value = "Cut";
                        worksheet.Cells[3, 20].Value = "Polish";
                        worksheet.Cells[3, 21].Value = "Symm";
                        worksheet.Cells[3, 22].Value = "Fls";
                        worksheet.Cells[3, 23].Value = "RATIO";
                        worksheet.Cells[3, 24].Value = "Key To Symbol";
                        worksheet.Cells[3, 25].Value = "Length";
                        worksheet.Cells[3, 26].Value = "Width";
                        worksheet.Cells[3, 27].Value = "Depth";
                        worksheet.Cells[3, 28].Value = "Depth (%)";
                        worksheet.Cells[3, 29].Value = "Table (%)";
                        worksheet.Cells[3, 30].Value = "Comment";
                        worksheet.Cells[3, 31].Value = "Girdle(%)";
                        worksheet.Cells[3, 32].Value = "Crown Angle";
                        worksheet.Cells[3, 33].Value = "Crown Height";
                        worksheet.Cells[3, 34].Value = "Pav Angle";
                        worksheet.Cells[3, 35].Value = "Pav Height";
                        worksheet.Cells[3, 36].Value = "Table Natts";
                        worksheet.Cells[3, 37].Value = "Crown Natts";
                        worksheet.Cells[3, 38].Value = "Table Inclusion";
                        worksheet.Cells[3, 39].Value = "Crown Inclusion";
                        worksheet.Cells[3, 40].Value = "Culet";
                        worksheet.Cells[3, 41].Value = "Table Open";
                        worksheet.Cells[3, 42].Value = "Girdle Open";
                        worksheet.Cells[3, 43].Value = "Crown Open";
                        worksheet.Cells[3, 44].Value = "Pavilion Open";


                        ExcelStyle cellStyleHeader1 = worksheet.Cells[3, 1, 3, 44].Style;
                        cellStyleHeader1.Border.Left.Style = cellStyleHeader1.Border.Right.Style
                                = cellStyleHeader1.Border.Top.Style = cellStyleHeader1.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;

                        #endregion

                        #region Set AutoFit and Decimal Number Format

                        worksheet.View.FreezePanes(4, 1);

                        worksheet.Cells[3, 1].AutoFitColumns(7);
                        worksheet.Cells[3, 2].AutoFitColumns(7);
                        worksheet.Cells[3, 3].AutoFitColumns(10);
                        worksheet.Cells[3, 4].AutoFitColumns(8);
                        worksheet.Cells[3, 5].AutoFitColumns(13);
                        worksheet.Cells[3, 6].AutoFitColumns(7);
                        worksheet.Cells[3, 7].AutoFitColumns(12);
                        worksheet.Cells[3, 8].AutoFitColumns(12);
                        worksheet.Cells[3, 9].AutoFitColumns(9);
                        worksheet.Cells[3, 10].AutoFitColumns(8.5);
                        worksheet.Cells[3, 11].AutoFitColumns(7);
                        worksheet.Cells[3, 12].AutoFitColumns(7);
                        worksheet.Cells[3, 13].AutoFitColumns(7);
                        worksheet.Cells[3, 14].AutoFitColumns(10);
                        worksheet.Cells[3, 15].AutoFitColumns(10);
                        worksheet.Cells[3, 16].AutoFitColumns(13);
                        worksheet.Cells[3, 17].AutoFitColumns(13);
                        worksheet.Cells[3, 18].AutoFitColumns(10);
                        worksheet.Cells[3, 19].AutoFitColumns(7);
                        worksheet.Cells[3, 20].AutoFitColumns(7);
                        worksheet.Cells[3, 21].AutoFitColumns(7);
                        worksheet.Cells[3, 22].AutoFitColumns(7);
                        worksheet.Cells[3, 23].AutoFitColumns(7);
                        worksheet.Cells[3, 24].AutoFitColumns(25);
                        worksheet.Cells[3, 25].AutoFitColumns(6.5);
                        worksheet.Cells[3, 26].AutoFitColumns(6.5);
                        worksheet.Cells[3, 27].AutoFitColumns(6.5);
                        worksheet.Cells[3, 28].AutoFitColumns(6.5);
                        worksheet.Cells[3, 29].AutoFitColumns(6.5);
                        worksheet.Cells[3, 30].AutoFitColumns(25);
                        worksheet.Cells[3, 31].AutoFitColumns(7);
                        worksheet.Cells[3, 32].AutoFitColumns(7);
                        worksheet.Cells[3, 33].AutoFitColumns(7);
                        worksheet.Cells[3, 34].AutoFitColumns(7);
                        worksheet.Cells[3, 35].AutoFitColumns(7);
                        worksheet.Cells[3, 36].AutoFitColumns(7.5);
                        worksheet.Cells[3, 37].AutoFitColumns(7.5);
                        worksheet.Cells[3, 38].AutoFitColumns(7.5);
                        worksheet.Cells[3, 39].AutoFitColumns(7.5);
                        worksheet.Cells[3, 40].AutoFitColumns(7);
                        worksheet.Cells[3, 41].AutoFitColumns(7);
                        worksheet.Cells[3, 42].AutoFitColumns(7);
                        worksheet.Cells[3, 43].AutoFitColumns(7);
                        worksheet.Cells[3, 44].AutoFitColumns(7);


                        //Set Cell Faoat value with Alignment
                        worksheet.Cells[inStartIndex, 1, inEndCounter, 44].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        #endregion

                        var asTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo;

                        int pairNo, tempPairNo = 0;
                        bool PairLastColumn = false;
                        for (i = inStartIndex; i < inEndCounter; i++)
                        {
                            #region Assigns Value to Cell


                            Image_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image_URL"]);
                            if (Image_URL != "")
                            {
                                worksheet.Cells[inwrkrow, 1].Formula = "=HYPERLINK(\"" + Image_URL + "\",\" Image \")";
                                worksheet.Cells[inwrkrow, 1].Style.Font.UnderLine = true;
                                worksheet.Cells[inwrkrow, 1].Style.Font.Color.SetColor(Color.Blue);
                            }

                            Video_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Video_URL"]);
                            if (Video_URL != "")
                            {
                                worksheet.Cells[inwrkrow, 2].Formula = "=HYPERLINK(\"" + Video_URL + "\",\" Video \")";
                                worksheet.Cells[inwrkrow, 2].Style.Font.UnderLine = true;
                                worksheet.Cells[inwrkrow, 2].Style.Font.Color.SetColor(Color.Blue);
                            }

                            worksheet.Cells[inwrkrow, 3].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["OrderDate"]);

                            worksheet.Cells[inwrkrow, 4].Value = ((dtDiamonds.Rows[i - inStartIndex]["OrderId"] != null) ?
                                      (dtDiamonds.Rows[i - inStartIndex]["OrderId"].GetType().Name != "DBNull" ?
                                      Convert.ToInt32(dtDiamonds.Rows[i - inStartIndex]["OrderId"]) : ((Int32?)null)) : null);

                            worksheet.Cells[inwrkrow, 5].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Ref_No"]);
                            
                            worksheet.Cells[inwrkrow, 6].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]);
                            string URL = "";
                            string Certificate_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_URL"]);

                            if (!string.IsNullOrEmpty(Certificate_URL))
                            {
                                URL = Certificate_URL;
                            }
                            else if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "GIA")
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
                                worksheet.Cells[inwrkrow, 6].Formula = "=HYPERLINK(\"" + Convert.ToString(URL) + "\",\" " + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) + " \")";
                                worksheet.Cells[inwrkrow, 6].Style.Font.UnderLine = true;
                                worksheet.Cells[inwrkrow, 6].Style.Font.Color.SetColor(Color.Blue);
                            }

                            worksheet.Cells[inwrkrow, 7].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                            worksheet.Cells[inwrkrow, 8].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shape"]);
                            worksheet.Cells[inwrkrow, 9].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pointer"]);
                            worksheet.Cells[inwrkrow, 10].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["BGM"]);
                            worksheet.Cells[inwrkrow, 11].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Color"]);
                            worksheet.Cells[inwrkrow, 12].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Clarity"]);
                            worksheet.Cells[inwrkrow, 13].Value = ((dtDiamonds.Rows[i - inStartIndex]["Cts"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Cts"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Cts"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 14].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 15].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 16].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 17].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 18].Value = ((dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 19].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]);
                            worksheet.Cells[inwrkrow, 20].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Polish"]);
                            worksheet.Cells[inwrkrow, 21].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Symm"]);
                            worksheet.Cells[inwrkrow, 22].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fls"]);
                            worksheet.Cells[inwrkrow, 23].Value = ((dtDiamonds.Rows[i - inStartIndex]["RATIO"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["RATIO"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["RATIO"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 24].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Key_To_Symboll"]);
                            worksheet.Cells[inwrkrow, 25].Value = ((dtDiamonds.Rows[i - inStartIndex]["Length"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Length"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Length"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 26].Value = ((dtDiamonds.Rows[i - inStartIndex]["Width"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Width"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Width"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 27].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Depth"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 28].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Depth_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth_Per"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 29].Value = ((dtDiamonds.Rows[i - inStartIndex]["Table_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Table_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Table_Per"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 30].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab_Comments"]);
                            worksheet.Cells[inwrkrow, 31].Value = ((dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 32].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 33].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Height"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Crown_Height"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Height"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 34].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 35].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Height"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Height"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Height"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 36].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Natts"]);
                            worksheet.Cells[inwrkrow, 37].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"]);
                            worksheet.Cells[inwrkrow, 38].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Inclusion"]);
                            worksheet.Cells[inwrkrow, 39].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Inclusion"]);
                            worksheet.Cells[inwrkrow, 40].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Culet"]);
                            worksheet.Cells[inwrkrow, 41].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Open"]);
                            worksheet.Cells[inwrkrow, 42].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle_Open"]);
                            worksheet.Cells[inwrkrow, 43].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Open"]);
                            worksheet.Cells[inwrkrow, 44].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pav_Open"]);


                            inwrkrow++;

                            #endregion
                        }

                        worksheet.Cells[inStartIndex, 1, (inwrkrow - 1), 44].Style.Font.Size = 9;
                        worksheet.Cells[inStartIndex, 16, (inwrkrow - 1), 17].Style.Font.Bold = true;

                        worksheet.Cells[inStartIndex, 13, (inwrkrow - 1), 18].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[inStartIndex, 23, (inwrkrow - 1), 23].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[inStartIndex, 14, (inwrkrow - 1), 17].Style.Numberformat.Format = "#,##0.00";

                        worksheet.Cells[inStartIndex, 25, (inwrkrow - 1), 29].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[inStartIndex, 31, (inwrkrow - 1), 35].Style.Numberformat.Format = "0.00";



                        worksheet.Cells[2, 5].Formula = "ROUND(SUBTOTAL(102," + GetExcelColumnLetter(13) + "" + inStartIndex + ":" + GetExcelColumnLetter(13) + "" + (inwrkrow - 1) + "),2)";
                        worksheet.Cells[2, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, 5].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                        worksheet.Cells[2, 5].Style.Numberformat.Format = "#,##";

                        ExcelStyle cellStyleHeader_Total = worksheet.Cells[2, 5].Style;
                        cellStyleHeader_Total.Border.Left.Style = cellStyleHeader_Total.Border.Right.Style
                                = cellStyleHeader_Total.Border.Top.Style = cellStyleHeader_Total.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;

                        worksheet.Cells[2, 13].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(13) + "" + inStartIndex + ":" + GetExcelColumnLetter(13) + "" + (inwrkrow - 1) + "),2)";
                        worksheet.Cells[2, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, 13].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                        worksheet.Cells[2, 13].Style.Numberformat.Format = "#,##0.00";

                        ExcelStyle cellStyleHeader_Totalcarat = worksheet.Cells[2, 13].Style;
                        cellStyleHeader_Totalcarat.Border.Left.Style = cellStyleHeader_Totalcarat.Border.Right.Style
                                = cellStyleHeader_Totalcarat.Border.Top.Style = cellStyleHeader_Totalcarat.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;


                        worksheet.Cells[2, 16].Formula = "IF(SUBTOTAL(109," + GetExcelColumnLetter(15) + "" + inStartIndex + ": " + GetExcelColumnLetter(15) + "" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109," + GetExcelColumnLetter(17) + "" + inStartIndex + ":" + GetExcelColumnLetter(17) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109," + GetExcelColumnLetter(15) + "" + inStartIndex + ":" + GetExcelColumnLetter(15) + "" + (inwrkrow - 1) + ")))*(-100),2))";
                        worksheet.Cells[2, 16].Style.Numberformat.Format = "#,##0.00";

                        ExcelStyle cellStyleHeader_TotalDis = worksheet.Cells[2, 16].Style;
                        cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                                = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;

                        worksheet.Cells[2, 17].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(17) + "" + inStartIndex + ":" + GetExcelColumnLetter(17) + "" + (inwrkrow - 1) + "),2)";
                        worksheet.Cells[2, 17].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, 17].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                        worksheet.Cells[2, 17].Style.Numberformat.Format = "#,##0";

                        ExcelStyle cellStyleHeader_TotalNet = worksheet.Cells[2, 17].Style;
                        cellStyleHeader_TotalNet.Border.Left.Style = cellStyleHeader_TotalNet.Border.Right.Style
                                = cellStyleHeader_TotalNet.Border.Top.Style = cellStyleHeader_TotalNet.Border.Bottom.Style
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
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }
        public static void MyCart_Excel(DataTable dtDiamonds, string _strFolderPath, string _strFilePath, string UserTypeList)
        {
            try
            {
                if (UserTypeList == "Admin" || UserTypeList == "Employee" || UserTypeList == "Buyer")
                {
                    using (ExcelPackage p = new ExcelPackage())
                    {
                        int inStartIndex = 4;
                        int inwrkrow = 4;
                        int inEndCounter = dtDiamonds.Rows.Count + inStartIndex;
                        int TotalRow = dtDiamonds.Rows.Count;
                        int i;
                        string values_1, Image_URL, Video_URL, cut, status, ForCust_Hold;
                        Int64 number_1;
                        bool success1;

                        Color colFromHex_Pointer = System.Drawing.ColorTranslator.FromHtml("#c6e0b4");
                        Color colFromHex_Dis = System.Drawing.ColorTranslator.FromHtml("#ccffff");
                        Color colFromHexTotal = System.Drawing.ColorTranslator.FromHtml("#d9e1f2");
                        Color tcpg_bg_clr = System.Drawing.ColorTranslator.FromHtml("#fff2cc");

                        #region Company Detail on Header

                        p.Workbook.Properties.Author = "SUNRISE DIAMOND";
                        p.Workbook.Properties.Title = "SUNRISE DIAMOND PVT. LTD.";
                        p.Workbook.Worksheets.Add("MyCart");

                        ExcelWorksheet worksheet = p.Workbook.Worksheets[1];
                        worksheet.Name = DateTime.Now.ToString("dd-MM-yyyy");
                        worksheet.Cells.Style.Font.Size = 11;
                        worksheet.Cells.Style.Font.Name = "Calibri";


                        Color colFromHex_H1 = System.Drawing.ColorTranslator.FromHtml("#8497b0");
                        Color col_color_Red = System.Drawing.ColorTranslator.FromHtml("#ff0000");

                        worksheet.Row(2).Height = 40;
                        worksheet.Row(3).Height = 40;
                        worksheet.Row(3).Style.WrapText = true;

                        worksheet.Cells[2, 1].Value = "Total";
                        worksheet.Cells[2, 1, 2, 46].Style.Font.Bold = true;
                        worksheet.Cells[2, 1, 2, 46].Style.Font.Size = 11;
                        worksheet.Cells[2, 1, 2, 46].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[2, 1, 2, 46].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[2, 1, 2, 46].Style.Font.Size = 11;

                        worksheet.Cells[3, 1, 3, 46].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[3, 1, 3, 46].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        worksheet.Cells[3, 1, 3, 46].Style.Font.Size = 10;
                        worksheet.Cells[3, 1, 3, 46].Style.Font.Bold = true;

                        worksheet.Cells[3, 1, 3, 46].AutoFilter = true;

                        var cellBackgroundColor1 = worksheet.Cells[3, 1, 3, 46].Style.Fill;
                        cellBackgroundColor1.PatternType = ExcelFillStyle.Solid;
                        Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                        cellBackgroundColor1.BackgroundColor.SetColor(colFromHex);

                        #endregion

                        #region Header Name Declaration

                        worksheet.Cells[3, 1].Value = "Image";
                        worksheet.Cells[3, 2].Value = "Video";
                        worksheet.Cells[3, 3].Value = "Date";
                        worksheet.Cells[3, 4].Value = "Customer Name";
                        worksheet.Cells[3, 5].Value = "Company Name";
                        worksheet.Cells[3, 6].Value = "Ref No";
                        worksheet.Cells[3, 7].Value = "Supplier Ref No";
                        worksheet.Cells[3, 8].Value = "Lab";
                        worksheet.Cells[3, 9].Value = "Cert No";
                        worksheet.Cells[3, 10].Value = "Shape";
                        worksheet.Cells[3, 11].Value = "Pointer";
                        worksheet.Cells[3, 12].Value = "BGM";
                        worksheet.Cells[3, 13].Value = "Color";
                        worksheet.Cells[3, 14].Value = "Clarity";
                        worksheet.Cells[3, 15].Value = "Cts";
                        worksheet.Cells[3, 16].Value = "Rap Rate($)";
                        worksheet.Cells[3, 17].Value = "Rap Amount($)";
                        worksheet.Cells[3, 18].Value = "Offer Disc(%)";
                        worksheet.Cells[3, 19].Value = "Offer Value($)";
                        worksheet.Cells[3, 20].Value = "Price Cts";
                        worksheet.Cells[3, 21].Value = "Cut";
                        worksheet.Cells[3, 22].Value = "Polish";
                        worksheet.Cells[3, 23].Value = "Symm";
                        worksheet.Cells[3, 24].Value = "Fls";
                        worksheet.Cells[3, 25].Value = "RATIO";
                        worksheet.Cells[3, 26].Value = "Key To Symbol";
                        worksheet.Cells[3, 27].Value = "Length";
                        worksheet.Cells[3, 28].Value = "Width";
                        worksheet.Cells[3, 29].Value = "Depth";
                        worksheet.Cells[3, 30].Value = "Depth (%)";
                        worksheet.Cells[3, 31].Value = "Table (%)";
                        worksheet.Cells[3, 32].Value = "Comment";
                        worksheet.Cells[3, 33].Value = "Girdle(%)";
                        worksheet.Cells[3, 34].Value = "Crown Angle";
                        worksheet.Cells[3, 35].Value = "Crown Height";
                        worksheet.Cells[3, 36].Value = "Pav Angle";
                        worksheet.Cells[3, 37].Value = "Pav Height";
                        worksheet.Cells[3, 38].Value = "Table Natts";
                        worksheet.Cells[3, 39].Value = "Crown Natts";
                        worksheet.Cells[3, 40].Value = "Table Inclusion";
                        worksheet.Cells[3, 41].Value = "Crown Inclusion";
                        worksheet.Cells[3, 42].Value = "Culet";
                        worksheet.Cells[3, 43].Value = "Table Open";
                        worksheet.Cells[3, 44].Value = "Girdle Open";
                        worksheet.Cells[3, 45].Value = "Crown Open";
                        worksheet.Cells[3, 46].Value = "Pavilion Open";


                        ExcelStyle cellStyleHeader1 = worksheet.Cells[3, 1, 3, 46].Style;
                        cellStyleHeader1.Border.Left.Style = cellStyleHeader1.Border.Right.Style
                                = cellStyleHeader1.Border.Top.Style = cellStyleHeader1.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;

                        #endregion

                        #region Set AutoFit and Decimal Number Format

                        worksheet.View.FreezePanes(4, 1);

                        worksheet.Cells[3, 1].AutoFitColumns(7);
                        worksheet.Cells[3, 2].AutoFitColumns(7);
                        worksheet.Cells[3, 3].AutoFitColumns(10);
                        worksheet.Cells[3, 4].AutoFitColumns(18);
                        worksheet.Cells[3, 5].AutoFitColumns(23);
                        worksheet.Cells[3, 6].AutoFitColumns(13); 
                        worksheet.Cells[3, 7].AutoFitColumns(13);
                        worksheet.Cells[3, 8].AutoFitColumns(7);
                        worksheet.Cells[3, 9].AutoFitColumns(12);
                        worksheet.Cells[3, 10].AutoFitColumns(12);
                        worksheet.Cells[3, 11].AutoFitColumns(9);
                        worksheet.Cells[3, 12].AutoFitColumns(8.5);
                        worksheet.Cells[3, 13].AutoFitColumns(7);
                        worksheet.Cells[3, 14].AutoFitColumns(7);
                        worksheet.Cells[3, 15].AutoFitColumns(7);
                        worksheet.Cells[3, 16].AutoFitColumns(10);
                        worksheet.Cells[3, 17].AutoFitColumns(10);
                        worksheet.Cells[3, 18].AutoFitColumns(13);
                        worksheet.Cells[3, 19].AutoFitColumns(13);
                        worksheet.Cells[3, 20].AutoFitColumns(10);
                        worksheet.Cells[3, 21].AutoFitColumns(7);
                        worksheet.Cells[3, 22].AutoFitColumns(7);
                        worksheet.Cells[3, 23].AutoFitColumns(7);
                        worksheet.Cells[3, 24].AutoFitColumns(7);
                        worksheet.Cells[3, 25].AutoFitColumns(7);
                        worksheet.Cells[3, 26].AutoFitColumns(25);
                        worksheet.Cells[3, 27].AutoFitColumns(6.5);
                        worksheet.Cells[3, 28].AutoFitColumns(6.5);
                        worksheet.Cells[3, 29].AutoFitColumns(6.5);
                        worksheet.Cells[3, 30].AutoFitColumns(6.5);
                        worksheet.Cells[3, 31].AutoFitColumns(6.5);
                        worksheet.Cells[3, 32].AutoFitColumns(25);
                        worksheet.Cells[3, 33].AutoFitColumns(7);
                        worksheet.Cells[3, 34].AutoFitColumns(7);
                        worksheet.Cells[3, 35].AutoFitColumns(7);
                        worksheet.Cells[3, 36].AutoFitColumns(7);
                        worksheet.Cells[3, 37].AutoFitColumns(7);
                        worksheet.Cells[3, 38].AutoFitColumns(7.5);
                        worksheet.Cells[3, 39].AutoFitColumns(7.5);
                        worksheet.Cells[3, 40].AutoFitColumns(7.5);
                        worksheet.Cells[3, 41].AutoFitColumns(7.5);
                        worksheet.Cells[3, 42].AutoFitColumns(7);
                        worksheet.Cells[3, 43].AutoFitColumns(7);
                        worksheet.Cells[3, 44].AutoFitColumns(7);
                        worksheet.Cells[3, 45].AutoFitColumns(7);
                        worksheet.Cells[3, 46].AutoFitColumns(7);


                        //Set Cell Faoat value with Alignment
                        worksheet.Cells[inStartIndex, 1, inEndCounter, 46].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        #endregion

                        var asTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo;

                        int pairNo, tempPairNo = 0;
                        bool PairLastColumn = false;
                        for (i = inStartIndex; i < inEndCounter; i++)
                        {
                            #region Assigns Value to Cell


                            Image_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image_URL"]);
                            if (Image_URL != "")
                            {
                                worksheet.Cells[inwrkrow, 1].Formula = "=HYPERLINK(\"" + Image_URL + "\",\" Image \")";
                                worksheet.Cells[inwrkrow, 1].Style.Font.UnderLine = true;
                                worksheet.Cells[inwrkrow, 1].Style.Font.Color.SetColor(Color.Blue);
                            }

                            Video_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Video_URL"]);
                            if (Video_URL != "")
                            {
                                worksheet.Cells[inwrkrow, 2].Formula = "=HYPERLINK(\"" + Video_URL + "\",\" Video \")";
                                worksheet.Cells[inwrkrow, 2].Style.Font.UnderLine = true;
                                worksheet.Cells[inwrkrow, 2].Style.Font.Color.SetColor(Color.Blue);
                            }

                            worksheet.Cells[inwrkrow, 3].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["TransDate"]);
                            worksheet.Cells[inwrkrow, 4].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["UserFullName"]);
                            worksheet.Cells[inwrkrow, 5].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["CompName"]); 
                            worksheet.Cells[inwrkrow, 6].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Ref_No"]);
                            worksheet.Cells[inwrkrow, 7].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Supplier_Stone_Id"]);

                            worksheet.Cells[inwrkrow, 8].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]);
                            string URL = "";
                            string Certificate_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_URL"]);

                            if (!string.IsNullOrEmpty(Certificate_URL))
                            {
                                URL = Certificate_URL;
                            }
                            else if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "GIA")
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
                                worksheet.Cells[inwrkrow, 8].Formula = "=HYPERLINK(\"" + Convert.ToString(URL) + "\",\" " + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) + " \")";
                                worksheet.Cells[inwrkrow, 8].Style.Font.UnderLine = true;
                                worksheet.Cells[inwrkrow, 8].Style.Font.Color.SetColor(Color.Blue);
                            }

                            worksheet.Cells[inwrkrow, 9].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                            worksheet.Cells[inwrkrow, 10].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shape"]);
                            worksheet.Cells[inwrkrow, 11].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pointer"]);
                            worksheet.Cells[inwrkrow, 12].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["BGM"]);
                            worksheet.Cells[inwrkrow, 13].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Color"]);
                            worksheet.Cells[inwrkrow, 14].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Clarity"]);
                            worksheet.Cells[inwrkrow, 15].Value = ((dtDiamonds.Rows[i - inStartIndex]["Cts"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Cts"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Cts"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 16].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 17].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 18].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 19].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 20].Value = ((dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 21].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]);
                            worksheet.Cells[inwrkrow, 22].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Polish"]);
                            worksheet.Cells[inwrkrow, 23].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Symm"]);
                            worksheet.Cells[inwrkrow, 24].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fls"]);
                            worksheet.Cells[inwrkrow, 25].Value = ((dtDiamonds.Rows[i - inStartIndex]["RATIO"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["RATIO"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["RATIO"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 26].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Key_To_Symboll"]);
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

                            worksheet.Cells[inwrkrow, 32].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab_Comments"]);
                            worksheet.Cells[inwrkrow, 33].Value = ((dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 34].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 35].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Height"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Crown_Height"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Height"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 36].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 37].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Height"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Height"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Height"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 38].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Natts"]);
                            worksheet.Cells[inwrkrow, 39].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"]);
                            worksheet.Cells[inwrkrow, 40].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Inclusion"]);
                            worksheet.Cells[inwrkrow, 41].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Inclusion"]);
                            worksheet.Cells[inwrkrow, 42].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Culet"]);
                            worksheet.Cells[inwrkrow, 43].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Open"]);
                            worksheet.Cells[inwrkrow, 44].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle_Open"]);
                            worksheet.Cells[inwrkrow, 45].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Open"]);
                            worksheet.Cells[inwrkrow, 46].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pav_Open"]);


                            inwrkrow++;

                            #endregion
                        }

                        worksheet.Cells[inStartIndex, 1, (inwrkrow - 1), 46].Style.Font.Size = 9;
                        worksheet.Cells[inStartIndex, 18, (inwrkrow - 1), 19].Style.Font.Bold = true;

                        worksheet.Cells[inStartIndex, 15, (inwrkrow - 1), 20].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[inStartIndex, 25, (inwrkrow - 1), 25].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[inStartIndex, 16, (inwrkrow - 1), 19].Style.Numberformat.Format = "#,##0.00";

                        worksheet.Cells[inStartIndex, 27, (inwrkrow - 1), 31].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[inStartIndex, 33, (inwrkrow - 1), 37].Style.Numberformat.Format = "0.00";



                        worksheet.Cells[2, 6].Formula = "ROUND(SUBTOTAL(102," + GetExcelColumnLetter(15) + "" + inStartIndex + ":" + GetExcelColumnLetter(15) + "" + (inwrkrow - 1) + "),2)";
                        worksheet.Cells[2, 6].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, 6].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                        worksheet.Cells[2, 6].Style.Numberformat.Format = "#,##";

                        ExcelStyle cellStyleHeader_Total = worksheet.Cells[2, 6].Style;
                        cellStyleHeader_Total.Border.Left.Style = cellStyleHeader_Total.Border.Right.Style
                                = cellStyleHeader_Total.Border.Top.Style = cellStyleHeader_Total.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;

                        worksheet.Cells[2, 15].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(15) + "" + inStartIndex + ":" + GetExcelColumnLetter(15) + "" + (inwrkrow - 1) + "),2)";
                        worksheet.Cells[2, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, 15].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                        worksheet.Cells[2, 15].Style.Numberformat.Format = "#,##0.00";

                        ExcelStyle cellStyleHeader_Totalcarat = worksheet.Cells[2, 15].Style;
                        cellStyleHeader_Totalcarat.Border.Left.Style = cellStyleHeader_Totalcarat.Border.Right.Style
                                = cellStyleHeader_Totalcarat.Border.Top.Style = cellStyleHeader_Totalcarat.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;


                        worksheet.Cells[2, 18].Formula = "IF(SUBTOTAL(109," + GetExcelColumnLetter(17) + "" + inStartIndex + ": " + GetExcelColumnLetter(17) + "" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109," + GetExcelColumnLetter(19) + "" + inStartIndex + ":" + GetExcelColumnLetter(19) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109," + GetExcelColumnLetter(17) + "" + inStartIndex + ":" + GetExcelColumnLetter(17) + "" + (inwrkrow - 1) + ")))*(-100),2))";
                        worksheet.Cells[2, 18].Style.Numberformat.Format = "#,##0.00";

                        ExcelStyle cellStyleHeader_TotalDis = worksheet.Cells[2, 18].Style;
                        cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                                = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;

                        worksheet.Cells[2, 19].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(19) + "" + inStartIndex + ":" + GetExcelColumnLetter(19) + "" + (inwrkrow - 1) + "),2)";
                        worksheet.Cells[2, 19].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, 19].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                        worksheet.Cells[2, 19].Style.Numberformat.Format = "#,##0";

                        ExcelStyle cellStyleHeader_TotalNet = worksheet.Cells[2, 19].Style;
                        cellStyleHeader_TotalNet.Border.Left.Style = cellStyleHeader_TotalNet.Border.Right.Style
                                = cellStyleHeader_TotalNet.Border.Top.Style = cellStyleHeader_TotalNet.Border.Bottom.Style
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
                else
                {
                    using (ExcelPackage p = new ExcelPackage())
                    {
                        int inStartIndex = 4;
                        int inwrkrow = 4;
                        int inEndCounter = dtDiamonds.Rows.Count + inStartIndex;
                        int TotalRow = dtDiamonds.Rows.Count;
                        int i;
                        string values_1, Image_URL, Video_URL, cut, status, ForCust_Hold;
                        Int64 number_1;
                        bool success1;

                        Color colFromHex_Pointer = System.Drawing.ColorTranslator.FromHtml("#c6e0b4");
                        Color colFromHex_Dis = System.Drawing.ColorTranslator.FromHtml("#ccffff");
                        Color colFromHexTotal = System.Drawing.ColorTranslator.FromHtml("#d9e1f2");
                        Color tcpg_bg_clr = System.Drawing.ColorTranslator.FromHtml("#fff2cc");

                        #region Company Detail on Header

                        p.Workbook.Properties.Author = "SUNRISE DIAMOND";
                        p.Workbook.Properties.Title = "SUNRISE DIAMOND PVT. LTD.";
                        p.Workbook.Worksheets.Add("MyCart");

                        ExcelWorksheet worksheet = p.Workbook.Worksheets[1];
                        worksheet.Name = DateTime.Now.ToString("dd-MM-yyyy");
                        worksheet.Cells.Style.Font.Size = 11;
                        worksheet.Cells.Style.Font.Name = "Calibri";


                        Color colFromHex_H1 = System.Drawing.ColorTranslator.FromHtml("#8497b0");
                        Color col_color_Red = System.Drawing.ColorTranslator.FromHtml("#ff0000");

                        worksheet.Row(2).Height = 40;
                        worksheet.Row(3).Height = 40;
                        worksheet.Row(3).Style.WrapText = true;

                        worksheet.Cells[2, 1].Value = "Total";
                        worksheet.Cells[2, 1, 2, 44].Style.Font.Bold = true;
                        worksheet.Cells[2, 1, 2, 44].Style.Font.Size = 11;
                        worksheet.Cells[2, 1, 2, 44].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[2, 1, 2, 44].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        worksheet.Cells[2, 1, 2, 44].Style.Font.Size = 11;

                        worksheet.Cells[3, 1, 3, 44].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        worksheet.Cells[3, 1, 3, 44].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        worksheet.Cells[3, 1, 3, 44].Style.Font.Size = 10;
                        worksheet.Cells[3, 1, 3, 44].Style.Font.Bold = true;

                        worksheet.Cells[3, 1, 3, 44].AutoFilter = true;

                        var cellBackgroundColor1 = worksheet.Cells[3, 1, 3, 44].Style.Fill;
                        cellBackgroundColor1.PatternType = ExcelFillStyle.Solid;
                        Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                        cellBackgroundColor1.BackgroundColor.SetColor(colFromHex);

                        #endregion

                        #region Header Name Declaration

                        worksheet.Cells[3, 1].Value = "Image";
                        worksheet.Cells[3, 2].Value = "Video";
                        worksheet.Cells[3, 3].Value = "Date";
                        worksheet.Cells[3, 4].Value = "Ref No";
                        worksheet.Cells[3, 5].Value = "Supplier Ref No";
                        worksheet.Cells[3, 6].Value = "Lab";
                        worksheet.Cells[3, 7].Value = "Cert No";
                        worksheet.Cells[3, 8].Value = "Shape";
                        worksheet.Cells[3, 9].Value = "Pointer";
                        worksheet.Cells[3, 10].Value = "BGM";
                        worksheet.Cells[3, 11].Value = "Color";
                        worksheet.Cells[3, 12].Value = "Clarity";
                        worksheet.Cells[3, 13].Value = "Cts";
                        worksheet.Cells[3, 14].Value = "Rap Rate($)";
                        worksheet.Cells[3, 15].Value = "Rap Amount($)";
                        worksheet.Cells[3, 16].Value = "Offer Disc(%)";
                        worksheet.Cells[3, 17].Value = "Offer Value($)";
                        worksheet.Cells[3, 18].Value = "Price Cts";
                        worksheet.Cells[3, 19].Value = "Cut";
                        worksheet.Cells[3, 20].Value = "Polish";
                        worksheet.Cells[3, 21].Value = "Symm";
                        worksheet.Cells[3, 22].Value = "Fls";
                        worksheet.Cells[3, 23].Value = "RATIO";
                        worksheet.Cells[3, 24].Value = "Key To Symbol";
                        worksheet.Cells[3, 25].Value = "Length";
                        worksheet.Cells[3, 26].Value = "Width";
                        worksheet.Cells[3, 27].Value = "Depth";
                        worksheet.Cells[3, 28].Value = "Depth (%)";
                        worksheet.Cells[3, 29].Value = "Table (%)";
                        worksheet.Cells[3, 30].Value = "Comment";
                        worksheet.Cells[3, 31].Value = "Girdle(%)";
                        worksheet.Cells[3, 32].Value = "Crown Angle";
                        worksheet.Cells[3, 33].Value = "Crown Height";
                        worksheet.Cells[3, 34].Value = "Pav Angle";
                        worksheet.Cells[3, 35].Value = "Pav Height";
                        worksheet.Cells[3, 36].Value = "Table Natts";
                        worksheet.Cells[3, 37].Value = "Crown Natts";
                        worksheet.Cells[3, 38].Value = "Table Inclusion";
                        worksheet.Cells[3, 39].Value = "Crown Inclusion";
                        worksheet.Cells[3, 40].Value = "Culet";
                        worksheet.Cells[3, 41].Value = "Table Open";
                        worksheet.Cells[3, 42].Value = "Girdle Open";
                        worksheet.Cells[3, 43].Value = "Crown Open";
                        worksheet.Cells[3, 44].Value = "Pavilion Open";


                        ExcelStyle cellStyleHeader1 = worksheet.Cells[3, 1, 3, 44].Style;
                        cellStyleHeader1.Border.Left.Style = cellStyleHeader1.Border.Right.Style
                                = cellStyleHeader1.Border.Top.Style = cellStyleHeader1.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;

                        #endregion

                        #region Set AutoFit and Decimal Number Format

                        worksheet.View.FreezePanes(4, 1);

                        worksheet.Cells[3, 1].AutoFitColumns(7);
                        worksheet.Cells[3, 2].AutoFitColumns(7);
                        worksheet.Cells[3, 3].AutoFitColumns(10);
                        worksheet.Cells[3, 4].AutoFitColumns(13);
                        worksheet.Cells[3, 5].AutoFitColumns(13);
                        worksheet.Cells[3, 6].AutoFitColumns(7);
                        worksheet.Cells[3, 7].AutoFitColumns(12);
                        worksheet.Cells[3, 8].AutoFitColumns(12);
                        worksheet.Cells[3, 9].AutoFitColumns(9);
                        worksheet.Cells[3, 10].AutoFitColumns(8.5);
                        worksheet.Cells[3, 11].AutoFitColumns(7);
                        worksheet.Cells[3, 12].AutoFitColumns(7);
                        worksheet.Cells[3, 13].AutoFitColumns(7);
                        worksheet.Cells[3, 14].AutoFitColumns(10);
                        worksheet.Cells[3, 15].AutoFitColumns(10);
                        worksheet.Cells[3, 16].AutoFitColumns(13);
                        worksheet.Cells[3, 17].AutoFitColumns(13);
                        worksheet.Cells[3, 18].AutoFitColumns(10);
                        worksheet.Cells[3, 19].AutoFitColumns(7);
                        worksheet.Cells[3, 20].AutoFitColumns(7);
                        worksheet.Cells[3, 21].AutoFitColumns(7);
                        worksheet.Cells[3, 22].AutoFitColumns(7);
                        worksheet.Cells[3, 23].AutoFitColumns(7);
                        worksheet.Cells[3, 24].AutoFitColumns(25);
                        worksheet.Cells[3, 25].AutoFitColumns(6.5);
                        worksheet.Cells[3, 26].AutoFitColumns(6.5);
                        worksheet.Cells[3, 27].AutoFitColumns(6.5);
                        worksheet.Cells[3, 28].AutoFitColumns(6.5);
                        worksheet.Cells[3, 29].AutoFitColumns(6.5);
                        worksheet.Cells[3, 30].AutoFitColumns(25);
                        worksheet.Cells[3, 31].AutoFitColumns(7);
                        worksheet.Cells[3, 32].AutoFitColumns(7);
                        worksheet.Cells[3, 33].AutoFitColumns(7);
                        worksheet.Cells[3, 34].AutoFitColumns(7);
                        worksheet.Cells[3, 35].AutoFitColumns(7);
                        worksheet.Cells[3, 36].AutoFitColumns(7.5);
                        worksheet.Cells[3, 37].AutoFitColumns(7.5);
                        worksheet.Cells[3, 38].AutoFitColumns(7.5);
                        worksheet.Cells[3, 39].AutoFitColumns(7.5);
                        worksheet.Cells[3, 40].AutoFitColumns(7);
                        worksheet.Cells[3, 41].AutoFitColumns(7);
                        worksheet.Cells[3, 42].AutoFitColumns(7);
                        worksheet.Cells[3, 43].AutoFitColumns(7);
                        worksheet.Cells[3, 44].AutoFitColumns(7);


                        //Set Cell Faoat value with Alignment
                        worksheet.Cells[inStartIndex, 1, inEndCounter, 44].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        #endregion

                        var asTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo;

                        int pairNo, tempPairNo = 0;
                        bool PairLastColumn = false;
                        for (i = inStartIndex; i < inEndCounter; i++)
                        {
                            #region Assigns Value to Cell


                            Image_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Image_URL"]);
                            if (Image_URL != "")
                            {
                                worksheet.Cells[inwrkrow, 1].Formula = "=HYPERLINK(\"" + Image_URL + "\",\" Image \")";
                                worksheet.Cells[inwrkrow, 1].Style.Font.UnderLine = true;
                                worksheet.Cells[inwrkrow, 1].Style.Font.Color.SetColor(Color.Blue);
                            }

                            Video_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Video_URL"]);
                            if (Video_URL != "")
                            {
                                worksheet.Cells[inwrkrow, 2].Formula = "=HYPERLINK(\"" + Video_URL + "\",\" Video \")";
                                worksheet.Cells[inwrkrow, 2].Style.Font.UnderLine = true;
                                worksheet.Cells[inwrkrow, 2].Style.Font.Color.SetColor(Color.Blue);
                            }

                            worksheet.Cells[inwrkrow, 3].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["TransDate"]);
                            worksheet.Cells[inwrkrow, 4].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Ref_No"]);
                            worksheet.Cells[inwrkrow, 5].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Supplier_Stone_Id"]);

                            worksheet.Cells[inwrkrow, 6].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]);
                            string URL = "";
                            string Certificate_URL = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_URL"]);

                            if (!string.IsNullOrEmpty(Certificate_URL))
                            {
                                URL = Certificate_URL;
                            }
                            else if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) == "GIA")
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
                                worksheet.Cells[inwrkrow, 6].Formula = "=HYPERLINK(\"" + Convert.ToString(URL) + "\",\" " + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab"]) + " \")";
                                worksheet.Cells[inwrkrow, 6].Style.Font.UnderLine = true;
                                worksheet.Cells[inwrkrow, 6].Style.Font.Color.SetColor(Color.Blue);
                            }


                            worksheet.Cells[inwrkrow, 7].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Certificate_No"]);
                            worksheet.Cells[inwrkrow, 8].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Shape"]);
                            worksheet.Cells[inwrkrow, 9].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pointer"]);
                            worksheet.Cells[inwrkrow, 10].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["BGM"]);
                            worksheet.Cells[inwrkrow, 11].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Color"]);
                            worksheet.Cells[inwrkrow, 12].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Clarity"]);
                            worksheet.Cells[inwrkrow, 13].Value = ((dtDiamonds.Rows[i - inStartIndex]["Cts"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Cts"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Cts"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 14].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Rate"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 15].Value = ((dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Rap_Amount"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 16].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_DISC"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 17].Value = ((dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["CUSTOMER_COST_VALUE"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 18].Value = ((dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Base_Price_Cts"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 19].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Cut"]);
                            worksheet.Cells[inwrkrow, 20].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Polish"]);
                            worksheet.Cells[inwrkrow, 21].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Symm"]);
                            worksheet.Cells[inwrkrow, 22].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Fls"]);
                            worksheet.Cells[inwrkrow, 23].Value = ((dtDiamonds.Rows[i - inStartIndex]["RATIO"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["RATIO"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["RATIO"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 24].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Key_To_Symboll"]);
                            worksheet.Cells[inwrkrow, 25].Value = ((dtDiamonds.Rows[i - inStartIndex]["Length"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Length"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Length"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 26].Value = ((dtDiamonds.Rows[i - inStartIndex]["Width"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Width"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Width"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 27].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Depth"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 28].Value = ((dtDiamonds.Rows[i - inStartIndex]["Depth_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Depth_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Depth_Per"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 29].Value = ((dtDiamonds.Rows[i - inStartIndex]["Table_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Table_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Table_Per"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 30].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Lab_Comments"]);
                            worksheet.Cells[inwrkrow, 31].Value = ((dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Girdle_Per"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 32].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Angle"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 33].Value = ((dtDiamonds.Rows[i - inStartIndex]["Crown_Height"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Crown_Height"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Crown_Height"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 34].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Angle"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 35].Value = ((dtDiamonds.Rows[i - inStartIndex]["Pav_Height"] != null) ?
                                       (dtDiamonds.Rows[i - inStartIndex]["Pav_Height"].GetType().Name != "DBNull" ?
                                       Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["Pav_Height"]) : ((Double?)null)) : null);

                            worksheet.Cells[inwrkrow, 36].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Natts"]);
                            worksheet.Cells[inwrkrow, 37].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"]);
                            worksheet.Cells[inwrkrow, 38].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Inclusion"]);
                            worksheet.Cells[inwrkrow, 39].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Inclusion"]);
                            worksheet.Cells[inwrkrow, 40].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Culet"]);
                            worksheet.Cells[inwrkrow, 41].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Table_Open"]);
                            worksheet.Cells[inwrkrow, 42].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Girdle_Open"]);
                            worksheet.Cells[inwrkrow, 43].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Open"]);
                            worksheet.Cells[inwrkrow, 44].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Pav_Open"]);


                            inwrkrow++;

                            #endregion
                        }

                        worksheet.Cells[inStartIndex, 1, (inwrkrow - 1), 44].Style.Font.Size = 9;
                        worksheet.Cells[inStartIndex, 16, (inwrkrow - 1), 17].Style.Font.Bold = true;

                        worksheet.Cells[inStartIndex, 13, (inwrkrow - 1), 18].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[inStartIndex, 23, (inwrkrow - 1), 23].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[inStartIndex, 14, (inwrkrow - 1), 17].Style.Numberformat.Format = "#,##0.00";

                        worksheet.Cells[inStartIndex, 25, (inwrkrow - 1), 29].Style.Numberformat.Format = "0.00";
                        worksheet.Cells[inStartIndex, 31, (inwrkrow - 1), 35].Style.Numberformat.Format = "0.00";



                        worksheet.Cells[2, 4].Formula = "ROUND(SUBTOTAL(102," + GetExcelColumnLetter(13) + "" + inStartIndex + ":" + GetExcelColumnLetter(13) + "" + (inwrkrow - 1) + "),2)";
                        worksheet.Cells[2, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, 4].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                        worksheet.Cells[2, 4].Style.Numberformat.Format = "#,##";

                        ExcelStyle cellStyleHeader_Total = worksheet.Cells[2, 4].Style;
                        cellStyleHeader_Total.Border.Left.Style = cellStyleHeader_Total.Border.Right.Style
                                = cellStyleHeader_Total.Border.Top.Style = cellStyleHeader_Total.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;

                        worksheet.Cells[2, 13].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(13) + "" + inStartIndex + ":" + GetExcelColumnLetter(13) + "" + (inwrkrow - 1) + "),2)";
                        worksheet.Cells[2, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, 13].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                        worksheet.Cells[2, 13].Style.Numberformat.Format = "#,##0.00";

                        ExcelStyle cellStyleHeader_Totalcarat = worksheet.Cells[2, 13].Style;
                        cellStyleHeader_Totalcarat.Border.Left.Style = cellStyleHeader_Totalcarat.Border.Right.Style
                                = cellStyleHeader_Totalcarat.Border.Top.Style = cellStyleHeader_Totalcarat.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;


                        worksheet.Cells[2, 16].Formula = "IF(SUBTOTAL(109," + GetExcelColumnLetter(15) + "" + inStartIndex + ": " + GetExcelColumnLetter(15) + "" + (inwrkrow - 1) + ")=0,0,ROUND((1-(SUBTOTAL(109," + GetExcelColumnLetter(17) + "" + inStartIndex + ":" + GetExcelColumnLetter(17) + "" + (inwrkrow - 1) + ")/SUBTOTAL(109," + GetExcelColumnLetter(15) + "" + inStartIndex + ":" + GetExcelColumnLetter(15) + "" + (inwrkrow - 1) + ")))*(-100),2))";
                        worksheet.Cells[2, 16].Style.Numberformat.Format = "#,##0.00";

                        ExcelStyle cellStyleHeader_TotalDis = worksheet.Cells[2, 16].Style;
                        cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                                = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                                = ExcelBorderStyle.Medium;

                        worksheet.Cells[2, 17].Formula = "ROUND(SUBTOTAL(109," + GetExcelColumnLetter(17) + "" + inStartIndex + ":" + GetExcelColumnLetter(17) + "" + (inwrkrow - 1) + "),2)";
                        worksheet.Cells[2, 17].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        worksheet.Cells[2, 17].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                        worksheet.Cells[2, 17].Style.Numberformat.Format = "#,##0";

                        ExcelStyle cellStyleHeader_TotalNet = worksheet.Cells[2, 17].Style;
                        cellStyleHeader_TotalNet.Border.Left.Style = cellStyleHeader_TotalNet.Border.Right.Style
                                = cellStyleHeader_TotalNet.Border.Top.Style = cellStyleHeader_TotalNet.Border.Bottom.Style
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
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, null);
                throw ex;
            }
        }
        public static string GetHexValue(int val)
        {
            //return String.Format("{0:X}", val);
            return val.ToString();
        }







        // Added By Kaushal 13-12-2018 Given Bhai Tejas Bhai
        public static void Excel_Data_video(DataTable p_dt, string _strFilePath, string _strcerti)
        {
            FileInfo newFile = new FileInfo(_strFilePath);
            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(_strFilePath);
            }
            string _date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string _strlabName = "";
            string _strcertino = "";
            string _strloction = "";
            string _strhyperlink = "";
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // For Set Excel Sheet WorkSheet Name
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("TotalStock");

                // For Set Company Name, Address & Others

                //worksheet.Cells["B2"].Value = "Abbreviation";
                //worksheet.Cells["B2"].Style.Font.Bold = true;
                //worksheet.Cells["C2"].Value = "Buss. Proc";
                //worksheet.Cells["D2"].Value = "B";
                //worksheet.Cells["D2"].Style.Font.Bold = true;
                //worksheet.Cells["C3"].Value = "Promotion";
                //worksheet.Cells["D3"].Value = "P";
                //worksheet.Cells["D3"].Style.Font.Bold = true;
                //worksheet.Cells["B4:F4"].Value = "Table & Crown Inclusion = White Inclusion";
                //worksheet.Cells["B4:F4"].Merge = true;
                //worksheet.Cells["B5:E5"].Value = "Table & Crown Natts = Black Inclusion";
                //worksheet.Cells["B5:E5"].Merge = true;

                worksheet.Cells["F1"].Value = "SUNRISE DIAMONDS INVENTORY FOR THE DATE " + _date + "";
                //worksheet.Cells["F1:S1"].Merge = true;
                worksheet.Cells["F1"].Style.Font.Size = 24;
                worksheet.Cells["F1:S1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["F1:S1"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["F1:S1"].Style.Font.Bold = true;

                worksheet.Cells["F2"].Value = "UNIT 1, 14/F, PENINSULA SQUARE, EAST WING, 18 SUNG ON STREET, HUNG HOM, KOWLOON, HONG KONG TEL : +852 - 27235100    FAX : +852 - 2314 9100";
                //worksheet.Cells["F2:T2"].Merge = true;
                worksheet.Cells["F2:T2"].Style.Font.Size = 12;
                worksheet.Cells["F2:T2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["F2:T2"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["F2:T2"].Style.Font.Bold = true;

                worksheet.Cells["F3"].Value = "Email Id : sales@sunrisediam.com    Web : www.sunrisediamonds.com.hk . Download Apps on Android, IOS and Windows";
                //worksheet.Cells["F3:S3"].Merge = true;
                worksheet.Cells["F3:S3"].Style.Font.Size = 12;
                worksheet.Cells["F3:S3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["F3:S3"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["F3:S3"].Style.Font.Bold = true;

                //worksheet.Cells["B2:D2"].Value = "All Prices are final Selling Cash Price";
                //worksheet.Cells["B2:D2"].Merge = true;
                //worksheet.Cells["B2:D2"].Style.Font.Bold = true;
                //worksheet.Cells["B2:D2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                //worksheet.Cells["B2:D2"].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                //worksheet.Cells["B2:D2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                //worksheet.Cells["B2:D2"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

                int rowCount = 6; // Start Printing Records

                foreach (DataRow dr in p_dt.Rows)
                {
                    rowCount += 1;
                    for (int i = 1; i < p_dt.Columns.Count + 1; i++)
                    {
                        if (rowCount == 7) // For Colounm Header Name
                        {
                            worksheet.Cells[rowCount, i].Value = p_dt.Columns[i - 1].ColumnName;
                            worksheet.Cells[rowCount, i].Style.Font.Bold = true;
                            worksheet.Cells[rowCount, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[rowCount, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                            worksheet.Cells[rowCount, i].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[rowCount, i].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[rowCount, i].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[rowCount, i].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        }

                        // For Set 2 Decimal 
                        if (p_dt.Columns[i - 1].ColumnName == "Cts" || p_dt.Columns[i - 1].ColumnName == "Rap Price($)"
                           || p_dt.Columns[i - 1].ColumnName == "Length" || p_dt.Columns[i - 1].ColumnName == "Disc (%)"
                           || p_dt.Columns[i - 1].ColumnName == "Width" || p_dt.Columns[i - 1].ColumnName == "Depth"
                           || p_dt.Columns[i - 1].ColumnName == "Depth (%)" || p_dt.Columns[i - 1].ColumnName == "Table (%)"
                           || p_dt.Columns[i - 1].ColumnName == "Cr Ang" || p_dt.Columns[i - 1].ColumnName == "Cr Ht"
                           || p_dt.Columns[i - 1].ColumnName == "Pav Ang" || p_dt.Columns[i - 1].ColumnName == "Pav Ht")
                        {
                            try // For Decimal value Come Here
                            {
                                worksheet.Cells[rowCount + 1, i].Value = double.Parse(dr[i - 1].ToString());
                            }
                            catch // For Non Decimal or Blank Value come here
                            {
                                worksheet.Cells[rowCount + 1, i].Value = dr[i - 1].ToString();
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "Image") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""Image"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "HDMovie") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""HDMovie"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "SImage") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""SImage"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }

                        else if (p_dt.Columns[i - 1].ColumnName == "Lab") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                _strcertino = (dr[7].ToString());
                                _strlabName = (dr[39].ToString()); // Lab1 column data for lab name
                                _strloction = (dr[1].ToString());

                                if (_strloction == "Hong Kong")
                                {
                                    _strhyperlink = "https://sunrisediamonds.com.hk/certi/" + _strcertino + ".pdf";
                                    worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + _strhyperlink + @""",""" + _strlabName + @""")";
                                }
                                else
                                {
                                    worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""" + _strlabName + @""")";
                                }

                                //https://sunrisediamonds.com.hk/certi/5191592902.pdf
                                //worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""" + _strlabName + @""")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "DNA") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""DNA"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "VdoLink") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""SVideo"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "Rap Amt($)") // For Set Formula Here
                        {
                            int _intcnt = rowCount + 1;
                            worksheet.Cells[rowCount + 1, i].Formula = "=L" + _intcnt + "*M" + _intcnt;
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "Net Amt($)") // For Set Formula Here
                        {
                            int _intcnt = rowCount + 1;
                            worksheet.Cells[rowCount + 1, i].Formula = "=N" + _intcnt + "+(" + "N" + _intcnt + "*O" + _intcnt + "/100" + ")";
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "Cut") // For Set Formula Here
                        {
                            worksheet.Cells[rowCount + 1, i].Value = dr[i - 1].ToString();
                            if (dr[i - 1].ToString() == "3EX")
                            {
                                worksheet.Cells[rowCount + 1, i].Style.Font.Bold = true;
                            }
                        }
                        else // For Others Value Come Here
                        {
                            worksheet.Cells[rowCount + 1, i].Value = dr[i - 1].ToString();
                        }
                    }
                }
                rowCount = rowCount + 1;
                worksheet.Cells["A7:B250000"].AutoFitColumns();
                worksheet.View.FreezePanes(8, 1); // For Panel Freeze
                int rowEnd = worksheet.Dimension.End.Row;
                removingGreenTagWarning(worksheet, worksheet.Cells[1, 1, rowEnd, 100].Address);
                package.Save();
            }
        }

        public static void Excel_Generate(DataTable p_dt, string _strFilePath)
        {
            FileInfo newFile = new FileInfo(_strFilePath);
            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(_strFilePath);
            }

            using (ExcelPackage pck = new ExcelPackage(newFile))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Data");
                ws.Cells["A1"].LoadFromDataTable(p_dt, true);
                int rowEnd = ws.Dimension.End.Row;
                removingGreenTagWarning(ws, ws.Cells[1, 1, rowEnd, 100].Address);
                pck.Save();
            }
        }

       
        public static void Excel_Data(DataTable p_dt, string _strFilePath, string _strcerti)
        {
            FileInfo newFile = new FileInfo(_strFilePath);
            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(_strFilePath);
            }
            string _date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string _strlabName = "";
            string _strcertino = "";
            string _strloction = "";
            string _strhyperlink = "";
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // For Set Excel Sheet WorkSheet Name
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("TotalStock");

                // For Set Company Name, Address & Others
                worksheet.Cells["B3"].Value = "Abbreviation";
                worksheet.Cells["B3"].Style.Font.Bold = true;
                worksheet.Cells["C3"].Value = "Buss. Proc";
                worksheet.Cells["D3"].Value = "B";
                worksheet.Cells["D3"].Style.Font.Bold = true;
                worksheet.Cells["C4"].Value = "Promotion";
                worksheet.Cells["D4"].Value = "P";
                worksheet.Cells["D4"].Style.Font.Bold = true;
                //worksheet.Cells["B5"].Value = "Table & Crown Inclusion = White Inclusion";
                //worksheet.Cells["F5"].Value = "Table & Crown Natts = Black Inclusion";

                worksheet.Cells["F1"].Value = "SUNRISE DIAMONDS INVENTORY FOR THE DATE " + _date + "";
                worksheet.Cells["F1"].Style.Font.Size = 24;
                worksheet.Cells["F1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["F1"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["F1"].Style.Font.Bold = true;

                worksheet.Cells["F2"].Value = "UNIT 1, 14/F, PENINSULA SQUARE, EAST WING, 18 SUNG ON STREET, HUNG HOM, KOWLOON, HONG KONG TEL : +852 - 27235100    FAX : +852 - 2314 9100";
                worksheet.Cells["F2"].Style.Font.Size = 12;
                worksheet.Cells["F2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["F2"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["F2"].Style.Font.Bold = true;

                worksheet.Cells["F3"].Value = "Email Id : sales@sunrisediam.com    Web : www.sunrisediamonds.com.hk . Download Apps on Android, IOS and Windows";
                worksheet.Cells["F3"].Style.Font.Size = 12;
                worksheet.Cells["F3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["F3"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["F3"].Style.Font.Bold = true;

                worksheet.Cells["B2"].Value = "All Prices are final Selling Cash Price";
                worksheet.Cells["B2"].Style.Font.Bold = true;
                worksheet.Cells["B2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["B2"].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                worksheet.Cells["B2:D2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B2:D2"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

                worksheet.Cells["A6:" + "AM6"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A6:" + "AM6"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                worksheet.Cells["A6"].Value = "Total";
                worksheet.Cells["A6"].Style.Font.Bold = true;

                // For Border Lines
                worksheet.Cells["A6:" + "AM6"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A6:" + "AM6"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A6:" + "AM6"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                worksheet.Cells["A7:" + "AM7"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A7:" + "AM7"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A7:" + "AM7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                worksheet.Cells["A7:" + "AM7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A7:" + "AM7"].AutoFilter = true;


                int rowCount = 6; // Start Printing Records

                foreach (DataRow dr in p_dt.Rows)
                {
                    rowCount += 1;
                    for (int i = 1; i < p_dt.Columns.Count + 1; i++)
                    {
                        if (rowCount == 7) // For Colounm Header Name
                        {
                            if (p_dt.Columns[i - 1].ColumnName.ToUpper() == "LUSTER/ MILKY")
                            {
                                worksheet.Cells[rowCount, i].Value = "Luster";
                            }
                            else
                            {
                                worksheet.Cells[rowCount, i].Value = p_dt.Columns[i - 1].ColumnName;
                            }
                            worksheet.Cells[rowCount, i].Style.Font.Bold = true;
                            worksheet.Cells[rowCount, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[rowCount, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                            worksheet.Cells[rowCount, i].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[rowCount, i].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[rowCount, i].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[rowCount, i].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                        }

                        // For Set 2 Decimal 
                        if (p_dt.Columns[i - 1].ColumnName == "Cts" || p_dt.Columns[i - 1].ColumnName == "Rap Price($)"
                           || p_dt.Columns[i - 1].ColumnName == "Length" || p_dt.Columns[i - 1].ColumnName == "Disc (%)"
                           || p_dt.Columns[i - 1].ColumnName == "Width" || p_dt.Columns[i - 1].ColumnName == "Depth"
                           || p_dt.Columns[i - 1].ColumnName == "Depth (%)" || p_dt.Columns[i - 1].ColumnName == "Table (%)"
                           || p_dt.Columns[i - 1].ColumnName == "Cr Ang" || p_dt.Columns[i - 1].ColumnName == "Cr Ht"
                           || p_dt.Columns[i - 1].ColumnName == "Pav Ang" || p_dt.Columns[i - 1].ColumnName == "Pav Ht"
                           || p_dt.Columns[i - 1].ColumnName == "Certi No")
                        {
                            try // For Decimal value Come Here
                            {
                                worksheet.Cells[rowCount + 1, i].Value = double.Parse(dr[i - 1].ToString());
                            }
                            catch // For Non Decimal or Blank Value come here
                            {
                                worksheet.Cells[rowCount + 1, i].Value = dr[i - 1].ToString();
                            }
                        }

                        else if (p_dt.Columns[i - 1].ColumnName.ToUpper() == "REF. NO")
                        {
                            try // For Decimal value Come Here
                            {
                                worksheet.Cells[rowCount + 1, i].Value = double.Parse(dr[i - 1].ToString());
                            }
                            catch // For Non Decimal or Blank Value come here
                            {
                                worksheet.Cells[rowCount + 1, i].Value = dr[i - 1].ToString();
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "Image") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""Image"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "HDMovie") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""HDMovie"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "SImage") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""SImage"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }

                        else if (p_dt.Columns[i - 1].ColumnName == "Lab") // For Create Hyperlink
                        {
                            if (p_dt.Columns[2].ColumnName == "offer_status")
                            {
                                if (dr[i - 1].ToString() != "")
                                {
                                    _strlabName = (dr[6].ToString());
                                    worksheet.Cells[rowCount + 1, i].Value = _strlabName;
                                }
                            }
                            else
                            {
                                if (dr[i - 1].ToString() != "")
                                {
                                    _strcertino = (dr[7].ToString());
                                    _strlabName = (dr[39].ToString()); // Lab1 column data for lab name
                                    _strloction = (dr[1].ToString());

                                    if (_strloction == "Hong Kong")
                                    {
                                        _strhyperlink = "https://sunrisediamonds.com.hk/certi/" + _strcertino + ".pdf";
                                        worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + _strhyperlink + @""",""" + _strlabName + @""")";
                                    }
                                    else
                                    {
                                        worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""" + _strlabName + @""")";
                                    }

                                    //https://sunrisediamonds.com.hk/certi/5191592902.pdf
                                    //worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""" + _strlabName + @""")";
                                    worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                    worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                                }
                            }

                        }

                        else if (p_dt.Columns[i - 1].ColumnName == "DNA") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""DNA"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "VdoLink") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""SVideo"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "Rap Amt($)") // For Set Formula Here
                        {
                            int _intcnt = rowCount + 1;
                            worksheet.Cells[rowCount + 1, i].Formula = "=L" + _intcnt + "*M" + _intcnt;
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "Net Amt($)") // For Set Formula Here
                        {
                            int _intcnt = rowCount + 1;
                            worksheet.Cells[rowCount + 1, i].Formula = "=N" + _intcnt + "+(" + "N" + _intcnt + "*O" + _intcnt + "/100" + ")";
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "Cut") // For Set Formula Here
                        {
                            worksheet.Cells[rowCount + 1, i].Value = dr[i - 1].ToString();
                            if (dr[i - 1].ToString() == "3EX")
                            {
                                worksheet.Cells[rowCount + 1, i].Style.Font.Bold = true;
                            }
                        }
                        else // For Others Value Come Here
                        {
                            worksheet.Cells[rowCount + 1, i].Value = dr[i - 1].ToString();
                        }
                    }
                }
                rowCount = rowCount + 1;

                // For Set Formula Here (Total)
                worksheet.Cells["D6"].Formula = "=SUBTOTAL(103," + "E8:" + "E" + rowCount + ")"; //Ref. No
                worksheet.Cells["L6"].Formula = "=SUBTOTAL(109," + "L8:" + "L" + rowCount + ")"; //Cts
                worksheet.Cells["N6"].Formula = "=SUBTOTAL(109," + "N8:" + "N" + rowCount + ")"; //Rap Amt($)
                worksheet.Cells["P6"].Formula = "=SUBTOTAL(109," + "P8:" + "P" + rowCount + ")"; //Net Amt($)
                worksheet.Cells["O6"].Formula = "=(1-(SUBTOTAL(109," + "P8:" + "P" + rowCount + ")/SUBTOTAL(109," + "N8:" + "N" + rowCount + ")))*-100"; //Disc (%)

                // For Set Front Color
                worksheet.Cells["O8:" + "O" + rowCount].Style.Font.Color.SetColor(System.Drawing.Color.Red);// Disc (%)
                worksheet.Cells["P8:" + "P" + rowCount].Style.Font.Color.SetColor(System.Drawing.Color.Red);// Net Amt($)

                //// For Set Front Bold
                worksheet.Cells["O8:" + "O" + rowCount].Style.Font.Bold = true;// Disc (%)
                worksheet.Cells["P8:" + "P" + rowCount].Style.Font.Bold = true;// Net Amt($)

                //// For Set Number Format Here
                worksheet.Cells["P8:" + "P" + rowCount].Style.Numberformat.Format = "0.00";// Net Amt($)
                worksheet.Cells["L8:" + "L" + rowCount].Style.Numberformat.Format = "0.00";// Cts
                worksheet.Cells["N8:" + "N" + rowCount].Style.Numberformat.Format = "#,##0";// Rap Amt($)
                worksheet.Cells["O8:" + "O" + rowCount].Style.Numberformat.Format = "0.00";// Disc (%)
                worksheet.Cells["Y8:" + "Y" + rowCount].Style.Numberformat.Format = "0.00";// Table (%)
                worksheet.Cells["U8:" + "U" + rowCount].Style.Numberformat.Format = "0.00";// Length
                worksheet.Cells["V8:" + "V" + rowCount].Style.Numberformat.Format = "0.00";// Width
                worksheet.Cells["W8:" + "W" + rowCount].Style.Numberformat.Format = "0.00";// Depth
                worksheet.Cells["X8:" + "X" + rowCount].Style.Numberformat.Format = "0.00";// Depth (%)
                worksheet.Cells["Y8:" + "Y" + rowCount].Style.Numberformat.Format = "0.00";// Table (%)
                worksheet.Cells["AF8:" + "AF" + rowCount].Style.Numberformat.Format = "0.00";// Cr Ang
                worksheet.Cells["AG8:" + "AG" + rowCount].Style.Numberformat.Format = "0.00";// Cr Ht
                worksheet.Cells["AH8:" + "AH" + rowCount].Style.Numberformat.Format = "0.00";// Pav Ang
                worksheet.Cells["AI8:" + "AI" + rowCount].Style.Numberformat.Format = "0.00";// Pav Ang

                // For Total Formate Here
                worksheet.Cells["D6"].Style.Numberformat.Format = "#,##0";// Ref. No
                worksheet.Cells["L6"].Style.Numberformat.Format = "#,##0.00";// Cts
                worksheet.Cells["N6"].Style.Numberformat.Format = "#,##0";// Rap Amt($)
                worksheet.Cells["P6"].Style.Numberformat.Format = "#,##0";// Net Amt($)
                worksheet.Cells["O6"].Style.Numberformat.Format = "0.00";// Disc (%)

                worksheet.Cells["A6:AL25000"].AutoFitColumns(); // For Autofit Colounm
                worksheet.Cells["A6:" + "AL" + rowCount].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["Z7:" + "Z" + rowCount].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;//(Key To Symbol)
                worksheet.Row(7).Height = 26.25;
                worksheet.Column(13).Width = 10;
                worksheet.Column(17).Width = 6.75;
                worksheet.Column(18).Width = 6.75;
                worksheet.Column(19).Width = 6.75;
                worksheet.Column(20).Width = 6.75;
                worksheet.Column(21).Width = 6.75;
                worksheet.Column(22).Width = 6.75;
                worksheet.Column(23).Width = 6.75;
                worksheet.Column(26).Width = 60;
                worksheet.Column(27).Width = 6.75;
                worksheet.Cells["A7:" + "AL" + 7].Style.WrapText = true;// For Header Only
                // For Special columns width(Key To Symbol)
                worksheet.Column(40).Hidden = true; // For Hide Lab column
                worksheet.View.FreezePanes(8, 1); // For Panel Freeze
                int rowEnd = worksheet.Dimension.End.Row;
                removingGreenTagWarning(worksheet, worksheet.Cells[1, 1, rowEnd, 100].Address);
                package.Save();
            }
        }

        // Added By Kaushal 18-12-2018 as per tj date 18-12-2018
        public static void Excel_Data_Offer_Dwnld(DataTable p_dt, string _strFilePath, string _strcerti)
        {
            FileInfo newFile = new FileInfo(_strFilePath);
            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(_strFilePath);
            }
            string _date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // For Set Excel Sheet WorkSheet Name
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("TotalStock");

                // For Set Company Name, Address & Others
                worksheet.Cells["B2"].Value = "Abbreviation";
                worksheet.Cells["B2"].Style.Font.Bold = true;
                worksheet.Cells["C2"].Value = "Buss. Proc";
                worksheet.Cells["D2"].Value = "B";
                worksheet.Cells["D2"].Style.Font.Bold = true;

                worksheet.Cells["F1"].Value = "SUNRISE DIAMONDS INVENTORY FOR THE DATE " + _date + "";
                //worksheet.Cells["F1:S1"].Merge = true;
                worksheet.Cells["F1"].Style.Font.Size = 24;
                worksheet.Cells["F1:S1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["F1:S1"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["F1:S1"].Style.Font.Bold = true;

                worksheet.Cells["F2"].Value = "UNIT 1, 14/F, PENINSULA SQUARE, EAST WING, 18 SUNG ON STREET, HUNG HOM, KOWLOON, HONG KONG TEL : +852 - 27235100    FAX : +852 - 2314 9100";
                //worksheet.Cells["F2:T2"].Merge = true;
                worksheet.Cells["F2:T2"].Style.Font.Size = 12;
                worksheet.Cells["F2:T2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["F2:T2"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["F2:T2"].Style.Font.Bold = true;

                worksheet.Cells["F3"].Value = "Email Id : sales@sunrisediam.com    Web : www.sunrisediamonds.com.hk . Download Apps on Android, IOS and Windows";
                //worksheet.Cells["F3:S3"].Merge = true;
                worksheet.Cells["F3:S3"].Style.Font.Size = 12;
                worksheet.Cells["F3:S3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["F3:S3"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["F3:S3"].Style.Font.Bold = true;

                worksheet.Cells["B2:D2"].Value = "All Prices are final Selling Cash Price";
                //worksheet.Cells["B2:D2"].Merge = true;
                worksheet.Cells["B2:D2"].Style.Font.Bold = true;
                worksheet.Cells["B2:D2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["B2:D2"].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                worksheet.Cells["B2:D2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B2:D2"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

                worksheet.Cells["A6:" + "T6"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A6:" + "T6"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                worksheet.Cells["A6"].Value = "Total";
                worksheet.Cells["A6"].Style.Font.Bold = true;

                // For Border Lines
                worksheet.Cells["A6:" + "T6"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A6:" + "T6"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A6:" + "T6"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                worksheet.Cells["A7:" + "T7"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A7:" + "T7"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A7:" + "T7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                worksheet.Cells["A7:" + "T7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A7:" + "T7"].AutoFilter = true;


                int rowCount = 6; // Start Printing Records

                foreach (DataRow dr in p_dt.Rows)
                {
                    rowCount += 1;
                    for (int i = 1; i < p_dt.Columns.Count + 1; i++)
                    {
                        if (rowCount == 7) // For Colounm Header Name
                        {
                            worksheet.Cells[rowCount, i].Value = p_dt.Columns[i - 1].ColumnName;
                            worksheet.Cells[rowCount, i].Style.Font.Bold = true;
                            worksheet.Cells[rowCount, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[rowCount, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                            worksheet.Cells[rowCount, i].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[rowCount, i].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[rowCount, i].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[rowCount, i].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        }

                        // For Set 2 Decimal 
                        if (p_dt.Columns[i - 1].ColumnName == "Cts" || p_dt.Columns[i - 1].ColumnName == "Rap Price($)" ||
                          p_dt.Columns[i - 1].ColumnName == "Disc (%)")
                        {
                            try // For Decimal value Come Here
                            {
                                worksheet.Cells[rowCount + 1, i].Value = double.Parse(dr[i - 1].ToString());
                            }
                            catch // For Non Decimal or Blank Value come here
                            {
                                worksheet.Cells[rowCount + 1, i].Value = dr[i - 1].ToString();
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "Rap Amt($)") // For Set Formula Here
                        {
                            int _intcnt = rowCount + 1;
                            worksheet.Cells[rowCount + 1, i].Formula = "=I" + _intcnt + "*J" + _intcnt;
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "Net Amt($)") // For Set Formula Here
                        {
                            int _intcnt = rowCount + 1;
                            worksheet.Cells[rowCount + 1, i].Formula = "=K" + _intcnt + "+(" + "K" + _intcnt + "*L" + _intcnt + "/100" + ")";
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "Cut") // For Set Formula Here
                        {
                            worksheet.Cells[rowCount + 1, i].Value = dr[i - 1].ToString();
                            if (dr[i - 1].ToString() == "3EX")
                            {
                                worksheet.Cells[rowCount + 1, i].Style.Font.Bold = true;
                            }
                        }
                        else // For Others Value Come Here
                        {
                            worksheet.Cells[rowCount + 1, i].Value = dr[i - 1].ToString();
                        }
                    }
                }
                rowCount = rowCount + 1;

                // For Set Formula Here (Total)
                worksheet.Cells["D6"].Formula = "=SUBTOTAL(103," + "D8:" + "D" + rowCount + ")"; //Ref. No
                worksheet.Cells["I6"].Formula = "=SUBTOTAL(109," + "I8:" + "I" + rowCount + ")"; //Cts
                worksheet.Cells["K6"].Formula = "=SUBTOTAL(109," + "K8:" + "K" + rowCount + ")"; //Rap Amt($)
                worksheet.Cells["M6"].Formula = "=SUBTOTAL(109," + "M8:" + "M" + rowCount + ")"; //Net Amt($)
                worksheet.Cells["L6"].Formula = "=(1-(SUBTOTAL(109," + "M8:" + "M" + rowCount + ")/SUBTOTAL(109," + "K8:" + "K" + rowCount + ")))*-100"; //Disc (%)

                // For Set Front Color
                worksheet.Cells["L8:" + "L" + rowCount].Style.Font.Color.SetColor(System.Drawing.Color.Red);// Disc (%)
                worksheet.Cells["M8:" + "M" + rowCount].Style.Font.Color.SetColor(System.Drawing.Color.Red);// Net Amt($)

                //// For Set Front Bold
                worksheet.Cells["L8:" + "L" + rowCount].Style.Font.Bold = true;// Disc (%)
                worksheet.Cells["M8:" + "M" + rowCount].Style.Font.Bold = true;// Net Amt($)

                //// For Set Number Format Here
                worksheet.Cells["M8:" + "M" + rowCount].Style.Numberformat.Format = "0.00";// Net Amt($)
                worksheet.Cells["I8:" + "I" + rowCount].Style.Numberformat.Format = "0.00";// Cts
                worksheet.Cells["K8:" + "K" + rowCount].Style.Numberformat.Format = "0.00";// Rap Amt($)
                worksheet.Cells["L8:" + "L" + rowCount].Style.Numberformat.Format = "0.00";// Disc (%)                

                // For Total Formate Here
                worksheet.Cells["D6"].Style.Numberformat.Format = "#,##0";// Ref. No
                worksheet.Cells["I6"].Style.Numberformat.Format = "#,##0.00";// Cts
                worksheet.Cells["K6"].Style.Numberformat.Format = "#,##0";// Rap Amt($)
                worksheet.Cells["M6"].Style.Numberformat.Format = "#,##0";// Net Amt($)
                worksheet.Cells["L6"].Style.Numberformat.Format = "0.00";// Disc (%)

                worksheet.Cells["A6:AL25000"].AutoFitColumns(); // For Autofit Colounm
                worksheet.Column(26).Width = 60;// For Special columns width(Key To Symbol)
                worksheet.Column(40).Hidden = true; // For Hide Lab column
                worksheet.View.FreezePanes(8, 1); // For Panel Freeze
                int rowEnd = worksheet.Dimension.End.Row;
                removingGreenTagWarning(worksheet, worksheet.Cells[1, 1, rowEnd, 100].Address);
                package.Save();
            }
        }

        // Added By Kaushal 15-12-2018 Given Bhai Tejas Bhai
        public static void Excel_Data_Offer(DataTable p_dt, string _strFilePath, string _strcerti, string _strpurpose)
        {
            FileInfo newFile = new FileInfo(_strFilePath);
            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(_strFilePath);
            }
            string _date = System.DateTime.Today.ToString("dd-MMM-yyyy");
            string _strlabName = "";
            string _strcertino = "";
            string _strloction = "";
            string _strhyperlink = "";
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // For Set Excel Sheet WorkSheet Name
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("TotalStock");

                // For Set Company Name, Address & Others
                worksheet.Cells["B2"].Value = "Abbreviation";
                worksheet.Cells["B2"].Style.Font.Bold = true;
                worksheet.Cells["C2"].Value = "Buss. Proc";
                worksheet.Cells["D2"].Value = "B";
                worksheet.Cells["D2"].Style.Font.Bold = true;
                worksheet.Cells["B3:D3"].Value = "Offer(%) & Validity Compulsory";
                //worksheet.Cells["B3:D3"].Merge = true;
                worksheet.Cells["B3:D3"].Style.Font.Bold = true;
                worksheet.Cells["B3:D3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["B3:D3"].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                worksheet.Cells["B3:D3"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B3:D3"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                //worksheet.Cells["D3"].Value = "P";
                //worksheet.Cells["D3"].Style.Font.Bold = true;

                //worksheet.Cells["B4:F4"].Value = "Table & Crown Inclusion = White Inclusion";
                //worksheet.Cells["B4:F4"].Merge = true;
                //worksheet.Cells["B5:E5"].Value = "Table & Crown Natts = Black Inclusion";
                //worksheet.Cells["B5:E5"].Merge = true;

                worksheet.Cells["F1"].Value = "SUNRISE DIAMONDS INVENTORY FOR THE DATE " + _date + "";
                //worksheet.Cells["F1:S1"].Merge = true;
                worksheet.Cells["F1"].Style.Font.Size = 24;
                worksheet.Cells["F1:S1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["F1:S1"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["F1:S1"].Style.Font.Bold = true;

                worksheet.Cells["F2"].Value = "UNIT 1, 14/F, PENINSULA SQUARE, EAST WING, 18 SUNG ON STREET, HUNG HOM, KOWLOON, HONG KONG TEL : +852 - 27235100    FAX : +852 - 2314 9100";
                //worksheet.Cells["F2:T2"].Merge = true;
                worksheet.Cells["F2:T2"].Style.Font.Size = 12;
                worksheet.Cells["F2:T2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["F2:T2"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["F2:T2"].Style.Font.Bold = true;

                worksheet.Cells["F3"].Value = "Email Id : sales@sunrisediam.com    Web : www.sunrisediamonds.com.hk . Download Apps on Android, IOS and Windows";
                //worksheet.Cells["F3:S3"].Merge = true;
                worksheet.Cells["F3:S3"].Style.Font.Size = 12;
                worksheet.Cells["F3:S3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["F3:S3"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["F3:S3"].Style.Font.Bold = true;

                worksheet.Cells["B2:D2"].Value = "All Prices are final Selling Cash Price";
                //worksheet.Cells["B2:D2"].Merge = true;
                worksheet.Cells["B2:D2"].Style.Font.Bold = true;
                worksheet.Cells["B2:D2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["B2:D2"].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                worksheet.Cells["B2:D2"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["B2:D2"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);

                worksheet.Cells["A6:" + "AP6"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A6:" + "AP6"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Yellow);
                worksheet.Cells["A6"].Value = "Total";
                worksheet.Cells["A6"].Style.Font.Bold = true;

                // For Border Lines
                worksheet.Cells["A6:" + "AP6"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A6:" + "AP6"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A6:" + "AP6"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                worksheet.Cells["A7:" + "AO7"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A7:" + "AO7"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A7:" + "AO7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                worksheet.Cells["A7:" + "AO7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["A7:" + "AO7"].AutoFilter = true;
                worksheet.Cells["A7:" + "AO" + 7].Style.WrapText = true;


                int rowCount = 6; // Start Printing Records

                foreach (DataRow dr in p_dt.Rows)
                {
                    rowCount += 1;
                    for (int i = 1; i < p_dt.Columns.Count + 1; i++)
                    {
                        if (rowCount == 7) // For Colounm Header Name
                        {
                            if (p_dt.Columns[i - 1].ColumnName == "Validity")
                            {
                                worksheet.Cells[rowCount, i].Value = "Validity & Days";
                            }
                            else
                            {
                                worksheet.Cells[rowCount, i].Value = p_dt.Columns[i - 1].ColumnName;
                            }
                            worksheet.Cells[rowCount, i].Style.Font.Bold = true;
                            worksheet.Cells[rowCount, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                            worksheet.Cells[rowCount, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                            worksheet.Cells[rowCount, i].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[rowCount, i].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[rowCount, i].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                            worksheet.Cells[rowCount, i].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        }

                        // For Set 2 Decimal 
                        if (p_dt.Columns[i - 1].ColumnName == "Cts" || p_dt.Columns[i - 1].ColumnName == "Rap Price($)"
                           || p_dt.Columns[i - 1].ColumnName == "Length" || p_dt.Columns[i - 1].ColumnName == "Disc (%)"
                           || p_dt.Columns[i - 1].ColumnName == "Width" || p_dt.Columns[i - 1].ColumnName == "Depth"
                           || p_dt.Columns[i - 1].ColumnName == "Depth (%)" || p_dt.Columns[i - 1].ColumnName == "Table (%)"
                           || p_dt.Columns[i - 1].ColumnName == "Cr Ang" || p_dt.Columns[i - 1].ColumnName == "Cr Ht"
                           || p_dt.Columns[i - 1].ColumnName == "Pav Ang" || p_dt.Columns[i - 1].ColumnName == "Pav Ht"
                           || p_dt.Columns[i - 1].ColumnName == "Certi No" || p_dt.Columns[i - 1].ColumnName == "Offer"
                           || p_dt.Columns[i - 1].ColumnName == "Validity & Days")
                        {
                            try // For Decimal value Come Here
                            {
                                worksheet.Cells[rowCount + 1, i].Value = double.Parse(dr[i - 1].ToString());
                            }
                            catch // For Non Decimal or Blank Value come here
                            {
                                worksheet.Cells[rowCount + 1, i].Value = dr[i - 1].ToString();
                            }
                        }

                        else if (p_dt.Columns[i - 1].ColumnName.ToUpper() == "REF. NO")
                        {
                            try // For Decimal value Come Here
                            {
                                worksheet.Cells[rowCount + 1, i].Value = double.Parse(dr[i - 1].ToString());
                            }
                            catch // For Non Decimal or Blank Value come here
                            {
                                worksheet.Cells[rowCount + 1, i].Value = dr[i - 1].ToString();
                            }
                        }

                        else if (p_dt.Columns[i - 1].ColumnName == "Image") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""Image"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "HDMovie") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""HDMovie"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "SImage") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""SImage"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }

                        else if (p_dt.Columns[i - 1].ColumnName == "Lab") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                _strcertino = (dr[7].ToString());
                                _strlabName = (dr[41].ToString()); // Lab1 column data for lab name
                                _strloction = (dr[1].ToString());

                                if (_strloction == "Hong Kong")
                                {
                                    _strhyperlink = "https://sunrisediamonds.com.hk/certi/" + _strcertino + ".pdf";
                                    worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + _strhyperlink + @""",""" + _strlabName + @""")";
                                }
                                else
                                {
                                    worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""" + _strlabName + @""")";
                                }

                                //https://sunrisediamonds.com.hk/certi/5191592902.pdf
                                //worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""" + _strlabName + @""")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "DNA") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""DNA"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "VdoLink") // For Create Hyperlink
                        {
                            if (dr[i - 1].ToString() != "")
                            {
                                worksheet.Cells[rowCount + 1, i].Formula = @"=HYPERLINK(""" + dr[i - 1].ToString() + @""",""SVideo"")";
                                worksheet.Cells[rowCount + 1, i].Style.Font.UnderLine = true;
                                worksheet.Cells[rowCount + 1, i].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                            }
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "Rap Amt($)") // For Set Formula Here
                        {
                            int _intcnt = rowCount + 1;
                            worksheet.Cells[rowCount + 1, i].Formula = "=L" + _intcnt + "*M" + _intcnt;
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "Net Amt($)") // For Set Formula Here
                        {
                            int _intcnt = rowCount + 1;
                            worksheet.Cells[rowCount + 1, i].Formula = "=N" + _intcnt + "+(" + "N" + _intcnt + "*O" + _intcnt + "/100" + ")";
                        }
                        else if (p_dt.Columns[i - 1].ColumnName == "Cut") // For Set Formula Here
                        {
                            worksheet.Cells[rowCount + 1, i].Value = dr[i - 1].ToString();
                            if (dr[i - 1].ToString() == "3EX")
                            {
                                worksheet.Cells[rowCount + 1, i].Style.Font.Bold = true;
                            }
                        }
                        else // For Others Value Come Here
                        {
                            worksheet.Cells[rowCount + 1, i].Value = dr[i - 1].ToString();
                        }
                    }
                }
                rowCount = rowCount + 1;

                // For Set Formula Here (Total)
                worksheet.Cells["D6"].Formula = "=SUBTOTAL(103," + "E8:" + "E" + rowCount + ")"; //Ref. No
                worksheet.Cells["L6"].Formula = "=SUBTOTAL(109," + "L8:" + "L" + rowCount + ")"; //Cts
                worksheet.Cells["N6"].Formula = "=SUBTOTAL(109," + "N8:" + "N" + rowCount + ")"; //Rap Amt($)
                worksheet.Cells["P6"].Formula = "=SUBTOTAL(109," + "P8:" + "P" + rowCount + ")"; //Net Amt($)
                worksheet.Cells["O6"].Formula = "=(1-(SUBTOTAL(109," + "P8:" + "P" + rowCount + ")/SUBTOTAL(109," + "N8:" + "N" + rowCount + ")))*-100"; //Disc (%)

                // For Set Front Color
                worksheet.Cells["O8:" + "O" + rowCount].Style.Font.Color.SetColor(System.Drawing.Color.Red);// Disc (%)
                worksheet.Cells["P8:" + "P" + rowCount].Style.Font.Color.SetColor(System.Drawing.Color.Red);// Net Amt($)

                //// For Set Front Bold
                worksheet.Cells["O8:" + "O" + rowCount].Style.Font.Bold = true;// Disc (%)
                worksheet.Cells["P8:" + "P" + rowCount].Style.Font.Bold = true;// Net Amt($)

                //// For Set Number Format Here
                worksheet.Cells["P8:" + "P" + rowCount].Style.Numberformat.Format = "0.00";// Net Amt($)
                worksheet.Cells["L8:" + "L" + rowCount].Style.Numberformat.Format = "0.00";// Cts
                worksheet.Cells["N8:" + "N" + rowCount].Style.Numberformat.Format = "0.00";// Rap Amt($)
                worksheet.Cells["O8:" + "O" + rowCount].Style.Numberformat.Format = "0.00";// Disc (%)
                worksheet.Cells["Y8:" + "Y" + rowCount].Style.Numberformat.Format = "0.00";// Table (%)
                worksheet.Cells["U8:" + "U" + rowCount].Style.Numberformat.Format = "0.00";// Length
                worksheet.Cells["V8:" + "V" + rowCount].Style.Numberformat.Format = "0.00";// Width
                worksheet.Cells["W8:" + "W" + rowCount].Style.Numberformat.Format = "0.00";// Depth
                worksheet.Cells["X8:" + "X" + rowCount].Style.Numberformat.Format = "0.00";// Depth (%)
                worksheet.Cells["Y8:" + "Y" + rowCount].Style.Numberformat.Format = "0.00";// Table (%)
                worksheet.Cells["AH8:" + "AH" + rowCount].Style.Numberformat.Format = "0.00";// Cr Ang
                worksheet.Cells["AI8:" + "AI" + rowCount].Style.Numberformat.Format = "0.00";// Cr Ht
                worksheet.Cells["AJ8:" + "AJ" + rowCount].Style.Numberformat.Format = "0.00";// Pav Ang
                worksheet.Cells["AK8:" + "AK" + rowCount].Style.Numberformat.Format = "0.00";// Pav Ang

                // For Total Formate Here
                worksheet.Cells["D6"].Style.Numberformat.Format = "#,##0";// Ref. No
                worksheet.Cells["L6"].Style.Numberformat.Format = "#,##0.00";// Cts
                worksheet.Cells["N6"].Style.Numberformat.Format = "#,##0";// Rap Amt($)
                worksheet.Cells["P6"].Style.Numberformat.Format = "#,##0";// Net Amt($)
                worksheet.Cells["O6"].Style.Numberformat.Format = "0.00";// Disc (%)

                worksheet.Cells["A6:AO25000"].AutoFitColumns(); // For Autofit Colounm
                worksheet.Cells["A6:" + "AO" + rowCount].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells["AB7:" + "AB" + rowCount].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;//(Key To Symbol)
                worksheet.Column(28).Width = 60;// For Special columns width(Key To Symbol)
                if (_strpurpose == "Mail")
                {
                    worksheet.Column(40).Hidden = true;
                    worksheet.Column(41).Hidden = true;
                    worksheet.Column(42).Hidden = true;
                }
                else
                {
                    worksheet.Column(42).Hidden = true; // For Hide Lab column
                }
                worksheet.View.FreezePanes(8, 1); // For Panel Freeze
                int rowEnd = worksheet.Dimension.End.Row;
                removingGreenTagWarning(worksheet, worksheet.Cells[1, 1, rowEnd, 100].Address);
                package.Save();
            }
        }

        // Added By Jubin Shah 04-10-2018 Given Bhai Tejas Bhai
        public static void Excel_CopyData(DataTable p_dt, string _strFilePath)
        {
            int _intno = p_dt.Rows.Count + 7;
            FileInfo newFile = new FileInfo(_strFilePath);
            if (newFile.Exists)
            {
                newFile.Delete(); // ensures we create a new workbook
                newFile = new FileInfo(_strFilePath);
            }
            string _date = System.DateTime.Today.ToString("dd-MMM-yyyy");

            using (ExcelPackage pck = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = pck.Workbook.Worksheets.Add("TotalStock");
                // Hard Code Part // Added By Jubin Shah 
                worksheet.Cells["B2"].Value = "Abbreviation";
                worksheet.Cells["B2"].Style.Font.Bold = true;
                worksheet.Cells["C2"].Value = "Buss. Proc";
                worksheet.Cells["D2"].Value = "B";
                worksheet.Cells["D2"].Style.Font.Bold = true;
                worksheet.Cells["C3"].Value = "Promotion";
                worksheet.Cells["D3"].Value = "P";
                worksheet.Cells["D3"].Style.Font.Bold = true;

                //worksheet.Cells["B4:F4"].Value = "Table & Crown Inclusion = White Inclusion";
                //worksheet.Cells["B4:F4"].Merge = true;
                //worksheet.Cells["B5:E5"].Value = "Table & Crown Natts = Black Inclusion";
                //worksheet.Cells["B5:E5"].Merge = true;

                worksheet.Cells["G1:Q1"].Value = "SUNRISE DIAMONDS INVENTORY FOR THE DATE " + _date + "";
                //worksheet.Cells["G1:Q1"].Merge = true;
                worksheet.Cells["G1:Q1"].Style.Font.Size = 24;
                worksheet.Cells["G1:Q1"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["G1:Q1"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["G1:Q1"].Style.Font.Bold = true;

                worksheet.Cells["G2:U2"].Value = "UNIT 1, 14/F, PENINSULA SQUARE, EAST WING, 18 SUNG ON STREET, HUNG HOM, KOWLOON, HONG KONG TEL : +852 - 27235100    FAX : +852 - 2314 9100";
                //worksheet.Cells["G2:U2"].Merge = true;
                worksheet.Cells["G2:U2"].Style.Font.Size = 12;
                worksheet.Cells["G2:U2"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["G2:U2"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["G2:U2"].Style.Font.Bold = true;

                worksheet.Cells["G3:Q3"].Value = "Email Id : sales@sunrisediam.com    Web : www.sunrisediamonds.com.hk . Download Apps on Android, IOS and Windows";
                //worksheet.Cells["G3:Q3"].Merge = true;
                worksheet.Cells["G3:Q3"].Style.Font.Size = 12;
                worksheet.Cells["G3:Q3"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                worksheet.Cells["G3:Q3"].Style.Font.Color.SetColor(System.Drawing.Color.Blue);
                worksheet.Cells["G3:Q3"].Style.Font.Bold = true;

                worksheet.Cells["A6:" + "AI6"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A6:" + "AI6"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                worksheet.Cells["A6"].Value = "Summary";
                worksheet.Cells["A6"].Style.Font.Bold = true;

                // For Border Lines
                worksheet.Cells["A6:" + "AI6"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A6:" + "AI6"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A6:" + "AI6"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                worksheet.Cells["A7:" + "AI7"].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A7:" + "AI7"].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                worksheet.Cells["A7:" + "AI7"].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                worksheet.Cells["A7:" + "AI7"].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                // Data Fill Part & Property Set Part 
                worksheet.Cells["A7"].LoadFromDataTable(p_dt, true);
                worksheet.Cells["A7:" + "AI7"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                worksheet.Cells["A7:" + "AI7"].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                worksheet.Cells["A7:" + "AI7"].AutoFilter = true;
                worksheet.Cells["A7:" + "AI7"].Style.Font.Bold = true;

                // For Formula
                worksheet.Cells["E6"].Formula = "=SUBTOTAL(103," + "E8:" + "E" + _intno + ")";
                worksheet.Cells["K6"].Formula = "=SUBTOTAL(109," + "K8:" + "K" + _intno + ")";
                worksheet.Cells["P6"].Formula = "=SUBTOTAL(109," + "P8:" + "P" + _intno + ")";
                worksheet.Cells["R6"].Formula = "=SUBTOTAL(109," + "R8:" + "R" + _intno + ")";

                // worksheet.Cells["B8:" + "B" + _intno].Formula = @"=HYPERLINK(""" + "B8:" + "B" + _intno + @""",""SImage"")";

                // worksheet.Cells["Q6"].Formula = "=SUBTOTAL(109," + "Q8:" + "Q" + _intno + ")"; //=(1- (SUBTOTAL(109,S8:S13972)/SUBTOTAL(109,Q8:Q13972) ))*-100

                // For 2 Decimal 
                worksheet.Cells["K8:" + "K" + _intno].Style.Numberformat.Format = "0.00";// Cts
                worksheet.Cells["Q8:" + "Q" + _intno].Style.Numberformat.Format = "0.00";// Discount
                worksheet.Cells["R8:" + "R" + _intno].Style.Numberformat.Format = "0.00";// Net Amt($)
                worksheet.Cells["W8:" + "W" + _intno].Style.Numberformat.Format = "0.00";// Length
                worksheet.Cells["X8:" + "X" + _intno].Style.Numberformat.Format = "0.00";// Width
                worksheet.Cells["Y8:" + "Y" + _intno].Style.Numberformat.Format = "0.00";// Depth
                worksheet.Cells["Z8:" + "Z" + _intno].Style.Numberformat.Format = "0.00";// Depth (%)
                worksheet.Cells["AA8:" + "AA" + _intno].Style.Numberformat.Format = "0.00";// Table (%)

                worksheet.Cells["AE8:" + "AE" + _intno].Style.Numberformat.Format = "0.00";// Cr Ang
                worksheet.Cells["AF8:" + "AF" + _intno].Style.Numberformat.Format = "0.00";// Cr Ht
                worksheet.Cells["AG8:" + "AG" + _intno].Style.Numberformat.Format = "0.00";// Pav Ang
                worksheet.Cells["AH8:" + "AH" + _intno].Style.Numberformat.Format = "0.00";// Pav Ht

                // For Set Front Color
                worksheet.Cells["Q8:" + "Q" + _intno].Style.Font.Color.SetColor(System.Drawing.Color.Red);// Discount
                worksheet.Cells["R8:" + "R" + _intno].Style.Font.Color.SetColor(System.Drawing.Color.Red);// Net Amt($)

                // For Set Front Color
                worksheet.Cells["Q8:" + "Q" + _intno].Style.Font.Bold = true;// Discount
                worksheet.Cells["R8:" + "R" + _intno].Style.Font.Bold = true;// Net Amt($)

                worksheet.Cells["E7:AI25000"].AutoFitColumns();
                worksheet.View.FreezePanes(8, 1);
                int rowEnd = worksheet.Dimension.End.Row;
                removingGreenTagWarning(worksheet, worksheet.Cells[1, 1, rowEnd, 100].Address);
                pck.Save();
            }
        }


        public static object GetConvertedDecimalValue(object row)
        {
            string values_2;
            decimal number_2;
            bool success2;

            if (row != null)
            {
                if (row.GetType().Name != "DBNull")
                    values_2 = Convert.ToString(row);
                else
                    values_2 = "";
            }
            else
            {
                values_2 = "";
            }
            success2 = decimal.TryParse(values_2, out number_2);
            if (success2)
            {
                return Convert.ToDecimal(row);
            }
            else
            {
                return values_2;
            }
        }

        public static object GetConvertedIntValue(object row)
        {
            string values_2;
            Int64 number_2;
            bool success2;

            if (row != null)
            {
                if (row.GetType().Name != "DBNull")
                    values_2 = Convert.ToString(row);
                else
                    values_2 = "";
            }
            else
            {
                values_2 = "";
            }
            success2 = Int64.TryParse(values_2, out number_2);
            if (success2)
            {
                return Convert.ToInt64(row);
            }
            else
            {
                return values_2;
            }
        }

        public void AddNewRow(String Address)
        {
            //UInt32 rowNumber = GetRowIndex(Address);
            ////Row row = GetRow(sheetData1, rowNumber);
            //this.CurrentRowCount += 1;
            this.SetCellValue(Address, "");
        }

        public String GetTableName()
        {
            return _TableName;
        }

        protected void RaiseFillingWorksheet(FillingWorksheetEventArgs e)
        {
            if (FillingWorksheetEvent != null)
            {
                FillingWorksheetEvent(this, ref e);
            }

        }

        private void setDefaultStyleIndex()
        {
            ExcelCellFormat ef = new ExcelCellFormat();
            //ef.backgroundArgb = KnownHexValue.White;
            ef.isbold = false;
            ef.isitalic = false;
            DefaultStyleindex = AddStyle(ef);
        }

        public static UInt32 GetRowIndex(string address)
        {
            string rowPart;
            UInt32 l;
            UInt32 result = 0;

            for (int i = 0; i < address.Length; i++)
            {
                if (UInt32.TryParse(address.Substring(i, 1), out l))
                {
                    rowPart = address.Substring(i, address.Length - i);
                    if (UInt32.TryParse(rowPart, out l))
                    {
                        result = l;
                        break;
                    }
                }
            }
            return result;
        }

        public String GetColumnName(int ColIndex)
        {
            return AllColumns[ColIndex].Caption;
        }

        public int GetColumnIndex(String ColName)
        {
            //return AllColumns[ColIndex].Caption;
            foreach (ExcelHeader item in AllColumns)
            {
                if (item.Caption == ColName)
                    return item.ColInd;
            }
            return -1;
        }

        public static String GetColumnAlphaIndex(double ColIndex)
        {

            if (ColIndex <= 0) return "";

            double LastCharIndex = ColIndex % 26;

            if (ColIndex <= 0)
            {
                return GetCharacter(ColIndex);
            }
            else if (ColIndex <= 26)
            {
                return GetCharacter(ColIndex);
            }
            else
            {
                if (LastCharIndex == 0)
                {
                    LastCharIndex = 26;
                }

                string Ch = GetCharacter(LastCharIndex);
                return GetColumnAlphaIndex(Math.Floor((ColIndex - LastCharIndex) / 26)) + Ch;
            }


        }

        private static string GetCharacter(double Index)
        {

            string c = ((char)(64 + Index)).ToString();


            return c;


        }

        public String ReplaceFormulaWithAlphaIndex(String formula)
        {
            foreach (ExcelHeader item in AllColumns)
            {
                formula = formula.Replace(this.GetTableName() + "[" + item.Caption + "]", GetColumnAlphaIndex(item.ColInd) + this.TableDetailStartRow + ":" + GetColumnAlphaIndex(item.ColInd) + (this.TableFooterStartRow - 1).ToString());
            }
            return formula;
        }

        public String ReplaceFormulaWithAlphaIndexDetail(String formula)
        {
            foreach (ExcelHeader item in AllColumns)
            {
                formula = formula.Replace(this.GetTableName() + "[[#This Row],[" + item.Caption + "]]", GetColumnAlphaIndex(item.ColInd) + this.CurrentRowCount.ToString());
            }
            return formula;
        }

        public EpExcelExport(string SheetName, String TableName)
        {
            this._SheetName = SheetName;
            this._TableName = TableName;
        }


        public uint AddStyle(ExcelFormat format)
        {
            return this.AddStyle(this._worksheet, format);
        }

        public uint AddStyle(ExcelWorksheet ws, ExcelFormat format)
        {
            foreach (ExcelFormat cf in StyleList.Values)
            {
                if (cf.IsEqual(format))
                {
                    return (UInt32)cf.StyleInd;
                }
            }

            //if (stylesheet1.CellFormats == null)
            //{
            //    stylesheet1.CellFormats = new CellFormats();

            //}

            //if (stylesheet1.CellFormats.ChildElements.Count == 0)
            //{
            //    stylesheet1.CellFormats.Append(new CellFormat());
            //}

            //uint newStyle = this.AddCellFormat(stylesheet1, format.fontsize, format.forColorArgb, format.fontname, format.FontFamilyindex
            //, format.fontScheme, format.isbold, format.isitalic, format.ul,
            //format.pattern, format.backgroundArgb, format.left, format.right, format.top, format.bottom, format.diagonal, format.Format, format.HorizontalAllign, format.VerticalAllign);





            uint newStyle = (uint)StyleList.Count() + 1;
            var namedStyle = ws.Workbook.Styles.CreateNamedStyle(newStyle.ToString());

            if (format.FontFamilyindex > 0)
                namedStyle.Style.Font.Family = format.FontFamilyindex;

            if (!String.IsNullOrEmpty(format.fontname))
                namedStyle.Style.Font.Name = format.fontname;

            if (format.fontsize > 0)
                namedStyle.Style.Font.Size = (float)format.fontsize;

            namedStyle.Style.Font.Bold = format.isbold;
            namedStyle.Style.Font.Italic = format.isitalic;
            namedStyle.Style.VerticalAlignment = format.VerticalAllign;
            namedStyle.Style.HorizontalAlignment = format.HorizontalAllign;

            if (format.ul == ExcelUnderLineType.None)
            {

            }
            else
            {
                namedStyle.Style.Font.UnderLineType = format.ul;
            }

            if (!string.IsNullOrEmpty(format.forColorArgb))
                namedStyle.Style.Font.Color.SetColor(System.Drawing.Color.FromArgb(Convert.ToInt32(format.forColorArgb)));


            if (!string.IsNullOrEmpty(format.backgroundArgb))
            {
                namedStyle.Style.Fill.PatternType = ExcelFillStyle.Solid;
                namedStyle.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(Convert.ToInt32(format.backgroundArgb)));
            }


            if (!string.IsNullOrEmpty(format.Format))
                namedStyle.Style.Numberformat.Format = format.Format;

            format.StyleInd = newStyle;

            StyleList.Add(newStyle, format);

            return newStyle;
        }


        public void ApplyStyle(ExcelRange c, uint StyleInd)
        {
            c.StyleName = StyleInd.ToString();


            //c.StyleIndex = StyleInd;
        }

        public void ApplyStyle(ExcelWorksheet ws, ExcelRange c, ExcelFormat format)
        {

            UInt32 styleind = AddStyle(ws, format);

            ApplyStyle(c, styleind);


            //format.StyleInd = c.StyleIndex;


            format.StyleInd = Convert.ToUInt32(c.StyleName.ToString());
            //format.StyleInd = styleind;



            //if (!StyleList.ContainsKey(styleind))
            //{
            //    StyleList.Add(c.StyleIndex, format);
            //}

        }


        protected virtual void CreateHeader(FillingWorksheetEventArgs e)
        {
            AddHeaderEventArgs AddHeaderpara = new AddHeaderEventArgs(e.Worksheet, CurrentRowCount);

            if (AddHeaderEvent != null)
            {
                AddHeaderEvent(this, ref AddHeaderpara);
            }

            CurrentRowCount = AddHeaderpara.HeaderRowcount;
        }

        protected virtual void CreateDetail(FillingWorksheetEventArgs e)
        {

        }

        protected virtual void CreateFooter(FillingWorksheetEventArgs e)
        {
            Int32 CellColNo = 0;
            CurrentRowCount = CurrentRowCount + 1;
            //Row row = GetRow(e.Data, (uint)CurrentRowCount);

            for (int i = 0; i < AllColumns.Count; i++)
            {
                ExcelHeader h = AllColumns[i];


                if (h.visible == true)
                {
                    CellColNo = h.ColInd;

                    string colname = GetColumnAlphaIndex(CellColNo);
                    BeforeCreateCellEventArgs CellArgs = new BeforeCreateCellEventArgs();
                    //CellArgs.ColDataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                    CellArgs.ta = TableArea.Footer;
                    RaiseBeforeCreateCellEvent(CellArgs);
                    //Cell c = this.InsertCellInRow(e.Data, row, colname + (CurrentRowCount).ToString());
                    ExcelRange c = this._worksheet.Cells[colname + (CurrentRowCount).ToString()];
                    ExcelCellFormat cf = new ExcelCellFormat();
                    //cf.ul = UnderlineValues.None;
                    //c.DataType = CellArgs.ColDataType;
                    string prevText = "";

                    if (c.Value != null)
                    {
                        prevText = c.Value.ToString();
                    }


                    if (h.SummFunction != TotalsRowFunctionValues.None)
                    {
                        //col.TotalsRowFunction = AllColumns[i].SummFunction;



                        //cf.Formula = GetSummFormula(h.Caption, h.SummFunction);

                        //String vBaseFormula = GetSummFormula(h.Caption, h.SummFunction);
                        //vBaseFormula = vBaseFormula.Replace(this.GetTableName() + "[" + h.Caption + "]", colname + this.TableDetailStartRow.ToString() + ":" + colname + (this.TableFooterStartRow - 1).ToString());
                        //cf.Formula = vBaseFormula;

                        //cf.Formula = GetSummFormula(h.Caption, h.SummFunction).Replace(cFORMULA_START_INDEX, this.TableDetailStartRow.ToString()).Replace(cFORMULA_END_INDEX, (this.TableFooterStartRow - 1).ToString());
                        //priyanka on date [24-Oct-16]
                        //cf.Formula = ReplaceFormulaWithAlphaIndex(GetSummFormula(h.Caption, h.SummFunction));
                        ////priyanka on date [24-Oct-16] edn
                        if (h.SummFunction == TotalsRowFunctionValues.Custom &&
                            h.SummFormula != null &&
                            h.SummFormula.Length > 0)
                        {

                            //cf.Formula= h.SummFormula;

                            //vBaseFormula = h.SummFormula;
                            //vBaseFormula = vBaseFormula.Replace(this.GetTableName() + "[" + h.Caption + "]", colname + this.TableDetailStartRow.ToString() + ":" + colname + (this.TableFooterStartRow - 1).ToString());
                            //cf.Formula = vBaseFormula;

                            //cf.Formula = h.SummFormula.Replace(cFORMULA_START_INDEX, this.TableDetailStartRow.ToString()).Replace(cFORMULA_END_INDEX, (this.TableFooterStartRow - 1).ToString());

                            cf.Formula = ReplaceFormulaWithAlphaIndex(h.SummFormula);
                        }//priyanka on date [24-Oct-16]
                        else
                        {
                            cf.Formula = ReplaceFormulaWithAlphaIndex(GetSummFormula(h.Caption, h.SummFunction));
                        }
                        //priyanka on date [24-Oct-16] end//
                        //AddFormulaCalculationChain(colname + (TableFooterStartRow).ToString());
                    }


                    cf.tableArea = TableArea.Footer;
                    cf.RowInd = (int)CurrentRowCount;
                    cf.ColInd = CellColNo;
                    cf.HeaderStartRow = TableHeaderStartRow;
                    cf.DetailStartRow = TableDetailStartRow;
                    cf.ColumnName = h.Caption;
                    cf.Format = h.NumFormat;
                    cf.HorizontalAllign = h.HorizontalAllign;
                    cf.VerticalAllign = h.VerticalAllign;
                    cf.Text = prevText;

                    RaiseAfterCreateCellEvent(ref cf);

                    ApplyStyle(e.Worksheet, c, cf);

                    if (cf.Formula != null && cf.Formula.Length > 0)
                    {
                        //CellFormula cfor = new CellFormula();
                        //cfor.FormulaType = CellFormulaValues.Normal;
                        //cfor.Text = cf.Formula;
                        //AddFormulaCalculationChain(c.CellReference);
                        //c.Append(cfor);
                        //RJCHANGE
                        c.Formula = cf.Formula;
                        //SetCellValue(c, "=" + cf.Formula);
                    }
                    else if (cf.Text != null)
                    {
                        SetCellValue(c, cf.Text);
                    }
                }
            }

            AddHeaderEventArgs AddHeaderpara = new AddHeaderEventArgs(e.Worksheet, 0);
            AddFooterEventArgs footerArgs = new AddFooterEventArgs(e.Worksheet, AddHeaderpara.HeaderRowcount, CurrentRowCount);

            if (AddFooterEvent != null)
            {
                AddFooterEvent(this, ref footerArgs);
            }

        }
        public virtual void CreateExcel(System.IO.Stream ms, String TempFolderPath)
        {
            this.PrepareExcel(ms, TempFolderPath);
            this.Save();
        }

        public virtual void CreateExcel(System.IO.Stream ms, String TempFolderPath, int iTransId)
        {
            this.PrepareExcel(ms, TempFolderPath, iTransId);
            this.Save();
        }

        public virtual void PrepareExcel(System.IO.Stream ms, String TempFolderPath)
        {
            try
            {
                //String xFileName = TempFolderPath + System.DateTime.Now.ToString("ddMMyyyy-HHmmss-fffffff") + ".xlsx";

                Random rnd = new Random();
                String xFileName = TempFolderPath + rnd.Next().ToString() + ".xlsx";

                try
                {
                    if (System.IO.File.Exists(xFileName))
                        System.IO.File.Delete(xFileName);
                }
                catch (Exception) { }

                System.IO.FileInfo x = new FileInfo(xFileName);
                MemoryStream tmpMs = new MemoryStream();
                //RJCHECK
                //using (ExcelPackage package = new ExcelPackage(tmpMs))
                using (ExcelPackage package = new ExcelPackage(x))
                {
                    _worksheet = package.Workbook.Worksheets.Add(_SheetName);
                    CurrentRowCount = 0;
                    List<ExcelColumn> columns1 = new List<ExcelColumn>();
                    this.AllColumns = new List<ExcelHeader>();
                    this.AddColumnDef(this.AllColumns);
                    List<string> collist = new List<string>();
                    StyleList = new SortedList<UInt32, ExcelFormat>();
                    ///priyanka on date [24-Oct-16]
                    //for (int i = 0; i < this.AllColumns.Count; i++)
                    //{
                    //    ExcelHeader h = this.AllColumns[i];
                    //    if (h.visible == true)
                    //    {

                    //        if (h.Width > 0)
                    //        {

                    //            this._worksheet.Column(h.ColInd).Width = h.Width;
                    //            this._worksheet.Column(h.ColInd).ColumnMax = h.ColInd;
                    //        }


                    //        fistColAdded = true;
                    //        string colName = h.Caption;

                    //        if (collist.Contains(h.Caption))
                    //        {
                    //            colName = colName + (collist.Count + 1).ToString();
                    //        }

                    //        collist.Add(colName);

                    //        ExcelCellFormat f = new ExcelCellFormat();
                    //        f.Format = h.NumFormat;
                    //    }
                    //}

                    // TableHeaderStartRow = (int)CurrentRowCount;
                    /////priyanka on date [24-Oct-16] end//
                    setDefaultStyleIndex();

                    FillingWorksheetEventArgs e = new FillingWorksheetEventArgs(_worksheet, null);
                    RaiseFillingWorksheet(e);

                    this.CreateHeader(e);

                    if (this._worksheet.Cells.Count() > 0)
                    {
                        CurrentRowCount = Convert.ToUInt32(this._worksheet.Cells.LastOrDefault().Start.Row + 1);
                        // Change By Hitesh on [31-03-2016] as per [Doc No 201]
                        // Freeze Header Row when Export 
                        int HeaderRowCount = (int)CurrentRowCount + 1;
                        this._worksheet.View.FreezePanes(HeaderRowCount, 1);
                        // End By Hitesh on [31-03-2016] as per [Doc No 201]
                    }

                    //sET CurrentRowCount

                    String StartAddr = "";
                    String EndArre = "";

                    for (int i = 0; i < AllColumns.Count; i++)
                    {
                        ExcelHeader h = AllColumns[i];
                        ///priyanka on date [24-Oct-16]
                        if (h.visible == true)
                        {
                            // Change By Hitesh on [31-03-2016] as per [Doc No 201]
                            VisibleColumn += 1;
                            // End By Hitesh on [31-03-2016] as per [Doc No 201]
                            if (h.Width > 0)
                            {

                                this._worksheet.Column(h.ColInd).Width = h.Width;
                                this._worksheet.Column(h.ColInd).ColumnMax = h.ColInd;
                            }

                            string colName = h.Caption;

                            if (collist.Contains(h.Caption))
                            {
                                colName = colName + (collist.Count + 1).ToString();
                            }

                            collist.Add(colName);

                            ExcelCellFormat f = new ExcelCellFormat();
                            f.Format = h.NumFormat;
                            ///priyanka on date [24-Oct-16] end//
                            string colname = GetColumnAlphaIndex(h.ColInd);
                            //string colname = GetColumnAlphaIndex(i + 1);

                            BeforeCreateCellEventArgs CellArgs = new BeforeCreateCellEventArgs();
                            CellArgs.ColDataType = this.AllColumns[i].ColDataType;
                            CellArgs.ta = TableArea.Detail;

                            RaiseBeforeCreateCellEvent(CellArgs);

                            //Cell c = this.InsertCellInRow(sheetData1, row1, colname + (row1.RowIndex).ToString());

                            ExcelCellFormat cf = new ExcelCellFormat();

                            ExcelRange c = this._worksheet.Cells[colname + (this.CurrentRowCount).ToString()];

                            if (h.Width <= 0)
                            {
                                c.AutoFitColumns();
                            }

                            //cf.ul = DocumentFormat.OpenXml.Spreadsheet.UnderlineValues.None;

                            //ExcelRange c = this._worksheet.Cells[colname + (row1.RowIndex)].ToString();

                            SetCellValue(c, h.Caption);

                            cf.tableArea = TableArea.Header;
                            cf.HeaderStartRow = TableHeaderStartRow;
                            cf.ColumnName = h.Caption;
                            cf.Format = h.NumFormat;
                            cf.HorizontalAllign = h.HorizontalAllign;
                            cf.VerticalAllign = h.VerticalAllign;
                            string prevtext = cf.ColumnName;
                            cf.Text = prevtext;

                            RaiseAfterCreateCellEvent(ref cf);

                            ApplyStyle(this._worksheet, c, cf);

                            //if (cf.Formula != null && cf.Formula.Length > 0)
                            //{
                            //    CellFormula cfor = new CellFormula();
                            //    cfor.FormulaType = CellFormulaValues.Normal;
                            //    cfor.Text = cf.Formula;
                            //    c.Append(cfor);
                            //}

                            if (cf.Text != null && cf.Text != prevtext)
                            {
                                SetCellValue(c, cf.Text);
                            }

                            if (StartAddr == "")
                                StartAddr = c.Address;

                            EndArre = c.Address;
                            //
                        }
                    }


                    this._worksheet.Cells[StartAddr + ":" + EndArre].AutoFilter = true;

                    e.StartrowCount = 0;

                    TableDetailStartRow = (int)CurrentRowCount + 1;
                    CreateDetail(e);

                    TableFooterStartRow = (int)CurrentRowCount + 1;
                    CreateFooter(e);

                    // Change By Hitesh on [31-03-2016] as per [Doc No 201]
                    // Add For AutoFit Column
                    //this._worksheet.Cells[(TableDetailStartRow - 1), 1, (TableFooterStartRow), VisibleColumn].AutoFitColumns();
                    // Add For Cell Border
                    this._worksheet.Cells[(TableDetailStartRow - 1), 1, (int)CurrentRowCount, VisibleColumn].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    this._worksheet.Cells[(TableDetailStartRow - 1), 1, (int)CurrentRowCount, VisibleColumn].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    this._worksheet.Cells[(TableDetailStartRow - 1), 1, (int)CurrentRowCount, VisibleColumn].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    this._worksheet.Cells[(TableDetailStartRow - 1), 1, (int)CurrentRowCount, VisibleColumn].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    // End By Hitesh on [31-03-2016] as per [Doc No 201]
                    ///priyanka on date [24-Oct-16]
                    //this._worksheet.Workbook.Styles.UpdateXml();
                    //_worksheet.Workbook.Calculate();
                    ///priyanka on date [24-Oct-16] end//
                    //_worksheet.Cells[this.TableHeaderStartRow,0].AutoFilter = true;

                    //_worksheet.InsertRow(1, 1, 1);
                    //_worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

                    //RJCHECK
                    //_worksheet.Calculate();
                    //this._worksheet.Workbook.Calculate();
                    ////package.Save();
                    //ResAry = package.GetAsByteArray();
                    package.Save();
                }

                byte[] byteDatan = System.IO.File.ReadAllBytes(xFileName);

                try
                {
                    if (System.IO.File.Exists(xFileName))
                        System.IO.File.Delete(xFileName);
                }
                catch (Exception) { }

                ms.Write(byteDatan, 0, byteDatan.Length);
                ms.Flush();
                ms.Position = 0;

                //System.IO.MemoryStream RetMs = new System.IO.MemoryStream();

                //RJCHECK
                //byte[] byteDatan = ResAry;
                //ms.Write(byteDatan, 0, byteDatan.Length);
                //ms.Flush();
                //ms.Position = 0;
            }
            catch (Exception ex)
            {
                Lib.Model.Common.InsertErrorLog(ex, null, null);
            }
        }

        public virtual void PrepareExcel(System.IO.Stream ms, String TempFolderPath, int iTransId)
        {
            //String xFileName = TempFolderPath + System.DateTime.Now.ToString("ddMMyyyy-HHmmss-fffffff") + ".xlsx";

            Random rnd = new Random();
            String xFileName = TempFolderPath + rnd.Next().ToString() + ".xlsx";


            System.IO.FileInfo x = new FileInfo(xFileName);
            MemoryStream tmpMs = new MemoryStream();
            //RJCHECK
            //using (ExcelPackage package = new ExcelPackage(tmpMs))
            using (ExcelPackage package = new ExcelPackage(x))
            {
                _worksheet = package.Workbook.Worksheets.Add(_SheetName);
                CurrentRowCount = 0;
                List<ExcelColumn> columns1 = new List<ExcelColumn>();
                this.AllColumns = new List<ExcelHeader>();
                this.AddColumnDef(this.AllColumns, iTransId);
                List<string> collist = new List<string>();
                StyleList = new SortedList<UInt32, ExcelFormat>();
                ///priyanka on date [24-Oct-16]
                //for (int i = 0; i < this.AllColumns.Count; i++)
                //{
                //    ExcelHeader h = this.AllColumns[i];
                //    if (h.visible == true)
                //    {

                //        if (h.Width > 0)
                //        {

                //            this._worksheet.Column(h.ColInd).Width = h.Width;
                //            this._worksheet.Column(h.ColInd).ColumnMax = h.ColInd;
                //        }


                //        fistColAdded = true;
                //        string colName = h.Caption;

                //        if (collist.Contains(h.Caption))
                //        {
                //            colName = colName + (collist.Count + 1).ToString();
                //        }

                //        collist.Add(colName);

                //        ExcelCellFormat f = new ExcelCellFormat();
                //        f.Format = h.NumFormat;
                //    }
                //}

                // TableHeaderStartRow = (int)CurrentRowCount;
                /////priyanka on date [24-Oct-16] end//
                setDefaultStyleIndex();

                FillingWorksheetEventArgs e = new FillingWorksheetEventArgs(_worksheet, null);
                RaiseFillingWorksheet(e);

                this.CreateHeader(e);

                if (this._worksheet.Cells.Count() > 0)
                {
                    CurrentRowCount = Convert.ToUInt32(this._worksheet.Cells.LastOrDefault().Start.Row + 1);
                    // Change By Hitesh on [31-03-2016] as per [Doc No 201]
                    // Freeze Header Row when Export 
                    int HeaderRowCount = (int)CurrentRowCount + 1;
                    this._worksheet.View.FreezePanes(HeaderRowCount, 1);
                    // End By Hitesh on [31-03-2016] as per [Doc No 201]
                }

                //sET CurrentRowCount

                String StartAddr = "";
                String EndArre = "";

                for (int i = 0; i < AllColumns.Count; i++)
                {
                    ExcelHeader h = AllColumns[i];
                    ///priyanka on date [24-Oct-16]
                    if (h.visible == true)
                    {
                        // Change By Hitesh on [31-03-2016] as per [Doc No 201]
                        VisibleColumn += 1;
                        // End By Hitesh on [31-03-2016] as per [Doc No 201]
                        if (h.Width > 0)
                        {

                            this._worksheet.Column(h.ColInd).Width = h.Width;
                            this._worksheet.Column(h.ColInd).ColumnMax = h.ColInd;
                        }

                        string colName = h.Caption;

                        if (collist.Contains(h.Caption))
                        {
                            colName = colName + (collist.Count + 1).ToString();
                        }

                        collist.Add(colName);

                        ExcelCellFormat f = new ExcelCellFormat();
                        f.Format = h.NumFormat;
                        ///priyanka on date [24-Oct-16] end//
                        string colname = GetColumnAlphaIndex(h.ColInd);
                        //string colname = GetColumnAlphaIndex(i + 1);

                        BeforeCreateCellEventArgs CellArgs = new BeforeCreateCellEventArgs();
                        CellArgs.ColDataType = this.AllColumns[i].ColDataType;
                        CellArgs.ta = TableArea.Detail;

                        RaiseBeforeCreateCellEvent(CellArgs);

                        //Cell c = this.InsertCellInRow(sheetData1, row1, colname + (row1.RowIndex).ToString());

                        ExcelCellFormat cf = new ExcelCellFormat();

                        ExcelRange c = this._worksheet.Cells[colname + (this.CurrentRowCount).ToString()];

                        if (h.Width <= 0)
                        {
                            c.AutoFitColumns();
                        }

                        //cf.ul = DocumentFormat.OpenXml.Spreadsheet.UnderlineValues.None;

                        //ExcelRange c = this._worksheet.Cells[colname + (row1.RowIndex)].ToString();

                        SetCellValue(c, h.Caption);

                        cf.tableArea = TableArea.Header;
                        cf.HeaderStartRow = TableHeaderStartRow;
                        cf.ColumnName = h.Caption;
                        cf.Format = h.NumFormat;
                        cf.HorizontalAllign = h.HorizontalAllign;
                        cf.VerticalAllign = h.VerticalAllign;
                        string prevtext = cf.ColumnName;
                        cf.Text = prevtext;

                        RaiseAfterCreateCellEvent(ref cf);

                        ApplyStyle(this._worksheet, c, cf);

                        //if (cf.Formula != null && cf.Formula.Length > 0)
                        //{
                        //    CellFormula cfor = new CellFormula();
                        //    cfor.FormulaType = CellFormulaValues.Normal;
                        //    cfor.Text = cf.Formula;
                        //    c.Append(cfor);
                        //}

                        if (cf.Text != null && cf.Text != prevtext)
                        {
                            SetCellValue(c, cf.Text);
                        }

                        if (StartAddr == "")
                            StartAddr = c.Address;

                        EndArre = c.Address;
                        //
                    }
                }


                this._worksheet.Cells[StartAddr + ":" + EndArre].AutoFilter = true;

                e.StartrowCount = 0;

                TableDetailStartRow = (int)CurrentRowCount + 1;
                CreateDetail(e);

                TableFooterStartRow = (int)CurrentRowCount + 1;
                CreateFooter(e);

                // Change By Hitesh on [31-03-2016] as per [Doc No 201]
                // Add For AutoFit Column
                //this._worksheet.Cells[(TableDetailStartRow - 1), 1, (TableFooterStartRow), VisibleColumn].AutoFitColumns();
                // Add For Cell Border
                this._worksheet.Cells[(TableDetailStartRow - 1), 1, (int)CurrentRowCount, VisibleColumn].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                this._worksheet.Cells[(TableDetailStartRow - 1), 1, (int)CurrentRowCount, VisibleColumn].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                this._worksheet.Cells[(TableDetailStartRow - 1), 1, (int)CurrentRowCount, VisibleColumn].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                this._worksheet.Cells[(TableDetailStartRow - 1), 1, (int)CurrentRowCount, VisibleColumn].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                // End By Hitesh on [31-03-2016] as per [Doc No 201]
                ///priyanka on date [24-Oct-16]
                //this._worksheet.Workbook.Styles.UpdateXml();
                //_worksheet.Workbook.Calculate();
                ///priyanka on date [24-Oct-16] end//
                //_worksheet.Cells[this.TableHeaderStartRow,0].AutoFilter = true;

                //_worksheet.InsertRow(1, 1, 1);
                //_worksheet.Cells.AutoFitColumns(0);  //Autofit columns for all cells

                //RJCHECK
                //_worksheet.Calculate();
                //this._worksheet.Workbook.Calculate();
                ////package.Save();
                //ResAry = package.GetAsByteArray();
                package.Save();
            }

            byte[] byteDatan = System.IO.File.ReadAllBytes(xFileName);

            try
            {
                if (System.IO.File.Exists(xFileName))
                    System.IO.File.Delete(xFileName);
            }
            catch (Exception) { }

            ms.Write(byteDatan, 0, byteDatan.Length);
            ms.Flush();
            ms.Position = 0;

            //System.IO.MemoryStream RetMs = new System.IO.MemoryStream();

            //RJCHECK
            //byte[] byteDatan = ResAry;
            //ms.Write(byteDatan, 0, byteDatan.Length);
            //ms.Flush();
            //ms.Position = 0;
        }
        protected void SetCellValue(String Address, String val)
        {
            this.SetCellValue(this._worksheet.Cells[Address], val, -1);
        }

        public void SetCellValue(String Address, String val, UInt32 StyleInd)
        {
            this.SetCellValue(this._worksheet.Cells[Address], val, (Int32)StyleInd);
        }

        protected void SetCellValue(ExcelRange c, String val)
        {
            this.SetCellValue(c, val, -1);
        }

        protected void SetCellValue(ExcelRange c, String val, Int32 StyleInd)
        {
            //if (c.Start.Column == 1 || c.End.Column == 1)

            //this._worksheet.Column(1).ColumnMax = 1;

            double dVal;
            if (c.Style.Numberformat.Format != "General" && double.TryParse(val, out dVal))
            {
                c.Value = dVal;
            }
            else
            {
                c.Value = val;
            }

            if (StyleInd > 0)
            {
                c.StyleName = StyleInd.ToString();
            }

            //if (c.Start.Column == 1 || c.End.Column == 1)
            //    this._worksheet.Column(1).ColumnMax = 1;

        }

        //public void InsertCellInWorksheet(ExcelWorksheet worksheet, String CellAddress, int row, int col, String CellValue, String HyperLink, String Formula, ExcelStyle cStyle)
        //{
        //    worksheet.Cells[row, col].Value = CellValue;
        //    worksheet.Cells[row, col].Hyperlink = new Uri(HyperLink);
        //    worksheet.Cells[row, col].Formula = Formula;
        //}

        public void Save()
        {
            //document.WorkbookPart.Workbook.Save();
            //document.Close();
        }

        protected virtual void AddColumnDef(List<ExcelHeader> AllColumns)
        {
        }

        protected virtual void AddColumnDef(List<ExcelHeader> AllColumns, int iTransId)
        {
        }

        protected void RaiseAfterCreateCellEvent(ref ExcelCellFormat e)
        {
            if (AfterCreateCellEvent != null)
            {
                AfterCreateCellEvent(this, ref e);
            }
        }

        protected void RaiseBeforeCreateColumn(ref ExcelHeader e)
        {
            if (BeforeCreateColumnEvent != null)
            {
                BeforeCreateColumnEvent(this, ref e);
            }
        }

        protected void RaiseBeforeCreateColumn1(ref ExcelHeader e, List<ApiColSettings> columnsSettings)
        {
            if (BeforeCreateColumnEvent1 != null)
            {
                BeforeCreateColumnEvent1(this, ref e, columnsSettings);
            }
        }

        protected void RaiseBeforeCreateCellEvent(BeforeCreateCellEventArgs e)
        {
            if (BeforeCreateCellEvent != null)
            {
                BeforeCreateCellEvent(this, ref e);
            }
        }

        public string GetSummFormula(String ColName, TotalsRowFunctionValues ind, String CompareColName, String CompareCondition)
        {
            int funcind = 0;

            switch (ind)
            {
                case TotalsRowFunctionValues.Average:
                    funcind = (int)FormulaIndex.Average;
                    break;
                case TotalsRowFunctionValues.Count:
                    funcind = (int)FormulaIndex.CountA;
                    break;
                case TotalsRowFunctionValues.CountNumbers:
                    funcind = (int)FormulaIndex.Count;
                    break;
                case TotalsRowFunctionValues.Maximum:
                    funcind = (int)FormulaIndex.Max;
                    break;
                case TotalsRowFunctionValues.Minimum:
                    funcind = (int)FormulaIndex.Min;
                    break;
                case TotalsRowFunctionValues.StandardDeviation:
                    funcind = (int)FormulaIndex.StdDev;
                    break;
                case TotalsRowFunctionValues.Sum:
                    funcind = (int)FormulaIndex.Sum;
                    break;
                case TotalsRowFunctionValues.Variance:
                    funcind = (int)FormulaIndex.Var;
                    break;
                case TotalsRowFunctionValues.SumIf:
                    funcind = (int)FormulaIndex.SumIf;
                    break;
                default:
                    return "";
            }
            if (funcind == (int)FormulaIndex.SumIf)
                return "SUMIF(" + this.GetTableName() + "[" + CompareColName + @"], """ + CompareCondition + @"""," + this.GetTableName() + "[" + ColName + @"])";
            else
                return "SUBTOTAL(" + ((Int32)funcind).ToString() + "," + this.GetTableName() + "[" + ColName + "])";
            //return "SUBTOTAL(" + ((Int32)funcind).ToString() + "," + GetColumnAlphaIndex(GetColumnIndex(ColName)) + cFORMULA_START_INDEX + ":" + GetColumnAlphaIndex(GetColumnIndex(ColName)) + cFORMULA_END_INDEX + ")";
            //return "SUBTOTAL(" + ((Int32)funcind).ToString() + "," + "[" + ColName + "]" + cFORMULA_START_INDEX + ":" + "[" + ColName + "]" + cFORMULA_END_INDEX + ")";

        }


        public string GetSummFormula(String ColName, TotalsRowFunctionValues ind)
        {
            return GetSummFormula(ColName, ind, "", "");
        }

        public static void CreateOverseasExcel(DataTable dtDiamonds, string _strFolderPath, string _strFilePath, string LivePath)
        {
            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {
                    Color colFromHex_Pointer = System.Drawing.ColorTranslator.FromHtml("#c6e0b4");
                    Color colFromHex_Dis = System.Drawing.ColorTranslator.FromHtml("#ccffff");

                    #region Company Detail on Header

                    p.Workbook.Properties.Author = "SUNRISE DIAMOND";
                    p.Workbook.Properties.Title = "SUNRISE DIAMOND PVT. LTD.";

                    //Create a sheet
                    p.Workbook.Worksheets.Add("OverseasSearch");

                    ExcelWorksheet worksheet = p.Workbook.Worksheets[1];
                    worksheet.Name = DateTime.Now.ToString("dd-MM-yyyy");
                    worksheet.Cells.Style.Font.Size = 11;
                    worksheet.Cells.Style.Font.Name = "Calibri";
                    worksheet.Cells[1, 3, 3, 12].Style.Font.Bold = true;

                    worksheet.Cells[1, 6].Value = "SUNRISE DIAMONDS INVENTORY FOR THE DATE " + " " + DateTime.Now.ToString("dd-MM-yyyy");
                    worksheet.Cells[1, 6].Style.Font.Size = 24;
                    worksheet.Cells[1, 6].Style.Font.Bold = true;

                    Color colFromHex_H1 = System.Drawing.ColorTranslator.FromHtml("#8497b0");
                    worksheet.Cells[1, 6].Style.Font.Color.SetColor(colFromHex_H1);

                    worksheet.Row(5).Height = 40;
                    worksheet.Row(6).Height = 40;
                    worksheet.Row(6).Style.WrapText = true;

                    worksheet.Cells[2, 2].Value = "All Prices are final Selling Cash Price";
                    worksheet.Cells[2, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[2, 2].Style.Font.Size = 11;
                    worksheet.Cells[2, 2].Style.Font.Bold = true;
                    worksheet.Cells[2, 2].Style.Font.Color.SetColor(colFromHex_H1);

                    worksheet.Cells[2, 6].Value = "UNIT 1, 14/F, PENINSULA SQUARE, EAST WING, 18 SUNG ON STREET, HUNG HOM, KOWLOON, HONG KONG TEL : +852 - 27235100    FAX : +852 - 2314 9100";
                    worksheet.Cells[2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[2, 6].Style.Font.Size = 11;
                    worksheet.Cells[2, 6].Style.Font.Bold = true;
                    var cellBackgroundColor1_H2 = worksheet.Cells[1, 6].Style.Fill;
                    worksheet.Cells[2, 6].Style.Font.Color.SetColor(colFromHex_H1);

                    worksheet.Cells[3, 6].Value = "Email Id : sales@sunrisediam.com    Web : www.sunrisediamonds.com.hk . Download Apps on Android, IOS and Windows";
                    worksheet.Cells[3, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[3, 6].Style.Font.Size = 11;
                    worksheet.Cells[3, 6].Style.Font.Bold = true;
                    worksheet.Cells[3, 6].Style.Font.Color.SetColor(colFromHex_H1);

                    //worksheet.Cells[4, 2].Value = "Table & Crown Inclusion = White Inclusion";
                    //worksheet.Cells[4, 2, 4, 5].Merge = true;
                    //worksheet.Cells[4, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[4, 2].Style.Font.Size = 9;
                    //worksheet.Cells[4, 6].Value = "Table & Crown Natts = Black Inclusion";
                    //worksheet.Cells[4, 6, 4, 9].Merge = true;
                    //worksheet.Cells[4, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    //worksheet.Cells[4, 6].Style.Font.Size = 9;

                    worksheet.Cells[5, 1].Value = "Total";
                    worksheet.Cells[5, 1, 5, 40].Style.Font.Bold = true;
                    worksheet.Cells[5, 1, 5, 40].Style.Font.Size = 11;
                    worksheet.Cells[5, 1, 5, 40].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[5, 1, 5, 40].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    worksheet.Cells[5, 1, 5, 40].Style.Font.Size = 11;
                    worksheet.Cells[6, 1, 6, 40].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    worksheet.Cells[6, 1, 6, 40].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                    worksheet.Cells[6, 1, 6, 40].Style.Font.Size = 10;
                    worksheet.Cells[6, 1, 6, 40].Style.Font.Bold = true;
                    worksheet.Cells[6, 1, 6, 40].AutoFilter = true;

                    worksheet.Cells[1, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[2, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    worksheet.Cells[3, 6].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    var cellBackgroundColor1 = worksheet.Cells[6, 1, 6, 40].Style.Fill;
                    cellBackgroundColor1.PatternType = ExcelFillStyle.Solid;
                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                    cellBackgroundColor1.BackgroundColor.SetColor(colFromHex);

                    #endregion

                    #region Header Name Declaration

                    worksheet.Cells[6, 1].Value = "Sr. No";
                    //worksheet.Cells[6, 2].Value = "DNA";
                    worksheet.Cells[6, 2].Value = "View Image";
                    worksheet.Cells[6, 3].Value = "HD Movie";
                    worksheet.Cells[6, 4].Value = "Stock Id";
                    worksheet.Cells[6, 5].Value = "Location";
                    worksheet.Cells[6, 6].Value = "Status";
                    worksheet.Cells[6, 7].Value = "Shape";
                    worksheet.Cells[6, 8].Value = "Pointer";
                    worksheet.Cells[6, 9].Value = "Lab";
                    worksheet.Cells[6, 10].Value = "BGM";
                    worksheet.Cells[6, 11].Value = "Color";
                    worksheet.Cells[6, 12].Value = "Clarity";
                    worksheet.Cells[6, 13].Value = "Cts";
                    worksheet.Cells[6, 14].Value = "Rap Price($)";
                    worksheet.Cells[6, 15].Value = "Rap Amt($)";
                    worksheet.Cells[6, 16].Value = "Disc(%)";
                    worksheet.Cells[6, 17].Value = "Net Amt($)";
                    worksheet.Cells[6, 18].Value = "Price/Cts";
                    worksheet.Cells[6, 19].Value = "Cut";
                    worksheet.Cells[6, 20].Value = "Polish";
                    worksheet.Cells[6, 21].Value = "Symm";
                    worksheet.Cells[6, 22].Value = "Fls";
                    worksheet.Cells[6, 23].Value = "Length";
                    worksheet.Cells[6, 24].Value = "Width";
                    worksheet.Cells[6, 25].Value = "Depth";
                    worksheet.Cells[6, 26].Value = "Depth(%)";
                    worksheet.Cells[6, 27].Value = "Table(%)";
                    worksheet.Cells[6, 28].Value = "Key To Symbol";
                    worksheet.Cells[6, 29].Value = "Culet";
                    worksheet.Cells[6, 30].Value = "Table Black";
                    worksheet.Cells[6, 31].Value = "Crown Black";
                    worksheet.Cells[6, 32].Value = "Table White";
                    worksheet.Cells[6, 33].Value = "Crown White";
                    worksheet.Cells[6, 34].Value = "Cr Ang";
                    worksheet.Cells[6, 35].Value = "Cr Ht";
                    worksheet.Cells[6, 36].Value = "Pav Ang";
                    worksheet.Cells[6, 37].Value = "Pav Ht";
                    worksheet.Cells[6, 38].Value = "Girdle(%)";
                    worksheet.Cells[6, 39].Value = "Girdle Type";
                    worksheet.Cells[6, 40].Value = "Laser Insc";

                    ExcelStyle cellStyleHeader1 = worksheet.Cells[6, 1, 6, 40].Style;
                    cellStyleHeader1.Border.Left.Style = cellStyleHeader1.Border.Right.Style
                            = cellStyleHeader1.Border.Top.Style = cellStyleHeader1.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    #endregion

                    int inStartIndex = 7, i = 0;
                    int inwrkrow = 7;
                    int inEndCounter = dtDiamonds.Rows.Count + inStartIndex;
                    int TotalRow = dtDiamonds.Rows.Count;

                    #region Set AutoFit and Decimal Number Format

                    worksheet.View.FreezePanes(7, 1);

                    worksheet.Cells[6, 1].AutoFitColumns(5.43);
                    //worksheet.Cells[6, 2].AutoFitColumns(8.86);
                    worksheet.Cells[6, 2].AutoFitColumns(9);
                    worksheet.Cells[6, 3].AutoFitColumns(12);
                    worksheet.Cells[6, 4].AutoFitColumns(12);
                    worksheet.Cells[6, 5].AutoFitColumns(10.14);
                    worksheet.Cells[6, 6].AutoFitColumns(8.43);
                    worksheet.Cells[6, 7].AutoFitColumns(9.57);
                    worksheet.Cells[6, 8].AutoFitColumns(8.14);
                    worksheet.Cells[6, 9].AutoFitColumns(8.14);
                    worksheet.Cells[6, 10].AutoFitColumns(8.43);
                    worksheet.Cells[6, 11].AutoFitColumns(9.29);
                    worksheet.Cells[6, 12].AutoFitColumns(13);
                    worksheet.Cells[6, 13].AutoFitColumns(12);
                    worksheet.Cells[6, 14].AutoFitColumns(8.14);
                    worksheet.Cells[6, 15].AutoFitColumns(15);
                    worksheet.Cells[6, 16].AutoFitColumns(8.14);
                    worksheet.Cells[6, 17].AutoFitColumns(15);
                    worksheet.Cells[6, 18].AutoFitColumns(8.14);
                    worksheet.Cells[6, 19].AutoFitColumns(7.86);
                    worksheet.Cells[6, 20].AutoFitColumns(7.86);
                    worksheet.Cells[6, 21].AutoFitColumns(7.86);
                    worksheet.Cells[6, 22].AutoFitColumns(7.86);
                    worksheet.Cells[6, 23].AutoFitColumns(7.86);
                    worksheet.Cells[6, 24].AutoFitColumns(7.86);
                    worksheet.Cells[6, 25].AutoFitColumns(7.86);
                    worksheet.Cells[6, 26].AutoFitColumns(9);
                    worksheet.Cells[6, 27].AutoFitColumns(7.9);
                    worksheet.Cells[6, 28].AutoFitColumns(36);
                    worksheet.Cells[6, 29].AutoFitColumns(7.86);
                    worksheet.Cells[6, 30].AutoFitColumns(7.86);
                    worksheet.Cells[6, 32].AutoFitColumns(7.86);
                    worksheet.Cells[6, 33].AutoFitColumns(7.86);
                    worksheet.Cells[6, 34].AutoFitColumns(7.86);
                    worksheet.Cells[6, 35].AutoFitColumns(7.86);
                    worksheet.Cells[6, 36].AutoFitColumns(7.86);
                    worksheet.Cells[6, 37].AutoFitColumns(7.86);
                    worksheet.Cells[6, 38].AutoFitColumns(7.86);
                    worksheet.Cells[6, 39].AutoFitColumns(7.86);
                    worksheet.Cells[6, 40].AutoFitColumns(7.86);

                    //Set Cell Faoat value with Alignment
                    worksheet.Cells[inStartIndex, 1, inEndCounter, 40].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    #endregion

                    string values_1, Image, Video, hyprlink1, Certificate, cut;
                    Int64 number_1;
                    bool success1;
                    var asTitleCase = Thread.CurrentThread.CurrentCulture.TextInfo;
                    Color colFromHexTotal = System.Drawing.ColorTranslator.FromHtml("#d9e1f2");

                    for (i = inStartIndex; i < inEndCounter; i++)
                    {
                        #region Assigns Value to Cell

                        worksheet.Cells[inwrkrow, 1].Value = Convert.ToInt32(dtDiamonds.Rows[i - inStartIndex]["sr"]);

                        //S_Detail = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["view_dna"] == null ? "" : dtDiamonds.Rows[i - inStartIndex]["view_dna"]);
                        //if (S_Detail != "")
                        //{
                        //    worksheet.Cells[inwrkrow, 2].Formula = "=HYPERLINK(\"" + S_Detail + "\",\" DNA \")";
                        //    worksheet.Cells[inwrkrow, 2].Style.Font.UnderLine = true;
                        //    worksheet.Cells[inwrkrow, 2].Style.Font.Color.SetColor(Color.Blue);
                        //}

                        Image = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["image_url"] == null ? "" : dtDiamonds.Rows[i - inStartIndex]["image_url"]);
                        if (Image != "")
                        {
                            hyprlink1 = External_ImageURL + Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["certi_no"]) + "/PR.jpg";
                            worksheet.Cells[inwrkrow, 2].Formula = "=HYPERLINK(\"" + Image + "\",\" Image \")";
                            worksheet.Cells[inwrkrow, 2].Style.Font.UnderLine = true;
                            worksheet.Cells[inwrkrow, 2].Style.Font.Color.SetColor(Color.Blue);
                        }

                        Video = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["movie_url"] == null ? "" : dtDiamonds.Rows[i - inStartIndex]["movie_url"]);
                        if (Video != "")
                        {
                            worksheet.Cells[inwrkrow, 3].Formula = "=HYPERLINK(\"" + Video + "\",\" Video \")";
                            worksheet.Cells[inwrkrow, 3].Style.Font.UnderLine = true;
                            worksheet.Cells[inwrkrow, 3].Style.Font.Color.SetColor(Color.Blue);
                        }

                        values_1 = dtDiamonds.Rows[i - inStartIndex]["stone_ref_no"].ToString();
                        success1 = Int64.TryParse(values_1, out number_1);
                        if (success1)
                        {
                            worksheet.Cells[inwrkrow, 4].Value = Convert.ToInt64(dtDiamonds.Rows[i - inStartIndex]["stone_ref_no"]);
                        }
                        else
                        {
                            worksheet.Cells[inwrkrow, 4].Value = values_1;
                        }
                        worksheet.Cells[inwrkrow, 5].Value = asTitleCase.ToTitleCase(Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Location"]).ToLower());
                        worksheet.Cells[inwrkrow, 6].Value = asTitleCase.ToTitleCase(Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["status"]).ToLower());
                        worksheet.Cells[inwrkrow, 7].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["shape"]);
                        worksheet.Cells[inwrkrow, 8].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["pointer"]);
                        worksheet.Cells[inwrkrow, 9].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["lab"]);

                        Certificate = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["view_certi_url"] == null ? "" : dtDiamonds.Rows[i - inStartIndex]["view_certi_url"]);
                        if (Certificate != "")
                        {
                            worksheet.Cells[inwrkrow, 9].Formula = "=HYPERLINK(\"" + Certificate + "\",\"" + dtDiamonds.Rows[i - inStartIndex]["lab"] + " \")";
                            worksheet.Cells[inwrkrow, 9].Style.Font.UnderLine = true;
                            worksheet.Cells[inwrkrow, 9].Style.Font.Color.SetColor(Color.Blue);
                        }

                        worksheet.Cells[inwrkrow, 10].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["BGM"]);
                        worksheet.Cells[inwrkrow, 11].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["color"]);
                        worksheet.Cells[inwrkrow, 12].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["clarity"]);
                        worksheet.Cells[inwrkrow, 13].Value = ((dtDiamonds.Rows[i - inStartIndex]["cts"] != null) ?
                                (dtDiamonds.Rows[i - inStartIndex]["cts"].GetType().Name != "DBNull" ?
                                Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["cts"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 14].Value = ((dtDiamonds.Rows[i - inStartIndex]["cur_rap_rate"] != null) ?
                                (dtDiamonds.Rows[i - inStartIndex]["cur_rap_rate"].GetType().Name != "DBNull" ?
                                Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["cur_rap_rate"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 15].Value = ((dtDiamonds.Rows[i - inStartIndex]["rap_amount"] != null) ?
                                (dtDiamonds.Rows[i - inStartIndex]["rap_amount"].GetType().Name != "DBNull" ?
                                Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["rap_amount"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 16].Value = ((dtDiamonds.Rows[i - inStartIndex]["sales_disc_per"] != null) ?
                                (dtDiamonds.Rows[i - inStartIndex]["sales_disc_per"].GetType().Name != "DBNull" ?
                                Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["sales_disc_per"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 17].Value = ((dtDiamonds.Rows[i - inStartIndex]["net_amount"] != null) ?
                                (dtDiamonds.Rows[i - inStartIndex]["net_amount"].GetType().Name != "DBNull" ?
                                Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["net_amount"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 18].Value = ((dtDiamonds.Rows[i - inStartIndex]["price_per_cts"] != null) ?
                                (dtDiamonds.Rows[i - inStartIndex]["price_per_cts"].GetType().Name != "DBNull" ?
                                Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["price_per_cts"]) : ((Double?)null)) : null);

                        cut = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["cut"]);
                        worksheet.Cells[inwrkrow, 19].Value = (cut == "FR" ? "F" : cut);
                        worksheet.Cells[inwrkrow, 20].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["polish"]);
                        worksheet.Cells[inwrkrow, 21].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["symm"]);

                        if (Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["cut"]) == "3EX")
                        {
                            worksheet.Cells[inwrkrow, 19].Style.Font.Bold = true;
                            worksheet.Cells[inwrkrow, 20].Style.Font.Bold = true;
                            worksheet.Cells[inwrkrow, 21].Style.Font.Bold = true;
                        }

                        worksheet.Cells[inwrkrow, 22].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["fls"]);

                        worksheet.Cells[inwrkrow, 23].Value = ((dtDiamonds.Rows[i - inStartIndex]["length"] != null) ?
                                (dtDiamonds.Rows[i - inStartIndex]["length"].GetType().Name != "DBNull" ?
                                Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["length"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 24].Value = ((dtDiamonds.Rows[i - inStartIndex]["width"] != null) ?
                                (dtDiamonds.Rows[i - inStartIndex]["width"].GetType().Name != "DBNull" ?
                                Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["width"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 25].Value = ((dtDiamonds.Rows[i - inStartIndex]["depth"] != null) ?
                                (dtDiamonds.Rows[i - inStartIndex]["depth"].GetType().Name != "DBNull" ?
                                Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["depth"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 26].Value = ((dtDiamonds.Rows[i - inStartIndex]["depth_per"] != null) ?
                                (dtDiamonds.Rows[i - inStartIndex]["depth_per"].GetType().Name != "DBNull" ?
                                Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["depth_per"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 27].Value = ((dtDiamonds.Rows[i - inStartIndex]["table_per"] != null) ?
                                (dtDiamonds.Rows[i - inStartIndex]["table_per"].GetType().Name != "DBNull" ?
                                Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["table_per"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 28].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["symbol"]);
                        worksheet.Cells[inwrkrow, 29].Value = asTitleCase.ToTitleCase(Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["sculet"]).ToLower());
                        worksheet.Cells[inwrkrow, 30].Value = asTitleCase.ToTitleCase(Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["table_natts"] == null ? "" : dtDiamonds.Rows[i - inStartIndex]["table_natts"]).ToLower());
                        worksheet.Cells[inwrkrow, 31].Value = asTitleCase.ToTitleCase(Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"] == null ? "" : dtDiamonds.Rows[i - inStartIndex]["Crown_Natts"]).ToLower());
                        worksheet.Cells[inwrkrow, 32].Value = asTitleCase.ToTitleCase(Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["inclusion"]).ToLower());
                        worksheet.Cells[inwrkrow, 33].Value = asTitleCase.ToTitleCase(Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["Crown_Inclusion"] == null ? "" : dtDiamonds.Rows[i - inStartIndex]["Crown_Inclusion"]).ToLower());
                        worksheet.Cells[inwrkrow, 34].Value = dtDiamonds.Rows[i - inStartIndex]["crown_angle"] == null ? 0 : dtDiamonds.Rows[i - inStartIndex]["crown_angle"].ToString() == "" ? 0 : dtDiamonds.Rows[i - inStartIndex]["crown_angle"];

                        worksheet.Cells[inwrkrow, 35].Value = dtDiamonds.Rows[i - inStartIndex]["crown_height"] == null ? 0 : dtDiamonds.Rows[i - inStartIndex]["crown_height"].ToString() == "" ? 0 : dtDiamonds.Rows[i - inStartIndex]["crown_height"];

                        worksheet.Cells[inwrkrow, 36].Value = dtDiamonds.Rows[i - inStartIndex]["pav_angle"] == null ? 0 : dtDiamonds.Rows[i - inStartIndex]["pav_angle"].ToString() == "" ? 0 : dtDiamonds.Rows[i - inStartIndex]["pav_angle"];

                        worksheet.Cells[inwrkrow, 37].Value = dtDiamonds.Rows[i - inStartIndex]["pav_height"] == null ? 0 : dtDiamonds.Rows[i - inStartIndex]["pav_height"].ToString() == "" ? 0 : dtDiamonds.Rows[i - inStartIndex]["pav_height"];

                        worksheet.Cells[inwrkrow, 38].Value = ((dtDiamonds.Rows[i - inStartIndex]["girdle_per"] != null) ?
                                (dtDiamonds.Rows[i - inStartIndex]["girdle_per"].GetType().Name != "DBNull" ?
                                Convert.ToDouble(dtDiamonds.Rows[i - inStartIndex]["girdle_per"]) : ((Double?)null)) : null);

                        worksheet.Cells[inwrkrow, 39].Value = asTitleCase.ToTitleCase(Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["girdle_type"] == null ? "" : dtDiamonds.Rows[i - inStartIndex]["girdle_type"]).ToLower());

                        worksheet.Cells[inwrkrow, 40].Value = Convert.ToString(dtDiamonds.Rows[i - inStartIndex]["sInscription"] == null ? "" : dtDiamonds.Rows[i - inStartIndex]["sInscription"]);

                        inwrkrow++;

                        #endregion
                    }

                    worksheet.Cells[inStartIndex, 1, (inwrkrow - 1), 40].Style.Font.Size = 9;

                    worksheet.Cells[6, 8, (inwrkrow - 1), 8].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[6, 8, (inwrkrow - 1), 8].Style.Fill.BackgroundColor.SetColor(colFromHex_Pointer);

                    worksheet.Cells[inStartIndex, 13, (inwrkrow - 1), 18].Style.Numberformat.Format = "#,##0.00";

                    worksheet.Cells[inStartIndex, 16, (inwrkrow - 1), 17].Style.Font.Bold = true;
                    worksheet.Cells[inStartIndex, 16, (inwrkrow - 1), 17].Style.Font.Color.SetColor(System.Drawing.Color.Red);
                    worksheet.Cells[6, 16, (inwrkrow - 1), 17].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[6, 16, (inwrkrow - 1), 17].Style.Fill.BackgroundColor.SetColor(colFromHex_Dis);

                    worksheet.Cells[inStartIndex, 23, (inwrkrow - 1), 27].Style.Numberformat.Format = "0.00";
                    worksheet.Cells[inStartIndex, 34, (inwrkrow - 1), 38].Style.Numberformat.Format = "0.00";

                    worksheet.Cells[5, 4].Formula = "ROUND(SUBTOTAL(102,A" + inStartIndex + ":A" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[5, 4].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[5, 4].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[5, 4].Style.Numberformat.Format = "#,##";
                    ExcelStyle cellStyleHeader_Total = worksheet.Cells[5, 4].Style;
                    cellStyleHeader_Total.Border.Left.Style = cellStyleHeader_Total.Border.Right.Style
                            = cellStyleHeader_Total.Border.Top.Style = cellStyleHeader_Total.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[5, 13].Formula = "ROUND(SUBTOTAL(109,M" + inStartIndex + ":M" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[5, 13].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[5, 13].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[5, 13].Style.Numberformat.Format = "#,##0.00";
                    ExcelStyle cellStyleHeader_Totalcarat = worksheet.Cells[5, 13].Style;
                    cellStyleHeader_Totalcarat.Border.Left.Style = cellStyleHeader_Totalcarat.Border.Right.Style
                            = cellStyleHeader_Totalcarat.Border.Top.Style = cellStyleHeader_Totalcarat.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[5, 15].Formula = "ROUND(SUBTOTAL(109,O" + inStartIndex + ":O" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[5, 15].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[5, 15].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[5, 15].Style.Numberformat.Format = "#,##0";
                    ExcelStyle cellStyleHeader_TotalAmt = worksheet.Cells[5, 15].Style;
                    cellStyleHeader_TotalAmt.Border.Left.Style = cellStyleHeader_TotalAmt.Border.Right.Style
                            = cellStyleHeader_TotalAmt.Border.Top.Style = cellStyleHeader_TotalAmt.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[5, 16].Formula = "ROUND((1-(SUBTOTAL(109,Q" + inStartIndex + ":Q" + (inwrkrow - 1) + ")/SUBTOTAL(109,O" + inStartIndex + ":O" + (inwrkrow - 1) + ")))*(-100),2)";
                    worksheet.Cells[5, 16].Style.Numberformat.Format = "#,##0.00";
                    ExcelStyle cellStyleHeader_TotalDis = worksheet.Cells[5, 16].Style;
                    cellStyleHeader_TotalDis.Border.Left.Style = cellStyleHeader_TotalDis.Border.Right.Style
                            = cellStyleHeader_TotalDis.Border.Top.Style = cellStyleHeader_TotalDis.Border.Bottom.Style
                            = ExcelBorderStyle.Medium;

                    worksheet.Cells[5, 17].Formula = "ROUND(SUBTOTAL(109,Q" + inStartIndex + ":Q" + (inwrkrow - 1) + "),2)";
                    worksheet.Cells[5, 17].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    worksheet.Cells[5, 17].Style.Fill.BackgroundColor.SetColor(colFromHexTotal);
                    worksheet.Cells[5, 17].Style.Numberformat.Format = "#,##0";
                    ExcelStyle cellStyleHeader_TotalNet = worksheet.Cells[5, 17].Style;
                    cellStyleHeader_TotalNet.Border.Left.Style = cellStyleHeader_TotalNet.Border.Right.Style
                            = cellStyleHeader_TotalNet.Border.Top.Style = cellStyleHeader_TotalNet.Border.Bottom.Style
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
                throw ex;
            }
        }

        public class FillingWorksheetEventArgs : EventArgs
        {

            public FillingWorksheetEventArgs(ExcelWorksheet ws, ExcelStyle Style)
            {

                this.Worksheet = ws;
                this.Style = Style;

            }


            private ExcelStyle _Style;

            public ExcelStyle Style
            {
                get { return _Style; }
                set { _Style = value; }
            }



            private ExcelWorksheet ws;

            public ExcelWorksheet Worksheet
            {
                get { return ws; }
                set { ws = value; }
            }

            //private SheetData wsd;

            //public SheetData Data
            //{
            //    get { return wsd; }
            //    set { wsd = value; }
            //}

            private int _FooterRowCount;

            public int FooterRowCount
            {
                get { return _FooterRowCount; }
                set { _FooterRowCount = value; }
            }

            private int _StartrowCount;

            public int StartrowCount
            {
                get { return _StartrowCount; }
                set { _StartrowCount = value; }
            }



        }

        public class BeforeCreateCellEventArgs : EventArgs
        {
            public OfficeOpenXml.eDataTypes ColDataType;
            public CellType celltype;
            public TableArea ta;
        }

        public class AddHeaderEventArgs : EventArgs
        {

            public AddHeaderEventArgs(ExcelWorksheet ws, UInt32 HeaderRowcount)
            {

                this._HeaderRowcount = HeaderRowcount;

            }

            private ExcelWorksheet _Worksheet;

            public ExcelWorksheet Worksheet
            {
                get { return _Worksheet; }
                set { _Worksheet = value; }
            }

            private UInt32 _HeaderRowcount;

            public UInt32 HeaderRowcount
            {
                get { return _HeaderRowcount; }
                set { _HeaderRowcount = value; }
            }


        }

        public class AddFooterEventArgs : EventArgs
        {

            public AddFooterEventArgs(ExcelWorksheet ws, UInt32 DetailStartRowcount, UInt32 DetailEndRowcount)
            {
                //this._Worksheet = ws;
                //this._DetailStartCount = DetailStartRowcount;
                //this._DetailEndCount = DetailEndRowcount;
            }

            private ExcelWorksheet _Worksheet;

            public ExcelWorksheet Worksheet
            {
                get { return _Worksheet; }
                set { _Worksheet = value; }
            }

            private int _DetailStartCount;

            public int DetailStartCount
            {
                get { return _DetailStartCount; }
                set { _DetailStartCount = value; }
            }


            private int _DetailEndCount;

            public int DetailEndCount
            {
                get { return _DetailEndCount; }
                set { _DetailEndCount = value; }
            }
        }

        public struct ExcelHeader
        {
            public String ColName;
            public String HyperlinkColName;
            public String Caption;
            public eDataTypes ColDataType;
            public TotalsRowFunctionValues SummFunction;
            public String SummFormula;
            public String SummText;
            public Int32 ColInd;
            public bool visible;
            public string NumFormat;
            public OfficeOpenXml.Style.ExcelVerticalAlignment VerticalAllign;
            public OfficeOpenXml.Style.ExcelHorizontalAlignment HorizontalAllign;
            public Double Width;
        }

        public class ExcelFormat : ICloneable
        {
            public String ColumnName;
            public Double fontsize;
            public String forColorArgb;
            public String fontname;
            public Int32 FontFamilyindex;
            public Boolean isbold;
            public Boolean isitalic = false;
            public String backgroundArgb;
            public TableArea tableArea;
            public UInt32? StyleInd;
            public string Format;
            public string url;
            public object GridRow;
            public int RowIndex; //add by kaushal date[07-04-2018]
            public OfficeOpenXml.Style.ExcelUnderLineType ul;
            public OfficeOpenXml.Style.ExcelVerticalAlignment VerticalAllign;
            public OfficeOpenXml.Style.ExcelHorizontalAlignment HorizontalAllign;


            public bool IsEqual(ExcelFormat cf)
            {

                if (this.fontsize != cf.fontsize)
                {
                    return false;
                }

                if (this.forColorArgb != cf.forColorArgb)
                {
                    return false;
                }

                if (this.backgroundArgb != cf.backgroundArgb)
                {
                    return false;
                }

                if (this.fontname != cf.fontname)
                {
                    return false;
                }
                if (this.FontFamilyindex != cf.FontFamilyindex)
                {
                    return false;
                }

                if (this.isbold != cf.isbold)
                {
                    return false;
                }
                if (this.isitalic != cf.isitalic)
                {
                    return false;
                }

                if (this.Format != cf.Format)
                {
                    return false;
                }

                if (this.VerticalAllign != cf.VerticalAllign)
                {
                    return false;
                }

                if (this.HorizontalAllign != cf.HorizontalAllign)
                {
                    return false;
                }


                return true;

            }

            public object Clone()
            {
                ExcelFormat newclone = new ExcelFormat();

                newclone.ColumnName = this.ColumnName;
                newclone.fontsize = this.fontsize;
                newclone.forColorArgb = this.forColorArgb;
                newclone.fontname = this.fontname;
                newclone.FontFamilyindex = this.FontFamilyindex;
                newclone.isbold = this.isbold;
                newclone.isitalic = this.isitalic;
                newclone.backgroundArgb = this.backgroundArgb;
                newclone.tableArea = this.tableArea;
                newclone.StyleInd = this.StyleInd;
                newclone.Format = this.Format;
                newclone.url = this.url;
                newclone.GridRow = this.GridRow;
                newclone.VerticalAllign = this.VerticalAllign;
                newclone.HorizontalAllign = this.HorizontalAllign;

                return newclone;

            }
        }

        public class ExcelCellFormat : ExcelFormat
        {

            public bool OverrideDefault = false;
            public String Formula;
            public String Text;
            public int ColInd;
            public int RowInd;
            public int? HeaderStartRow;
            public int? DetailStartRow;
            public int? FooterStartRow;

            public bool IsEqual(ExcelCellFormat cf)
            {

                if (this.fontsize != cf.fontsize)
                {
                    return false;
                }

                if (this.forColorArgb != cf.forColorArgb)
                {
                    return false;
                }
                if (this.fontname != cf.fontname)
                {
                    return false;
                }
                if (this.FontFamilyindex != cf.FontFamilyindex)
                {
                    return false;
                }

                if (this.isbold != cf.isbold)
                {
                    return false;
                }
                if (this.isitalic != cf.isitalic)
                {
                    return false;
                }
                if (this.Formula != cf.Formula)
                {
                    return false;
                }
                if (this.Format != cf.Format)
                {
                    return false;
                }

                if (this.VerticalAllign != cf.VerticalAllign)
                {
                    return false;
                }

                if (this.HorizontalAllign != cf.HorizontalAllign)
                {
                    return false;
                }


                return true;

            }
        }

        public enum CellType
        {
            Normal = 1,
            Formula = 2
        }

        public enum TotalsRowFunctionValues
        {
            //[EnumString("none")]
            None = 0,
            //[EnumString("sum")]
            Sum = 1,
            //[EnumString("min")]
            Minimum = 2,
            //[EnumString("max")]
            Maximum = 3,
            //[EnumString("average")]
            Average = 4,
            //[EnumString("count")]
            Count = 5,
            //[EnumString("countNums")]
            CountNumbers = 6,
            //[EnumString("stdDev")]
            StandardDeviation = 7,
            //[EnumString("var")]
            Variance = 8,
            //[EnumString("custom")]
            Custom = 9,
            SumIf = 10,
        }

        public enum FormulaIndex
        {

            Average = 101,
            Count = 102, CountA = 103, Max = 104, Min = 105, Product = 106, StdDev = 107, StdEvp = 108,
            Sum = 109, Var = 110, VarP = 111,
            AverageAll = 1,
            CountAll = 2, CountAAll = 3, MaxAll = 4, MinAll = 5, ProductAll = 6, StdDevAll = 7, StdDevPAll = 8,
            SumAll = 9, VarAll = 10, VarPAll = 11, SumIf = 12
        }

        public struct KnownHexValue
        {
            //public const String Black = "FF000000";
            //public const String White = "FFFFFFFF";
            //public const String Blue = "FF0000FF";

            public const String Black = "-16777216";
            public const String White = "-1";
            public const String Blue = "-16776961";
        }

        public enum TableArea
        {
            Header = 1, Detail = 2, Footer = 3
        }
    }
}

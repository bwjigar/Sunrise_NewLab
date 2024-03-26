using SunriseLabWeb.Helper;
using System;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace SunriseLabWeb_New.Filter
{
    public class AuthorizeActionFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpSessionStateBase session = filterContext.HttpContext.Session;
            Controller controller = filterContext.Controller as Controller;

            if (controller != null)
            {
                if (session != null && SessionFacade.UserSession == null)
                {
                    Uri url = new Uri(System.Web.HttpContext.Current.Request.Url.AbsoluteUri);
                    string action = String.Format("{3}", url.Scheme, Uri.SchemeDelimiter, url.Authority, url.AbsolutePath);

                    if (action != "/User/URL")
                    {
                        filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                }
                else if (SessionFacade.UserSession != null)
                {
                    string UserTypeId = SessionFacade.UserSession.UserTypeId;
                    bool View = SessionFacade.UserSession.View;
                    bool Download = SessionFacade.UserSession.Download;

                    bool IsPrimaryUser = SessionFacade.UserSession.IsPrimaryUser;
                    bool IsSubUser = SessionFacade.UserSession.IsSubUser;
                    bool SearchStock = SessionFacade.UserSession.SearchStock;
                    bool StockDownload = SessionFacade.UserSession.StockDownload;
                    bool MyCart = SessionFacade.UserSession.MyCart;
                    bool OrderHistoryAll = SessionFacade.UserSession.OrderHistoryAll;
                    bool OrderHistoryByHisUser = SessionFacade.UserSession.OrderHistoryByHisUser;

                    Int32 UserId = Convert.ToInt32(SessionFacade.UserSession.UserId);
                    string cntlr = Convert.ToString(filterContext.RouteData.Values["controller"]);
                    string act = Convert.ToString(filterContext.RouteData.Values["action"]);

                    if (IsPrimaryUser == true)
                    {
                        if ((cntlr == "User" && act == "SupplierMas") || (cntlr == "User" && act == "Category")
                             || (cntlr == "User" && act == "SupplierValue") || (cntlr == "User" && act == "SupplierColumnSetting")
                             || (cntlr == "User" && act == "SupplierColumnSettingFromFile") || (cntlr == "User" && act == "SupplierPriceList")
                             || (cntlr == "User" && act == "LabEntry") || (cntlr == "User" && act == "LabEntryReport"))
                        {
                            if (!(UserTypeId.Contains("1")))
                                GoTo_LoginPage(filterContext);
                        }
                        else if (cntlr == "User" && act == "SearchStock")
                        {
                            if (View == false && Download == false)
                                GoTo_LoginPage(filterContext);
                        }
                        else if ((cntlr == "User" && act == "Manage"))
                        {
                            if (!(UserTypeId.Contains("1") || UserId == 8))
                                GoTo_LoginPage(filterContext);
                        }
                        else if ((cntlr == "User" && act == "LabAvailability") || (cntlr == "User" && act == "StockDiscMgt") || (cntlr == "User" && act == "LoginDetail") || (cntlr == "User" && act == "UserActivity"))
                        {
                            if (!(UserTypeId.Contains("1") || UserTypeId.Contains("2")))
                                GoTo_LoginPage(filterContext);
                        }
                        else if ((cntlr == "User" && act == "MyCart") || (cntlr == "User" && act == "OrderHistory"))
                        {
                            if (!(UserTypeId.Contains("1") || UserTypeId.Contains("2") || UserTypeId.Contains("3")))
                                GoTo_LoginPage(filterContext);
                        }
                    }
                    else if (IsSubUser == true)
                    {
                        if ((cntlr == "User" && act == "Manage") || (cntlr == "User" && act == "Category")
                             || (cntlr == "User" && act == "SupplierValue") || (cntlr == "User" && act == "SupplierMas")
                             || (cntlr == "User" && act == "SupplierColumnSetting") || (cntlr == "User" && act == "SupplierColumnSettingFromFile")
                             || (cntlr == "User" && act == "SupplierStockUpload") || (cntlr == "User" && act == "SupplierPriceList")
                             || (cntlr == "User" && act == "CustomerPriceList") || (cntlr == "User" && act == "StockDiscMgt")
                             || (cntlr == "User" && act == "ColumnSetting") || (cntlr == "User" && act == "LabEntry")
                             || (cntlr == "User" && act == "LabAvailability") || (cntlr == "User" && act == "LabEntryReport")
                             || (cntlr == "User" && act == "LoginDetail") || (cntlr == "User" && act == "UserActivity"))
                        {
                            GoTo_LoginPage(filterContext);
                        }
                        else if (cntlr == "User" && act == "SearchStock")
                        {
                            if (SearchStock == false && StockDownload == false)
                                GoTo_LoginPage(filterContext);
                        }
                        else if (cntlr == "User" && act == "MyCart")
                        {
                            if (MyCart == false)
                                GoTo_LoginPage(filterContext);
                        }
                        else if (cntlr == "User" && act == "OrderHistory")
                        {
                            if (OrderHistoryAll == false && OrderHistoryByHisUser == false)
                                GoTo_LoginPage(filterContext);
                        }
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
        public void GoTo_LoginPage(ActionExecutingContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Login/Index");
            SessionFacade.Abandon();
        }
    }
}
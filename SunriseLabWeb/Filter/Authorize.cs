using SunriseLabWeb.Helper;
using System;
using System.Web;
using System.Web.Mvc;

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

                    //if (action != "/Lab/Download" && action != "/Lab/DownloadAction" && action != "/Lab/Index" && action != "/Lab/LabAPI_LabDataUpload_Ora" && action != "/Lab" && action != "/Lab/LabStockDataDelete")
                    //{
                    filterContext.Result = new RedirectResult("~/Login/Index");
                    //}
                }
                else if (SessionFacade.UserSession != null)
                {
                    string UserTypeId = SessionFacade.UserSession.UserTypeId;
                    string cntlr = filterContext.RouteData.Values["controller"].ToString();
                    string act = filterContext.RouteData.Values["action"].ToString();
                    
                    if ((cntlr == "User" && act == "Manage") || (cntlr == "User" && act == "SupplierMas") || (cntlr == "User" && act == "Category")
                         || (cntlr == "User" && act == "SupplierValue") || (cntlr == "User" && act == "SupplierColumnSetting") 
                         || (cntlr == "User" && act == "SupplierColumnSettingFromFile") || (cntlr == "User" && act == "SupplierPriceList"))
                    {
                        if (!(UserTypeId.Contains("1") || UserTypeId.Contains("4")))
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
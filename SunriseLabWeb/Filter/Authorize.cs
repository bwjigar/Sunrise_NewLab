﻿using SunriseLabWeb.Helper;
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

                    if (action != "/User/URL")
                    {
                        filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                }
                else if (SessionFacade.UserSession != null)
                {
                    string UserTypeId = SessionFacade.UserSession.UserTypeId;
                    string cntlr = filterContext.RouteData.Values["controller"].ToString();
                    string act = filterContext.RouteData.Values["action"].ToString();
                    
                    if ((cntlr == "User" && act == "Manage") || (cntlr == "User" && act == "SupplierMas") || (cntlr == "User" && act == "Category")
                         || (cntlr == "User" && act == "SupplierValue") || (cntlr == "User" && act == "SupplierColumnSetting") 
                         || (cntlr == "User" && act == "SupplierColumnSettingFromFile") || (cntlr == "User" && act == "SupplierPriceList")
                         || (cntlr == "User" && act == "LabEntry"))
                    {
                        if (!(UserTypeId.Contains("1")))
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                    else if ((cntlr == "User" && act == "LabAvailibility"))
                    {
                        if (!(UserTypeId.Contains("1") || UserTypeId.Contains("2")))
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }
                    else if ((cntlr == "User" && act == "MyCart") || (cntlr == "User" && act == "OrderHistory"))
                    {
                        if (!(UserTypeId.Contains("1") || UserTypeId.Contains("2") || UserTypeId.Contains("3")))
                            filterContext.Result = new RedirectResult("~/Login/Index");
                    }

                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}
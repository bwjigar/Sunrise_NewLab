using SunriseLabWeb_New.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SunriseLabWeb_New.Controllers
{
    [AuthorizeActionFilterAttribute]
    public class DashBoardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
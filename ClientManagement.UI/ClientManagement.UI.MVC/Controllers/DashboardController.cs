using ClientManagement.UI.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManagement.UI.MVC.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Result(int statusCode, string message)
        {
            DashboardViewModel model = new DashboardViewModel();
            model.statusCode = statusCode;
            model.message = message;

            return View(model);
        }


    }
}
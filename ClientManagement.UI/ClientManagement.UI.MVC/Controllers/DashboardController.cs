using ClientManagement.UI.MVC.Models;
using ClientManagement.UI.ServiceAccess;
using Microsoft.AspNet.Identity;
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
            DashboardViewModel model = new DashboardViewModel();

            string UserId = User.Identity.GetUserId();
            LogicService cmLogic = new LogicService();

            //Get jobs for user for dashboard view model
            model.ClientsForUser.ClientsToDisplay = cmLogic.GetClientsForUser(UserId);
            model.JobsForUser.JobsToDisplay = cmLogic.GetJobsForUser(UserId);

            return View(model);
        }

        public ActionResult Result(int statusCode, string message)
        {
            DashboardViewModel model = new DashboardViewModel();

            string UserId = User.Identity.GetUserId();
            LogicService cmLogic = new LogicService();

            //Get jobs for user for dashboard view model
            model.ClientsForUser.ClientsToDisplay = cmLogic.GetClientsForUser(UserId);
            model.JobsForUser.JobsToDisplay = cmLogic.GetJobsForUser(UserId);
            model.statusCode = statusCode;
            model.message = message;

            return View(model);
        }

    }
}
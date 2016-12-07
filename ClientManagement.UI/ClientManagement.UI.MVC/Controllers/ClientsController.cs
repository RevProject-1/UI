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
    public class ClientsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddClient()
        {
            var model = new ClientViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddClient(ClientViewModel model)
        {

            string UserId = User.Identity.GetUserId();

            LogicService cmLogic = new LogicService();

            bool addedSucessfully = cmLogic.AddClient(
                model.Name,
                model.Email,
                model.PhoneNumber,
                UserId,
                model.StreetAddress,
                model.City,
                model.State,
                model.Zip);


            if (addedSucessfully)
            {
                return RedirectToAction("Result", "Dashboard", new { statusCode = 0, message = "Client Added Sucessfully" });
            }
            else
            {
                return RedirectToAction("Result", "Dashboard", new { statusCode = 1, message = "Client Not Added Sucessfully" });
            }
        }

    }
}
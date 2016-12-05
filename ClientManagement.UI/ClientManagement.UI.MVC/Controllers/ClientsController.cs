using ClientManagement.UI.MVC.Models;
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
        // GET: Clients
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddClient(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {

                string UserId = User.Identity.GetUserId();
            }
     
            return View(model);
        }
    }
}
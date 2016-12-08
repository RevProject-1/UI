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
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult ViewClient(int clientId)
        {
            string UserId = User.Identity.GetUserId();

            var model = new ClientViewModel();
            LogicService cmLogic = new LogicService();

            //Acquire client
            var clientsForUser = cmLogic.GetClientsForUser(UserId);
            var matchingClient = clientsForUser.Where(cl => cl.Id == clientId);

            //Acquire jobs for client
            var jobsForUser = cmLogic.GetJobsForUser(UserId);
            var matchingJobsForClient = jobsForUser.Where(j => j.ClientId == clientId);

            //Sort jobs by start date
            var  sortedJobsForClient = matchingJobsForClient.OrderBy(j => j.StartDate).ToList();

            if (matchingClient.Count() > 0)
            {
                var client = matchingClient.First();

                model.Name = client.Name;
                model.Email = client.Email;
                model.PhoneNumber = client.PhoneNumber;
                model.StreetAddress = client.Address.Street;
                model.City = client.Address.City;
                model.State = client.Address.State;
                model.Zip = client.Address.Zip;
                model.JobsForClient = sortedJobsForClient.ToList();

                return View(model);
            }

            else
            {
                return RedirectToAction("Result", "Dashboard", new { statusCode = 1, message = "Unable To Access Client Information" });
            }

        }

        public ActionResult AddClient()
        {
            var model = new AddClientViewModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult AddClient(AddClientViewModel model)
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
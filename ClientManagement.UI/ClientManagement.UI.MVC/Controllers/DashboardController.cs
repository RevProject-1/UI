using ClientManagement.UI.MVC.Models;
using ClientManagement.UI.ServiceAccess;
using ClientManagement.UI.ServiceAccess.cmLogicService;
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
            var jobsForUser = cmLogic.GetJobsForUser(UserId);

            //Remove any completed jobs
            var incompleteJobs = jobsForUser.Where(j => j.Complete == false).ToList();

            //Remove any jobs with a start date greater than 30 days from now
            var upcomingJobs = incompleteJobs.Where(j => ((j.StartDate >= DateTime.Now) && (j.StartDate < DateTime.Now.AddDays(30))) ).ToList();

            //Sort jobs by start date
            List<jobDTO> sortedJobsForUser = new List<jobDTO>();
            sortedJobsForUser = upcomingJobs.OrderBy(j => j.StartDate).ToList();
            model.JobsForUser.JobsToDisplay = sortedJobsForUser;

            return View(model);
        }

        public ActionResult Result(int statusCode, string message)
        {
            DashboardViewModel model = new DashboardViewModel();

            string UserId = User.Identity.GetUserId();
            LogicService cmLogic = new LogicService();

            //Get jobs for user for dashboard view model
            model.ClientsForUser.ClientsToDisplay = cmLogic.GetClientsForUser(UserId);
            var jobsForUser = cmLogic.GetJobsForUser(UserId);

            //Remove any completed jobs
            var incompleteJobs = jobsForUser.Where(j => j.Complete == false).ToList();

            //Remove any jobs with a start date greater than 30 days from now
            var upcomingJobs = incompleteJobs.Where(j => ((j.StartDate >= DateTime.Now) && (j.StartDate < DateTime.Now.AddDays(30)))).ToList();

            //Sort jobs by start date
            List<jobDTO> sortedJobsForUser = new List<jobDTO>();
            sortedJobsForUser = upcomingJobs.OrderBy(j => j.StartDate).ToList();
            model.JobsForUser.JobsToDisplay = sortedJobsForUser;
            model.statusCode = statusCode;
            model.message = message;

            return View(model);
        }

        public ActionResult SearchClient(string clientName)
        {
            DashboardViewModel model = new DashboardViewModel();

            string UserId = User.Identity.GetUserId();
            LogicService cmLogic = new LogicService();

            //Get clients for the user that match the search string
            var allClientsForUser = cmLogic.GetClientsForUser(UserId);
            List<ClientDTO> matchingClients = new List<ClientDTO>();

            foreach (ClientDTO client in allClientsForUser)
            {
                //Only return search result if the search term is 2 characters or more
                if (clientName.Length >= 2)
                {
                    //Make case insensitive
                    //Return client if client name matches or the search term contains the client name
                    if ((client.Name.ToLower().Contains(clientName.ToLower())) || (client.Name.ToLower() == clientName.ToLower()))
                    {
                        matchingClients.Add(client);
                    }
                }
            }

            model.ClientsForUser.ClientsToDisplay = matchingClients;

            return View(model);
        }
    }
}
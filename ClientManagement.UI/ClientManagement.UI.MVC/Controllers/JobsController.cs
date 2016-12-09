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
    public class JobsController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Dashboard");
        }

        public ActionResult ViewJob(int jobId)
        {
            string UserId = User.Identity.GetUserId();

            var model = new JobViewModel();
            LogicService cmLogic = new LogicService();

            //Acquire job from logic
            var jobsForUser = cmLogic.GetJobsForUser(UserId);
            var matchingJob= jobsForUser.Where(j => j.Id == jobId);

            if (matchingJob.Count() > 0)
            {
                var job = matchingJob.First();

                model.ServiceType = job.type.Name;
                model.ClientName = job.client.Name;
                model.Duration = job.EstimatedDuration.ToString();
                model.Notes = job.Notes;
                model.Completed = job.Complete ? "Complete" : "Incomplete";
                model.StartDate = job.StartDate;
                model.JobId = job.Id.ToString();

                return View(model);
            }

            else
            {
                return RedirectToAction("Result", "Dashboard", new { statusCode = 1, message = "Unable To Access Job Information" });
            }
        }

        [HttpGet]
        public ActionResult CompleteJob(string jobId)
        {
            var model = new CompleteJobViewModel();

            model.JobId = jobId;

            return View(model);
        }

        [HttpPost]
        public ActionResult CompleteJob(CompleteJobViewModel model)
        {
            string userId = User.Identity.GetUserId();
            LogicService cmLogic = new LogicService();

            //Send job to complete to logic layer
            var allJobsForUser = cmLogic.GetJobsForUser(userId);
            var jobToComplete = allJobsForUser.Where(j => j.Id == int.Parse(model.JobId)).First();

                bool jobCompletedSuccessfully = cmLogic.CompleteJob(jobToComplete);

                if (jobCompletedSuccessfully)
                {
                    //Reload jobs for updating hours
                    var allJobsForUserRefresh1 = cmLogic.GetJobsForUser(userId);
                    var jobToUpdate = allJobsForUserRefresh1.Where(j => j.Id == int.Parse(model.JobId)).First();

                    //Update job hours
                    jobToUpdate.Hours = decimal.Parse(model.Hours);
                    bool jobUpdated = cmLogic.UpdateJob(jobToUpdate);

                    if (jobUpdated)
                    {
                        //Add expense
                        ExpenseDTO newExpense = new ExpenseDTO();
                        newExpense.Name = model.Expenses[0].ExpenseName;
                        newExpense.Cost = decimal.Parse(model.Expenses[0].ExpenseCost);
                        bool expenseAdded = cmLogic.addExpense(newExpense);

                        if (expenseAdded)
                        {
                            //Reload expenses and jobs, save expense assignment
                            var allJobsForUserRefresh2 = cmLogic.GetJobsForUser(userId);
                            var jobToAssign = allJobsForUserRefresh2.Where(j => j.Id == int.Parse(model.JobId)).First();

                            var allExpenses = cmLogic.GetAllExpenses();
                            var expenseToAssign = allExpenses.Where(ex => ex.Name == newExpense.Name).First();

                            bool expenseAssigned = cmLogic.assignExpense(jobToAssign, expenseToAssign);

                            if (expenseAssigned)
                            {
                                return View(model);
                            }

                            else
                            {
                                return RedirectToAction("Result", "Dashboard", new { statusCode = 1, message = "Failed To Assign Expense" });
                            }
                        }

                        else
                        {
                            return RedirectToAction("Result", "Dashboard", new { statusCode = 1, message = "Failed To Add Expense" });
                        }
                    }
               
                    else
                    {
                        return RedirectToAction("Result", "Dashboard", new { statusCode = 1, message = "Failed To Update Job" });
                    }
                }

                else
                {
                    return RedirectToAction("Result", "Dashboard", new { statusCode = 1, message = "Failed To Complete Job" });
                }

            }

        public ActionResult AddServiceType()
        {
            var model = new ServiceTypeViewModel();

            return View(model);
        }


        [HttpPost]
        public ActionResult AddServiceType(ServiceTypeViewModel model)
        {
            string userId = User.Identity.GetUserId();

            //Send new data to logic layer
            LogicService cmLogic = new LogicService();
            bool addedSucessfully = cmLogic.AddServiceType(model.ServiceName, decimal.Parse(model.HourlyRate), userId);

            //Redirect back to dashbaord, print results to screen
            if (addedSucessfully)
            {
                return RedirectToAction("Result", "Dashboard", new { statusCode = 0, message = "Service Type Added Successfully" });
            }
            else
            {
                return RedirectToAction("Result", "Dashboard", new { statusCode = 1, message = "Failed To Add Service Type" });
            }
        }

        [HttpGet]
        public ActionResult ScheduleJob()
        {
            var model = new AddJobViewModel();

            //Populate list box for displaying Service Type selection
            LogicService cmLogic = new LogicService();

            List<SelectListItem> clientSelection = new List<SelectListItem>();
            var clientOptions = cmLogic.GetClientsForUser(User.Identity.GetUserId());

            foreach (ClientDTO c in clientOptions)
            {
                SelectListItem selectItem = new SelectListItem() { Text = c.Name, Value = c.Name };
                clientSelection.Add(selectItem);
            }
            model.JobOptions.ClientOptions = clientSelection;


            //Populate dropdown for displaying Client selection
            List<SelectListItem> serviceTypeSelection = new List<SelectListItem>();
            var serviceTypeOptions = cmLogic.GetServiceTypes();

            foreach (ServiceTypeDTO st in serviceTypeOptions)
            {
                SelectListItem selectItem = new SelectListItem() { Text = st.Name, Value = st.Name };
                serviceTypeSelection.Add(selectItem);
            }
            model.JobOptions.ServiceTypeOptions = serviceTypeSelection;


            //Return updated view model with selection lists for service types and clients

            return View(model);
        }


        [HttpPost]
        public ActionResult ScheduleJob(AddJobViewModel model)
        {
            string userId = User.Identity.GetUserId();

            //Construct date time to send to logic 
            DateTime startDateTime = new DateTime(
                            int.Parse(model.JobData.Year),
                            int.Parse(model.JobData.Month),
                            int.Parse(model.JobData.Day),
                            int.Parse(model.JobData.Hour),
                            int.Parse(model.JobData.Minute),
                            0
                            );

            //Send new data to logic layer
            LogicService cmLogic = new LogicService();
            bool addedSucessfully = cmLogic.ScheduleJob(
                startDateTime,
                int.Parse(model.JobData.Duration),
                model.JobData.Notes,
                userId,
                model.JobOptions.SelectedClient,
                model.JobOptions.SelectedServiceType);

            //Redirect back to dashbaord, print results to screen
            if (addedSucessfully)
            {
                return RedirectToAction("Result", "Dashboard", new { statusCode = 0, message = "Job Added Successfully" });
            }
            else
            {
                return RedirectToAction("Result", "Dashboard", new { statusCode = 1, message = "Failed To Add Job" });
            }
        }

    }
}
using ClientManagement.UI.ServiceAccess.cmLogicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagement.UI.ServiceAccess
{

    public class LogicService
    {
        private ServiceClient cmLogicService = new ServiceClient();

        #region User Related
        public AspNetUsers GetUserById(string userId)
        {
            List<AspNetUsers> users = cmLogicService.getUserById(userId).ToList();

            return users.First();
        }

        public bool UpdateUser(string name, string email, string phone, string userId, string street, string city, string state, string zip)
        {
            AspNetUsers updatedUser = new AspNetUsers();

            updatedUser.Name = name;
            updatedUser.Email = email;
            updatedUser.UserName = email;
            updatedUser.PhoneNumber = phone;
            updatedUser.Id = userId;
            updatedUser.StreetAddress = street;
            updatedUser.City = city;
            updatedUser.State = state;
            updatedUser.Zip = zip;

            return cmLogicService.updateAspNetUsers(updatedUser);
        }
        #endregion

        #region Client Related
        public List<ClientDTO> GetClientsForUser(string userId)
        {
            List<ClientDTO> clients = cmLogicService.getClientsByUserId(userId).ToList();
            return clients;
        }

        public bool AddClient(string name, string email, string phone, string userId, string street, string city, string state, string zip)
        {
            ClientDTO newClient = new ClientDTO();

            newClient.Name = name;
            newClient.Email = email;
            newClient.PhoneNumber = phone;
            newClient.UserId = userId;

            AddressDTO address = new AddressDTO();

            address.Street = street;
            address.City = city;
            address.State = state;
            address.Zip = zip;

            newClient.Address = address;

            return cmLogicService.insertClients(newClient);
        }
        #endregion

        #region Service Type Related
        public bool AddServiceType(string name, decimal rate, string userId)
        {
            ServiceTypeDTO newServiceType = new ServiceTypeDTO();

            newServiceType.Name = name;
            newServiceType.Rate = rate;
            newServiceType.UserId = userId;

            return cmLogicService.insertServiceType(newServiceType);
        }

        public List<ServiceTypeDTO> GetServiceTypes()
        {
            List<ServiceTypeDTO> serviceTypes = cmLogicService.getServiceTypes().ToList();

            return serviceTypes;
        }
        #endregion

        #region Job Related
        public List<jobDTO> GetJobsForUser(string userId)
        {
            List<jobDTO> jobs = cmLogicService.getJobsForUser(userId).ToList();
            return jobs;
        }

        public bool JobCompletionStatus(string jobId, string userId)
        {
            //Get matching job
            var allJobsForUser = cmLogicService.getJobsForUser(userId);
            var matchingJob = allJobsForUser.Where(j => j.Id == int.Parse(jobId)).First();

            //Return completion status
            return matchingJob.Complete;
        }

        public Invoice GetJobInvoice(string jobId, string userId)
        {
            //Get matching job
            var allJobsForUser = cmLogicService.getJobsForUser(userId);
            var matchingJob = allJobsForUser.Where(j => j.Id == int.Parse(jobId)).First();

            //Return generated invoice
            return cmLogicService.generateInvoice(matchingJob);
        }

        public bool UpdateJobForUser(string jobId, string userId, string hours)
        {
            //Get matching job
            var allJobsForUser = cmLogicService.getJobsForUser(userId);
            var matchingJob = allJobsForUser.Where(j => j.Id == int.Parse(jobId)).First();

            //Update job hours
            matchingJob.Hours = decimal.Parse(hours);

            return cmLogicService.updateJob(matchingJob);
        }

        public bool CompleteJobForUser(string jobId, string userId)
        {
            //Get matching job
            var allJobsForUser = cmLogicService.getJobsForUser(userId);
            var matchingJob = allJobsForUser.Where(j => j.Id == int.Parse(jobId)).First();

            //Complete Job
            return cmLogicService.completeJob(matchingJob);
        }

        public bool ScheduleJob(DateTime startDate, int estDuration, string notes, string userId, string clientName, string serviceTypeName)
        {
            //Define data memebers that need to be assigned for InsertJob in the logic layer
            jobDTO newJob = new jobDTO();

            AspNetUsers user = new AspNetUsers();
            ClientDTO client = new ClientDTO();
            ServiceTypeDTO serviceType = new ServiceTypeDTO();

            //Assign data members given the data from the Jobs controller
            user = cmLogicService.getUserById(userId).First();
            client = cmLogicService.getClientsByName(clientName).First();
            serviceType = cmLogicService.getServiceTypeByName(serviceTypeName).First();

            //Create JobDTO object and pass to logic layer
            newJob.StartDate = startDate;
            newJob.EstimatedDuration = estDuration;
            newJob.Notes = notes;
            newJob.user = user;
            newJob.client = client;
            newJob.type = serviceType;

            return cmLogicService.insertJob(newJob);
        }

        #endregion

        #region Expense Related
        public List<ExpenseDTO> GetAllExpenses()
        {
            List<ExpenseDTO> expenses = cmLogicService.getExpenses().ToList();
            return expenses;
        }

        public List<JobExpenseDTO> GetAllExpenseAssignmentsForJob(string jobId)
        {
            var allExpenseAssignments = cmLogicService.getJobExpenses().ToList();
            var matchingExpenseAssignments = allExpenseAssignments.Where(ea => ea.JobId == int.Parse(jobId)).ToList();

            return matchingExpenseAssignments;
        }

        public bool AddExpense(string expenseName, string expenseCost)
        {
            ExpenseDTO expenseToAdd = new ExpenseDTO();
            expenseToAdd.Name = expenseName;
            expenseToAdd.Cost = decimal.Parse(expenseCost);

            return cmLogicService.insertExpense(expenseToAdd);
        }

        public bool AssignExpense(string jobId, string expenseId, string userId)
        {
            //Get matching expense
            var allExpenses = cmLogicService.getExpenses().ToList();
            var matchingExpense = allExpenses.Where(e => e.Id == int.Parse(expenseId)).First();

            //Get matching job
            var allJobsForUser = cmLogicService.getJobsForUser(userId);
            var matchingJob = allJobsForUser.Where(j => j.Id == int.Parse(jobId)).First();

            //Send expense assignment to logic
            bool result = cmLogicService.insertJobExpense(matchingJob, matchingExpense);

            return result;
        }

        public string GetExpenseIdByName(string expenseName)
        {
            //Get matching expense
            var allExpenses = cmLogicService.getExpenses().ToList();
            var matchingExpense = allExpenses.Where(e => e.Name.ToLower() == expenseName.ToLower()).First();

            //Return expense Id
            return matchingExpense.Id.ToString();
        }
        #endregion

    }
}

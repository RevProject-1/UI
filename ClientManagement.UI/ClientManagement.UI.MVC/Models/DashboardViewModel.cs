using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientManagement.UI.MVC.Models
{
    public class DashboardViewModel
    {
        public DisplayClientsViewModel ClientsForUser { get; set; }
        public DisplayJobsViewModel JobsForUser { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }

        public DashboardViewModel()
        {
            ClientsForUser = new DisplayClientsViewModel();
            JobsForUser = new DisplayJobsViewModel();
        }

    }
}
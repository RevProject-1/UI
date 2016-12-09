using ClientManagement.UI.ServiceAccess.cmLogicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientManagement.UI.MVC.Models
{
    public class JobViewModel
    {
        public bool CompleteRequestSent { get; set; }
        public string ServiceType { get; set; }
        public string ClientName { get; set; }
        public string Duration { get; set; }
        public string Notes { get; set; }
        public string Completed { get; set; }
        public DateTime? StartDate { get; set; }
        public string JobId { get; set; }


        //Only for completed jobs
        public Invoice JobInvoice { get; set; }

        public JobViewModel()
        {
            CompleteRequestSent = false;
        }

    }
}
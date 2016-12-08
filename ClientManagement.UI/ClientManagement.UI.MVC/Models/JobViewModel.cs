using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientManagement.UI.MVC.Models
{
    public class JobViewModel
    {
        public string ServiceType { get; set; }
        public string ClientName { get; set; }
        public string Duration { get; set; }
        public string Notes { get; set; }
        public string Completed { get; set; }
        public DateTime? StartDate { get; set; }
    }
}
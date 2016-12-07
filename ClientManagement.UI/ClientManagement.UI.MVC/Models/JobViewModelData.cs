using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientManagement.UI.MVC.Models
{
    public class JobViewModelData
    {
        public string Day { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Hour { get; set; }
        public string Minute { get; set; }

        public string Duration { get; set; }
        public string Notes { get; set; }
    }
}
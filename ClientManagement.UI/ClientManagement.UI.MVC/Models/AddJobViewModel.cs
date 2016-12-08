using ClientManagement.UI.ServiceAccess;
using ClientManagement.UI.ServiceAccess.cmLogicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManagement.UI.MVC.Models
{
    public class AddJobViewModel
    {
        public JobViewModelOptions JobOptions { get; set; }
        public JobViewModelData JobData { get; set; }

        public AddJobViewModel()
        {
            JobOptions = new JobViewModelOptions();
            JobData = new JobViewModelData();
        }
    }
}
using ClientManagement.UI.ServiceAccess;
using ClientManagement.UI.ServiceAccess.cmLogicService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManagement.UI.MVC.Models
{
    public class JobViewModel
    {
        public JobViewModelOptions JobOptions;
        public JobViewModelData JobData;

        public JobViewModel()
        {
            JobOptions = new JobViewModelOptions();
            JobData = new JobViewModelData();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManagement.UI.MVC.Models
{
    public class JobViewModelOptions
    {
        public List<SelectListItem> ServiceTypeOptions { get; set; }
        public List<SelectListItem> ClientOptions { get; set; }
    }
}
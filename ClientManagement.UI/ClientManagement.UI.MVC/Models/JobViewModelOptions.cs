using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientManagement.UI.MVC.Models
{
    public class JobViewModelOptions
    {
        public List<SelectListItem> ServiceTypeOptions { get; set; }

        [Required]
        public string SelectedServiceType { get; set; }

        public List<SelectListItem> ClientOptions { get; set; }

        [Required]
        public string SelectedClient { get; set; }
    }
}
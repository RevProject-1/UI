using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientManagement.UI.MVC.Models
{
    public class ServiceTypeViewModel
    {
        [Required]
        public string ServiceName { get; set; }

        [Required]
        public string HourlyRate { get; set; }

    }
}
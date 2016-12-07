using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientManagement.UI.MVC.Models
{
    public class UpdateUserViewModel
    {
        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        //Address
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }
}
using ClientManagement.UI.ServiceAccess.cmLogicService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ClientManagement.UI.MVC.Models
{
    public class CompleteJobViewModel
    {
        public string JobId { get; set; }

        public string Hours { get; set; }

        public ExpenseViewModel ExpenseToAdd { get; set; }

        public List<ExpenseViewModel> AllExpensesForJob { get; set; }

        public CompleteJobViewModel()
        {
            ExpenseToAdd = new ExpenseViewModel();
        }


    }
}
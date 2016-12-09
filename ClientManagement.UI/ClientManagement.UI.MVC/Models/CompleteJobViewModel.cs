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

        [Required]
        public string Hours { get; set; }

        public List<ExpenseViewModel> Expenses { get; set; }

        public CompleteJobViewModel()
        {
            Expenses = new List<ExpenseViewModel>();
            ExpenseViewModel Expense = new ExpenseViewModel();
            Expenses.Add(Expense);
        }
    }
}
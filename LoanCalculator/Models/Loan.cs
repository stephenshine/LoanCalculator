using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoanCalculator.Models
{
    public class Loan
    {

        [Required(ErrorMessage = "You must enter and amount")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "You must enter an interest rate")]
        [Display(Name = "Interest rate")]
        public decimal APR { get; set; }
        [Display(Name = "Term in months")]
        [Required(ErrorMessage = "You must enter a term")]
        public int TermInMonths { get; set; }

        public Loan() { }
        public Loan(decimal amount, decimal apr, int term)
        {
            Amount = amount;
            APR = apr;
            TermInMonths = term;
        }
    }
}
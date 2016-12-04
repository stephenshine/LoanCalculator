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
        [Range(500, Double.MaxValue, ErrorMessage = "The loan must be at least £500")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "You must enter an interest rate")]
        [Range(0.5, 100, ErrorMessage = "The interest rate must be between 0.5% and 100%")]
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chart.Mvc.SimpleChart;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LoanCalculator.Models
{
    [DisplayName("Information about loan")]
    public class LoanViewModel
    {
        public LoanViewModel(Loan loan)
        {
            Loan = loan;
        }

        public Loan Loan { get; set; }

        [DisplayName("Monthly repayment")]
        public decimal MonthlyRepayment { get; set; }

        [DisplayName("Total interest")]
        public decimal TotalInterest { get; set; }

        [DisplayName("Total amount to repay")]
        public decimal TotalRepaid { get; set; }

        [DisplayName("Opening balance")]
        public decimal[] OpeningBalances { get; set; }

        [DisplayName("Debit")]
        public decimal[] Debits { get; set; }

        [DisplayName("Credit")]
        public decimal[] Credits { get; set; }

        [DisplayName("Closing balance")]
        public decimal[] ClosingBalances { get; set; }
        public PieChart PieChart { get; set; }
    }
}
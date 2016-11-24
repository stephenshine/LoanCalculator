using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanCalculator.Models
{
    public class LoanViewModel
    {

        public LoanViewModel(Loan loan)
        {
            Loan = loan;
        }
        public Loan Loan { get; set; }

        public decimal MonthlyRepayment { get { return Loan.CalculateMonthlyRepayment(); } }
        public decimal TotalInterest { get { return Loan.TotalInterest; } }
        public decimal TotalRepaid { get { return Loan.TotalRepaid; } }
        public decimal Amount { get { return Loan.Amount; } }
        public List<Transaction> Transactions { get { return Loan.Transactions; } }

    }
}
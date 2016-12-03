using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chart.Mvc.ComplexChart;
using Chart.Mvc.SimpleChart;

namespace LoanCalculator.Models
{
    // This class is just for displaying data on the view
    public class LoanViewModel
    {
        // set to private because only data within objects needs to be exposed
        private Loan Loan { get; set; }
        private LoanChart LoanChart { get; set; }

        // read only properties get information from private objects
        public decimal Amount { get { return Loan.Amount; } }
        //public decimal MonthlyRepayment { get { return Loan.MonthlyRepayment; } }
        public decimal TotalInterest { get { return Loan.TotalInterest; } }
        public decimal TotalRepaid { get { return Loan.TotalRepaid; } }
        public List<Transaction> Transactions { get { return Loan.Transactions; } }
        public PieChart Pie { get { return LoanChart.CreatePieChart(); } }

        // constructor sets private objects
        public LoanViewModel(Loan loan)
        {
            Loan = loan;
            //loan.RepayLoan();
            LoanChart = new LoanChart(Loan);
        }
    }
}
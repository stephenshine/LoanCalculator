using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chart.Mvc.ComplexChart;
using Chart.Mvc.SimpleChart;

namespace LoanCalculator.Models
{
    public class LoanViewModel
    {
        private Loan Loan { get; set; }
        private LoanChart LoanChart { get; set; }

        public LoanViewModel(Loan loan)
        {
            Loan = loan;
            LoanChart = new LoanChart(Loan);
        }

        public decimal Amount { get { return Loan.Amount; } }
        public decimal MonthlyRepayment { get { return Loan.MonthlyRepayment; } }
        public decimal TotalInterest { get { return Loan.TotalInterest; } }
        public decimal TotalRepaid { get { return Loan.TotalRepaid; } }
        public List<Transaction> Transactions { get { return Loan.Transactions; } }
        public PieChart Pie { get { return LoanChart.CreatePieChart(); } }

    }
}
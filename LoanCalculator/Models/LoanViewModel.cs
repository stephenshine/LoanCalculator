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

        public LoanViewModel(Loan loan)
        {
            Loan = loan;
        }
        public Loan Loan { get; set; }

        public decimal Amount { get { return Loan.Amount; } }
        public decimal MonthlyRepayment { get { return Loan.MonthlyRepayment; } }
        public decimal TotalInterest { get { return Loan.TotalInterest; } }
        public decimal TotalRepaid { get { return Loan.TotalRepaid; } }
        public List<Transaction> Transactions { get { return Loan.Transactions; } }

        public PieChart Pie { get { return CreatePieChart(); } }

        private PieChart CreatePieChart()
        {
            PieChart pieChart = new PieChart();
            SimpleData interest = new SimpleData()
            {
                Value = (double)Math.Round(TotalInterest, 2),
                Label = "Interest",
                Color = "rgba(220, 0, 0, 0.5)"
            };
            SimpleData capital = new SimpleData()
            {
                Value = (double)Math.Round(Amount, 2),
                Label = "Capital",
                Color = "rgba(0, 220, 0, 0.5)"
            };
            pieChart.Data.Add(interest);
            pieChart.Data.Add(capital);
            return pieChart;
        }
    }
}
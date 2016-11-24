using Chart.Mvc.SimpleChart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanCalculator.Models
{
    public class LoanChart
    {
        public LoanChart(Loan loan)
        {
            Loan = loan;
        }

        Loan Loan { get; set; }

        public PieChart CreatePieChart()
        {
            PieChart pieChart = new PieChart();
            SimpleData interest = new SimpleData()
            {
                Value = (double)Math.Round(Loan.TotalInterest, 2),
                Label = "Interest",
                Color = "rgba(220, 0, 0, 0.5)"
            };
            SimpleData capital = new SimpleData()
            {
                Value = (double)Math.Round(Loan.Amount, 2),
                Label = "Capital",
                Color = "rgba(0, 220, 0, 0.5)"
            };
            pieChart.Data.Add(interest);
            pieChart.Data.Add(capital);
            return pieChart;
        }
    }
}
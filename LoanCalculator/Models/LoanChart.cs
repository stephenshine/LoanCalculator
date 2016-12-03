using Chart.Mvc.SimpleChart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanCalculator.Models
{
    public class LoanChart
    {

        // Loan data is used to draw chart
        private Loan Loan { get; set; }

        public LoanChart(Loan loan)
        {
            Loan = loan;
        }

        public PieChart CreatePieChart()
        {
            // creates a new piechart object
            PieChart pieChart = new PieChart();

            // add interest from loan to chart data
            SimpleData interest = new SimpleData()
            {
                //Value = (double)Math.Round(Loan.TotalInterest, 2),
                Label = "Interest",
                Color = "rgba(220, 0, 0, 0.5)",
            };
            pieChart.Data.Add(interest);

            // add capital from loan to chart data
            SimpleData capital = new SimpleData()
            {
                Value = (double)Math.Round(Loan.Amount, 2),
                Label = "Capital",
                Color = "rgba(0, 220, 0, 0.5)"
            };
            pieChart.Data.Add(capital);

            // return the completed chart
            return pieChart;
        }
    }
}
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
   
        public int TermInMonths { get; set; }
        public decimal MonthlyRepayment { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalRepaid { get; set; }

        public List<string> OpeningBalances = new List<string>();
        public List<decimal> Debits = new List<decimal>();
        public List<decimal> Credits = new List<decimal>();
        public List<decimal> ClosingBalances = new List<decimal>();
    }
}
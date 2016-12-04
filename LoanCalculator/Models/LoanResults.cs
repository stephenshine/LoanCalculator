using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Chart.Mvc.SimpleChart;

namespace LoanCalculator.Models
{
    public class LoanResults
    {
        public int TermInMonths { get; set; }
        public decimal MonthlyRepayment { get; set; }
        public decimal[] OpeningBalances { get; set; }
        public decimal[] Debits { get; set; }
        public decimal[] Credits { get; set; }
        public decimal[] ClosingBalances { get; set; }
        public PieChart pieChart { get; set; }
    }
}
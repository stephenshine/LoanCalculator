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
        public decimal Repayment { get; set; }
        public PieChart pieChart { get; set; }
        public decimal[] OpeningBalances { get; set; }
    }
}
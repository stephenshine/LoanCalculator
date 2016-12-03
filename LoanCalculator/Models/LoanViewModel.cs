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
   
        public decimal MonthlyRepayment { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalRepaid { get; set; }
        public List<List<decimal>> Transactions = new List<List<decimal>>();
        public List<int> StatementMonths = new List<int>();
        public List<decimal> Debits = new List<decimal>();
    }
}
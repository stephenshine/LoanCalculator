﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public decimal MonthlyRepayment { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalRepaid { get; set; }
        public decimal[] OpeningBalances { get; set; }
        public decimal[] Debits { get; set; }
        public decimal[] Credits { get; set; }
        public decimal[] ClosingBalances { get; set; }
        public PieChart PieChart { get; set; }
    }
}
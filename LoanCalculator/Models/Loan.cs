using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanCalculator.Models
{
    public class Loan
    {
        public double Amount { get; set; }
        public double APR { get; set; }
        public double TermInMonths { get; set; }
        public double MonthylyInterestRate { get { return (APR / 12) / 100; } }

        public double MonthlyRepayment { get; set; }

        public void MonthlyTransaction()
        {
            Amount += (Amount * MonthylyInterestRate);
            if (MonthlyRepayment > Amount)
            {
                MonthlyRepayment = Amount;
            }
            Amount -= MonthlyRepayment;
        }

        public void CalculateMonthlyRepayment()
        {
            double payment = 0;
            double power = Math.Pow((1 + MonthylyInterestRate), (TermInMonths * -1));
            double denominator = 1 - power;
            double numerator = Amount * (MonthylyInterestRate);
            payment = numerator / denominator;

            MonthlyRepayment = payment;
        }
    }
}
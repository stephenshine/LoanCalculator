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

        public List<Transaction> Transactions { get; set; }

        public void MonthlyTransaction()
        {
            Amount += CalculateMonthlyInterest();
            if (MonthlyRepayment > Amount)
            {
                MonthlyRepayment = Amount;
            }
            Amount -= MonthlyRepayment;
        }

        private double CalculateMonthlyInterest()
        {
            return Amount * MonthylyInterestRate;
        }

        public void RepayLoan()
        {
            List<Transaction> transactions = new List<Transaction>();
            for(int i = 1; Amount > 0 ; i++)
            {
                MonthlyTransaction();
                transactions.Add(new Transaction
                {
                    TransactionID = i,
                    Balance = Amount,
                    Debit = CalculateMonthlyInterest(),
                    Credit = MonthlyRepayment
                });
            }
            Transactions = transactions;
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
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanCalculator.Models
{
    public class Loan
    {
        public Loan(decimal amount, decimal apr, int term)
        {
            Amount = amount;
            APR = apr;
            TermInMonths = term;
        }

        #region inputs
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        public decimal APR { get; set; }
        [Display(Name = "Term in months")]
        public int TermInMonths { get; set; }
        #endregion inputs

        public decimal MonthlyRepayment { get; set; }
        public List<Transaction> Transactions { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalRepaid { get; set; }
        private decimal OutstandingBalance { get; set; }
        private decimal MonthylyInterestRate { get { return (APR / 12) / 100; } }

        #region private methods
        private void CalculateTotalInterest()
        {
            foreach(Transaction t in Transactions)
            {
                TotalInterest += t.Debit;
            }
        }

        private void CalculateTotalAmountRepid()
        {
            foreach(Transaction t in Transactions)
            {
                TotalRepaid += t.Credit;
            }
        }

        private decimal CalculateMonthlyInterest()
        {
            return OutstandingBalance * MonthylyInterestRate;
        }
        #endregion private methods

        public void RepayLoan()
        {
            List<Transaction> transactions = new List<Transaction>();
            decimal interest = 0;
            OutstandingBalance = Amount;
            MonthlyRepayment = CalculateMonthlyRepayment();

            for (int i = 1; i <= TermInMonths; i++)
            {

                Transaction transaction = new Transaction(i, OutstandingBalance);

                interest = CalculateMonthlyInterest();

                transaction.Debit = interest;
                OutstandingBalance += interest;

                if (i == TermInMonths)
                {
                    MonthlyRepayment = OutstandingBalance;
                }
                transaction.Credit = MonthlyRepayment;

                OutstandingBalance -= MonthlyRepayment;
                transaction.ClosingBalance = OutstandingBalance;


                transactions.Add(transaction);

            }

            Transactions = transactions;
            CalculateTotalInterest();
            CalculateTotalAmountRepid();
        }

        public decimal CalculateMonthlyRepayment()
        {
            decimal payment = 0;
            decimal power = (decimal)Math.Pow((double)(1 + MonthylyInterestRate), (TermInMonths * -1));
            decimal denominator = 1 - power;
            decimal numerator = Amount * MonthylyInterestRate;
            payment = numerator / denominator;

            return payment;
        }
    }
}
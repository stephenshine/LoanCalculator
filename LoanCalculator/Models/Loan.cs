using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LoanCalculator.Models
{
    public class Loan
    {

        #region inputs
        public decimal Amount { get; set; }
        public decimal APR { get; set; }
        public int TermInMonths { get; set; }
        #endregion inputs

        public List<Transaction> Transactions { get; set; }

        // readonly properties - only have get accessor
        public decimal MonthlyRepayment { get { return CalculateMonthlyRepayment(); } }
        public decimal TotalInterest { get { return Transactions.Sum(t => t.Debit); } }
        public decimal TotalRepaid { get { return Transactions.Sum(t => t.Credit); } }

        private decimal OutstandingBalance { get; set; }
        private decimal MonthylyInterestRate { get { return (APR / 12) / 100; } }

        public Loan(decimal amount, decimal apr, int term)
        {
            Amount = amount;
            APR = apr;
            TermInMonths = term;
        }

        public void RepayLoan()
        {
            // local variables used for each payment
            // a transaction stores information about each payment
            // interest is the interest due on that months repayment
            // repayment is the amount due to pay that month
            // OutstandingBalance is the amount left to pay
            List<Transaction> transactions = new List<Transaction>();
            decimal interest = 0;
            decimal repayment = MonthlyRepayment;
            OutstandingBalance = Amount;

            // carry out a transaction for each month of the term
            for (int i = 1; i <= TermInMonths; i++)
            {
                // transaction ID is the month, OutstandingBalance is the opening balance
                Transaction transaction = new Transaction(i, OutstandingBalance);

                // calculate the interest due on the balance
                // add the interest to the OutstandingBalance
                // the debit for the current transaction is the interest
                interest = CalculateMonthlyInterest();
                OutstandingBalance += interest;
                transaction.Debit = interest;

                // if the loan is in the last month, the repayment should clear the loan
                // otherwise it will be the normal monthly repayment
                // reduce the balance by the repayment
                // the credit for the current transaction is the repayment
                if (i == TermInMonths)
                {
                    repayment = OutstandingBalance;
                }
                OutstandingBalance -= repayment;
                transaction.Credit = repayment;

                // now the transaction has been completed, the closing balance is the OutstandingBalance
                // add the whole transaction to the list
                transaction.ClosingBalance = OutstandingBalance;
                transactions.Add(transaction);
            }
            // all the transactions are added to the public property
            Transactions = transactions;
        }

        #region private methods
        private decimal CalculateMonthlyInterest()
        {
            return Math.Round(OutstandingBalance * MonthylyInterestRate, 2);
        }

        private decimal CalculateMonthlyRepayment()
        {
            decimal payment = 0;
            decimal power = (decimal)Math.Pow((double)(1 + MonthylyInterestRate), (TermInMonths * -1));
            decimal denominator = 1 - power;
            decimal numerator = Amount * MonthylyInterestRate;
            payment = numerator / denominator;

            return Math.Round(payment, 2);
        }
        #endregion private methods
    }
}
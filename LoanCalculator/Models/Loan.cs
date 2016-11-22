using System;
using System.Collections.Generic;
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

        public decimal Amount { get; set; }
        public decimal APR { get; set; }
        public int TermInMonths { get; set; }
        public decimal MonthlyRepayment { get; set; }
        public List<Transaction> Transactions { get; set; }
        public decimal TotalInterest { get; set; }
        public decimal TotalRepaid { get; set; }
        private decimal OutstandingBalance { get; set; }
        private decimal MonthylyInterestRate { get { return (APR / 12) / 100; } }

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

        public void RepayLoan()
        {
            // new list of transactions will store information about each repayment
            List<Transaction> transactions = new List<Transaction>();

            // calcualte the monthly repayment first
            CalculateMonthlyRepayment();
            OutstandingBalance = Amount;

            // i will be the transaction id, keep looping while there's an outstanding balance
            for (int i = 1; OutstandingBalance > 0.5m ; i++)
            {

                decimal interest = 0;
                // create a new transaction - (id, StartingBalance)
                Transaction transaction = new Transaction(i, OutstandingBalance);

                // check if the monthly repayment is more than outstanding balance
                // if it is, set the monthly repayment amount to be the outstanding balance
                if (OutstandingBalance < MonthlyRepayment)
                {

                    MonthlyRepayment = OutstandingBalance;
                }
                transaction.Credit = MonthlyRepayment;


                // reduce balance by monthly repayment
                // then increase by interest
                //OutstandingBalance += interest;
                OutstandingBalance = Math.Round((OutstandingBalance - MonthlyRepayment), 2);
                transaction.ClosingBalance = OutstandingBalance;

                if (OutstandingBalance > 0)
                {
                    // calculate the monthly interest before the transaction
                    interest = CalculateMonthlyInterest();
                    // debit for the transaction is the monthly interest
                    transaction.Debit = interest;
                    OutstandingBalance = Math.Round((OutstandingBalance + interest), 2);
                }
                else
                {
                    transaction.Debit = interest;
                }

                // add the whole transaction to the list
                transactions.Add(transaction);
            }

            // copy list of transactions to transaction property
            Transactions = transactions;
            CalculateTotalInterest();
            CalculateTotalAmountRepid();
        }

        public void CalculateMonthlyRepayment()
        {
            decimal payment = 0;
            decimal power = (decimal)Math.Pow((double)(1 + MonthylyInterestRate), (TermInMonths * -1));
            decimal denominator = 1 - power;
            decimal numerator = Amount * MonthylyInterestRate;
            payment = numerator / denominator;

            MonthlyRepayment = Math.Round(payment, 2);
        }
    }
}
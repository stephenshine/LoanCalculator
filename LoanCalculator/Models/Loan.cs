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
        public double MonthlyRepayment { get; set; }
        public List<Transaction> Transactions { get; set; }
        public double TotalInterest { get; set; }
        public double TotalRepaid { get; set; }
        private double OutstandingBalance { get; set; }
        private double MonthylyInterestRate { get { return (APR / 12) / 100; } }

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


        private double CalculateMonthlyInterest()
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
            for (int i = 1; OutstandingBalance > 0 ; i++)
            {
                // calculate the monthly interest before the transaction
                double interest = CalculateMonthlyInterest();
            
                // create a new transaction - (id, StartingBalance)
                Transaction transaction = new Transaction(i, OutstandingBalance);

                // debit for the transaction is the monthly interest
                transaction.Debit = interest;

                // check if the monthly repayment is more than outstanding balance
                // if it is, set the monthly repayment amount to be the outstanding balance
                if(OutstandingBalance < MonthlyRepayment)
                {
                    transaction.Credit = OutstandingBalance;
                }
                else
                {
                    transaction.Credit = MonthlyRepayment;
                }

                // reduce balance by monthly repayment
                // then increase by interest
                OutstandingBalance -= MonthlyRepayment;
                OutstandingBalance += interest;
                transaction.ClosingBalance = OutstandingBalance;

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
            double payment = 0;
            double power = Math.Pow((1 + MonthylyInterestRate), (TermInMonths * -1));
            double denominator = 1 - power;
            double numerator = Amount * (MonthylyInterestRate);
            payment = numerator / denominator;

            MonthlyRepayment = payment;
        }
    }
}
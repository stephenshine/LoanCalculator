using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoanCalculator.Models;

namespace LoanCalculator.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }
        public HomeController(Loan loan)
        {
            Loan = loan;
        }

        private Loan Loan = new Loan();

        public ActionResult Index()
        {
            return View();
        }

        // Modelbinding extracts values from loan passed to method
        public PartialViewResult Result(decimal Amount = 0, decimal APR = 0, int TermInMonths = 0)
        {
            Loan.Amount = Amount;
            Loan.APR = APR;
            Loan.TermInMonths = TermInMonths;
            //Loan loan = new Loan(Amount, APR, TermInMonths);
            if (APR != 0)
            {
                MonthlyRepayment = CalculateMonthlyRepayment();
            }

            ViewBag.MonthlyRepayment = MonthlyRepayment;
            //LoanViewModel model = new LoanViewModel(Loan);
            
            return PartialView(Loan);
        }

        public decimal MonthlyRepayment { get; set; }
        private decimal MonthylyInterestRate { get; set; }
        private decimal OutstandingBalance { get; set; }
        //public List<Transaction> Transactions { get; set; }
        private List<decimal> OutstandingBalances { get; set; }
        private List<decimal> Debits { get; set; }
        private List<decimal> Credits { get; set; }
        private List<decimal> ClosingBalances { get; set; }

        private void CalculateMonthlyInterestRate()
        {
            MonthylyInterestRate = (Loan.APR / 12) / 100;
        }

        private decimal CalculateMonthlyInterest()
        {
            return Math.Round(OutstandingBalance * MonthylyInterestRate, 2);
        }

        private decimal CalculateMonthlyRepayment()
        {
            CalculateMonthlyInterestRate();
            decimal payment = 0;
            decimal power = (decimal)Math.Pow((double)(1 + MonthylyInterestRate), (Loan.TermInMonths * -1));
            decimal denominator = 1 - power;
            decimal numerator = Loan.Amount * MonthylyInterestRate;
            payment = numerator / denominator;

            return Math.Round(payment, 2);
        }

        public void RepayLoan()
        {
            // local variables used for each payment
            // a transaction stores information about each payment
            // interest is the interest due on that months repayment
            // repayment is the amount due to pay that month
            // OutstandingBalance is the amount left to pay

            // List<Transaction> transactions = new List<Transaction>();
            decimal interest = 0;
            decimal repayment = MonthlyRepayment;
            OutstandingBalance = Loan.Amount;

            // carry out a transaction for each month of the term
            for (int i = 1; i <= Loan.TermInMonths; i++)
            {
                // transaction ID is the month, OutstandingBalance is the opening balance
                OutstandingBalances.Add(OutstandingBalance);
                // Transaction transaction = new Transaction(i, OutstandingBalance);

                // calculate the interest due on the balance
                // add the interest to the OutstandingBalance
                // the debit for the current transaction is the interest
                interest = CalculateMonthlyInterest();
                OutstandingBalance += interest;
                Debits.Add(interest);
                // transaction.Debit = interest;

                // if the loan is in the last month, the repayment should clear the loan
                // otherwise it will be the normal monthly repayment
                // reduce the balance by the repayment
                // the credit for the current transaction is the repayment
                if (i == Loan.TermInMonths)
                {
                    repayment = OutstandingBalance;
                }
                OutstandingBalance -= repayment;
                Credits.Add(repayment);
                //transaction.Credit = repayment;

                // now the transaction has been completed, the closing balance is the OutstandingBalance
                // add the whole transaction to the list
                ClosingBalances.Add(OutstandingBalance);

                //transaction.ClosingBalance = OutstandingBalance;
                //transactions.Add(transaction);
            }
            // all the transactions are added to the public property
            //Transactions = transactions;
        }

    }
}
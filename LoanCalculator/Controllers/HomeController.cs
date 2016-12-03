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
            LoanViewModel Model = new LoanViewModel();
            decimal MonthlyRepayment = 0;
            decimal MonthlyInterestRate = (APR / 12) / 100;

            if (Amount != 0 && APR != 0 && TermInMonths != 0)
            {
                Model.TermInMonths = TermInMonths;
                MonthlyRepayment = CalculateMonthlyRepayment(Amount, MonthlyInterestRate, TermInMonths);
                Model.MonthlyRepayment = MonthlyRepayment;
                decimal interest = 0;
                decimal repayment = MonthlyRepayment;
                decimal OutstandingBalance = Amount;
                string[] OpeningBalances = new string[TermInMonths];
                string[] Debits = new string[TermInMonths];
                string[] Credits = new string[TermInMonths];
                string[] ClosingBalances = new string[TermInMonths];

                for (int i = 0; i < TermInMonths; i++)
                {
                    OpeningBalances[i] = OutstandingBalance.ToString("c");

                    interest = CalculateMonthlyInterest(OutstandingBalance, MonthlyInterestRate);
                    OutstandingBalance += interest;
                    Debits[i] = interest.ToString("c");

                    if (i == (TermInMonths-1))
                    {
                        repayment = OutstandingBalance;
                    }
                    OutstandingBalance -= repayment;
                    Credits[i] = repayment.ToString("c");

                    ClosingBalances[i] = OutstandingBalance.ToString("c");
                }
                ViewBag.OpeningBalances = OpeningBalances;
                ViewBag.Debits = Debits;
                ViewBag.Credits = Credits;
                ViewBag.ClosingBalances = ClosingBalances;
            }

            return PartialView(Model);
        }

        private decimal CalculateMonthlyInterest(decimal Balance, decimal InterestRate)
        {
            return Math.Round(Balance * InterestRate, 2);
        }

        private decimal CalculateMonthlyRepayment(decimal Amount, decimal MonthlyInterestRate, int TermInMonths)  
        {
            decimal payment = 0;
            decimal power = (decimal)Math.Pow((double)(1 + MonthlyInterestRate), (TermInMonths * -1));
            decimal denominator = 1 - power;
            decimal numerator = Amount * MonthlyInterestRate;
            payment = numerator / denominator;

            return Math.Round(payment, 2);
        }

        //public void RepayLoan()
        //{
        //    decimal interest = 0;
        //    decimal repayment = MonthlyRepayment;
        //    OutstandingBalance = Loan.Amount;

        //    for (int i = 1; i <= Loan.TermInMonths; i++)
        //    {
        //        OutstandingBalances.Add(OutstandingBalance);

        //        interest = CalculateMonthlyInterest();
        //        OutstandingBalance += interest;
        //        Debits.Add(interest);

        //        if (i == Loan.TermInMonths)
        //        {
        //            repayment = OutstandingBalance;
        //        }
        //        OutstandingBalance -= repayment;
        //        Credits.Add(repayment);

        //        ClosingBalances.Add(OutstandingBalance);
        //    }
        //}

    }
}
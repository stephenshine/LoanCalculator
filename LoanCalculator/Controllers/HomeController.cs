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
            bool ShowResults = false;

            if (Amount != 0 && APR != 0 && TermInMonths != 0)
            {
                decimal MonthlyInterestRate = (APR / 12) / 100;
                decimal MonthlyRepayment = CalculateMonthlyRepayment(Amount, MonthlyInterestRate, TermInMonths);
                decimal Repayment = MonthlyRepayment;
                ViewBag.MonthlyRepayment = MonthlyRepayment;

                decimal OutstandingBalance = Amount;
                decimal Interest = 0;
                decimal TotalInterest = 0;
                decimal TotalRepaid = 0;

                decimal[] OpeningBalances = new decimal[TermInMonths];
                decimal[] Debits = new decimal[TermInMonths];
                decimal[] Credits = new decimal[TermInMonths];
                decimal[] ClosingBalances = new decimal[TermInMonths];

                for (int i = 0; i < TermInMonths; i++)
                {
                    OpeningBalances[i] = OutstandingBalance;

                    Interest = Math.Round(OutstandingBalance * MonthlyInterestRate, 2);
                    OutstandingBalance += Interest;
                    Debits[i] = Interest;

                    if (i == (TermInMonths-1))
                    {
                        Repayment = OutstandingBalance;
                    }
                    OutstandingBalance -= Repayment;
                    Credits[i] = Repayment;

                    ClosingBalances[i] = OutstandingBalance;
                }
                TotalInterest = Debits.Sum();
                TotalRepaid = Credits.Sum();
                ShowResults = true;

                ViewBag.TotalInterest = TotalInterest;
                ViewBag.TotalRepaid = TotalRepaid;
                ViewBag.OpeningBalances = OpeningBalances;
                ViewBag.Debits = Debits;
                ViewBag.Credits = Credits;
                ViewBag.ClosingBalances = ClosingBalances;
            }
            ViewBag.ShowResults = ShowResults;

            return PartialView(Loan);
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

    }
}
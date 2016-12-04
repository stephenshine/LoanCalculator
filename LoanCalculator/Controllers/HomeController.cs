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

        private Loan Loan = new Loan(0, 0, 0);

        public ActionResult Index()
        {
            return View(Loan);
        }

        public PartialViewResult Result(Loan model)
        {
            Loan = model;
            bool ShowResults = false;

            if(ModelState.IsValid)
            {
                decimal MonthlyInterestRate = (Loan.APR / 12) / 100;
                decimal MonthlyRepayment = CalculateMonthlyRepayment(Loan.Amount, MonthlyInterestRate, Loan.TermInMonths);
                decimal Repayment = MonthlyRepayment;
                ViewBag.MonthlyRepayment = MonthlyRepayment;

                decimal OutstandingBalance = Loan.Amount;
                decimal Interest = 0;
                decimal TotalInterest = 0;
                decimal TotalRepaid = 0;

                decimal[] OpeningBalances = new decimal[Loan.TermInMonths];
                decimal[] Debits = new decimal[Loan.TermInMonths];
                decimal[] Credits = new decimal[Loan.TermInMonths];
                decimal[] ClosingBalances = new decimal[Loan.TermInMonths];

                for (int i = 0; i < Loan.TermInMonths; i++)
                {
                    OpeningBalances[i] = OutstandingBalance;

                    Interest = Math.Round(OutstandingBalance * MonthlyInterestRate, 2);
                    OutstandingBalance += Interest;
                    Debits[i] = Interest;

                    if (i == (Loan.TermInMonths -1))
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
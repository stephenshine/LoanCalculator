using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LoanCalculator.Models;
using Chart.Mvc.SimpleChart;

namespace LoanCalculator.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        public ActionResult Index()
        {
            return View(new Loan(0, 0, 0));
        }

        public PartialViewResult CalculateResults(Loan loan)
        {
            bool ShowResults = false;
            if (ModelState.IsValid)
            {
                // viewModel.TermInMonths = model.TermInMonths;
                decimal MonthlyInterestRate = (loan.APR / 12) / 100;
                decimal MonthlyRepayment = CalculateMonthlyRepayment(loan.Amount, MonthlyInterestRate, loan.TermInMonths);
                ViewBag.MonthlyRepayment = MonthlyRepayment;
                decimal Repayment = MonthlyRepayment;

                decimal OutstandingBalance = loan.Amount;
                decimal Interest = 0;
                decimal TotalInterest = 0;

                ViewBag.OpeningBalances = new decimal[loan.TermInMonths];
                ViewBag.Debits = new decimal[loan.TermInMonths];
                ViewBag.Credits = new decimal[loan.TermInMonths];
                ViewBag.ClosingBalances = new decimal[loan.TermInMonths];

                for (int i = 0; i < loan.TermInMonths; i++)
                {
                    ViewBag.OpeningBalances[i] = OutstandingBalance;

                    Interest = Math.Round(OutstandingBalance * MonthlyInterestRate, 2);
                    OutstandingBalance += Interest;
                    TotalInterest += Interest;
                    ViewBag.Debits[i] = Interest;

                    if (i == (loan.TermInMonths -1))
                    {
                        Repayment = OutstandingBalance;
                    }
                    OutstandingBalance -= Repayment;
                    ViewBag.Credits[i] = Repayment;

                    ViewBag.ClosingBalances[i] = OutstandingBalance;
                }

                ShowResults = true;
                ViewBag.TotalInterest = TotalInterest;
                ViewBag.TotalRepaid = loan.Amount + TotalInterest;

                ViewBag.ChartData = new List<SimpleData>
                {
                    new SimpleData
                    {
                        Value = (double)Math.Round(TotalInterest, 2),
                        Label = "Interest",
                        Color = "rgba(220, 0, 0, 0.5)",
                    },
                    new SimpleData()
                    {
                        Value = (double)Math.Round(loan.Amount, 2),
                        Label = "Capital",
                        Color = "rgba(0, 220, 0, 0.5)"
                    }};
            }
            ViewBag.ShowResults = ShowResults;

            return PartialView("Results", loan);
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
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
            LoanViewModel Model = new LoanViewModel(loan);
            bool ShowResults = false;
            if (ModelState.IsValid)
            {
                decimal MonthlyInterestRate = (loan.APR / 12) / 100;
                decimal MonthlyRepayment = CalculateMonthlyRepayment(loan.Amount, MonthlyInterestRate, loan.TermInMonths);
                Model.MonthlyRepayment = MonthlyRepayment;
                decimal Repayment = MonthlyRepayment;

                decimal OutstandingBalance = loan.Amount;
                decimal Interest = 0;
                decimal TotalInterest = 0;

                Model.OpeningBalances = new decimal[loan.TermInMonths];
                Model.Debits = new decimal[loan.TermInMonths];
                Model.Credits = new decimal[loan.TermInMonths];
                Model.ClosingBalances = new decimal[loan.TermInMonths];

                for (int i = 0; i < loan.TermInMonths; i++)
                {
                    Model.OpeningBalances[i] = OutstandingBalance;

                    Interest = Math.Round(OutstandingBalance * MonthlyInterestRate, 2);
                    OutstandingBalance += Interest;
                    TotalInterest += Interest;
                    Model.Debits[i] = Interest;

                    if (i == (loan.TermInMonths -1))
                    {
                        Repayment = OutstandingBalance;
                    }
                    OutstandingBalance -= Repayment;
                    Model.Credits[i] = Repayment;

                    Model.ClosingBalances[i] = OutstandingBalance;
                }

                ShowResults = true;
                Model.TotalInterest = TotalInterest;
                Model.TotalRepaid = loan.Amount + TotalInterest;

                PieChart chart = new PieChart();
                chart.Data.AddRange(new List<SimpleData>
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
                    }});
                Model.PieChart = chart;
            }
            ViewBag.ShowResults = ShowResults;

            return PartialView("Results", Model);
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
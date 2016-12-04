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

        public PartialViewResult Result(Loan model)
        {
            bool ShowResults = false;
            LoanResults viewModel = new LoanResults();
            if (ModelState.IsValid)
            {
                viewModel.TermInMonths = model.TermInMonths;
                decimal MonthlyInterestRate = (model.APR / 12) / 100;
                viewModel.MonthlyRepayment = CalculateMonthlyRepayment(model.Amount, MonthlyInterestRate, model.TermInMonths);
                decimal Repayment = viewModel.MonthlyRepayment;

                decimal OutstandingBalance = model.Amount;
                decimal Interest = 0;
                decimal TotalInterest = 0;

                viewModel.OpeningBalances = new decimal[model.TermInMonths];
                viewModel.Debits = new decimal[model.TermInMonths];
                viewModel.Credits = new decimal[model.TermInMonths];
                viewModel.ClosingBalances = new decimal[model.TermInMonths];

                for (int i = 0; i < model.TermInMonths; i++)
                {
                    viewModel.OpeningBalances[i] = OutstandingBalance;

                    Interest = Math.Round(OutstandingBalance * MonthlyInterestRate, 2);
                    OutstandingBalance += Interest;
                    viewModel.Debits[i] = Interest;

                    if (i == (model.TermInMonths -1))
                    {
                        Repayment = OutstandingBalance;
                    }
                    OutstandingBalance -= Repayment;
                    viewModel.Credits[i] = Repayment;

                    viewModel.ClosingBalances[i] = OutstandingBalance;
                }

                ShowResults = true;
                viewModel.TotalInterest = viewModel.Debits.Sum();
                viewModel.TotalRepaid = viewModel.Credits.Sum();

                PieChart pieChart = new PieChart();
                pieChart.Data.AddRange(new List<SimpleData>
                {
                    new SimpleData
                    {
                        Value = (double)Math.Round(viewModel.TotalInterest, 2),
                        Label = "Interest",
                        Color = "rgba(220, 0, 0, 0.5)",
                    },
                    new SimpleData()
                    {
                        Value = (double)Math.Round(model.Amount, 2),
                        Label = "Capital",
                        Color = "rgba(0, 220, 0, 0.5)"
                    }
                });
                viewModel.pieChart = pieChart;
            }
            ViewBag.ShowResults = ShowResults;

            return PartialView(viewModel);
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
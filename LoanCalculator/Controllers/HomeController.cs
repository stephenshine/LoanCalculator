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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Result([Bind(Include = "Amount,APR,termInMonths")]Loan loan)
        {
            loan.CalculateMonthlyRepayment();
            loan.RepayLoan();
            return View(loan);
        }

    }
}
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

        public PartialViewResult Result(double Amount = 0, double APR = 0, double TermInMonths = 0)
        {
            Loan loan = new Loan(Amount, APR, TermInMonths);

            loan.RepayLoan();
            
            return PartialView(loan);
        }

    }
}
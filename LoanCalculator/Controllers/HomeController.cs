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
        public ActionResult Test()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult Result(decimal Amount = 0, decimal APR = 0, int TermInMonths = 0)
        {
            Loan loan = new Loan(Amount, APR, TermInMonths);
            if (loan.APR != 0)
            {
                loan.RepayLoan();
            }
            
            return PartialView(loan);
        }

    }
}
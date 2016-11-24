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

        // Modelbinding extracts values from loan passed to method
        public PartialViewResult Result(decimal Amount = 1000, decimal APR = 5, int TermInMonths = 12)
        {
            // LoanViewModel contains information to display about loan and chart
            LoanViewModel model = new LoanViewModel(new Loan(Amount, APR, TermInMonths));          
            return PartialView(model);
        }

    }
}
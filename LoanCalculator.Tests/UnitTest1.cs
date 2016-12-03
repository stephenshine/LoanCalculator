using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoanCalculator.Controllers;
using LoanCalculator.Models;
using System.Web.Mvc;

namespace LoanCalculator.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange
            Loan loan = new Loan(1000, 5, 12);
            HomeController target = new HomeController(loan);

            // Act
            PartialViewResult result = target.Result(loan.Amount, loan.APR, loan.TermInMonths);

            // Assert
            Assert.IsNotNull(result.Model);
            Assert.AreEqual(typeof(LoanViewModel), result.Model);

        }
    }
}

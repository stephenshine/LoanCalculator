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
            PartialViewResult result = target.Result(loan);

            // Assert
            Assert.IsNotNull(result.Model);
            Assert.AreEqual(85.61m, result.ViewData["MonthlyRepayment"]);
            Assert.AreEqual(27.30m, result.ViewData["TotalInterest"]);
        }

        [TestMethod]
        public void ModelState()
        {
            // Arrange
            Loan loan = new Loan(1000, 0, 12);
            HomeController target = new HomeController(loan);

            // Act
            PartialViewResult result = target.Result(loan);

            // Assert
            Assert.IsFalse(result.ViewData.ModelState.IsValid);
        }
    }
}

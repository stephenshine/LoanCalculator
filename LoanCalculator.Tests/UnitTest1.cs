using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LoanCalculator.Controllers;
using LoanCalculator.Models;
using System.Web;
using System.Web.Mvc;

namespace LoanCalculator.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestIndex()
        {
            // Arrange
            HomeController target = new HomeController();

            // Act
            var result = target.Index() as ViewResult;
            var loan = (Loan) result.ViewData.Model;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("", result.ViewName);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(Loan));
            Assert.AreEqual(0, loan.Amount);
            Assert.AreEqual(0, loan.APR);
            Assert.AreEqual(0, loan.TermInMonths);
        }

        [TestMethod]
        public void CalculationTest1()
        {
            // Arrange
            HomeController target = new HomeController();
            Loan loan = new Loan(1000, 5, 12);

            // Act
            var result = target.Result(loan) as PartialViewResult;
            var resultModel = (LoanResults) result.ViewData.Model;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(LoanResults));
            Assert.AreEqual(85.61m, resultModel.MonthlyRepayment);
            Assert.AreEqual(27.30m, resultModel.TotalInterest);
            Assert.AreEqual(12, resultModel.OpeningBalances.Length);
        }
    }
}

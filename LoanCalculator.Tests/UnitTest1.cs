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
        public void TestIndex()
        {
            // Arrange
            var controller = new HomeController();
            Loan loan = new Loan(0, 0, 0);

            // Act
            var result = (ViewResult) controller.Index();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(loan.GetType(), result.ViewData.Model.GetType());
        }

    }
}

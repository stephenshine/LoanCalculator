using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanCalculator.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public double StartingBalance { get; set; }
        public double ClosingBalance { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }

        public Transaction() { }
        public Transaction(int id, double balance)
        {
            TransactionID = id;
            StartingBalance = balance;
        }
    }
}
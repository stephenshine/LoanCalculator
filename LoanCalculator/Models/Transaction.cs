using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanCalculator.Models
{
    public class Transaction
    {
        public int TransactionID { get; set; }
        public decimal StartingBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }

        public Transaction() { }
        public Transaction(int id, decimal balance)
        {
            TransactionID = id;
            StartingBalance = balance;
        }
    }
}
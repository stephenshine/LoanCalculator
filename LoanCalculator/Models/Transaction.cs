using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoanCalculator.Models
{
    public class Transaction
    {
        public double TransactionID { get; set; }
        public double Balance { get; set; }
        public double Debit { get; set; }
        public double Credit { get; set; }
    }
}
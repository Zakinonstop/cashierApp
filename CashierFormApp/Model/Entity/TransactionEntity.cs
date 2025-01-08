using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierFormApp.Model.Entity
{
    public class TransactionEntity
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public int TransactionDetailId {  get; set; }
        public string Datetime { get; set; }
        public float TotalAmount {  get; set; }
        public float Paid { get; set; }
        public float Changed { get; set; }
        public int MemberId {  get; set; }
    }

    public class TransactionSummary
    {
        public int TotalTransactions { get; set; }
        public float TotalAmount { get; set; }
    }

    public class MonthlyTransactionSummary
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int TransactionCount { get; set; }
    }
}

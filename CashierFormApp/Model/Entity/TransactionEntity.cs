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
        public int TransactionDetailId {  get; set; }
        public float TotalAmount {  get; set; }
    }
}

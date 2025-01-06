using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierFormApp.Model.Entity
{
    public class TransactionDetailEntity
    {
        public int Urut { get; set; }
        public int TransactionDetailId { get; set; }
        public int ProductId { get; set; }
        public int Qty {  get; set; }
        public double Price {  get; set; }
        public string ProductName { get; set; }

        public string ProductCode { get; set; }
    }
}

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
        public int qty {  get; set; }
        public double price {  get; set; }
    }
}

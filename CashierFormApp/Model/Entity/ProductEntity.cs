using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierFormApp.Model.Entity
{
    public class ProductEntity
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name {  get; set; }
        public int Stock {  get; set; }
        public double Price { get; set; }
    }
}

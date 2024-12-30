using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierFormApp.Model.Entity
{
    internal class Inventory_log
    {
        public int inventory_log {  get; set; }
        public int change_id { get; set; }
        public int change_quantity { get; set; }
        public string mode { get; set; }
        public string datetime { get; set; }

    }
}

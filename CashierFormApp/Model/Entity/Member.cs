using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierFormApp.Model.Entity
{
    internal class Member
    {
        public int urut {  get; set; }
        public string nik { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public int count_shopping { get; set; }

    }
}

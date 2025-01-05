using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierFormApp.Model.Entity
{
    public class MemberEntity
    {
        public int MemberId {  get; set; }
        public string Nik { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Shopping { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierFormApp.Controller
{
    public class SessionController
    {
        private static SessionController _instance;
        public int UserId { get; set; }
        public string Username { get; set; }

        public int RoleId { get; set; }

        private SessionController() { }

        public static SessionController Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SessionController();
                }
                return _instance;
            }
        }

        public void Clear()
        {
            UserId = 0;
            Username = null;
        }
    }
}

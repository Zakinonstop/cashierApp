using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CashierFormApp.Model.Context;
using CashierFormApp.Model.Entity;
using CashierFormApp.Model.Repository;

namespace CashierFormApp.Controller
{
    internal class AuthController
    {
        private UserRepository _repository;

        public List<User> ReadByUsername(string username)
        {
            List<User> list = new List<User>();

            using (DbContext context = new DbContext())
            {
                _repository = new UserRepository(context);
                list = _repository.ReadByUsername(username);
            }
            return list;
        }
        public (bool, string) isLoginTrue(string username, string password)
        {
            var data = ReadByUsername(username);

            if (data.Any())
            {
                if (username == data[0].username && password == data[0].password)
                {
                    var Session = SessionController.Instance;

                    Session.Username = data[0].username;
                    Session.RoleId = data[0].role_id;

                    return (true, "Login successful");
                }
            }

            return (false, "Invalid username or password");
        }
    }
}

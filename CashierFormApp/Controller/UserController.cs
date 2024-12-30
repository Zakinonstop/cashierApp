using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashierFormApp.Model.Context;
using CashierFormApp.Model.Repository;
using CashierFormApp.Model.Entity;

namespace CashierFormApp.Controller
{
    internal class UserController
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
    }
}

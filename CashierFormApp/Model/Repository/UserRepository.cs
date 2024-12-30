using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CashierFormApp.Model.Context;
using CashierFormApp.Model.Entity;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;

namespace CashierFormApp.Model.Repository
{
    internal class UserRepository
    {
        private MySqlConnection _conn;

        public UserRepository(DbContext context)
        {
            _conn = context.Conn;
        }

        public List<User> ReadByUsername(string username)
        {
            List<User> List = new List<User>();

            try
            {
                string sql = @"SELECT * FROM `user` WHERE username = @username";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User();
                            user.username = reader["username"].ToString();
                            user.password = reader["password"].ToString();
                            user.role_id = reader.GetInt32(3);

                            List.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadByUsername error: {0}", ex.Message);
            }

            return List;
        }
    }
}

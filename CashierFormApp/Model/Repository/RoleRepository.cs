using CashierFormApp.Model.Context;
using CashierFormApp.Model.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierFormApp.Model.Repository
{
    internal class RoleRepository
    {
        private MySqlConnection _conn;
        public RoleRepository(DbContext context)
        {
            _conn = context.Conn;
        }
        public int Create(Role role)
        {
            int result = 0;

            string sql = @"INSERT INTO role (name) values (@name)";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@name", role.name);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }
            return result;
        }

        public int Delete(Role role)
        {
            int result = 0;

            string sql = @"DELETE FROM role WHERE `role`.`role_id` = @role_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@role_id", role.role_id);
                try
                {
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Delete error: {0}", ex.Message);
                }
            }
            return result;
        }




    }
}

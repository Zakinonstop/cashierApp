using CashierFormApp.Model.Context;
using CashierFormApp.Model.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
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

            string sql = @"insert into role (name) values (@name)";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                // mendaftarkan parameter dan mengeset nilainya
                cmd.Parameters.AddWithValue("@name", role.name);
                try
                {
                    // jalankan perintah INSERT dan tampung hasilnya ke dalam variabel result
                    result = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.Print("Create error: {0}", ex.Message);
                }
            }
            return result;
        }
    }
}

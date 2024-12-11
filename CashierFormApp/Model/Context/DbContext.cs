using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using MySql.Data.MySqlClient;

using MySqlX.XDevAPI;
using System.Windows.Forms;


namespace CashierFormApp.Model.Context
{
    public class DbContext : IDisposable
    {
        private MySqlConnection _conn;

        public MySqlConnection Conn
        {
            get { return _conn ?? (_conn = GetMySqlConnection()); }
        }

        public MySqlConnection GetMySqlConnection()
        {
            MySqlConnection conn = null;

            try
            {
                string myConnectionString = "server=127.0.0.1;uid=root;pwd=;database=cashierdb";
                conn = new MySqlConnection(myConnectionString);
                conn.Open();
            }
            catch (Exception ex)
            {
                // Log to a file or external logging system
                System.Diagnostics.Debug.Print("Open Connection Error: {0}", ex.Message);
                throw new InvalidOperationException("Failed to connect to the database.", ex); // Optionally throw exception
            }

            return conn;
        }


        // Method ini digunakan untuk menghapus objek koneksi dari memory ketika sudah tidak digunakan
        public void Dispose()
        {
            if (_conn != null)
            {
                try
                {
                    if (_conn.State != ConnectionState.Closed) _conn.Close();
                }
                finally
                {
                    _conn.Dispose();
                }
            }
            GC.SuppressFinalize(this);
        }
    }
}

using CashierFormApp.Model.Context;
using CashierFormApp.Model.Entity;
using CashierFormApp.Views.Components;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashierFormApp.Model.Repository
{
    internal class TransactionRepository
    {
        private MySqlConnection _conn;
        public TransactionRepository(DbContext context)
        {
            _conn = context.Conn;
        }
        public int Create(TransactionEntity transaction)
        {
            int result = 0;

            string sql = @"INSERT INTO `transaction` (`user_id`, `transaction_detail_id`, `total_amount`, `member_id`) 
                                VALUES (@userId, @transactionDetailId, @totalAmount, @memberId)";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@transactionDetailId", transaction.TransactionDetailId);
                cmd.Parameters.AddWithValue("@totalAmount", transaction.TotalAmount);
                cmd.Parameters.AddWithValue("@userId", transaction.UserId);
                cmd.Parameters.AddWithValue("@memberId", transaction.MemberId);

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
        public int Update(TransactionEntity transaction)
        {
            int result = 0;

            string sql = @"UPDATE `transaction` 
                            SET `nik` = @nik, 
                                `name` = @name, 
                                `shopping` = @shopping, 
                                `address` = @address 
                            WHERE `transaction`.`transaction_id` = @transaction_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                //cmd.Parameters.AddWithValue("@transaction_id", transaction.transactionId);
                //cmd.Parameters.AddWithValue("@nik", transaction.Nik);
                //cmd.Parameters.AddWithValue("@name", transaction.Name);
                //cmd.Parameters.AddWithValue("@shopping", transaction.Shopping);
                //cmd.Parameters.AddWithValue("@address", transaction.Address);
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
        public List<TransactionEntity> ReadAll()
        {
            List<TransactionEntity> list = new List<TransactionEntity>();

            try
            {
                string sql = @"SELECT * FROM `transaction`";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            TransactionEntity transaction = new TransactionEntity();
                            //transaction.transactionId = Convert.ToInt32(dtr["transaction_id"]);
                            //transaction.Name = dtr["name"].ToString();
                            //transaction.Nik = dtr["nik"].ToString();
                            //transaction.Address = dtr["address"].ToString();
                            //transaction.Shopping = Convert.ToInt32(dtr["shopping"]);

                            list.Add(transaction);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }

            return list;
        }

        public List<TransactionEntity> GetMaxTransactionDetailId()
        {
            List<TransactionEntity> list = new List<TransactionEntity>();

            try
            {
                string sql = @"SELECT MAX(transaction_detail_id) as transaction_detail_id FROM `transaction`";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            TransactionEntity transaction = new TransactionEntity();
                            transaction.TransactionDetailId = Convert.ToInt32(dtr["transaction_detail_id"]);
                            list.Add(transaction);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }

            return list;
        }
        public List<TransactionEntity> ReadByAnything(string keyword)
        {
            List<TransactionEntity> list = new List<TransactionEntity>();

            try
            {
                string sql = @"SELECT * FROM `transaction` 
                                WHERE code LIKE @keyword OR
                                name LIKE @keyword OR
                                stock LIKE @keyword OR
                                price LIKE @keyword  
                               ORDER BY `transaction`.`name` ASC";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", string.Format("%{0}%", keyword));
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            TransactionEntity transaction = new TransactionEntity();
                            //transaction.transactionId = Convert.ToInt32(dtr["transaction_id"]);
                            //transaction.Name = dtr["name"].ToString();
                            //transaction.Nik = dtr["nik"].ToString();
                            //transaction.Shopping = Convert.ToInt32(dtr["shopping"]);

                            list.Add(transaction);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
            }

            return list;
        }
        public int Delete(TransactionEntity transaction)
        {
            int result = 0;

            string sql = @"DELETE FROM transaction WHERE `transaction`.`transaction_id` = @transaction_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                //cmd.Parameters.AddWithValue("@transaction_id", transaction.transactionId);
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

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

            string sql = @"INSERT INTO `transaction` (`user_id`, `transaction_detail_id`, `datetime`, `total_amount`, `paid`, `changed`, `member_id`) 
                                VALUES (@userId, @transactionDetailId, @datetime, @totalAmount, @paid, @changed, @memberId)";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@userId", transaction.UserId);
                cmd.Parameters.AddWithValue("@transactionDetailId", transaction.TransactionDetailId);
                cmd.Parameters.AddWithValue("@datetime", transaction.Datetime);
                cmd.Parameters.AddWithValue("@totalAmount", transaction.TotalAmount);
                cmd.Parameters.AddWithValue("@paid", transaction.Paid);
                cmd.Parameters.AddWithValue("@changed", transaction.Changed);
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
                string sql = @"SELECT user.username, transaction.* FROM `transaction`
                                JOIN user ON user.user_id = transaction.user_id";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            TransactionEntity transaction = new TransactionEntity();
                            transaction.TransactionId = Convert.ToInt32(dtr["transaction_id"]);
                            transaction.UserId = Convert.ToInt32(dtr["user_id"]);
                            transaction.Username = dtr["username"].ToString();
                            transaction.TransactionDetailId = Convert.ToInt32(dtr["transaction_detail_id"]);
                            transaction.Datetime = dtr["datetime"].ToString();
                            transaction.TotalAmount = Convert.ToSingle( dtr["total_amount"] );
                            transaction.Paid = Convert.ToSingle(dtr["paid"]);
                            transaction.Changed = Convert.ToSingle(dtr["changed"]);
                            transaction.MemberId = Convert.ToInt32(dtr["member_id"]);

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
                string sql = @"SELECT user.username, transaction.* FROM `transaction` 
                                JOIN user ON user.user_id = transaction.user_id
                                WHERE transaction.user_id LIKE @keyword OR
                                    transaction.transaction_detail_id LIKE @keyword OR
                                    transaction.datetime LIKE @keyword OR
                                    transaction.paid LIKE @keyword OR
                                    transaction.changed LIKE @keyword OR
                                    transaction.member_id LIKE @keyword OR
                                    transaction.total_amount LIKE @keyword  
                               ORDER BY `transaction`.`transaction_id` ASC";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", string.Format("%{0}%", keyword));
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            TransactionEntity transaction = new TransactionEntity();
                            transaction.TransactionId = Convert.ToInt32(dtr["transaction_id"]);
                            transaction.UserId = Convert.ToInt32(dtr["user_id"]);
                            transaction.Username = dtr["username"].ToString();
                            transaction.TransactionDetailId = Convert.ToInt32(dtr["transaction_detail_id"]);
                            transaction.Datetime = dtr["datetime"].ToString();
                            transaction.TotalAmount = Convert.ToSingle(dtr["total_amount"]);
                            transaction.Paid = Convert.ToSingle(dtr["paid"]);
                            transaction.Changed = Convert.ToSingle(dtr["changed"]);
                            transaction.MemberId = Convert.ToInt32(dtr["member_id"]);

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

        public TransactionSummary GetMonthlySummary()
        {
            TransactionSummary summary = new TransactionSummary();

            try
            {
                string sql = @"SELECT 
                                COUNT(*) AS total_transactions,
                                SUM(total_amount) AS Total_Amount
                                FROM transaction
                                WHERE MONTH(datetime) = MONTH(CURRENT_DATE()) 
                                  AND YEAR(datetime) = YEAR(CURRENT_DATE());";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        if (dtr.Read())
                        {
                            summary.TotalTransactions = Convert.ToInt32(dtr["total_transactions"]);
                            summary.TotalAmount = Convert.ToSingle(dtr["Total_Amount"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("GetMonthlySummary error: {0}", ex.Message);
            }

            return summary;
        }
        public List<MonthlyTransactionSummary> GetMonthlyTransactionCounts()
        {
            List<MonthlyTransactionSummary> summaryList = new List<MonthlyTransactionSummary>();

            try
            {
                string sql = @"
                            SELECT 
                                YEAR(datetime) AS year,
                                MONTH(datetime) AS month,
                                COUNT(*) AS transaction_count
                            FROM transaction
                            GROUP BY YEAR(CURRENT_DATE()), MONTH(datetime)
                            ORDER BY year DESC, month DESC;";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            MonthlyTransactionSummary summary = new MonthlyTransactionSummary
                            {
                                Year = Convert.ToInt32(dtr["year"]),
                                Month = Convert.ToInt32(dtr["month"]),
                                TransactionCount = Convert.ToInt32(dtr["transaction_count"])
                            };

                            summaryList.Add(summary);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.Print("GetMonthlyTransactionCounts error: {0}", ex.Message);
            }

            return summaryList;
        }
    }
}

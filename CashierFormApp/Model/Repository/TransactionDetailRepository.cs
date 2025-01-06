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
    internal class TransactionDetailRepository
    {
        private MySqlConnection _conn;
        public TransactionDetailRepository(DbContext context)
        {
            _conn = context.Conn;
        }
        public int Create(int transactionDetailId, int productId, int qty, float price)
        {
            int result = 0;

            string sql = @"INSERT INTO `transaction_detail` (`transaction_detail_id`, `product_id`, `qty`, `price`) 
                                          VALUES (@transactionDetailId, @productId, @qty, @price)";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@transactionDetailId", transactionDetailId);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@qty", qty);
                cmd.Parameters.AddWithValue("@price", price);
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
        public int Update(TransactionDetailEntity transactionDetail)
        {
            int result = 0;

            string sql = @"UPDATE `transactionDetail` 
                            SET `nik` = @nik, 
                                `name` = @name, 
                                `shopping` = @shopping, 
                                `address` = @address 
                            WHERE `transactionDetail`.`transactionDetail_id` = @transactionDetail_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                //cmd.Parameters.AddWithValue("@transactionDetail_id", transactionDetail.transactionDetailId);
                //cmd.Parameters.AddWithValue("@nik", transactionDetail.Nik);
                //cmd.Parameters.AddWithValue("@name", transactionDetail.Name);
                //cmd.Parameters.AddWithValue("@shopping", transactionDetail.Shopping);
                //cmd.Parameters.AddWithValue("@address", transactionDetail.Address);
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
        public List<TransactionDetailEntity> ReadAll()
        {
            List<TransactionDetailEntity> list = new List<TransactionDetailEntity>();

            try
            {
                string sql = @"SELECT * FROM `transactionDetail`";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            TransactionDetailEntity transactionDetail = new TransactionDetailEntity();
                            //transactionDetail.transactionDetailId = Convert.ToInt32(dtr["transactionDetail_id"]);
                            //transactionDetail.Name = dtr["name"].ToString();
                            //transactionDetail.Nik = dtr["nik"].ToString();
                            //transactionDetail.Address = dtr["address"].ToString();
                            //transactionDetail.Shopping = Convert.ToInt32(dtr["shopping"]);

                            list.Add(transactionDetail);
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

        //public List<TransactionDetailEntity> GetMaxtransactionDetailId()
        //{
        //    List<TransactionDetailEntity> list = new List<TransactionDetailEntity>();

        //    try
        //    {
        //        string sql = @"SELECT MAX(transactionDetail_detail_id) as transactionDetail_detail_id FROM `transactionDetail`";

        //        using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
        //        {
        //            using (MySqlDataReader dtr = cmd.ExecuteReader())
        //            {
        //                while (dtr.Read())
        //                {
        //                    TransactionDetailEntity transactionDetail = new TransactionDetailEntity();
        //                    transactionDetail.transactionDetailId = Convert.ToInt32(dtr["transaction_detail_id"]);
        //                    list.Add(transactionDetail);
        //                }
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.Print("ReadAll error: {0}", ex.Message);
        //    }

        //    return list;
        //}
        public List<TransactionDetailEntity> ReadByAnything(string keyword)
        {
            List<TransactionDetailEntity> list = new List<TransactionDetailEntity>();

            try
            {
                string sql = @"SELECT * FROM `transactionDetail` 
                                WHERE code LIKE @keyword OR
                                name LIKE @keyword OR
                                stock LIKE @keyword OR
                                price LIKE @keyword  
                               ORDER BY `transactionDetail`.`name` ASC";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", string.Format("%{0}%", keyword));
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            TransactionDetailEntity transactionDetail = new TransactionDetailEntity();
                            //transactionDetail.transactionDetailId = Convert.ToInt32(dtr["transactionDetail_id"]);
                            //transactionDetail.Name = dtr["name"].ToString();
                            //transactionDetail.Nik = dtr["nik"].ToString();
                            //transactionDetail.Shopping = Convert.ToInt32(dtr["shopping"]);

                            list.Add(transactionDetail);
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

        public List<TransactionDetailEntity> ReadByTransactionDetailId(int transactionDetailId)
        {
            List<TransactionDetailEntity> list = new List<TransactionDetailEntity>();

            try
            {
                string sql = @"SELECT transaction_detail_id, 
                                        product.name, 
                                        SUM(transaction_detail.qty) AS qty, 
                                        SUM(transaction_detail.price) AS sub_total 
                                FROM `transaction_detail`
                                JOIN product ON product.product_id = transaction_detail.product_id
                                WHERE transaction_detail.transaction_detail_id = @transaction_detail_id

                                GROUP BY transaction_detail_id, transaction_detail.product_id";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@transaction_detail_id", string.Format("{0}", transactionDetailId));
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            TransactionDetailEntity transactionDetail = new TransactionDetailEntity();
                            transactionDetail.TransactionDetailId = Convert.ToInt32(dtr["transaction_detail_id"]);
                            transactionDetail.Qty = Convert.ToInt32(dtr["qty"]);
                            transactionDetail.ProductName = dtr["name"].ToString();
                            transactionDetail.Price = Convert.ToInt32(dtr["sub_total"]);    

                            list.Add(transactionDetail);
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

        public List<TransactionDetailEntity> ReadDetailByTransactionDetailId(int transactionDetailId)
        {
            List<TransactionDetailEntity> list = new List<TransactionDetailEntity>();

            try
            {
                string sql = @"SELECT product.name, product.code, transaction_detail.* 
                                FROM `transaction_detail`
                                JOIN product ON product.product_id = transaction_detail.product_id
                                WHERE transaction_detail_id = 1";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@transaction_detail_id", string.Format("{0}", transactionDetailId));
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            TransactionDetailEntity transactionDetail = new TransactionDetailEntity();
                            transactionDetail.TransactionDetailId = Convert.ToInt32(dtr["transaction_detail_id"]);
                            transactionDetail.Qty = Convert.ToInt32(dtr["qty"]);
                            transactionDetail.ProductCode = dtr["code"].ToString();
                            transactionDetail.ProductName = dtr["name"].ToString();
                            transactionDetail.Price = Convert.ToInt32(dtr["price"]);
                            transactionDetail.Urut = Convert.ToInt32(dtr["urut"]);

                            list.Add(transactionDetail);
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
        public int Delete(TransactionDetailEntity transactionDetail)
        {
            int result = 0;

            string sql = @"DELETE FROM transaction_detail WHERE `transaction_detail`.`urut` = @urut";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@urut", transactionDetail.Urut);
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

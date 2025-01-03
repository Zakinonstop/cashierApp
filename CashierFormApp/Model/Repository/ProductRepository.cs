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
    internal class ProductRepository
    {
        private MySqlConnection _conn;
        public ProductRepository(DbContext context)
        {
            _conn = context.Conn;
        }
        public int Create(ProductEntity product)
        {
            int result = 0;

            string sql = @"INSERT INTO `product` (`code`, `name`, `stock`, `price`) 
                                          VALUES (@code, @name, @stock, @price)";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@code", product.Code);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@stock", product.Stock);
                cmd.Parameters.AddWithValue("@price", product.Price);
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
        public int Update(ProductEntity product)
        {
            int result = 0;

            string sql = @"UPDATE `product` 
                            SET `code` = @code, 
                                `name` = @name, 
                                `stock` = @stock, 
                                `price` = @price 
                            WHERE `product`.`product_id` = @product_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@product_id", product.ProductId);
                cmd.Parameters.AddWithValue("@code", product.Code);
                cmd.Parameters.AddWithValue("@name", product.Name);
                cmd.Parameters.AddWithValue("@stock", product.Stock);
                cmd.Parameters.AddWithValue("@price", product.Price);
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

        public int Delete(ProductEntity product)
        {
            int result = 0;

            string sql = @"DELETE FROM product WHERE `product`.`product_id` = @product_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@product_id", product.ProductId);
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

        public List<ProductEntity> ReadAll()
        {
            List<ProductEntity> list = new List<ProductEntity>();

            try
            {
                string sql = @"SELECT * FROM `product` ORDER BY `product`.`name` ASC";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            ProductEntity product = new ProductEntity();
                            product.ProductId = Convert.ToInt32(dtr["product_id"]);
                            product.Name = dtr["name"].ToString();
                            product.Code = dtr["code"].ToString();
                            product.Stock = Convert.ToInt32(dtr["stock"]);
                            product.Price = Convert.ToSingle(dtr["price"]);

                            list.Add(product);
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
    }
}

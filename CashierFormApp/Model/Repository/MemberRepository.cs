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
    internal class MemberRepository
    {
        private MySqlConnection _conn;
        public MemberRepository(DbContext context)
        {
            _conn = context.Conn;
        }
        public int Create(MemberEntity member)
        {
            int result = 0;

            string sql = @"INSERT INTO `member` (`nik`, `name`, `shopping`, `address`) 
                                          VALUES (@nik, @name, @shopping, @address)";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@nik", member.Nik);
                cmd.Parameters.AddWithValue("@name", member.Name);
                cmd.Parameters.AddWithValue("@shopping", member.Shopping);
                cmd.Parameters.AddWithValue("@address", member.Address);
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
        public int Update(MemberEntity member)
        {
            int result = 0;

            string sql = @"UPDATE `member` 
                            SET `nik` = @nik, 
                                `name` = @name, 
                                `shopping` = @shopping, 
                                `address` = @address 
                            WHERE `member`.`member_id` = @member_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@member_id", member.MemberId);
                cmd.Parameters.AddWithValue("@nik", member.Nik);
                cmd.Parameters.AddWithValue("@name", member.Name);
                cmd.Parameters.AddWithValue("@shopping", member.Shopping);
                cmd.Parameters.AddWithValue("@address", member.Address);
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
        public List<MemberEntity> ReadAll()
        {
            List<MemberEntity> list = new List<MemberEntity>();

            try
            {
                string sql = @"SELECT * FROM `member`";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            MemberEntity member = new MemberEntity();
                            member.MemberId = Convert.ToInt32(dtr["member_id"]);
                            member.Name = dtr["name"].ToString();
                            member.Nik = dtr["nik"].ToString();
                            member.Address = dtr["address"].ToString();
                            member.Shopping = Convert.ToInt32(dtr["shopping"]);

                            list.Add(member);
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
        public List<MemberEntity> ReadByAnything(string keyword)
        {
            List<MemberEntity> list = new List<MemberEntity>();

            try
            {
                string sql = @"SELECT * FROM `member` 
                                WHERE code LIKE @keyword OR
                                name LIKE @keyword OR
                                stock LIKE @keyword OR
                                price LIKE @keyword  
                               ORDER BY `member`.`name` ASC";

                using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
                {
                    cmd.Parameters.AddWithValue("@keyword", string.Format("%{0}%", keyword));
                    using (MySqlDataReader dtr = cmd.ExecuteReader())
                    {
                        while (dtr.Read())
                        {
                            MemberEntity member = new MemberEntity();
                            member.MemberId = Convert.ToInt32(dtr["member_id"]);
                            member.Name = dtr["name"].ToString();
                            member.Nik = dtr["nik"].ToString();
                            member.Shopping = Convert.ToInt32(dtr["shopping"]);

                            list.Add(member);
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
        public int Delete(MemberEntity member)
        {
            int result = 0;

            string sql = @"DELETE FROM member WHERE `member`.`member_id` = @member_id";

            using (MySqlCommand cmd = new MySqlCommand(sql, _conn))
            {
                cmd.Parameters.AddWithValue("@member_id", member.MemberId);
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

using LaptopShopApplication.Repository;
using layout.domain;
using layout.luong;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace layout.repository
{
    public class SalaryRepository
    {
        MssSQLConnection conn = new MssSQLConnection();

        public DataTable getAllPayRoll()
        {
            DataTable data = new DataTable();

            try
            {
                using (SqlConnection connection = conn.dbConnection())
                {
                    connection.Open();
                    string sql = @"SELECT payroll_id, payroll.user_id, full_name, pos_name, base_salary, net_salary
                    FROM payroll 
                    JOIN users on users.user_id = payroll.user_id
                    JOIN positions on users.pos_id = positions.pos_id
                    ORDER BY payroll_id ASC";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                    {
                        adapter.Fill(data);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi getAllPayRoll: " + ex.Message, "Lỗi DB");
            }

            return data;
        }


        public Payroll getPayRollById(int payrollId)
        {
            Payroll payroll = new Payroll();
            try
            {
                using (SqlConnection connection = conn.dbConnection())
                {
                    connection.Open();
                    string sql = "Select payroll_id, user_id ,allowance, bonus, deduction, net_salary, month, year from payroll " +
                                 "where payroll_id = @id";

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@id", payrollId);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            payroll.id = reader["payroll_id"] != DBNull.Value ? Convert.ToInt32(reader["payroll_id"]) : 0;
                            payroll.userId = reader["user_id"] != DBNull.Value ? Convert.ToInt32(reader["user_id"]) : 0;
                            payroll.allowance = reader["allowance"] != DBNull.Value ? Convert.ToSingle(reader["allowance"]) : 0;
                            payroll.bonus = reader["bonus"] != DBNull.Value ? Convert.ToSingle(reader["bonus"]) : 0;
                            payroll.deduction = reader["deduction"] != DBNull.Value ? Convert.ToSingle(reader["deduction"]) : 0;
                            payroll.netSalary = reader["net_salary"] != DBNull.Value ? Convert.ToSingle(reader["net_salary"]) : 0;
                            payroll.month = reader["month"] != DBNull.Value ? Convert.ToInt32(reader["month"]) : 0;
                            payroll.year = reader["year"] != DBNull.Value ? Convert.ToInt32(reader["year"]) : 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi getPayRollById: " + ex.Message, "Lỗi DB");
            }
            return payroll;
        }

        public bool UpdateSalary(Payroll payroll)
        {
            try
            {
                using (SqlConnection connection = conn.dbConnection())
                {
                    connection.Open();
                    string sql = @"UPDATE payroll SET allowance = @Allowance, bonus = @Bonus,
                                   deduction = @Deduction, net_salary  = @net
                                   WHERE payroll_id = @id ";

                    SqlCommand cmd = new SqlCommand(sql, connection);

                    cmd.Parameters.AddWithValue("@Allowance", payroll.allowance);
                    cmd.Parameters.AddWithValue("@Bonus", payroll.bonus);
                    cmd.Parameters.AddWithValue("@Deduction", payroll.deduction);
                    cmd.Parameters.AddWithValue("@net", payroll.netSalary);
                    cmd.Parameters.AddWithValue("@id", payroll.id);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi khi update : " + ex.Message, "Lỗi DB");
                return false;
            }
        }
        public void createSalary(Payroll payroll)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();

                string checkSql = "select count(*) from payroll where user_id = @id and month = @month and year = @year";
                SqlCommand checkCmd = new SqlCommand(checkSql, connection);
                checkCmd.Parameters.AddWithValue("@id", payroll.userId);
                checkCmd.Parameters.AddWithValue("@month", payroll.month);
                checkCmd.Parameters.AddWithValue("@year", payroll.year);
                int existed = (int)checkCmd.ExecuteScalar();
                if (existed > 0)
                {
                    return;
                }

                string sql = "insert into payroll (user_id, allowance, bonus, deduction, net_salary, month, year) " +
                    "values (@id, @allow, @bonus, @deduct, @net, @month, @year)";

                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@id", payroll.userId);
                cmd.Parameters.AddWithValue("@allow", payroll.allowance);
                cmd.Parameters.AddWithValue("@bonus", payroll.bonus);
                cmd.Parameters.AddWithValue("@deduct", payroll.deduction);
                cmd.Parameters.AddWithValue("@net", payroll.netSalary);
                cmd.Parameters.AddWithValue("@month", payroll.month);
                cmd.Parameters.AddWithValue("@year", payroll.year);

                cmd.ExecuteNonQuery();
            }
        }

        public DataTable getCurrentMonthAndYearPayRoll(int userId)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Select payroll_id, user_id ,allowance, bonus, deduction, net_salary from payroll " +
                             "where user_id = @id and month = @month and year = @year";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@id", userId);
                adapter.SelectCommand.Parameters.AddWithValue("@month", DateTime.Now.Month);
                adapter.SelectCommand.Parameters.AddWithValue("@year", DateTime.Now.Year);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
        }
        public bool isPayRollWithCurrentMonthAndYearExisted(int userId)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Select payroll_id from payroll " +
                             "where user_id = @id and month = @month and year = @year";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", userId);
                cmd.Parameters.AddWithValue("@month", DateTime.Now.Month);
                cmd.Parameters.AddWithValue("@year", DateTime.Now.Year);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    return true;
                }
            }
            return false;
        }

        public float getTotalSalaryCurrentMonth()
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Select ISNULL(SUM(net_salary), 0) from payroll where month = @month and year = @year";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@month", DateTime.Now.Month);
                cmd.Parameters.AddWithValue("@year", DateTime.Now.Year);

                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    return Convert.ToSingle(rs);
                }
            }

            return 0;
        }
    }
}
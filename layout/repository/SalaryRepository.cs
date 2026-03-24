    using LaptopShopApplication.Repository;
using layout.luong;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout.repository
{
    public class SalaryRepository
    {
        MssSQLConnection conn = new MssSQLConnection();

        public List<SalaryDay.Luong> getAllSalary()
        {
            List<SalaryDay.Luong> salaryList = new List<SalaryDay.Luong>();

            try
            {
                using (SqlConnection connection = conn.dbConnection())
                {
                    connection.Open();
                    string sql = @"
                SELECT 
                    p.payroll_id   AS PayrollId,
                    p.user_id      AS UserId,
                    u.full_name    AS FullName,
                    p.base_salary  AS BaseSalary,
                    p.allowance    AS Allowance,
                    p.bonus        AS Bonus,
                    p.deduction    AS Deduction,
                    p.month        AS Month,
                    p.year         AS Year,
                    p.net_salary   AS NetSalary
                FROM payroll p
                INNER JOIN users u ON u.user_id = p.user_id
                ORDER BY p.payroll_id ASC";

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SalaryDay.Luong salary = new SalaryDay.Luong
                        {
                            PayrollId = reader["PayrollId"] != DBNull.Value ? Convert.ToInt32(reader["PayrollId"]) : 0,
                            UserId = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"]) : 0,
                            FullName = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : "N/A",
                            BaseSalary = reader["BaseSalary"] != DBNull.Value ? Convert.ToDouble(reader["BaseSalary"]) : 0,
                            Allowance = reader["Allowance"] != DBNull.Value ? Convert.ToDouble(reader["Allowance"]) : 0,
                            Bonus = reader["Bonus"] != DBNull.Value ? Convert.ToDouble(reader["Bonus"]) : 0,
                            Deduction = reader["Deduction"] != DBNull.Value ? Convert.ToDouble(reader["Deduction"]) : 0,
                            Month = reader["Month"] != DBNull.Value ? Convert.ToInt32(reader["Month"]) : 0,
                            Year = reader["Year"] != DBNull.Value ? Convert.ToInt32(reader["Year"]) : 0,
                            NetSalary = reader["NetSalary"] != DBNull.Value ? Convert.ToDouble(reader["NetSalary"]) : 0
                        };
                        salaryList.Add(salary);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi getAllSalary: " + ex.Message, "Lỗi DB");
            }

            return salaryList;
        }
        public bool UpdateSalary(int id, double luongCoBan, double troCap, double thuong, double khoanTru)
        {
            try
            {
                using (SqlConnection connection = conn.dbConnection())
                {
                    connection.Open();
                    string sql = @"UPDATE payroll 
                           SET base_salary = @LuongCoBan, 
                               allowance   = @TroCap, 
                               bonus       = @Thuong,
                               deduction   = @KhoanTru
                           WHERE payroll_id = @Id";

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@LuongCoBan", luongCoBan);
                    cmd.Parameters.AddWithValue("@TroCap", troCap);
                    cmd.Parameters.AddWithValue("@Thuong", thuong);
                    cmd.Parameters.AddWithValue("@KhoanTru", khoanTru);
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi UpdateSalary: " + ex.Message, "Lỗi DB");
                return false;
            }
        }
    }
}

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

        public List<SalaryCal.Luong> getAllSalary()
        {
            List<SalaryCal.Luong> salaryList = new List<SalaryCal.Luong>();

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
                    pos.pos_name     AS PosName,
                    pos.base_salary  AS BaseSalary,
                    p.allowance    AS Allowance,
                    p.bonus        AS Bonus,
                    p.deduction    AS Deduction,
                    p.month        AS Month,
                    p.year         AS Year,
                    p.net_salary   AS NetSalary
                FROM payroll p
                INNER JOIN users u ON u.user_id = p.user_id
                LEFT  JOIN positions pos ON pos.pos_id = u.pos_id
                ORDER BY p.payroll_id ASC";

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        salaryList.Add(new SalaryCal.Luong
                        {
                            PayrollId = reader["PayrollId"] != DBNull.Value ? Convert.ToInt32(reader["PayrollId"]) : 0,
                            UserId = reader["UserId"] != DBNull.Value ? Convert.ToInt32(reader["UserId"]) : 0,
                            FullName = reader["FullName"] != DBNull.Value ? reader["FullName"].ToString() : "N/A",
                            PosName = reader["PosName"] != DBNull.Value ? reader["PosName"].ToString() : "N/A",
                            BaseSalary = reader["BaseSalary"] != DBNull.Value ? Convert.ToDouble(reader["BaseSalary"]) : 0,
                            Allowance = reader["Allowance"] != DBNull.Value ? Convert.ToDouble(reader["Allowance"]) : 0,
                            Bonus = reader["Bonus"] != DBNull.Value ? Convert.ToDouble(reader["Bonus"]) : 0,
                            Deduction = reader["Deduction"] != DBNull.Value ? Convert.ToDouble(reader["Deduction"]) : 0,
                            Month = reader["Month"] != DBNull.Value ? Convert.ToInt32(reader["Month"]) : 0,
                            Year = reader["Year"] != DBNull.Value ? Convert.ToInt32(reader["Year"]) : 0,
                            NetSalary = reader["NetSalary"] != DBNull.Value ? Convert.ToDouble(reader["NetSalary"]) : 0,
                        });
                    }
                    reader.Close();

                    foreach (SalaryCal.Luong salary in salaryList)
                    {
                        salary.Attendances = GetAttendanceByUserAndPeriod(connection, salary.UserId, salary.Month, salary.Year);
                        salary.Calculate();
                        UpdateCalculatedSalary(connection, salary);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Lỗi getAllSalary: " + ex.Message, "Lỗi DB");
            }

            return salaryList;
        }

        private List<AttendanceAdminRecord> GetAttendanceByUserAndPeriod(SqlConnection connection, int userId, int month, int year)
        {
            List<AttendanceAdminRecord> attendances = new List<AttendanceAdminRecord>();

            string sql = @"SELECT check_in, check_out, loai_ca
                           FROM attendance
                           WHERE user_id = @uid
                             AND MONTH(check_in) = @month
                             AND YEAR(check_in) = @year";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add("@uid", SqlDbType.Int).Value = userId;
                cmd.Parameters.Add("@month", SqlDbType.Int).Value = month;
                cmd.Parameters.Add("@year", SqlDbType.Int).Value = year;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        attendances.Add(new AttendanceAdminRecord
                        {
                            check_in = (DateTime)reader["check_in"],
                            check_out = reader["check_out"] as DateTime?,
                            loai_ca = reader["loai_ca"] == DBNull.Value ? null : reader["loai_ca"].ToString()
                        });
                    }
                }
            }

            return attendances;
        }

        private void UpdateCalculatedSalary(SqlConnection connection, SalaryCal.Luong salary)
        {
            string sql = @"UPDATE payroll
                           SET deduction = @deduction,
                               net_salary = @net
                           WHERE payroll_id = @id";

            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add("@deduction", SqlDbType.Float).Value = salary.Deduction;
                cmd.Parameters.Add("@net", SqlDbType.Float).Value = salary.NetSalary;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = salary.PayrollId;
                cmd.ExecuteNonQuery();
            }
        }

        public bool UpdateSalary(int id, double allowance, double bonus, double deduction)
        {
            try
            {
                using (SqlConnection connection = conn.dbConnection())
                {
                    connection.Open();
                    string sql = @"UPDATE payroll 
                           SET allowance   = @Allowance, 
                               bonus       = @Bonus,
                               deduction   = @Deduction,
                               net_salary  = ISNULL((
                                    SELECT pos.base_salary 
                                    FROM users u 
                                    INNER JOIN positions pos ON pos.pos_id = u.pos_id
                                    WHERE u.user_id = (SELECT user_id FROM payroll WHERE payroll_id = @Id)
                                    ), 0) + @Allowance + @Bonus - @Deduction
                           WHERE payroll_id = @Id";

                    SqlCommand cmd = new SqlCommand(sql, connection);

                    cmd.Parameters.AddWithValue("@Allowance", allowance);
                    cmd.Parameters.AddWithValue("@Bonus", bonus);
                    cmd.Parameters.AddWithValue("@Deduction", deduction);
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
    }
}
using LaptopShopApplication.Repository;
using layout.domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.RightsManagement;

namespace layout.repository
{
    public class AttendanceRepository
    {
        private readonly MssSQLConnection conn = new MssSQLConnection();

        public DataTable getAllAttendanceForAdmin()
        {
            DataTable list = new DataTable();

            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = @"SELECT u.full_name, a.check_in, a.check_out, a.shift_type, a.status
                               FROM attendance a
                               INNER JOIN users u ON a.user_id = u.user_id
                               ORDER BY a.check_in DESC";

                using (SqlDataAdapter adapter = new SqlDataAdapter(sql, connection))
                {
                    adapter.Fill(list);
                }
            }

            return list;
        }

        public List<Attendance> getTodayAttendanceByUser(int userId)
        {
            List<Attendance> list = new List<Attendance>();

            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = @"SELECT attendance_id, check_in, check_out, shift_type, status
                               FROM attendance
                               WHERE user_id = @uid
                                 AND CAST(check_in AS DATE) = CAST(GETDATE() AS DATE)
                               ORDER BY check_in DESC";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@uid", SqlDbType.Int).Value = userId;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Attendance
                            {
                                id = (int)reader["attendance_id"],
                                checkIn = (DateTime)reader["check_in"],
                                checkOut = reader["check_out"] as DateTime?,
                                shiftType = reader["shift_type"] == DBNull.Value ? string.Empty : reader["shift_type"].ToString(),
                                status = reader["status"] == DBNull.Value ? string.Empty : reader["status"].ToString()
                            });
                        }
                    }
                }
            }

            return list;
        }

        public bool checkIn(int userId)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();

                string checkSql = "SELECT COUNT(*) FROM attendance " +
                                  "WHERE user_id = @uid " +
                                  "AND CAST(check_in AS DATE) = CAST(GETDATE() AS DATE) " +
                                  "AND check_out IS NULL";

                using (SqlCommand checkCmd = new SqlCommand(checkSql, connection))
                {
                    checkCmd.Parameters.Add("@uid", SqlDbType.Int).Value = userId;
                    int openedShift = (int)checkCmd.ExecuteScalar();
                    if (openedShift > 0)
                    {
                        return false;
                    }
                }

                using (SqlCommand cmd = new SqlCommand("INSERT INTO attendance (check_in, user_id, status) VALUES (GETDATE(), @uid, @status)", connection))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@status", "Đang làm việc");
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool checkOut(int userId, DateTime date)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();

                string sql = @"UPDATE attendance
                               SET check_out = @date, status = @status
                               WHERE attendance_id = (
                                   SELECT TOP 1 attendance_id
                                   FROM attendance
                                   WHERE user_id = @uid AND check_out IS NULL
                                   ORDER BY check_in DESC
                               )";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@date", SqlDbType.DateTime).Value = date;
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@status", "Đã xong ca");
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public DateTime getTodayCheckIn(int userId)
        {
            DateTime? checkIn = null;

            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "SELECT check_in FROM attendance " +
                             "WHERE user_id = @uid " +
                             "AND CAST(check_in AS DATE) = CAST(GETDATE() AS DATE) " +
                             "AND check_out IS NULL";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@uid", userId);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    checkIn = Convert.ToDateTime(rs);
                }
            }
            return checkIn.Value;
        }
        public void updateShiftType(int userId)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();

                string sql = @"UPDATE attendance
                               SET shift_type = @type
                               WHERE attendance_id = (
                                   SELECT TOP 1 attendance_id
                                   FROM attendance
                                    WHERE user_id = @uid
                                   ORDER BY check_in DESC
                               )";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.AddWithValue("@uid", userId);
                    cmd.Parameters.AddWithValue("@type", "Tăng ca");
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

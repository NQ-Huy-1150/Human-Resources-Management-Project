using LaptopShopApplication.Repository;
using layout.domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace layout.repository
{
    public class AttendanceRepository
    {
        private readonly MssSQLConnection conn = new MssSQLConnection();

        public List<AttendanceAdminRecord> getAllAttendanceForAdmin()
        {
            List<AttendanceAdminRecord> list = new List<AttendanceAdminRecord>();

            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = @"SELECT u.full_name, a.check_in, a.check_out, a.loai_ca
                               FROM attendance a
                               INNER JOIN users u ON a.user_id = u.user_id
                               ORDER BY a.check_in DESC";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AttendanceAdminRecord
                        {
                            full_name = reader["full_name"].ToString(),
                            check_in = (DateTime)reader["check_in"],
                            check_out = reader["check_out"] as DateTime?,
                            loai_ca = reader["loai_ca"] == DBNull.Value ? null : reader["loai_ca"].ToString(),
                        });
                    }
                }
            }

            return list;
        }

        public List<AttendanceUserRecord> getTodayAttendanceByUser(int userId)
        {
            List<AttendanceUserRecord> list = new List<AttendanceUserRecord>();

            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = @"SELECT attendance_id, check_in, check_out, loai_ca
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
                            list.Add(new AttendanceUserRecord
                            {
                                attendance_id = (int)reader["attendance_id"],
                                check_in = (DateTime)reader["check_in"],
                                check_out = reader["check_out"] as DateTime?,
                                loai_ca = reader["loai_ca"] == DBNull.Value ? null : reader["loai_ca"].ToString()
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

                string checkSql = @"SELECT COUNT(*) FROM attendance
                                    WHERE user_id = @uid
                                      AND CAST(check_in AS DATE) = CAST(GETDATE() AS DATE)
                                      AND check_out IS NULL";

                using (SqlCommand checkCmd = new SqlCommand(checkSql, connection))
                {
                    checkCmd.Parameters.Add("@uid", SqlDbType.Int).Value = userId;
                    int openedShift = (int)checkCmd.ExecuteScalar();
                    if (openedShift > 0)
                    {
                        return false;
                    }
                }

                using (SqlCommand cmd = new SqlCommand("INSERT INTO attendance (check_in, user_id) VALUES (GETDATE(), @uid)", connection))
                {
                    cmd.Parameters.Add("@uid", SqlDbType.Int).Value = userId;
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool checkOut(int userId)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();

                string sql = @"UPDATE attendance
                               SET check_out = GETDATE()
                               WHERE attendance_id = (
                                   SELECT TOP 1 attendance_id
                                   FROM attendance
                                   WHERE user_id = @uid AND check_out IS NULL
                                   ORDER BY check_in DESC
                               )";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@uid", SqlDbType.Int).Value = userId;
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}

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
                    l.id           AS Id,
                    l.nv_id        AS NvId,
                    nd.name        AS Name,
                    l.luong_co_ban AS LuongCoBan,
                    l.tro_cap      AS TroCap,
                    l.thuong       AS Thuong,
                    l.thang        AS Thang,
                    l.nam          AS Nam
                FROM luong l
                INNER JOIN nguoidung nd ON nd.id = l.nv_id
                ORDER BY l.id ASC";

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        SalaryDay.Luong salary = new SalaryDay.Luong
                        {
                            Id = reader["Id"] != DBNull.Value ? Convert.ToInt32(reader["Id"]) : 0,
                            NvId = reader["NvId"] != DBNull.Value ? Convert.ToInt32(reader["NvId"]) : 0,
                            Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : "N/A",
                            LuongCoBan = reader["LuongCoBan"] != DBNull.Value ? Convert.ToDouble(reader["LuongCoBan"]) : 0,
                            TroCap = reader["TroCap"] != DBNull.Value ? Convert.ToDouble(reader["TroCap"]) : 0,
                            Thuong = reader["Thuong"] != DBNull.Value ? Convert.ToDouble(reader["Thuong"]) : 0,
                            Thang = reader["Thang"] != DBNull.Value ? Convert.ToInt32(reader["Thang"]) : 0,
                            Nam = reader["Nam"] != DBNull.Value ? Convert.ToInt32(reader["Nam"]) : 0,
                            Muon = 0
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
                    string sql = @"UPDATE luong 
                           SET luong_co_ban = @LuongCoBan, 
                               tro_cap      = @TroCap, 
                               thuong       = @Thuong
                           WHERE id = @Id";

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.Parameters.AddWithValue("@LuongCoBan", luongCoBan);
                    cmd.Parameters.AddWithValue("@TroCap", troCap);
                    cmd.Parameters.AddWithValue("@Thuong", thuong);
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

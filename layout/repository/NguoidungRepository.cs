using LaptopShopApplication.Repository;
using layout.domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace layout.repository
{
    public class NguoidungRepository
    {
        MssSQLConnection conn = new MssSQLConnection();
        //Hiển thị toàn bộ người dùng
        public DataTable getAllRecruitment()
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = @"
            SELECT 
                n.user_id AS ma_nguoidung,
                n.full_name AS ho_ten,
                n.email AS thu_dien_tu,
                n.password AS mat_khau,
                n.address AS dia_chi,
                n.phone_number AS so_dien_thoai,
                r.role_name AS vai_tro,
                n.department_id AS ma_phongban
            FROM users n
            INNER JOIN roles r ON n.role_id = r.role_id
            ORDER BY n.user_id";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
        }
        public DataTable findByHoten(string hoten)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = @"
        SELECT 
            n.user_id AS ma_nguoidung,
            n.full_name AS ho_ten,
            n.email AS thu_dien_tu,
            n.password AS mat_khau,
            n.address AS dia_chi,
            n.phone_number AS so_dien_thoai,
            r.role_name AS vai_tro,
            n.department_id AS ma_phongban
        FROM users n
        INNER JOIN roles r ON n.role_id = r.role_id
        WHERE n.full_name LIKE @hoten
        ORDER BY n.user_id";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@hoten", "%" + hoten + "%");

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
        }
        // Xóa người dùng
        public bool deleteNguoidung(int id)
        {
            using (SqlConnection connection = conn.dbConnection())
            using (SqlCommand cmd = new SqlCommand("DELETE FROM users WHERE user_id=@id", connection))
            {
                connection.Open();
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        //Cập nhật người dùng
        public bool updateNguoidung(DataRowView row)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = @"UPDATE users
                       SET full_name=@hoten,
                           email=@email,
                           password=@matkhau,
                           address=@diachi,
                           phone_number=@sodienthoai,
                           department_id=@maphong
                       WHERE user_id=@id";

                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("@hoten", row["ho_ten"]);
                cmd.Parameters.AddWithValue("@email", row["thu_dien_tu"]);
                cmd.Parameters.AddWithValue("@matkhau", row["mat_khau"]);
                cmd.Parameters.AddWithValue("@diachi", row["dia_chi"]);
                cmd.Parameters.AddWithValue("@sodienthoai", row["so_dien_thoai"]);
                cmd.Parameters.AddWithValue("@maphong", row["ma_phongban"]);
                cmd.Parameters.AddWithValue("@id", row["ma_nguoidung"]);

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        

        // Thêm người dùng với kiểm tra trùng lặp email và số điện thoại
        public bool addNguoidung1(NguoiDung obj, out string message)
        {
            message = string.Empty;

            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                // Check email tồn tại
                string checkEmailSql = "SELECT COUNT(*) FROM users WHERE email = @email";
                SqlCommand checkEmailCmd = new SqlCommand(checkEmailSql, connection);
                checkEmailCmd.Parameters.AddWithValue("@email", obj.thu_dien_tu);
                int emailCount = (int)checkEmailCmd.ExecuteScalar();
                if (emailCount > 0)
                {
                    message = "Email đã tồn tại. Vui lòng nhập lại.";
                    return false;
                }

                // Check số điện thoại tồn tại
                string checkPhoneSql = "SELECT COUNT(*) FROM users WHERE phone_number = @sodienthoai";
                SqlCommand checkPhoneCmd = new SqlCommand(checkPhoneSql, connection);
                checkPhoneCmd.Parameters.AddWithValue("@sodienthoai", obj.so_dien_thoai);
                int phoneCount = (int)checkPhoneCmd.ExecuteScalar();
                if (phoneCount > 0)
                {
                    message = "Số điện thoại đã tồn tại. Vui lòng nhập lại.";
                    return false;
                }

                // Thêm người dùng
                string sql = @"INSERT INTO users
            (full_name, email, password, address, phone_number, role_id, department_id)
            VALUES
            (@hoten, @email, @matkhau, @diachi, @sodienthoai, @vaitro, @phongban)";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@hoten", obj.ho_ten);
                cmd.Parameters.AddWithValue("@email", obj.thu_dien_tu);
                cmd.Parameters.AddWithValue("@matkhau", obj.mat_khau);
                cmd.Parameters.AddWithValue("@diachi", obj.dia_chi);
                cmd.Parameters.AddWithValue("@sodienthoai", obj.so_dien_thoai);
                cmd.Parameters.AddWithValue("@vaitro", obj.ma_vaitro);
                cmd.Parameters.AddWithValue("@phongban", obj.ma_phongban);

                int result = cmd.ExecuteNonQuery();

                if (result > 0)
                {
                    message = "Thêm người dùng thành công.";
                    return true;
                }
                else
                {
                    message = "Thêm người dùng thất bại. Vui lòng thử lại.";
                    return false;
                }
            }
            
        }
        public bool isEmailAndPasswordExisted(string email, string pass)
        {
            bool flag = false;
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Select email, password from users where email=@email and password=@pass";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@pass", pass);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    flag = true;
                }
            }
            return flag;
        }
        public DataTable getUserByEmail(string email)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                const string sql = "SELECT user_id, role_id, full_name FROM users WHERE email = @email";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar, 255).Value = email;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable data = new DataTable();
                        adapter.Fill(data);
                        return data;
                    }
                }
            }
        }
        public int getUserIdFromName(string name)
        {
            int id = 0;
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Select user_id from users where full_name = @name";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@name", name);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    id = Convert.ToInt32(rs);
                }
            }
            return id;
        }
    }
}

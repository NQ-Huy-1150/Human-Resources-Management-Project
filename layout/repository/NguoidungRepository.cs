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
                n.role_id AS ma_vaitro,
                r.role_name AS vai_tro,
                n.department_id AS ma_phongban,
                n.pos_id AS ma_chucvu
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
            n.role_id AS ma_vaitro,
            r.role_name AS vai_tro,
            n.department_id AS ma_phongban,
            n.pos_id AS ma_chucvu
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
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    SqlCommand deleteAttendanceCmd = new SqlCommand("DELETE FROM attendance WHERE user_id=@id", connection, transaction);
                    deleteAttendanceCmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    deleteAttendanceCmd.ExecuteNonQuery();

                    SqlCommand deletePayrollCmd = new SqlCommand("DELETE FROM payroll WHERE user_id=@id", connection, transaction);
                    deletePayrollCmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    deletePayrollCmd.ExecuteNonQuery();

                    SqlCommand deleteUserCmd = new SqlCommand("DELETE FROM users WHERE user_id=@id", connection, transaction);
                    deleteUserCmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                    int affectedRows = deleteUserCmd.ExecuteNonQuery();

                    transaction.Commit();
                    return affectedRows > 0;
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }

            }
        }

        public DataTable getNguoidungById(int id)
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
                n.role_id AS ma_vaitro,
                r.role_name AS vai_tro,
                n.department_id AS ma_phongban,
                n.pos_id AS ma_chucvu
            FROM users n
            INNER JOIN roles r ON n.role_id = r.role_id
            WHERE n.user_id = @id";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
        }

        public bool updateNguoidungFromForm(int userId, string hoTen, string email, string matKhau, string diaChi, string soDienThoai, int roleId, string departmentId, int? posId)
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
                           role_id=@vaitro,
                           department_id=@maphong,
                           pos_id=@machucvu
                       WHERE user_id=@id";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@hoten", hoTen);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@matkhau", matKhau);
                cmd.Parameters.AddWithValue("@diachi", diaChi);
                cmd.Parameters.AddWithValue("@sodienthoai", soDienThoai);
                cmd.Parameters.AddWithValue("@vaitro", roleId);
                cmd.Parameters.AddWithValue("@maphong", departmentId);
                if (posId.HasValue)
                {
                    cmd.Parameters.AddWithValue("@machucvu", posId.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@machucvu", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@id", userId);

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
            (full_name, email, password, address, phone_number, role_id, department_id, pos_id)
            VALUES
            (@hoten, @email, @matkhau, @diachi, @sodienthoai, @vaitro, @phongban, @machucvu)";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@hoten", obj.ho_ten);
                cmd.Parameters.AddWithValue("@email", obj.thu_dien_tu);
                cmd.Parameters.AddWithValue("@matkhau", obj.mat_khau);
                cmd.Parameters.AddWithValue("@diachi", obj.dia_chi);
                cmd.Parameters.AddWithValue("@sodienthoai", obj.so_dien_thoai);
                cmd.Parameters.AddWithValue("@vaitro", obj.ma_vaitro);
                cmd.Parameters.AddWithValue("@phongban", obj.ma_phongban);
                if (obj.ma_chucvu.HasValue)
                {
                    cmd.Parameters.AddWithValue("@machucvu", obj.ma_chucvu.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@machucvu", DBNull.Value);
                }

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

        public int getUserIdFromEmail(string email)
        {
            int temp = -1;
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Select user_id from users where email = @email";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@email", email);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    temp = Convert.ToInt32(rs);
                }
            }
            return temp;
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

        public string getUserNameFromId(int userId)
        {
            string name = string.Empty;
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Select full_name from users where user_id = @userId";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@userId", userId);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    name = rs.ToString();
                }
            }
            return name;
        }

        public int getUserRoleId(int userId)
        {
            int roleId = -1;
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Select role_id from users where user_id = @userId";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@userId", userId);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    roleId = Convert.ToInt32(rs);
                }
            }
            return roleId;
        }
        public int getUserPositionId(int userId)
        {
            int positionId = -1;
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Select pos_id from users where user_id = @userId";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@userId", userId);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    positionId = Convert.ToInt32(rs);
                }
            }
            return positionId;
        }
    }
}

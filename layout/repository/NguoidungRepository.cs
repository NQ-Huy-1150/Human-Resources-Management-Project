using LaptopShopApplication.Repository;
using layout.domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                n.ma_nguoidung,
                n.ho_ten,
                n.thu_dien_tu,
                n.mat_khau,
                n.dia_chi,
                n.so_dien_thoai,
                v.ten_vaitro AS vai_tro,
                n.ma_phongban
            FROM nguoidung n
            INNER JOIN vaitro v ON n.ma_vaitro = v.ma_vaitro
            ORDER BY n.ma_nguoidung";

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
            n.ma_nguoidung,
            n.ho_ten,
            n.thu_dien_tu,
            n.mat_khau,
            n.dia_chi,
            n.so_dien_thoai,
            v.ten_vaitro AS vai_tro,
            n.ma_phongban
        FROM nguoidung n
        INNER JOIN vaitro v ON n.ma_vaitro = v.ma_vaitro
        WHERE n.ho_ten LIKE @hoten
        ORDER BY n.ma_nguoidung";

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
            using (SqlCommand cmd = new SqlCommand("DELETE FROM nguoidung WHERE ma_nguoidung=@id", connection))
            {
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                return cmd.ExecuteNonQuery() > 0;
            }
        }
        //Cập nhật người dùng
        public bool updateNguoidung(DataRowView row)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = @"UPDATE nguoidung
                       SET ho_ten=@hoten,
                           thu_dien_tu=@email,
                           mat_khau=@matkhau,
                           dia_chi=@diachi,
                           so_dien_thoai=@sodienthoai,
                           ma_phongban=@maphong
                       WHERE ma_nguoidung=@id";

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
                // Check email tồn tại
                string checkEmailSql = "SELECT COUNT(*) FROM nguoidung WHERE thu_dien_tu = @email";
                SqlCommand checkEmailCmd = new SqlCommand(checkEmailSql, connection);
                checkEmailCmd.Parameters.AddWithValue("@email", obj.thu_dien_tu);
                int emailCount = (int)checkEmailCmd.ExecuteScalar();
                if (emailCount > 0)
                {
                    message = "Email đã tồn tại. Vui lòng nhập lại.";
                    return false;
                }

                // Check số điện thoại tồn tại
                string checkPhoneSql = "SELECT COUNT(*) FROM nguoidung WHERE so_dien_thoai = @sodienthoai";
                SqlCommand checkPhoneCmd = new SqlCommand(checkPhoneSql, connection);
                checkPhoneCmd.Parameters.AddWithValue("@sodienthoai", obj.so_dien_thoai);
                int phoneCount = (int)checkPhoneCmd.ExecuteScalar();
                if (phoneCount > 0)
                {
                    message = "Số điện thoại đã tồn tại. Vui lòng nhập lại.";
                    return false;
                }

                // Thêm người dùng
                string sql = @"INSERT INTO nguoidung
            (ho_ten, thu_dien_tu, mat_khau, dia_chi, so_dien_thoai, ma_vaitro, ma_phongban)
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
    }
}

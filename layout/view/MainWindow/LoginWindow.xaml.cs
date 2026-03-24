using layout.domain;
using layout.service;
using layout.view.Bars;
using layout.view.Main_Window;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace layout.view.Main_Window
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        Nguoidungservice service = new Nguoidungservice();
        RoleService roleService = new RoleService();
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            getLogin();
        }

        private void hlForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ Admin để lấy lại mật khẩu.");
        }
        private void getLogin()
        {
            
            if (service.verifyUser(txtEmail.Text,txtPassword.Password))
            {
                NguoiDung nguoi = convertDataTableToObject(txtEmail.Text);
                string roleName = roleService.getRoleName(nguoi.ma_vaitro);
                if (roleName.Equals("Quản trị viên"))
                {
                    MainWindow mw = new MainWindow(nguoi.ma_nguoidung);
                    mw.Show();
                    if (this.Owner is HomePageWindow hp)
                    {
                        hp.Close();
                        this.Close();
                    }
                }
                if (roleName.Equals("Nhân viên"))
                {
                    // Lấy cửa sổ cha và ép kiểu về HomePageWindow
                    if (this.Owner is HomePageWindow hp)
                    {
                        hp.UpdateUser(nguoi.ma_nguoidung);
                    }

                    this.Close(); // Đóng LoginWindow và cập nhật HomePageWindow đang mở
                }
            }
        }
        private NguoiDung convertDataTableToObject(string email)
        {
            NguoiDung nguoi = new NguoiDung();
            DataTable data = service.getUserRoleAndIdByEmail(email);
            foreach (DataRow row in data.Rows)
            {
                nguoi.ma_nguoidung = Convert.ToInt32(row["user_id"].ToString());
                nguoi.ma_vaitro = Convert.ToInt32(row["role_id"].ToString());
                nguoi.ho_ten = Convert.ToString(row["full_name"].ToString());
            }
            return nguoi;
        }
    }
}

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
        public LoginWindow()
        {
            InitializeComponent();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string email1 = "admin";
            string username = "admin bla bla";
            string pass1 = "123";
            string role1 = "user";

            if (txtEmail.Text.Equals(email1) && txtPassword.Password.Equals(pass1))
            {
                if (role1.Equals("admin"))
                {
                    MainWindow mw = new MainWindow();
                    mw.Show();
                    this.Close();
                }
                if (role1.Equals("user"))
                {
                    // Lấy cửa sổ cha và ép kiểu về HomePageWindow
                    if (this.Owner is HomePageWindow hp)
                    {
                        hp.UpdateUser(username); // Cập nhật tên user lên TopBar
                    }

                    this.Close(); // Đóng LoginWindow và cập nhật HomePageWindow đang mở
                }
            }
        }

        private void hlForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Vui lòng liên hệ Admin để lấy lại mật khẩu.");
        }
    }
}

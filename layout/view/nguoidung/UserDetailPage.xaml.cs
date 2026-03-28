using layout.service;
using layout.view.Main_Window;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace layout.view.Nguoidung
{
    public partial class UserDetailPage : Page
    {
        private readonly Nguoidungservice service = new Nguoidungservice();

        public UserDetailPage(int userId)
        {
            InitializeComponent();
            loadUser(userId);
        }

        private void loadUser(int userId)
        {
            DataTable data = service.getNguoidungById(userId);
            if (data.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy người dùng.");
                BackToUsers();
                return;
            }

            DataRow row = data.Rows[0];
            txtUserId.Text = Convert.ToString(row["ma_nguoidung"]);
            txtFullName.Text = Convert.ToString(row["ho_ten"]);
            txtEmail.Text = Convert.ToString(row["thu_dien_tu"]);
            txtAddress.Text = Convert.ToString(row["dia_chi"]);
            txtPhone.Text = Convert.ToString(row["so_dien_thoai"]);
            txtRole.Text = Convert.ToString(row["vai_tro"]);
            txtDepartment.Text = Convert.ToString(row["ma_phongban"]);
            txtPosition.Text = row["ma_chucvu"] == DBNull.Value ? "" : Convert.ToString(row["ma_chucvu"]);
        }

        private void OpenDashboard_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new AdminDashboardPage());
            }
        }

        private void BackToUsers_Click(object sender, RoutedEventArgs e)
        {
            BackToUsers();
        }

        private void BackToUsers()
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new layout.view.nguoidung.nguoidungPage());
            }
        }
    }
}
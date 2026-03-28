using layout.service;
using layout.view.Main_Window;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace layout.view.Nguoidung
{
    public partial class DeleteNguoidungPage : Page
    {
        private readonly Nguoidungservice service = new Nguoidungservice();

        public DeleteNguoidungPage(int userId)
        {
            InitializeComponent();
            loadUser(userId);
        }

        private void loadUser(int userId)
        {
            DataTable data = service.getNguoidungById(userId);
            if (data.Rows.Count == 0)
            {
                txtUserId.Text = Convert.ToString(userId);
                txtName.Text = "";
                return;
            }

            DataRow row = data.Rows[0];
            txtUserId.Text = Convert.ToString(row["ma_nguoidung"]);
            txtName.Text = Convert.ToString(row["ho_ten"]);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int userId = Convert.ToInt32(txtUserId.Text);
            bool ok = service.XoaNguoiDung(userId);
            if (ok)
            {
                MessageBox.Show("Xóa thành công.");
                BackToUsers();
            }
            else
            {
                MessageBox.Show("Xóa thất bại.");
            }
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
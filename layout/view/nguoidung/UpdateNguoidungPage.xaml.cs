using layout.service;
using layout.view.Main_Window;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace layout.view.Nguoidung
{
    public partial class UpdateNguoidungPage : Page
    {
        private readonly Nguoidungservice service = new Nguoidungservice();
        private readonly DepartmentService departmentService = new DepartmentService();
        private readonly RoleService roleService = new RoleService();
        private readonly PositionService positionService = new PositionService();

        public UpdateNguoidungPage(int userId)
        {
            InitializeComponent();
            loadReferenceData();
            loadUser(userId);
        }

        private void loadReferenceData()
        {
            cbPhongban.ItemsSource = departmentService.getAllDepartments();
            cbVaitro.ItemsSource = roleService.getAllRoles().DefaultView;
            cbChucvu.ItemsSource = positionService.getAllPositionName().DefaultView;
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
            txtPos.Text = Convert.ToString(row["ma_nguoidung"]);
            txtHoten.Text = Convert.ToString(row["ho_ten"]);
            txtEmail.Text = Convert.ToString(row["thu_dien_tu"]);
            txtMatkhau.Text = Convert.ToString(row["mat_khau"]);
            txtDiachi.Text = Convert.ToString(row["dia_chi"]);
            txtSodienthoai.Text = Convert.ToString(row["so_dien_thoai"]);
            cbPhongban.SelectedValue = Convert.ToString(row["ma_phongban"]);
            cbVaitro.SelectedValue = Convert.ToInt32(row["ma_vaitro"]);

            if (row["ma_chucvu"] != DBNull.Value)
            {
                cbChucvu.SelectedValue = Convert.ToInt32(row["ma_chucvu"]);
            }
            else
            {
                cbChucvu.SelectedItem = null;
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string hoTen = txtHoten.Text.Trim();
            string email = txtEmail.Text.Trim();
            string matKhau = txtMatkhau.Text.Trim();
            string diaChi = txtDiachi.Text.Trim();
            string soDienThoai = txtSodienthoai.Text.Trim();

            if (string.IsNullOrWhiteSpace(hoTen) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(matKhau) ||
                string.IsNullOrWhiteSpace(diaChi) ||
                string.IsNullOrWhiteSpace(soDienThoai) ||
                cbPhongban.SelectedValue == null ||
                cbVaitro.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            if (!Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                MessageBox.Show("Email không đúng định dạng.");
                return;
            }

            if (!Regex.IsMatch(soDienThoai, @"^\d{8,15}$"))
            {
                MessageBox.Show("Số điện thoại chỉ gồm số và dài từ 8 đến 15 ký tự.");
                return;
            }

            int userId = Convert.ToInt32(txtPos.Text);
            if (service.isPhoneNumberExistedForOtherUser(userId, soDienThoai))
            {
                MessageBox.Show("Số điện thoại đã tồn tại. Vui lòng nhập lại.");
                return;
            }

            int roleId = Convert.ToInt32(cbVaitro.SelectedValue);
            string departmentId = Convert.ToString(cbPhongban.SelectedValue);
            int? posId = null;
            if (cbChucvu.SelectedValue != null)
            {
                posId = Convert.ToInt32(cbChucvu.SelectedValue);
            }

            bool ok = service.capnhatNguoidungFromForm(userId, hoTen, email, matKhau, diaChi, soDienThoai, roleId, departmentId, posId);
            if (ok)
            {
                MessageBox.Show("Cập nhật thành công.");
                BackToUsers();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại.");
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
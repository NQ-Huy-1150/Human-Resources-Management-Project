using layout.domain;
using layout.service;
using layout.view.Main_Window;
using layout.view.nguoidung;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace layout.view.Nguoidung
{

    public partial class Themnguoidung : Page
    {
        private readonly Nguoidungservice service = new Nguoidungservice();
        private readonly DepartmentService departmentService = new DepartmentService();
        private readonly RoleService roleService = new RoleService();
        private readonly PositionService positionService = new PositionService();
        private readonly SalaryService salaryService = new SalaryService();

        public Themnguoidung()
        {
            InitializeComponent();
            loadReferenceData();
        }

        private void loadReferenceData()
        {
            cbPhongban.ItemsSource = departmentService.getAllDepartments();
            cbVaitro.ItemsSource = roleService.getAllRoles().DefaultView;
            cbChucvu.ItemsSource = positionService.getAllPositionName().DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string hoten = txtHoten.Text.Trim();
            string email = txtEmail.Text.Trim();
            string matkhau = txtMatkhau.Password.Trim();
            string matkhau2 = txtMatkhau2.Password.Trim(); // mật khẩu nhập lại
            string diachi = txtDiachi.Text.Trim();
            string sodienthoai = txtSodienthoai.Text.Trim();

            if (string.IsNullOrEmpty(hoten) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(matkhau) || string.IsNullOrEmpty(matkhau2) ||
                string.IsNullOrEmpty(diachi) || string.IsNullOrEmpty(sodienthoai))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin");
                return;
            }

            if (!email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Email thiếu ký tự @ hoặc dấu chấm.");
                return;
            }


            if (matkhau.Length < 4)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 4 ký tự.");
                txtMatkhau.Focus();
                return;
            }

            // So sánh mật khẩu
            if (matkhau != matkhau2)
            {
                MessageBox.Show("Mật khẩu không khớp!");
                txtMatkhau.Focus();
                txtMatkhau.SelectAll();
                return;
            }

            if (cbPhongban.SelectedItem == null || cbVaitro.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn phòng ban và vai trò");
                return;
            }

            string maphong = cbPhongban.SelectedValue.ToString();
            int vaitro = Convert.ToInt32(cbVaitro.SelectedValue);
            int? maChucVu = null;
            if (cbChucvu.SelectedValue != null)
            {
                maChucVu = Convert.ToInt32(cbChucvu.SelectedValue);
            }

            NguoiDung nd = new NguoiDung
            {
                ho_ten = hoten,
                thu_dien_tu = email,
                mat_khau = matkhau,
                dia_chi = diachi,
                so_dien_thoai = sodienthoai,
                ma_vaitro = vaitro,
                ma_phongban = maphong,
                ma_chucvu = maChucVu
            };

            string message;
            bool ok = service.themnguoidung(nd, out message);

            if (ok)
            {
                if (nd.ma_chucvu.HasValue)
                {
                    addUserToSalaryTable(nd.thu_dien_tu, nd.ma_chucvu.Value);
                }

                MessageBox.Show(message);
                var mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.MainFrame.Navigate(new nguoidungPage());
                }
            }
            else
            {
                MessageBox.Show(message);
                txtEmail.Focus();
                txtEmail.SelectAll();
            }

        }

        private void addUserToSalaryTable(string email, int positionId)
        {
            int userId = service.getUserIdFromEmail(email);
            if (userId != -1)
            {
                Payroll payroll = new Payroll();
                payroll.userId = userId;
                payroll.netSalary = 0;
                int currentMonth = DateTime.Now.Month;
                payroll.month = currentMonth;
                int currentYear = DateTime.Now.Year;
                payroll.year = currentYear;
                payroll.deduction = 0;
                payroll.bonus = 0;
                payroll.allowance = 0;
                salaryService.createSalary(payroll);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                
                mainWindow.MainFrame.Navigate(new nguoidungPage());
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
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new nguoidungPage());
            }
        }

    }
}

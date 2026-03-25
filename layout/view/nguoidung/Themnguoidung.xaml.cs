using layout.domain;
using layout.service;
using layout.view.Main_Window;
using layout.view.nguoidung;
using System;
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

            MessageBox.Show(message);

            if (ok)
            {
                var mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.MainFrame.Navigate(new nguoidungPage());
                }
            }
            else
            {
                txtEmail.Focus();
                txtEmail.SelectAll();
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

    }
}

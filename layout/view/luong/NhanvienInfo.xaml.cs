using layout;
using layout.repository;
using layout.domain;
using layout.service;
using layout.view.Main_Window;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Data;

namespace layout.luong
{
    /// <summary>
    /// Interaction logic for NhanvienInfo.xaml
    /// </summary>
    public partial class NhanvienInfo : Page
    {
        private Payroll _payroll;
        private int _userId;
        private string _fullName;
        private float _baseSalary;

        public NhanvienInfo()
        {
            InitializeComponent();
        }

        public NhanvienInfo(int payrollId, int userId, string fullName, float baseSalary) : this()
        {
            _userId = userId;
            _fullName = fullName;
            _baseSalary = baseSalary;

            SalaryService salaryService = new SalaryService();
            _payroll = salaryService.getPayRollById(payrollId);

            LoadData();
        }

        private void LoadData()
        {
            if (_payroll == null || _payroll.id == 0) return;

            // Thông tin nhân viên
            TbId.Text = _userId.ToString();
            TbTenNV.Text = _fullName;

            // Thông tin lương
            TbIdLuong.Text = _payroll.id.ToString();
            TbLuongCoBan.Text = _baseSalary.ToString("N0");
            TbTroCap.Text = _payroll.allowance.ToString("N0");
            TbThuong.Text = _payroll.bonus.ToString("N0");
            TbMuon.Text = "0";
            TbKhoanTru.Text = _payroll.deduction.ToString("N0");
            TbThangNam.Text = $"{_payroll.month}/{_payroll.year}";
            TbThucLinh.Text = _payroll.netSalary.ToString("N0");
        }

        private void BtnBangLuong_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new Luong());
            }
        }

        private void BtnUpd_Click(object sender, RoutedEventArgs e)
        {
            if (_payroll == null || _payroll.id == 0)
            {
                MessageBox.Show("Không có dữ liệu lương để sửa", "Sửa Lương",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!float.TryParse(TbTroCap.Text.Replace(",", ""), out float troCap) ||
                !float.TryParse(TbThuong.Text.Replace(",", ""), out float thuong) ||
                !float.TryParse(TbKhoanTru.Text.Replace(",", ""), out float khoanTru))
            {
                MessageBox.Show("Vui lòng nhập số hợp lệ.", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Xác nhận trước khi lưu
            MessageBoxResult confirm = MessageBox.Show(
                "Bạn có chắc chắn muốn cập nhật thông tin lương?", "Xác Nhận",
                MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (confirm != MessageBoxResult.Yes) return;

            float newNetSalary = _baseSalary + troCap + thuong - khoanTru;

            _payroll.allowance = troCap;
            _payroll.bonus = thuong;
            _payroll.deduction = khoanTru;
            _payroll.netSalary = newNetSalary;

            SalaryService salaryService = new SalaryService();
            if (salaryService.UpdateSalary(_payroll))
            {
                // Cập nhật lại Thực Lĩnh
                TbThucLinh.Text = _payroll.netSalary.ToString("N0");
                TbKhoanTru.Text = khoanTru.ToString("N0");

                MessageBox.Show("Cập nhật thành công!", "Thành Công",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
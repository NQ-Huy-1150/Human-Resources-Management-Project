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
        private Payroll payroll;
        private int userId;
        private string fullName;
        private float baseSalary;

        public NhanvienInfo()
        {
            InitializeComponent();
        }

        public NhanvienInfo(int payrollId, int userId, string fullName, float baseSalary) : this()
        {
            this.userId = userId;
            this.fullName = fullName;
            this.baseSalary = baseSalary;

            SalaryService salaryService = new SalaryService();
            this.payroll = salaryService.getPayRollById(payrollId);

            loadData();
        }

        private void loadData()
        {
            if (payroll == null || payroll.id == 0) return;

            // Thông tin nhân viên
            TbId.Text = userId.ToString();
            TbTenNV.Text = fullName;

            // Thông tin lương
            TbIdLuong.Text = payroll.id.ToString();
            TbLuongCoBan.Text = baseSalary.ToString("N0");
            TbTroCap.Text = payroll.allowance.ToString("N0");
            TbThuong.Text = payroll.bonus.ToString("N0");
            TbMuon.Text = "0";
            TbKhoanTru.Text = payroll.deduction.ToString("N0");
            TbThangNam.Text = $"{payroll.month}/{payroll.year}";
            TbThucLinh.Text = payroll.netSalary.ToString("N0");
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
            if (payroll == null || payroll.id == 0)
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

            // lương tích luỹ cơ bản khi không có bất cứ khoản trừ, thưởng nào
            float attendanceSalary = payroll.netSalary
                                     - payroll.allowance
                                     - payroll.bonus
                                     + payroll.deduction;

            // cập nhật các khoản trừ, thưởng mới
            float newNetSalary = attendanceSalary + troCap + thuong - khoanTru;
            if (newNetSalary < 0) newNetSalary = 0;

            payroll.allowance = troCap;
            payroll.bonus = thuong;
            payroll.deduction = khoanTru;
            payroll.netSalary = newNetSalary;

            SalaryService salaryService = new SalaryService();
            if (salaryService.UpdateSalary(payroll))
            {
                // Cập nhật lại UI
                TbThucLinh.Text = payroll.netSalary.ToString("N0");
                TbTroCap.Text = troCap.ToString("N0");
                TbThuong.Text = thuong.ToString("N0");
                TbKhoanTru.Text = khoanTru.ToString("N0");

                MessageBox.Show("Cập nhật thành công!", "Thành Công",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
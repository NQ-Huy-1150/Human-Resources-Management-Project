using layout;
using layout.repository;
using System;
using System.Windows;
using System.Windows.Controls;

namespace layout.luong
{
    /// <summary>
    /// Interaction logic for NhanvienInfo.xaml
    /// </summary>
    public partial class NhanvienInfo : Page
    {
        private SalaryCal.Luong currentSalary;

        public NhanvienInfo()
        {
            InitializeComponent();
        }

        public NhanvienInfo(SalaryCal.Luong salary) : this()
        {
            currentSalary = salary;
            LoadData();
        }

        private void LoadData()
        {
            if (currentSalary == null) return;

            // Thông tin nhân viên
            TbId.Text = currentSalary.UserId.ToString();
            TbTenNV.Text = currentSalary.FullName;

            // Thông tin lương
            TbIdLuong.Text = currentSalary.PayrollId.ToString();
            TbLuongCoBan.Text = currentSalary.BaseSalary.ToString("N0");
            TbTroCap.Text = currentSalary.Allowance.ToString("N0");
            TbThuong.Text = currentSalary.Bonus.ToString("N0");
            TbMuon.Text = "0";
            TbKhoanTru.Text = currentSalary.Deduction.ToString("N0");
            TbThangNam.Text = $"{currentSalary.Month}/{currentSalary.Year}";
            TbThucLinh.Text = currentSalary.NetSalary.ToString("N0");
        }

        private void BtnBangLuong_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Luong());
        }

        private void BtnUpd_Click(object sender, RoutedEventArgs e)
        {
            if (currentSalary == null)
            {
                MessageBox.Show("Không có dữ liệu lương để sửa", "Sửa Lương",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(TbTroCap.Text.Replace(",", ""), out double troCap) ||
                !double.TryParse(TbThuong.Text.Replace(",", ""), out double thuong) ||
                !double.TryParse(TbKhoanTru.Text.Replace(",", ""), out double khoanTru))
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

            SalaryRepository repo = new SalaryRepository();
            if (repo.UpdateSalary(currentSalary.PayrollId, troCap, thuong, khoanTru))
            {

                currentSalary.Allowance = troCap;
                currentSalary.Bonus = thuong;
                currentSalary.Deduction = khoanTru;
                currentSalary.NetSalary = currentSalary.BaseSalary + troCap + thuong - khoanTru;


                // Cập nhật lại Thực Lĩnh
                TbThucLinh.Text = currentSalary.NetSalary.ToString("N0");
                TbKhoanTru.Text = khoanTru.ToString("N0");

                MessageBox.Show("Cập nhật thành công!", "Thành Công",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
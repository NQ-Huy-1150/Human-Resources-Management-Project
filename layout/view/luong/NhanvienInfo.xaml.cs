using layout;
using layout.repository;
using layout.view.Main_Window;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace layout.luong
{
    /// <summary>
    /// Interaction logic for NhanvienInfo.xaml
    /// </summary>
    public partial class NhanvienInfo : Page
    {
        private SalaryCal.Luong _currentSalary;

        public NhanvienInfo()
        {
            InitializeComponent();
        }

        public NhanvienInfo(SalaryCal.Luong salary) : this()
        {
            _currentSalary = salary;
            LoadData();
        }

        private void LoadData()
        {
            if (_currentSalary == null) return;

            // Thông tin nhân viên
            TbId.Text = _currentSalary.UserId.ToString();
            TbTenNV.Text = _currentSalary.FullName;

            // Thông tin lương
            TbIdLuong.Text = _currentSalary.PayrollId.ToString();
            TbLuongCoBan.Text = _currentSalary.BaseSalary.ToString("N0");
            TbTroCap.Text = _currentSalary.Allowance.ToString("N0");
            TbThuong.Text = _currentSalary.Bonus.ToString("N0");
            TbMuon.Text = "0";
            TbKhoanTru.Text = _currentSalary.Deduction.ToString("N0");
            TbThangNam.Text = $"{_currentSalary.Month}/{_currentSalary.Year}";
            TbThucLinh.Text = _currentSalary.NetSalary.ToString("N0");
        }

        private void BtnBangLuong_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Luong());
        }

        private void BtnUpd_Click(object sender, RoutedEventArgs e)
        {
            if (_currentSalary == null)
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
            if (repo.UpdateSalary(_currentSalary.PayrollId,troCap, thuong, khoanTru))
            {

                _currentSalary.Allowance = troCap;
                _currentSalary.Bonus = thuong;


                // Cập nhật lại Thực Lĩnh
                TbThucLinh.Text = _currentSalary.NetSalary.ToString("N0");
                TbKhoanTru.Text = _currentSalary.Deduction.ToString("N0");

                MessageBox.Show("Cập nhật thành công!", "Thành Công",
                    MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}

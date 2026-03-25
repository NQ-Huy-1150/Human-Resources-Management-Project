using layout.repository;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace layout.luong
{
    /// <summary>
    /// Interaction logic for Luong.xaml
    /// </summary>
    public partial class Luong : Page
    {
        public Luong()
        {
            InitializeComponent();
            LoadSalaryData();
        }

        private void LoadSalaryData()
        {
            try
            {
                SalaryRepository salaryRepository = new SalaryRepository();
                List<SalaryCal.Luong> salaryList = salaryRepository.getAllSalary();

                DgNhanVien.ItemsSource = salaryList;

                if (salaryList == null || salaryList.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu lương.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu lương: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadSalaryData();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SalaryCal.Luong selected = DgNhanVien.SelectedItem as SalaryCal.Luong;

            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NavigationService.Navigate(new NhanvienInfo(selected));
        }
    }
}
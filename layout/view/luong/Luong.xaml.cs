using layout.domain;
using layout.repository;
using layout.view.Main_Window;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data;

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
                DataTable data = salaryRepository.getAllPayRoll();

                DgNhanVien.ItemsSource = data.DefaultView;

                if (data == null || data.Rows.Count == 0)
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
            DataRowView selected = DgNhanVien.SelectedItem as DataRowView;

            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int payrollId = Convert.ToInt32(selected["payroll_id"]);
            int userId = Convert.ToInt32(selected["user_id"]);
            string fullName = selected["full_name"].ToString();
            float baseSalary = Convert.ToSingle(selected["base_salary"]);

            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new NhanvienInfo(payrollId, userId, fullName, baseSalary));
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
    }
}
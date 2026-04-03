using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using layout.service;
using layout.domain;
using System.Data;
using layout.view.Main_Window;

namespace layout.view.chamcong
{
    public partial class AdminPage : Page
    {
        private readonly AttendanceService attendanceService = new AttendanceService();

        public AdminPage()
        {
            InitializeComponent();
            LoadAdminData();
        }

        private void LoadAdminData()
        {
            try
            {
                DataTable list = attendanceService.getAllAttendanceForAdmin();
                dgAdmin.ItemsSource = list.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị dữ liệu: " + ex.Message);
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
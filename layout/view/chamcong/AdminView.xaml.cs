using layout.service;
using System;
using System.Windows;
using System.Windows.Controls;

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
                var list = attendanceService.getAllAttendanceForAdmin();
                dgAdmin.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Admin: " + ex.Message);
            }
        }
    }

}
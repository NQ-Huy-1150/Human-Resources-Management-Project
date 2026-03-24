using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using layout.service;
using layout.domain;

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
                List<AttendanceAdminRecord> list = attendanceService.getAllAttendanceForAdmin();
                dgAdmin.ItemsSource = list;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hiển thị dữ liệu: " + ex.Message);
            }
        }
    }
}
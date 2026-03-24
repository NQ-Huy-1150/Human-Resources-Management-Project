using System;
using layout.service;
using System.Windows;
using System.Windows.Controls;

namespace layout.view.chamcong
{
    public partial class UserView : Page
    {
        private readonly AttendanceService attendanceService = new AttendanceService();
        private int currentUserId = -1;
        string username = "";
        Nguoidungservice service = new Nguoidungservice();
        public UserView(string username)
        {
            InitializeComponent();
            LoadData();
            this.username = username;
            currentUserId = service.getUserIdByName(this.username);
        }

        private void LoadData()
        {
            try
            {
                var list = attendanceService.getTodayAttendanceByUser(currentUserId);
                dgLichSu.ItemsSource = list;
            }
            catch (Exception ex) { MessageBox.Show("Lỗi hiển thị: " + ex.Message); }
        }

        private void btnVao_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(currentUserId != -1)
                {
                    attendanceService.checkIn(currentUserId);
                    LoadData();
                    MessageBox.Show("Đã ghi nhận giờ vào!");
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnRa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (currentUserId != -1)
                {
                    if (attendanceService.checkOut(currentUserId)) MessageBox.Show("Đã ghi nhận giờ ra!");
                    else MessageBox.Show("Không tìm thấy ca đang hoạt động!");
                    LoadData();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
    }
}
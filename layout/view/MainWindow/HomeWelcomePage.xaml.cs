using layout.service;
using System;
using System.Windows.Controls;

namespace layout.view.Main_Window
{
    public partial class HomeWelcomePage : Page
    {
        private readonly int userId;
        private readonly Nguoidungservice nguoidungService = new Nguoidungservice();

        public HomeWelcomePage() : this(-1)
        {
        }

        public HomeWelcomePage(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            LoadData();
        }

        private void LoadData()
        {
            string userName = "Khách";
            if (userId > 0)
            {
                userName = nguoidungService.getUserNameById(userId);
            }

            txtGreeting.Text = $"Xin chào, {userName}!";
            txtCurrentTime.Text = "Thời gian hiện tại: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            txtAttendanceTip.Text = userId > 0
                ? "• Dùng mục Chấm công để vào/ra ca hằng ngày."
                : "• Đăng nhập để sử dụng chức năng chấm công cá nhân.";
        }
    }
}

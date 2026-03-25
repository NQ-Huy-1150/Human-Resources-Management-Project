using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using layout.service;
using layout.domain;

namespace layout.view.chamcong
{
    public partial class UserView : Page
    {
        private readonly AttendanceService attendanceService = new AttendanceService();
        private int currentUserId;

        public UserView(int userId)
        {
            InitializeComponent();
            this.currentUserId = userId;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var list = attendanceService.getTodayAttendanceByUser(currentUserId);
                dgLichSu.ItemsSource = list;

                var lastRecord = list.FirstOrDefault();

                if (lastRecord == null)
                {
                    ToggleButtons(canVao: true, canRa: false);
                    txtNotify.Visibility = Visibility.Collapsed;
                }
                else if (lastRecord.check_out == null)
                {
                    ToggleButtons(canVao: false, canRa: true);
                    txtNotify.Visibility = Visibility.Collapsed;
                }
                else
                {
                    ToggleButtons(canVao: false, canRa: false);
                    txtNotify.Text = "Hôm nay bạn đã hoàn thành chấm công. Hẹn gặp lại ngày mai!";
                    txtNotify.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void ToggleButtons(bool canVao, bool canRa)
        {
            btnVao.IsEnabled = canVao;
            btnRa.IsEnabled = canRa;
            btnVao.Opacity = canVao ? 1.0 : 0.4;
            btnRa.Opacity = canRa ? 1.0 : 0.4;
        }

        private void btnVao_Click(object sender, RoutedEventArgs e)
        {
            if (attendanceService.checkIn(currentUserId))
            {
                LoadData();
                MessageBox.Show("Vào ca thành công!");
            }
            else
            {
                MessageBox.Show("Bạn đang trong ca làm hoặc đã vào ca chưa kết thúc.");
            }
        }

        private void btnRa_Click(object sender, RoutedEventArgs e)
        {
            if (attendanceService.checkOut(currentUserId))
            {
                LoadData();
                MessageBox.Show("Ra ca thành công!");
            }
            else
            {
                MessageBox.Show("Không tìm thấy ca đang mở để ra ca.");
            }
        }
    }
}
using layout.domain;
using layout.service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace layout.view.chamcong
{
    public partial class UserView : Page
    {
        private readonly AttendanceService attendanceService = new AttendanceService();
        private readonly Nguoidungservice nguoidungservice = new Nguoidungservice();
        private readonly PositionService positionService = new PositionService();
        private readonly SalaryService salaryService = new SalaryService();
        private int currentUserId;
        private DateTime checkInTime;
        private const int WorkingDays = 26;
        private const int MinutesPerDay = 480;
        private const int breakTime = 60;
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

                Attendance lastRecord = null;
                if (list.Count > 0)
                {
                    lastRecord = list[0];
                }

                if (lastRecord == null)
                {
                    ToggleButtons(canVao: true, canRa: false);
                    txtNotify.Visibility = Visibility.Collapsed;
                }
                else if (lastRecord.checkOut == null)
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
            var openShift = GetOpenShift();

            if (openShift == null)
            {
                MessageBox.Show("Không tìm thấy ca đang mở để ra ca.");
                return;
            }

            checkInTime = openShift.checkIn;
            DateTime currentTime = DateTime.Now;
            // time dif
            TimeSpan timeDif = currentTime - checkInTime;
            double totalMinutes = timeDif.TotalMinutes;

            if (attendanceService.checkOut(currentUserId, currentTime))
            {
                int workTime = Math.Max(0, Convert.ToInt32(totalMinutes) - breakTime);
                if (workTime > MinutesPerDay)
                {
                    attendanceService.updateShiftType(currentUserId);

                }
                salaryCal(workTime);
                LoadData();
                MessageBox.Show("Ra ca thành công!");
            }
            else
            {
                MessageBox.Show("Không tìm thấy ca đang mở để ra ca.");
            }
        }

        private Attendance GetOpenShift()
        {
            List<Attendance> attendanceList = attendanceService.getTodayAttendanceByUser(currentUserId);
            foreach (Attendance attendance in attendanceList)
            {
                if (attendance.checkOut == null)
                {
                    return attendance;
                }
            }

            return null;
        }

        private void salaryCal(int workTime)
        {
            int positionId = nguoidungservice.getPositionId(currentUserId);
            float salaryPerDay = positionService.getBaseSalary(positionId) / WorkingDays;
            if (salaryPerDay < 0)
            {
                salaryPerDay = 0;
            }
            float salaryPerMinute = salaryPerDay / MinutesPerDay;
            // check if new month payroll is created or not
            if (!salaryService.isPayRollWithCurrentMonthAndYearExisted(currentUserId))
            {
                createNewPayRoll();
            }
            // get current payroll
            Payroll payroll = convertDataTableToObject(currentUserId);
            if (workTime > MinutesPerDay)
            {
                int overTime = workTime - MinutesPerDay;
                float overtimeBonus = salaryPerMinute * overTime * 1.5f;
                payroll.bonus += overtimeBonus;
                payroll.netSalary += salaryPerDay + overtimeBonus;
            }
            else
            {
                int missingMinutes = MinutesPerDay - workTime;
                // phạt 3k6 mỗi phút muộn
                float deduction = missingMinutes * 3600;
                float dailySalary = salaryPerDay - deduction;
                if (dailySalary < 0)
                {
                    dailySalary = 0;
                }
                payroll.deduction += deduction;
                payroll.netSalary += dailySalary;
            }
            salaryService.UpdateSalary(payroll);
        }

        private Payroll convertDataTableToObject(int userId)
        {
                Payroll payroll = new Payroll();
                DataTable data = salaryService.getCurrentMonthAndYearPayRoll(userId);
                foreach (DataRow row in data.Rows)
                {
                    payroll.id = Convert.ToInt32(row["payroll_id"].ToString());
                    payroll.userId = Convert.ToInt32(row["user_id"].ToString());
                    payroll.allowance = float.Parse(row["allowance"].ToString());
                    payroll.bonus = float.Parse(row["bonus"].ToString());
                    payroll.deduction = float.Parse(row["deduction"].ToString());
                    payroll.netSalary = float.Parse(row["net_salary"].ToString());
                }
                return payroll;
        }
        private void createNewPayRoll()
        {
            Payroll temp = new Payroll();
            temp.userId = currentUserId;
            temp.netSalary = 0;
            int currentMonth = DateTime.Now.Month;
            temp.month = currentMonth;
            int currentYear = DateTime.Now.Year;
            temp.year = currentYear;
            temp.deduction = 0;
            temp.bonus = 0;
            temp.allowance = 0;
            salaryService.createSalary(temp);
        }
    }
}
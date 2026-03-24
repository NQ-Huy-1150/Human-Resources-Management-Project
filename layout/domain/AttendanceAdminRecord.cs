using System;

namespace layout.domain
{
    public class AttendanceAdminRecord
    {
        public string full_name { get; set; }
        public DateTime check_in { get; set; }
        public DateTime? check_out { get; set; }
        public string loai_ca { get; set; }
        public string ghi_chu { get; set; }

        public string TimeRange =>
            $"{check_in:HH:mm} - {(check_out.HasValue ? check_out.Value.ToString("HH:mm") : "...")}";

        private double TotalMinutes => check_out.HasValue ? (check_out.Value - check_in).TotalMinutes : 0;

        public int StandardMinutes => TotalMinutes > 2 ? 2 : (int)TotalMinutes;

        // 4. Tăng ca (Phần dư trên 480 phút)
        public int OvertimeMinutes => TotalMinutes > 2 ? (int)(TotalMinutes - 2) : 0;

        // 5. Trạng thái để hiển thị chữ "Tăng ca" hoặc "Bình thường"
        public string StatusDisplay
        {
            get
            {
                if (!check_out.HasValue) return "Đang làm việc";
                return OvertimeMinutes > 0 ? "Tăng ca" : "Bình thường";
            }
        }
    }
}
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

        public string TimeRange
        {
            get
            {
                return $"{check_in:HH:mm} - {(check_out.HasValue ? check_out.Value.ToString("HH:mm") : "...")}";
            }
        }

        public int StandardMinutes
        {
            get
            {
                if (!check_out.HasValue) return 0;
                double total = (check_out.Value - check_in).TotalMinutes;
                return total > 480 ? 480 : (int)total;
            }
        }

        public int OvertimeMinutes
        {
            get
            {
                if (!check_out.HasValue) return 0;
                double total = (check_out.Value - check_in).TotalMinutes;
                return total > 480 ? (int)(total - 480) : 0;
            }
        }
        public int MissingMinutes
        {
            get
            {
                return 480 - StandardMinutes;
            }
        }
    }
}

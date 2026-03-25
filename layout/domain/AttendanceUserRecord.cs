using System;

namespace layout.domain
{
    public class AttendanceUserRecord
    {
        public int attendance_id { get; set; }
        public DateTime check_in { get; set; }
        public DateTime? check_out { get; set; }
        public string loai_ca { get; set; }

        public string Status
        {
            get
            {
                return check_out == null ? "Đang làm việc" : "Đã xong ca";
            }
        }
    }
}
using System;

namespace layout.view.chamcong
{
public class AttendanceUserRecord
{
    public int attendance_id { get; set; }
    public DateTime check_in { get; set; }
    public DateTime? check_out { get; set; }
    public string loai_ca { get; set; } // Cột mới thêm
    public string ghi_chu { get; set; } // Cột mới thêm
    public string Status => check_out == null ? "Đang làm việc" : "Đã xong ca";
}
}

using System;

namespace layout.domain
{
    public class Attendance
    {
        public int id { get; set; }
        public DateTime checkIn { get; set; }
        public DateTime? checkOut { get; set; }
        public string shiftType { get; set; }
        
        public string status { get; set; }
    }
}
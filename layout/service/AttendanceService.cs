using layout.domain;
using layout.repository;
using System.Collections.Generic;

namespace layout.service
{
    public class AttendanceService
    {
        private readonly AttendanceRepository repository = new AttendanceRepository();

        public List<AttendanceAdminRecord> getAllAttendanceForAdmin()
        {
            return repository.getAllAttendanceForAdmin();
        }

        public List<AttendanceUserRecord> getTodayAttendanceByUser(int userId)
        {
            return repository.getTodayAttendanceByUser(userId);
        }

        public bool checkIn(int userId)
        {
            return repository.checkIn(userId);
        }

        public bool checkOut(int userId)
        {
            return repository.checkOut(userId);
        }
    }
}

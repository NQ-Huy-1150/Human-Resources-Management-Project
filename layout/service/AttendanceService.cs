using layout.domain;
using layout.repository;
using System;
using System.Collections.Generic;
using System.Data;

namespace layout.service
{
    public class AttendanceService
    {
        private readonly AttendanceRepository repository = new AttendanceRepository();

        public DataTable getAllAttendanceForAdmin()
        {
            return repository.getAllAttendanceForAdmin();
        }

        public List<Attendance> getTodayAttendanceByUser(int userId)
        {
            return repository.getTodayAttendanceByUser(userId);
        }

        public bool checkIn(int userId)
        {
            return repository.checkIn(userId);
        }

        public bool checkOut(int userId, DateTime time)
        {
            return repository.checkOut(userId, time);
        }
        public DateTime getCheckIn(int userid)
        {
            return repository.getTodayCheckIn(userid);
        }
        public void updateShiftType(int userId)
        {
            repository.updateShiftType(userId);
        }
    }
}

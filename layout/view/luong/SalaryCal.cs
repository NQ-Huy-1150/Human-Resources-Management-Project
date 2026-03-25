using layout.domain;
using System;
using System.Collections.Generic;

namespace layout.luong
{
    public class SalaryCal
    {
        public class Luong
        {
            private const int WorkingDays = 26;
            private const int MinutesPerDay = 480;
            private const int TotalWorkingMinutes = WorkingDays * MinutesPerDay;

            public int PayrollId { get; set; }
            public int UserId { get; set; }
            public string Name { get; set; }
            public string PosName { get; set; }
            public string FullName { get; set; }
            public double BaseSalary { get; set; }
            public double Allowance { get; set; }
            public double Bonus { get; set; }
            public double Deduction { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public List<AttendanceAdminRecord> Attendances { get; set; }
            public double NetSalary { get; set; }

            private double GetSalaryPerMinute()
            {
                return BaseSalary / TotalWorkingMinutes;
            }

            private void SumAttendanceMinutes(out int totalMissing, out int totalOvertime)
            {
                totalMissing = 0;
                totalOvertime = 0;

                if (Attendances == null) return;

                foreach (AttendanceAdminRecord record in Attendances)
                {
                    totalMissing += record.MissingMinutes;
                    totalOvertime += record.OvertimeMinutes;
                }
            }

            public void Calculate()
            {
                int totalMissing;
                int totalOvertime;
                SumAttendanceMinutes(out totalMissing, out totalOvertime);

                double salaryPerMinute = GetSalaryPerMinute();
                double overtimePay = totalOvertime * salaryPerMinute;

                Deduction = totalMissing * salaryPerMinute;
                NetSalary = BaseSalary + Allowance + Bonus + overtimePay - Deduction;
            }
        }
    }
}
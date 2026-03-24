using layout.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout.luong
{
    public class SalaryCal
    {
        public class Luong
        {
            public int PayrollId { get; set; }
            public int UserId { get; set; }
            public string Name { get; set; }
            public string PosName { get; set; }
            public string FullName { get; set; }
            public double BaseSalary { get; set; }
            public double Allowance { get; set; }
            public double Bonus { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }

            public List<AttendanceAdminRecord> Attendances { get; set; }
            public double NetSalary
            {
                get
                {
                    return BaseSalary + Allowance + Bonus - Deduction;
                }
            }

            public void AddOvertimeToBonus()
            {
                
                    if (Attendances == null) return;

                    double salaryPerMinute = BaseSalary / 26 / 480;
                    double overtimePay = Attendances.Sum(a => a.OvertimeMinutes) * salaryPerMinute;

                    Bonus += overtimePay;
                
            }
            public double Deduction
            {
                get
                {
                    if (Attendances == null || Attendances.Count == 0)
                        return 0;

                    int workingDays = 26;
                    int minutesPerDay = 480;

                    double salaryPerMinute = BaseSalary / workingDays / minutesPerDay;

                    int totalMissingMinutes = Attendances.Sum(a => 480 - a.StandardMinutes);

                    return totalMissingMinutes * salaryPerMinute;
                }
            }

        }
    }
}

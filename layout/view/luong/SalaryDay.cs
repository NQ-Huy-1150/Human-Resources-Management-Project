using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout.luong
{
    public class SalaryDay
    {
        public class Luong
        {
            public int PayrollId { get; set; }
            public int UserId { get; set; }
            public string FullName { get; set; }
            public double BaseSalary { get; set; }
            public double Allowance { get; set; }
            public double Bonus { get; set; }
            public double Deduction { get; set; }
            public int Month { get; set; }
            public int Year { get; set; }
            public double NetSalary { get; set; }
        }
    }
}

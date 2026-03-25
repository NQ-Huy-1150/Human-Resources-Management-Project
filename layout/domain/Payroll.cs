using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout.domain
{
    public class Payroll
    {
        public int id { get; set; }
        public int userId { get; set; }
        public float allowance { get; set; }
        public float bonus { get; set; }
        public float deduction { get; set; }
        public float netSalary { get; set; }
        public int month { get; set; }
        public int year { get; set; }
    }
}

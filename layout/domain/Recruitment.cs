using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout
{
    public class Recruitment
    {
        public int id { get; set; }
        public string departmentId { get; set; }
        public string position { get; set; }
        public float estimateIncome { get; set; }
        public string condition { get; set; }
        public DateTime subDeadline { get; set; }
        public int quantity { get; set; }
        public string status { get; set; }
    }
}

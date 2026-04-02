using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace layout.domain
{
    public class Candidate
    {
        public int id { get; set; }
        public int recruitId { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string edu_level { get; set; }
        public int yearOfExp { get; set; }
        public string status { get; set; }
        public string lookupId { get; set; }
        public int posId { get; set; }
    }
}

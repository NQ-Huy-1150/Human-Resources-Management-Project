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
            public int Id { get; set; }
            public int NvId { get; set; }

            public string Name { get; set; }
            public double LuongCoBan { get; set; }
            public double TroCap { get; set; }
            public double Thuong { get; set; }
            public int Thang { get; set; }
            public int Nam { get; set; }
            public int Muon { get; set; }
            public double KhoanTru
            {
                get
                {
                    if (Muon < 30)
                    {
                        return Muon * 1000;
                    }
                    if (Muon <= 60)
                    {
                        return 100000;
                    }
                    return 200000;
                }
            }

            public double ThucLinh
            {
                get
                {
                    return ((LuongCoBan+TroCap) * Thang) - KhoanTru;
                }
            }
        }
    }
}

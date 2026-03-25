using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout.domain
{
    public class NguoiDung
    {
        public int ma_nguoidung { get; set; }

        public string ho_ten { get; set; }

        public string thu_dien_tu { get; set; }

        public string mat_khau { get; set; }

        public string dia_chi { get; set; }

        public string so_dien_thoai { get; set; }

        public int ma_vaitro { get; set; }

        public string ma_phongban { get; set; }

        public int? ma_chucvu { get; set; }
    }
}

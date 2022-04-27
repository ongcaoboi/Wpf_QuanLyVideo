using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_QuanLyVideoHocTap.Controller
{
    public class NDBinhLuan
    {
        public NDBinhLuan(int id, string noiDung, DateTime ngay)
        {
            this.id = id;
            this.noiDung = noiDung;
            this.ngay = ngay;
        }

        public int id { get; set; }
        public string noiDung { get; set; }
        public DateTime ngay { get; set; }
    }
}

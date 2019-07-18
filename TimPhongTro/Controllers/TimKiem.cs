using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimPhongTro.Controllers
{
    public class TimKiem
    {
        private int MaPhong;
        private string DiaChi;

        public TimKiem(int MaPhong, string DiaChi)
        {
            this.MaPhong = MaPhong;
            this.DiaChi = DiaChi;
        }

        public int maphong
        {
            get { return MaPhong; }
            set { MaPhong = value; }
        }

        public string diachi
        {
            get { return DiaChi; }
            set { DiaChi = value; }
        }
    }
}
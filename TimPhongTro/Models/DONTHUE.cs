using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimPhongTro.Models
{
    [Table("DONTHUE")]
    public partial class DONTHUE
    {
        [Key]
        public int MaDon { get; set; }

        public int? MaKH { get; set; }

        public int? MaPhong { get; set; }

        [StringLength(30)]
        public string TinhTrang { get; set; }

        public DateTime? NgayNhan { get; set; }

        [StringLength(30)]
        public string NgayHen { get; set; }

        public virtual NGUOIDUNG NGUOIDUNG { get; set; }

        public virtual PHONGTRO PHONGTRO { get; set; }
    }
}
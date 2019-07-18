using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimPhongTro.Models
{
    [Table("NHANXET")]
    public partial class NHANXET
    {
        [Key]
        public int MaNhanXet { get; set; }

        public int? MaPhong { get; set; }

        public int? MaKH { get; set; }

        [StringLength(300)]
        public string NoiDung { get; set; }

        public virtual NGUOIDUNG NGUOIDUNG { get; set; }

        public virtual PHONGTRO PHONGTRO { get; set; }
    }
}
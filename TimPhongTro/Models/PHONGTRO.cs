using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimPhongTro.Models
{
    [Table("PHONGTRO")]
    public partial class PHONGTRO
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PHONGTRO()
        {
            DONTHUEs = new HashSet<DONTHUE>();
            NHANXETs = new HashSet<NHANXET>();
        }

        [Key]
        public int MaPhong { get; set; }

        [StringLength(10)]
        public string SoPhong { get; set; }

        [StringLength(30)]
        public string DienTich { get; set; }

        [StringLength(300)]
        public string DiaChi { get; set; }

        [StringLength(10)]
        public string GiaThue { get; set; }

        public int? MaKH { get; set; }

        [StringLength(300)]
        public string MoTa { get; set; }

        public int? DaNhan { get; set; }

        [StringLength(20)]
        public string Loai { get; set; }

        public int? SoNguoiO { get; set; }

        public DateTime? NgayCapNhat { get; set; }

        public string Anh { get; set; }

        [StringLength(60)]
        public string TinhTrang { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONTHUE> DONTHUEs { get; set; }

        public virtual NGUOIDUNG NGUOIDUNG { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHANXET> NHANXETs { get; set; }
    }
}
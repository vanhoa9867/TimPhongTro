using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimPhongTro.Models
{
    [Table("NGUOIDUNG")]
    public partial class NGUOIDUNG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NGUOIDUNG()
        {
            DONTHUEs = new HashSet<DONTHUE>();
            NhanXets = new HashSet<NHANXET>();
            PHONGTROes = new HashSet<PHONGTRO>();
        }

        [Key]
        public int MaKH { get; set; }

        [StringLength(30)]
        public string TenKH { get; set; }

        [StringLength(30)]
        public string TaiKhoan { get; set; }

        [StringLength(300)]
        public string MatKhau { get; set; }

        [StringLength(10)]
        public string Sdt { get; set; }

        [StringLength(30)]
        public string Email { get; set; }

        [StringLength(90)]
        public string DiaChi { get; set; }

        public string GioiTinh { get; set; }

        [StringLength(30)]
        public string Anh { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DONTHUE> DONTHUEs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NHANXET> NhanXets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PHONGTRO> PHONGTROes { get; set; }
    }
}
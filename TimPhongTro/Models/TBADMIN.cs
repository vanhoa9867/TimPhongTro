using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimPhongTro.Models
{
    [Table("TBADMIN")]
    public partial class TBADMIN
    {
        [Key]
        public int ID { get; set; }

        [StringLength(30)]
        public string Ten { get; set; }

        [StringLength(30)]
        public string TaiKhoan { get; set; }

        [StringLength(300)]
        public string Matkhau { get; set; }
    }
}
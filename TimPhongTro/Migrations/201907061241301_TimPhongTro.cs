namespace TimPhongTro.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimPhongTro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DONTHUE",
                c => new
                    {
                        MaDon = c.Int(nullable: false, identity: true),
                        MaKH = c.Int(),
                        MaPhong = c.Int(),
                        TinhTrang = c.String(maxLength: 30),
                        NgayNhan = c.DateTime(),
                        NgayHen = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.MaDon)
                .ForeignKey("dbo.NGUOIDUNG", t => t.MaKH)
                .ForeignKey("dbo.PHONGTRO", t => t.MaPhong)
                .Index(t => t.MaKH)
                .Index(t => t.MaPhong);
            
            CreateTable(
                "dbo.NGUOIDUNG",
                c => new
                    {
                        MaKH = c.Int(nullable: false, identity: true),
                        TenKH = c.String(maxLength: 30),
                        TaiKhoan = c.String(maxLength: 30),
                        MatKhau = c.String(maxLength: 300),
                        Sdt = c.String(maxLength: 10),
                        Email = c.String(maxLength: 30),
                        DiaChi = c.String(maxLength: 90),
                        GioiTinh = c.String(),
                        Anh = c.String(maxLength: 30),
                    })
                .PrimaryKey(t => t.MaKH);
            
            CreateTable(
                "dbo.NHANXET",
                c => new
                    {
                        MaNhanXet = c.Int(nullable: false, identity: true),
                        MaPhong = c.Int(),
                        MaKH = c.Int(),
                        NoiDung = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.MaNhanXet)
                .ForeignKey("dbo.NGUOIDUNG", t => t.MaKH)
                .ForeignKey("dbo.PHONGTRO", t => t.MaPhong)
                .Index(t => t.MaPhong)
                .Index(t => t.MaKH);
            
            CreateTable(
                "dbo.PHONGTRO",
                c => new
                    {
                        MaPhong = c.Int(nullable: false, identity: true),
                        SoPhong = c.String(maxLength: 10),
                        DienTich = c.String(maxLength: 30),
                        DiaChi = c.String(maxLength: 300),
                        GiaThue = c.String(maxLength: 10),
                        MaKH = c.Int(),
                        MoTa = c.String(maxLength: 300),
                        DaNhan = c.Int(),
                        Loai = c.String(maxLength: 20),
                        SoNguoiO = c.Int(),
                        NgayCapNhat = c.DateTime(),
                        Anh = c.String(),
                        TinhTrang = c.String(maxLength: 60),
                    })
                .PrimaryKey(t => t.MaPhong)
                .ForeignKey("dbo.NGUOIDUNG", t => t.MaKH)
                .Index(t => t.MaKH);
            
            CreateTable(
                "dbo.TBADMIN",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Ten = c.String(maxLength: 30),
                        TaiKhoan = c.String(maxLength: 30),
                        Matkhau = c.String(maxLength: 300),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NHANXET", "MaPhong", "dbo.PHONGTRO");
            DropForeignKey("dbo.PHONGTRO", "MaKH", "dbo.NGUOIDUNG");
            DropForeignKey("dbo.DONTHUE", "MaPhong", "dbo.PHONGTRO");
            DropForeignKey("dbo.NHANXET", "MaKH", "dbo.NGUOIDUNG");
            DropForeignKey("dbo.DONTHUE", "MaKH", "dbo.NGUOIDUNG");
            DropIndex("dbo.PHONGTRO", new[] { "MaKH" });
            DropIndex("dbo.NHANXET", new[] { "MaKH" });
            DropIndex("dbo.NHANXET", new[] { "MaPhong" });
            DropIndex("dbo.DONTHUE", new[] { "MaPhong" });
            DropIndex("dbo.DONTHUE", new[] { "MaKH" });
            DropTable("dbo.TBADMIN");
            DropTable("dbo.PHONGTRO");
            DropTable("dbo.NHANXET");
            DropTable("dbo.NGUOIDUNG");
            DropTable("dbo.DONTHUE");
        }
    }
}

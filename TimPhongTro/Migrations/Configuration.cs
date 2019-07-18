namespace TimPhongTro.Migrations
{
    using global::TimPhongTro.Common;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<global::TimPhongTro.Models.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(global::TimPhongTro.Models.DatabaseContext context)
        {
            context.TBADMINs.AddOrUpdate(
                new Models.TBADMIN() { Ten = "Văn Hoà", TaiKhoan = "vanhoa", Matkhau = StringHash.crypto("12345678") }
                );
        }
    }
}

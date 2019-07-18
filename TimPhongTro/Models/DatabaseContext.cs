using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace TimPhongTro.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("name=ConnectionString")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, global::TimPhongTro.Migrations.Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<DatabaseContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<DONTHUE> DONTHUEs { get; set; }
        public virtual DbSet<NGUOIDUNG> NGUOIDUNGs { get; set; }
        public virtual DbSet<NHANXET> NHANXETs { get; set; }
        public virtual DbSet<PHONGTRO> PHONGTROes { get; set; }
        public virtual DbSet<TBADMIN> TBADMINs { get; set; }
    }
}
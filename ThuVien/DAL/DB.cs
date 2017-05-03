using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using ThuVien.Models;

namespace ThuVien.DAL
{
    public class DB : DbContext
    {
        public DbSet<GiangVien> GiangVien { get; set; }
        public DbSet<MonHoc> MonHoc { get; set; }
        public DbSet<TaiLieu> TaiLieu { get; set; }
        public DbSet<TL_Sach> TL_Sach { get; set; }
        public DbSet<TL_BaiBao> TL_BaiBao { get; set; }
        public DbSet<TL_Khac> TL_Khac { get; set; }
        public DbSet<HK> HK { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
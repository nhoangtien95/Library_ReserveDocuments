namespace ThuVien.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ThuVien.DAL.DB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ThuVien.DAL.DB context)
        {
            var gv = new List<GiangVien>
            {
                new GiangVien {
                    HoTen = "Nguyen Van A",
                    Khoa = "CNTT",
                    SoThe = "0123456789",
                    DienThoai = "0123456789",
                    MonHocId = 1,
                    BookId = 1
                }
            };
            gv.ForEach(s => context.GiangVien.AddOrUpdate(p => p.HoTen, s));
            context.SaveChanges();

            var mon = new List<MonHoc>
            {
                new MonHoc
                {
                    CourseID = "00125",
                    TenMon = "Tên môn học",
                    Nganh = "KHMT",
                    Nhom = "2",
                    HK = "HK2/ 2016-2017",
                    SoLuongSV = "60",
                    DuTruFrom = DateTime.UtcNow,
                    DuTruTo = DateTime.UtcNow
                }
            };
            mon.ForEach(s => context.MonHoc.AddOrUpdate(p => p.CourseID, s));
            context.SaveChanges();

            var tailieu = new List<TaiLieu>
            {
                new TaiLieu
                {
                    LoaiTaiLieu = "Sách"
                },

                new TaiLieu
                {
                    LoaiTaiLieu = "Bài báo"
                },

                new TaiLieu
                {
                    LoaiTaiLieu = "Khác"
                }
            };
            tailieu.ForEach(s => context.TaiLieu.AddOrUpdate(p => p.LoaiTaiLieu, s));
            context.SaveChanges();
        }
    }
}
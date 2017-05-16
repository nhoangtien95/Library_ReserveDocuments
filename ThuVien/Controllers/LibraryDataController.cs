using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuVien.DAL;
using ThuVien.Models;
using ThuVien.ViewModels;

namespace ThuVien.Controllers
{
    public class LibraryDataController : Controller
    {
        private DB db = new DB();
        private IData data;

        // GET: LibraryData
        public ActionResult Index()
        {
            var list = new List<DataViewModel>();
            var gv = db.GiangVien.ToList();
            foreach (var instructer in gv)
            {
                var model = new DataViewModel();
                model.gv = instructer;
                var monhoc = db.MonHoc.First(x => x.ID == instructer.MonHocId);
                model.mh = monhoc;

                if (instructer.BookId != null)
                {
                    var book = db.TL_Sach.First(x => x.ID == instructer.BookId);
                    model.TL_Sach = book;
                }
                else if (instructer.PaperId != null)
                {
                    var paper = db.TL_BaiBao.First(x => x.ID == instructer.PaperId);
                    model.TL_BaiBao = paper;
                }
                else
                {
                    var other = db.TL_Khac.First(x => x.ID == instructer.OtherId);
                    model.TL_Khac = other;
                }

                list.Add(model);
            }

            ViewBag.DataList = list;
            return View();
        }

        [HttpPost]
        public ActionResult Index(DataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var gv = db.GiangVien.First(x => x.ID == model.gv.ID);
                gv.HoTen = model.gv.HoTen;
                gv.Khoa = model.gv.Khoa;
                gv.SoThe = model.gv.SoThe;
                gv.DienThoai = model.gv.DienThoai;
                gv.Status = model.gv.Status;
                gv.Note = model.gv.Note;
                db.Entry(gv).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                var mh = db.MonHoc.First(x => x.ID == gv.MonHocId);
                mh.CourseID = model.mh.CourseID;
                mh.TenMon = model.mh.TenMon;
                mh.Nganh = model.mh.Nganh;
                mh.HK = model.mh.HK;
                mh.DuTruFrom = model.mh.DuTruFrom;
                mh.DuTruTo = model.mh.DuTruTo;
                mh.SoLuongSV = model.mh.SoLuongSV;

                db.Entry(mh).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                if (gv.BookId != null)
                {
                    var book = db.TL_Sach.First(x => x.ID == gv.BookId);

                    book.TraCuu = model.TL_Sach.TraCuu;
                    book.NhanDe = model.TL_Sach.NhanDe;
                    book.TacGia = model.TL_Sach.TacGia;
                    book.NXB = model.TL_Sach.NXB;
                    book.LanXuatBan = model.TL_Sach.LanXuatBan;
                    book.ChiTiet = model.TL_Sach.ChiTiet;
                    book.DangDuTru = model.TL_Sach.DangDuTru;
                    book.SoLuong = model.TL_Sach.SoLuong;

                    db.Entry(book).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else if (gv.PaperId != null)
                {
                    var paper = db.TL_BaiBao.First(x => x.ID == gv.PaperId);

                    paper.TraCuu = model.TL_BaiBao.TraCuu;
                    paper.TenBai = model.TL_BaiBao.TenBai;
                    paper.TacGia = model.TL_BaiBao.TacGia;
                    paper.TenTapChi = model.TL_BaiBao.TenTapChi;
                    paper.PhatHanh = model.TL_BaiBao.PhatHanh;
                    paper.ChiTiet = model.TL_BaiBao.ChiTiet;
                    paper.DangDuTru = model.TL_BaiBao.DangDuTru;
                    paper.SoLuong = model.TL_BaiBao.SoLuong;

                    db.Entry(paper).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    var other = db.TL_Khac.First(x => x.ID == gv.OtherId);
                    other.TraCuu = model.TL_Khac.TraCuu;
                    other.NhanDe = model.TL_Khac.NhanDe;
                    other.TacGia = model.TL_Khac.TacGia;

                    db.Entry(other).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}
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
            var gv = db.GiangVien.First(x => x.ID == model.gv.ID);
            gv.HoTen = model.gv.HoTen;
            gv.Khoa = model.gv.Khoa;
            gv.SoThe = model.gv.SoThe;
            gv.DienThoai = model.gv.DienThoai;
            db.Entry(gv).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            var mh = db.MonHoc.First(x => x.ID == model.gv.MonHocId);
            mh.CourseID = model.mh.CourseID;
            mh.TenMon = model.mh.TenMon;
            mh.Nganh = model.mh.Nganh;
            mh.HK = model.mh.HK;
            mh.DuTruFrom = model.mh.DuTruFrom;
            mh.DuTruTo = model.mh.DuTruTo;
            mh.SoLuongSV = model.mh.SoLuongSV;

            db.Entry(mh).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            if (model.gv.BookId != null)
            {
                var book = db.TL_Sach.First(x => x.ID == model.gv.BookId);
            }
            else if (model.gv.PaperId != null)
            {
                var paper = db.TL_BaiBao.First(x => x.ID == model.gv.PaperId);
            }
            else
            {
                var other = db.TL_Khac.First(x => x.ID == model.gv.OtherId);
            }

            return RedirectToAction("Index");
        }
    }
}
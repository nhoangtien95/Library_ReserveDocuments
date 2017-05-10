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
            return RedirectToAction("Index");
        }
    }
}
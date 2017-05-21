using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuVien.DAL;
using ThuVien.Models;
using ThuVien.ViewModels;
using PagedList;

namespace ThuVien.Controllers
{
    public class LibraryDataController : Controller
    {
        private DB db = new DB();
        private IData data;

        public LibraryDataController()
        {
            IData _data = data;
        }

        public LibraryDataController(IData _data)
        {
            this.data = _data;
        }

        // GET: LibraryData
        public ActionResult Index(string Name, string Card, string HK, string Status, string Name_Resource, string Card_Resource, string HK_Resource, string tab)
        {
            ViewBag.CurrentName = Name;
            ViewBag.CurrentCard = Card;
            ViewBag.CurrentHK = HK;
            ViewBag.CurrentStatus = Status;

            var list = new List<DataViewModel>();
            var gv = db.GiangVien.Where(x => x.Tab == 0).ToList();
            int stt = 0;
            if (Status != null)
            {
                stt = int.Parse(Status);
            }

            if (Name != null && Card == null && Status == null)
            {
                gv = db.GiangVien.Where(x => x.Tab == 0 && x.HoTen == Name).ToList();
            }
            else if (Name == null && Card != null && Status == null)
            {
                gv = db.GiangVien.Where(x => x.Tab == 0 && x.SoThe == Card).ToList();
            }
            else if (Name == null && Card == null && Status != null)
            {
                gv = db.GiangVien.Where(x => x.Tab == 0 && x.Status == stt).ToList();
            }
            else if (Name != null && Card != null && Status == null)
            {
                gv = db.GiangVien.Where(x => x.Tab == 0 && x.HoTen == Name && x.SoThe == Card).ToList();
            }
            else if (Name != null && Card == null && Status != null)
            {
                gv = db.GiangVien.Where(x => x.Tab == 0 && x.HoTen == Name && x.Status == stt).ToList();
            }
            else if (Name == null && Card != null && Status != null)
            {
                gv = db.GiangVien.Where(x => x.Tab == 0 && x.SoThe == Card && x.Status == stt).ToList();
            }
            else if (Name != null && Card != null && Status != null)
            {
                gv = db.GiangVien.Where(x => x.Tab == 0 && x.HoTen == Name && x.SoThe == Card && x.Status == stt).ToList();
            }

            if (HK != null)
            {
                int hk = int.Parse(HK);
                var tenHK = db.HK.FirstOrDefault(x => x.STT == hk);
                foreach (var instructer in gv)
                {
                    var model = new DataViewModel();
                    model.gv = instructer;
                    var monhoc = db.MonHoc.FirstOrDefault(x => x.ID == instructer.MonHocId && (string.Compare(x.HK, tenHK.Hocky, true) == 0));
                    //var monhoc = (from x in db.MonHoc where x.ID == instructer.MonHocId && x.HK == tenHK.Hocky select x).Single();

                    if (monhoc == null)
                    {
                        continue;
                    }
                    else
                    {
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
                }
            }
            else
            {
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
            }

            // Resource List
            var Resourcelist = new List<DataViewModel>();

            var Resourcegv = db.GiangVien.Where(x => x.Tab == 1).ToList();

            if (Name_Resource != null && Card_Resource == null)
            {
                Resourcegv = db.GiangVien.Where(x => x.Tab == 1 && x.HoTen == Name_Resource).ToList();
            }
            else if (Name_Resource == null && Card_Resource != null)
            {
                Resourcegv = db.GiangVien.Where(x => x.Tab == 1 && x.SoThe == Card_Resource).ToList();
            }
            else if (Name_Resource != null && Card_Resource != null)
            {
                Resourcegv = db.GiangVien.Where(x => x.Tab == 1 && x.HoTen == Name_Resource && x.SoThe == Card_Resource).ToList();
            }

            if (HK_Resource != null)
            {
                int temphk = int.Parse(HK_Resource);
                var tenHK_Resource = db.HK.FirstOrDefault(x => x.STT == temphk);

                foreach (var instructer in Resourcegv)
                {
                    var model = new DataViewModel();
                    model.gv = instructer;
                    var monhoc = db.MonHoc.FirstOrDefault(x => x.ID == instructer.MonHocId && (string.Compare(x.HK, tenHK_Resource.Hocky, true) == 0));

                    if (monhoc == null)
                    {
                        continue;
                    }
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

                    Resourcelist.Add(model);
                }
            }
            else
            {
                foreach (var instructer in Resourcegv)
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

                    Resourcelist.Add(model);
                }
            }

            ViewBag.DataList = list;
            ViewBag.ResourceDataList = Resourcelist;

            ViewBag.HK = db.HK.Select(m => new SelectListItem { Text = m.Hocky, Value = m.ID.ToString() });
            ViewBag.Card = db.GiangVien.Select(x => new SelectListItem { Text = x.SoThe, Value = x.SoThe }).Distinct().OrderBy(x => x.Value);
            ViewBag.Name = db.GiangVien.Select(x => new SelectListItem { Text = x.HoTen, Value = x.HoTen }).Distinct().OrderBy(x => x.Value);
            if (tab == null)
            {
                ViewBag.Tab = "0";
            }
            else { ViewBag.Tab = tab; }
            ViewBag.Form1_Name = Name;
            ViewBag.Form1_Card = Card;
            ViewBag.Form1_HK = HK;
            ViewBag.Form1_Status = Status;
            ViewBag.Form2_Name = Name_Resource;
            ViewBag.Form2_Card = Card_Resource;
            ViewBag.Form2_HK = HK_Resource;

            //Tab report
            var allHK = db.HK.ToList();
            int temp = 0;
            foreach (var hk in allHK)
            {
                switch (temp)
                {
                    case 0:
                        {
                            ViewBag.HKI = db.MonHoc.Where(x => x.HK == hk.Hocky).ToList().Distinct();
                            break;
                        }
                    case 1:
                        {
                            ViewBag.HKII = db.MonHoc.Where(x => x.HK == hk.Hocky).ToList().Distinct();
                            break;
                        }
                    default:
                        {
                            ViewBag.HKIII = db.MonHoc.Where(x => x.HK == hk.Hocky).ToList().Distinct();
                            break;
                        }
                }
                temp++;
            }

            var Course = db.GiangVien.Select(x => x.Khoa).ToList().Distinct();
            var ListCourse = new List<int>();
            var CourseCount = 0;
            foreach (var c in Course)
            {
                var count = db.GiangVien.Where(x => x.Khoa == c).Distinct().Count();
                ListCourse.Add(count);
                CourseCount++;
            }

            ViewBag.CourseCount = CourseCount;
            ViewBag.Course = Course;
            ViewBag.ListCourse = ListCourse;
            ViewBag.AllRequest = db.GiangVien.Count();

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

        [HttpPost]
        public ActionResult ChangeTab(DataViewModel model)
        {
            if (ModelState.IsValid)
            {
                var gv = db.GiangVien.First(x => x.ID == model.gv.ID);
                gv.Tab = 1;
                db.Entry(gv).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DataFilter(string Name, string Card, string HK, string Status, string Name_Resource, string Card_Resource, string HK_Resource, string tab)
        {
            return RedirectToAction("Index", new { Name = Name, Card = Card, HK = HK, Status = Status, Name_Resource = Name_Resource, Card_Resource = Card_Resource, HK_Resource = HK_Resource, tab = tab });
        }
    }
}
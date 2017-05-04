using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuVien.DAL;
using ThuVien.Models;
using ThuVien.ViewModels;

namespace ThuVien.Controllers
{
    public class LibraryStoredController : Controller
    {
        private DB db = new DB();
        private IData data;
        // GET: LibraryStored
        
        
        
        public ActionResult Index()
        {
            ViewBag.HK = db.HK.Select(m => new SelectListItem { Text = m.Hocky , Value = m.ID.ToString() });
            return View();
        }

        [HttpPost]
        public ActionResult Index(Step1Model step1)
        {

            if (ModelState.IsValid)
            {
                if (data == null) data = new DataRepository(db);
                Response.Cookies["Step1Cookies"]["HoTen"] = step1.HoTen;
                Response.Cookies["Step1Cookies"]["SoThe"] = step1.SoThe;
                Response.Cookies["Step1Cookies"]["Khoa"] = step1.Khoa;
                Response.Cookies["Step1Cookies"]["DienThoai"] = step1.DienThoai;
                Response.Cookies["Step1Cookies"]["CourseID"] = step1.CourseID;
                Response.Cookies["Step1Cookies"]["TenMon"] = step1.TenMon;
                Response.Cookies["Step1Cookies"]["Nganh"] = step1.Nganh;
                Response.Cookies["Step1Cookies"]["Nhom"] = step1.Nhom;
                Response.Cookies["Step1Cookies"]["HK"] = step1.HK;
                Response.Cookies["Step1Cookies"]["DuTruFrom"] = step1.DuTruFrom.ToString();
                Response.Cookies["Step1Cookies"]["DuTruTo"] = step1.DuTruTo.ToString();
                Response.Cookies["Step1Cookies"]["SoLuongSV"] = step1.SoLuongSV;
                Response.Cookies["Step1Cookies"].Expires = DateTime.Now.AddDays(1);
                return RedirectToAction("Step2");
            }
            else
            {
                ViewBag.HK = db.HK.Select(m => new SelectListItem { Text = m.Hocky, Value = m.ID.ToString() });
                TempData["data"] = null;
                return View(step1);
            }
            
        }


        public ActionResult Step2()
        {
            if (Request.Cookies["Step1Cookies"] == null) return RedirectToAction("Index");

            Step2Model model = new Step2Model();
            ViewBag.sId = null;
            ViewBag.tId = null;
            return View(model);
        }

        [HttpPost]
        public ActionResult Step2(Step2Model step2, int sId, int tId)
        {
            Step1Model step1 = new Step1Model();
            if (ModelState.IsValid)
            {
                var from = DateTime.ParseExact(Server.HtmlEncode(Request.Cookies["Step1Cookies"]["DuTruFrom"]), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                var to = DateTime.ParseExact(Server.HtmlEncode(Request.Cookies["Step1Cookies"]["DuTruTo"]), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                step1.HoTen = Server.HtmlEncode(Request.Cookies["Step1Cookies"]["HoTen"]);
                step1.SoThe = Server.HtmlEncode(Request.Cookies["Step1Cookies"]["SoThe"]);
                step1.Khoa = Server.HtmlEncode(Request.Cookies["Step1Cookies"]["Khoa"]);
                step1.DienThoai = Server.HtmlEncode(Request.Cookies["Step1Cookies"]["DienThoai"]);
                step1.CourseID = Server.HtmlEncode(Request.Cookies["Step1Cookies"]["CourseID"]);
                step1.TenMon = Server.HtmlEncode(Request.Cookies["Step1Cookies"]["TenMon"]);
                step1.Nganh = Server.HtmlEncode(Request.Cookies["Step1Cookies"]["Nganh"]);
                step1.Nhom = Server.HtmlEncode(Request.Cookies["Step1Cookies"]["Nhom"]);
                step1.HK = Server.HtmlEncode(Request.Cookies["Step1Cookies"]["HK"]);
                step1.DuTruFrom = from;
                step1.DuTruTo = to;
                step1.SoLuongSV = Server.HtmlEncode(Request.Cookies["Step1Cookies"]["SoLuongSV"]);

                data.InsertData(tId, sId, step1, step2);
                Response.Cookies["Step1Cookies"].Expires = DateTime.Now.AddDays(-1);
                return RedirectToAction("Index");
            }
            ViewBag.sId = sId;
            ViewBag.tId = tId;

            var errors = ViewData.ModelState.Where(n => n.Value.Errors.Count > 0).ToList();
            ViewBag.Error = errors;
            return View(step2);
        }
    }
}
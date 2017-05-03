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
                Session["user"] = data.FirstData(step1);
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
            // if (Session["user"] == null) return RedirectToAction("Index");

            Step2Model model = new Step2Model();
            ViewBag.sId = null;
            ViewBag.tId = null;
            return View(model);
        }

        [HttpPost]
        public ActionResult Step2(Step2Model step2, int sId, int tId)
        {
            var a = Session["user"];
            if (ModelState.IsValid)
            {
                
                return RedirectToAction("Index");
            }
            ViewBag.sId = sId;
            ViewBag.tId = tId;

            return View(step2);
        }
    }
}
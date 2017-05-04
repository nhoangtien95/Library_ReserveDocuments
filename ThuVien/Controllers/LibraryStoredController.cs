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
        public LibraryStoredController()
        {
            IData _data = data;
        }

        public LibraryStoredController(IData _data)
        {
            this.data = _data;
        }

        public ActionResult Index()
        {
            ViewBag.HK = db.HK.Select(m => new SelectListItem { Text = m.Hocky, Value = m.ID.ToString() });
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

                ////////////////////////////////////////////////////////

                string ReserveType;
                int hk = Int32.Parse(step1.HK);
                var tenHK = db.HK.FirstOrDefault(x => x.STT == hk);

                var MonHoc = new MonHoc()
                {
                    CourseID = step1.CourseID,
                    TenMon = step1.TenMon,
                    Nganh = step1.Nganh,
                    DuTruFrom = step1.DuTruFrom,
                    DuTruTo = step1.DuTruTo,
                    Nhom = step1.Nhom,
                    SoLuongSV = step1.SoLuongSV,
                    HK = tenHK.Hocky
                };

                db.MonHoc.Add(MonHoc);

                db.SaveChanges();

                switch (step2.MucDich)
                {
                    case "0":
                        {
                            ReserveType = "Giáo Trình";
                            break;
                        }
                    case "1":
                        {
                            ReserveType = "Tham Khảo Chính";
                            break;
                        }
                    default:
                        {
                            ReserveType = "Tham Khảo Khác";
                            break;
                        }
                }

                if (tId == 0)
                {
                    switch (sId)
                    {
                        case 0:
                            {
                                var document = new TL_Sach()
                                {
                                    Loai = "Sách",
                                    Nguon = "Thư Viện",
                                    TraCuu = step2.TL_Sach.TraCuu,
                                    ChiTiet = step2.TL_Sach.ChiTiet,
                                    MucDich = ReserveType,
                                    YKien = step2.TL_Sach.YKien,
                                    LanXuatBan = "Không",
                                    NhanDe = "Không",
                                    TacGia = "Không",
                                    NXB = "Không",
                                };

                                if (step2.DangDuTru == "0")
                                {
                                    document.DangDuTru = "Dự trữ dạng giấy";
                                    document.SoLuong = step2.SoLuong;
                                }
                                else
                                {
                                    document.DangDuTru = "Dự trữ dạng điện tử";
                                    document.SoLuong = null;
                                }

                                db.TL_Sach.Add(document);

                                db.SaveChanges();

                                var gv = new GiangVien()
                                {
                                    HoTen = step1.HoTen,
                                    DienThoai = step1.DienThoai,
                                    Khoa = step1.Khoa,
                                    SoThe = step1.SoThe,
                                    MonHocId = MonHoc.ID,
                                    BookId = document.ID
                                };

                                db.GiangVien.Add(gv);

                                db.SaveChanges();

                                break;
                            }
                        case 1:
                            {
                                var document = new TL_Sach()
                                {
                                    Loai = "Sách",
                                    Nguon = "Giảng Viên",
                                    TraCuu = "Không",
                                    NhanDe = step2.TL_Sach.NhanDe,
                                    TacGia = step2.TL_Sach.TacGia,
                                    NXB = step2.TL_Sach.NXB,
                                    LanXuatBan = step2.TL_Sach.LanXuatBan,
                                    MucDich = ReserveType,
                                    ChiTiet = step2.TL_Sach.ChiTiet,
                                    YKien = step2.TL_Sach.YKien
                                };

                                if (step2.DangDuTru == "0")
                                {
                                    document.DangDuTru = "Dự trữ dạng giấy";
                                    document.SoLuong = step2.SoLuong;
                                }
                                else
                                {
                                    document.DangDuTru = "Dự trữ dạng điện tử";
                                    document.SoLuong = null;
                                }

                                db.TL_Sach.Add(document);
                                db.SaveChanges();

                                var gv = new GiangVien()
                                {
                                    HoTen = step1.HoTen,
                                    DienThoai = step1.DienThoai,
                                    Khoa = step1.Khoa,
                                    SoThe = step1.SoThe,
                                    MonHocId = MonHoc.ID,
                                    BookId = document.ID
                                };

                                db.GiangVien.Add(gv);
                                db.SaveChanges();
                                break;
                            }
                        default:
                            {
                                var document = new TL_Sach()
                                {
                                    Loai = "Sách",
                                    Nguon = "Khác",
                                    NhanDe = step2.TL_Sach.NhanDe,
                                    TraCuu = "Không",
                                    TacGia = step2.TL_Sach.TacGia,
                                    NXB = step2.TL_Sach.NXB,
                                    LanXuatBan = step2.TL_Sach.LanXuatBan,
                                    ChiTiet = step2.TL_Sach.ChiTiet,
                                    MucDich = ReserveType,
                                    YKien = step2.TL_Sach.YKien
                                };

                                if (step2.DangDuTru == "0")
                                {
                                    document.DangDuTru = "Dự trữ dạng giấy";
                                    document.SoLuong = step2.SoLuong;
                                }
                                else
                                {
                                    document.DangDuTru = "Dự trữ dạng điện tử";
                                    document.SoLuong = null;
                                }

                                db.TL_Sach.Add(document);
                                db.SaveChanges();

                                var gv = new GiangVien()
                                {
                                    HoTen = step1.HoTen,
                                    DienThoai = step1.DienThoai,
                                    Khoa = step1.Khoa,
                                    SoThe = step1.SoThe,
                                    MonHocId = MonHoc.ID,
                                    BookId = document.ID
                                };

                                db.GiangVien.Add(gv);
                                db.SaveChanges();
                                break;
                            }
                    }
                }
                else if (tId == 1)
                {
                    switch (sId)
                    {
                        case 0:
                            {
                                var document = new TL_BaiBao()
                                {
                                    Loai = "Bài Báo",
                                    Nguon = "Thư Viện",
                                    TraCuu = step2.TL_BaiBao.TraCuu,
                                    ChiTiet = step2.TL_BaiBao.ChiTiet,
                                    MucDich = ReserveType,
                                    YKien = step2.TL_BaiBao.YKien,
                                    TacGia = "Không",
                                    TenBai = "Không",
                                    TenTapChi = "Không",
                                    PhatHanh = "Không",
                                };

                                if (step2.DangDuTru == "0")
                                {
                                    document.DangDuTru = "Dự trữ dạng giấy";
                                    document.SoLuong = step2.SoLuong;
                                }
                                else
                                {
                                    document.DangDuTru = "Dự trữ dạng điện tử";
                                    document.SoLuong = null;
                                }

                                db.TL_BaiBao.Add(document);
                                db.SaveChanges();

                                var gv = new GiangVien()
                                {
                                    HoTen = step1.HoTen,
                                    DienThoai = step1.DienThoai,
                                    Khoa = step1.Khoa,
                                    SoThe = step1.SoThe,
                                    MonHocId = MonHoc.ID,
                                    PaperId = document.ID
                                };

                                db.GiangVien.Add(gv);
                                db.SaveChanges();
                                break;
                            }
                        case 1:
                            {
                                var document = new TL_BaiBao()
                                {
                                    Loai = "Bài Báo",
                                    Nguon = "Giảng Viên",
                                    TraCuu = "Không",
                                    TacGia = step2.TL_BaiBao.TacGia,
                                    TenBai = step2.TL_BaiBao.TenBai,
                                    TenTapChi = step2.TL_BaiBao.TenTapChi,
                                    PhatHanh = step2.TL_BaiBao.PhatHanh,
                                    ChiTiet = step2.TL_BaiBao.ChiTiet,
                                    MucDich = ReserveType,
                                    YKien = step2.TL_BaiBao.YKien
                                };

                                if (step2.DangDuTru == "0")
                                {
                                    document.DangDuTru = "Dự trữ dạng giấy";
                                    document.SoLuong = step2.SoLuong;
                                }
                                else
                                {
                                    document.DangDuTru = "Dự trữ dạng điện tử";
                                    document.SoLuong = null;
                                }

                                db.TL_BaiBao.Add(document);
                                db.SaveChanges();

                                var gv = new GiangVien()
                                {
                                    HoTen = step1.HoTen,
                                    DienThoai = step1.DienThoai,
                                    Khoa = step1.Khoa,
                                    SoThe = step1.SoThe,
                                    MonHocId = MonHoc.ID,
                                    PaperId = document.ID
                                };

                                db.GiangVien.Add(gv);
                                db.SaveChanges();
                                break;
                            }
                        default:
                            {
                                var document = new TL_BaiBao()
                                {
                                    Loai = "Bài Báo",
                                    Nguon = "Khác",
                                    TacGia = step2.TL_BaiBao.TacGia,
                                    TenBai = step2.TL_BaiBao.TenBai,
                                    TenTapChi = step2.TL_BaiBao.TenTapChi,
                                    PhatHanh = step2.TL_BaiBao.PhatHanh,
                                    ChiTiet = step2.TL_BaiBao.ChiTiet,
                                    MucDich = ReserveType,
                                    YKien = step2.TL_BaiBao.YKien,
                                    TraCuu = "Không"
                                };

                                if (step2.DangDuTru == "0")
                                {
                                    document.DangDuTru = "Dự trữ dạng giấy";
                                    document.SoLuong = step2.SoLuong;
                                }
                                else
                                {
                                    document.DangDuTru = "Dự trữ dạng điện tử";
                                    document.SoLuong = null;
                                }

                                db.TL_BaiBao.Add(document);
                                db.SaveChanges();

                                var gv = new GiangVien()
                                {
                                    HoTen = step1.HoTen,
                                    DienThoai = step1.DienThoai,
                                    Khoa = step1.Khoa,
                                    SoThe = step1.SoThe,
                                    MonHocId = MonHoc.ID,
                                    PaperId = document.ID
                                };

                                db.GiangVien.Add(gv);
                                db.SaveChanges();
                                break;
                            }
                    }
                }
                else
                {
                    switch (sId)
                    {
                        case 0:
                            {
                                var document = new TL_Khac()
                                {
                                    Loai = "Khác",
                                    Nguon = "Thư Viện",
                                    TraCuu = step2.TL_Khac.TraCuu,
                                    MucDich = ReserveType,
                                    YKien = step2.TL_Khac.YKien,
                                    NhanDe = "Không",
                                    TacGia = "Không"
                                };

                                db.TL_Khac.Add(document);
                                db.SaveChanges();

                                var gv = new GiangVien()
                                {
                                    HoTen = step1.HoTen,
                                    DienThoai = step1.DienThoai,
                                    Khoa = step1.Khoa,
                                    SoThe = step1.SoThe,
                                    MonHocId = MonHoc.ID,
                                    OtherId = document.ID
                                };

                                db.GiangVien.Add(gv);
                                db.SaveChanges();
                                break;
                            }
                        case 1:
                            {
                                var document = new TL_Khac()
                                {
                                    Loai = "Khác",
                                    Nguon = "Giảng Viên",
                                    TraCuu = "Không",
                                    NhanDe = step2.TL_Khac.NhanDe,
                                    TacGia = step2.TL_Khac.TacGia,
                                    MucDich = ReserveType,
                                    YKien = step2.TL_BaiBao.YKien
                                };

                                db.TL_Khac.Add(document);
                                db.SaveChanges();

                                var gv = new GiangVien()
                                {
                                    HoTen = step1.HoTen,
                                    DienThoai = step1.DienThoai,
                                    Khoa = step1.Khoa,
                                    SoThe = step1.SoThe,
                                    MonHocId = MonHoc.ID,
                                    OtherId = document.ID
                                };

                                db.GiangVien.Add(gv);
                                db.SaveChanges();
                                break;
                            }
                        default:
                            {
                                var document = new TL_Khac()
                                {
                                    Loai = "Khác",
                                    Nguon = "Giảng Viên",
                                    TraCuu = "Không",
                                    NhanDe = step2.TL_Khac.NhanDe,
                                    TacGia = step2.TL_Khac.TacGia,
                                    MucDich = ReserveType,
                                    YKien = step2.TL_Khac.YKien
                                };

                                db.TL_Khac.Add(document);
                                db.SaveChanges();

                                var gv = new GiangVien()
                                {
                                    HoTen = step1.HoTen,
                                    DienThoai = step1.DienThoai,
                                    Khoa = step1.Khoa,
                                    SoThe = step1.SoThe,
                                    MonHocId = MonHoc.ID,
                                    OtherId = document.ID
                                };

                                db.GiangVien.Add(gv);
                                db.SaveChanges();
                                break;
                            }
                    }
                }

                //////////////////////////////////

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
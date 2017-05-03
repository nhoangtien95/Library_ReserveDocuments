using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThuVien.Models;
using ThuVien.ViewModels;

namespace ThuVien.DAL
{
    public class DataRepository : IData, IDisposable
    {
        private DB db = new DB();

        //public List<Step1Model> Items { get; } = new List<Step1Model>();

        public Step1Model FirstModel { get; set; } = new Step1Model();
        public DataRepository(DB db)
        {
            this.db = db;
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Step1Model FirstData(Step1Model step1)
        {
            FirstModel.HoTen = step1.HoTen;
            FirstModel.SoThe = step1.SoThe;
            FirstModel.Khoa = step1.Khoa;
            FirstModel.DienThoai = step1.DienThoai;
            FirstModel.CourseID = step1.CourseID;
            FirstModel.TenMon = step1.TenMon;
            FirstModel.Nganh = step1.Nganh;
            FirstModel.Nhom = step1.Nhom;
            FirstModel.HK = step1.HK;
            FirstModel.DuTruFrom = step1.DuTruFrom;
            FirstModel.DuTruTo = step1.DuTruTo;
            FirstModel.SoLuongSV = step1.SoLuongSV;

            return FirstModel;
        }
        public Step1Model GetData()
        {
                return FirstModel;
        }

        

        public IEnumerable<SelectListItem> getHK()
        {
            return db.HK.Select(m => new SelectListItem { Text = m.Hocky, Value = m.ID.ToString() });
        }

        public void InsertData(int tId, int sId, Step1Model step1, Step2Model step2)
        {
            string ReserveType;
            var tenHK = db.HK.FirstOrDefault(x => x.STT == Int32.Parse(step1.HK));

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

            if ( tId == 0)
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

                            break;
                        }
                    case 1:
                        {
                            var document = new TL_Sach()
                            {
                                Loai = "Sách",
                                Nguon = "Giảng Viên",
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
                            break;
                        }
                    default:
                        {
                            var document = new TL_Sach()
                            {
                                Loai = "Sách",
                                Nguon = "Khác",
                                NhanDe = step2.TL_Sach.NhanDe,
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
                            break;
                        }
                }
            } else if (tId == 1)
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
                            break;
                        }
                    case 1:
                        {
                            var document = new TL_BaiBao()
                            {
                                Loai = "Bài Báo",
                                Nguon = "Giảng Viên",
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
                                TraCuu = step2.TL_BaiBao.TraCuu,
                                MucDich = ReserveType,
                                YKien = step2.TL_BaiBao.YKien
                            };
                            break;
                        }
                    case 1:
                        {
                            var document = new TL_Khac()
                            {
                                Loai = "Khác",
                                Nguon = "Giảng Viên",
                                NhanDe = step2.TL_Khac.NhanDe,
                                TacGia = step2.TL_Khac.TacGia,                                
                                MucDich = ReserveType,
                                YKien = step2.TL_BaiBao.YKien
                            };
                            break;
                        }
                    default:
                        {
                            var document = new TL_Khac()
                            {
                                Loai = "Khác",
                                Nguon = "Giảng Viên",
                                NhanDe = step2.TL_Khac.NhanDe,
                                TacGia = step2.TL_Khac.TacGia,
                                MucDich = ReserveType,
                                YKien = step2.TL_BaiBao.YKien
                            };
                            break;
                        }
                }
            }

           
        }
    }
}
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
            string Reserve;
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
                HK = tenHK.ToString()
            };

            db.MonHoc.Add(MonHoc);

            if (step2.DangDuTru == "0")
            {
                Reserve = "Dự trữ dạng giấy";
            }
            else
            {
                Reserve = "Dự trữ dạng điện tử";
            }

            if ( tId == 0)
            {
                switch (sId)
                {
                    case 0:
                        {
                            var document = new TL_Sach()
                            {
                                Nguon = "Thư Viện",
                                TraCuu = step2.TL_Sach.TraCuu,
                                ChiTiet = step2.TL_Sach.ChiTiet,
                                DangDuTru = Reserve
                            };
                            break;
                        }
                    case 1:
                        {
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            } else if (tId == 1)
            {

            }
            else
            {

            }

           
        }
    }
}
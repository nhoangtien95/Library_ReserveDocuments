using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThuVien.Models;

namespace ThuVien.ViewModels
{
    public class DataViewModel
    {
        //public List<GiangVien> gv { get; set; }
        //public List<MonHoc> mh { get; set; }
        //public List<TL_Sach> TL_Sach { get; set; }
        //public List<TL_BaiBao> TL_BaiBao { get; set; }
        //public List<TL_Khac> TL_Khac { get; set; }

        public GiangVien gv { get; set; }
        public MonHoc mh { get; set; }
        public TL_Sach TL_Sach { get; set; }
        public TL_BaiBao TL_BaiBao { get; set; }
        public TL_Khac TL_Khac { get; set; }
    }
}
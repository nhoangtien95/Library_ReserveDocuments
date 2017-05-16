using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ThuVien.Models;

namespace ThuVien.ViewModels
{
    public class DataViewModel
    {
        public class Status
        {
            public int SourceId { get; set; }
            public string Value { get; set; }
        }

        public IEnumerable<Status> StatusOptions = new List<Status>
        {
            new Status {SourceId = 0, Value = "Chưa xử lý"},
            new Status {SourceId = 1, Value = "Đang xử lý"},
            new Status {SourceId = 2, Value = "Đã xử lý"}
        };

        public GiangVien gv { get; set; }
        public MonHoc mh { get; set; }
        public TL_Sach TL_Sach { get; set; }
        public TL_BaiBao TL_BaiBao { get; set; }
        public TL_Khac TL_Khac { get; set; }
    }
}
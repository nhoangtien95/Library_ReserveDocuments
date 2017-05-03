using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ThuVien.Models;

namespace ThuVien.ViewModels
{
    public class Step2Model
    {
        public class Type
        {
            public int TypeId { get; set; }
            public string Value { get; set; }        
        }

        public IEnumerable<Type> TypeOptions = new List<Type>
        {
            new Type {TypeId = 0, Value = "Sách" },
            new Type {TypeId = 1, Value = "Bài Báo" },
            new Type {TypeId = 2, Value = "Khác" }
        };
    
        public class Source
        {
            public int SourceId { get; set; }
            public string Value { get; set; }
        }

        public IEnumerable<Source> SourceOptions = new List<Source>
        {
            new Source {SourceId = 0, Value = "Thư Viện"},
            new Source {SourceId = 1, Value = "Giảng Viên"},
            new Source {SourceId = 2, Value = "Khác"}
        };

        public class Reserve
        {
            public int ReserveId { get; set; }
            public string Value { get; set; }
        }

        public IEnumerable<Reserve> ReserveOptions = new List<Reserve>
        {
            new Reserve {ReserveId = 0, Value = "Dự trữ dạng giấy"},
            new Reserve {ReserveId = 1, Value = "Dự trữ dạng điện tử"}
        };

        public class Purpose
        {
            public int PurposeId { get; set; }
            public string Value { get; set; }
        }

        public IEnumerable<Purpose> PurposeOptions = new List<Purpose>
        {
            new Purpose {PurposeId = 0, Value = "Giáo trình"},
            new Purpose {PurposeId = 1, Value = "Tham khảo chính"},
            new Purpose {PurposeId = 2, Value = "Tham khảo khác"}
        };



        [Required(ErrorMessage = "Vui lòng không bỏ trống Nguồn cung cấp. ")]
        [Display(Name = "Nguồn cung cấp")]
        public string Nguon { get; set; }

        public string Loai { get; set; }

        public bool checkSource
        {
            get
            {
                return Loai == "2";
            }
        }


        [RequiredIf("checkSource", false, ErrorMessage = "Vui lòng không bỏ trống Dạng dự trữ.")]
        [Display(Name = "Dạng dự trữ")]
        public string DangDuTru { get; set; }

        public bool quantity
        {
            get
            {
                return DangDuTru == "0";
            }
        }

        [RequiredIf("quantity", true, ErrorMessage = "Vui lòng không bỏ trống Số lượng bản giấy đề nghị dự trữ.")]
        [Display(Name = "Số lượng bản giấy đề nghị dự trữ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Xin vui lòng nhập số. ")]
        public string SoLuong { get; set; }

       
        
        public TL_Sach TL_Sach { get; set; }
        public TL_BaiBao TL_BaiBao { get; set; }
        public TL_Khac TL_Khac { get; set; }


    }
}
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThuVien.Models
{
    public class TL_Khac
    {
        public bool SourceRequire
        {
            get
            {
                return Nguon == "0";
            }
        }

        public int ID { get; set; }
        public string Loai { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống Nguồn cung cấp. ")]
        [Display(Name = "Nguồn cung cấp")]
        public string Nguon { get; set; }

        [RequiredIf("SourceRequire", true, ErrorMessage = "Vui lòng không bỏ trống Tra cứu mục lục thư viện.")]
        [Display(Name = "Tra cứu mục lục thư viện")]
        public string TraCuu { get; set; }

        [RequiredIf("SourceRequire", false, ErrorMessage = "Vui lòng không bỏ trống Nhan đề.")]
        [Display(Name = "Nhan đề")]
        public string NhanDe { get; set; }

        [RequiredIf("SourceRequire", false, ErrorMessage = "Vui lòng không bỏ trống Tác giả.")]
        [Display(Name = "Tác giả")]
        public string TacGia { get; set; }

        //[Required(ErrorMessage = "Vui lòng không bỏ trống Mục đích sử dụng. ")]
        [Display(Name = "Mục đích sử dụng")]
        public string MucDich { get; set; }

        [Display(Name = "Ý kiến")]
        public string YKien { get; set; }

        //public virtual TaiLieu TaiLieu { get; set; }
    }
}
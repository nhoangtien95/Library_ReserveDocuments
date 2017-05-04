using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThuVien.Models
{
    public class TL_Sach
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

        [RequiredIf("SourceRequire", false, ErrorMessage = "Vui lòng không bỏ trống Nhà xuất bản.")]
        [Display(Name = "Nhà xuất bản")]
        public string NXB { get; set; }

        [RequiredIf("SourceRequire", false, ErrorMessage = "Vui lòng không bỏ trống Lần xuất bản/Năm xuất bản.")]
        [Display(Name = "Lần xuất bản/Năm xuất bản")]
        public string LanXuatBan { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống Chi tiết đề nghị dự trữ. ")]
        [Display(Name = "Chi tiết đề nghị dự trữ")]
        public string ChiTiet { get; set; }

        //[Required(ErrorMessage = "Vui lòng không bỏ trống Dạng dự trữ. ")]
        [Display(Name = "Dạng dự trữ")]
        public string DangDuTru { get; set; }
        
        [Display(Name = "Số lượng bản giấy đề nghị dự trữ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Xin vui lòng nhập số. ")]
        public string SoLuong { get; set; }             

        //[Required(ErrorMessage = "Vui lòng không bỏ trống Mục đích sử dụng. ")]
        [Display(Name = "Mục đích sử dụng")]
        public string MucDich { get; set; }

        [Display(Name = "Ý kiến")]
        public string YKien { get; set; }

        public virtual TaiLieu TaiLieu { get; set; }

        
    }
}
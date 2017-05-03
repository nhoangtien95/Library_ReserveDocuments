using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThuVien.Models
{
    public class GiangVien
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Họ tên giảng viên:")]
        [StringLength(50, ErrorMessage = "Họ tên không dài quá 50 ký tự. ")]
        public string HoTen { get; set; }

        [Required]
        [Display(Name = "Số thẻ:")]
        [StringLength(20, ErrorMessage = "Số thẻ không dài quá 20 ký tự. ")]
        public string SoThe { get; set; }

        [Required]
        [Display(Name = "Khoa:")]
        [StringLength(20, ErrorMessage = "Tên khoa không dài quá 20 ký tự. ")]
        public string Khoa { get; set; }

        [Required]
        [Display(Name = "Số điện thoại:")]
        [StringLength(13, ErrorMessage = "Số điện thoại không dài quá 13. ", MinimumLength = 2)]
        public string DienThoai { get; set; }

        public int MonHocId { get; set; }
        public int? BookId { get; set; }
        public int? PaperId { get; set; }
        public int? OtherId { get; set; }
    }
}
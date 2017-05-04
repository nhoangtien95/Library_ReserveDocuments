using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ThuVien.Models;

namespace ThuVien.ViewModels
{
    public class Step1Model
    {
        [Required(ErrorMessage = "Vui lòng không bỏ trống. ")]
        [Display(Name = "Họ tên giảng viên:")]
        [StringLength(50, ErrorMessage = "Họ tên không dài quá 50 ký tự. ")]
        public string HoTen { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống. ")]
        [Display(Name = "Số thẻ:")]
        [StringLength(20, ErrorMessage = "Số thẻ không dài quá 20 ký tự. ")]
        public string SoThe { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống. ")]
        [Display(Name = "Khoa:")]
        [StringLength(20, ErrorMessage = "Tên khoa không dài quá 20 ký tự. ")]
        public string Khoa { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống. ")]
        [Display(Name = "Số điện thoại:")]
        [StringLength(13, ErrorMessage = "Số điện thoại không dài quá 13. ")]
        public string DienThoai { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống. ")]
        [Display(Name = "Mã môn học")]
        [StringLength(20, ErrorMessage = "Mã môn học không dài quá 20 ký tự. ")]
        public string CourseID { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống. ")]
        [Display(Name = "Tên môn học")]
        [StringLength(60, ErrorMessage = " Tên môn học không dài quá 60 ký tự. ")]
        public string TenMon { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống. ")]
        [Display(Name = "Chuyên ngành đào tạo")]
        [StringLength(60, ErrorMessage = " Tên chuyên ngành không dài quá 60 ký tự. ")]
        public string Nganh { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống. ")]
        [Display(Name = "Mã lớp")]
        public string Nhom { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống. ")]
        [Display(Name = "Học kỳ/Năm học")]
        [StringLength(20, ErrorMessage = " Học kỳ/Năm học không dài quá 20 ký tự. ")]
        public string HK { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống. ")]
        [DataType(DataType.Date)]
        [Display(Name = "Dự trữ từ:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DuTruFrom { get; set; }

        [Required(ErrorMessage = "Vui lòng không bỏ trống. ")]
        [DataType(DataType.Date)]
        [Display(Name = "Dự trữ đến:")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime DuTruTo { get; set; }

        [Display(Name = "Số lượng sinh viên cần tiếp cận tài liệu này: ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Xin vui lòng nhập số. ")]
        public string SoLuongSV { get; set; }
    }
}
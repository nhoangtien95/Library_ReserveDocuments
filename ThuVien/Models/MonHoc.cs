using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ThuVien.Models
{
    public class MonHoc
    {
        public int ID { get; set; }

        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Mã môn học")]
        public string CourseID { get; set; }

        [Required]
        [Display(Name = "Tên môn học")]
        [StringLength(60, ErrorMessage = " Tên môn học không dài quá 60 ký tự. ")]
        public string TenMon { get; set; }

        [Required]
        [Display(Name = "Chuyên ngành đào tạo")]
        [StringLength(60, ErrorMessage = " Tên chuyên ngành không dài quá 60 ký tự. ")]
        public string Nganh { get; set; }

        [Required]
        [Display(Name = "Mã nhóm lớp")]
        [Range(0, 5)]
        public string Nhom { get; set; }

        [Required]
        [Display(Name = "Học kỳ/Năm học")]
        [StringLength(20, ErrorMessage = " Học kỳ/Năm học không dài quá 20 ký tự. ")]
        public string HK { get; set; }

        //[Required]
        [Display(Name = "Thời Gian Dự Trữ")]
        public DateTime DuTruFrom { get; set; }

        //[Required]
        public DateTime DuTruTo { get; set; }

        [Display(Name = "Số lượng sinh viên cần tiếp cận tài liệu này: ")]
        public string SoLuongSV { get; set; }
    }
}
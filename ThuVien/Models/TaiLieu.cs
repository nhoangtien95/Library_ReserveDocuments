using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ThuVien.Models
{
    public class TaiLieu
    {
        public int ID { get; set; }
        
        [Display(Name = "Loại tài liệu")]
        public string LoaiTaiLieu { get; set; }

        public virtual ICollection<TL_Sach> TL_Sach { get; set; }
        public virtual ICollection<TL_BaiBao> TL_BaiBao { get; set; }
        public virtual ICollection<TL_Khac> TL_Khac { get; set; }
    }
}
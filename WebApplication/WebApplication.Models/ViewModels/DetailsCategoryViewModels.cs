using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models.ViewModels
{
    public class DetailsCategoryViewModels
    {
        public int ID { get; set; }
        public System.Guid GUID { get; set; }
        [Display(Name = "Danh mục cha")]
        public string Parent { get; set; }
        [Display(Name = "Tên Danh mục")]
        public string Title { get; set; }
        [Display(Name = "Mô tả")]
        public string Description { get; set; }
        [Display(Name = "Link")]
        public string Url { get; set; }
        [Display(Name = "Mức độ ưu tiên")]
        public Nullable<int> SortOrder { get; set; }
        [Display(Name = "Trạng thái")]
        public string Status { get; set; }
        [Display(Name = "Người tạo")]
        public Nullable<int> CreatedBy { get; set; }
        [Display(Name = "Ngày tạo")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [Display(Name = "Người sửa")]
        public Nullable<int> ModifiedBy { get; set; }
        [Display(Name = "Ngày sửa")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

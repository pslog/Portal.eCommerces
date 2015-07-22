using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models.MetaData
{
    public partial class cms_Categories
    {
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }
        [DisplayName("Mô tả")]
        public string Description { get; set; }
        [DisplayName("Tạo bởi")]
        public Nullable<int> CreatedBy { get; set; }
        [DisplayName("Ngày tạo")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DisplayName("Sửa bởi")]
        public Nullable<int> ModifiedBy { get; set; }
        [DisplayName("Ngày sửa")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }

    public partial class cms_News
    {
        [DisplayName("Tiêu đề")]
        public string Title { get; set; }
        [DisplayName("Tóm tắt")]
        public string SubTitle { get; set; }
        [DisplayName("Nội dung")]
        public string ContentNews { get; set; }
        [DisplayName("Tác giả")]
        public string Authors { get; set; }
        [DisplayName("Lượt xem")]
        public Nullable<int> TotalView { get; set; }
        [DisplayName("Tạo bởi")]
        public Nullable<int> CreatedBy { get; set; }
        [DisplayName("Ngày tạo")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [DisplayName("Sửa bởi")]
        public Nullable<int> ModifiedBy { get; set; }
        [DisplayName("Ngày sửa")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}

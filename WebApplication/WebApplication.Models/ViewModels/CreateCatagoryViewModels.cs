using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models.Models;

namespace WebApplication.Models.ViewModels
{
    public class CreateCatagoryViewModels
    {
        public int ID { get; set; }
        public System.Guid GUID { get; set; }
        public System.Guid ParentID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public IEnumerable<product_Categories> listParentCategories { get; set; }
    }
}

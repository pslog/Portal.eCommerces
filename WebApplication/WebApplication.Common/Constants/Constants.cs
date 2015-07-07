using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Common.Constants
{
    public class ModelName
    {
        public class CmsCategory
        {
            public const string ID = "ID";
            public const string Title = "Title";
            public const string GUID = "GUID";
            public const string ParentID = "ParentID";
        }
    }

    public class ExcludeProperties
    {
        public const string CmsCategory = "ID,GUID,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate";
    }

    public class Label
    {
        public class CmsCategory
        {
            public const string CreateTitle = "Thêm danh mục";
            public const string Title = "Tên danh mục";
            public const string Description = "Mô tả";
        }
    }

}

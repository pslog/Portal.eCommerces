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
            public const string ParentTitle = "ParentTitle";
            public const string CreatedBy = "CreatedBy";
            public const string CreatedDate = "CreatedDate";
            public const string ModifiedBy = "ModifiedBy";
            public const string ModifiedDate = "ModifiedDate";
        }
    }

    public class ExcludeProperties
    {
        public const string CmsCategory = "ID,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate";
    }

    public class Label
    {
        public class CmsCategory
        {
            public const string CreateTitle = "Thêm danh mục";
            public const string CreateSubCategory = "Thêm mục con";
            public const string Title = "Tên danh mục";
            public const string Description = "Mô tả";
            public const string ParentID = "Ông già";
            public const string CreatedBy = "Người thêm";
            public const string CreatedDate = "Ngày thêm";
            public const string ModifiedBy = "Người sửa";
            public const string ModifiedDate = "Ngày sửa";
        }

        public class CRUD
        {
            public const string Create = "Thêm";
            public const string Details = "Chi tiết";
            public const string Edit = "Sửa";
            public const string Delete = "Xóa";
        }
    }

    public class PageTitle
    {
        public class CmsCategory
        {
            public const string Index = "Danh sách danh mục";
        }
    }

    public class RouteName
    {
        public class CmsCategory
        {
            public const string Controller = "Cms";
            public const string CmsCategoryIndex = "CmsCategoryIndex";
            public const string CreateCmsCategory = "CreateCmsCategory";
            public const string CmsCategoryDetails = "CmsCategoryDetails";
            public const string EditCmsCategory = "EditCmsCategory";
            public const string DeleteCmsCategory = "DeleteCmsCategory";
        }
    }

    public class ConstValue
    {
        public const int PageSize = 5;
    }

}

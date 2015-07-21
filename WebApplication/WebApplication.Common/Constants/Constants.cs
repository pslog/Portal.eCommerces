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

        public class CmsNews
        {
            public const string ID = "ID";
            public const string GUID = "GUID";
            public const string CategoryID = "CategoryID";
            public const string Title = "Title";
            public const string SubTitle = "SubTitle";
            public const string ContentNews = "ContentNews";
            public const string Authors = "Authors";
            public const string Tags = "Tags";
            public const string TotalView = "TotalView";
            public const string Status = "Status";
            public const string CreatedBy = "CreatedBy";
            public const string CreatedDate = "CreatedDate";
            public const string ModifiedBy = "ModifiedBy";
            public const string ModifiedDate = "ModifiedDate";
        }

        public class PagingRouteValue
        {
            public const string ActionName = "ActionName";
            public const string ControllerName = "ControllerName";
            public const string SearchKey = "SearchKey";
            public const string OrderBy = "OrderBy";
            public const string OrderByDesc = "OrderByDesc";
            public const string PageNumber = "PageNumber";
            public const string TotalPages = "TotalPages";
            public const string ActivePage = "ActivePage";
        }
    }

    public class Label
    {
        public class CmsCategory
        {
            public const string CreateSubCategory = "Thêm mục con";
            public const string Title = "Tên danh mục";
            public const string Url = "Url";
            public const string Status = "Tình trạng";
            public const string Description = "Mô tả";
            public const string ParentID = "Danh mục con của";
            public const string CreatedBy = "Người thêm";
            public const string CreatedDate = "Ngày thêm";
            public const string ModifiedBy = "Người sửa";
            public const string ModifiedDate = "Ngày sửa";
            public const string RootCategory = "";
        }

        public class CmsNews
        {
            public const string Title = "Tiêu đề";
            public const string Authors = "Tác giả";
            public const string TotalView = "Lượt xem";
        }

        public class Paging
        {
            public const string Search = "Tìm danh mục";
            public const string OrderBy = "Sắp xếp";
            public const string OrderByAsc = "Tăng dần";
            public const string OrderByDesc = "Giảm dần";
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
            public const string Create = "Thêm danh mục";
            public const string Edit = "Sửa danh mục";
            public const string Delete = "Xóa danh mục";
            public const string Details = "Chi tiết danh mục";
        }

        public class CmsNews
        {
            public const string Index = "Danh sách tin tức";
            public const string Create = "Thêm tin tức";
            public const string Edit = "Sửa tin tức";
            public const string Delete = "Xóa tin tức";
            public const string Details = "Chi tiết tin tức";
        }
    }

    public class RouteName
    {
        public class Controller
        {
            public const string Cms = "Cms";
        }

        public class CmsCategoryAction
        {
            public const string Index = "CmsCategoryIndex";
            public const string Create = "CreateCmsCategory";
            public const string Details = "CmsCategoryDetails";
            public const string Edit = "EditCmsCategory";
            public const string Delete = "DeleteCmsCategory";
        }

        public class CmsNewsAction
        {
            public const string Index = "CmsNewsIndex";
            public const string Create = "CreateCmsNews";
            public const string Details = "CmsNewsDetails";
            public const string Edit = "EditCmsNews";
            public const string Delete = "DeleteCmsNews";
        }
    }

    public class PagingOptConst
    {
        public const int Wrapper = 0;
        public const int FirstLastButton = 1;
        public const int PrevNextButton = 2;
        public const int NumberButton = 3;
        public const int NoneActionString = 4;
        public const string First = "Trang đầu";
        public const string Last = "Trang cuối";
        public const string Prev = "Trang trước";
        public const string Next = "Trang sau";
        public const string PageNumber = "PageNumber";
    }

    public class ConstValue
    {
        public const int PageSize = 10;
        public const int PagingFirstPagesNumber = 1;
        public const int PagingLastPagesNumber = 1;
        public const int PagingMiddlePagesNumber = 3;
    }
}

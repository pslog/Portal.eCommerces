using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models.Models;

namespace WebApplication.Models.ViewModels
{
    public class CmsCategoryView
    {
        public IEnumerable<cms_Categories> CmsCategories { get; set; }
        public PagingRouteValue RouteValue { get; set; }
    }

    public class PagingRouteValue
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string SearchKey { get; set; }
        public string OrderBy { get; set; }
        public bool OrderByDesc { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public PagingRouteValue()
        {
            this.SearchKey = string.Empty;
            this.OrderBy = string.Empty;
            this.OrderByDesc = false;
            this.PageNumber = 1;
        }
    }
}

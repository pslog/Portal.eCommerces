using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models.Models;
using System.Web.Mvc;

namespace WebApplication.Models.ViewModels
{
    public class CmsCategoryCreateView
    {
        public cms_Categories CmsCategory { get; set; }
        public Guid ParentID { get; set; }
        public string ParentTitle { get; set; }
    }

    public class CmsCategoryEditView
    {
        public cms_Categories CmsCategory { get; set; }
        public SelectList Parents { get; set; }
    }

    public class CmsCategoryIndexView
    {
        public IEnumerable<cms_Categories> CmsCategories { get; set; }
        public PagingRouteValue RouteValue { get; set; }
    }

}

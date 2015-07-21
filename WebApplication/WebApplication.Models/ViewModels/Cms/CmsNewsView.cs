using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models.Models;
using System.Web.Mvc;

namespace WebApplication.Models.ViewModels
{
    public class CmsNewsDTO
    {
        public cms_News CmsNews { get; set; }
        public SelectList Categories { get; set; }
    }

    public class CmsNewsIndexViewDTO
    {
        public int? CategoryID { get; set; }
        public PagingRouteValue RouteValue { get; set; }
    }
}

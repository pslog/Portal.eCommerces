using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Ajax;

namespace WebApplication.Models.ViewModels
{

    public class PagingRouteValue
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string SearchKey { get; set; }
        public string OrderBy { get; set; }
        public bool OrderByDesc { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }

        public AjaxOptions AjaxOptions { get; set; }
        public PagingRouteValue()
        {
            this.SearchKey = string.Empty;
            this.OrderBy = string.Empty;
            this.OrderByDesc = false;
            this.PageNumber = 1;
            this.AjaxOptions = null;
        }

        public PagingRouteValue(AjaxOptions ajaxOptions) : this()
        {
            this.AjaxOptions = ajaxOptions;
        }
    }

    public class PagingGeneratorOption
    {
        public bool DisplayAllNumber { get; set; }
        public bool DisplayFirstLast { get; set; }
        public bool DisplayPrevNext { get; set; }
        public bool AutoHidePrevNext { get; set; }
        public int FirstPageNumbers { get; set; }
        public int LastPageNumbers { get; set; }
        public int MiddlePageNumbers { get; set; }
        
        public static PagingGeneratorOption DefaultOption
        {
            get 
            {
                return new PagingGeneratorOption
                {
                    DisplayAllNumber = false,
                    DisplayFirstLast = true,
                    DisplayPrevNext = true,
                    AutoHidePrevNext = true,
                    FirstPageNumbers = 1,
                    LastPageNumbers = 1,
                    MiddlePageNumbers = 3
                };
            }
        }

        public bool AutoDisplayAllNumber(int totalPages)
        {
            return this.FirstPageNumbers + this.MiddlePageNumbers + this.LastPageNumbers > totalPages - 2;
        }
    }
}

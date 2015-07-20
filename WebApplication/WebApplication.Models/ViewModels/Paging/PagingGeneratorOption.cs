using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Ajax;
using WebApplication.Common.Constants;

namespace WebApplication.Models.ViewModels
{

    public class PagingRouteValue
    {
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string SearchKey { get; set; }
        public string OrderBy { get; set; }
        public bool OrderByDesc { get; set; }
        public string RouteValuePrefix { get; set; }
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public object OptionValues { get; set; }
        public AjaxOptions AjaxOptions { get; set; }
        public PagingRouteValue()
        {
            this.RouteValuePrefix = string.Empty;
            this.SearchKey = string.Empty;
            this.OrderBy = string.Empty;
            this.OrderByDesc = false;
            this.PageNumber = 1;
            this.AjaxOptions = null;
            this.OptionValues = null;
        }

        public PagingRouteValue(string actionName, string controllerName, object optionValues = null, AjaxOptions ajaxOption = null, string prefix = "") : this()
        {
            this.ActionName = actionName;
            this.ControllerName = controllerName;
            this.AjaxOptions = ajaxOption;
            this.OptionValues = optionValues;
            this.RouteValuePrefix = prefix;
        }

        public void SetTotalPages(int itemsCount)
        {
            this.TotalPages = itemsCount % ConstValue.PageSize == 0 ? itemsCount / ConstValue.PageSize : itemsCount / ConstValue.PageSize + 1;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models.ViewModels;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using WebApplication.Common.Constants;

namespace WebApplication.Libraries.CustomHtml
{
    public class CustomHtmlGenerator
    {
        public static string DefaultPagingHtml(int option, string action = "", string text = "", bool active = false)
        {
            switch (option)
            {
                case PagingOptConst.Wrapper: return @"<ul class=""pagination"">{0}{1}{2}{3}{4}</ul>";
                case PagingOptConst.PagerButton: return string.Format(@"<li><a class=""pager-item"" href=""{0}"">{1}</a></li>", action, text);
                case PagingOptConst.NumberButton: return string.Format(@"<li><a class=""page-number {0}"" href=""{1}"">{2}</a></li>", active ? "paging-active" : string.Empty, action, text);
                case PagingOptConst.NoneActionString: return string.Format(@"<li><a href=""javascript:;"">...</a></li>");
            }

            return string.Empty;
        }
    }
}

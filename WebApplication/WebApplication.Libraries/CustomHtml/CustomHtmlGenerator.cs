using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Mvc.Ajax;
using WebApplication.Common.Constants;
using WebApplication.Models.ViewModels;
namespace WebApplication.Libraries.CustomHtml
{
    public class CustomHtmlGenerator
    {
        public static string DefaultPagingHtml(int option, MvcHtmlString actionLink = null, bool active = false)
        {
            switch (option)
            {
                case PagingOptConst.Wrapper: return @"<ul class=""pagination"">{0}{1}{2}{3}{4}</ul>";
                case PagingOptConst.FirstLastButton: return string.Format(@"<li class=""paging-first-last"">{0}</li>", actionLink.ToHtmlString());
                case PagingOptConst.PrevNextButton: return string.Format(@"<li class=""paging-prev-next"">{0}</li>", actionLink.ToHtmlString());
                case PagingOptConst.NumberButton: return string.Format(@"<li class=""page-number {0}"">{1}</li>", active ? "paging-active" : string.Empty, actionLink.ToHtmlString());
                case PagingOptConst.NoneActionString: return string.Format(@"<li class=""paging-none-action""><a href=""javascript:;"">...</a></li>");
            }

            return string.Empty;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;
using System.Web.Mvc.Ajax;
using WebApplication.Common.Constants;
using WebApplication.Libraries.Delegates;
using WebApplication.Models.ViewModels;
using WebApplication.Models.Models;


namespace WebApplication.Libraries.Extensions
{
    public static class CustomHtmlHelper
    {
        public static MvcHtmlString PagingGenerator(this HtmlHelper helper, PagingGeneratorOption option, PagingHtmlGenerator generator, PagingRouteValue routeValue)
        {
            if (routeValue.TotalPages < 2)
            {
                return new MvcHtmlString(string.Empty);
            }

            var htmlElements = generator.CreateHtmlElements(helper, option, routeValue);

            return new MvcHtmlString(string.Format(generator(PagingOptConst.Wrapper), htmlElements.Item1, htmlElements.Item2, htmlElements.Item3, htmlElements.Item4, htmlElements.Item5));
        }

        public static MvcHtmlString ToNavigation(this IEnumerable<cms_Categories> cmsCategories, AjaxHelper ajaxHelper, bool hide = false)
        {
            StringBuilder builder = new StringBuilder(string.Format(@"<ul class=""nav nav-pills nav-stacked"" {0}>", hide ? @"style=""display:none;""" : string.Empty));

            foreach (var cmsCategory in cmsCategories)
            {
                builder.Append(string.Format(@"<li id=""category_{0}"">", cmsCategory.ID));
                builder.Append(ajaxHelper.ActionLink(cmsCategory.Title, "GetCmsNews", "Cms", new { CategoryID = cmsCategory.ID }, new AjaxOptions { UpdateTargetId = "cms_news_container" }));

                if (cmsCategory.cms_Categories1 != null && cmsCategory.cms_Categories1.Count > 0)
                {
                    builder.Append(cmsCategory.cms_Categories1.ToNavigation(ajaxHelper, true).ToString());
                }
                

                builder.Append("</li>");
            }

            builder.Append("</ul>");
            return new MvcHtmlString(builder.ToString());
        }
    }
}

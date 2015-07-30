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

        public static MvcHtmlString ToNavigation(this IEnumerable<cms_Categories> cmsCategories, HtmlHelper htmlHelper, AjaxHelper ajaxHelper = null)
        {
            StringBuilder builder = new StringBuilder(string.Format(@"<ul id='nav-sidebox' class='category-items'>"));

            foreach (var cmsCategory in cmsCategories)
            {
                builder.Append(string.Format(@"<li class='level0 subcatemenu'>"));
                if(ajaxHelper == null)
                {
                    builder.Append(htmlHelper.ActionLink(cmsCategory.Title, "GuestCmsNewsIndex", new { categoryID = cmsCategory.ID }));
                }   
                else
                {
                    builder.Append(ajaxHelper.ActionLink(cmsCategory.Title, "GetCmsNews", "Cms", new { CategoryID = cmsCategory.ID }, new AjaxOptions { UpdateTargetId = "cms_news_container" }, new { title = cmsCategory.Title }));
                }
                

                if (cmsCategory.cms_Categories1 != null && cmsCategory.cms_Categories1.Count > 0)
                {
                    builder.Append(cmsCategory.cms_Categories1.ToNavigation(htmlHelper, ajaxHelper).ToString());
                }
                

                builder.Append("</li>");
            }

            builder.Append("</ul>");
            return new MvcHtmlString(builder.ToString());
        }

        public static MvcHtmlString ProductCategoryActionLink(this AjaxHelper ajaxHelper, string linkText, string actionName, string controllerName, object routeValues, AjaxOptions ajaxOptions, object htmlAttributes)
        {
            var repID = Guid.NewGuid().ToString();
            var lnk = ajaxHelper.ActionLink(repID, actionName, controllerName, routeValues, ajaxOptions, htmlAttributes);
            return MvcHtmlString.Create(lnk.ToString().Replace(repID, linkText));
        }
    }
}

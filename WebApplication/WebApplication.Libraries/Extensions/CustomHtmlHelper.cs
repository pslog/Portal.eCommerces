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

    }
}

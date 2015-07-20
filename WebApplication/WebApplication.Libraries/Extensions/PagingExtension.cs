using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.CompilerServices;
using System.Web.Mvc;
using WebApplication.Models.ViewModels;
using System.Web.Routing;
using WebApplication.Common.Constants;
using WebApplication.Libraries.Delegates;
using System.Text;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
namespace WebApplication.Libraries.Extensions
{
    public static class PagingExtension
    {
        public static List<T> ToPageList<T>(this IEnumerable<T> query, int pageSize, int pageNumber)
        {
            return query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }
        public static MvcHtmlString PagingActionLink(this HtmlHelper helper, PagingRouteValue routeValue, string text, string pageType, int pageNumber = 0)
        {
            if (routeValue.AjaxOptions == null)
            {
                return helper.ActionLink(text, routeValue.ActionName, routeValue.ControllerName, routeValue.ToRouteValueDictionary(pageType, pageNumber), null);
            }
            else
            {
                AjaxHelper ajax = new AjaxHelper(helper.ViewContext, helper.ViewDataContainer);

                return ajax.ActionLink(text, routeValue.ActionName, routeValue.ControllerName, routeValue.ToRouteValueDictionary(pageType, pageNumber), routeValue.AjaxOptions, null);
            }
        }
        public static PagingRouteValue AddAjaxOptions(this PagingRouteValue routeValue, AjaxOptions ajaxOptions)
        {
            routeValue.AjaxOptions = ajaxOptions;

            return routeValue;
        }
        public static RouteValueDictionary ToRouteValueDictionary(this PagingRouteValue pagingRouteValue, string pageType, int pageNumber = 0)
        {

            var routeValues = new RouteValueDictionary
            {
                { string.Format("{0}.{1}", pagingRouteValue.RouteValuePrefix, ModelName.PagingRouteValue.SearchKey), pagingRouteValue.SearchKey},
                { string.Format("{0}.{1}", pagingRouteValue.RouteValuePrefix, ModelName.PagingRouteValue.OrderBy), pagingRouteValue.OrderBy},
                { string.Format("{0}.{1}", pagingRouteValue.RouteValuePrefix, ModelName.PagingRouteValue.OrderByDesc), pagingRouteValue.OrderByDesc},
                { string.Format("{0}.{1}", pagingRouteValue.RouteValuePrefix, ModelName.PagingRouteValue.TotalPages), pagingRouteValue.TotalPages},
                { string.Format("{0}.{1}", pagingRouteValue.RouteValuePrefix, ModelName.PagingRouteValue.PageNumber), pagingRouteValue.PageNumber},
            };

            if (pagingRouteValue.OptionValues != null)
            {
                var optionValues = new RouteValueDictionary(pagingRouteValue.OptionValues);

                foreach (var key in optionValues.Keys)
                {
                    string routeKey = key.First().ToString().ToUpper() + key.Substring(1);

                    if (!routeValues.ContainsKey(routeKey))
                    {
                        routeValues.Add(routeKey, optionValues[key]);   
                    }
                }
            }

            switch (pageType)
            {
                case PagingOptConst.First:
                    routeValues[string.Format("{0}.{1}", pagingRouteValue.RouteValuePrefix, ModelName.PagingRouteValue.PageNumber)] = 1;
                    break;
                case PagingOptConst.Last:
                    routeValues[string.Format("{0}.{1}", pagingRouteValue.RouteValuePrefix, ModelName.PagingRouteValue.PageNumber)] = pagingRouteValue.TotalPages;
                    break;
                case PagingOptConst.Prev:
                    routeValues[string.Format("{0}.{1}", pagingRouteValue.RouteValuePrefix, ModelName.PagingRouteValue.PageNumber)] = pagingRouteValue.PageNumber == 1 ? pagingRouteValue.PageNumber : pagingRouteValue.PageNumber - 1;
                    break;
                case PagingOptConst.Next:
                    routeValues[string.Format("{0}.{1}", pagingRouteValue.RouteValuePrefix, ModelName.PagingRouteValue.PageNumber)] = pagingRouteValue.PageNumber == pagingRouteValue.TotalPages ? pagingRouteValue.PageNumber : pagingRouteValue.PageNumber + 1;
                    break;
                default:
                    routeValues[string.Format("{0}.{1}", pagingRouteValue.RouteValuePrefix, ModelName.PagingRouteValue.PageNumber)] = pageNumber;
                    break;
            }

            return routeValues;
        }
        public static string GeneratePageNumbersHtml(this PagingHtmlGenerator generator, HtmlHelper helper, PagingGeneratorOption option, PagingRouteValue routeValue)
        {

            if (option.DisplayAllNumber || option.AutoDisplayAllNumber(routeValue.TotalPages))
            {
                return generator.GeneratePageNumbersHtmlInRange(helper, routeValue, 1, routeValue.TotalPages + 1);
            }

            StringBuilder pageNumbers = new StringBuilder();
            int start = 0;
            int stop = 0;

            if (routeValue.PageNumber < option.FirstPageNumbers + 3)
            {
                start = 1;
                stop = option.FirstPageNumbers + option.MiddlePageNumbers + 1;

                pageNumbers.Append(generator.GeneratePageNumbersHtmlInRange(helper, routeValue, start, stop));
                pageNumbers.Append(generator(PagingOptConst.NoneActionString));
                
                start = routeValue.TotalPages - option.LastPageNumbers + 1;
                stop = routeValue.TotalPages + 1;

                pageNumbers.Append(generator.GeneratePageNumbersHtmlInRange(helper, routeValue, start, stop));
            }
            else if (routeValue.TotalPages - option.LastPageNumbers - 2 < routeValue.PageNumber)
            {
                start = 1;
                stop = option.FirstPageNumbers + 1;

                pageNumbers.Append(generator.GeneratePageNumbersHtmlInRange(helper, routeValue, start, stop));
                pageNumbers.Append(generator(PagingOptConst.NoneActionString));

                start = routeValue.TotalPages - option.FirstPageNumbers - option.MiddlePageNumbers;
                stop = routeValue.TotalPages + 1;

                pageNumbers.Append(generator.GeneratePageNumbersHtmlInRange(helper, routeValue, start, stop));
            }
            else
            {
                int firstLeft = option.MiddlePageNumbers / 2;
                start = 1;
                stop = option.FirstPageNumbers + 1;

                pageNumbers.Append(generator.GeneratePageNumbersHtmlInRange(helper, routeValue, start, stop));
                pageNumbers.Append(generator(PagingOptConst.NoneActionString));

                start = routeValue.PageNumber - firstLeft;
                stop = routeValue.PageNumber + option.MiddlePageNumbers - firstLeft;

                pageNumbers.Append(generator.GeneratePageNumbersHtmlInRange(helper, routeValue, start, stop));
                pageNumbers.Append(generator(PagingOptConst.NoneActionString));

                start = routeValue.TotalPages - option.LastPageNumbers + 1;
                stop = routeValue.TotalPages + 1;

                pageNumbers.Append(generator.GeneratePageNumbersHtmlInRange(helper, routeValue, start, stop));
            }
            
            return pageNumbers.ToString();
        }
        public static string GeneratePageNumbersHtmlInRange(this PagingHtmlGenerator generator, HtmlHelper helper, PagingRouteValue routeValue, int start, int stop)
        {
            StringBuilder pageNumbers = new StringBuilder();

            for (int i = start; i < stop; i++)
            {
                //pageNumbers.Append(generator(PagingOptConst.NumberButton, url.PagingAction(routeValue, string.Empty, i), i.ToString(), i == routeValue.PageNumber));
                pageNumbers.Append(generator(PagingOptConst.NumberButton, helper.PagingActionLink(routeValue, i.ToString(), string.Empty, i), i == routeValue.PageNumber));
            }

            return pageNumbers.ToString();
        }
        public static Tuple<string, string, string, string, string> CreateHtmlElements(this PagingHtmlGenerator generator, HtmlHelper helper, PagingGeneratorOption option, PagingRouteValue routeValue)
        {
            string firstButton = string.Empty;
            string lastButton = string.Empty;
            string prevButton = string.Empty;
            string nextButton = string.Empty;
            string pageNumbers = generator.GeneratePageNumbersHtml(helper, option, routeValue);

            if (option.DisplayFirstLast)
            {
                //firstButton = generator(PagingOptConst.FirstLastButton, url.PagingAction(routeValue, PagingOptConst.First), PagingOptConst.First);
                firstButton = generator(PagingOptConst.FirstLastButton, helper.PagingActionLink(routeValue, PagingOptConst.First, PagingOptConst.First));
                //lastButton = generator(PagingOptConst.FirstLastButton, url.PagingAction(routeValue, PagingOptConst.Last), PagingOptConst.Last);
                lastButton = generator(PagingOptConst.FirstLastButton, helper.PagingActionLink(routeValue, PagingOptConst.Last, PagingOptConst.Last));
            }

            if (option.DisplayPrevNext)
            {
                if (option.AutoHidePrevNext)
                {
                    if (routeValue.PageNumber > 1)
                    {
                        //prevButton = generator(PagingOptConst.PrevNextButton, url.PagingAction(routeValue, PagingOptConst.Prev), PagingOptConst.Prev);
                        prevButton = generator(PagingOptConst.FirstLastButton, helper.PagingActionLink(routeValue, PagingOptConst.Prev, PagingOptConst.Prev));
                    }

                    if (routeValue.PageNumber < routeValue.TotalPages)
                    {
                        //nextButton = generator(PagingOptConst.PrevNextButton, url.PagingAction(routeValue, PagingOptConst.Next), PagingOptConst.Next);
                        nextButton = generator(PagingOptConst.FirstLastButton, helper.PagingActionLink(routeValue, PagingOptConst.Next, PagingOptConst.Next));
                    }
                }
                else
                {
                    //prevButton = generator(PagingOptConst.PrevNextButton, url.PagingAction(routeValue, PagingOptConst.Prev), PagingOptConst.Prev);
                    prevButton = generator(PagingOptConst.FirstLastButton, helper.PagingActionLink(routeValue, PagingOptConst.Prev, PagingOptConst.Prev));
                    //nextButton = generator(PagingOptConst.PrevNextButton, url.PagingAction(routeValue, PagingOptConst.Next), PagingOptConst.Next);
                    nextButton = generator(PagingOptConst.FirstLastButton, helper.PagingActionLink(routeValue, PagingOptConst.Next, PagingOptConst.Next));
                }
            }

            return Tuple.Create<string, string, string, string, string>(firstButton, prevButton, pageNumbers, nextButton, lastButton);
        }
    }
}
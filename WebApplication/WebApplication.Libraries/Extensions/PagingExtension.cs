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

namespace WebApplication.Libraries.Extensions
{
    public static class PagingExtension
    {
        public static List<T> ToPageList<T>(this IEnumerable<T> query, int pageSize, int pageNumber)
        {
            return query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        }

        public static string PagingAction(this UrlHelper url, PagingRouteValue routeValue, string pageType, int pageNumber = 0)
        {
            return url.Action(routeValue.ActionName, routeValue.ControllerName, routeValue.ToRouteValueDictionary(pageType, pageNumber));
        }

        public static RouteValueDictionary ToRouteValueDictionary(this PagingRouteValue routeValue, string pageType, int pageNumber = 0)
        {

            var routeValues = new RouteValueDictionary
            {
                { ModelName.PagingRouteValue.SearchKey, routeValue.SearchKey},
                { ModelName.PagingRouteValue.OrderBy, routeValue.OrderBy},
                { ModelName.PagingRouteValue.OrderByDesc, routeValue.OrderByDesc},
                { ModelName.PagingRouteValue.TotalPages, routeValue.TotalPages},
                { ModelName.PagingRouteValue.PageNumber, routeValue.PageNumber}
            };

            switch (pageType)
            {
                case PagingOptConst.First:
                    routeValues[ModelName.PagingRouteValue.PageNumber] = 1;
                    break;
                case PagingOptConst.Last:
                    routeValues[ModelName.PagingRouteValue.PageNumber] = routeValue.TotalPages;
                    break;
                case PagingOptConst.Prev:
                    routeValues[ModelName.PagingRouteValue.PageNumber] = routeValue.PageNumber == 1 ? routeValue.PageNumber : routeValue.PageNumber - 1;
                    break;
                case PagingOptConst.Next:
                    routeValues[ModelName.PagingRouteValue.PageNumber] = routeValue.PageNumber == routeValue.TotalPages ? routeValue.PageNumber : routeValue.PageNumber + 1;
                    break;
                default:
                    routeValues[ModelName.PagingRouteValue.PageNumber] = pageNumber;
                    break;
            }

            return routeValues;
        }

        public static string GeneratePageNumbersHtml(this PagingHtmlGenerator generator, UrlHelper url, PagingGeneratorOption option, PagingRouteValue routeValue)
        {

            if (option.DisplayAllNumber || option.AutoDisplayAllNumber(routeValue.TotalPages))
            {
                return generator.GeneratePageNumbersHtmlInRange(url, routeValue, 1, routeValue.TotalPages + 1);
            }

            StringBuilder pageNumbers = new StringBuilder();
            int start = 0;
            int stop = 0;

            if (routeValue.PageNumber < option.FirstPageNumbers + 3)
            {
                start = 1;
                stop = option.FirstPageNumbers + option.MiddlePageNumbers + 1;
                
                pageNumbers.Append(generator.GeneratePageNumbersHtmlInRange(url, routeValue, start, stop));
                pageNumbers.Append(generator(PagingOptConst.NoneActionString));
                
                start = routeValue.TotalPages - option.LastPageNumbers + 1;
                stop = routeValue.TotalPages + 1;
                
                pageNumbers.Append(generator.GeneratePageNumbersHtmlInRange(url, routeValue, start, stop));
            }
            else if (routeValue.TotalPages - option.LastPageNumbers - 2 < routeValue.PageNumber)
            {
                start = 1;
                stop = option.FirstPageNumbers + 1;

                pageNumbers.Append(generator.GeneratePageNumbersHtmlInRange(url, routeValue, start, stop));
                pageNumbers.Append(generator(PagingOptConst.NoneActionString));

                start = routeValue.TotalPages - option.FirstPageNumbers - option.MiddlePageNumbers;
                stop = routeValue.TotalPages + 1;

                pageNumbers.Append(generator.GeneratePageNumbersHtmlInRange(url, routeValue, start, stop));
            }
            else
            {
                int firstLeft = option.MiddlePageNumbers / 2;
                start = 1;
                stop = option.FirstPageNumbers + 1;

                pageNumbers.Append(generator.GeneratePageNumbersHtmlInRange(url, routeValue, start, stop));
                pageNumbers.Append(generator(PagingOptConst.NoneActionString));

                start = routeValue.PageNumber - firstLeft;
                stop = routeValue.PageNumber + option.MiddlePageNumbers - firstLeft;

                pageNumbers.Append(generator.GeneratePageNumbersHtmlInRange(url, routeValue, start, stop));
                pageNumbers.Append(generator(PagingOptConst.NoneActionString));

                start = routeValue.TotalPages - option.LastPageNumbers + 1;
                stop = routeValue.TotalPages + 1;

                pageNumbers.Append(generator.GeneratePageNumbersHtmlInRange(url, routeValue, start, stop));
            }
            
            return pageNumbers.ToString();
        }

        public static string GeneratePageNumbersHtmlInRange(this PagingHtmlGenerator generator, UrlHelper url, PagingRouteValue routeValue, int start, int stop)
        {
            StringBuilder pageNumbers = new StringBuilder();

            for (int i = start; i < stop; i++)
            {
                pageNumbers.Append(generator(PagingOptConst.NumberButton, url.PagingAction(routeValue, string.Empty, i), i.ToString(), i == routeValue.PageNumber));
            }

            return pageNumbers.ToString();
        }

        public static Tuple<string, string, string, string, string> CreateHtmlElements(this PagingHtmlGenerator generator, UrlHelper url, PagingGeneratorOption option, PagingRouteValue routeValue)
        {
            string firstButton = string.Empty;
            string lastButton = string.Empty;
            string prevButton = string.Empty;
            string nextButton = string.Empty;
            string pageNumbers = generator.GeneratePageNumbersHtml(url, option, routeValue);

            if (option.DisplayFirstLast)
            {
                firstButton = generator(PagingOptConst.FirstLastButton, url.PagingAction(routeValue, PagingOptConst.First), PagingOptConst.First);
                lastButton = generator(PagingOptConst.FirstLastButton, url.PagingAction(routeValue, PagingOptConst.Last), PagingOptConst.Last);
            }

            if (option.DisplayPrevNext)
            {
                if (option.AutoHidePrevNext)
                {
                    if (routeValue.PageNumber > 1)
                    {
                        prevButton = generator(PagingOptConst.PrevNextButton, url.PagingAction(routeValue, PagingOptConst.Prev), PagingOptConst.Prev);
                    }

                    if (routeValue.PageNumber < routeValue.TotalPages)
                    {
                        nextButton = generator(PagingOptConst.PrevNextButton, url.PagingAction(routeValue, PagingOptConst.Next), PagingOptConst.Next);
                    }
                }
                else
                {
                    prevButton = generator(PagingOptConst.PrevNextButton, url.PagingAction(routeValue, PagingOptConst.Prev), PagingOptConst.Prev);
                    nextButton = generator(PagingOptConst.PrevNextButton, url.PagingAction(routeValue, PagingOptConst.Next), PagingOptConst.Next);
                }
            }

            return Tuple.Create<string, string, string, string, string>(firstButton, prevButton, pageNumbers, nextButton, lastButton);
        }
    }
}
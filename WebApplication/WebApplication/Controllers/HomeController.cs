using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.BusinessLogic.BusinessLogic;
using WebApplication.Models.ViewModels;
using WebApplication.Libraries.Extensions;
using PagedList;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        #region fields
        private ProductBoard_Business _productBoard_Business = new ProductBoard_Business();
        #endregion
        public ActionResult Index(int pageNumber = 1)
        {
            ViewBag.PageNumber = pageNumber;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ListProduct(int pageNumber = 1)
        {
            pageNumber = pageNumber  < 1 ? 1 : pageNumber;
            var products = _productBoard_Business.GetAllProducts().ToPagedList(pageNumber, 6);
            ViewBag.PageNumber = pageNumber;

            return PartialView(products);
        }
    }
}
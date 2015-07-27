using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.BusinessLogic.BusinessLogic;
using WebApplication.BusinessLogic.Repositories;
using WebApplication.Models.Models;
using WebApplication.Models.ViewModels;
using PagedList;

namespace WebApplication.Controllers
{
    public class ShopController : Controller
    {
        #region fields
        private ProductBoard_Business _productBoard_Business = new ProductBoard_Business();
        #endregion
        // GET: Shop
        public ActionResult Index(Guid? CategoryGuid, int pageNumber = 1)
        {
            ViewBag.CategoryGuid = CategoryGuid;

            if (CategoryGuid == null)
            {
                return View(_productBoard_Business.GetAllProducts().ToPagedList(pageNumber, 9));
            }
            else
            {
                return View(_productBoard_Business.GetProductAfterCategory((Guid)CategoryGuid).ToPagedList(pageNumber, 9));
            }
        }
        [HttpPost]
        public ActionResult ListProduct(Guid? CategoryGuid, int pageNumber = 1)
        {
            ViewBag.CategoryGuid = CategoryGuid;

            if (CategoryGuid == null)
            {
                return PartialView("ListProductPartialView", _productBoard_Business.GetAllProducts().ToPagedList(pageNumber, 9));
            }
            else
            {
                return PartialView("ListProductPartialView", _productBoard_Business.GetProductAfterCategory((Guid)CategoryGuid).ToPagedList(pageNumber, 9));
            }
        }
    }
}
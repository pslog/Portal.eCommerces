using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.BusinessLogic.BusinessLogic;
using WebApplication.BusinessLogic.Repositories;
using WebApplication.Models.Models;

namespace WebApplication.Controllers
{
    public class ShopController : Controller
    {
        #region fields
        private ProductBoard_Business _productBoard_Business = new ProductBoard_Business();
        #endregion
        // GET: Shop
        public ActionResult Index(Guid catalog)
        {
            return View(_productBoard_Business.GetProductAfterCategory(catalog));
        }

        public ActionResult ListProduct()
        {
            return PartialView(_productBoard_Business.GetAllProducts());
        }
    }
}
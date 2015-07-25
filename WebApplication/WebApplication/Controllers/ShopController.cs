using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.BusinessLogic.BusinessLogic;
using WebApplication.BusinessLogic.Repositories;
using WebApplication.Models.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
    public class ShopController : Controller
    {
        #region fields
        private ProductBoard_Business _productBoard_Business = new ProductBoard_Business();
        #endregion
        // GET: Shop
        public ActionResult Index(Guid? CatagoryGuid)
        {
            if (CatagoryGuid == null)
            {
                return View(_productBoard_Business.GetAllProducts());
            }
            else
            {
                return View(_productBoard_Business.GetProductAfterCategory((Guid)CatagoryGuid));
            }
        }
      [HttpPost]
        public ActionResult ListProduct(Guid ?CatagoryGuid)
        {
            if (CatagoryGuid == null)
            {
                return PartialView("ListProductPartialView",_productBoard_Business.GetAllProducts());
            }
            else
            {
                return PartialView("ListProductPartialView",_productBoard_Business.GetProductAfterCategory((Guid)CatagoryGuid));
            }
        }
    }
}
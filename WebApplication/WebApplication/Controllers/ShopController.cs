﻿using System;
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
        public ActionResult Index(Guid ?catalogGuid)
        {
            if (catalogGuid == null)
            {
                return View(_productBoard_Business.GetAllProducts());
            }
            else
            {
                return View(_productBoard_Business.GetProductAfterCategory((Guid)catalogGuid));
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
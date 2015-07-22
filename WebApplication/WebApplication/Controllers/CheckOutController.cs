using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
    public class CheckOutController : Controller
    {
        // GET: CheckOut
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Remove a product from Cart
        /// </summary>
        /// <param name="Id">product Id</param>
        /// <returns>updated view of Cart</returns>
        public ActionResult RemoveFromCart(int Id)
        {
            List<ProductPartialViewModel> productsInCart = new List<ProductPartialViewModel>();
            if (HttpContext.Session != null && HttpContext.Session["ASPNETShoppingCart"] != null)
            {
                productsInCart = (List<ProductPartialViewModel>)HttpContext.Session["ASPNETShoppingCart"];
            }
            productsInCart.RemoveAll(product => product.Id == Id);
            if (HttpContext.Session != null)
            {
                HttpContext.Session["ASPNETShoppingCart"] = productsInCart;
            }

            return PartialView("_CartDetail_PartialView", productsInCart);
        }

        /// <summary>
        /// Update quantity of product in Cart and reCalculate total price of order 
        /// </summary>
        /// <param name="Id">product Id</param>
        /// <param name="quantity">product quantity</param>
        /// <returns>updated view of Cart</returns>
        /// 
        public ActionResult UpdateQuantityOfProduct(int Id, int quantity)
        {

            List<ProductPartialViewModel> productsInCart = new List<ProductPartialViewModel>();
            if (HttpContext.Session != null && HttpContext.Session["ASPNETShoppingCart"] != null)
            {
                productsInCart = (List<ProductPartialViewModel>)HttpContext.Session["ASPNETShoppingCart"];
            }

            // Update quantity of product in Cart
            if (quantity > 0)
            {
                foreach (var item in productsInCart)
                {
                    if (item.Id == Id)
                    {
                        item.Quantity = quantity;
                    }
                }
            }

            if (HttpContext.Session != null)
            {
                HttpContext.Session["ASPNETShoppingCart"] = productsInCart;
            }

            return PartialView("_CartDetail_PartialView", productsInCart);
        }
    }
}
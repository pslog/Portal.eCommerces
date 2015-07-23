using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
    public class CheckOutController : Controller
    {
        private PortalEntities db = new PortalEntities();
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

        public ActionResult CartDetails()
        {
            List<ProductPartialViewModel> productsInCart = new List<ProductPartialViewModel>();
            if (HttpContext.Session != null && HttpContext.Session["ASPNETShoppingCart"] != null)
            {
                productsInCart = (List<ProductPartialViewModel>)HttpContext.Session["ASPNETShoppingCart"];
            }
            return PartialView("_CartDetail_PartialView", productsInCart);
        }

        public ActionResult OrderProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderProduct([Bind(Include = "ID,UserID,OrderNote,NameOfRecipient,PhoneOfRecipient,AddressOfRecipient")] product_Orders product_Orders)
        {
            if (ModelState.IsValid)
            {
                decimal Total = 0;
                decimal FeeShip = 0;
                string OrderCode = "abc";
                int OrderStatus = 1;
                int Status = 1;
                int DiscountRate = 0;
                int StatusOrderDetail = 1;
                List<ProductPartialViewModel> productsInCart = new List<ProductPartialViewModel>();
                if (HttpContext.Session != null && HttpContext.Session["ASPNETShoppingCart"] != null)
                {
                    productsInCart = (List<ProductPartialViewModel>)HttpContext.Session["ASPNETShoppingCart"];
                }
                product_Orders.product_OrderDetails = new List<product_OrderDetails>();
                if(productsInCart.Count>0){
                    foreach (var item in productsInCart)
	                {
                        Total += item.Price * item.Quantity;
                        
                        product_OrderDetails orderDetails = new product_OrderDetails();
                            orderDetails.GUID = Guid.NewGuid();
                            orderDetails.OrderID = product_Orders.ID;
                            orderDetails.ProductID = item.Id;
                            orderDetails.Quantity = item.Quantity;
                            orderDetails.PriceOfUnit = item.Price;
                            orderDetails.TotalDiscount = item.Price * item.Quantity * (1 - DiscountRate);
                            orderDetails.TotalOrder = item.Price * item.Quantity;
                            orderDetails.Status = StatusOrderDetail;
                            orderDetails.CreatedDate = DateTime.Now;
                        product_Orders.product_OrderDetails.Add(orderDetails);
	                }
                }
                product_Orders.GUID = System.Guid.NewGuid();
                product_Orders.OrderCode = OrderCode;
                product_Orders.FeeShip = FeeShip;
                product_Orders.TotalOrder = Total + FeeShip;
                product_Orders.OrderStatus = OrderStatus;
                product_Orders.Status = Status;
                product_Orders.CreatedDate = DateTime.Now;

                db.product_Orders.Add(product_Orders);
                db.SaveChanges();
                if (HttpContext.Session != null)
                {
                    HttpContext.Session["ASPNETShoppingCart"] = null;
                }
                return RedirectToAction("Index","Home");
            }

            return View(product_Orders);
        }
    }
}
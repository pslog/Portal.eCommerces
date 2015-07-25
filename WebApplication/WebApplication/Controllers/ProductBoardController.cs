using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.BusinessLogic.Repositories;
using WebApplication.Models.ViewModels;
using WebApplication.BusinessLogic;
using WebApplication.BusinessLogic.BusinessLogic;

namespace WebApplication.Controllers
{
    public class ProductBoardController : Controller
    {
        #region fields
        public ProductRepository _productRepository;
        private ProductBoard_Business _productBoard_Business = new ProductBoard_Business();
        #endregion
        public ProductBoardController()
        {
            _productRepository = new ProductRepository();
        }
        // GET: ProductBoard
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Add a product to Cart
        /// </summary>
        /// <param name="Id">product Id</param>
        /// <returns>updated view of Cart</returns>
        public ActionResult AddToCart(int Id)
        {
            List<ProductPartialViewModel> productsInCart = new List<ProductPartialViewModel>();
            if (HttpContext.Session != null && HttpContext.Session["ASPNETShoppingCart"] != null)
            {
                productsInCart = (List<ProductPartialViewModel>)HttpContext.Session["ASPNETShoppingCart"];
            }
            // Check whether product is exist in cart or not
            bool IsExist = false;

            foreach (var item in productsInCart)
            {
                if (item.Id == Id)
                {
                    IsExist = true;
                    item.Quantity++;
                }
            }

            if (!IsExist)
            {
                Product temp = _productRepository.FindById(Id);
                ProductPartialViewModel newProductInCart = new ProductPartialViewModel()
                {
                    Id = temp.Id,
                    Name = temp.Name,
                    Catagory = temp.Catalog,
                    Description = temp.Description,
                    Price = temp.Price,
                    Image = temp.share_Images.Count() > 0 ? temp.share_Images.First().ImagePath : "/Content/Images/404/404.png",
                    Quantity = 1
                };
                productsInCart.Add(newProductInCart);
            }

            if (HttpContext.Session != null)
            {
                HttpContext.Session["ASPNETShoppingCart"] = productsInCart;
            }

            return PartialView(productsInCart);
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

            return PartialView("AddToCart", productsInCart);
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

            return PartialView("AddToCart", productsInCart);
        }

        public ActionResult CheckOutCart()
        {
            return View();
        }

        public ActionResult ProductDetails(int Id)
        {
            Product product = _productRepository.FindById(Id);
            ProductDetailsPartialViewModels productDetailsPartialViewModels = product.ConvertToProductDetailsPartialViewModels();
            return View(productDetailsPartialViewModels);
        }

        public ActionResult CartDetails()
        {
            List<ProductPartialViewModel> productsInCart = new List<ProductPartialViewModel>();
            if (HttpContext.Session != null && HttpContext.Session["ASPNETShoppingCart"] != null)
            {
                productsInCart = (List<ProductPartialViewModel>)HttpContext.Session["ASPNETShoppingCart"];
            }
            return PartialView("AddToCart", productsInCart);
        }
        public ActionResult ListBestByProduct()
        {
            var products = _productBoard_Business.GetAllBestSellProduct();

            return PartialView(products);
        }
    }
}
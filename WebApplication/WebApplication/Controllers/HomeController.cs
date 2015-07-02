using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

        public ActionResult ListProduct()
        {
            List<ProductPartialViewModel> products = new List<ProductPartialViewModel>();
            ProductPartialViewModel product1 = new ProductPartialViewModel(){
                Id = 1,
                Name = "Galaxy S5",
                Catagory = "Samsung",
                Description = "32GB, 2GB Ram, 1080HD, 5.1 inches, Android",
                Price = 649.99,
                Image = "http://placehold.it/650x450&text=Galaxy S5"
            };
             ProductPartialViewModel product2 = new ProductPartialViewModel(){
                Id = 2,
                Name = "iPhone 6",
                Catagory = "Apple",
                Description = "32GB, 64Bit, 1080HD, 4.7 inches, iOS 8",
                Price = 749.99,
                Image = "http://placehold.it/650x450&text=iPhone 6"
            };
             ProductPartialViewModel product3 = new ProductPartialViewModel(){
                Id = 3,
                Name = "Lumia 1520",
                Catagory = "Nokia",
                Description = "32GB, 4GB Ram, 1080HD, 6.3 inches, WP 8",
                Price = 749.00,
                Image = "http://placehold.it/650x450&text=Lumia 1520"
            };
             ProductPartialViewModel product4 = new ProductPartialViewModel()
             {
                 Id = 4,
                 Name = "Lumia 1520",
                 Catagory = "Nokia",
                 Description = "32GB, 4GB Ram, 1080HD, 6.3 inches, WP 8",
                 Price = 749.00,
                 Image = "http://placehold.it/650x450&text=Lumia 1520"
             };
             ProductPartialViewModel product5 = new ProductPartialViewModel()
             {
                 Id = 5,
                 Name = "Lumia 1520",
                 Catagory = "Nokia",
                 Description = "32GB, 4GB Ram, 1080HD, 6.3 inches, WP 8",
                 Price = 749.00,
                 Image = "http://placehold.it/650x450&text=Lumia 1520"
             };
            products.Add(product1);
            products.Add(product2);
            products.Add(product3);
            products.Add(product4);
            products.Add(product5);

            return PartialView(products);
        }
    }
}
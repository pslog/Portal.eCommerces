using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebApplication.Models.ViewModels;

namespace WebApplication.Controllers
{
    public class ShoppingCart
    {
        public List<ProductPartialViewModel> Items { get; private set; }
        public static ShoppingCart Instance;

        public static ShoppingCart RetrieveShoppingCart()
        {
            if (HttpContext.Current.Session["ASPNETShoppingCart"] == null)
            {
                Instance = new ShoppingCart();
                Instance.Items = new List<ProductPartialViewModel>();
                HttpContext.Current.Session["ASPNETShoppingCart"] = Instance;
            }
            else
            {
                Instance = (ShoppingCart)HttpContext.Current.Session["ASPNETShoppingCart"];
            }

            return Instance;
        }
    }
}

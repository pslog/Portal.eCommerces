using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;
using WebApplication.Models.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.BusinessLogic.Repositories
{
    public class ProductRepository
    {
        PortalEntities dc = new PortalEntities();
        public IList<Product> FindAll()
        {
            List<Product> ret = new List<Product>();
            var products = (from p in dc.product_Products
                            join c in dc.product_Categories on p.CategoryID equals c.ID
                            select new Product() { Name = p.Description, Catalog = c.Description, Id = p.ID, Description = p.Description2, Price = p.PriceOfUnit }).ToList<Product>();
            return products;
        }
        public Product FindById(int Id)
        {
            Product ret = (from p in dc.product_Products
                           join c in dc.product_Categories on p.CategoryID equals c.ID
                           where p.ID == Id
                   select new Product() { Name = p.Description, Catalog = c.Description, Id = p.ID, Description = p.Description2, Price = p.PriceOfUnit }).SingleOrDefault();
            return ret;
        }
    }
}

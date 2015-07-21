using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.BusinessLogic.BusinessLogic;
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
                            select new Product() { Name = p.Title,
                                Catalog = c.Description,
                                Id = p.ID,
                                Description = p.Description,
                                Description2 = p.Description2, 
                                Price = p.PriceOfUnit,
                                IsNewProduct = (bool)p.IsNewProduct,
                                Status = p.Status==null?1:(int)p.Status,
                                Tags = p.Tags,
                                share_Images = p.share_Images }).ToList<Product>();
            return products;
        }
        public Product FindById(int Id)
        {
            Product ret = (from p in dc.product_Products
                           join c in dc.product_Categories on p.CategoryID equals c.ID
                           where p.ID == Id
                           select new Product() { Name = p.Title,
                               Catalog = c.Description,
                               Id = p.ID,
                               Description = p.Description,
                               Description2 = p.Description2,
                               Price = p.PriceOfUnit,
                               IsNewProduct = (bool)p.IsNewProduct,
                               Status = p.Status==null?1:(int)p.Status,
                               Tags = p.Tags,
                               share_Images = p.share_Images }).SingleOrDefault();
            return ret;
        }

        public IList<Product> FindProductAfterCategory(Guid categoryGuid)
        {
            var products = (from p in dc.product_Products
                            where p.GUID == categoryGuid
                            join c in dc.product_Categories on p.CategoryID equals c.ID
                            select new Product()
                            {
                                Name = p.Title,
                                Catalog = c.Description,
                                Id = p.ID,
                                Description = p.Description,
                                Description2 = p.Description2,
                                Price = p.PriceOfUnit,
                                IsNewProduct = (bool)p.IsNewProduct,
                                Status = p.Status == null ? 1 : (int)p.Status,
                                Tags = p.Tags,
                                share_Images = p.share_Images
                            }).ToList<Product>();
            return products;
        }
    }
}

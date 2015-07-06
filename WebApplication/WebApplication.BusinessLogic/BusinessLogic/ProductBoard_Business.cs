using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.BusinessLogic.Repositories;
using WebApplication.Models.ViewModels;

namespace WebApplication.BusinessLogic.BusinessLogic
{
    public class ProductBoard_Business
    {
        #region fields
        private ProductRepository _productRepository = new ProductRepository();
        #endregion

        public List<ProductPartialViewModel> GetAllProducts()
        {
            List<ProductPartialViewModel> products = new List<ProductPartialViewModel>();
            IList<Product> listProducts = _productRepository.FindAll();
            foreach (Product product in listProducts)
            {
                ProductPartialViewModel pro = new ProductPartialViewModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Catagory = product.Catalog,
                    Description = product.Description,
                    Price = product.Price,
                    Image = "http://placehold.it/650x450&text=Lumia 1520",
                    Quantity = 1
                };
                products.Add(pro);
            }
            return products;
        }
    }
}

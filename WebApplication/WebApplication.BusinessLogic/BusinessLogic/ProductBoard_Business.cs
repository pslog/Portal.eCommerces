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

        public IList<ProductPartialViewModel> GetAllProducts()
        {
            IList<ProductPartialViewModel> products = new List<ProductPartialViewModel>();
            IList<Product> listProducts = _productRepository.FindAll();
            products = listProducts.ConvertToProductListViewModel();
            return products;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.BusinessLogic.Repositories;
using WebApplication.Models.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.BusinessLogic.BusinessLogic
{
    public class ProductBoard_Business
    {
        #region fields
        private ProductRepository _productRepository = new ProductRepository();
        private CategoryRepository _categoryRepository = new CategoryRepository();
        #endregion

        public IList<ProductPartialViewModel> GetAllProducts()
        {
            IList<ProductPartialViewModel> products = new List<ProductPartialViewModel>();
            IList<Product> listProducts = _productRepository.FindAll();
            products = listProducts.ConvertToProductListViewModel();
            return products;
        }

        public IList<ProductPartialViewModel> GetProductAfterCategory(Guid categoryGuid)
        {
            IList<ProductPartialViewModel> products = new List<ProductPartialViewModel>();
            IList<Product> listProducts = _productRepository.FindProductAfterCategory(categoryGuid);
            //products.Add(listProducts.ConvertToProductListViewModel());
            foreach (var p in listProducts)
            {
                products.Add(p.ConvertToProductViewModel());
            }
            IList<product_Categories> childs = _categoryRepository.GetChildCategory(categoryGuid);
            if (childs != null)
            {
                foreach (var child in childs)
                {
                    IList<Product> tempListProducts = _productRepository.FindProductAfterCategory(child.GUID);
                    foreach (var p in tempListProducts)
                    {
                        products.Add(p.ConvertToProductViewModel());
                    }
                }
            }
            return products;
        }

    }
}

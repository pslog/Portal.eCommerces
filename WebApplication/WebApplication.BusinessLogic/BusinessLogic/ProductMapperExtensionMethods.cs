using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models.ViewModels;

namespace WebApplication.BusinessLogic.BusinessLogic
{
    public static class ProductMapperExtensionMethods
    {
        public static IList<ProductPartialViewModel> ConvertToProductListViewModel(this IList<Product> products)
        {
            IList<ProductPartialViewModel> productViewModels = new List<ProductPartialViewModel>();
            foreach (Product p in products)
            {
                productViewModels.Add(p.ConvertToProductViewModel());
            }
            return productViewModels;
        }
        public static ProductPartialViewModel ConvertToProductViewModel(this Product product)
        {
            ProductPartialViewModel productViewModel = new ProductPartialViewModel();
            productViewModel.Id = product.Id;
            productViewModel.Name = product.Name;
            productViewModel.Catagory = product.Catalog;
            productViewModel.Description = product.Description;
            productViewModel.Price = product.Price;
            productViewModel.Image = "http://placehold.it/650x450&text=Lumia 1520";
            productViewModel.Quantity = 1;
            return productViewModel;
        }
    }
}

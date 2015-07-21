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
            productViewModel.Image = product.share_Images.Count() > 0 ? product.share_Images.First().ImagePath : "/Content/Images/404/404.png";
            productViewModel.Quantity = 1;
            productViewModel.share_Images = product.share_Images;
            return productViewModel;
        }

        public static IList<ProductDetailsPartialViewModels> ConvertToProductDetailsPartialListViewModel(this IList<Product> products)
        {
            IList<ProductDetailsPartialViewModels> productDetailsViewModels = new List<ProductDetailsPartialViewModels>();
            foreach (Product p in products)
            {
                productDetailsViewModels.Add(p.ConvertToProductDetailsPartialViewModels());
            }
            return productDetailsViewModels;
        }
        public static ProductDetailsPartialViewModels ConvertToProductDetailsPartialViewModels(this Product product)
        {
            ProductDetailsPartialViewModels productDetailsPartialViewModels = new ProductDetailsPartialViewModels();
            productDetailsPartialViewModels.Id = product.Id;
            productDetailsPartialViewModels.Name = product.Name;
            productDetailsPartialViewModels.Catalog = product.Catalog;
            productDetailsPartialViewModels.Description = product.Description;
            productDetailsPartialViewModels.Price = product.Price;
            productDetailsPartialViewModels.Image = product.share_Images.Count() > 0 ? product.share_Images.First().ImagePath : "/Content/Images/404/404.png";
            productDetailsPartialViewModels.Quantity = 1;
            productDetailsPartialViewModels.share_Images = product.share_Images;
            productDetailsPartialViewModels.Status = StatusProductViewModels.GetValueOfStatus(product.Status);
            productDetailsPartialViewModels.IsNewProduct = product.IsNewProduct;
            productDetailsPartialViewModels.Tags = product.Tags;
            productDetailsPartialViewModels.Description2 = product.Description2;
            productDetailsPartialViewModels.IsAvailable = product.Status == 2 ? false : true;
            return productDetailsPartialViewModels;
        }
    }
}

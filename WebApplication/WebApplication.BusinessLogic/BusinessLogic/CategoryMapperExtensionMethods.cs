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
    public static class CategoryMapperExtensionMethods
    {
        public static CategoryRepository _categoryRepository = new CategoryRepository();
        public static IList<DetailsCategoryViewModels> ConvertToListDetailsCategoryViewModels(this IList<product_Categories> categories)
        {
            IList<DetailsCategoryViewModels> listDetailsCategoryViewModels = new List<DetailsCategoryViewModels>();
            foreach (product_Categories c in categories)
            {
                listDetailsCategoryViewModels.Add(c.ConvertToDetailsCategoryViewModels());
            }
            return listDetailsCategoryViewModels;
        }
        public static DetailsCategoryViewModels ConvertToDetailsCategoryViewModels(this product_Categories category)
        {
            DetailsCategoryViewModels detailsCategoryViewModels = new DetailsCategoryViewModels();
            //{
                detailsCategoryViewModels.ID = category.ID;
                detailsCategoryViewModels.GUID = category.GUID;
                detailsCategoryViewModels.Description = category.Description;
                detailsCategoryViewModels.Title = category.Title;
                detailsCategoryViewModels.Url = category.Url;
                detailsCategoryViewModels.ModifiedBy = category.ModifiedBy;
                detailsCategoryViewModels.ModifiedDate = category.ModifiedDate;
                detailsCategoryViewModels.CreatedBy = category.CreatedBy;
                detailsCategoryViewModels.CreatedDate = category.CreatedDate;
                detailsCategoryViewModels.SortOrder = category.SortOrder;
                detailsCategoryViewModels.Status = StatusCategoryViewModel.GetValueOfStatus(category.Status);
                detailsCategoryViewModels.Parent = _categoryRepository.FindByGuid(category.ParentID) == null ? "Danh mục" : _categoryRepository.FindByGuid(category.ParentID).Title;
            //};

            return detailsCategoryViewModels;
        }

        public static IList<ListCategoriesLeftMenuViewModels> ConvertToListCategoriesLeftMenuViewModels(this IList<product_Categories> categories)
        {
            IList<ListCategoriesLeftMenuViewModels> listCategoriesLeftMenuViewModels = new List<ListCategoriesLeftMenuViewModels>();
            foreach (product_Categories c in categories)
            {
                listCategoriesLeftMenuViewModels.Add(c.ConvertToCategoriesLeftMenuViewModels());
            }
            return listCategoriesLeftMenuViewModels;
        }
        public static ListCategoriesLeftMenuViewModels ConvertToCategoriesLeftMenuViewModels(this product_Categories category)
        {
            //ListCategoriesLeftMenuViewModels listCategoriesLeftMenuViewModels = new ListCategoriesLeftMenuViewModels()
            //{
            //    ID = category.ID,
            //    GUID = category.GUID,
            //    Description = category.Description,
            //    Title = category.Title,
            //    Url = category.Url,
            //    SortOrder = category.SortOrder,
            //    Status = StatusCategoryViewModel.GetValueOfStatus(category.Status),
            //    Parent = _categoryRepository.FindByGuid(category.ParentID).Title
            //};

            ListCategoriesLeftMenuViewModels listCategoriesLeftMenuViewModels = new ListCategoriesLeftMenuViewModels();
            
                listCategoriesLeftMenuViewModels.ID = category.ID;
                listCategoriesLeftMenuViewModels.GUID = category.GUID;
                listCategoriesLeftMenuViewModels.Description = category.Description;
                listCategoriesLeftMenuViewModels.Title = category.Title;
                listCategoriesLeftMenuViewModels.Url = category.Url;
                listCategoriesLeftMenuViewModels.SortOrder = category.SortOrder;
                listCategoriesLeftMenuViewModels.Status = StatusCategoryViewModel.GetValueOfStatus(category.Status);
            

            return listCategoriesLeftMenuViewModels;
        }
    }
}

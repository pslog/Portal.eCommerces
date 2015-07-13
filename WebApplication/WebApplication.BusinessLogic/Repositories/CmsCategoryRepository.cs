using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uow.Package.Data.Repositories;
using WebApplication.BusinessLogic.Interface;
using WebApplication.Common.Constants;
using WebApplication.Models.Models;
using WebApplication.Libraries.Extensions;
using WebApplication.Models.ViewModels;

namespace WebApplication.BusinessLogic.Repositories
{
    public class CmsCategoryRepository : Repository<cms_Categories>, ICmsCategoryRepository
    {
        public CmsCategoryRepository(DbContext dbContext) 
            : base(dbContext)
        {
        }

        public IQueryable<cms_Categories> GetExcept(int id)
        {
            return DbSet.Where(nc => nc.ID != id);
        }

        public IQueryable<cms_Categories> GetExcepts(int[] id)
        {
            return DbSet.Where(nc => !id.Contains(nc.ID));
        }

        public cms_Categories GetCmsCategory(cms_Categories cmsCategory, int creatorId, int modifierId)
        {
            cmsCategory.CreatedBy = creatorId;
            cmsCategory.CreatedDate = DateTime.Now;
            cmsCategory.ModifiedBy = modifierId;
            cmsCategory.ModifiedDate = cmsCategory.CreatedDate;
            cmsCategory.GUID = Guid.NewGuid();

            return cmsCategory;
        }

        public CmsCategoryView SearchCategories(PagingRouteValue routeValue = null)
        {
            var cmsCategories = DbSet.AsQueryable();
          
            if (!string.IsNullOrEmpty(routeValue.SearchKey))
            {
                cmsCategories = cmsCategories.Where(c => c.ID.ToString().Contains(routeValue.SearchKey) || c.Title.Contains(routeValue.SearchKey) || c.GUID.ToString().Contains(routeValue.SearchKey));
            }

            switch (routeValue.OrderBy)
            {
                case ModelName.CmsCategory.ID:
                    cmsCategories = routeValue.OrderByDesc ? cmsCategories.OrderBy(c => c.ID) : cmsCategories.OrderByDescending(c => c.ID);
                    break;
                case ModelName.CmsCategory.Title:
                    cmsCategories = routeValue.OrderByDesc ? cmsCategories.OrderBy(c => c.Title) : cmsCategories.OrderByDescending(c => c.Title);
                    break;
                case ModelName.CmsCategory.GUID:
                    cmsCategories = routeValue.OrderByDesc ? cmsCategories.OrderBy(c => c.GUID) : cmsCategories.OrderByDescending(c => c.GUID);
                    break;
                case ModelName.CmsCategory.CreatedBy:
                    cmsCategories = routeValue.OrderByDesc ? cmsCategories.OrderBy(c => c.CreatedBy) : cmsCategories.OrderByDescending(c => c.CreatedBy);
                    break;
                case ModelName.CmsCategory.CreatedDate:
                    cmsCategories = routeValue.OrderByDesc ? cmsCategories.OrderBy(c => c.CreatedDate) : cmsCategories.OrderByDescending(c => c.CreatedDate);
                    break;
                case ModelName.CmsCategory.ModifiedBy:
                    cmsCategories = routeValue.OrderByDesc ? cmsCategories.OrderBy(c => c.ModifiedBy) : cmsCategories.OrderByDescending(c => c.ModifiedBy);
                    break;
                case ModelName.CmsCategory.ModifiedDate:
                    cmsCategories = routeValue.OrderByDesc ? cmsCategories.OrderBy(c => c.ModifiedDate) : cmsCategories.OrderByDescending(c => c.ModifiedDate);
                    break;
                default:
                    cmsCategories = routeValue.OrderByDesc ? cmsCategories.OrderBy(c => c.ID) : cmsCategories.OrderByDescending(c => c.ID);
                    break;
            }

            routeValue.TotalPages = cmsCategories.Count();
            routeValue.TotalPages = routeValue.TotalPages % ConstValue.PageSize == 0 ? routeValue.TotalPages / ConstValue.PageSize : routeValue.TotalPages / ConstValue.PageSize + 1;

            return new CmsCategoryView
            {
                CmsCategories = cmsCategories.ToPageList(ConstValue.PageSize, routeValue.PageNumber),
                RouteValue = routeValue
            };
        }
    }
}

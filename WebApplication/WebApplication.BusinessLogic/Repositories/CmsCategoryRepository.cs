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

        public cms_Categories GetCmsCategory(cms_Categories cmsCategory, int creatorId, int modiferId)
        {
            cmsCategory.CreatedBy = creatorId;
            cmsCategory.CreatedDate = DateTime.Now;
            cmsCategory.ModifiedBy = modiferId;
            cmsCategory.ModifiedDate = cmsCategory.CreatedDate;
            cmsCategory.GUID = Guid.NewGuid();

            return cmsCategory;
        }

        public Task<IQueryable<cms_Categories>> SearchCategories(string searchKey = null, string orderBy = null, bool orderbyDesc = false, int page = 1)
        {
            var categories = DbSet.AsQueryable();

            if (!string.IsNullOrEmpty(searchKey))
            {
                categories = categories.Where(c => c.ID.ToString().Contains(searchKey) || c.Title.Contains(searchKey) || c.GUID.ToString().Contains(searchKey));
            }
            
            switch (orderBy)
            {
                case ModelName.CmsCategory.ID:
                    categories = orderbyDesc ? categories.OrderBy(c => c.ID) : categories.OrderByDescending(c => c.ID);
                    break;
                case ModelName.CmsCategory.Title:
                    categories = orderbyDesc ? categories.OrderBy(c => c.Title) : categories.OrderByDescending(c => c.Title);
                    break;
                case ModelName.CmsCategory.GUID:
                    categories = orderbyDesc ? categories.OrderBy(c => c.GUID) : categories.OrderByDescending(c => c.GUID);
                    break;
                case ModelName.CmsCategory.CreatedBy:
                    categories = orderbyDesc ? categories.OrderBy(c => c.CreatedBy) : categories.OrderByDescending(c => c.CreatedBy);
                    break;
                case ModelName.CmsCategory.CreatedDate:
                    categories = orderbyDesc ? categories.OrderBy(c => c.CreatedDate) : categories.OrderByDescending(c => c.CreatedDate);
                    break;
                case ModelName.CmsCategory.ModifiedBy:
                    categories = orderbyDesc ? categories.OrderBy(c => c.ModifiedBy) : categories.OrderByDescending(c => c.ModifiedBy);
                    break;
                case ModelName.CmsCategory.ModifiedDate:
                    categories = orderbyDesc ? categories.OrderBy(c => c.ModifiedDate) : categories.OrderByDescending(c => c.ModifiedDate);
                    break;
                default:
                    categories = orderbyDesc ? categories.OrderBy(c => c.ID) : categories.OrderByDescending(c => c.ID);
                    break;
            }

            return Task.FromResult<IQueryable<cms_Categories>>(categories.Skip(ConstValue.PageSize * (page - 1)).Take(ConstValue.PageSize));
        }
    }
}

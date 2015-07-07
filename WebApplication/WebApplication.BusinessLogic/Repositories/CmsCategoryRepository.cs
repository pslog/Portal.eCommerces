using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uow.Package.Data.Repositories;
using WebApplication.BusinessLogic.Interface;
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
    }
}

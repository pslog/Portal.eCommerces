using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Uow.Package.Data.Repositories;
using WebApplication.BusinessLogic.Interface;
using WebApplication.Common.Constants;
using WebApplication.Models.Models;
using WebApplication.Libraries.Extensions;
using WebApplication.Models.ViewModels;
using PagedList;

namespace WebApplication.BusinessLogic.Repositories
{
    public class CmsCategoryRepository : Repository<cms_Categories>, ICmsCategoryRepository
    {
        public CmsCategoryRepository(DbContext dbContext) 
            : base(dbContext)
        {
        }

        public cms_Categories GetNewCmsCategory(cms_Categories cmsCategory, int creatorId, int modifierId)
        {
            cmsCategory.CreatedBy = creatorId;
            cmsCategory.CreatedDate = DateTime.Now;
            cmsCategory.ModifiedBy = modifierId;
            cmsCategory.ModifiedDate = cmsCategory.CreatedDate;
            cmsCategory.GUID = Guid.NewGuid();

            return cmsCategory;
        }
        public cms_Categories GetUpdateCmsCategory(cms_Categories updateCmsCategory,  int modifierId)
        {
            updateCmsCategory.ParentID = updateCmsCategory.ParentID == 0 ? null : updateCmsCategory.ParentID;
            updateCmsCategory.ModifiedBy = modifierId;
            updateCmsCategory.ModifiedDate = DateTime.Now;

            return updateCmsCategory;
        }
        public IQueryable<cms_Categories> GetCmsCategoriesByParentID(int? parentId)
        {
            return DbSet.AsQueryable().Where(c => c.ParentID == parentId);
        }
        public IQueryable<cms_Categories> GetChildren(int id)
        {
            return DbSet.Where(c => c.ParentID == id);
        }
    }
}

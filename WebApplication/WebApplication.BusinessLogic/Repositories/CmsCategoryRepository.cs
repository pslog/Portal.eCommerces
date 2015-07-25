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

        public cms_Categories GetByGuid(Guid guid)
        {
            return DbSet.FirstOrDefault(c => c.GUID == guid);
        }
        public IQueryable<cms_Categories> GetExcept(int id)
        {
            return DbSet.Where(nc => nc.ID != id);
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


        public IEnumerable<cms_Categories> GetCmsCategories(int? parentId)
        {
            var cmsCategories = DbSet.AsQueryable().Where(c => c.ParentID == parentId).ToList();

            //if (parentId == null && cmsCategories.Count > 0)
            //{
            //    cmsCategories.Add(new cms_Categories { ID = 0, Title = "khác", GUID = Guid.Empty });
            //}

            return cmsCategories;
        }

        public CmsCategoryCreateView GetCreateView(int? parentID)
        {
            var parent = this.GetById(parentID ?? 0);
            
            if (parent != null)
            {
                return new CmsCategoryCreateView
                {
                    ParentID = parent.ID,
                    ParentTitle = parent.Title
                };
            }

            return new CmsCategoryCreateView();
        }
        public CmsCategoryEditView GetEditView(int id) 
        {
            var cmsCategory = this.GetById(id);
            var parents = this.GetExcept(id).ToList();

            parents.Insert(0, new cms_Categories { ID = 0, Title = Label.CmsCategory.RootCategory });

            if (cmsCategory != null)
            {
                return new CmsCategoryEditView
                {
                    CmsCategory = cmsCategory,
                    Parents = new SelectList(parents, ModelName.CmsCategory.ID, ModelName.CmsCategory.Title, cmsCategory.ParentID ?? 0)
                };
            }

            return null;
        }


        public IQueryable<cms_Categories> GetChildren(int id)
        {
            return DbSet.Where(c => c.ParentID == id);
        }
    }
}

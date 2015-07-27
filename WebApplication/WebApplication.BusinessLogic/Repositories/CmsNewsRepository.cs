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
using WebApplication.Models.ViewModels;
using WebApplication.Libraries.Extensions;
using System.Web.Mvc;

namespace WebApplication.BusinessLogic.Repositories
{
    public class CmsNewsRepository : Repository<cms_News>, ICmsNewsRepository
    {
        public CmsNewsRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public IQueryable<cms_News> GetCmsNewsByCategoryID(int? categoryID, ICmsCategoryRepository cmsCategoryRepository, out string rootCategoryTitle, string searchKey = "")
        {
            var cmsNews = DbSet.AsQueryable();
            
            if(categoryID == null)
            {
                rootCategoryTitle = string.Empty;
                cmsNews = DbSet.AsQueryable().Where(n => n.CategoryID == categoryID);
            }
            else
            {
                var category = cmsCategoryRepository.GetById((int)categoryID);

                if (category == null)
                {
                    rootCategoryTitle = string.Empty;
                    return Enumerable.Empty<cms_News>().AsQueryable();
                }

                var categoryIds = new List<int>() { category.ID };

                categoryIds.AddRange(category.GetChildren<cms_Categories>("cms_Categories1").Select(c => c.ID).ToList());
                
                cmsNews = DbSet.AsQueryable().Where(n => categoryIds.Contains((int)n.CategoryID));
                rootCategoryTitle = category.Title;
            }

            
            if(!string.IsNullOrEmpty(searchKey))
            {
                cmsNews = cmsNews.Where(c => c.Title.ToLower().Contains(searchKey) || c.SubTitle.ToLower().Contains(searchKey));
            }

            return cmsNews.OrderByDescending(c => c.CreatedDate);
        }

        public cms_News GetNewCmsNews(cms_News cmsNews, int creatorId, int modifierId)
        {
            if(string.IsNullOrEmpty(cmsNews.Authors))
            {
                cmsNews.Authors = creatorId.ToString();
            }

            cmsNews.CategoryID = cmsNews.CategoryID == 0 ? null : cmsNews.CategoryID;
            cmsNews.GUID = Guid.NewGuid();
            cmsNews.CreatedBy = creatorId;
            cmsNews.CreatedDate = DateTime.Now;
            cmsNews.ModifiedBy = modifierId;
            cmsNews.ModifiedDate = cmsNews.CreatedDate;
            
            return cmsNews;
        }

        public cms_News GetUpdateCmsNews(cms_News updateCmsNews, int modifierId)
        {
            updateCmsNews.CategoryID = updateCmsNews.CategoryID == 0 ? null : updateCmsNews.CategoryID;
            updateCmsNews.ModifiedBy = modifierId;
            updateCmsNews.ModifiedDate = DateTime.Now;

            return updateCmsNews;
        }
    }
}

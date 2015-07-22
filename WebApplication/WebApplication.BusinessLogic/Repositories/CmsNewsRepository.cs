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

        class Order
        {
            public bool OrderByAsc { get; set; }
            public string OrderProperty { get; set; }
        }

        public PagingView<cms_News> GetPagingView(CmsNewsIndexViewDTO indexView, ICmsCategoryRepository cmsCategoryRepository)
        {
            var cmsNews = DbSet.AsQueryable();

            if(indexView.CategoryID == null)
            {
                cmsNews = DbSet.Where(c => c.CategoryID == indexView.CategoryID).AsQueryable();
            }
            else
            {
                var cmsCategoryIDs = cmsCategoryRepository.GetChildren((int)indexView.CategoryID).Select(c => c.ID);
                cmsNews = DbSet.Where(c => c.CategoryID == indexView.CategoryID || cmsCategoryIDs.Contains((int)c.CategoryID)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(indexView.RouteValue.SearchKey))
            {
                cmsNews = cmsNews.Where(c => c.ID.ToString().Contains(indexView.RouteValue.SearchKey) 
                                          || c.Title.Contains(indexView.RouteValue.SearchKey) 
                                          || c.SubTitle.Contains(indexView.RouteValue.SearchKey)
                                          || c.Authors.Contains(indexView.RouteValue.SearchKey) 
                                          || c.GUID.ToString().Contains(indexView.RouteValue.SearchKey));
            }

            switch (indexView.RouteValue.OrderBy)
            {
                case ModelName.CmsNews.ID:
                    cmsNews = indexView.RouteValue.OrderByDesc ? cmsNews.OrderByDescending(c => c.ID) : cmsNews.OrderBy(c => c.ID);
                    break;
                case ModelName.CmsNews.Title:
                    cmsNews = indexView.RouteValue.OrderByDesc ? cmsNews.OrderByDescending(c => c.Title) : cmsNews.OrderBy(c => c.Title);
                    break;
                case ModelName.CmsNews.Authors:
                    cmsNews = indexView.RouteValue.OrderByDesc ? cmsNews.OrderByDescending(c => c.Authors) : cmsNews.OrderBy(c => c.Authors);
                    break;
                case ModelName.CmsNews.TotalView:
                    cmsNews = indexView.RouteValue.OrderByDesc ? cmsNews.OrderByDescending(c => c.Authors) : cmsNews.OrderBy(c => c.Authors);
                    break;
                case ModelName.CmsNews.CreatedBy:
                    cmsNews = indexView.RouteValue.OrderByDesc ? cmsNews.OrderByDescending(c => c.CreatedBy) : cmsNews.OrderBy(c => c.CreatedBy);
                    break;
                case ModelName.CmsNews.CreatedDate:
                    cmsNews = indexView.RouteValue.OrderByDesc ? cmsNews.OrderByDescending(c => c.CreatedDate) : cmsNews.OrderBy(c => c.CreatedDate);
                    break;
                case ModelName.CmsNews.ModifiedBy:
                    cmsNews = indexView.RouteValue.OrderByDesc ? cmsNews.OrderByDescending(c => c.ModifiedBy) : cmsNews.OrderBy(c => c.ModifiedBy);
                    break;
                case ModelName.CmsNews.ModifiedDate:
                    cmsNews = indexView.RouteValue.OrderByDesc ? cmsNews.OrderByDescending(c => c.ModifiedDate) : cmsNews.OrderBy(c => c.ModifiedDate);
                    break;
                default:
                    cmsNews = indexView.RouteValue.OrderByDesc ? cmsNews.OrderByDescending(c => c.CreatedDate) : cmsNews.OrderBy(c => c.CreatedDate);
                    break;
            }

            indexView.RouteValue.TotalPages = cmsNews.Count();
            indexView.RouteValue.TotalPages = indexView.RouteValue.TotalPages % ConstValue.PageSize == 0 ? indexView.RouteValue.TotalPages / ConstValue.PageSize : indexView.RouteValue.TotalPages / ConstValue.PageSize + 1;

            return new PagingView<cms_News>
            {
                Items = cmsNews.ToPageList(ConstValue.PageSize, indexView.RouteValue.PageNumber),
                RouteValue = indexView.RouteValue
            };
        }
        public CmsNewsDTO GetCmsNewsDTO(int? categoryID, IRepository<cms_Categories> cmsCategoryRepository, cms_News cmsNews = null)
        {
            var categories = cmsCategoryRepository.GetAll().ToList();

            categories.Insert(0, new cms_Categories
            {
                ID = 0,
                Title = string.Empty
            });

            return new CmsNewsDTO
            {
                CmsNews = cmsNews,
                Categories = new SelectList(categories, "ID", "Title", categoryID)
            };
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

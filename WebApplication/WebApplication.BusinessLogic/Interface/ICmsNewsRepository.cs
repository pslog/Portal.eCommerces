using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uow.Package.Data.Repositories;
using WebApplication.Models.Models;
using WebApplication.Models.ViewModels;

namespace WebApplication.BusinessLogic.Interface
{
    public interface ICmsNewsRepository : IRepository<cms_News>
    {
        IQueryable<cms_News> GetCmsNewsByCategoryID(int? categoryID, ICmsCategoryRepository cmsCategoryRepository, out string rootCategoryTitle,  string searchKey = "");
        cms_News GetNewCmsNews(cms_News cmsNews, int creatorId, int modifierId);
        cms_News GetUpdateCmsNews(cms_News updateCmsNews, int modifierId);
    }
}

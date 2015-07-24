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
        PagingView<cms_News> GetPagingView(CmsNewsIndexViewDTO indexView, ICmsCategoryRepository cmsCategoryRepository, int pageSize = 0);
        CmsNewsDTO GetCmsNewsDTO(int? categoryID, IRepository<cms_Categories> cmsCategoryRepository, cms_News cmsNews = null);
        cms_News GetNewCmsNews(cms_News cmsNews, int creatorId, int modifierId);
        cms_News GetUpdateCmsNews(cms_News updateCmsNews, int modifierId);
    }
}

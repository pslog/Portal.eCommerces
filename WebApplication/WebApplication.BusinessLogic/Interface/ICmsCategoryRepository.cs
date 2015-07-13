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
    public interface ICmsCategoryRepository : IRepository<cms_Categories>
    {
        IQueryable<cms_Categories> GetExcept(int id);
        IQueryable<cms_Categories> GetExcepts(int[] id);
        CmsCategoryView SearchCategories(PagingRouteValue routeValue = null);
        cms_Categories GetCmsCategory(cms_Categories cmsCategory, int creatorId, int modiferId);
    }
}

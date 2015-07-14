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
        cms_Categories GetByGuid(Guid guid);
        IQueryable<cms_Categories> GetExcept(int id);
        CmsCategoryCreateView GetCreateView(int? parentID);
        CmsCategoryEditView GetEditView(int id);
        CmsCategoryIndexView GetIndexView(PagingRouteValue routeValue = null);
        cms_Categories GetNewCmsCategory(cms_Categories cmsCategory, int creatorId, int modiferId);
        cms_Categories GetUpdateCmsCategory(cms_Categories cmsCategory, int modiferId);

    }
}

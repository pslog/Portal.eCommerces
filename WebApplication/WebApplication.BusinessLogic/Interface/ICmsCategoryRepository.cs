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
        IQueryable<cms_Categories> GetChildren(int id);
        IQueryable<cms_Categories> GetCmsCategoriesByParentID(int? parentId);
        cms_Categories GetNewCmsCategory(cms_Categories cmsCategory, int creatorId, int modiferId);
        cms_Categories GetUpdateCmsCategory(cms_Categories updateCmsCategory, int modiferId);
    }
}

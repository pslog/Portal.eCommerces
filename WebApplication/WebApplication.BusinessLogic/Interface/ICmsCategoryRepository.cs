using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uow.Package.Data.Repositories;
using WebApplication.Models.Models;

namespace WebApplication.BusinessLogic.Interface
{
    public interface ICmsCategoryRepository : IRepository<cms_Categories>
    {
        IQueryable<cms_Categories> GetExcept(int id);
        IQueryable<cms_Categories> GetExcepts(int[] id);
        Task<IQueryable<cms_Categories>> SearchCategories(string searchKey = null, string orderBy = null, bool orderbyDesc = false, int page = 1);
        cms_Categories GetCmsCategory(cms_Categories cmsCategory, int creatorId, int modiferId);
    }
}

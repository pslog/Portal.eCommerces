using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uow.Package.Data.Repositories;
using WebApplication.BusinessLogic.Interface;
using WebApplication.Models.Models;

namespace WebApplication.BusinessLogic.Repositories
{
    public class CmsNewsRepository : Repository<cms_News>, ICmsNewsRepository
    {
        public CmsNewsRepository(DbContext dbContext)
            : base(dbContext)
        {
        }

        public IQueryable<cms_News> GetNewsByCategory(int categoryId)
        {
            return DbSet.Where(n => n.CategoryID == categoryId);
        }
    }
}

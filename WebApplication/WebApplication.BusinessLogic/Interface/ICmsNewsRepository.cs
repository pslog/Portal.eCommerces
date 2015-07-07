using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uow.Package.Data.Repositories;
using WebApplication.Models.Models;

namespace WebApplication.BusinessLogic.Interface
{
    public interface ICmsNewsRepository : IRepository<cms_News>
    {
        IQueryable<cms_News> GetNewsByCategory(int categoryId);
    }
}

using System;
using System.Threading.Tasks;
using Uow.Package.Data.Repositories;
using WebApplication.BusinessLogic.Interface;
using WebApplication.BusinessLogic.Repositories;
using WebApplication.Models.Models;

namespace Uow.Package.Data
{
    /// <summary>
    /// Unit of work provides access to repositories.  Operations on multiple repositories are atomic through
    /// the use of Commit().
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PortalEntities db = new PortalEntities();

        private ICmsCategoryRepository cmsCategory;
        private ICmsNewsRepository cmsNews;


        public ICmsCategoryRepository CmsCategory { get { return cmsCategory ?? (cmsCategory = new CmsCategoryRepository(db)); } }
        public ICmsNewsRepository CmsNews { get { return cmsNews ?? (cmsNews = new CmsNewsRepository(db)); } }

        /// <summary>
        /// Factoring method for starting a new UOW
        /// </summary>
        public static IUnitOfWork Begin()
        {
            return DataContainer.Resolve<IUnitOfWork>();
        }

        /// <summary>
        /// Commits changes made to all repositories
        /// </summary>
        public void Commit()
        {
            try
            {
                db.SaveChanges();
            }
            catch(Exception)
            {
                db.Dispose();
            }
        }

        public Task<int> CommitAsync()
        {
            try
            {
                return db.SaveChangesAsync();
            }
            catch(Exception)
            {
                db.Dispose();
            }

            return Task.FromResult<int>(-1);
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}

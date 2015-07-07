using System;
using System.Threading.Tasks;
using Uow.Package.Data.Repositories;
using WebApplication.BusinessLogic.Interface;
using WebApplication.BusinessLogic.Repositories;

namespace Uow.Package.Data
{
    /// <summary>
    /// Unit of work provides access to repositories.  Operations on multiple repositories are atomic through
    /// the use of Commit().
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        ICmsCategoryRepository CmsCategory { get; }
        ICmsNewsRepository CmsNews { get; }
        void Commit();
        Task<int> CommitAsync();
    }
}
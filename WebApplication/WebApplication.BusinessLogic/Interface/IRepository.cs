using System;
using System.Linq;
using System.Linq.Expressions;

namespace Uow.Package.Data.Repositories
{
    public interface IRepository<T>
    {
        T GetById(int id);
        void Create(T entity);
        void Delete(T entity);
        IQueryable<T> GetAll();
    }
}
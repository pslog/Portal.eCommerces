using System;
using System.Linq;
using System.Linq.Expressions;

namespace Uow.Package.Data.Repositories
{
    public interface IRepository<T>
    {
        T GetById(int id);
        T GetById(int id, params string[] includeProperties);
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
        bool Update(T entity, params string[] updateProperties);
        IQueryable<T> GetAll();
    }
}
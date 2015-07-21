using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Uow.Package.Data.Repositories
{
    // TODO: Add more base functionality
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext DbContext;
        protected readonly DbSet<T> DbSet;

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }

        public T GetById(int id)
        {
            return DbContext.Set<T>().Find(id);
            //return DbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return DbContext.Set<T>().AsQueryable();
            //return DbSet.AsQueryable();
        }

        public void Create(T entity)
        {
            DbContext.Set<T>().Add(entity);
            //DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            //DbSet.Remove(entity);
        }
    }
}
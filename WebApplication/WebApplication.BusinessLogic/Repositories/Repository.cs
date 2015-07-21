using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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

        public T GetById(int id, params string[] includeProperties)
        {
            var dbSet = DbContext.Set<T>();//.AsQueryable();

            if (includeProperties != null)
            {
                foreach (var property in includeProperties)
                {
                    dbSet = (DbSet<T>)dbSet.Include(property);
                }
            }

            return dbSet.Find(id);
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

        public bool Update(T entity, params string[] updateProperties)
        {
            try
            {
                if (DbContext.Entry(entity).State == EntityState.Detached)
                {
                    DbSet.Attach(entity);
                }

                if (updateProperties != null)
                {
                    var entityProperties = entity.GetType().GetProperties();

                    foreach (var property in updateProperties)
                    {
                        DbContext.Entry(entity).Property(property).IsModified = true;
                    }
                }

                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public void Delete(T entity)
        {
            Type entityType = entity.GetType();

            foreach (var property in entityType.GetProperties())
            {
                if (property.PropertyType.Name == typeof(ICollection<>).Name)
                {
                    dynamic navProp = entityType.GetProperty(property.Name).GetValue(entity);
                    navProp.Clear();
                }
            }


            DbContext.Set<T>().Remove(entity);
            //DbSet.Remove(entity);
        }
    }
}
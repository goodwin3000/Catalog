using CatalogLib.Abstraction;
using CatalogLib.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLib.Concrete
{
   public class CatalogRepository<T, Pk> : IRepository<T, Pk> where T : class
    {
        private DataContext _context;

        public CatalogRepository()
        {
            _context = new DataContext();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = _context.Set<T>();
            return query;
        }
       
        public void Save()
        {
            _context.SaveChanges();
        }

        public T FindById(Pk id)
        {
            return _context.Set<T>().Find(id);
        }
    }
}

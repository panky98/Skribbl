using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq.Expressions;
using DataLayer.Models;

namespace DataLayer.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class, IEntityWithId
    {
        protected readonly ProjekatContext _context;

        public Repository(ProjekatContext context)
        {
            this._context = context;
        }
        public bool Add(T obj)
        {
            this._context.Set<T>().Add(obj);
            return true;
        }

        public bool DeleteAsync(T obj)
        {
            this._context.Set<T>().Remove(obj);
           // _context.Entry(obj).State = EntityState.Detached;
            return true;
        }

        public virtual ICollection<T> GetAll()
        {
            return new List<T>(this._context.Set<T>().ToListAsync<T>().Result.ToArray());
        }

        public IEnumerable<T> GetIncludes(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>().Include(includes[0]);
            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }
            return query.ToList();
        }
        public T GetOneWithIncludes(int id, params Expression<Func<T, object>>[] includes)
        {
            /*IQueryable<T> query = _context.Set<T>();
            if (includes != null)
                foreach (Expression<Func<T, object>> include in includes)
                    query = query.Include(include);

            return ((DbSet<T>)query).Find(id);*/

            IQueryable<T> query = _context.Set<T>().Include(includes[0]);
            foreach (var include in includes.Skip(1))
            {
                query = query.Include(include);
            }

            return query.SingleOrDefault(e => e.Id == id);
            
        }
        public virtual T GetOne(int id)
        {
            return this._context.Set<T>().Find(id);
        }

        public bool Update(T obj)
        {
            //_context.Entry(obj).State = EntityState.Modified;
            this._context.Set<T>().Update(obj);
            return true;
        }
    }
}

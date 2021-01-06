using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataLayer.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
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

        public bool Delete(T obj)
        {
            this._context.Set<T>().Remove(obj);
            return true;
        }

        public virtual ICollection<T> GetAll()
        {
            return new List<T>(this._context.Set<T>().ToListAsync<T>().Result.ToArray());
        }

        public virtual T GetOne(int id)
        {
            return this._context.Set<T>().Find(id);
        }

        public bool Update(T obj)
        {
            this._context.Set<T>().Update(obj);
            return true;
        }
    }
}

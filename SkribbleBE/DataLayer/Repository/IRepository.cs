using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataLayer.Repository
{
    public interface IRepository<T> where T : class, IEntityWithId
    {
        T GetOne(int id);
        bool Add(T obj);
        bool Delete(T obj);
        bool Update(T obj);
        ICollection<T> GetAll();
        IEnumerable<T> GetIncludes(params Expression<Func<T, Object>>[] includes);

        T GetOneWithIncludes(int id, params Expression<Func<T, object>>[] includes);
    }
}

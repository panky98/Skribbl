using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public interface IRepository<T> where T : class
    {
        T GetOne(int id);
        bool Add(T obj);
        bool Delete(T obj);
        bool Update(T obj);
        ICollection<T> GetAll();
    }
}

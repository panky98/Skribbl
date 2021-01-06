using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public interface IUnitOfWork
    {
        public void Commit();
        public void Rollback();
    }
}

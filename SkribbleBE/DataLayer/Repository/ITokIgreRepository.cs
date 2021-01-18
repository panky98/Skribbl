using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public interface ITokIgreRepository : IRepository<TokIgre>
    {
        IList<TokIgre> GetTokIgreByWordId(int idRec);
    }
}

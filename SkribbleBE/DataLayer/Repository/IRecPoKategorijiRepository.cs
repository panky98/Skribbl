using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public interface IRecPoKategorijiRepository : IRepository<RecPoKategoriji>
    {
        bool AddByIds(int idRec, int idKategorija);
        IList<Rec> GetAllWordsByCategoryId(int idKateg);
    }
}

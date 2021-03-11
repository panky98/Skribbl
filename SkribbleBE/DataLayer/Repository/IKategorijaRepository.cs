using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public interface IKategorijaRepository : IRepository<Kategorija>
    {
        Kategorija getKategorijaByName(string name);
    }
}

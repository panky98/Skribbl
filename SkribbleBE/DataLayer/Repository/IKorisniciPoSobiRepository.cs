using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public interface IKorisniciPoSobiRepository : IRepository<KorisnikPoSobi>
    {
        bool AddByIds(int idKorisnik, int idSoba);
    }
}

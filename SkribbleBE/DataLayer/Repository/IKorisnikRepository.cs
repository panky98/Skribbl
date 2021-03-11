using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public interface IKorisnikRepository : IRepository<Korisnik>
    {
        bool FindByUsername(string username);
        int FindIdByUsername(string username);
    }
}

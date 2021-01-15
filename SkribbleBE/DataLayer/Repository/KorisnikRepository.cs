using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Text;

namespace DataLayer.Repository
{
    public class KorisnikRepository :Repository<Korisnik>, IKorisnikRepository
    {

        private DbSet<Korisnik> korisnici;

        public KorisnikRepository(ProjekatContext _context)
            :base(_context)
        {
            this.korisnici = _context.Set<Korisnik>();
        }
    }
}

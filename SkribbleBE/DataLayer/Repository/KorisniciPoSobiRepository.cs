using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataLayer.Repository
{
    public class KorisniciPoSobiRepository : Repository<KorisnikPoSobi>, IKorisniciPoSobiRepository
    {
        private DbSet<KorisnikPoSobi> korisnici;

        public KorisniciPoSobiRepository(ProjekatContext _context) : base(_context)
        {
            this.korisnici = _context.Set<KorisnikPoSobi>();
        }
        public bool AddByIds(int idKorisnik, int idSoba)
        {
            Korisnik k = this._context.Set<Korisnik>().Where(x => x.Id == idKorisnik).FirstOrDefault();
            Soba s = this._context.Set<Soba>().Where(x => x.Id == idSoba).FirstOrDefault();
            if (k != null && s != null)
            {
                this.korisnici.Add(new KorisnikPoSobi(k, s));
                return true;
            }
            return false;
        }
    }
}

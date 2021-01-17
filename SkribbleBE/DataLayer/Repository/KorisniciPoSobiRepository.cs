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
        public void DeleteAllByRoomId(int idSoba)
        {
            IList<KorisnikPoSobi> korisniciPoSobiPoKorisniku = this.korisnici.Where(x => x.SobaId == idSoba).ToList();
            if (korisniciPoSobiPoKorisniku != null)
            {
                foreach (KorisnikPoSobi kps in korisniciPoSobiPoKorisniku)
                {
                    this.korisnici.Remove(kps);
                }
            }
        }
        public void DeleteAllByUserdId(int idKorisnik)
        {
            IList<KorisnikPoSobi> korisniciPoSobiPoKorisniku = this.korisnici.Where(x => x.KorisnikId == idKorisnik).ToList();
            if (korisniciPoSobiPoKorisniku != null)
            {
                foreach (KorisnikPoSobi kps in korisniciPoSobiPoKorisniku)
                {
                    this.korisnici.Remove(kps);
                }
            }
        }
        public IList<Korisnik> GetAllUsersByRoomId(int idSoba)
        {
            IList<KorisnikPoSobi> korisniciPoSobama = this.korisnici.Include(x => x.Korisnik).Include(x=>x.Soba).Where(x => x.SobaId == idSoba).ToList();
            IList<Korisnik> korisnici = new List<Korisnik>();
            foreach (KorisnikPoSobi k in korisniciPoSobama)
            {
                korisnici.Add(k.Korisnik);
            }
            return korisnici;
        }
    }
}

using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataLayer.Repository
{
    public class KategorijaRepository : Repository<Kategorija>, IKategorijaRepository
    {
        private DbSet<Kategorija> kategorije;

        public KategorijaRepository(ProjekatContext _context) : base(_context)
        {
            this.kategorije = _context.Set<Kategorija>();
        }

        public Kategorija getKategorijaByName(string name)
        {
            return this.kategorije.Where(k => k.Naziv.Equals(name)).FirstOrDefault();
        }
    }
}

using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public class KategorijaRepository : Repository<Kategorija>, IKategorijaRepository
    {
        private DbSet<Kategorija> kategorije;

        public KategorijaRepository(ProjekatContext _context) : base(_context)
        {
            this.kategorije = _context.Set<Kategorija>();
        }
    }
}

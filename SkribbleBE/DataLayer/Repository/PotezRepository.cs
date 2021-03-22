using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repository
{
    public class PotezRepository :Repository<Potez>, IPotezRepository
    {

        private DbSet<Potez> potezi;

        public PotezRepository(ProjekatContext _context)
            :base(_context)
        {
            this.potezi = _context.Set<Potez>();
        }

        public IList<Potez> VratiPotezeTokaIgre(int idTokaIgre)
        {
            IList<Potez> potezi= this.potezi.Include(p => p.Korisnik)
                    .Where(p => p.TokIgre.Id == idTokaIgre).OrderBy(x=>x.VremePoteza).ToList();
            return potezi;
        }
    }
}

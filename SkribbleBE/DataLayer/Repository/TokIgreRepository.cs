using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataLayer.Repository
{
    public class TokIgreRepository :Repository<TokIgre>, ITokIgreRepository
    {

        private DbSet<TokIgre> tokoviIgre;

        public TokIgreRepository(ProjekatContext _context)
            :base(_context)
        {
            this.tokoviIgre = _context.Set<TokIgre>();
        }
        public IList<TokIgre> GetTokIgreByWordId(int idRec)
        {
            return this.tokoviIgre.Where(x => x.RecZaPogadjanje.Id == idRec).ToList();
        }
    }
}

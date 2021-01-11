using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
    }
}

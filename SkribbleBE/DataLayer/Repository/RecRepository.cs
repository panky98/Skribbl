using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public class RecRepository : Repository<Rec>, IRecRepository
    {
        private DbSet<Rec> reci;

        public RecRepository(ProjekatContext _context) : base(_context)
        {
            this.reci = _context.Set<Rec>();
        }
    }
}

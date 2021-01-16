using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public class SobaRepository:Repository<Soba>, ISobaRepository
    {
        private DbSet<Soba> sobe;
        public SobaRepository(ProjekatContext _context) : base(_context)
        {
            this.sobe = _context.Set<Soba>();
        }
    }
}

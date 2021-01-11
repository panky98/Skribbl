using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

    }
}

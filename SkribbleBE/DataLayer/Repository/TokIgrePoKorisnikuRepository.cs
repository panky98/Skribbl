using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Repository
{
    public class TokIgrePoKorisnikuRepository :Repository<TokIgrePoKorisniku>, ITokIgrePoKorisnikuRepository
    {

        private DbSet<TokIgrePoKorisniku> tokoviIgrePoKorisniku;


        public TokIgrePoKorisnikuRepository(ProjekatContext _context)
            :base(_context)
        {
            tokoviIgrePoKorisniku = _context.Set<TokIgrePoKorisniku>();
        }
    }
}

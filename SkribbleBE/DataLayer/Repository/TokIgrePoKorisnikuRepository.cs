﻿using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IList<TokIgrePoKorisniku> VratiTokIgrePoKorisnikuZaTokIgre(int idTokaIgre)
        {
            IList<TokIgrePoKorisniku> tokoviIgrePoKorisniku =
                this.tokoviIgrePoKorisniku.Include(t => t.Korisnik)
                .Where(t => t.TokIgre.Id == idTokaIgre)
                .ToList();

            return tokoviIgrePoKorisniku;
                
        }
        public IList<TokIgrePoKorisniku> VratiTokIgrePoKorisnikuZaKorisnika(int idKorisnika)
        {
            IList<TokIgrePoKorisniku> tokoviIgrePoKorisniku =
                this.tokoviIgrePoKorisniku.Include(t => t.Korisnik).Include(t => t.TokIgre)
                .Where(t => t.Korisnik.Id == idKorisnika)
                .ToList();

            return tokoviIgrePoKorisniku;

        }
    }
}

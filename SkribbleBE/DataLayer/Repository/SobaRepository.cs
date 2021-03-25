using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void DeleteAllByCategoryId(int idKateg)
        {
            IList<Soba> deleteList = this.sobe.Where(x => x.KategorijaId == idKateg).ToList();
            if (deleteList != null)
            {
                foreach (Soba soba in deleteList)
                {
                    this.sobe.Remove(soba);
                }
            }
        }
        public IList<Soba> GetAllRoomByCategoryId(int idKateg)
        {
            return this.sobe.Where(x => x.KategorijaId == idKateg&&x.Status==true).ToList();
        }


    }
}

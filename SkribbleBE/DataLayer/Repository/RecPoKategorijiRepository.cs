using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataLayer.Repository
{
    public class RecPoKategorijiRepository : Repository<RecPoKategoriji>, IRecPoKategorijiRepository
    {
        private DbSet<RecPoKategoriji> reciPoKategorijama;

        public RecPoKategorijiRepository(ProjekatContext _context) : base(_context)
        {
            this.reciPoKategorijama = _context.Set<RecPoKategoriji>();
        }

        public bool AddByIds(int idRec, int idKategorija)
        {
            Rec r=this._context.Set<Rec>().Where(x => x.Id == idRec).FirstOrDefault();
            Kategorija k = this._context.Set<Kategorija>().Where(x => x.Id == idKategorija).FirstOrDefault();
            if(k!=null && r!=null)
            {
                this.reciPoKategorijama.Add(new RecPoKategoriji(r,k));
                return true;
            }
            return false;
        }
        public override RecPoKategoriji GetOne(int id)
        {
            return this.reciPoKategorijama.Include(x => x.Rec).Include(x => x.Kategorija).Where(x => x.Id == id).FirstOrDefault();
        }

        public override ICollection<RecPoKategoriji> GetAll()
        {
            return this.reciPoKategorijama.Include(x=>x.Rec).Include(x=>x.Kategorija).ToList();
        }

        public IList<Rec> GetAllWordsByCategoryId(int idKateg)
        {
            IList<RecPoKategoriji> reciPoKategorijama= this.reciPoKategorijama.Include(x => x.Rec).Include(x => x.Kategorija).Where(x => x.Kategorija.Id == idKateg).ToList();
            IList<Rec> returnList = new List<Rec>();
            foreach(RecPoKategoriji r in reciPoKategorijama)
            {
                returnList.Add(r.Rec);
            }
            return returnList;
        }

        public void DeleteAllByCategoryId(int idKateg)
        {
            IList<RecPoKategoriji> recPoKategoriji = this.reciPoKategorijama.Where(x => x.KategorijaId == idKateg).ToList();
            if(recPoKategoriji!=null)
            {
                foreach(RecPoKategoriji rec in recPoKategoriji)
                {
                    this.reciPoKategorijama.Remove(rec);
                }
            }
        }

        public void DeleteAllByWordId(int idRec)
        {
            IList<RecPoKategoriji> recPoKategoriji = this.reciPoKategorijama.Where(x => x.RecId == idRec).ToList();
            if (recPoKategoriji != null)
            {
                foreach (RecPoKategoriji rec in recPoKategoriji)
                {
                    this.reciPoKategorijama.Remove(rec);
                }
            }
        }
    }
}

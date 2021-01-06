using DataLayer.Models;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Services
{
    public class KategorijaService
    {
        private UnitOfWork unitOfWork;
        public KategorijaService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
        }

        public void AddNewKategorija(Kategorija k)
        {
            this.unitOfWork.KategorijaRepository.Add(k);
            this.unitOfWork.Commit();
        }

        public IList<Kategorija> GetAll()
        {
            return (List<Kategorija>)this.unitOfWork.KategorijaRepository.GetAll();
        }

        public void UpdateKategorija(Kategorija r)
        {
            this.unitOfWork.KategorijaRepository.Update(r);
            this.unitOfWork.Commit();
        }
        public void DeleteKategorija(Kategorija r)
        {
            this.unitOfWork.KategorijaRepository.Delete(r);
            this.unitOfWork.Commit();
        }
        public Kategorija getOneKategorija(int id)
        {
            return this.unitOfWork.KategorijaRepository.GetOne(id);
        }
    }
}

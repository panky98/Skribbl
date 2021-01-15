using DataLayer.Models;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Services
{
    public class RecPoKategorijiService
    {
        private UnitOfWork unitOfWork;
        public RecPoKategorijiService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
        }

        public void AddNewRecPoKategoriji(RecPoKategoriji k)
        {
            this.unitOfWork.RecPoKategorijiRepository.Add(k);
            this.unitOfWork.Commit();
        }

        public IList<RecPoKategoriji> GetAll()
        {
            return (List<RecPoKategoriji>)this.unitOfWork.RecPoKategorijiRepository.GetAll();
        }

        public void UpdateRecPoKategoriji(RecPoKategoriji r)
        {
            this.unitOfWork.RecPoKategorijiRepository.Update(r);
            this.unitOfWork.Commit();
        }
        public void DeleteRecPoKategoriji(RecPoKategoriji r)
        {
            this.unitOfWork.RecPoKategorijiRepository.DeleteAsync(r);
            this.unitOfWork.Commit();
        }
        public RecPoKategoriji getOneRecPoKategoriji(int id)
        {
            return this.unitOfWork.RecPoKategorijiRepository.GetOne(id);
        }
        public void CreateByIds(int idKat,int idRec)
        {
            this.unitOfWork.RecPoKategorijiRepository.AddByIds(idRec, idKat);
            this.unitOfWork.Commit();
        }
        public IList<Rec> GetAllWordsByCategoryId(int idKat)
        {
            return this.unitOfWork.RecPoKategorijiRepository.GetAllWordsByCategoryId(idKat);
        }
    }
}

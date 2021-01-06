using DataLayer.Models;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Services
{
    public class RecService
    {
        private UnitOfWork unitOfWork;

        public RecService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
        }

        public void AddNewRec(Rec r)
        {
            this.unitOfWork.RecRepository.Add(r);
            this.unitOfWork.Commit();
        }

        public IList<Rec> GetAll()
        {
            return (List<Rec>)this.unitOfWork.RecRepository.GetAll();
        }

        public void UpdateRec(Rec r)
        {
            this.unitOfWork.RecRepository.Update(r);
            this.unitOfWork.Commit();
        }
        public void DeleteRec(Rec r)
        {
            this.unitOfWork.RecRepository.Delete(r);
            this.unitOfWork.Commit();
        }
        public Rec getOneRec(int id)
        {
            return this.unitOfWork.RecRepository.GetOne(id);
        }
    }
}

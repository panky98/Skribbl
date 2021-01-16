using DataLayer.Models;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataLayer.Services
{
    public class SobaService
    {
        private UnitOfWork unitOfWork;

        public SobaService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
        }
        public void AddNewSoba(Soba s)
        {
            this.unitOfWork.SobaRepository.Add(s);
            this.unitOfWork.Commit();
        }

        public IList<Soba> GetAll()
        {
            return (List<Soba>)this.unitOfWork.SobaRepository.GetAll();
        }

        public void UpdateSoba(Soba s)
        {
            this.unitOfWork.SobaRepository.Update(s);
            this.unitOfWork.Commit();
        }
        public void DeleteSoba(Soba s)
        {
            this.unitOfWork.SobaRepository.Delete(s);
            this.unitOfWork.Commit();
        }
        public Soba getOneSoba(int id)
        {
            return this.unitOfWork.SobaRepository.GetOne(id);
        }
    }
}

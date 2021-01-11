using DataLayer.Models;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataLayer.Services
{
    public class PotezService
    {
        private UnitOfWork unitOfWork;

        public PotezService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
        }

        public IList<Potez> GetAll()
        {
            return (IList<Potez>)this.unitOfWork.PotezRepository.GetAll();
        }

        public IList<Potez> GetAllWithIncludes(params Expression<Func<Potez, object>>[] includes)
        {
            return (IList<Potez>)this.unitOfWork.PotezRepository.GetIncludes(includes);
        }

        public Potez GetOneWithIncludes( int id, params Expression<Func<Potez, object>>[] includes)
        {
            
            return (Potez)this.unitOfWork.PotezRepository.GetOneWithIncludes(id, includes);
           
        }

        public Potez GetOnePotez(int id)
        {
            return this.unitOfWork.PotezRepository.GetOne(id);
        }

        public void AddNewPotez(Potez potez)
        {
            this.unitOfWork.PotezRepository.Add(potez);
            this.unitOfWork.Commit();
        }

        public void UpdatePotez(Potez potez)
        {
            this.unitOfWork.PotezRepository.Update(potez);
            this.unitOfWork.Commit();
        }

        public void DeletePotez(Potez potez)
        {
            this.unitOfWork.PotezRepository.Delete(potez);
            this.unitOfWork.Commit();
        }
    }
}

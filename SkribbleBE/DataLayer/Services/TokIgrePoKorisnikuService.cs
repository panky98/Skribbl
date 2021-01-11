using DataLayer.Models;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataLayer.Services
{
    public class TokIgrePoKorisnikuService
    {
        private UnitOfWork unitOfWork;

        public TokIgrePoKorisnikuService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
        }

        public void AddNewTokIgrePoKorisniku(TokIgrePoKorisniku tokIgrePoKorisniku)
        {
            this.unitOfWork.TokIgrePoKorisnikuRepository.Add(tokIgrePoKorisniku);
            this.unitOfWork.Commit();
        }
        public IList<TokIgrePoKorisniku> GetAllWithIncludes(params Expression<Func<TokIgrePoKorisniku, object>>[] includes)
        {
            return (IList<TokIgrePoKorisniku>)this.unitOfWork.TokIgrePoKorisnikuRepository.GetIncludes(includes);
        }

        public TokIgrePoKorisniku GetOneWithIncludes(int id, params Expression<Func<TokIgrePoKorisniku, object>>[] includes)
        {
            return (TokIgrePoKorisniku) this.unitOfWork.TokIgrePoKorisnikuRepository.GetOneWithIncludes(id, includes);
        }
        public IList<TokIgrePoKorisniku> GetAll()
        {
            return (IList<TokIgrePoKorisniku>)this.unitOfWork.TokIgrePoKorisnikuRepository.GetAll();
        }

        public void UpdateTokIgrePoKorisniku(TokIgrePoKorisniku tokIgrePoKorisniku)
        {
            this.unitOfWork.TokIgrePoKorisnikuRepository.Update(tokIgrePoKorisniku);
            this.unitOfWork.Commit();
        }

        public void DeleteTokIgrePoKorisniku(TokIgrePoKorisniku tokIgrePoKorisniku)
        {
            this.unitOfWork.TokIgrePoKorisnikuRepository.Delete(tokIgrePoKorisniku);
            this.unitOfWork.Commit();
        }

        public TokIgrePoKorisniku GetOneTokIgrePoKorisniku(int id)
        {
            return this.unitOfWork.TokIgrePoKorisnikuRepository.GetOne(id);
        }
    }
}

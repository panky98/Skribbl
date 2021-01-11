using DataLayer.Models;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataLayer.Services
{
    public class TokIgreService
    {
        private UnitOfWork unitOfWork;

        public TokIgreService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
        }

        public void AddNewTokIgre(TokIgre tokIgre)
        {
            this.unitOfWork.TokIgreRepository.Add(tokIgre);
            this.unitOfWork.Commit();
        }

        public IList<TokIgre> GetAll()
        {
            return (IList<TokIgre>)this.unitOfWork.TokIgreRepository.GetAll();
        }
        public IList<TokIgre> GetAllWithIncludes(params Expression<Func<TokIgre, object>>[] includes)
        {
            return (IList<TokIgre>)this.unitOfWork.TokIgreRepository.GetIncludes(includes);
        }

        public TokIgre GetOneWithIncludes(int id, params Expression<Func<TokIgre, object>>[] includes)
        {
            return (TokIgre)this.unitOfWork.TokIgreRepository.GetOneWithIncludes(id, includes);
        }
        public void UpdateTokIgre(TokIgre tokIgre)
        {
            this.unitOfWork.TokIgreRepository.Update(tokIgre);
            this.unitOfWork.Commit();
        }

        public void DeleteTokIgre(TokIgre tokIgre)
        {
            this.unitOfWork.TokIgreRepository.Delete(tokIgre);
            this.unitOfWork.Commit();
        }

        public TokIgre GetOneTokIgre(int id)
        {
            return this.unitOfWork.TokIgreRepository.GetOne(id);
        }
    }
}

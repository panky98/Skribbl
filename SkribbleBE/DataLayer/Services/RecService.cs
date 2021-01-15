using DataLayer.Models;
using DataLayer.Repository;
using DTOs;
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

        public void AddNewRec(RecDTO r)
        {
            this.unitOfWork.RecRepository.Add(new Rec(r.Naziv));
            this.unitOfWork.Commit();
        }

        public IList<RecDTO> GetAll()
        {
            IList<RecDTO> returnList = new List<RecDTO>();
            foreach(Rec r in (List<Rec>)this.unitOfWork.RecRepository.GetAll())
            {
                returnList.Add(new RecDTO(r.Id,r.Naziv));
            }
            return returnList;
        }

        public void UpdateRec(RecDTO r)
        {
            Rec rec = new Rec
            {
                Id = r.Id,
                Naziv = r.Naziv
            };
            this.unitOfWork.RecRepository.Update(rec);
            this.unitOfWork.Commit();
        }
        public void DeleteRec(RecDTO r)
        {
            Rec rec = new Rec()
            {
                Id = r.Id,
                Naziv = r.Naziv
            };

            this.unitOfWork.RecPoKategorijiRepository.DeleteAllByWordId(r.Id);
            this.unitOfWork.RecRepository.Delete(rec);
            this.unitOfWork.Commit();
        }
        public RecDTO getOneRec(int id)
        {
            Rec r = this.unitOfWork.RecRepository.GetOne(id);
            if (r != null)
                return new RecDTO(r.Id, r.Naziv);
            else
                return null;
        }
    }
}

using DataLayer.DTOs;
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

        public void AddNewTokIgrePoKorisniku(TokIgrePoKorisnikuDTO tokIgrePoKorisnikuDTO)
        {
            TokIgrePoKorisniku tokIgrePoKorisniku = new TokIgrePoKorisniku();
            tokIgrePoKorisniku.Korisnik = this.unitOfWork.KorisnikRepository.GetOne(tokIgrePoKorisnikuDTO.Korisnik);
            tokIgrePoKorisniku.TokIgre = this.unitOfWork.TokIgreRepository.GetOneWithIncludes(tokIgrePoKorisnikuDTO.TokIgre, t => t.RecZaPogadjanje);

            this.unitOfWork.TokIgrePoKorisnikuRepository.Add(tokIgrePoKorisniku);
            this.unitOfWork.Commit();
        }
        public IList<TokIgrePoKorisnikuDTO> GetAllWithIncludes(params Expression<Func<TokIgrePoKorisniku, object>>[] includes)
        {
            IList<TokIgrePoKorisniku> tokoviIgrePoKorisniku= (IList<TokIgrePoKorisniku>)this.unitOfWork.TokIgrePoKorisnikuRepository.GetIncludes(includes);

            IList<TokIgrePoKorisnikuDTO> tokoviIgrePoKorisnikuDTO = new List<TokIgrePoKorisnikuDTO>();
            foreach(var t in tokoviIgrePoKorisniku)
            {
                tokoviIgrePoKorisnikuDTO.Add(new TokIgrePoKorisnikuDTO(t));
            }
            return tokoviIgrePoKorisnikuDTO;
        }

        public TokIgrePoKorisnikuDTO GetOneWithIncludes(int id, params Expression<Func<TokIgrePoKorisniku, object>>[] includes)
        {
            TokIgrePoKorisniku tokIgrePoKorisniku= (TokIgrePoKorisniku) this.unitOfWork.TokIgrePoKorisnikuRepository.GetOneWithIncludes(id, includes);
            return new TokIgrePoKorisnikuDTO(tokIgrePoKorisniku);
        }
        public IList<TokIgrePoKorisnikuDTO> GetAll()
        {
            IList<TokIgrePoKorisniku> tokoviIgrePoKorisniku = (IList<TokIgrePoKorisniku>)this.unitOfWork.TokIgrePoKorisnikuRepository.GetAll();

            IList<TokIgrePoKorisnikuDTO> tokoviIgrePoKorisnikuDTO = new List<TokIgrePoKorisnikuDTO>();
            foreach (var t in tokoviIgrePoKorisniku)
            {
                tokoviIgrePoKorisnikuDTO.Add(new TokIgrePoKorisnikuDTO(t));
            }
            return tokoviIgrePoKorisnikuDTO;
        }

        public void UpdateTokIgrePoKorisniku(TokIgrePoKorisnikuDTO tokIgrePoKorisnikuDTO)
        {
            TokIgrePoKorisniku tokIgrePoKorisniku = new TokIgrePoKorisniku();
            tokIgrePoKorisniku.NapraviOdDTO(tokIgrePoKorisnikuDTO);
            tokIgrePoKorisniku.Korisnik = this.unitOfWork.KorisnikRepository.GetOne(tokIgrePoKorisnikuDTO.Korisnik);
            tokIgrePoKorisniku.TokIgre = this.unitOfWork.TokIgreRepository.GetOneWithIncludes(tokIgrePoKorisnikuDTO.TokIgre, t => t.RecZaPogadjanje);

            this.unitOfWork.TokIgrePoKorisnikuRepository.Update(tokIgrePoKorisniku);
            this.unitOfWork.Commit();
        }

        public void DeleteTokIgrePoKorisniku(TokIgrePoKorisnikuDTO tokIgrePoKorisnikuDTO)
        {
            TokIgrePoKorisniku tokIgrePoKorisniku = new TokIgrePoKorisniku();
            tokIgrePoKorisniku.NapraviOdDTO(tokIgrePoKorisnikuDTO);

            
            tokIgrePoKorisniku.Korisnik = this.unitOfWork.KorisnikRepository.GetOne(tokIgrePoKorisnikuDTO.Korisnik);
            if (tokIgrePoKorisniku.Korisnik != null)
            {
                /* tokIgrePoKorisniku.Korisnik = null;
                 unitOfWork.TokIgrePoKorisnikuRepository.Update(tokIgrePoKorisniku);*/
                tokIgrePoKorisniku.Korisnik.TokIgrePoKorisniku.Remove(tokIgrePoKorisniku);
                unitOfWork.TokIgrePoKorisnikuRepository.Update(tokIgrePoKorisniku);
            }

            tokIgrePoKorisniku.TokIgre = this.unitOfWork.TokIgreRepository.GetOne(tokIgrePoKorisnikuDTO.TokIgre);
            if(tokIgrePoKorisniku.TokIgre!=null)
            {
                //tokIgrePoKorisniku.TokIgre = null;
                tokIgrePoKorisniku.TokIgre.TokIgrePoKorisniku.Remove(tokIgrePoKorisniku);
                unitOfWork.TokIgrePoKorisnikuRepository.Update(tokIgrePoKorisniku);
            }


            this.unitOfWork.TokIgrePoKorisnikuRepository.Delete(tokIgrePoKorisniku);
            this.unitOfWork.Commit();
        }

        public TokIgrePoKorisnikuDTO GetOneTokIgrePoKorisniku(int id)
        {
            TokIgrePoKorisniku tokIgrePoKorisniku= this.unitOfWork.TokIgrePoKorisnikuRepository.GetOne(id);
            return new TokIgrePoKorisnikuDTO(tokIgrePoKorisniku);
        }

        public int GetCountByTokIgreId(int tokIgreId)
        {
            IList<TokIgrePoKorisniku> retList = this.unitOfWork.TokIgrePoKorisnikuRepository.VratiTokIgrePoKorisnikuZaTokIgre(tokIgreId);
            int val = retList.Count;
            this.unitOfWork.Commit();
            return val;
        }
    }
}

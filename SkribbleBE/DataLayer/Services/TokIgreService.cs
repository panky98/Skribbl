using DataLayer.DTOs;
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

        public int AddNewTokIgre(TokIgreDTO tokIgreDTO)
        {
            TokIgre tokIgre = new TokIgre();
            tokIgre.NapraviOdDTO(tokIgreDTO);

            tokIgre.RecZaPogadjanje = this.unitOfWork.RecRepository.GetOne(tokIgreDTO.RecZaPogadjanjeId);
            tokIgre.Soba = this.unitOfWork.SobaRepository.GetOne(tokIgreDTO.SobaId);

            this.unitOfWork.TokIgreRepository.Add(tokIgre);
            this.unitOfWork.Commit();

            return tokIgre.Id;
        }

        public IList<TokIgreDTO> GetAll()
        {
            IList<TokIgre> tokoviIgre= (IList<TokIgre>)this.unitOfWork.TokIgreRepository.GetAll();
            IList<TokIgreDTO> tokoviIgreDTO = new List<TokIgreDTO>();
            foreach(var t in tokoviIgre)
            {
                tokoviIgreDTO.Add(new TokIgreDTO(t));
            }
            return tokoviIgreDTO;
        }
        public IList<TokIgreDTO> GetAllWithIncludes(params Expression<Func<TokIgre, object>>[] includes)
        {
            IList<TokIgre> tokoviIgre = (IList<TokIgre>)this.unitOfWork.TokIgreRepository.GetIncludes(includes);

            IList<TokIgreDTO> tokoviIgreDTO = new List<TokIgreDTO>();
            foreach (var t in tokoviIgre)
            {
                tokoviIgreDTO.Add(new TokIgreDTO(t));
            }
            return tokoviIgreDTO;
        }

        public TokIgreDTO GetOneWithIncludes(int id, params Expression<Func<TokIgre, object>>[] includes)
        {
            TokIgre tokIgre= (TokIgre)this.unitOfWork.TokIgreRepository.GetOneWithIncludes(id, includes);
            return new TokIgreDTO(tokIgre);
        }
        public void UpdateTokIgre(TokIgreDTO tokIgreDTO)
        {
            TokIgre tokIgre = new TokIgre();

            tokIgre.NapraviOdDTO(tokIgreDTO);
            tokIgre.RecZaPogadjanje = this.unitOfWork.RecRepository.GetOne(tokIgreDTO.RecZaPogadjanjeId);
            tokIgre.Soba = this.unitOfWork.SobaRepository.GetOne(tokIgreDTO.SobaId);

            this.unitOfWork.TokIgreRepository.Update(tokIgre);
            this.unitOfWork.Commit();
        }

        public void  DeleteTokIgreAsync(TokIgreDTO tokIgreDTO,TokIgre param=null)
        {
            TokIgre tokIgre;
            if (param == null)
            {
                tokIgre = new TokIgre();

                tokIgre.NapraviOdDTO(tokIgreDTO);
            }
            else
            {
                tokIgre = param;
            }

            //tokIgre.RecZaPogadjanje = this.unitOfWork.RecRepository.GetOne(tokIgreDTO.RecZaPogadjanjeId);
            //tokIgre.RecZaPogadjanje.TokoviIgre.Remove(tokIgre);
            //this.unitOfWork.TokIgreRepository.Update(tokIgre);


            tokIgre.Potezi = this.unitOfWork.PotezRepository.VratiPotezeTokaIgre(tokIgreDTO.Id);
            tokIgre.TokIgrePoKorisniku = this.unitOfWork.TokIgrePoKorisnikuRepository.VratiTokIgrePoKorisnikuZaTokIgre(tokIgreDTO.Id);
            //this.unitOfWork.Commit();
            //prvo moramo obrisati sve poteze i tokove igre po korisniku...i jos nesto ako ima

           foreach(var p in tokIgre.Potezi)
            {
                this.unitOfWork.PotezRepository.Delete(p);
            }
            //this.unitOfWork.Commit();

            foreach(var t in tokIgre.TokIgrePoKorisniku)
            {
                this.unitOfWork.TokIgrePoKorisnikuRepository.Delete(t);
            }

            //this.unitOfWork.Commit();

            

            this.unitOfWork.TokIgreRepository.Delete(tokIgre);
            this.unitOfWork.Commit();
        }

        public TokIgreDTO GetOneTokIgre(int id)
        {
            TokIgre tokIgre= this.unitOfWork.TokIgreRepository.GetOne(id);
            return new TokIgreDTO(tokIgre);
        }
    }
}

using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Confluent.Kafka;
using System.Threading;

namespace DataLayer.Services
{
    public class SobaService
    {
        private UnitOfWork unitOfWork;
        private TokIgreService TokIgreService;

        public SobaService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
            TokIgreService = new TokIgreService(projekatContext);
        }
        public void AddNewSoba(SobaDTO s)
        {
            Soba soba = new Soba()
            {
                Id = s.Id,
                Naziv = s.Naziv,
                Kategorija = this.unitOfWork.KategorijaRepository.GetOne(s.Kategorija.Id)
            };
            this.unitOfWork.SobaRepository.Add(soba);
            this.unitOfWork.Commit();
        }
        public IList<SobaDTO> GetAllWithIncludes(params Expression<Func<Soba, object>>[] includes)
        {
            IList<Soba> sobe = (IList<Soba>)this.unitOfWork.SobaRepository.GetIncludes(includes);


            IList<SobaDTO> sobeDTO = new List<SobaDTO>();
            foreach (var s in sobe)
            {
                IList<TokIgre> tokoviIgre = this.unitOfWork.TokIgreRepository.GetTokIgreByRoomId(s.Id);
                sobeDTO.Add(new SobaDTO()
                {
                    Id = s.Id,
                    Naziv = s.Naziv,
                    Kategorija = new KategorijaDTO()
                    {
                        Id = s.Kategorija.Id,
                        Naziv = s.Kategorija.Naziv
                    }

                });
            }
            return sobeDTO;
        }

        public IList<SobaDTO> GetAll()
        {
            IList<SobaDTO> returnList = new List<SobaDTO>();
            foreach (Soba s in (List<Soba>)this.unitOfWork.SobaRepository.GetAll())
            {
                SobaDTO sobaDTO = new SobaDTO(s.Id, s.Naziv);
                Kategorija k= this.unitOfWork.KategorijaRepository.GetOne(s.KategorijaId);
                sobaDTO.Kategorija = new KategorijaDTO(k.Id, k.Naziv);
                returnList.Add(sobaDTO);
            }
            return returnList;
        }

        public void UpdateSoba(SobaDTO s)
        {
            Soba soba = new Soba()
            {
                Id = s.Id,
                Naziv = s.Naziv,
                Kategorija = new Kategorija()
                {
                    Id = s.Kategorija.Id,
                    Naziv = s.Kategorija.Naziv
                }
            };
            this.unitOfWork.SobaRepository.Update(soba);
            this.unitOfWork.Commit();
        }
        public void DeleteSoba(SobaDTO s,Soba param=null)
        {

            this.unitOfWork.KorisniciPoSobiRepository.DeleteAllByRoomId(s.Id);
            IList<TokIgre> tokoviIgre = this.unitOfWork.TokIgreRepository.GetTokIgreByRoomId(s.Id);
            foreach(TokIgre t in tokoviIgre)
            {
                DTOs.TokIgreDTO tokIgreDTO = new DTOs.TokIgreDTO()
                {
                    Id = t.Id
                };
                TokIgreService.DeleteTokIgreAsync(tokIgreDTO,t);
            }
            Soba soba = new Soba();
            if (param == null)
            {
                 soba = new Soba()
                {
                    Id = s.Id,
                    Naziv = s.Naziv,
                    Kategorija = new Kategorija()
                    {
                        Id = s.Kategorija.Id,
                        Naziv = s.Kategorija.Naziv
                    }
                };
            }
            else
            {
                soba = param;
            }
            
            this.unitOfWork.SobaRepository.Delete(soba);
            this.unitOfWork.Commit();
        }
        public SobaDTO getOneSoba(int id)
        {


            Soba s = this.unitOfWork.SobaRepository.GetOne(id);
            if(s!=null)
            {
                SobaDTO sobaDTO = new SobaDTO(s.Id, s.Naziv);
                Kategorija k = this.unitOfWork.KategorijaRepository.GetOne(s.KategorijaId);
                sobaDTO.Kategorija = new KategorijaDTO(k.Id, k.Naziv);
                return sobaDTO;
            }
            return null;
        }

        public IList<SobaDTO> getAllByCategoryId(int idCateg)
        {
            IList<Soba> sobe = this.unitOfWork.SobaRepository.GetAllRoomByCategoryId(idCateg);
            IList<SobaDTO> retList = new List<SobaDTO>();
            if(sobe!=null && sobe.Count>0)
            {
                foreach(Soba s in sobe)
                {
                   SobaDTO newSoba=new SobaDTO()
                    {
                        Id=s.Id,
                        Naziv=s.Naziv
                    };
                    Kategorija kateg = this.unitOfWork.KategorijaRepository.GetOne(s.KategorijaId);
                    newSoba.Kategorija = new KategorijaDTO(kateg.Id, kateg.Naziv);
                    retList.Add(newSoba);
                }
            }
            return retList;
        }

    }
}

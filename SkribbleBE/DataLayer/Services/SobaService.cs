using DataLayer.Models;
using DataLayer.Repository;
using DTOs;
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

        public IList<SobaDTO> GetAll()
        {
            return (List<SobaDTO>)this.unitOfWork.SobaRepository.GetAll();
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
        public void DeleteSoba(SobaDTO s)
        {
            this.unitOfWork.KorisniciPoSobiRepository.DeleteAllByRoomId(s.Id);
            this.unitOfWork.Commit();

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

            this.unitOfWork.SobaRepository.Delete(soba);
            this.unitOfWork.Commit();
        }
        public SobaDTO getOneSoba(int id)
        {
            Soba s = this.unitOfWork.SobaRepository.GetOne(id);
            if(s!=null)
            {
                SobaDTO soba = new SobaDTO()
                {
                    Id = s.Id,
                    Naziv = s.Naziv,
                    Kategorija = new KategorijaDTO()
                    {
                        Id = s.Kategorija.Id,
                        Naziv = s.Kategorija.Naziv
                    }
                };
                return soba;
            }
            return null;
        }

    }
}

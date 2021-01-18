﻿using DataLayer.Models;
using DataLayer.Repository;
using DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Services
{
    public class KategorijaService
    {
        private UnitOfWork unitOfWork;
        private SobaService SobaService;
        public KategorijaService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
            SobaService = new SobaService(projekatContext);
        }

        public void AddNewKategorija(KategorijaDTO k)
        {
            this.unitOfWork.KategorijaRepository.Add(new Kategorija(k.Naziv));
            this.unitOfWork.Commit();
        }

        public IList<KategorijaDTO> GetAll()
        {
            IList<KategorijaDTO> returnList = new List<KategorijaDTO>();
            foreach(Kategorija k in (List<Kategorija>)this.unitOfWork.KategorijaRepository.GetAll())
            {
                returnList.Add(new KategorijaDTO(k.Id,k.Naziv));
            }
            return returnList;
        }

        public void UpdateKategorija(KategorijaDTO r)
        {
            Kategorija rec = new Kategorija()
            {
                Id = r.Id,
                Naziv = r.Naziv
            };
            this.unitOfWork.KategorijaRepository.Update(rec);
            this.unitOfWork.Commit();
        }
        public void DeleteKategorija(KategorijaDTO r)
        {
            Kategorija kategorija = new Kategorija()
            {
                Id = r.Id,
                Naziv = r.Naziv
            };
            IList<Soba> sobe = this.unitOfWork.SobaRepository.GetAllRoomByCategoryId(r.Id);
            for (int i = 0; i < sobe.Count; i++)
            {
                SobaDTO sobaDTO = new SobaDTO()
                {
                    Id = sobe[i].Id
                };
                SobaService.DeleteSoba(sobaDTO, sobe[i]);
            }

            this.unitOfWork.SobaRepository.DeleteAllByCategoryId(r.Id);

            this.unitOfWork.RecPoKategorijiRepository.DeleteAllByCategoryId(r.Id);

            this.unitOfWork.KategorijaRepository.Delete(kategorija);

            //da slucajno neka rec nije ostala bez kategorije
            IList<Rec> reci = (IList<Rec>)this.unitOfWork.RecRepository.GetAll();
            foreach(Rec rec in reci)
            {
                if(rec.RecPoKategoriji==null || rec.RecPoKategoriji.Count==0)
                {
                    this.unitOfWork.RecRepository.Delete(rec);
                }
            }
            this.unitOfWork.Commit();
        }
        public KategorijaDTO getOneKategorija(int id)
        {
            Kategorija k = this.unitOfWork.KategorijaRepository.GetOne(id);
            if (k != null)
                return new KategorijaDTO(k.Id, k.Naziv);
            else
                return null;
        }
    }
}

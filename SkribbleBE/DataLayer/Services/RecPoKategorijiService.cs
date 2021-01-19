using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Services
{
    public class RecPoKategorijiService
    {
        private UnitOfWork unitOfWork;
        public RecPoKategorijiService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
        }

        public void AddNewRecPoKategoriji(RecPoKategorijiDTO k)
        {
            RecPoKategoriji recPoKategoriji = new RecPoKategoriji()
            {
                Id = k.Id,
                Rec = new Rec()
                {
                    Id = k.Rec.Id,
                    Naziv = k.Rec.Naziv
                },
                Kategorija = new Kategorija()
                {
                    Id = k.Kategorija.Id,
                    Naziv = k.Kategorija.Naziv
                }
            };
            this.unitOfWork.RecPoKategorijiRepository.Add(recPoKategoriji);
            this.unitOfWork.Commit();
        }

        public IList<RecPoKategorijiDTO> GetAll()
        {
            IList<RecPoKategorijiDTO> returnList = new List<RecPoKategorijiDTO>();
            foreach(RecPoKategoriji recPoKategoriji in (List<RecPoKategoriji>)this.unitOfWork.RecPoKategorijiRepository.GetAll())
            {
                RecPoKategorijiDTO recPoKategoriji1 = new RecPoKategorijiDTO()
                {
                    Id = recPoKategoriji.Id,
                    Rec = new RecDTO()
                    {
                        Id = recPoKategoriji.Rec.Id,
                        Naziv = recPoKategoriji.Rec.Naziv
                    },
                    Kategorija = new KategorijaDTO
                    {
                        Id = recPoKategoriji.Kategorija.Id,
                        Naziv = recPoKategoriji.Kategorija.Naziv
                    }
                };
                returnList.Add(recPoKategoriji1);
            }
            return returnList;
        }

        public void UpdateRecPoKategoriji(RecPoKategorijiDTO k)
        {
            RecPoKategoriji recPoKategoriji = new RecPoKategoriji()
            {
                Id = k.Id,
                Rec = new Rec()
                {
                    Id = k.Rec.Id,
                    Naziv = k.Rec.Naziv
                },
                Kategorija = new Kategorija()
                {
                    Id = k.Kategorija.Id,
                    Naziv = k.Kategorija.Naziv
                }
            };
            this.unitOfWork.RecPoKategorijiRepository.Update(recPoKategoriji);
            this.unitOfWork.Commit();
        }
        public void DeleteRecPoKategoriji(RecPoKategorijiDTO k)
        {
            RecPoKategoriji recPoKategoriji = new RecPoKategoriji()
            {
                Id = k.Id,
                Rec = new Rec()
                {
                    Id = k.Rec.Id,
                    Naziv = k.Rec.Naziv
                },
                Kategorija = new Kategorija()
                {
                    Id = k.Kategorija.Id,
                    Naziv = k.Kategorija.Naziv
                }
            };
            this.unitOfWork.RecPoKategorijiRepository.Delete(recPoKategoriji);
            this.unitOfWork.Commit();
        }
        public RecPoKategorijiDTO getOneRecPoKategoriji(int id)
        {
            RecPoKategoriji rec = this.unitOfWork.RecPoKategorijiRepository.GetOne(id);
            if(rec!=null)
            {
                RecPoKategorijiDTO recPoKategoriji = new RecPoKategorijiDTO()
                {
                    Id = rec.Id,
                    Rec = new RecDTO()
                    {
                        Id = rec.Rec.Id,
                        Naziv = rec.Rec.Naziv
                    },
                    Kategorija = new KategorijaDTO()
                    {
                        Id = rec.Kategorija.Id,
                        Naziv = rec.Kategorija.Naziv
                    }
                };
                return recPoKategoriji;
            }
            return null;
        }
        public void CreateByIds(int idKat,int idRec)
        {
            this.unitOfWork.RecPoKategorijiRepository.AddByIds(idRec, idKat);
            this.unitOfWork.Commit();
        }
        public IList<RecDTO> GetAllWordsByCategoryId(int idKat)
        {
            IList<RecDTO> returnList = new List<RecDTO>();
            foreach (Rec rec in this.unitOfWork.RecPoKategorijiRepository.GetAllWordsByCategoryId(idKat))
            {
                returnList.Add(new RecDTO(rec.Id,rec.Naziv));
            }
            return returnList;
        }

        public void DeleteAllByCategoryId(int idKateg)
        {
            this.unitOfWork.RecPoKategorijiRepository.DeleteAllByCategoryId(idKateg);
            this.unitOfWork.Commit();
        }
    }
}

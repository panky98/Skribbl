using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.DTOs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using ServiceStack.Redis;
using DataLayer.SignalR;

namespace DataLayer.Services
{
    public class RecService
    {
        private UnitOfWork unitOfWork;
        private TokIgreService TokIgreService;
        readonly RedisClient redis = new RedisClient(Config.SingleHost);

        public RecService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
            TokIgreService = new TokIgreService(projekatContext);

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
            IList<TokIgre> tokoviIgre= this.unitOfWork.TokIgreRepository.GetTokIgreByWordId(r.Id);
            for(int i=0;i<tokoviIgre.Count;i++)
            {
                DTOs.TokIgreDTO tokIgreDTO = new DTOs.TokIgreDTO()
                {
                    Id = tokoviIgre[i].Id
                };
                TokIgreService.DeleteTokIgreAsync(tokIgreDTO,tokoviIgre[i]);
            }
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

        public IList<RecDTO> getThreeRecFromKategorija(int idKategorija,string groupName)
        {
            IList<RecDTO> returnList = new List<RecDTO>();

            IList<Rec> reci=unitOfWork.RecPoKategorijiRepository.GetAllWordsByCategoryId(idKategorija);
            IList<string> prezentovaneReci = redis.GetAllItemsFromList("groupListWords:" + groupName);

            if(reci.Count>=3)
            {
                for(int i=0;i<3;i++)
                {
                    Random r = new Random();
                    int index = r.Next(0, reci.Count);

                    bool finded = false;
                    if(prezentovaneReci!=null)
                    {
                        for(int j=0;j<prezentovaneReci.Count;j++)
                        {
                            if(prezentovaneReci[j]==reci[index].Naziv)
                            {
                                finded = true;
                                break;
                            }
                        }
                    }

                    for(int j=0;j<returnList.Count;j++)
                    {
                        if(returnList[j].Naziv==reci[index].Naziv)
                        {
                            finded = true;
                            break;
                        }
                    }

                    if (!finded)
                        returnList.Add(new RecDTO(reci[index].Id, reci[index].Naziv));
                    else
                        i--;
                }
            }

            return returnList;
        }
    }
}

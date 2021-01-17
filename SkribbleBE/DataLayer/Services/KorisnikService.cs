using DataLayer.Models;
using DataLayer.Repository;
using System;
using DTOs;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataLayer.Services
{
    public class KorisnikService
    {

        private UnitOfWork unitOfWork;

        public KorisnikService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
        }
        public void AddNewKorisnik(KorisnikDTO k)
        {
            this.unitOfWork.KorisnikRepository.Add(new Korisnik(k.Username,k.Password));
            this.unitOfWork.Commit();
        }

        public IList<KorisnikDTO> GetAll()
        {
            IList<KorisnikDTO> returnList = new List<KorisnikDTO>();
            foreach (Korisnik k in (List<Korisnik>)this.unitOfWork.KorisnikRepository.GetAll())
            {
                returnList.Add(new KorisnikDTO(k.Id, k.Username,k.Password));
            }
            return returnList;
        }

        public void UpdateKorisnik(KorisnikDTO k)
        {
            Korisnik korisnik = new Korisnik()
            {
                Id = k.Id,
                Username = k.Username,
                Password=k.Password
            };
            this.unitOfWork.KorisnikRepository.Update(korisnik);
            this.unitOfWork.Commit();
        }
        public void DeleteKorisnik(KorisnikDTO k)
        {
            this.unitOfWork.KorisniciPoSobiRepository.DeleteAllByUserdId(k.Id);
            this.unitOfWork.Commit();
            Korisnik korisnik = new Korisnik()
            {
                Id = k.Id,
                Username = k.Username,
                Password = k.Password
            };
            this.unitOfWork.KorisnikRepository.Delete(korisnik);
            this.unitOfWork.Commit();


        }
        public Korisnik getOneKorisnik(int id)
        {
            return this.unitOfWork.KorisnikRepository.GetOne(id);
        }
        public bool findKorisnik(string username)
        {
            return this.unitOfWork.KorisnikRepository.FindByUsername(username);
        }
    }
}

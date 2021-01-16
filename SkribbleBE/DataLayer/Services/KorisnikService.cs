using DataLayer.Models;
using DataLayer.Repository;
using System;
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
        public void AddNewKorisnik(Korisnik k)
        {
            this.unitOfWork.KorisnikRepository.Add(k);
            this.unitOfWork.Commit();
        }

        public IList<Korisnik> GetAll()
        {
            return (List<Korisnik>)this.unitOfWork.KorisnikRepository.GetAll();
        }

        public void UpdateKorisnik(Korisnik k)
        {
            this.unitOfWork.KorisnikRepository.Update(k);
            this.unitOfWork.Commit();
        }
        public void DeleteKorisnik(Korisnik k)
        {
            this.unitOfWork.KorisnikRepository.Delete(k);
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

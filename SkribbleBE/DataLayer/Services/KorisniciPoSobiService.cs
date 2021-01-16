using DataLayer.Models;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataLayer.Services
{
    public class KorisniciPoSobiService
    {
        private UnitOfWork unitOfWork;

        public KorisniciPoSobiService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
        }
        public void AddNewKorisniciPoSobi(KorisnikPoSobi k)
        {
            this.unitOfWork.KorisniciPoSobiRepository.Add(k);
            this.unitOfWork.Commit();
        }

        public IList<KorisnikPoSobi> GetAll()
        {
            return (List<KorisnikPoSobi>)this.unitOfWork.KorisniciPoSobiRepository.GetAll();
        }

        public void UpdateKorisniciPoSobi(KorisnikPoSobi k)
        {
            this.unitOfWork.KorisniciPoSobiRepository.Update(k);
            this.unitOfWork.Commit();
        }
        public void DeleteKorisniciPoSobi(KorisnikPoSobi k)
        {
            this.unitOfWork.KorisniciPoSobiRepository.Delete(k);
            this.unitOfWork.Commit();
        }
        public KorisnikPoSobi getOneKorisnikPoSobi(int id)
        {
            return this.unitOfWork.KorisniciPoSobiRepository.GetOne(id);
        }
        public IList<KorisnikPoSobi> GetAllWithIncludes(params Expression<Func<KorisnikPoSobi, object>>[] includes)
        {
            return (IList<KorisnikPoSobi>)this.unitOfWork.KorisniciPoSobiRepository.GetIncludes(includes);
        }

        public KorisnikPoSobi GetOneWithIncludes(int id, params Expression<Func<KorisnikPoSobi, object>>[] includes)
        {
            return (KorisnikPoSobi)this.unitOfWork.KorisniciPoSobiRepository.GetOneWithIncludes(id, includes);
        }
        public void CreateByIds(int idKorisnik, int idSoba)
        {
            this.unitOfWork.KorisniciPoSobiRepository.AddByIds(idKorisnik, idSoba);
            this.unitOfWork.Commit();
        }
    }
}

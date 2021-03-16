using DataLayer.DTOs;
using DataLayer.Models;
using DataLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataLayer.Services
{
    public class PotezService
    {
        private UnitOfWork unitOfWork;

        public PotezService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
        }

        public IList<PotezDTO> GetAll()
        {
            IList<Potez> potezi= (IList<Potez>)this.unitOfWork.PotezRepository.GetAll();
            IList<PotezDTO> poteziDTO = new List<PotezDTO>();
            foreach(var p in potezi)
            {
                poteziDTO.Add(new PotezDTO(p));
            }
            return poteziDTO;
        }

        public IList<PotezDTO> GetAllWithIncludes(params Expression<Func<Potez, object>>[] includes)
        {
            IList<Potez> potezi = (IList<Potez>)this.unitOfWork.PotezRepository.GetIncludes(includes);
            IList<PotezDTO> poteziDTO = new List<PotezDTO>();
            foreach (var p in potezi)
            {
                poteziDTO.Add(new PotezDTO(p));
            }
            return poteziDTO;
        }

        public PotezDTO GetOneWithIncludes( int id, params Expression<Func<Potez, object>>[] includes)
        {
            
            Potez potez= (Potez)this.unitOfWork.PotezRepository.GetOneWithIncludes(id, includes);
            return new PotezDTO(potez);
           
        }

        public PotezDTO GetOnePotez(int id)
        {
            Potez potez = this.unitOfWork.PotezRepository.GetOne(id);
            return new PotezDTO(potez);
        }

        public void AddNewPotez(PotezDTO potezDTO)
        {
            Potez potez = new Potez();
            /*potez.VremePoteza = potezDTO.VremePoteza;
            potez.Crtanje = potezDTO.Crtanje;
            potez.Poruka = potezDTO.Poruka;
            potez.TekstPoruke = potezDTO.TekstPoruke;
            potez.BojaLinije = potezDTO.BojaLinije;
            potez.ParametarLinije = potezDTO.ParametarLinije;*/

            potez.NapraviOdDTO(potezDTO);

            potez.TokIgre = this.unitOfWork.TokIgreRepository.GetOne(potezDTO.TokIgreId);
            //korisnik
            potez.Korisnik = this.unitOfWork.KorisnikRepository.GetOne(potezDTO.KorisnikId);

            this.unitOfWork.PotezRepository.Add(potez);
            this.unitOfWork.Commit();
        }

        public void UpdatePotez(PotezDTO potezDTO)
        {
            Potez potez = new Potez();
            potez.NapraviOdDTO(potezDTO);
            

            potez.TokIgre = this.unitOfWork.TokIgreRepository.GetOne(potezDTO.TokIgreId);
                
            potez.Korisnik = this.unitOfWork.KorisnikRepository.GetOne(potezDTO.KorisnikId);
            this.unitOfWork.PotezRepository.Update(potez);
            this.unitOfWork.Commit();
        }

        public void DeletePotez(PotezDTO potezDTO)
        {
            Potez potez = new Potez();
            potez.NapraviOdDTO(potezDTO);
            potez.TokIgre = this.unitOfWork.TokIgreRepository.GetOneWithIncludes(potezDTO.TokIgreId,p => p.RecZaPogadjanje);
            potez.Korisnik = this.unitOfWork.KorisnikRepository.GetOne(potezDTO.KorisnikId);
            this.unitOfWork.Commit();
            this.unitOfWork.PotezRepository.Delete(potez);
            this.unitOfWork.Commit();
        }
    }
}

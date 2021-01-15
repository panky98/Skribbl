using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTOs
{
    public class PotezDTO
    {
        
        public int Id { get; set; }

        public DateTime VremePoteza { get; set; }

        public bool Crtanje { get; set; }
        
        public bool Poruka { get; set; }

        public string TekstPoruke { get; set; }

        public string BojaLinije { get; set; }

        public string ParametarLinije { get; set; }
  
        public int KorisnikId { get; set; }

        
        public int TokIgreId { get; set; }

        public PotezDTO()
        {

        }

        public PotezDTO(Potez potez)
        {
            this.Id = potez.Id;
            this.VremePoteza = potez.VremePoteza;
            this.Crtanje = potez.Crtanje;
            this.Poruka = potez.Poruka;
            this.TekstPoruke = potez.TekstPoruke;
            this.BojaLinije = potez.BojaLinije;
            this.ParametarLinije = potez.ParametarLinije;
            if(potez.Korisnik!=null)
                this.KorisnikId = potez.Korisnik.Id;
            if(potez.TokIgre!=null)
                this.TokIgreId = potez.TokIgre.Id;
        }
    }
}

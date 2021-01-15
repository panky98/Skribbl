using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTOs
{
    public class TokIgrePoKorisnikuDTO
    {
        public int Id { get; set; }
        public int Korisnik { get; set; }
        public int TokIgre { get; set; }

        public TokIgrePoKorisnikuDTO()
        {

        }

        public TokIgrePoKorisnikuDTO(TokIgrePoKorisniku tokIgrePoKorisniku)
        {
            Id = tokIgrePoKorisniku.Id;
            if (tokIgrePoKorisniku.Korisnik != null)
                Korisnik = tokIgrePoKorisniku.Korisnik.Id;
            if (tokIgrePoKorisniku.TokIgre != null)
                TokIgre = tokIgrePoKorisniku.TokIgre.Id;
        }

    }
}

using DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DataLayer.Models
{
    public class TokIgrePoKorisniku :IEntityWithId
    {

        [Key]
        public int Id { get; set; }

        [ForeignKey("KorisnikId")]
       // [JsonIgnore]
        public Korisnik Korisnik { get; set; }

        [ForeignKey("TokIgreId")]
        //[JsonIgnore]
        public TokIgre TokIgre { get; set; }

        public TokIgrePoKorisniku()
        {

        }

        public void NapraviOdDTO(TokIgrePoKorisnikuDTO tokIgrePoKorisnikuDTO)
        {
            Id = tokIgrePoKorisnikuDTO.Id;
        }

        
    }
}

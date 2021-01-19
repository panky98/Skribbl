using DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DataLayer.Models
{
    public class Potez : IEntityWithId
    {
        [Key]
        public int Id { get; set; }

        public DateTime VremePoteza { get; set; }

        [Required]
        public bool Crtanje { get; set; }

        [Required]
        public bool Poruka { get; set; }

        public string TekstPoruke { get; set; }

        public string BojaLinije { get; set; }

        public string ParametarLinije { get; set; }

        [ForeignKey("KorisnikId")]
        //[JsonIgnore]
        public Korisnik Korisnik { get; set; }

        [ForeignKey("TokIgreId")]
        //[JsonIgnore]
        public TokIgre TokIgre { get; set; }
       

        public Potez()
        {

        }

        public Potez(Korisnik korisnik, TokIgre tokIgre)
        {
            Korisnik = korisnik;
            TokIgre = tokIgre;
        }

        public void  NapraviOdDTO(PotezDTO potezDTO)
        {
            Id = potezDTO.Id;
            VremePoteza = potezDTO.VremePoteza;
            Crtanje = potezDTO.Crtanje;
            Poruka = potezDTO.Poruka;
            TekstPoruke = potezDTO.TekstPoruke;
            BojaLinije = potezDTO.BojaLinije;
            ParametarLinije = potezDTO.ParametarLinije;

        }
    }
}

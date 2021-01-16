using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;

namespace DataLayer.Models
{
    public class KorisnikPoSobi : IEntityWithId
    {
        [Key]
        public int Id { get; set; }
        [Required]

        [ForeignKey("SobaId")]
        public Soba Soba { get; set; }
        [ForeignKey("KorisnikId")]

        public Korisnik Korisnik { get; set; }
        [Required]
        public int Poeni { get; set; }
        public KorisnikPoSobi()
        {
            Poeni = 0;
        }
        public KorisnikPoSobi(Korisnik korisnik,Soba soba, int poeni=0)
        {
            Korisnik = korisnik;
            Soba = Soba;
            Poeni = poeni;
        }
    }
}

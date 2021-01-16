using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;


namespace DataLayer.Models
{
    public class Korisnik : IEntityWithId
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Username je obavezan")]
        [MinLength(5, ErrorMessage = "Minimalna duzina username-a je 5"), MaxLength(20, ErrorMessage = "Maksimalna duzina username-a je 20")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Username je obavezan")]
        [MinLength(5, ErrorMessage = "Minimalna duzina password-a je 5"), MaxLength(20, ErrorMessage = "Maksimalna duzina password-a je 20")]
        public string Password { get; set; }

        [JsonIgnore]
        public IList<Potez> Potezi { get; set; }

        [JsonIgnore]
        public IList<TokIgrePoKorisniku> TokIgrePoKorisniku { get; set; }
        [JsonIgnore]
        public IList<KorisnikPoSobi> KorisniciPoSobama { get; set; }

        public Korisnik()
        {
            Potezi = new List<Potez>();
            TokIgrePoKorisniku = new List<TokIgrePoKorisniku>();
        }
    }
}

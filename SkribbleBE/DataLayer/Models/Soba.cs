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

    public class Soba : IEntityWithId
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Naziv sobe je obavezan")]
        [MinLength(5, ErrorMessage = "Minimalna duzina naziva sobe je 5"), MaxLength(20, ErrorMessage = "Maksimalna duzina naziva sobe je 20")]
        public string Username { get; set; }
        [ForeignKey("KategorijaId")]
        public Kategorija Kategorija { get; set; }
        [JsonIgnore]
        public IList<TokIgre> TokoviIgre { get; set; }
        [JsonIgnore]
        public IList<KorisnikPoSobi> KorisniciPoSobama { get; set; }
        public Soba()
        {
            TokoviIgre = new List<TokIgre>();
        }
        public Soba(Kategorija kategorija)
        {
            TokoviIgre = new List<TokIgre>();
            Kategorija = kategorija;
        }

    }
}

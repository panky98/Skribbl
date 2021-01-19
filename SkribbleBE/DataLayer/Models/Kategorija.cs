using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace DataLayer.Models
{
    public class Kategorija : IEntityWithId
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Minimalna duzina naziva je 5"), MaxLength(20, ErrorMessage = "Maksimalna duzina naziva je 20")]
        public string Naziv { get; set; }
        [JsonIgnore]
        public IList<RecPoKategoriji> ReciPoKategorijama { get; set; }
        [JsonIgnore]
        public IList<Soba> SobePoKategoriji { get; set; }
        public Kategorija()
        {
            this.ReciPoKategorijama = new List<RecPoKategoriji>();
            this.SobePoKategoriji = new List<Soba>();
        }
        public Kategorija(string naziv)
        {
            this.Naziv = naziv;
            this.ReciPoKategorijama = new List<RecPoKategoriji>();
            this.SobePoKategoriji = new List<Soba>();
        }
    }
}

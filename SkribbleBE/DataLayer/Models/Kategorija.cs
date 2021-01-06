using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace DataLayer.Models
{
    public class Kategorija
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Minimalna duzina naziva je 5"), MaxLength(20, ErrorMessage = "Maksimalna duzina naziva je 20")]
        public string Naziv { get; set; }
        [JsonIgnore]
        public IList<RecPoKategoriji> ReciPoKategorijama { get; set; }
        public Kategorija()
        {
            this.ReciPoKategorijama = new List<RecPoKategoriji>();
        }
        public Kategorija(string naziv)
        {
            this.Naziv = naziv;
            this.ReciPoKategorijama = new List<RecPoKategoriji>();
        }
    }
}

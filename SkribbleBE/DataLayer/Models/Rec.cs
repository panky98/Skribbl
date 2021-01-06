using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace DataLayer.Models
{
    public class Rec
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Minimalna duzina naziva je 5"), MaxLength(20, ErrorMessage = "Maksimalna duzina naziva je 20")]
        public string Naziv { get; set; }
        [JsonIgnore]
        public IList<RecPoKategoriji> RecPoKategoriji { get; set; }

        public Rec()
        {
            this.RecPoKategoriji = new List<RecPoKategoriji>();
        }
        public Rec(string naziv)
        {
            this.Naziv = naziv;
            this.RecPoKategoriji = new List<RecPoKategoriji>();
        }
    }
}

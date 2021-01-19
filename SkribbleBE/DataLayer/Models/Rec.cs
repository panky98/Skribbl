using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DataLayer.Models
{
    public class Rec :IEntityWithId
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(5, ErrorMessage = "Minimalna duzina naziva je 5"), MaxLength(20, ErrorMessage = "Maksimalna duzina naziva je 20")]
        public string Naziv { get; set; }
        [JsonIgnore]
        public IList<RecPoKategoriji> RecPoKategoriji { get; set; }

        [JsonIgnore]
        public IList<TokIgre> TokoviIgre { get; set; }

        public Rec()
        {
            this.RecPoKategoriji = new List<RecPoKategoriji>();
            this.TokoviIgre = new List<TokIgre>();
        }
        public Rec(string naziv)
        {
            this.Naziv = naziv;
            this.RecPoKategoriji = new List<RecPoKategoriji>();
            this.TokoviIgre = new List<TokIgre>();
        }
    }
}

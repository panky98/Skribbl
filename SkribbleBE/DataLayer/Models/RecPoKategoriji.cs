using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Models
{
    public class RecPoKategoriji :IEntityWithId
    {
        [Key]
        public int Id { get; set; }
        public int RecId { get; set; }
        public int KategorijaId { get; set; }
    
        [Required]

        [ForeignKey("RecId")]
        public Rec Rec { get; set; }
        [ForeignKey("KategorijaId")]

        public Kategorija Kategorija { get; set; }

        public RecPoKategoriji()
        {

        }
        public RecPoKategoriji(Rec r, Kategorija k)
        {
            this.Rec = r;
            this.Kategorija = k;
        }
    }
}

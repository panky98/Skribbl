using DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DataLayer.Models
{
    public class TokIgre : IEntityWithId
    {
        [Key]
        public int Id { get; set; }

        
        public DateTime PocetakIgre { get; set; }

        [ForeignKey("SobaId")]
       public Soba Soba { get; set; }

        [ForeignKey("RecZaPogadjanjeId")]
        //[JsonIgnore]
        public Rec RecZaPogadjanje { get; set; }

        public string Naziv { get; set; }

        [JsonIgnore]
        public IList<Potez> Potezi { get; set; }


        [JsonIgnore]
        public IList<TokIgrePoKorisniku> TokIgrePoKorisniku { get; set; }

        public TokIgre()
        {
            Potezi = new List<Potez>();
            TokIgrePoKorisniku = new List<TokIgrePoKorisniku>();
        }

        public void NapraviOdDTO(TokIgreDTO tokIgreDTO)
        {
            Id = tokIgreDTO.Id;
            PocetakIgre = tokIgreDTO.PocetakIgre;
            Naziv = tokIgreDTO.Naziv;

            //nema potrebe, pre se poziva ctor
            /*
            Potezi = new List<Potez>();
            TokIgrePoKorisniku = new List<TokIgrePoKorisniku>();*/
        }
    }
}

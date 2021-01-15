using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTOs
{
    public class TokIgreDTO
    {
        public int Id { get; set; }

        public DateTime PocetakIgre { get; set; }

      
        // public int SobaId { get; set; }

        public int RecZaPogadjanjeId { get; set; }

        public TokIgreDTO()
        {

        }

        public TokIgreDTO(TokIgre tokIgre)
        {
            Id = tokIgre.Id;
            PocetakIgre = tokIgre.PocetakIgre;
            RecZaPogadjanjeId = tokIgre.RecZaPogadjanje.Id;
        }
    }
}

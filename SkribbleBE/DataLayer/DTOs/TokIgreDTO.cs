﻿using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTOs
{
    public class TokIgreDTO
    {
        public int Id { get; set; }

        public DateTime PocetakIgre { get; set; }

      
        public int SobaId { get; set; }

        public int RecZaPogadjanjeId { get; set; }

        public string Naziv { get; set; }

        public TokIgreDTO()
        {

        }

        public TokIgreDTO(TokIgre tokIgre)
        {
            if(tokIgre.Soba!=null)
                SobaId = tokIgre.Soba.Id;
            Id = tokIgre.Id;
            PocetakIgre = tokIgre.PocetakIgre;
            Naziv = tokIgre.Naziv;
            if(tokIgre.RecZaPogadjanje!=null)
                RecZaPogadjanjeId = tokIgre.RecZaPogadjanje.Id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTOs
{
    public class RecDTO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public IList<int> KategorijaId { get; set; }

        public RecDTO()
        {

        }
        public RecDTO(int Id,string naziv)
        {
            this.Id = Id;
            this.Naziv = naziv;
        }
    }
}

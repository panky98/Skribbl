using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DTOs
{
    public class KategorijaDTO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        [JsonIgnore]
        public IList<RecPoKategorijiDTO> ReciPoKategorijama { get; set; }

        public KategorijaDTO()
        {
            this.ReciPoKategorijama = new List<RecPoKategorijiDTO>();
        }
        public KategorijaDTO(int Id,string naziv)
        {
            this.Naziv = naziv;
            this.Id = Id;
            this.ReciPoKategorijama = new List<RecPoKategorijiDTO>();
        }
        public KategorijaDTO(string naziv, IList<RecPoKategorijiDTO> reciPoKategoriji)
        {
            this.Naziv = naziv;
            this.ReciPoKategorijama = reciPoKategoriji;
        }
    }
}

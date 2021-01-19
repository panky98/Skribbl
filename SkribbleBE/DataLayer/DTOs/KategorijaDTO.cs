using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace  DataLayer.DTOs
{
    public class KategorijaDTO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        [JsonIgnore]
        public IList<RecPoKategorijiDTO> ReciPoKategorijama { get; set; }
        [JsonIgnore]
        public IList<SobaDTO> SobePoKategoriji { get; set; }

        public KategorijaDTO()
        {
            this.ReciPoKategorijama = new List<RecPoKategorijiDTO>();
            this.SobePoKategoriji = new List<SobaDTO>();
        }
        public KategorijaDTO(int Id,string naziv)
        {
            this.Naziv = naziv;
            this.Id = Id;
            this.ReciPoKategorijama = new List<RecPoKategorijiDTO>();
            this.SobePoKategoriji = new List<SobaDTO>();
        }
        public KategorijaDTO(string naziv, IList<RecPoKategorijiDTO> reciPoKategoriji, IList<SobaDTO> sobePoKategoriji)
        {
            this.Naziv = naziv;
            this.ReciPoKategorijama = reciPoKategoriji;
            this.SobePoKategoriji = sobePoKategoriji;
        }
    }
}

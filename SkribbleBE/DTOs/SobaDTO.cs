using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DTOs
{
    public class SobaDTO
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public KategorijaDTO Kategorija { get; set; }
        [JsonIgnore]
        public IList<TokIgreDTO> TokoviIgre { get; set; }
        [JsonIgnore]
        public IList<KorisnikPoSobiDTO> KorisniciPoSobama { get; set; }
        public SobaDTO()
        {
            TokoviIgre = new List<TokIgreDTO>();
            KorisniciPoSobama = new List<KorisnikPoSobiDTO>();
        }
        public SobaDTO(string naziv)
        {
            Naziv = naziv;
            TokoviIgre = new List<TokIgreDTO>();
            KorisniciPoSobama = new List<KorisnikPoSobiDTO>();
        }
        public SobaDTO(string naziv,KategorijaDTO kategorija, IList<TokIgreDTO> tokoviIgre,IList<KorisnikPoSobiDTO> korisniciPoSobama)
        {
            Naziv = naziv;
            Kategorija = kategorija;
            TokoviIgre = tokoviIgre;
            KorisniciPoSobama = korisniciPoSobama;
        }
    }
}

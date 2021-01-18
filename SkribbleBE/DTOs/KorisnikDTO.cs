using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DTOs
{
    public class KorisnikDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        [JsonIgnore]
        public IList<PotezDTO> Potezi { get; set; }
        [JsonIgnore]
        public IList<TokIgrePoKorisnikuDTO> TokIgrePoKorisniku { get; set; }
        [JsonIgnore]
        public IList<KorisnikPoSobiDTO> KorisniciPoSobama { get; set; }
        public KorisnikDTO()
        {
            this.TokIgrePoKorisniku = new List<TokIgrePoKorisnikuDTO>();
            this.KorisniciPoSobama = new List<KorisnikPoSobiDTO>();
            this.Potezi = new List<PotezDTO>();
        }
        public KorisnikDTO(int Id, string username,string password)
        {
            this.Id = Id;
            this.Username = username;
            this.Password = password;
            this.TokIgrePoKorisniku = new List<TokIgrePoKorisnikuDTO>();
            this.KorisniciPoSobama = new List<KorisnikPoSobiDTO>();
            this.Potezi = new List<PotezDTO>();
        }
        public KorisnikDTO(string username,string password, IList<TokIgrePoKorisnikuDTO> tokIgrePoKorisniku, IList<KorisnikPoSobiDTO> korisniciPoSobama, IList<PotezDTO> potezi)
        {
            this.Username = username;
            this.Password = password;
            this.TokIgrePoKorisniku = tokIgrePoKorisniku;
            this.KorisniciPoSobama = korisniciPoSobama;
            this.Potezi = potezi;
        }
    }
}

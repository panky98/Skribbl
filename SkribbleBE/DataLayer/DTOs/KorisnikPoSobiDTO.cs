using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.DTOs
{
    public class KorisnikPoSobiDTO
    {
        public int Id { get; set; }
        public int Poeni { get; set; }

        public int KorisnikId { get; set; }
        public int SobaId { get; set; }
        public KorisnikDTO Korisnik { get; set; }
        public SobaDTO Soba { get; set; }
        public KorisnikPoSobiDTO()
        {
            Poeni = 0;         
        }
        public KorisnikPoSobiDTO(int poeni=0)
        {
            Poeni = poeni;
        }
        public KorisnikPoSobiDTO(int poeni,KorisnikDTO korisnik, SobaDTO soba)
        {
            Poeni = poeni;
            Korisnik = korisnik;
            Soba = soba;
        }
    }
}

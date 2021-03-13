using DataLayer.Models;
using DataLayer.Repository;
using DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Linq;

namespace DataLayer.Services
{
    public class KorisniciPoSobiService
    {
        private UnitOfWork unitOfWork;
        private KorisnikService KorisnikService;

        public KorisniciPoSobiService(ProjekatContext projekatContext)
        {
            this.unitOfWork = new UnitOfWork(projekatContext);
            KorisnikService = new KorisnikService(projekatContext);
        }
        public void AddNewKorisniciPoSobi(KorisnikPoSobiDTO k)
        {
            KorisnikPoSobi korisnikPoSobi = new KorisnikPoSobi()
            {
                Id = k.Id,
                Poeni = k.Poeni,
                Korisnik = this.unitOfWork.KorisnikRepository.GetOne(k.Korisnik.Id),
                Soba = this.unitOfWork.SobaRepository.GetOne(k.Soba.Id)
            };
            this.unitOfWork.KorisniciPoSobiRepository.Add(korisnikPoSobi);
            this.unitOfWork.Commit();
        }

        public IList<KorisnikPoSobiDTO> GetAll()
        {
            IList<KorisnikPoSobiDTO> returnList = new List<KorisnikPoSobiDTO>();
            foreach (KorisnikPoSobi korisnikPoSobi in (List<KorisnikPoSobi>)this.unitOfWork.KorisniciPoSobiRepository.GetAll())
            {
                KorisnikPoSobiDTO kps = new KorisnikPoSobiDTO()
                {
                    Id = korisnikPoSobi.Id,
                    Poeni=korisnikPoSobi.Poeni,
                    Korisnik = new KorisnikDTO()
                    {
                        Id = korisnikPoSobi.Korisnik.Id,
                        Username=korisnikPoSobi.Korisnik.Username,
                        Password=korisnikPoSobi.Korisnik.Password
                    },
                    Soba = new SobaDTO()
                    {
                        Id = korisnikPoSobi.Soba.Id,
                        Naziv=korisnikPoSobi.Soba.Naziv
                    }
                };
                returnList.Add(kps);
            }
            return returnList;
        }

        public void UpdateKorisniciPoSobi(KorisnikPoSobiDTO k)
        {
            KorisnikPoSobi korisnikPoSobi = new KorisnikPoSobi()
            {
                Id = k.Id,
                Poeni = k.Poeni,
                Korisnik = new Korisnik()
                {
                    Id = k.Korisnik.Id,
                    Username = k.Korisnik.Username,
                    Password = k.Korisnik.Password
                },
                Soba = new Soba()
                {
                    Id = k.Soba.Id,
                    Naziv = k.Soba.Naziv
                }
            };
            this.unitOfWork.KorisniciPoSobiRepository.Update(korisnikPoSobi);
            this.unitOfWork.Commit();
        }
        public void DeleteKorisniciPoSobi(KorisnikPoSobiDTO k)
        {
            KorisnikPoSobi korisnikPoSobi = new KorisnikPoSobi()
            {
                Id = k.Id,
                Poeni = k.Poeni,
                Korisnik = new Korisnik()
                {
                    Id = k.Korisnik.Id,
                    Username = k.Korisnik.Username,
                    Password = k.Korisnik.Password
                },
                Soba = new Soba()
                {
                    Id = k.Soba.Id,
                    Naziv = k.Soba.Naziv
                }
            };
            this.unitOfWork.KorisniciPoSobiRepository.Delete(korisnikPoSobi);
            this.unitOfWork.Commit();
        }
        public KorisnikPoSobiDTO getOneKorisnikPoSobi(int id)
        {
            KorisnikPoSobi k= this.unitOfWork.KorisniciPoSobiRepository.GetOne(id);
            if(k!=null)
            {
                KorisnikPoSobiDTO korisnikPoSobi = new KorisnikPoSobiDTO()
                {
                    Id = k.Id,
                    Poeni = k.Poeni,
                    Korisnik = new KorisnikDTO()
                    {
                        Id = k.Korisnik.Id,
                        Username = k.Korisnik.Username,
                        Password = k.Korisnik.Password
                    },
                    Soba = new SobaDTO()
                    {
                        Id = k.Soba.Id,
                        Naziv = k.Soba.Naziv
                    }
                };
                return korisnikPoSobi;
            }
            return null;
        }
        public IList<KorisnikPoSobiDTO> GetAllWithIncludes(params Expression<Func<KorisnikPoSobi, object>>[] includes)
        {
            IList<KorisnikPoSobi> korisniciPoSobi = (IList<KorisnikPoSobi>)this.unitOfWork.KorisniciPoSobiRepository.GetIncludes(includes);

            IList<KorisnikPoSobiDTO> korisnikPoSobiDTO = new List<KorisnikPoSobiDTO>();
            foreach (var k in korisniciPoSobi)
            {
                korisnikPoSobiDTO.Add(new KorisnikPoSobiDTO()
                {
                    Id = k.Id,
                    Poeni = k.Poeni,
                    Korisnik = new KorisnikDTO()
                    {
                        Id = k.Korisnik.Id,
                        Username = k.Korisnik.Username,
                        Password = k.Korisnik.Password
                    },
                    Soba = new SobaDTO()
                    {
                        Id = k.Soba.Id,
                        Naziv = k.Soba.Naziv
                    }
                });
            }
            return korisnikPoSobiDTO;
        }

        public KorisnikPoSobiDTO GetOneWithIncludes(int id, params Expression<Func<KorisnikPoSobi, object>>[] includes)
        {

            KorisnikPoSobi k = (KorisnikPoSobi)this.unitOfWork.KorisniciPoSobiRepository.GetOneWithIncludes(id, includes);
            KorisnikPoSobiDTO korisnikPoSobi = new KorisnikPoSobiDTO()
            {
                Id = k.Id,
                Poeni = k.Poeni,
                Korisnik = new KorisnikDTO()
                {
                    Id = k.Korisnik.Id,
                    Username = k.Korisnik.Username,
                    Password = k.Korisnik.Password
                },
                Soba = new SobaDTO()
                {
                    Id = k.Soba.Id,
                    Naziv = k.Soba.Naziv
                }
            };
            return korisnikPoSobi;
        }
        public void CreateByIds(int idKorisnik, int idSoba)
        {
            this.unitOfWork.KorisniciPoSobiRepository.AddByIds(idKorisnik, idSoba);
            this.unitOfWork.Commit();
        }
        public IList<KorisnikDTO> GetAllUsersByRoomId(int idSoba)
        {
            IList<KorisnikDTO> returnList = new List<KorisnikDTO>();
            foreach (Korisnik k in this.unitOfWork.KorisniciPoSobiRepository.GetAllUsersByRoomId(idSoba))
            {
                returnList.Add(new KorisnikDTO(k.Id,k.Username,k.Password));
            }
            return returnList;
        }
        public IList<KorisnikDTO> GetTopTenUsers()
        {
            IList<KorisnikDTO> returnList = new List<KorisnikDTO>();
            IDictionary<string, int> leaderboard = new Dictionary<string, int>();

            foreach (KorisnikPoSobi k in this.unitOfWork.KorisniciPoSobiRepository.GetAllRooms())
            {
                if (!leaderboard.ContainsKey(k.Korisnik.Username))
                {
                    leaderboard.Add(k.Korisnik.Username, 0);
                }
                leaderboard[k.Korisnik.Username] +=k.Poeni;
                
            }
            foreach(KeyValuePair<string,int> User in leaderboard)
            {
                int userId=KorisnikService.findIdByUsername(User.Key);
                int numberOfGames = this.unitOfWork.KorisniciPoSobiRepository.GetNumberOfGames(userId);
                returnList.Add(new KorisnikDTO(User.Value, User.Key, numberOfGames.ToString()));
                
            }
            returnList= returnList.OrderByDescending(x => x.Id).Take(10).ToList();
            return returnList;
        }

    }
}

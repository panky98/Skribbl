using DataLayer.DTOs;
using DataLayer.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DataLayer.SignalR
{
    public class CentralCoordinator : BackgroundService
    {
        private System.Timers.Timer timer;
        private readonly IHubContext<PotezHub> hub;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        readonly RedisClient redisClinet = new RedisClient(Config.SingleHost);
        private string groupName;

        public String GroupName { 
            get
            {
                return this.groupName;
            }
            set
            {
                this.groupName = value;
            }
        }

        public CentralCoordinator(IHubContext<PotezHub> hub,IServiceScopeFactory serviceScopeFactory)
        {
            this.hub = hub;
            this.timer = new System.Timers.Timer(1000);
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void StartTimer()
        {
            timer.Elapsed+= async (sender, ev) =>
            {
                redisClinet.DecrementValue("groupTimer:" + groupName);
                var counter = redisClinet.Get<string>("groupGuessed:" + groupName);

                if (redisClinet.Get<string>("groupTimer:" + groupName) == "0")
                {
                    //isteklo vreme
                    ((System.Timers.Timer)sender).Stop();

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        PotezService potezService = scope.ServiceProvider.GetRequiredService<PotezService>();
                        KorisniciPoSobiService korisniciPoSobiService = scope.ServiceProvider.GetRequiredService<KorisniciPoSobiService>();
                        KorisnikService korisnikService = scope.ServiceProvider.GetRequiredService<KorisnikService>();

                        //persistance of moves
                        IList<string> poruke = redisClinet.GetAllItemsFromList("groupMessages:" + groupName);
                        IList<string> potezi = redisClinet.GetAllItemsFromList("groupMoves:" + groupName);

                        if (poruke != null)
                        {
                            foreach (string poruka in poruke)
                            {
                                string[] splitStrings = poruka.Split("-");
                                string tekst = "";
                                for (int i = 2; i < splitStrings.Length; i++)
                                {
                                    if (i == 2)
                                        tekst += splitStrings[i];
                                    else
                                        tekst += "-" + splitStrings[i];
                                }

                                PotezDTO potezPersistance = new PotezDTO()
                                {
                                    KorisnikId = Convert.ToInt32(splitStrings[1]),
                                    TokIgreId = Convert.ToInt32(redisClinet.Get<string>("groupTokIgre:" + groupName)),
                                    Poruka = true,
                                    TekstPoruke = tekst,
                                    VremePoteza = Convert.ToInt64(splitStrings[0]),
                                    Crtanje = false
                                };
                                potezService.AddNewPotez(potezPersistance);
                            }
                            redisClinet.Remove("groupMessages:" + groupName);
                        }

                        if (potezi != null)
                        {
                            foreach (string potez in potezi)
                            {
                                string[] splitStrings = potez.Split("-");
                                PotezDTO potezPersistance = new PotezDTO()
                                {
                                    KorisnikId = Convert.ToInt32(splitStrings[1]),
                                    TokIgreId = Convert.ToInt32(redisClinet.Get<string>("groupTokIgre:" + groupName)),
                                    Poruka = false,
                                    Crtanje = true,
                                    VremePoteza = Convert.ToInt64(splitStrings[0]),
                                    ParametarLinije = splitStrings[2]
                                };
                                potezService.AddNewPotez(potezPersistance);
                            }
                            redisClinet.Remove("groupMoves:" + groupName);
                        }


                        await this.hub.Clients.Group(groupName).SendAsync("SaveReplay", redisClinet.Get<string>("groupTokIgre:" + groupName));
                        redisClinet.Remove("groupTokIgre:" + groupName);

                        redisClinet.DequeueItemFromList("groupLeft:" + groupName);
                        string newHostConnectionId = redisClinet.GetItemFromList("groupLeft:" + groupName, (int)(redisClinet.GetListCount("groupLeft:" + groupName) - 1));
                        if (newHostConnectionId != null)
                        {
                            await hub.Clients.Client(newHostConnectionId).SendAsync("ReceiveMessage", "HostMessage");
                            await hub.Clients.Client(newHostConnectionId).SendAsync("YourTurn");
                            await hub.Clients.GroupExcept(groupName, newHostConnectionId).SendAsync("SwitchedTurn", redisClinet.GetValueFromHash("groupHashConidUsername:" + groupName, newHostConnectionId) + "'s turn");

                            //cuvanje novog prezentera u redisu
                            redisClinet.Set<string>("groupPresenter:" + groupName, redisClinet.GetValueFromHash("groupHashConidUsername:" + groupName, redisClinet.GetValueFromHash("groupHashConidUsername:" + groupName, newHostConnectionId)));
                        }
                        else
                        {
                            //persistance of points
                            IDictionary<string, string> parovi = redisClinet.GetAllEntriesFromHash("groupHashPoints:" + groupName);
                            foreach (string kljuc in parovi.Keys)
                            {
                                int id = korisnikService.findIdByUsername(kljuc);
                                KorisnikPoSobiDTO obj = new KorisnikPoSobiDTO()
                                {
                                    Poeni = Convert.ToInt32(parovi[kljuc]),
                                    Korisnik = new KorisnikDTO()
                                    {
                                        Id = id
                                    },
                                    Soba = new SobaDTO()
                                    {
                                        Id = Convert.ToInt32(groupName.Substring(4))
                                    }
                                };
                                korisniciPoSobiService.AddNewKorisniciPoSobi(obj);
                            }

                            await hub.Clients.Group(groupName).SendAsync("FinishedGame");
                        }
                    }
                }
                else if ((counter != null && Convert.ToInt32(counter) == Convert.ToInt32(redisClinet.Get<string>("groupCounter:" + groupName)) - 1))
                {
                    //svi pogodili
                    ((System.Timers.Timer)sender).Stop();
                    await this.StopAsync(CancellationToken.None);
                }
                else
                {
                    await hub.Clients.Group(groupName).SendAsync("Timer", redisClinet.Get<string>("groupTimer:" + groupName));
                }
            };
            timer.Start();
        }


        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);            
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.StartTimer();
            return Task.CompletedTask;
        }
    }
}

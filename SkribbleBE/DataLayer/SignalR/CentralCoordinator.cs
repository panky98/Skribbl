using DataLayer.DTOs;
using DataLayer.Services;
using Microsoft.AspNetCore.SignalR;
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
        private readonly KorisnikService _korisnikService;
        private readonly KorisniciPoSobiService _korisniciPoSobiService;
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

        public CentralCoordinator(IHubContext<PotezHub> hub, KorisnikService kS, KorisniciPoSobiService kPS)
        {
            this.hub = hub;
            this.timer = new System.Timers.Timer(1000);
            _korisnikService = kS;
            _korisniciPoSobiService = kPS;
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
                            int id = _korisnikService.findIdByUsername(kljuc);
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
                            _korisniciPoSobiService.AddNewKorisniciPoSobi(obj);
                        }
                        await hub.Clients.Group(groupName).SendAsync("FinishedGame");
                    }
                }
                else if((counter != null && Convert.ToInt32(counter) == Convert.ToInt32(redisClinet.Get<string>("groupCounter:" + groupName)) - 1))
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

        public override void Dispose()
        {
            
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.StartTimer();
            return Task.CompletedTask;
        }
    }
}

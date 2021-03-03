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

        public CentralCoordinator(IHubContext<PotezHub> hub)
        {
            this.hub = hub;
            this.timer = new System.Timers.Timer(1000);
        }

        public void StartTimer()
        {
            timer.Elapsed+= async (sender, ev) =>
            {
                redisClinet.DecrementValue("groupTimer:" + groupName);
                var counter = redisClinet.Get<string>("groupGuessed:" + groupName);

                if (redisClinet.Get<string>("groupTimer:" + groupName) == "0")
                {
                    //svi pogodili prelazak na sl hosta ili isteklo vreme
                    ((System.Timers.Timer)sender).Stop();
                    redisClinet.DequeueItemFromList("groupLeft:" + groupName);
                    string newHostConnectionId = redisClinet.GetItemFromList("groupLeft:" + groupName, 0);
                    if (newHostConnectionId != null)
                    {
                        await hub.Clients.Client(newHostConnectionId).SendAsync("ReceiveMessage", "HostMessage");
                    }
                    else
                    {
                        await hub.Clients.Group(groupName).SendAsync("FinishedGame");
                    }
                }
                else if((counter != null && Convert.ToInt32(counter) == Convert.ToInt32(redisClinet.Get<string>("groupCounter:" + groupName)) - 1))
                {
                    redisClinet.Remove("groupGuessed:" + groupName);
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

        //public void Dispose()
        //{
        //    //timer.Dispose();
        //}

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            this.StartTimer();
            return Task.CompletedTask;
        }
    }
}

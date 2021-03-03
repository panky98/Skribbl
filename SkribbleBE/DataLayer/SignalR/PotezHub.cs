using DataLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace DataLayer.SignalR
{
    [Authorize]
    public class PotezHub : Hub
    {
        private readonly ProjekatContext _context;
        private readonly PotezService _potezService;
        readonly RedisClient redis = new RedisClient(Config.SingleHost);

        public PotezHub(ProjekatContext pc,PotezService potezService)
        {
            _context = pc;
            _potezService=potezService;
        }
        public async Task SendMessage(string groupName,string message)
        {
            var identity = (ClaimsIdentity)Context.User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim ime = Enumerable.ElementAt<Claim>(claims, 0);

            var recZaPogadjanje = redis.Get<object>("groupWord:" + groupName);
            if(recZaPogadjanje!=null && (string)recZaPogadjanje==message)
            {
                await Clients.OthersInGroup(groupName).SendAsync("ReceiveMessage", ime.Value + "has gussed the word!");
                await Clients.Caller.SendAsync("GussedWord", "You have gussed the word!");

                var counter = redis.Get<string>("groupGuessed:" + groupName);
                if(counter==null)
                {
                    int c = 1;
                    counter = "1";
                    redis.Set<int>("groupGuessed:" + groupName,c);
                }

                if (Convert.ToInt32(counter) == (Convert.ToInt32(redis.Get<string>("groupCounter:" + groupName)) - 1))
                {
                    //svi pogodili prelazak na sl hosta
                    redis.DequeueItemFromList("groupLeft:" + groupName);
                    string newHostConnectionId = redis.GetItemFromList("groupLeft:" + groupName, 0);
                    if(newHostConnectionId!=null)
                    {
                        await Clients.Client(newHostConnectionId).SendAsync("ReceiveMessage", "HostMessage");
                    }
                    else
                    {
                        await Clients.Groups(groupName).SendAsync("FinishedGame");
                    }

                }
                else
                {
                    redis.IncrementValue("groupGuessed:" + groupName);
                }
            }
            else
            {
                await Clients.OthersInGroup(groupName).SendAsync("ReceiveMessage", ime.Value + ": " + message);
            }
        }
        public async Task AddToGroup(string groupName)
        {
            try
            {
                var identity = (ClaimsIdentity)Context.User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                Claim ime=Enumerable.ElementAt<Claim>(claims, 0);

                //prvi koji je pristupio grupi HOST!
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                
                var counter=redis.Get<object>("groupCounter:" + groupName);
                if(counter==null || (string)counter=="0")
                {
                    await Clients.Caller.SendAsync("ReceiveMessage", "HostMessage");
                    redis.Set<int>("groupCounter:" + groupName, 1);
                    redis.EnqueueItemOnList("groupMembers:" + groupName, Context.ConnectionId);
                    redis.EnqueueItemOnList("groupLeft:" + groupName, Context.ConnectionId);
                }
                else 
                {
                    redis.IncrementValue("groupCounter:" + groupName);
                    redis.EnqueueItemOnList("groupMembers:" + groupName, Context.ConnectionId);
                    redis.EnqueueItemOnList("groupLeft:" + groupName, Context.ConnectionId);
                }

                await Clients.OthersInGroup(groupName).SendAsync("ReceiveMessage", $"{ime.Value} has joined the group {groupName}.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task RemoveFromGroup(string groupName, bool isHost)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            //izlazak iz grupe
            redis.DecrementValue("groupCounter:" + groupName);
            redis.RemoveItemFromList("groupMembers:" + groupName, Context.ConnectionId);
            redis.RemoveItemFromList("groupLeft:" + groupName, Context.ConnectionId);

            string counter = redis.Get<string>("groupCounter:" + groupName);
            if (counter != "0" && isHost)
            {
                string value=redis.GetItemFromList("groupMembers:" + groupName, 0);
                await Clients.Client(value).SendAsync("ReceiveMessage", "HostMessage");
            }
            await Clients.OthersInGroup(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has left the group {groupName}.");
        }

        public async Task SaveNewTokIgreId(string groupName, int newTokIgreId, string rec)
        {
            redis.Set<int>("groupTokIgre:" + groupName, newTokIgreId);
            redis.Set<string>("groupWord:" + groupName, rec);
            redis.Set<int>("groupTimer:" + groupName, 30);
        }

        public static void DoWork(object data)
        {
            ((CentralCoordinator)data).StartTimer();
        }

    }
}

using DataLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SignalR
{
    [Authorize]
    public class PotezHub : Hub
    {
        private readonly ProjekatContext _context;
        public PotezHub(ProjekatContext pc)
        {
            _context = pc;
        }
        public async Task SendMessage(string groupName,string message)
        {
            var identity = (ClaimsIdentity)Context.User.Identity;
            IEnumerable<Claim> claims = identity.Claims;
            Claim ime = Enumerable.ElementAt<Claim>(claims, 0);

            await Clients.OthersInGroup(groupName).SendAsync("ReceiveMessage",ime.Value+": "+ message);
        }
        public async Task AddToGroup(string groupName)
        {
            try
            {
                var identity = (ClaimsIdentity)Context.User.Identity;
                IEnumerable<Claim> claims = identity.Claims;
                Claim ime=Enumerable.ElementAt<Claim>(claims, 0);

                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
                await Clients.OthersInGroup(groupName).SendAsync("ReceiveMessage", $"{ime.Value} has joined the group {groupName}.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.OthersInGroup(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}

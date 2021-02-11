using DataLayer.Services;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.SignalR
{
    public class PotezHub : Hub
    {
        private readonly ProjekatContext _context;
        public PotezHub(ProjekatContext pc)
        {
            _context = pc;
        }
        public async Task SendMessage(string groupName,string message)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }
        public async Task AddToGroup(string groupName)
        {
            try
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, groupName);

                await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has joined the group {groupName}.");
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

            await Clients.Group(groupName).SendAsync("ReceiveMessage", $"{Context.ConnectionId} has left the group {groupName}.");
        }
    }
}

using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using WarShipServer.Models;

namespace WarShipServer.Services
{
    public class GameHub : Hub
    {
        
        public async Task Send(GameData gameData)
        {
            await Clients.All.SendAsync("Send", gameData);
        }

        public async Task LogPlayer(Player player)
        {

            await Clients.Others.SendAsync("LogPlayer", player);
        }
    }
}
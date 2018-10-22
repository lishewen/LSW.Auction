using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;
using 拍卖系统.Data;
using 拍卖系统.Models;

namespace 拍卖系统.Hubs
{
    public class AuctionHub : Hub
    {
        ApplicationDbContext db;
        public AuctionHub(ApplicationDbContext context)
        {
            db = context;
        }

        public void RefreshAuctionRecords(string msg)
        {
            Clients.All.SendAsync("refreshauctionrecords", msg);
        }

        public Task JoinRoom(string roomName)
        {
            var client = UserHandler.UserClients.FirstOrDefault(u => u.ConnectedId == Context.ConnectionId);
            if (client != null)
                client.Room = roomName;

            return Groups.AddToGroupAsync(Context.ConnectionId, roomName);
        }

        public Task LeaveRoom(string roomName)
        {
            var client = UserHandler.UserClients.FirstOrDefault(u => u.ConnectedId == Context.ConnectionId);
            if (client != null)
                client.Room = string.Empty;

            return Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }
    }
}

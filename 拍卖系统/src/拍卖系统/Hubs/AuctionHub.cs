using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using 拍卖系统.Data;
using 拍卖系统.Models;

namespace 拍卖系统.Hubs
{
	[HubName("auction")]
	public class AuctionHub : Hub
	{
		ApplicationDbContext db;
		public AuctionHub(ApplicationDbContext context)
		{
			db = context;
		}

		public void RefreshAuctionRecords(string msg)
		{
			Clients.All.refreshauctionrecords(msg);
		}

		public Task JoinRoom(string roomName)
		{
			var client = UserHandler.UserClients.FirstOrDefault(u => u.ConnectedId == Context.ConnectionId);
			if (client != null)
				client.Room = roomName;

			return Groups.Add(Context.ConnectionId, roomName);
		}

		public Task LeaveRoom(string roomName)
		{
			var client = UserHandler.UserClients.FirstOrDefault(u => u.ConnectedId == Context.ConnectionId);
			if (client != null)
				client.Room = string.Empty;

			return Groups.Remove(Context.ConnectionId, roomName);
		}
		public override Task OnConnected()
		{
			int mid = 0;
			int.TryParse(Context.QueryString["mid"], out mid);
			UserHandler.UserClients.Add(new UserClient
			{
				ConnectedId = Context.ConnectionId,
				Mid = mid,
			});
			return base.OnConnected();
		}
		public override Task OnDisconnected(bool stopCalled)
		{
			UserHandler.UserClients.Remove(UserHandler.UserClients.FirstOrDefault(u => u.ConnectedId == Context.ConnectionId));
			return base.OnDisconnected(stopCalled);
		}
	}
}

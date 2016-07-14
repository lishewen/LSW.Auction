using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using 拍卖系统.Data;

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
			return Groups.Add(Context.ConnectionId, roomName);
		}
	}
}

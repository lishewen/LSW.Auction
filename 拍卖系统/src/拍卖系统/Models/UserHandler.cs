using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace 拍卖系统.Models
{
    public class UserHandler
    {
		public static HashSet<UserClient> UserClients = new HashSet<UserClient>();
	}

	public class UserClient
	{
		public string ConnectedId { get; set; }
		public int Mid { get; set; }
		public string Room { get; set; }
	}
}

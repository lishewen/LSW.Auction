using Senparc.Weixin.MP.MessageHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Weixin.MP.Entities;
using 拍卖系统.Data;
using System.IO;
using Senparc.Weixin.MP.Entities.Request;

namespace 拍卖系统.Controllers
{
	public class CustomMessageHandler : MessageHandler<CustomMessageContext>
	{
		ApplicationDbContext context;
		public CustomMessageHandler(ApplicationDbContext context, Stream inputStream, PostModel postModel, int maxRecordCount = 0)
			: base(inputStream, postModel, maxRecordCount)
		{
			this.context = context;
		}
		public override IResponseMessageBase DefaultResponseMessage(IRequestMessageBase requestMessage)
		{
			return null;
		}
	}
}

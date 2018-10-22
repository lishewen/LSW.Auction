using Senparc.NeuChar.Entities;
using Senparc.Weixin.MP.Entities.Request;
using Senparc.Weixin.MP.MessageHandlers;
using System.IO;
using 拍卖系统.Data;

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

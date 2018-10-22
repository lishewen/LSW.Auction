using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace 拍卖系统.Models.ManageViewModels
{
    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }

        public IList<Microsoft.AspNetCore.Authentication.AuthenticationScheme> OtherLogins { get; set; }
    }
}

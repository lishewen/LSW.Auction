using Microsoft.AspNetCore.Identity;

namespace 拍卖系统.Models
{
    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
    }
}

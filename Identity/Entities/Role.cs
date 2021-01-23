using Microsoft.AspNetCore.Identity;

namespace Identity.Entities
{
    public class Role : IdentityRole<int>
    {
        public Role(string name) : base(name)
        {
        }
    }
}
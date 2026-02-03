using Microsoft.AspNetCore.Identity;

namespace Readify.Models.Authentication
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole() {}
        public ApplicationRole(string roleName) : base(roleName) {}

    }
}

using Microsoft.AspNetCore.Identity;

namespace Eccomerce.Models
{
    public class AppUser: IdentityUser
    {
        public string? fullName { get; set; }

    }
}

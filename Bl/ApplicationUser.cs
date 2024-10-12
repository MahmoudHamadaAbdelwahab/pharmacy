using Microsoft.AspNetCore.Identity;

namespace Dominos.Bl
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

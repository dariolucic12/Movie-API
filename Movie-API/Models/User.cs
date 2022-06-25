using Microsoft.AspNetCore.Identity;

namespace Movie_API.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

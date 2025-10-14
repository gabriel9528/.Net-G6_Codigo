using Microsoft.AspNetCore.Identity;

namespace Microservices.Microservices.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nombres { get; set; }
    }
}

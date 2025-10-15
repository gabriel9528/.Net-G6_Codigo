using Microservices.Microservices.AuthAPI.Models;

namespace Microservices.Microservices.AuthAPI.Services.IServices
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
    }
}

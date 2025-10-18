using Microservices.Web.Services.IService;
using Microservices.Web.Utility;

namespace Microservices.Web.Services
{
    public class TokenProvider : ITokenProvider
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public TokenProvider(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
        }

        public string? GetToken()
        {
            var context = _contextAccessor.HttpContext;
            if (context != null)
            {
                var cookie = context.Request.Cookies[SD.TokenCookie];
                Console.WriteLine("Cookie: " + cookie);
                return cookie;
            }

            return null;

        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie, token);
        }
    }
}

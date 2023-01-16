using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;

namespace RemoteFinder.BLL.Services.AuthorizationService
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizationService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public int GetCurrentUserId()
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return 0;
            }

            var subValue = _httpContextAccessor.HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

            bool parseRes = int.TryParse(subValue, out var userId);

            if (!parseRes)
            {
                throw new ValidationException("User id is not an integer");
            }

            return userId;
        }

        public string GetUserName()
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return "system";
            }
            
            return _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;
        }
        
        public string[] GetRoles()
        {
            if (_httpContextAccessor.HttpContext == null)
            {
                return new string[] {};
            }
            
            return _httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == "role").Select(c => c.Value).ToArray();
        }
    }
}
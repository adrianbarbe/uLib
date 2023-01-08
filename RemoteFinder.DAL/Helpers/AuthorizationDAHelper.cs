using Microsoft.AspNetCore.Http;

namespace RemoteFinder.DAL.Helpers;

public class AuthorizationDAHelper : IAuthorizationDAHelper
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationDAHelper(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    
    public string GetUserName()
    {
        if (_httpContextAccessor.HttpContext == null)
        {
            return "system";
        }
            
        return _httpContextAccessor.HttpContext?.User.FindFirst("name")?.Value ?? "";
    }
}
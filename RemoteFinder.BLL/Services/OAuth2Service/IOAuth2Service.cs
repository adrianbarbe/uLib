using RemoteFinder.Models;

namespace RemoteFinder.BLL.Services.OAuth2Service;

public interface IOAuth2Service
{
    AuthTokenResponse? AuthorizeCodeSignUp(string code);
    AuthTokenResponse? AuthorizeCodeSignIn(string code);
}
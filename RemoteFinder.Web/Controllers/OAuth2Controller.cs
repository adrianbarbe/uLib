using Microsoft.AspNetCore.Mvc;
using RemoteFinder.BLL.Services.OAuth2Service;
using RemoteFinder.Models;

namespace RemoteFinder.Web.Controllers
{
    [Route("oauth2")]
    public class OAuth2Controller : Controller
    {
        private readonly IOAuth2Service _ioAuth2Service;

        public OAuth2Controller(IOAuth2Service ioAuth2Service)
        {
            _ioAuth2Service = ioAuth2Service;
        }

        [HttpPost]
        [Route("authorize-sign-up")]
        public AuthTokenResponse? AuthorizeCodeSignUp([FromBody] AuthCode authCode)
        {
            return _ioAuth2Service.AuthorizeCodeSignUp(authCode.Code);
        }
        
        [HttpPost]
        [Route("authorize-sign-in")]
        public AuthTokenResponse? AuthorizeCodeSignIn([FromBody] AuthCode authCode)
        {
            return _ioAuth2Service.AuthorizeCodeSignIn(authCode.Code);
        }
    }
}
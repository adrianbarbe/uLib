using System.IdentityModel.Tokens.Jwt;
using RemoteFinder.BLL.Exceptions;
using RemoteFinder.DAL;
using RemoteFinder.Entities.Authentication;
using RemoteFinder.Entities.Constants;
using RemoteFinder.Models;
using RestSharp;

namespace RemoteFinder.BLL.Services.OAuth2Service;

public class OAuth2Service : IOAuth2Service
{
    private readonly MainContext _context;
    private readonly RestClient _googleRestClient;
    private readonly RestClient _googlePersonRestClient;

    public OAuth2Service(MainContext context)
    {
        _context = context;
        _googleRestClient = new RestClient("https://oauth2.googleapis.com/token");
        _googlePersonRestClient = new RestClient("https://people.googleapis.com/v1/people/me");
    }

    public AuthTokenResponse? AuthorizeCodeSignUp(string code)
    {
        var token = AuthorizeCode(code);

        var foundedUser = _context.UserSocial.FirstOrDefault(us => us.Email == "");
        
        if (foundedUser != null)
        {
            throw new UserAlreadyExistsException("User already exists. Please sign-in.");
        }
        
        var stream = token.IdToken;  
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(stream);
        var tokenS = jsonToken as JwtSecurityToken;

        if (tokenS == null)
        {
            throw new ValidationException("Cannot read token");
        }

        var sub = tokenS.Claims.FirstOrDefault(c => c.Type == "sub");
        var email = tokenS.Claims.FirstOrDefault(c => c.Type == "email");
        var firstName = tokenS.Claims.FirstOrDefault(c => c.Type == "given_name");
        var lastName = tokenS.Claims.FirstOrDefault(c => c.Type == "family_name");
        
        var profilePhotoRequest = new RestRequest("", Method.Get);
        profilePhotoRequest.AddHeader("Authorization", $"Bearer {token.AccessToken}");
        profilePhotoRequest.AddQueryParameter("personFields", "photos");
        var profileResponse = _googlePersonRestClient.Execute<ProfilePhotoResponse>(profilePhotoRequest);
        var profile = profileResponse.Data;

        var userEntity = new UserSocialEntity
        {
            Email = email?.Value ?? "",
            Username = email?.Value ?? "",
            FirstName = firstName?.Value ?? "",
            LastName = lastName?.Value ?? "",
            UserPicture = profile?.Photos.FirstOrDefault()?.Url ?? "",
            OAuthProvider = OAuthProviderTypes.Google,
            ProviderUserId = sub?.Value ?? "",
        };
        _context.UserSocial.Add(userEntity);
        _context.SaveChanges();

        return token;
    }

    public AuthTokenResponse AuthorizeCodeSignIn(string code)
    {
        var token = AuthorizeCode(code);

        var foundedUser = _context.UserSocial.FirstOrDefault(us => us.Email == "");

        if (foundedUser != null)
        {
            return token;
        }

        throw new UserNotFoundException("Cannot find user. Please sign-up.");
    }
    
    private AuthTokenResponse AuthorizeCode(string code)
    {
        var clientId = Environment.GetEnvironmentVariable("GoogleClientId") ?? "";
        var clientSecret = Environment.GetEnvironmentVariable("GoogleClientSecret") ?? "";
        var redirectUri = Environment.GetEnvironmentVariable("GoogleRedirectUri") ?? "";
        
        var codeExchangeRequest = new RestRequest("", Method.Post);

        codeExchangeRequest.AddQueryParameter("code", code);
        codeExchangeRequest.AddQueryParameter("client_id", clientId);
        codeExchangeRequest.AddQueryParameter("client_secret", clientSecret);
        codeExchangeRequest.AddQueryParameter("redirect_uri", redirectUri);
        codeExchangeRequest.AddQueryParameter("grant_type", "authorization_code");

        var tokenResponse = _googleRestClient.Execute<AuthTokenResponse>(codeExchangeRequest);
        var token = tokenResponse.Data;

        return token;
    }
}
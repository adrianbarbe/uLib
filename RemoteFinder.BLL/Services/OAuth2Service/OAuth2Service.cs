using System.IdentityModel.Tokens.Jwt;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Options;
using RemoteFinder.BLL.Exceptions;
using RemoteFinder.DAL;
using RemoteFinder.Entities.Authentication;
using RemoteFinder.Entities.Constants;
using RemoteFinder.Models;
using RestSharp;
using UMSA.StepTest.BLL.Configuration;

namespace RemoteFinder.BLL.Services.OAuth2Service;

public class OAuth2Service : IOAuth2Service
{
    private readonly MainContext _context;
    private readonly JwtSettings _jwtSettings;
    private readonly RestClient _googleRestClient;
    private readonly RestClient _googlePersonRestClient;

    public OAuth2Service(MainContext context, IOptions<JwtSettings> jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings.Value;
        _googleRestClient = new RestClient("https://oauth2.googleapis.com/token");
        _googlePersonRestClient = new RestClient("https://people.googleapis.com/v1/people/me");
    }

    public AuthTokenResponse? AuthorizeCode(string code)
    {
        var token = AuthorizeCodeRequest(code);
        
        if (token?.IdToken == null)
        {
            throw new ValidationException("Cannot validate token");
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

        var foundedUser = _context.UserSocial.FirstOrDefault(us => email != null && us.Email == email.Value);

        if (foundedUser != null)
        {
            // If we found user generate token immediately
            token.IdToken = GenerateJwtToken(foundedUser);
            
            return token;
        }

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
        
        // Generate token after creating user
        token.IdToken = GenerateJwtToken(userEntity);
        
        return token;
    }

    private AuthTokenResponse? AuthorizeCodeRequest(string code)
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
    
    private string GenerateJwtToken(UserSocialEntity userEntity)
    {
        string[] roles = new[] { "user" };

        var jwtSecretKey = Environment.GetEnvironmentVariable("JwtSecretKey") ?? _jwtSettings.SecretKey;

        var tokenBuilder = new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .WithSecret(jwtSecretKey)
            .AddClaim(JwtRegisteredClaimNames.Exp,
                DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes).ToUnixTimeSeconds())
            .AddClaim(JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer)
            .AddClaim(JwtRegisteredClaimNames.Aud, _jwtSettings.Audience)
            .AddClaim(JwtRegisteredClaimNames.Sub, userEntity.Id)
            .AddClaim(JwtRegisteredClaimNames.UniqueName, userEntity.Username)
            .AddClaim(JwtRegisteredClaimNames.Email, userEntity.Email)
            .AddClaim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.AddMilliseconds(1).ToUnixTimeSeconds());
        
        tokenBuilder.AddClaim("role", roles);
            
        return tokenBuilder.Encode();
    }
}
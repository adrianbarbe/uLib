using RemoteFinder.Entities.Authentication;
using RemoteFinder.Models;

namespace RemoteFinder.BLL.Mappers.Authentication;

public class UserSocialMapper : IMapper<UserSocialEntity, UserSocial>
{
    public UserSocialEntity Map(UserSocial source)
    {
        return new UserSocialEntity
        {
            Id = source.Id,
            Email = source.Email,
            Username = source.Username,
            FirstName = source.FirstName,
            LastName = source.LastName,
            ProviderUserId = source.ProviderUserId,
            UserPicture = source.UserPicture,
        };
    }

    public UserSocial Map(UserSocialEntity source)
    {
        return new UserSocial
        {
            Id = source.Id,
            Email = source.Email,
            Username = source.Username,
            FirstName = source.FirstName,
            LastName = source.LastName,
            ProviderUserId = source.ProviderUserId,
            UserPicture = source.UserPicture,
        };
    }
}
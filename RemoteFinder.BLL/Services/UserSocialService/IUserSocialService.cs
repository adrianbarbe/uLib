using RemoteFinder.Models;

namespace RemoteFinder.BLL.Services.UserSocialService;

public interface IUserSocialService
{
    List<UserSocial> GetAll();
    
    
    UserSocial Create(UserSocial userSocial);

}
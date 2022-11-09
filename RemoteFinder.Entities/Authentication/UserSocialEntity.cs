using RemoteFinder.Entities.Constants;
using RemoteFinder.Entities.Payments;
using RemoteFinder.Entities.Storage;

namespace RemoteFinder.Entities.Authentication;

public class UserSocialEntity
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string UserPicture { get; set; }
    public string ProviderUserId { get; set; }
    
    public OAuthProviderTypes OAuthProvider { get; set; }
    
    public virtual ICollection<FileEntity> Files { get; set; }
    public virtual ICollection<PaymentEntity> Payments { get; set; }
}
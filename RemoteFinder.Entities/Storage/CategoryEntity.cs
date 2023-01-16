using RemoteFinder.Entities.Authentication;

namespace RemoteFinder.Entities.Storage;

public class CategoryEntity : BaseSoftDelete
{
    public int Id { get; set; }

    public string Name { get; set; }

    public UserSocialEntity UserSocial { get; set; }
    public int UserSocialId { get; set; }
    
    public ICollection<BookEntity> Books { get; set; }
}
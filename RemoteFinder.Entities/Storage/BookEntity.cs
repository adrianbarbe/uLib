using RemoteFinder.Entities.Authentication;

namespace RemoteFinder.Entities.Storage;

public class BookEntity : BaseSoftDelete
{
    public int Id { get; set; }
    
    public string Name { get; set; }

    public string PreviewImageUrl { get; set; }
    
    public FileEntity File { get; set; }
    public int FileId { get; set; }

    public CategoryEntity Category { get; set; }
    public int CategoryId { get; set; }
    
    public UserSocialEntity UserSocial { get; set; }
    public int UserSocialId { get; set; }
}
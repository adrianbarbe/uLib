using RemoteFinder.Entities.Authentication;

namespace RemoteFinder.Entities.Storage;

public class FileEntity : BaseSoftDelete
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public decimal FileSize { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public UserSocialEntity UserSocial { get; set; }
    public int UserSocialId { get; set; }
}
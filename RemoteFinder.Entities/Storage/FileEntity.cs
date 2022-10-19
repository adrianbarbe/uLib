using RemoteFinder.Entities.Authentication;

namespace RemoteFinder.Entities.Storage;

public class FileEntity
{
    public FileEntity(string name, string fileName, string fileType, decimal fileSize)
    {
        Name = name;
        FileName = fileName;
        FileType = fileType;
        FileSize = fileSize;
        CreatedAt = DateTime.Now;
    }
    

    public int Id { get; set; }
    public string Name { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public decimal FileSize { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public UserSocialEntity UserSocial { get; set; }
    public int UserSocialId { get; set; }
}
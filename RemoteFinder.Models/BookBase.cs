namespace RemoteFinder.Models;

public class BookBase
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public string? FileName { get; set; }
    public string? PreviewImageUrl { get; set; }

    public Category Category { get; set; }
    
    public FileStorage File { get; set; }
}
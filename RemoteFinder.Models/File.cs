namespace RemoteFinder.Models;

public class File
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string FileName { get; set; }
    public string FileType { get; set; }
    public decimal FileSize { get; set; }
    public DateTime CreatedAt { get; set; }
}
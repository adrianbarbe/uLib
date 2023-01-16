namespace RemoteFinder.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public List<BookBase>? Books { get; set; }
}
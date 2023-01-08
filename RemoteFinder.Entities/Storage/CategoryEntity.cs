namespace RemoteFinder.Entities.Storage;

public class CategoryEntity : BaseSoftDelete
{
    public int Id { get; set; }

    public string Name { get; set; }
}
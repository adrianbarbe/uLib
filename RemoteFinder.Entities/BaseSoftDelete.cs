namespace RemoteFinder.Entities;

public class BaseSoftDelete
{
    public BaseSoftDelete()
    {
        IsDeleted = false;
    }
    public bool IsDeleted { get; set; }
    
    public DateTime? DeletedAt { get; set; }
    
    public string? DeletedBy { get; set; }
}
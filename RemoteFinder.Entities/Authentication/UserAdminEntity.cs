namespace RemoteFinder.Entities.Authentication;

public class UserAdminEntity
{
    public UserAdminEntity(string username, string password)
    {
        Username = username;
        Password = password;
        LoginsCount = LoginsCount++;
        LastLogin = DateTime.Now;
    }

    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int? LoginsCount { get; set; }
    public DateTime? LastLogin { get; set; }   
}
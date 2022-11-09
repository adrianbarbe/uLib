namespace RemoteFinder.Models;

public class UserSocial
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public string? ProviderUserId { get; set; }
    public string? UserPicture { get; set; }
}
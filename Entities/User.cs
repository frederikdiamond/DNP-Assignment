namespace Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
}


public class UserCollection
{
    private List<User> users;

    public UserCollection()
    {
        users = new List<User>();
    }
}



namespace Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}


public class UserCollection
{
    private List<User> users;

    public UserCollection()
    {
        users = new List<User>();
    }
}



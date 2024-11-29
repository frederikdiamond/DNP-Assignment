namespace Entities;

public class User
{
    private User() { }
    
    public User(string username, string password)
    {
        Username = username;
        Password = password;
        CreatedAt = DateTime.Now.ToString();
        Posts = new List<Post>();
        Comments = new List<Comment>();
    }
    
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string CreatedAt { get; set; } = string.Empty;
    
    public virtual ICollection<Post> Posts { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
}

/*public class UserCollection
{
    private List<User> users;

    public UserCollection()
    {
        users = new List<User>();
    }
}*/

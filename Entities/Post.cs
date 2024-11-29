namespace Entities;

public class Post
{
    private Post() { }

    public Post(string title, string body, User user)
    {
        Title = title;
        Body = body;
        User = user;
        CreatedAt = DateTime.Now.ToString();
        Comments = new List<Comment>();
        Categories = new List<Category>();
        Reactions = new List<Reaction>();
    }
    
    public int PostId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }
    public int UserId { get; set; }
    public string CreatedAt { get; set; }
    
    public virtual User User { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
    public virtual ICollection<Category> Categories { get; set; }
    public virtual ICollection<Reaction> Reactions { get; set; }
}

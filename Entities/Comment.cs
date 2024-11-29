namespace Entities;

public class Comment
{
    private Comment() { }

    public Comment(string body, User user, Post post)
    {
        Body = body;
        User = user;
        Post = post;
        Reactions = new List<Reaction>();
    }
    
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public string Body { get; set; }
    
    public virtual User User { get; set; }
    public virtual Post Post { get; set; }
    public virtual ICollection<Reaction> Reactions { get; set; }
}

/*public class CommentCollection
{
    private List<Comment> comments; 

    public CommentCollection()
    {
        comments = new List<Comment>();
    }
}*/

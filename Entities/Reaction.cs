namespace Entities;

public class Reaction
{
    private Reaction() { }

    public Reaction(User user, Post post = null, Comment comment = null)
    {
        User = user;
        Post = post;
        Comment = comment;
        Timestamp = (int) DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
    }
    
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public int CommentId { get; set; }
    public int Timestamp { get; set; }
    public int UpvoteCounter { get; set; }
    public int DownvoteCounter { get; set; }
    
    public virtual User User { get; set; }
    public virtual Post Post { get; set; }
    public virtual Comment Comment { get; set; }
}

/*public class ReactionCollection
{
    private List<Reaction> reactions; 

    public ReactionCollection()
    {
        reactions = new List<Reaction>();
    }
}*/

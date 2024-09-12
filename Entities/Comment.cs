namespace Entities;

public class Comment
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int PostId { get; set; }
    public string Body { get; set; }
}


public class CommentCollection
{
    private List<Comment> comments; 

    public CommentCollection()
    {
        comments = new List<Comment>();
    }
}
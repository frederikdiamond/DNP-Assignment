namespace Entities;

public class Reaction
{
    private int Id { get; set; }
    private int UserId { get; set; }
    private int PostId { get; set; }
    private int CommentId { get; set; }
    private int Timestamp { get; set; }
    private int UpvoteCounter { get; set; }
    private int DownvoteCounter { get; set; }
}
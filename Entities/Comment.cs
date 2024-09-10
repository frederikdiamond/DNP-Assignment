namespace Entities;

public class Comment
{
    private int Id { get; set; }
    private int UserId { get; set; }
    private int PostId { get; set; }
    private string Body { get; set; }
}
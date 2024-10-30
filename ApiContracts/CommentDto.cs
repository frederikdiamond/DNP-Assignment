namespace ApiContracts;

public class CommentDto
{
    public int Id { get; set; }
    public string Body { get; set; }
    public string AuthorUsername { get; set; }
}
namespace ApiContracts.DTOs
{
    public class CreateReactionDto
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int CommentId { get; set; }
        public bool IsUpvote { get; set; }
    }

    public class ReactionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public int CommentId { get; set; }
        public int Timestamp { get; set; }
        public int UpvoteCounter { get; set; }
        public int DownvoteCounter { get; set; }
    }
}

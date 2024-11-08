namespace ApiContracts.DTOs
{
    public class CreatePostDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
    }

    public class UpdatePostDto
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }

    public class PostDto
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int UserId { get; set; }
        public string CreatedAt { get; set; }
        public int UpvoteCount { get; set; }
        public int DownvoteCount { get; set; }

    }
}

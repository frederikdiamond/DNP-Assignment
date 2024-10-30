namespace Shared.ApiContracts.DTOs
{
    public class CreateUserDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PostsCount { get; set; }
        public int CommentsCount { get; set; }
    }
}

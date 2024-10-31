using ApiContracts;

namespace BlazorApp.Services;

public class HttpPostService
{
    public class PostService : IPostService
    {
        private readonly HttpClient httpClient;

        public PostService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<PostDto> CreateAsync(CreatePostDto dto)
        {
            var response = await httpClient.PostAsJsonAsync("api/posts", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PostDto>()!;
        }

        public async Task<IEnumerable<PostDto>> GetAsync(string? title)
        {
            var url = "api/posts";
            if (!string.IsNullOrEmpty(title))
            {
                url += $"?title={title}";
            }
            var posts = await httpClient.GetFromJsonAsync<IEnumerable<PostDto>>(url);
            return posts ?? Enumerable.Empty<PostDto>();
        }

        public async Task<PostDto> GetByIdAsync(int id)
        {
            var post = await httpClient.GetFromJsonAsync<PostDto>($"api/posts/{id}");
            if (post == null)
            {
                throw new Exception($"Post with ID {id} not found.");
            }
            return post;
        }

        public async Task UpdateAsync(int id, UpdatePostDto dto)
        {
            var response = await httpClient.PutAsJsonAsync($"api/posts/{id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"api/posts/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
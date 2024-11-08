﻿using ApiContracts;

namespace BlazorApp.Services;

public class HttpCommentService
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient httpClient;

        public CommentService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<CommentDto> CreateAsync(CreateCommentDto dto)
        {
            var response = await httpClient.PostAsJsonAsync("api/comments", dto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CommentDto>()!;
        }

        public async Task<IEnumerable<CommentDto>> GetAsync(string? authorUsername)
        {
            var url = "api/comments";
            if (!string.IsNullOrEmpty(authorUsername))
            {
                url += $"?authorUsername={authorUsername}";
            }
            var comments = await httpClient.GetFromJsonAsync<IEnumerable<CommentDto>>(url);
            return comments ?? Enumerable.Empty<CommentDto>();
        }

        public async Task<CommentDto> GetByIdAsync(int id)
        {
            var comment = await httpClient.GetFromJsonAsync<CommentDto>($"api/comments/{id}");
            if (comment == null)
            {
                throw new Exception($"Comment with ID {id} not found.");
            }
            return comment;
        }

        public async Task UpdateAsync(int id, UpdateCommentDto dto)
        {
            var response = await httpClient.PutAsJsonAsync($"api/comments/{id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync(int id)
        {
            var response = await httpClient.DeleteAsync($"api/comments/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
using System.Text.Json;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class CommentFileRepository : ICommentRepository
{
    private readonly string filePath = "comments.json";
    
    public CommentFileRepository()
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, "[]");
        }
    }
    
    public async Task<Comment> AddAsync(Comment comment)
    {
        List<Comment> comments = await LoadCommentsAsync();
        int maxId = comments.Count > 0 ? comments.Max(c => c.Id) : 0;
        comment.Id = maxId + 1;
        comments.Add(comment);
        await SaveCommentsAsync(comments);
        return comment;
    }

    public async Task UpdateAsync(Comment comment)
    {
        List<Comment> comments = await LoadCommentsAsync();
        int index = comments.FindIndex(c => c.Id == comment.Id);
        if (index != -1)
        {
            comments[index] = comment;
            await SaveCommentsAsync(comments);
        }
        else
        {
            throw new KeyNotFoundException($"Comment with ID {comment.Id} not found.");
        }
    }

    public async Task DeleteAsync(int id)
    {
        List<Comment> comments = await LoadCommentsAsync();
        Comment commentToRemove = comments.FirstOrDefault(c => c.Id == id);
        if (commentToRemove != null)
        {
            comments.Remove(commentToRemove);
            await SaveCommentsAsync(comments);
        }
        else
        {
            throw new KeyNotFoundException($"Comment with ID {id} not found.");
        }
    }

    public async Task<Comment> GetSingleAsync(int id)
    {
        List<Comment> comments = await LoadCommentsAsync();
        Comment comment = comments.FirstOrDefault(c => c.Id == id);
        if (comment == null)
        {
            throw new KeyNotFoundException($"Comment with ID {id} not found.");
        }
        return comment;
    }
    
    public async Task<IQueryable<Comment>> GetManyAsync() {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        List<Comment> comments = JsonSerializer.Deserialize<List<Comment>>(commentsAsJson)!;
        return comments.AsQueryable();
    }
    
    private async Task<List<Comment>> LoadCommentsAsync()
    {
        string commentsAsJson = await File.ReadAllTextAsync(filePath);
        return JsonSerializer.Deserialize<List<Comment>>(commentsAsJson) ?? new List<Comment>();
    }

    private async Task SaveCommentsAsync(List<Comment> comments)
    {
        string commentsAsJson = JsonSerializer.Serialize(comments);
        await File.WriteAllTextAsync(filePath, commentsAsJson);
    }
}
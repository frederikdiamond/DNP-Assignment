using ApiContracts;
using ApiContracts.DTOs;

namespace BlazorApp.Services;

public interface IReactionService
{
    Task<ReactionDto> CreateAsync(CreateReactionDto request);
    Task<ReactionDto> GetByIdAsync(int id);
    Task<IEnumerable<ReactionDto>> GetManyAsync(int? postId = null, int? userId = null, int? commentId = null);
    Task<ReactionSummaryDto> GetPostSummaryAsync(int postId);
    Task<ReactionSummaryDto> GetCommentSummaryAsync(int commentId);
    Task<ReactionDto> UpdateVoteAsync(int id, bool isUpvote);
    Task DeleteAsync(int id);
}

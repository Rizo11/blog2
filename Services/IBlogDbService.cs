using blog2.Entities;
using blog2.Models;

namespace blog2.Services;

public interface IBlogDbService
{
    public Task<bool> ExistsPostAsync(Guid id);

    public Task<List<Post>> GetAllPostsAsync();

    public Task<Post> GetPostByIdAsync(Guid id);
    public Task<(bool IsSuccess, Exception Exception)> CreatePostAsync(Post blog);
    public Task<(bool IsSuccess, Exception Exception)> UpdatePostAsync(Post blog);
    public Task<(bool IsSuccess, Exception Exception)> DeletePostAsync(Post blog);
}
using blog2.Entities;
using blog2.Models;

namespace blog2.Services;

public interface IBlogDbService
{
    public Task<bool> ExistsBlogAsync(Guid id);

    public Task<List<Blog>> GetAllBlogsAsync();

    public Task<Blog> GetBlogByIdAsync(Guid id);

    public Task<(bool IsSuccess, Exception Exception)> PostBlogAsync(Blog blog);
    public Task<(bool IsSuccess, Exception Exception)> UpdateBlogAsync(Blog blog);
}
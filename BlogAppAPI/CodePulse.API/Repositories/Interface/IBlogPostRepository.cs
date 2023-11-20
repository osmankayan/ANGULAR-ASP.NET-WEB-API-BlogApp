using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;

namespace CodePulse.API.Repositories.Interface
{
    public interface IBlogPostRepository
    {
        Task<BlogPost> CreateBlogPost(BlogPost blogpost);
        Task<IEnumerable<BlogPost>> GetAllBlogPosts();
        Task<BlogPost?> GetById(Guid id);
        Task<BlogPost?> GetByUrlHandle(string urlHandle);
        Task<BlogPost?> UpdateASync(BlogPost blogpost);
        Task<BlogPost?> DeleteAsync(Guid id);
    }
}

using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        public ApplicationDbContext dbContext;
        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<BlogPost> CreateBlogPost(BlogPost blogpost)
        {
            await dbContext.BlogPosts.AddAsync(blogpost);
            await dbContext.SaveChangesAsync();
            return blogpost;
        }

        public async Task<IEnumerable<BlogPost>> GetAllBlogPosts()
        {
            return await dbContext.BlogPosts.ToListAsync();
        }
    }
}

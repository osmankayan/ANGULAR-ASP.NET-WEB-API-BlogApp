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
            return await dbContext.BlogPosts.Include(x=>x.Categories).ToListAsync();
        }

        public async Task<BlogPost> GetById(Guid id)
        {
            return await dbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(y=>y.Id==id);
        }

        public async Task<BlogPost?> UpdateASync(BlogPost blogpost)
        {
            var existing = await dbContext.BlogPosts.Include(x=>x.Categories).FirstOrDefaultAsync(y=>y.Id==blogpost.Id);
            if (existing is null) { return null; }
            //update blogpost
             dbContext.Entry(existing).CurrentValues.SetValues(blogpost);
            //update categories
            existing.Categories = blogpost.Categories;
            await dbContext.SaveChangesAsync();
            return blogpost;

        }
        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existing= await  dbContext.BlogPosts.FirstOrDefaultAsync(x=>x.Id==id);
            if (existing != null) {
                dbContext.BlogPosts.Remove(existing);
                await dbContext.SaveChangesAsync() ;
                return existing;
            }
            return null;
        }
    }
}

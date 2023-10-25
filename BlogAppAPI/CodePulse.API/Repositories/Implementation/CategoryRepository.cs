using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        ApplicationDbContext context;
        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Category> CreateAsync(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return category;
        }

       

        public async Task<IEnumerable<Category>> ListAsync()
        {
           return await context.Categories.ToListAsync();
        }

        public async Task<Category?> ListById(Guid id)
        {
           return await  context.Categories.FirstOrDefaultAsync(x=>x.Id==id);
            

        }

        public async Task<Category?> UpdateAsync(Category category)
        {
            var existing= await context.Categories.FirstOrDefaultAsync(x=>x.Id==category.Id);
            if (existing!=null) 
            {
                context.Entry(existing).CurrentValues.SetValues(category);
               await context.SaveChangesAsync();
                return category;
            }
            return null;

        }
        public async Task<Category?> DeleteAsync(Guid id)
        {
            var existing = await context.Categories.FirstOrDefaultAsync(x=>x.Id==id);
            if (existing is null)
            {
                return null;
            }
            context.Categories.Remove(existing);
            context.SaveChanges();
            return existing;
        }
    }
}

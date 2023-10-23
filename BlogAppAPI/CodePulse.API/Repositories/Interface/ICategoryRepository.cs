using CodePulse.API.Models.Domain;
using System.Linq;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> ListAsync(); 
        Task<Category?> ListById(Guid id);
        Task<Category?> UpdateAsync(Category category);
    }
}

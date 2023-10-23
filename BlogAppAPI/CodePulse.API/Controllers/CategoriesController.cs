using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryRepository categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {

            this.categoryRepository = categoryRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
        {
            //Dto to domain model
            var Category = new Category { Name = request.Name, UrlHandle = request.UrlHandle };

            await categoryRepository.CreateAsync(Category);

            //domain to dto model
            var response = new CategoryDto { Id = Category.Id, Name = Category.Name, UrlHandle = Category.UrlHandle };

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> ListCategory()
        {

            var categories = await categoryRepository.ListAsync();
            //domain to dto
            var response = new List<CategoryDto>();
            foreach (var category in categories) {

                response.Add(new CategoryDto { Id = category.Id, Name = category.Name, UrlHandle = category.UrlHandle });

            }
            return Ok(response);
        }
        [HttpGet]
        [Route ("{id:Guid}")]
        public async Task<IActionResult> ListCategoryById([FromRoute]Guid id) 
        {
            var existcategory = await categoryRepository.ListById(id);
            if(existcategory is null) 
            {
                return NotFound();
            }

            var response = new CategoryDto
            {
                Id = existcategory.Id,
                Name = existcategory.Name,
                UrlHandle = existcategory.UrlHandle
            };
            return Ok(response);
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateCategory([FromRoute]Guid id, [FromBody]UpdateCategoryRequestDto request) 
        {
            //dto to domain
            var category = new Category {Id=id, Name = request.Name,UrlHandle=request.UrlHandle };
           category= await categoryRepository.UpdateAsync(category);
            if(category is null) { return NotFound(); }

            //domain to dto
            var response=new CategoryDto {Id=category.Id,Name=category.Name,UrlHandle=category.UrlHandle};
            return Ok(response);
        }
    }
}

using Azure.Core;
using CodePulse.API.Models.Domain;
using CodePulse.API.Models.DTO;
using CodePulse.API.Repositories.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePulse.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        IBlogPostRepository blogPostRepository;
        ICategoryRepository categoryRepository;
        public BlogPostController(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost(CreateBlogPostRequestDto request)
        {
            //dto to domain
            var blogPost = new BlogPost
            {
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                Author = request.Author,
                PublishedDate = request.PublishedDate,
                IsVisible = request.IsVisible,
                Categories = new List<Category>()

            };
            foreach (var categoryId in request.categories)
            {
                var existing = await categoryRepository.ListById(categoryId);
                if (existing != null)
                {
                    blogPost.Categories.Add(existing);
                }
            }

            await blogPostRepository.CreateBlogPost(blogPost);

            //domain to dto
            var response = new BlogPostDto {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                Author = blogPost.Author,
                PublishedDate = blogPost.PublishedDate,
                IsVisible = blogPost.IsVisible,
                categories = blogPost.Categories.Select(x => new CategoryDto {

                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogposts = await blogPostRepository.GetAllBlogPosts();
            //domain to dto çünkü veri çekmiyoruz.
            var response = new List<BlogPostDto>();
            foreach (var item in blogposts)
            {
                response.Add(new BlogPostDto {
                    Id = item.Id,
                    Title = item.Title,
                    ShortDescription = item.ShortDescription,
                    Content = item.Content,
                    FeaturedImageUrl = item.FeaturedImageUrl,
                    UrlHandle = item.UrlHandle,
                    Author = item.Author,
                    PublishedDate = item.PublishedDate,
                    IsVisible = item.IsVisible,
                    categories = item.Categories.Select(x => new CategoryDto
                    {

                        Id = x.Id,
                        Name = x.Name,
                        UrlHandle = x.UrlHandle,
                    }).ToList()
                });
            }
            return Ok(response);


        }
        [Route("{id:Guid}")]
        [HttpGet]
        public async Task<IActionResult> GetById([FromRoute]Guid id) 
        {
            var blogPost= await blogPostRepository.GetById(id);
            if (blogPost == null) return NotFound();
            //domain to dto
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                Author = blogPost.Author,
                PublishedDate = blogPost.PublishedDate,
                IsVisible = blogPost.IsVisible,
                categories = blogPost.Categories.Select(x => new CategoryDto
                {

                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(response);

        }
        [Route("{id:Guid}")]
        [HttpPut]
        public async Task<IActionResult> UpdateBlogPostById([FromRoute]Guid id,UpdateBlogPostRequestDto request) 
        {
            var blogPost = new BlogPost
            {
                Id=id,
                Title = request.Title,
                ShortDescription = request.ShortDescription,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                UrlHandle = request.UrlHandle,
                Author = request.Author,
                PublishedDate = request.PublishedDate,
                IsVisible = request.IsVisible,
                Categories = new List<Category>()

            };
            foreach (var categoryId in request.categories)
            {
                var existing = await categoryRepository.ListById(categoryId);
                if (existing != null)
                {
                    blogPost.Categories.Add(existing);
                }
            }
            var uptadedBlogPost= await blogPostRepository.UpdateASync(blogPost);
            if(uptadedBlogPost is null) { return NotFound(); }

            //domain to dto
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                Author = blogPost.Author,
                PublishedDate = blogPost.PublishedDate,
                IsVisible = blogPost.IsVisible,
                categories = blogPost.Categories.Select(x => new CategoryDto
                {

                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(response);




        }
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteBlogPost([FromRoute] Guid id) {
        
            var deletedBlogPost= await blogPostRepository.DeleteAsync(id);

            if (deletedBlogPost is null) {return NotFound();}

            //domain to dto

            var response = new BlogPostDto {
                Id = deletedBlogPost.Id,
                Title = deletedBlogPost.Title,
                ShortDescription = deletedBlogPost.ShortDescription,
                Content = deletedBlogPost.Content,
                FeaturedImageUrl = deletedBlogPost.FeaturedImageUrl,
                UrlHandle = deletedBlogPost.UrlHandle,
                Author =deletedBlogPost.Author,
                PublishedDate =deletedBlogPost.PublishedDate,
                IsVisible = deletedBlogPost.IsVisible
            };
            return Ok(response);

        }
        [HttpGet]
        [Route("{urlHandle}")]
        public async Task<IActionResult> GetBlogpostsByUrlHandle([FromRoute] string urlHandle) 
        {
           var blogPost=await this.blogPostRepository.GetByUrlHandle(urlHandle);
            if (blogPost == null) return NotFound();
            //domain to dto
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                ShortDescription = blogPost.ShortDescription,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                UrlHandle = blogPost.UrlHandle,
                Author = blogPost.Author,
                PublishedDate = blogPost.PublishedDate,
                IsVisible = blogPost.IsVisible,
                categories = blogPost.Categories.Select(x => new CategoryDto
                {

                    Id = x.Id,
                    Name = x.Name,
                    UrlHandle = x.UrlHandle,
                }).ToList()
            };

            return Ok(response);
        }
    }
}

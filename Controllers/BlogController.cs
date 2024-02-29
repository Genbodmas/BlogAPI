using BlogApi.Controllers;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace BlogApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogRepository _blogRepository;

        public BlogController(IBlogRepository blogRepository)
        {
            _blogRepository = blogRepository;
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Blog>>> Search(string name)
        {
            try
            {
                var blogs = await _blogRepository.Search(name);
                return Ok(blogs);
            }
            catch (Exception ex)
            {
              
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogs()
        {
            var blogs = await _blogRepository.GetBlogs();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Blog>> GetBlog(int id)
        {
            var blog = await _blogRepository.GetBlogById(id);
            if (blog == null)
            {
                return NotFound($"Blog with ID {id} not found");
            }
            return Ok(blog);
        }

        [HttpPost]
        public async Task<ActionResult<Blog>> AddBlog(Blog blog)
        {
            if (string.IsNullOrEmpty(blog.Desc) || string.IsNullOrEmpty(blog.Name) || string.IsNullOrEmpty(blog.UserName))
            {
                return BadRequest("Blog Desc, Name, and UserName are required.");
            }

            var addedBlog = await _blogRepository.AddBlog(blog);
            return CreatedAtAction(nameof(GetBlog), new { id = addedBlog.ID }, addedBlog);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<Blog>> ManageBlog(int id, Blog blog)
        {
            var updatedBlog = await _blogRepository.ManageBlog(blog);
            if (updatedBlog == null)
            {
                return NotFound($"Blog with ID {id} not found");
            }
            return Ok($"Blog with ID {id} updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Blog>> DeleteBlog(int id)
        {
            var deletedBlog = await _blogRepository.DeleteBlog(id);
            if (deletedBlog == null)
            {
                return NotFound($"Blog with ID {id} not found");
            }
            return Ok($"Blog with ID {id} deleted successfully");
        }
    }
}

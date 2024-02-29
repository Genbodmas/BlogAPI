using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Models
{
    public class BlogRepository : IBlogRepository
    {
        private readonly AppDbContext _context;

        public BlogRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> Search(string name)
        {
            return await _context.Blogs
                .Where(b => b.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<Blog> GetBlog(string name)
        {
            // Get blog by name
            return await _context.Blogs
                .FirstOrDefaultAsync(b => b.Name == name);
        }

        public async Task<IEnumerable<Blog>> GetBlogs()
        {
            // Get all blogs
            return await _context.Blogs.ToListAsync();
        }

        public async Task<Blog> GetBlogById(int id)
        {
            // Get blog by ID
            return await _context.Blogs.FindAsync(id);
        }

        public async Task<Blog> AddBlog(Blog blog)
        {
            // Add new blog
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog;
        }

        public async Task<Blog> ManageBlog(Blog blog)
        {
            var existingBlog = await _context.Blogs.FindAsync(blog.ID);
            if (existingBlog == null)
            {
                return null; // Blog not found
            }

            // Update blog name 
            if (!string.IsNullOrEmpty(blog.Name))
            {
                existingBlog.Name = blog.Name;
            }

            // Update blog description 
            if (!string.IsNullOrEmpty(blog.Desc))
            {
                existingBlog.Desc = blog.Desc;
            }

            await _context.SaveChangesAsync();
            return existingBlog;
        }

        public async Task<Blog> DeleteBlog(int id)
        {
            // Delete blog by ID
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
            }
            return blog;
        }
    }
}

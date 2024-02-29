namespace BlogApi.Models
{
    public interface IBlogRepository
    {
        // Search blogs by name
        Task<IEnumerable<Blog>> Search(string name);

        // Get blog by name
        Task<Blog> GetBlog(string name);

        // Get all blogs
        Task<IEnumerable<Blog>> GetBlogs();

        // Get blog by ID
        Task<Blog> GetBlogById(int id);

        // Add  new blog
        Task<Blog> AddBlog(Blog blog);

        // Manage  blog // updatw
        Task<Blog> ManageBlog(Blog blog);

        // Delete blog by ID
        Task<Blog> DeleteBlog(int id);
    }
}

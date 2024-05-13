using KZHDotNetCore.RestApiWithNLayer.DataSource;

namespace KZHDotNetCore.RestApiWithNLayer.Features.Blog
{
    // Data Access
    public class DA_Blog
    {
        private readonly AppDbContext _context;

        public DA_Blog()
        {
            _context = new AppDbContext();
        }

        // Get All
        public List<BlogModel> GetBlogs()
        {
           var list = _context.BlogDtos.ToList();
           return list;
        }
        
        // Get By ID
        public BlogModel GetBlog(int id)
        {
            var blogModel = _context.BlogDtos.FirstOrDefault(x => x.BlogId == id);
            return blogModel!;
        }

        // Create
        public int CreateBlog(BlogModel requestBlogModel)
        {
            _context.Add(requestBlogModel);
            var result = _context.SaveChanges();
            return result;
        }

        // Suggestion: RealWorld => BlogResponseModel notic
        // Update
        public int UpdateBlog(int id, BlogModel requestBlogModel)
        {
            var blogModel = _context.BlogDtos.FirstOrDefault(x => x.BlogId == id);
            if (blogModel is null) return 0;

            blogModel.BlogTitle = requestBlogModel.BlogTitle;
            blogModel.BlogAuthor = requestBlogModel.BlogAuthor;
            blogModel.BlogContent = requestBlogModel.BlogContent;
            
            var result = _context.SaveChanges();
            return result;
        }

        // Delete
        public int DeleteBlog(int id)
        {
            var blogModel = _context.BlogDtos.FirstOrDefault(x => x.BlogId == id);
            if (blogModel is null) return 0;

            _context.BlogDtos.Remove(blogModel);
            var result = _context.SaveChanges();
            return result;
        }
    }
}
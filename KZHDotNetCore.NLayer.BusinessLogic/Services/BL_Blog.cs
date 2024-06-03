using KZHDotNetCore.NLayer.DataAccess.Models;
using KZHDotNetCore.NLayer.DataAccess.Services;

namespace KZHDotNetCore.NLayer.BusinessLogic.Services
{
    public class BL_Blog
    {
        private readonly DA_Blog _daBlog;

        public BL_Blog()
        {
            _daBlog = new DA_Blog();
        }

        // Get All
        public List<BlogModel> GetBlogs()
        {
            var list = _daBlog.GetBlogs();
            return list;
        }

        // Get By ID
        public BlogModel GetBlog(int id)
        {
            var blogModel = _daBlog.GetBlog(id);
            return blogModel!;
        }

        // Create
        public int CreateBlog(BlogModel requestBlogModel)
        {
            var result = _daBlog.CreateBlog(requestBlogModel);
            return result;
        }

        // Suggestion: RealWorld => BlogResponseModel notic
        // Update
        public int UpdateBlog(int id, BlogModel requestBlogModel)
        {
            var result = _daBlog.UpdateBlog(id, requestBlogModel);
            return result;
        }

        // Delete
        public int DeleteBlog(int id)
        {
            var result = _daBlog.DeleteBlog(id);
            return result;
        }
    }
}
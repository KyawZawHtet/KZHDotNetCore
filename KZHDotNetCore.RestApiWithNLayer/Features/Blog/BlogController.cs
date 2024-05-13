using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KZHDotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _blBlog;

        public BlogController()
        {
            _blBlog = new BL_Blog();
        }
        
        [HttpGet]
        public IActionResult Read()
        {
            var blogDtos = _blBlog.GetBlogs();
            return Ok(blogDtos);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var blogDtos = _blBlog.GetBlog(id);
            if (blogDtos is null)
            {
                return NotFound("No Data Found.");
            }

            return Ok(blogDtos);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blogModel)
        {
            var result = _blBlog.CreateBlog(blogModel);
            String message = result > 0 ? "Saving Successfully." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blogModel)
        {
            var blogDtos = _blBlog.GetBlog(id);
            if (blogDtos is null)
            {
                return NotFound("No Data Found.");
            }

            var result = _blBlog.UpdateBlog(id, blogModel);

            String message = result > 0 ? "Updating Successfully." : "Updating Failed.";
            return Ok(message);
        }
    
        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(int id, BlogModel blogModel)
        {
            if (blogModel is null)
            {
                return NotFound("No Data Found.");
            }

            var blog = _blBlog.GetBlog(id);
            if (blog is null)
            {
                return NotFound("No Data Found.");
            }
            
            if (blogModel.BlogTitle != null)
            {
                blog.BlogTitle = blogModel.BlogTitle;
            }
            
            if (blogModel.BlogAuthor != null)
            {
                blog.BlogAuthor = blogModel.BlogAuthor;
            }

            if (blogModel.BlogContent != null)
            {
                blog.BlogContent = blogModel.BlogContent;
            }
            
            var result = _blBlog.UpdateBlog(id, blog);

            string message = result > 0 ? "Update successful." : "Update failed.";
            return Ok(message);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var blogDtos = _blBlog.GetBlog(id);
            if (blogDtos is null)
            {
                return NotFound("No Data Found.");
            }

            var result = _blBlog.DeleteBlog(id);

            String message = result > 0 ? "Deleting Successfully." : "Deleting Failed.";
            return Ok(message);
        }
    }
}

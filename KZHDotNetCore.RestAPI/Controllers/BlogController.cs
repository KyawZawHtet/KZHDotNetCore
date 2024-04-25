using KZHDotNetCore.RestAPI.DataSource;
using KZHDotNetCore.RestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KZHDotNetCore.RestAPI.Controllers
{
    // https://localhost:7299 => domain url
    // api/blog => endpoint 
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public BlogController()
        {
            _appDbContext = new AppDbContext();
        }

        [HttpGet]
        public IActionResult Read()
        {
            var blogDtos = _appDbContext.BlogDtos.ToList();
            return Ok(blogDtos);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var blogDtos = _appDbContext.BlogDtos.FirstOrDefault(x => x.BlogId == id);
            if (blogDtos is null)
            {
                return NotFound("No Data Found.");
            }

            return Ok(blogDtos);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blogModel)
        {
            _appDbContext.BlogDtos.Add(blogModel);
            var result = _appDbContext.SaveChanges();
            String message = result > 0 ? "Saving Successfully." : "Saving Failed.";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blogModel)
        {
            var blogDtos = _appDbContext.BlogDtos.FirstOrDefault(x => x.BlogId == id);
            if (blogDtos is null)
            {
                return NotFound("No Data Found.");
            }

            blogDtos.BlogTitle = blogModel.BlogTitle;
            blogDtos.BlogAuthor = blogModel.BlogAuthor;
            blogDtos.BlogContent = blogModel.BlogContent;

            var result = _appDbContext.SaveChanges();

            String message = result > 0 ? "Updating Successfully." : "Updating Failed.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(int id, BlogModel blogModel)
        {
            var blogDtos = _appDbContext.BlogDtos.FirstOrDefault(x => x.BlogId == id);
            if (blogDtos is null)
            {
                return NotFound("No Data Found.");
            }

            if (!string.IsNullOrEmpty(blogModel.BlogTitle))
            {
                blogDtos.BlogTitle = blogModel.BlogTitle;
            }

            if (!string.IsNullOrEmpty(blogModel.BlogAuthor))
            {
                blogDtos.BlogAuthor = blogModel.BlogAuthor;
            }

            if (!string.IsNullOrEmpty(blogModel.BlogContent))
            {
                blogDtos.BlogContent = blogModel.BlogContent;
            }
            
            var result = _appDbContext.SaveChanges();

            String message = result > 0 ? "Updating Successfully." : "Updating Failed.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var blogDtos = _appDbContext.BlogDtos.FirstOrDefault(x => x.BlogId == id);
            if (blogDtos is null)
            {
                return NotFound("No Data Found.");
            }

            _appDbContext.BlogDtos.Remove(blogDtos);
            
            var result = _appDbContext.SaveChanges();

            String message = result > 0 ? "Deleting Successfully." : "Deleting Failed.";
            return Ok(message);
        }
    }
}
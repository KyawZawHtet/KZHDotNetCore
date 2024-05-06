using Dapper;
using KZHDotNetCore.RestAPI.Models;
using KZHDotNetCore.RestAPI.Services;
using KZHDotNetCore.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace KZHDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapper2Controller : ControllerBase
    {
        private readonly DapperService _dapperService =
            new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        // Read
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * FROM Tbl_Blog";
            var list = _dapperService.Query<BlogModel>(query);

            return Ok(list);
        }

        // Read id
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            // string query = "SELECT * FROM Tbl_Blog WHERE blogid = @BlogId";
            // using IDbConnection dbConnection =
            //     new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            // var blogModel = dbConnection
            //     .Query<BlogModel>(query, new BlogModel() { BlogId = id })
            //     .FirstOrDefault();

            var blogModel = FindById(id);

            // if(blogDto == null) (old version)
            if (blogModel is null) // (new version)
            {
                return NotFound("No data found!");
            }

            return Ok(blogModel);
        }

        // Post
        [HttpPost]
        public IActionResult CreateBlog(BlogModel blogModel)
        {
            string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            int result = _dapperService.Execute(query, blogModel);

            string message = result > 0 ? "Save Successfully!" : "Try Again!";

            return Ok(message);
        }

        // Update Put
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blogModel)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data Found!");
            }

            blogModel.BlogId = id;

            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId ";

            int result = _dapperService.Execute(query, blogModel);

            string message = result > 0 ? "Update Successfully!" : "Try Again!";

            return Ok(message);
        }

        // Update Patch
        [HttpPatch("{id}")]
        public IActionResult UpdatePatchBlog(int id, BlogModel blogModel)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data Found!");
            }

            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blogModel.BlogTitle))
            {
                conditions += "[BlogTitle] = @BlogTitle, ";
            }

            if (!string.IsNullOrEmpty(blogModel.BlogAuthor))
            {
                conditions += "[BlogAuthor] = @BlogAuthor, ";
            }

            if (!string.IsNullOrEmpty(blogModel.BlogContent))
            {
                conditions += "[BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return NotFound("No data to update!");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);

            blogModel.BlogId = id;

            string query = $@"UPDATE [dbo].[Tbl_Blog]
   SET {conditions}
 WHERE BlogId = @BlogId ";

            int result = _dapperService.Execute(query, blogModel);

            string message = result > 0 ? "Update Successfully!" : "Try Again!";

            return Ok(message);
        }

        // Delete
        [HttpDelete("{id}")]
        public IActionResult DeleteBlogs(int id)
        {
            var item = FindById(id);
            if (item is null)
            {
                return NotFound("No data Found!");
            }

            string query = @"DELETE FROM Tbl_Blog WHERE BlogId = @BlogId";

            // using IDbConnection dbConnection =
            //     new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            // int result = dbConnection.Execute(query, new BlogModel { BlogId = id });
            
            int result = _dapperService.Execute(query, new BlogModel { BlogId = id });

            string message = result > 0 ? "Delete Successfully!" : "Try Again!";

            return Ok(message);
        }

        private BlogModel? FindById(int id)
        {
            string query = "SELECT * FROM Tbl_Blog WHERE blogid = @BlogId";
            var blogModel = _dapperService.QueryFirstOrDefault<BlogModel>(query, new BlogModel { BlogId = id });

            return blogModel;
        }
    }
}
using KZHDotNetCore.RestAPI.Models;
using KZHDotNetCore.RestAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using KZHDotNetCore.Shared;

namespace KZHDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService =
            new AdoDotNetService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);

        // Get
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * FROM Tbl_Blog";

            // SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            // connection.Open();
            // Console.WriteLine("Connection Open");
            //
            // SqlCommand command = new SqlCommand(query, connection);
            // SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            // DataTable dataTable = new DataTable();
            // sqlDataAdapter.Fill(dataTable);
            //
            // connection.Close();

            // (first way)
            // List<BlogModel> blogModels = new List<BlogModel>();
            // foreach (DataRow dataRow in dataTable.Rows)
            // {
            // BlogModel blogModel = new BlogModel();
            // blogModel.BlogId = Convert.ToInt32(dataRow["BlogId"]);
            // blogModel.BlogTitle = Convert.ToString(dataRow["BlogTitle"]);
            // blogModel.BlogAuthor = Convert.ToString(dataRow["BlogAuthor"]);
            // blogModel.BlogContent = Convert.ToString(dataRow["BlogContent"]);
            //     BlogModel blogModel = new BlogModel
            //     {
            //         BlogId = Convert.ToInt32(dataRow["BlogId"]),
            //         BlogTitle = Convert.ToString(dataRow["BlogTitle"]),
            //         BlogAuthor = Convert.ToString(dataRow["BlogAuthor"]),
            //         BlogContent = Convert.ToString(dataRow["BlogContent"])
            //     };
            //     blogModels.Add(blogModel);
            // }

            // (second way)
            // List<BlogModel> blogModels = dataTable.AsEnumerable().Select(dataRow => new BlogModel
            // {
            //     BlogId = Convert.ToInt32(dataRow["BlogId"]),
            //     BlogTitle = Convert.ToString(dataRow["BlogTitle"]),
            //     BlogAuthor = Convert.ToString(dataRow["BlogAuthor"]),
            //     BlogContent = Convert.ToString(dataRow["BlogContent"])
            // }).ToList();

            var blogModels = _adoDotNetService.Query<BlogModel>(query);

            return Ok(blogModels);
        }

        // Get By ID
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";

            // AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            // parameters[0] = new AdoDotNetParameter("@BlogId", id );
            // var list = _adoDotNetService.Query<BlogModel>(query, parameters);
            var blogModel =
                _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));


            // SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            // connection.Open();
            //
            // SqlCommand command = new SqlCommand(query, connection);
            // command.Parameters.AddWithValue("@BlogId", id);
            // SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            // DataTable dataTable = new DataTable();
            // sqlDataAdapter.Fill(dataTable);
            //
            // connection.Close();

            if (blogModel is null)
            {
                return NotFound("No data found!");
            }

            // DataRow dataRow = dataTable.Rows[0];
            //
            // var blogModel = new BlogModel
            // {
            //     BlogId = Convert.ToInt32(dataRow["BlogId"]),
            //     BlogTitle = Convert.ToString(dataRow["BlogTitle"]),
            //     BlogAuthor = Convert.ToString(dataRow["BlogAuthor"]),
            //     BlogContent = Convert.ToString(dataRow["BlogContent"])
            // };

            return Ok(blogModel);
        }

        // Create
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

            // SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            // connection.Open();
            //
            // SqlCommand sqlCommand = new SqlCommand(query, connection);
            // sqlCommand.Parameters.AddWithValue("@BlogTitle", blogModel.BlogTitle);
            // sqlCommand.Parameters.AddWithValue("@BlogAuthor", blogModel.BlogAuthor);
            // sqlCommand.Parameters.AddWithValue("@BlogContent", blogModel.BlogContent);
            // int result = sqlCommand.ExecuteNonQuery();
            //
            // connection.Close();

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogTitle", blogModel.BlogTitle!),
                new AdoDotNetParameter("@BlogAuthor", blogModel.BlogAuthor!),
                new AdoDotNetParameter("@BlogContent", blogModel.BlogContent!));

            string message = result > 0 ? "Save Successfully!" : "Try Again!";
            // return StatusCode(500, message);
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blogModel)
        {
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId ";

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@BlogId", id),
                new AdoDotNetParameter("@BlogTitle", blogModel.BlogTitle!),
                new AdoDotNetParameter("@BlogAuthor", blogModel.BlogAuthor!),
                new AdoDotNetParameter("@BlogContent", blogModel.BlogContent!)
            );
            
            string message = result > 0 ? "Save Successfully!" : "Try Again!";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(int id, BlogModel blogModel)
        {
            List<string> list = new List<string>();
            List<AdoDotNetParameter> parameters = new List<AdoDotNetParameter>
            {
                new AdoDotNetParameter("@BlogId", id)
            };

            if (blogModel.BlogTitle != null)
            {
                list.Add("[BlogTitle] = @BlogTitle");
                parameters.Add(new AdoDotNetParameter("@BlogTitle", blogModel.BlogTitle));
            }
            if (blogModel.BlogAuthor != null)
            {
                list.Add("[BlogAuthor] = @BlogAuthor");
                parameters.Add(new AdoDotNetParameter("@BlogAuthor", blogModel.BlogAuthor));
            }
            if (blogModel.BlogContent != null)
            {
                list.Add("[BlogContent] = @BlogContent");
                parameters.Add(new AdoDotNetParameter("@BlogContent", blogModel.BlogContent));
            }

            if (!list.Any())
            {
                return BadRequest("No data to update.");
            }

            string item = string.Join(", ", list);
            string query = $@"UPDATE [dbo].[Tbl_Blog]
                      SET {item}
                      WHERE BlogId = @BlogId";

            int result = _adoDotNetService.Execute(query, parameters.ToArray());

            string message = result > 0 ? "Updated Successfully!" : "Update failed, please try again.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string query = @"DELETE FROM Tbl_Blog WHERE BlogId = @BlogId";

            int result = _adoDotNetService.Execute(query, new AdoDotNetParameter("@BlogId",id));

            string message = result > 0 ? "Delete Successfully!" : "Try Again!";
            return Ok(message);
        }
    }
}
using KZHDotNetCore.RestAPI.Models;
using KZHDotNetCore.RestAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace KZHDotNetCore.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNetController : ControllerBase
    {
        // Get
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * FROM Tbl_Blog";

            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            Console.WriteLine("Connection Open");

            SqlCommand command = new SqlCommand(query, connection);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            connection.Close();

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
            List<BlogModel> blogModels = dataTable.AsEnumerable().Select(dataRow => new BlogModel
            {
                BlogId = Convert.ToInt32(dataRow["BlogId"]),
                BlogTitle = Convert.ToString(dataRow["BlogTitle"]),
                BlogAuthor = Convert.ToString(dataRow["BlogAuthor"]),
                BlogContent = Convert.ToString(dataRow["BlogContent"])
            }).ToList();

            return Ok(blogModels);
        }

        // Get By ID
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";

            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@BlogId", id);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            connection.Close();

            if (dataTable.Rows.Count == 0)
            {
                return NotFound("No data found!");
            }

            DataRow dataRow = dataTable.Rows[0];

            var blogModel = new BlogModel
            {
                BlogId = Convert.ToInt32(dataRow["BlogId"]),
                BlogTitle = Convert.ToString(dataRow["BlogTitle"]),
                BlogAuthor = Convert.ToString(dataRow["BlogAuthor"]),
                BlogContent = Convert.ToString(dataRow["BlogContent"])
            };

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

            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", blogModel.BlogTitle);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", blogModel.BlogAuthor);
            sqlCommand.Parameters.AddWithValue("@BlogContent", blogModel.BlogContent);
            int result = sqlCommand.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Save Successfully!" : "Try Again!";
            // return StatusCode(500, message);
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blogModel)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId ";

            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            sqlCommand.Parameters.AddWithValue("@BlogTitle", blogModel.BlogTitle);
            sqlCommand.Parameters.AddWithValue("@BlogAuthor", blogModel.BlogAuthor);
            sqlCommand.Parameters.AddWithValue("@BlogContent", blogModel.BlogContent);
            int result = sqlCommand.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Update Successfully!" : "Try Again!";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdatePatch(int id, BlogModel blogModel)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();
            List<string> list = new List<string>();
            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.Connection = connection;

            if (!string.IsNullOrEmpty(blogModel.BlogTitle))
            {
                list.Add("[BlogTitle] = @BlogTitle");
                sqlCommand.Parameters.AddWithValue("@BlogTitle", blogModel.BlogTitle);
            }

            if (!string.IsNullOrEmpty(blogModel.BlogAuthor))
            {
                list.Add("[BlogAuthor] = @BlogAuthor");
                sqlCommand.Parameters.AddWithValue("@BlogAuthor", blogModel.BlogAuthor);
            }

            if (!string.IsNullOrEmpty(blogModel.BlogContent))
            {
                list.Add("[BlogContent] = @BlogContent");
                sqlCommand.Parameters.AddWithValue("@BlogContent", blogModel.BlogContent);
            }

            if (!list.Any())
            {
                return BadRequest("No valid fields provided to update.");
            }

            string item = string.Join(", ", list);
            string query = $@"UPDATE [dbo].[Tbl_Blog]
                          SET {item}
                          WHERE BlogId = @BlogId";
            sqlCommand.CommandText = query;
            sqlCommand.Parameters.AddWithValue("@BlogId", id);

            int result = sqlCommand.ExecuteNonQuery();
            string message = result > 0 ? "Update Successfully!" : "No changes were made.";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            SqlConnection connection = new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            string query = @"DELETE FROM Tbl_Blog WHERE BlogId = @BlogId";

            SqlCommand sqlCommand = new SqlCommand(query, connection);
            sqlCommand.Parameters.AddWithValue("@BlogId", id);
            int result = sqlCommand.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Delete Successfully!" : "Try Again!";
            return Ok(message);
        }
    }
}
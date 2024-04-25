using System.Data;
using System.Data.SqlClient;
using Dapper;
using KZHDotNetCore.ConsoleApp.Dtos;
using KZHDotNetCore.ConsoleApp.Services;

namespace KZHDotNetCore.ConsoleApp.DapperExamples;

public class DapperExample
{
    public void Run()
    {
        // Read();
        // Edit(1);
        // Edit(13);
        // Create("new title", "new author", "new content");
        // Update(15,"new title another", "new author another", "new content another");
        Delete(14);
    }

    private void Read()
    {
        using IDbConnection dbConnection =
            new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        List<BlogDto> list = dbConnection.Query<BlogDto>("SELECT * FROM Tbl_Blog").ToList();
        foreach (BlogDto blogDto in list)
        {
            Console.WriteLine(blogDto.BlogId);
            Console.WriteLine(blogDto.BlogTitle);
            Console.WriteLine(blogDto.BlogAuthor);
            Console.WriteLine(blogDto.BlogContent);
            Console.WriteLine("-------------------------");
        }
    }

    private void Edit(int id)
    {
        using IDbConnection dbConnection =
            new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        var blogDto = dbConnection
            .Query<BlogDto>("SELECT * FROM Tbl_Blog WHERE blogid = @BlogId", new BlogDto { BlogId = id })
            .FirstOrDefault();
        // if(blogDto == null) (old version)
        if (blogDto is null) // (new version)
        {
            Console.WriteLine("No Data Found");
            return;
        }

        Console.WriteLine(blogDto.BlogId);
        Console.WriteLine(blogDto.BlogTitle);
        Console.WriteLine(blogDto.BlogAuthor);
        Console.WriteLine(blogDto.BlogContent);
        Console.WriteLine("-------------------------");
    }

    private void Create(string blogTitle, string blogAuthor, string blogContent)
    {
        var blogDto = new BlogDto()
        {
            BlogTitle = blogTitle,
            BlogAuthor = blogAuthor,
            BlogContent = blogContent
        };

        string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

        using IDbConnection dbConnection =
            new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        int result = dbConnection.Execute(query, blogDto);

        string message = result > 0 ? "Save Successfully!" : "Try Again!";
        Console.WriteLine(message);
    }

    private void Update(int blogId, string blogTitle, string blogAuthor, string blogContent)
    {
        var blogDto = new BlogDto()
        {
            BlogId = blogId,
            BlogTitle = blogTitle,
            BlogAuthor = blogAuthor,
            BlogContent = blogContent
        };

        string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId ";

        using IDbConnection dbConnection =
            new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        int result = dbConnection.Execute(query, blogDto);

        string message = result > 0 ? "Update Successfully!" : "Try Again!";
        Console.WriteLine(message);
    }
    
    private void Delete(int blogId)
    {
        var blogDto = new BlogDto()
        {
            BlogId = blogId,
        };

        string query = @"DELETE FROM Tbl_Blog WHERE BlogId = @BlogId";

        using IDbConnection dbConnection =
            new SqlConnection(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        int result = dbConnection.Execute(query, blogDto);

        string message = result > 0 ? "Delete Successfully!" : "Try Again!";
        Console.WriteLine(message);
    }
}
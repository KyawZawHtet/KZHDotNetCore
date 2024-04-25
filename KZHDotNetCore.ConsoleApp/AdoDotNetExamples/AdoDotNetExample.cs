using System.Data;
using System.Data.SqlClient;

namespace KZHDotNetCore.ConsoleApp.AdoDotNetExamples;

public class AdoDotNetExample
{
    private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = "DESKTOP-QMAAE64", // Server Name
        InitialCatalog = "KZHDotNetCore", // Database Name
        UserID = "sa",
        Password = "Kyaw279!"
    };

    public void Read()
    {
        SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        Console.WriteLine("Connection Open");

        string query = "SELECT * FROM Tbl_Blog";
        SqlCommand command = new SqlCommand(query, connection);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
        DataTable dataTable = new DataTable();
        sqlDataAdapter.Fill(dataTable);

        connection.Close();
        Console.WriteLine("Connection Close");

        // dataset => datatable
        // datatable => datarow
        // datarow => datacolumn

        foreach (DataRow dataRow in dataTable.Rows)
        {
            Console.WriteLine("Blog ID = > " + dataRow["BlogId"]);
            Console.WriteLine("Blog Title = > " + dataRow["BlogTitle"]);
            Console.WriteLine("Blog Author = > " + dataRow["BlogAuthor"]);
            Console.WriteLine("Blog Content = > " + dataRow["BlogContent"]);
            Console.WriteLine("---------------------------------------------");
        }
    }

    public void Edit(int id)
    {
        SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();
        Console.WriteLine("Connection Open");

        string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId";
        SqlCommand command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@BlogId", id);
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
        DataTable dataTable = new DataTable();
        sqlDataAdapter.Fill(dataTable);

        connection.Close();

        if (dataTable.Rows.Count == 0)
        {
            Console.WriteLine("No Data Found!");
            return;
        }

        DataRow dataRow = dataTable.Rows[0];
        Console.WriteLine("Blog ID = > " + dataRow["BlogId"]);
        Console.WriteLine("Blog Title = > " + dataRow["BlogTitle"]);
        Console.WriteLine("Blog Author = > " + dataRow["BlogAuthor"]);
        Console.WriteLine("Blog Content = > " + dataRow["BlogContent"]);
        Console.WriteLine("---------------------------------------------");
    }

    public void Create(string title, string author, string content)
    {
        SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        string query = @"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

        SqlCommand sqlCommand = new SqlCommand(query, connection);
        sqlCommand.Parameters.AddWithValue("@BlogTitle", title);
        sqlCommand.Parameters.AddWithValue("@BlogAuthor", author);
        sqlCommand.Parameters.AddWithValue("@BlogContent", content);
        int result = sqlCommand.ExecuteNonQuery();

        connection.Close();

        string message = result > 0 ? "Save Successfully!" : "Try Again!";
        Console.WriteLine(message);
    }

    public void Update(int id, string title, string author, string content)
    {
        SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId ";

        SqlCommand sqlCommand = new SqlCommand(query, connection);
        sqlCommand.Parameters.AddWithValue("@BlogId", id);
        sqlCommand.Parameters.AddWithValue("@BlogTitle", title);
        sqlCommand.Parameters.AddWithValue("@BlogAuthor", author);
        sqlCommand.Parameters.AddWithValue("@BlogContent", content);
        int result = sqlCommand.ExecuteNonQuery();

        connection.Close();

        string message = result > 0 ? "Update Successfully!" : "Try Again!";
        Console.WriteLine(message);
    }

    public void Delete(int id)
    {
        SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
        connection.Open();

        string query = @"DELETE FROM Tbl_Blog WHERE BlogId = @BlogId";

        SqlCommand sqlCommand = new SqlCommand(query, connection);
        sqlCommand.Parameters.AddWithValue("@BlogId", id);
        int result = sqlCommand.ExecuteNonQuery();

        connection.Close();

        string message = result > 0 ? "Delete Successfully!" : "Try Again!";
        Console.WriteLine(message);
    }
}
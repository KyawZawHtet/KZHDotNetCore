using System.Data;
using System.Data.SqlClient;
using KZHDotNetCore.ConsoleApp;

Console.WriteLine("Hello, World!");

/*SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder();
stringBuilder.DataSource = "DESKTOP-QMAAE64"; // Server Name
stringBuilder.InitialCatalog = "KZHDotNetCore"; // Database Name
stringBuilder.UserID = "sa";
stringBuilder.Password = "Kyaw279!";
SqlConnection connection = new SqlConnection(stringBuilder.ConnectionString);
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
}*/

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
// adoDotNetExample.Read();
// adoDotNetExample.Create("title", "author", "content");
// adoDotNetExample.Update(13, "test title", "test author", "test content");
// adoDotNetExample.Delete(13);
// adoDotNetExample.Edit(13);
// adoDotNetExample.Edit(1);

// DapperExample dapperExample = new DapperExample();
// dapperExample.Run();

EfCoreExample efCoreExample = new EfCoreExample();
efCoreExample.Run();

Console.ReadKey();




















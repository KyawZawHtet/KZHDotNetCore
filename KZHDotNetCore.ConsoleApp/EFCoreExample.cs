// ReSharper disable All

namespace KZHDotNetCore.ConsoleApp;

public class EfCoreExample
{
    private readonly AppDbContext appDbContext = new AppDbContext();

    public void Run()
    {
        // Read();
        // Edit(1);
        // Edit(20);
        // Create("new title EFCore", "new author EFCore", "new content EFCore");
        // Update(16,"new title EFCore 2", "new author EFCore 2", "new content EFCore 2");
        Delete(15);
    }

    private void Read()
    {
        var list = appDbContext.BlogDtos.ToList();
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
        var blogDto = appDbContext.BlogDtos.FirstOrDefault(x => x.BlogId == id);
        if (blogDto is null)
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
        appDbContext.BlogDtos.Add(blogDto);
        int result = appDbContext.SaveChanges();

        string message = result > 0 ? "Save Successfully!" : "Try Again!";
        Console.WriteLine(message);
    }

    private void Update(int blogId, string blogTitle, string blogAuthor, string blogContent)
    {
        var blogDto = appDbContext.BlogDtos.FirstOrDefault(x => x.BlogId == blogId);
        if (blogDto is null)
        {
            Console.WriteLine("No Data Found");
            return;
        }

        blogDto.BlogTitle = blogTitle;
        blogDto.BlogAuthor = blogAuthor;
        blogDto.BlogContent = blogContent;
        int result = appDbContext.SaveChanges();

        string message = result > 0 ? "Update Successfully!" : "Try Again!";
        Console.WriteLine(message);
    }

    private void Delete(int blogId)
    {
        var blogDto = appDbContext.BlogDtos.FirstOrDefault(x => x.BlogId == blogId);
        if (blogDto is null)
        {
            Console.WriteLine("No Data Found");
            return;
        }

        appDbContext.Remove(blogDto);
        int result = appDbContext.SaveChanges();
        
        string message = result > 0 ? "Delete Successfully!" : "Try Again!";
        Console.WriteLine(message);
    }
}
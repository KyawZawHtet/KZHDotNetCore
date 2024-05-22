using KZHDotNetCore.ConsoleAppRefitExamples;
using Refit;

// try
// {
//     RefitExample refitExample = new RefitExample();
//     await refitExample.RunAsync();
// }
// catch (Exception ex)
// {
//     Console.WriteLine(ex.ToString());
// }

RefitExample refitExample = new RefitExample();
await refitExample.RunAsync();

// var service = RestService.For<IBlogApi>("https://localhost:7177");
// var lst = await service.GetBlogs();
//
// foreach (var item in lst)
// {
//     Console.WriteLine($"Id => {item.BlogId}");
//     Console.WriteLine($"Title => {item.BlogTitle}");
//     Console.WriteLine($"Author => {item.BlogAuthor}");
//     Console.WriteLine($"Content => {item.BlogContent}");
//     Console.WriteLine("------------------------------------");
// }

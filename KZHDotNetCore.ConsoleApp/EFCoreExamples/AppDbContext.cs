using KZHDotNetCore.ConsoleApp.Dtos;
using KZHDotNetCore.ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;

namespace KZHDotNetCore.ConsoleApp.EFCoreExamples;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    }

    public DbSet<BlogDto> BlogDtos { get; set; }
}
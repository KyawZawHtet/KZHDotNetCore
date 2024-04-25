using KZHDotNetCore.RestAPI.Models;
using KZHDotNetCore.RestAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace KZHDotNetCore.RestAPI.DataSource;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    }

    public DbSet<BlogModel> BlogDtos { get; set; }
}
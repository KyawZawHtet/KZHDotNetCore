using KZHDotNetCore.NLayer.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace KZHDotNetCore.NLayer.DataAccess.DataSource;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
    }

    public DbSet<BlogModel> BlogDtos { get; set; }
}
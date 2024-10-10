using ApiAngularCsharp.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiAngularCsharp.Data;

public class AppDbContext : DbContext
{
    DbSet<Pessoa> Pessoas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connectionString: "Data Source=AppDbContext.db");
        base.OnConfiguring(optionsBuilder);
    }
}
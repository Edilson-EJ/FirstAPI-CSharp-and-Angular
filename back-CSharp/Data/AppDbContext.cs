namespace ApiAngularCsharp.Data
{
    using Microsoft.EntityFrameworkCore;
    using ApiAngularCsharp.Models;

    // Define the AppDbContext class which inherits from DbContext
    public class AppDbContext : DbContext
    {
        // Constructor that accepts DbContextOptions and passes it to the base class (DbContext).
        // This allows the context to be configured externally (e.g., via Dependency Injection).
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { } 

        // Define a DbSet for the Pessoa entity, representing the 'Pessoas' table in the database.
        // This will allow CRUD operations on Pessoa entities.
        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;
using Todo.Core.Entities;
namespace Todo.Infrastructure.Persistance;

//EF core database context to manage database connection
// EF core translate between C# and SQL
public class TodoDbContext : DbContext
{
    public DbSet<TodoItem> Todos {get;set; } = null!;
    public TodoDbContext(DbContextOptions<TodoDbContext> options)
        : base(options)
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Configure TodoItem entity
        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
            entity.Property(e => e.IsCompleted).IsRequired();
        });
    }
}

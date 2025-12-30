using Microsoft.EntityFrameworkCore;
using Todo.Core.Entities;
using Todo.Core.Interfaces;
using Todo.Infrastructure.Persistance;

namespace Todo.Infrastructure.Repositories;

//implements data access logic using Entity Framework Core
public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;
    public TodoRepository (TodoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return await _context.Todos.AsNoTracking().ToListAsync();
    }

    public async Task<TodoItem?> GetByIdAsync(Guid id)
    {
        return await _context.Todos.FindAsync(id);
    }
   
    public async Task AddAsync(TodoItem todo)
    {
        _context.Todos.Add(todo);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TodoItem todo)
    {
        _context.Todos.Update(todo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync (Guid id)
    {
        var todo = await _context.Todos.FindAsync(id);
        if (todo ==null) return;

        _context.Todos.Remove(todo);
        await _context.SaveChangesAsync();
    }
}

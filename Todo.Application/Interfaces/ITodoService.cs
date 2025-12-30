using Todo.Core.Entities;
namespace Todo.Application.Interfaces;

//application level operations for todo use cases
public interface ITodoService
{
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<TodoItem> CreateAsync(String title);
    Task UpdateAsync (Guid id, string title);
    Task DeleteAsync (Guid id);
}

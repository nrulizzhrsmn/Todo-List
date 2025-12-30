using Todo.Core.Entities;
namespace Todo.Core.Interfaces;
//defines the specification/operations for accessing todo data
//implementations are handled in the infrastructure layer
public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<TodoItem?>GetByIdAsync(Guid id);
    Task AddAsync(TodoItem todo);
    Task UpdateAsync(TodoItem todo);
    Task DeleteAsync(Guid id);
}
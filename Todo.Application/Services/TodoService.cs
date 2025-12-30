using Todo.Application.Interfaces;
using Todo.Core.Entities;
using Todo.Core.Interfaces;

namespace Todo.Application.Services;

//handles application logic related to todo operations
public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;
    public TodoService(ITodoRepository repository)
    {
        _repository = repository;
    }

    //retrieves all todo items
    public async Task <IEnumerable<TodoItem>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    //create a new todo item
    public async Task<TodoItem> CreateAsync(string title)
    {
        //validation rule (title cannot be empty)
        if (string.IsNullOrWhiteSpace (title))
            throw new ArgumentException("Title cannot be empty");
        
        //standardize input by trimming leading and trailing spaces
        title = title.Trim();
        var todo =new TodoItem(title);
        await _repository.AddAsync(todo);
        return todo;
    }

    //update title of todo item
    public async Task UpdateAsync (Guid id, string title)
    {
        //retrieve the todo to ensure it exists
        var todo = await _repository.GetByIdAsync(id);

        //validation rule (throw exception when todo does not exist)
        if (todo ==null)
            throw new KeyNotFoundException("Todo not found");
        
        //validation rule (throw exception when new title is empty)
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentNullException("New title cannot be empty");
        
        title = title.Trim();
        todo.UpdateTitle(title);
        await _repository.UpdateAsync(todo);
    }

    //delete a todo item by ID
    public async Task DeleteAsync(Guid id)
    {
        //retrieve the todo to ensure it exists
        var todo = await _repository.GetByIdAsync(id);

        //validation rule (throw exception when todo does not exist)
        if (todo ==null)
            throw new KeyNotFoundException("Todo not found");
        
        await _repository.DeleteAsync(id);
    }

}
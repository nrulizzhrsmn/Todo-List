namespace Todo.Core.Entities;
//represent a todo item in the domain/business model
public class TodoItem
{
    public Guid Id {get;private set;}
    public string Title {get;private set;} //title of todo task
    public bool IsCompleted{get; private set;} //whether the task is completed
    public TodoItem(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException ("Todo title cannot be empty.");
        Id=Guid.NewGuid();
        Title = title;
        IsCompleted = false;
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Todo title cannot be empty.");
        Title = title;
    }

    public void MarkCompleted()
    {
        IsCompleted= true;
    }

}
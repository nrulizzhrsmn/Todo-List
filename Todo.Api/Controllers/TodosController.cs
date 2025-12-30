using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Todo.Application.Interfaces;
using Todo.Application.Services;
using Todo.Core.Entities;
namespace Todo.Api.Controllers;

[ApiController] //web API controller
[Route("api/[controller]")] //base route: /api/todos
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService; //contains business logic for operations
    private readonly ILogger<TodosController> _logger; //to record runtime information & diagnostic messages
    public TodosController(ITodoService todoService, ILogger<TodosController> logger)
    {
        _todoService = todoService;
        _logger = logger;
    }

    //retrieve all todo items by sending HTTP Get request to endpoint
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TodoItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
    {
        _logger.LogInformation("fetching all todo items");
        var todos = await _todoService.GetAllAsync();
        return Ok(todos);
    }

    //create todo item by sending HTTP Post 
    [HttpPost]
    [ProducesResponseType(typeof(TodoItem), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TodoItem>> Create([FromBody] CreateTodoRequest request)
    {
        try
        {
            var created = await _todoService.CreateAsync(request.Title);
            return CreatedAtAction(nameof(GetAll), new{id = created.Id }, created);            
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, "invalid todo creation request");
            return BadRequest(new{message = ex.Message});
        }

    }

    //updates the title of existing todo item using ID
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult>Update(Guid id, [FromBody] UpdateTodoRequest request)
    {
        try
        {
            await _todoService.UpdateAsync(id, request.Title);
            return NoContent();            
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning (ex, "invalid todo update request");
            return BadRequest(new {message = ex.Message});
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning (ex, "todo not found for update");
            return NotFound (new {message = ex.Message});
        }

    }

    //deletes a todo item using ID
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
        await _todoService.DeleteAsync(id);
        return NoContent();            
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "Todo not found for deletion");
            return NotFound(new {message = ex.Message});
        }

    }
}

public record CreateTodoRequest(string Title); //request model for creating a todo
public record UpdateTodoRequest(string Title); //request model for updating a todo
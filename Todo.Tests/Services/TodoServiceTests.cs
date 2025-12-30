using Moq;
using Todo.Application.Services;
using Todo.Core.Entities;
using Todo.Core.Interfaces;
using Xunit;

namespace Todo.Tests.Services;

//unit tests for TodoService
//to verify application level business rules and interact with repo as expected
public class TodoServiceTests
{
    //mocked repo to isolate the service from infrastructure
    private readonly Mock<ITodoRepository> _repositoryMock;
    private readonly TodoService _service;
    public TodoServiceTests()
    {
        _repositoryMock = new Mock<ITodoRepository>();
        _service = new TodoService(_repositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_Should_Return_All_Todos()
    {
        // Arrange
        var todos = new List<TodoItem>
        {
            new TodoItem("Test 1"),
            new TodoItem("Test 2")
        };

        _repositoryMock
            .Setup(r => r.GetAllAsync())
            .ReturnsAsync(todos);

        var result = await _service.GetAllAsync(); // Act
        Assert.Equal(2, result.Count());// Assert
        Assert.Contains(result, t => t.Title == "Test 1");
    }

    [Fact]
    public async Task CreateAsync_Should_Create_When_Title_IsValid()
    {
        var title = "My test todo"; //arrange
        var result = await _service.CreateAsync(title); //act
        Assert.NotNull(result); //assert (check expected behaviour)
        Assert.Equal(title,result.Title); //assert

        _repositoryMock.Verify(
            r =>r.AddAsync(It.IsAny<TodoItem>()),Times.Once); //verify repository was called    
    }

    [Fact]
    public async Task CreateAsync_Should_ThrowException_When_Title_IsEmpty()
    {
        var title = ""; //arrange
        await Assert.ThrowsAsync<ArgumentException>(
            ()=> _service.CreateAsync(title)
        ); // await & assert
    }

    [Fact]
    public async Task CreateAsync_Should_ThrowException_When_Title_IsNull()
    {
        await Assert.ThrowsAsync<ArgumentException>(
            ()=> _service.CreateAsync(null!)
        ); // await & assert
    }

    [Fact]
    public async Task CreateAsync_Should_Trim_Title()
    {
        var title = " Test Todo "; //arange
        var result = await _service.CreateAsync(title); //await
        Assert.Equal("Test Todo", result.Title); //assert
    }
    
    [Fact]
    public async Task UpdateAsync_Should_Update_When_Found()
    {
        var id = Guid.NewGuid();
        var todo = new TodoItem("Old title");

        _repositoryMock
         .Setup(r=>r.GetByIdAsync(id))
         .ReturnsAsync(todo);

        await _service.UpdateAsync(id,"New Title"); //act
        Assert.Equal("New Title", todo.Title); // assert
        _repositoryMock.Verify(
            r => r.UpdateAsync(todo),Times.Once
        );
    }

    [Fact]
    public async Task UpdateAsync_Should_ThrowException_When_NotFound()
    {
        var id =Guid.NewGuid();
        _repositoryMock
            .Setup(r=> r.GetByIdAsync(id))
            .ReturnsAsync((TodoItem?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _service.UpdateAsync(id, "New Title")
        ); // await & assert
    }

    [Fact]
    public async Task DeleteAsync_Should_CallRepository_Delete_when_Todo_Exists()
    {
        var id = Guid.NewGuid(); //arrange
        var todo = new TodoItem("Test todo");

        _repositoryMock
            .Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync(todo);

        await _service.DeleteAsync(id); //act
        _repositoryMock.Verify(
            r => r.DeleteAsync(id),Times.Once
        );
    }

    [Fact]
    public async Task DeleteAsync_Should_Throw_When_Todo_Not_Found()
    {
        var id = Guid.NewGuid(); //arrange
        _repositoryMock
            .Setup(r => r.GetByIdAsync(id))
            .ReturnsAsync((TodoItem?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(
            () => _service.DeleteAsync(id)
        );
    }

}
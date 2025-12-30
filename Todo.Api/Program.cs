using Microsoft.EntityFrameworkCore;
using Todo.Application.Interfaces;
using Todo.Application.Services;
using Todo.Core.Interfaces;
using Todo.Infrastructure.Persistance;
using Todo.Infrastructure.Repositories;

//configures services
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//configure database (SQLite)
builder.Services.AddDbContext<TodoDbContext>(options => options.UseSqlite("Data Source= todos.db"));

//register services (dependency injection)
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<ITodoService, TodoService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//configure CORS for React frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
    policy => policy
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    );
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();
app.Run();

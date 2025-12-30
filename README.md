# Todo List – Clean Architecture

This project is a Todo List Web API built with .NET following Clean Architecture principles.  
It demonstrates separation of concerns across layers, uses SQLite for data persistence, includes unit tests, and provides a basic React frontend to interact with the API.

This project was developed as part of a technical assessment.

## Tech Stack
Backend
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQLite
- xUnit & Moq (Unit Testing)

Frontend
- React
- JavaScript (Fetch API)
- Basic CSS styling

## Project Structure
Todo-List/
- Todo.Api # API layer (controllers, configuration, Swagger)
- Todo.Application # Application services and business logic
- Todo.Core # Domain entities and core interfaces
- Todo.Infrastructure # Data access, repositories, EF Core setup
- Todo.Tests # Unit tests for application logic
- Frontend # React frontend to consume the API
- README.md

## The solution follows Clean Architecture to ensure:
- Clear separation of responsibilities
- Testable business logic
- Independence from infrastructure concerns

## Running the Backend API
1. Navigate to the API project:
   cd Todo.Api
2. Run the application:
   dotnet run
3. Open Swagger documentation in the browser (Swagger provides an interactive interface to test all available endpoints):
   http://localhost:<port>/swagger

## Running the Frontend
The frontend allows users to create, view, update, and delete todo items via the API.
1. Navigate to the frontend folder:
   cd Frontend
2. Install dependencies:
   npm install
3. Start the React development server:
   npm start
4. The frontend will be available at:
   http://localhost:300x/ 

## Running Unit Tests
1. From the solution root directory, run:
   dotnet test

## Author
Nurul Izzah Rosman



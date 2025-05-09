# Todo Management API

A simple Todo management application with basic CRUD operations and status management.

## Features

- CRUD operations for todos
- List todos with filtering by status
- Mark todo as complete
- Basic validation (title required, valid dates)

## Technical Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Clean Architecture

## Prerequisites

- .NET 8 SDK
- SQL Server (LocalDB or full version)
- Visual Studio 2022 or Visual Studio Code

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone <repository-url>
   cd TodoApp
   ```

2. Update the connection string in `TodoApp.Api/appsettings.json` if needed.

3. Open a terminal in the solution directory and run:
   ```bash
   dotnet restore
   dotnet build
   ```

4. Navigate to the API project and run the migrations:
   ```bash
   cd TodoApp.Api
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

5. Run the application:
   ```bash
   dotnet run
   ```

The API will be available at `https://localhost:7001` and `http://localhost:5001`.

## API Endpoints

- GET `/api/todos` - Get all todos
- GET `/api/todos/{id}` - Get todo by ID
- GET `/api/todos/status/{status}` - Get todos by status
- POST `/api/todos` - Create a new todo
- PUT `/api/todos/{id}` - Update a todo
- DELETE `/api/todos/{id}` - Delete a todo
- PUT `/api/todos/{id}/complete` - Mark a todo as complete

## Data Model

Todo:
- Id (Guid)
- Title (required, max 100 chars)
- Description (optional)
- Status (Pending/InProgress/Completed)
- Priority (Low/Medium/High)
- DueDate (optional)
- CreatedDate
- LastModifiedDate

## Error Handling

The API includes basic error handling for:
- Not found resources
- Invalid input data
- Database errors

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request 
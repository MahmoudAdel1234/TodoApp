# Todo Management API

A simple Todo management application with basic CRUD operations and status management.

## Features

- CRUD operations for todos
- List todos with filtering and sorting by status, priority, and date
- Mark todo as complete
- Basic validation (title required, valid dates)

## Technical Stack

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Clean Architecture

## Setup Instructions

1. Clone the repository:
   ```bash
   git clone https://github.com/MahmoudAdel1234/TodoApp.git
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

# Todo App Frontend

A modern, responsive web application for managing tasks and todos with advanced filtering and sorting capabilities.

## Features

- ✨ Create, Read, Update, and Delete todos
- 🎯 Set priority levels (Low, Medium, High)
- 📅 Due date management
- 📊 Status tracking (Pending, In Progress, Completed)
- 🔍 Advanced filtering options:
  - Filter by status
  - Filter by priority
  - Filter by date range
- 📈 Sorting capabilities:
  - Sort by different fields
  - Ascending/Descending order
- 🎨 Modern UI with Bootstrap and Bootstrap Icons
- 📱 Responsive design for all devices

## Prerequisites

- Modern web browser (Chrome, Firefox, Safari, Edge)
- Backend API running at `http://localhost:5217`

## Setup

1. Clone the repository
2. Ensure the backend API is running at `http://localhost:5217`
3. Open `index.html` in your web browser

## API Integration

The frontend communicates with the backend API at `http://localhost:5217/api/todos`. Make sure the backend is running before using the application.

## Technologies Used

- HTML5
- CSS3
- JavaScript (ES6+)
- Bootstrap 5
- Bootstrap Icons
- Fetch API for HTTP requests

## Features in Detail

### Todo Management
- Create new todos with title, description, priority, and due date
- Edit existing todos
- Delete todos
- Mark todos as complete

### Filtering
- Filter todos by status (Pending, In Progress, Completed)
- Filter by priority level (Low, Medium, High)
- Filter by date range using start and end dates

### Sorting
- Sort todos by different fields
- Toggle between ascending and descending order

## Error Handling

The application includes comprehensive error handling:
- Network error notifications
- Invalid input validation
- Server error handling
- User-friendly error messages

## Contributing

Feel free to submit issues and enhancement requests! 

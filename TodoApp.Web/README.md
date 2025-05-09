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

## UI Components

- Modal dialogs for adding and editing todos
- Alert notifications for user feedback
- Responsive card layout for todo items
- Priority-based color coding
- Status badges
- Action buttons for edit, complete, and delete operations

## Error Handling

The application includes comprehensive error handling:
- Network error notifications
- Invalid input validation
- Server error handling
- User-friendly error messages

## Contributing

Feel free to submit issues and enhancement requests! 
const API_URL = 'http://localhost:5217/api/todos';

// DOM Elements
const todoForm = document.getElementById('todoForm');
const todoList = document.getElementById('todoList');
const statusFilter = document.getElementById('statusFilter');
const priorityFilter = document.getElementById('priorityFilter');
const startDateFilter = document.getElementById('startDate');
const endDateFilter = document.getElementById('endDate');
const sortBySelect = document.getElementById('sortBy');
const sortOrderSelect = document.getElementById('sortOrder');
const editTodoModal = new bootstrap.Modal(document.getElementById('editTodoModal'));

// Event Listeners
todoForm.addEventListener('submit', handleAddTodo);
statusFilter.addEventListener('change', loadTodos);
priorityFilter.addEventListener('change', loadTodos);
startDateFilter.addEventListener('change', loadTodos);
endDateFilter.addEventListener('change', loadTodos);
sortBySelect.addEventListener('change', loadTodos);
sortOrderSelect.addEventListener('change', loadTodos);
document.getElementById('saveEdit').addEventListener('click', handleEditTodo);

// Load todos on page load
document.addEventListener('DOMContentLoaded', loadTodos);

// Functions
async function loadTodos() {
    try {
        const status = statusFilter.value;
        const priority = priorityFilter.value;
        const sortBy = sortBySelect.value;
        const sortOrder = sortOrderSelect.value;
        
        let startDate = startDateFilter.value || '';
        let endDate = endDateFilter.value || '';

        // Debug logging
        console.log('Filter values:', {
            startDate,
            endDate,
            status,
            priority,
            sortBy,
            sortOrder
        });

        let url = API_URL;
        const params = new URLSearchParams();

        if (status) {
            params.append('status', status);
        }
        if (priority) {
            params.append('priority', priority);
        }
        if (startDate) {
            params.append('startDate', startDate);
        }
        if (endDate) {
            params.append('endDate', endDate);
        }
        params.append('sortBy', sortBy);
        params.append('sortDescending', sortOrder === 'desc');

        url += `?${params.toString()}`;
        
        console.log('Request URL:', url);

        const response = await fetch(url);
        const responseData = await response.json();
        console.log('Response:', responseData);

        if (!response.ok) {
            throw new Error(`Server returned ${response.status}: ${response.statusText}`);
        }
        
        if (!Array.isArray(responseData)) {
            throw new Error('Invalid response format: expected an array');
        }
        
        displayTodos(responseData);
    } catch (error) {
        console.error('Error loading todos:', error);
        showAlert('Error loading todos: ' + error.message, 'danger');
        todoList.innerHTML = '<div class="alert alert-danger">Failed to load todos</div>';
    }
}

function displayTodos(todos) {
    todoList.innerHTML = '';
    todos.forEach(todo => {
        const todoElement = createTodoElement(todo);
        todoList.appendChild(todoElement);
    });
}

function createTodoElement(todo) {
    const div = document.createElement('div');
    const priorityMap = ['Low', 'Medium', 'High'];
    const statusMap = ['Pending', 'InProgress', 'Completed'];
    
    div.className = `card mb-3 shadow-sm todo-item priority-${priorityMap[todo.priority].toLowerCase()} ${todo.status === 2 ? 'completed' : ''}`;
    
    const statusClass = {
        0: 'bg-warning text-dark',
        1: 'bg-info text-dark',
        2: 'bg-success text-white'
    }[todo.status];

    div.innerHTML = `
        <div class="card-body">
            <div class="d-flex justify-content-between align-items-start">
                <div class="flex-grow-1">
                    <h5 class="card-title mb-2">${todo.title}</h5>
                    <div class="mb-2">
                        <span class="badge ${statusClass} me-2 px-3 py-2 rounded-pill">${statusMap[todo.status]}</span>
                        <span class="badge bg-secondary px-3 py-2 rounded-pill">${priorityMap[todo.priority]}</span>
                    </div>
                    ${todo.dueDate ? `
                        <div class="text-muted mb-2">
                            <i class="bi bi-calendar-event"></i> 
                            Due: ${new Date(todo.dueDate).toLocaleString()}
                        </div>
                    ` : ''}
                    ${todo.description ? `
                        <p class="card-text text-muted mb-3">${todo.description}</p>
                    ` : ''}
                </div>
                <div class="btn-group">
                    <button class="btn btn-outline-primary btn-sm" onclick="editTodo('${todo.id}')" title="Edit">
                        <i class="bi bi-pencil"></i>
                    </button>
                    <button class="btn btn-outline-success btn-sm" onclick="markAsComplete('${todo.id}')" title="Complete">
                        <i class="bi bi-check-lg"></i>
                    </button>
                    <button class="btn btn-outline-danger btn-sm" onclick="deleteTodo('${todo.id}')" title="Delete">
                        <i class="bi bi-trash"></i>
                    </button>
                </div>
            </div>
        </div>
    `;
    
    return div;
}

// Update DOM Elements section
const addTodoModal = new bootstrap.Modal(document.getElementById('addTodoModal'));
const addTodoBtn = document.getElementById('showAddTodoForm');

// Update Event Listeners section
addTodoBtn.addEventListener('click', () => {
    document.getElementById('todoForm').reset(); // Reset form before showing
    addTodoModal.show();
});

// Update handleAddTodo function
async function handleAddTodo(event) {
    event.preventDefault();
    
    const todo = {
        title: document.getElementById('title').value,
        description: document.getElementById('description').value,
        priority: parseInt(document.getElementById('priority').value),
        dueDate: document.getElementById('dueDate').value || null
    };

    try {
        const response = await fetch(API_URL, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(todo)
        });

        if (response.ok) {
            addTodoModal.hide();
            document.getElementById('todoForm').reset();
            loadTodos();
            showAlert('Todo added successfully', 'success');
        } else {
            throw new Error('Failed to add todo');
        }
    } catch (error) {
        console.error('Error adding todo:', error);
        showAlert('Error adding todo', 'danger');
    }
}

async function editTodo(id) {
    try {
        const response = await fetch(`${API_URL}/${id}`);
        const todo = await response.json();

        document.getElementById('editId').value = todo.id;
        document.getElementById('editTitle').value = todo.title;
        document.getElementById('editDescription').value = todo.description;
        document.getElementById('editStatus').value = todo.status;
        document.getElementById('editPriority').value = todo.priority;
        document.getElementById('editDueDate').value = todo.dueDate ? todo.dueDate.slice(0, 16) : '';

        editTodoModal.show();
    } catch (error) {
        console.error('Error loading todo:', error);
        showAlert('Error loading todo', 'danger');
    }
}

async function handleEditTodo() {
    const id = document.getElementById('editId').value;
    const priorityMap = {
        'Low': 0,
        'Medium': 1,
        'High': 2
    };
    
    const todo = {
        title: document.getElementById('editTitle').value,
        description: document.getElementById('editDescription').value,
        status: parseInt(document.getElementById('editStatus').value),
        priority: priorityMap[document.getElementById('editPriority').value],
        dueDate: document.getElementById('editDueDate').value || null
    };

    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(todo)
        });

        if (response.ok) {
            editTodoModal.hide();
            loadTodos();
            showAlert('Todo updated successfully', 'success');
        } else {
            throw new Error('Failed to update todo');
        }
    } catch (error) {
        console.error('Error updating todo:', error);
        showAlert('Error updating todo', 'danger');
    }
}

async function deleteTodo(id) {
    if (!confirm('Are you sure you want to delete this todo?')) {
        return;
    }

    try {
        const response = await fetch(`${API_URL}/${id}`, {
            method: 'DELETE'
        });

        if (response.ok) {
            loadTodos();
            showAlert('Todo deleted successfully', 'success');
        } else {
            throw new Error('Failed to delete todo');
        }
    } catch (error) {
        console.error('Error deleting todo:', error);
        showAlert('Error deleting todo', 'danger');
    }
}

async function markAsComplete(id) {
    try {
        const response = await fetch(`${API_URL}/${id}/complete`, {
            method: 'PUT'
        });

        if (response.ok) {
            loadTodos();
            showAlert('Todo marked as complete', 'success');
        } else {
            throw new Error('Failed to mark todo as complete');
        }
    } catch (error) {
        console.error('Error marking todo as complete:', error);
        showAlert('Error marking todo as complete', 'danger');
    }
}

function showAlert(message, type) {
    const alertDiv = document.createElement('div');
    alertDiv.className = `alert alert-${type} alert-dismissible fade show position-fixed top-0 end-0 m-3 shadow`;
    alertDiv.style.zIndex = '1050';
    alertDiv.innerHTML = `
        <div class="d-flex align-items-center">
            <i class="bi ${type === 'success' ? 'bi-check-circle' : 'bi-exclamation-circle'} me-2"></i>
            <div>${message}</div>
            <button type="button" class="btn-close ms-3" data-bs-dismiss="alert"></button>
        </div>
    `;
    document.body.appendChild(alertDiv);
    setTimeout(() => alertDiv.remove(), 3000);
}
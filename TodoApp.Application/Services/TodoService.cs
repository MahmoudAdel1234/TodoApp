using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Core.Entities;
using TodoApp.Core.Interfaces;

namespace TodoApp.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public async Task<IEnumerable<TodoDto>> GetAllAsync(
            TodoStatus? status = null,
            TodoPriority? priority = null,
            string? startDate = null,
            string? endDate = null,
            string sortBy = "CreatedDate",
            bool sortDescending = true)
        {
            var todos = await _todoRepository.GetAllAsync(status, priority, startDate, endDate, sortBy, sortDescending);
            return todos.Select(MapToDto);
        }

        public async Task<TodoDto> GetByIdAsync(Guid id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            return todo != null ? MapToDto(todo) : null;
        }

        public async Task<TodoDto> CreateAsync(CreateTodoDto createTodoDto)
        {
            var todo = new Todo
            {
                Id = Guid.NewGuid(),
                Title = createTodoDto.Title,
                Description = createTodoDto.Description,
                Status = TodoStatus.Pending,
                Priority = createTodoDto.Priority,
                DueDate = createTodoDto.DueDate,
                CreatedDate = DateTime.UtcNow,
                LastModifiedDate = DateTime.UtcNow
            }
;

            var createdTodo = await _todoRepository.AddAsync(todo);
            return MapToDto(createdTodo);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateTodoDto updateTodoDto)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null)
                return false;

            todo.DueDate = updateTodoDto.DueDate;
            todo.Priority = updateTodoDto.Priority;
            todo.Status = updateTodoDto.Status;
            todo.Title = updateTodoDto.Title;
            todo.Description = updateTodoDto.Description;
            todo.LastModifiedDate = DateTime.UtcNow;



            await _todoRepository.UpdateAsync(todo);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null)
                return false;

            await _todoRepository.DeleteAsync(id);
            return true;
        }

        public async Task<bool> MarkAsCompleteAsync(Guid id)
        {
            var todo = await _todoRepository.GetByIdAsync(id);
            if (todo == null)
                return false;

            todo.Status = TodoStatus.Completed;
            await _todoRepository.UpdateAsync(todo);
            return true;
        }

        private static TodoDto MapToDto(Todo todo)
        {
            return new TodoDto
            {
                Id = todo.Id,
                Title = todo.Title,
                Description = todo.Description,
                Status = todo.Status,
                Priority = todo.Priority,
                DueDate = todo.DueDate,
                CreatedDate = todo.CreatedDate,
                LastModifiedDate = todo.LastModifiedDate
            };
        }
    }
} 
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Application.DTOs;
using TodoApp.Core.Entities;

namespace TodoApp.Application.Interfaces
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoDto>> GetAllAsync(
            TodoStatus? status = null,
            TodoPriority? priority = null,
            string? startDate = null,
            string? endDate = null,
            string sortBy = "CreatedDate",
            bool sortDescending = true);
        Task<TodoDto> GetByIdAsync(Guid id);
        Task<TodoDto> CreateAsync(CreateTodoDto createTodoDto);
        Task<bool> UpdateAsync(Guid id,UpdateTodoDto updateTodoDto);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> MarkAsCompleteAsync(Guid id);
    }
} 
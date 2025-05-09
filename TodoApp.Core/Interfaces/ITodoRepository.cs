using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Core.Entities;

namespace TodoApp.Core.Interfaces
{
    public interface ITodoRepository
    {
        Task<Todo> GetByIdAsync(Guid id);
        Task<IEnumerable<Todo>> GetAllAsync(
            TodoStatus? status = null,
            TodoPriority? priority = null,
            string? startDate = null,
            string? endDate = null,
            string sortBy = "CreatedDate",
            bool sortDescending = true);
        Task<IEnumerable<Todo>> GetByStatusAsync(TodoStatus status);
        Task<Todo> AddAsync(Todo todo);
        Task<Todo> UpdateAsync(Todo todo);
        Task DeleteAsync(Guid id);
    }
} 